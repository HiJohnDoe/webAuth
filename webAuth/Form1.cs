using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

namespace webAuth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string err_status_0 = "无法连接到远程服务器";
        string err_status_1 = "请求被中止: 操作超时。";
        string err_status_2 = "操作超时";
        string err_status_3 = "无法连接到远程服务器";

        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// 限制启动一次
            /// </summary>
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(Application.CompanyName);
            if (processes.Length > 1)
            {
                System.Environment.Exit(1);
            }
            comboBox_username.Focus();
            do_remember_load();
            Form1_Shown();
        }

        /// <summary>
        /// 窗口加载完毕事件
        /// </summary>
        private async void Form1_Shown()
        {
            await Task.Delay(1000);
            timer1.Start();
        }
        private Dictionary<string, User> users = new Dictionary<string, User>();

        /// <summary>
        /// 登录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_login_Click(object sender, EventArgs e)
        {
            bool is_exist_name = string.IsNullOrEmpty(comboBox_username.Text.Trim());
            bool is_exist_pwd = string.IsNullOrEmpty(textBox_password.Text.Trim());
            bool flag_name = false;
            bool flag_pwd = true;

            if (is_exist_name)
            {
                MessageBox.Show("姓名不能为空!");
                flag_name = false;
                return;
            }else
            {
                globalData.user_name = comboBox_username.Text;
                flag_name = true;
            }

            if (is_exist_pwd)
            {
                MessageBox.Show("密码不能为空!");
                flag_pwd = false;
                return;
            }
            else
            {
                globalData.user_password = textBox_password.Text;
                flag_pwd = true;
            }
            do_remember();
            if ( flag_name && flag_pwd)
            {
                string ip = GetLocalIp();
                string login_status = err_status_0;
                while (login_status == err_status_0 || login_status == err_status_1 || login_status == err_status_2 || login_status == err_status_3)
                {
                    login_status = login_action(ip);
                    Thread.Sleep(3000);
                }
                
                globalData.login_cookie = login_status;
                bool is_exsit = login_status.Contains("0#");
                if (is_exsit)
                {
                    int index = login_status.IndexOf("0#");
                    if (index < 1) // 判断0#的位置，以防用户名或密码包含0#而影响登录
                    {
                        MessageBox.Show("账号或密码错误!");
                    }else
                    {
                        globalData.login_status_arr = login_status.Split('&');
                        Form2 f2 =  new Form2();
                        f2.Show();
                        this.Hide();
                        timer1.Stop();
                    }
                }else
                {
                    globalData.login_status_arr = login_status.Split('&');
                    globalData.keeplive_exit = false;
                    Form2 f2 = new Form2();
                    f2.Show();
                    this.Hide();
                    timer1.Stop();
                }
            }
        }

        /// <summary>
        /// 登录动作
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private string login_action(string ip)
        {
            Encoding encoding = Encoding.UTF8;
            string postData = "username=" + globalData.user_name + "&password=" + globalData.user_password + "&submit=submit";
            byte[] data = encoding.GetBytes(postData);

            //Prepare web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.10.12:8000/portal.cgi");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36 Edg/85.0.564.44";
            request.Referer = "http://10.10.10.12:8000/portal/local/?weburl=http%3A%2F%2Fwww.baidu.com";
            request.KeepAlive = true;
            request.Accept = "*/*";
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
            request.Headers.Add("Cookie", "logincookie=");
            request.Headers.Add("Origin", "http://10.10.10.12:8000");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.ContentLength = data.Length;
            request.Proxy = null;
            request.Timeout = 3000;

            Stream newStream;
            try
            {
                newStream = request.GetRequestStream();
            }
            catch (Exception ex)
            {
                return ex.Message;    // 无法连接到远程服务器
            }
            
            //Send the data
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            //Get response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
            string loginStatus = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            
            return loginStatus;
        }

        /// <summary>
        /// 分割登录反馈
        /// </summary>
        /// <param name="login_Status"></param>
        /// <returns></returns>
        private string[] getSplitRes(string login_Status)
        {
            string[] loginStatusArr = login_Status.Split('&');
            return loginStatusArr;
        }

        /// <summary>
        /// 显示密码check事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_showpwd_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_showpwd.Checked)
            {
                textBox_password.UseSystemPasswordChar = false;
            }
            else
            {
                textBox_password.UseSystemPasswordChar = true;
            }
        }
        
        /// <summary>
        /// 将账号和密码保存在本地公共用户的文档中
        /// </summary>
        private void do_remember()
        {
            User user = new User();
            FileStream fs = new FileStream("C:\\Users\\Public\\Documents\\webAuth.bin", FileMode.OpenOrCreate);   //登录时，如果没有data.bin文件就创建，有就打开
            BinaryFormatter bf = new BinaryFormatter();
            user.LoginID = comboBox_username.Text.Trim();    //将用户名保存在实体类属性中
            if (this.checkBox_remember.Checked)    //如果选择了记住密码功能，就在文件中保存密码
            {
                user.Pwd = textBox_password.Text.Trim();
            }else
            {
                user.Pwd = "";
            }
            user.AutoLogin = this.checkBox_autologin.Checked;//在文件中保存自动登录状态
            user.AutoStart = this.checkBox_autostart.Checked;//在文件中保存开机启动状态
            try
            {
                if (users.ContainsKey(user.LoginID))
                {
                    users.Remove(user.LoginID);
                }
                users.Add(user.LoginID, user);   //添加用户信息到集合
                bf.Serialize(fs, users);     //写入文件
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        /// <summary>
        /// 在本地公共用户的文档中加载账号和密码
        /// </summary>
        private void do_remember_load()
        {
            //读取文件流对象
            FileStream fs = new FileStream("C:\\Users\\Public\\Documents\\webAuth.bin", FileMode.OpenOrCreate);
            if (fs.Length > 0)
            {
                BinaryFormatter bf = new BinaryFormatter();
                //读出存在Data.bin 里的用户信息
                users = bf.Deserialize(fs) as Dictionary<string, User>;   //读出存在data.bin里的用户信息
                //循环添加到Combox
                foreach (User user in users.Values)
                {
                    //comboBox_username.Items.Add(user.LoginID);
                    comboBox_username.Items.Insert(0, user.LoginID);
                }
            }
            fs.Close();

            //combox 用户名默认选中第一个
            if (comboBox_username.Items.Count > 0)
            {
                comboBox_username.SelectedIndex = comboBox_username.Items.Count - 1;
            }
        }

        /// <summary>
        /// 当用户名发生改变时的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_username_SelectedValueChanged(object sender, EventArgs e)   //点击ComboBox显示对应的密码
        {
            FileStream fs = new FileStream("C:\\Users\\Public\\Documents\\webAuth.bin", FileMode.OpenOrCreate);   //首先读取记住密码的配置文件
            if (fs.Length > 0)
            {
                BinaryFormatter bf = new BinaryFormatter();
                users = bf.Deserialize(fs) as Dictionary<string, User>;
                for (int i = 0; i < users.Count; i++)
                {
                    if (this.comboBox_username.Text != "")
                    {
                        if (users.ContainsKey(comboBox_username.Text) && users[comboBox_username.Text].Pwd != "")
                        {
                            this.textBox_password.Text = users[comboBox_username.Text].Pwd;
                            this.checkBox_remember.Checked = true;
                            this.checkBox_autologin.Checked = users[comboBox_username.Text].AutoLogin;
                            this.checkBox_autostart.Checked = users[comboBox_username.Text].AutoStart;
                        }
                        else
                        {
                            this.textBox_password.Text = "";
                            this.checkBox_remember.Checked = false;
                            this.checkBox_autologin.Checked = users[comboBox_username.Text].AutoLogin;
                            this.checkBox_autostart.Checked = users[comboBox_username.Text].AutoStart;
                        }
                    }
                }
            }
            fs.Close();
        }

        /// <summary>
        /// 读取本机IP
        /// </summary>
        /// <returns></returns>
        public string GetLocalIp()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        /// <summary>
        /// 自动登录按钮改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_autostart_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_autostart.Checked)    //如果选择了记住密码功能，就在文件中保存密码
            {
                autoStart autoStart = new autoStart();
                autoStart.SetMeAutoStart(true);
            }
            else
            {
                autoStart autoStart = new autoStart();
                autoStart.SetMeAutoStart(false);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox_autologin.Checked == true)
            {
                if (globalData.auto_login == true)
                {

                    if (globalData.relogin == true)
                    {
                        button_login.Text = "重连中";
                        globalData.relogin = false;
                    }
                    globalData.auto_login = false;
                    button_login.PerformClick();
                    button_login.Text = "登录";
                }
            }
        }
    }

    [Serializable]  //要先将User类设为可以序列化（即在类的前面加上[Serializable]）
    class User
    {
        //记住密码
        private string _loginID;
        public string LoginID
        {
            get { return _loginID; }
            set { _loginID = value; }
        }

        private string _pwd;
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        private bool _autoLogin;
        public bool AutoLogin
        {
            get { return _autoLogin; }
            set { _autoLogin = value; }
        }

        private bool _autoStart;
        public bool AutoStart
        {
            get { return _autoStart; }
            set { _autoStart = value; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

namespace webAuth
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private static string keeplive_status, login_cookies;
        private static string[] login_status_arr;
        DateTime dt_start;

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan dtTo = DateTime.Now - dt_start;
            if (dtTo.Days >0)
            {
                label_keeplive_times.Text = dtTo.Days.ToString() + "天 " + dtTo.Hours.ToString() + "小时 " + dtTo.Minutes.ToString() + "分 " + dtTo.Seconds + "秒";
            }
            else
            {
                label_keeplive_times.Text = dtTo.Hours.ToString() + "小时 " + dtTo.Minutes.ToString() + "分 " + dtTo.Seconds + "秒";
            }
            if(globalData.keeplive_exit)
            {
                button_logout.PerformClick();
            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            dt_start = DateTime.Now;
            timer1.Start();

            login_status_arr = globalData.login_status_arr;
            login_cookies = globalData.login_cookie;
            Thread mythread = new Thread(keep_live);
            mythread.Start();
        }

        private void keep_live()
        {
            string err_status_1 = "请求被中止: 操作超时。";
            string err_status_2 = "操作超时";
            string err_status_3 = "无法连接到远程服务器";
            keeplive_status = err_status_1;

            while (!globalData.keeplive_exit)
            {
                //Thread.Sleep(90000);// 休眠90秒后， 发送keeplive保持激活状态
                Thread.Sleep(5000);// 休眠5秒后， 发送keeplive保持激活状态 debug用
                keeplive_status = keep_live_action(login_status_arr);
                if (keeplive_status == err_status_1 || keeplive_status == err_status_2 || keeplive_status == err_status_3)
                {
                    globalData.keeplive_exit = true;
                    globalData.auto_login = true;
                }
            }
        }
        
        private string keep_live_action(string[] login_status_arr)
        {

            Encoding encoding = Encoding.UTF8;
            string postData = "username=" + globalData.user_name + "&secret=" + login_status_arr[3] + "&submit=submit";
            byte[] data = encoding.GetBytes(postData);

            //Prepare web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.10.12:8000/keepalive.cgi");
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

            string keepliveStatus;
            try
            {
                Stream newStream = request.GetRequestStream();
            
                //Send the data
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                //Get response
                HttpWebResponse response;

                response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
                keepliveStatus = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;    // 请求被中止: 操作超时。
            }
            
            return keepliveStatus;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)//当用户点击窗体右上角X按钮或(Alt + F4)时 发生          
            {
                e.Cancel = true;
                this.ShowInTaskbar = false;
                this.myIcon.Icon = this.Icon;
                this.Hide();
            }
        }

        private void myIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.myMenu.Show();
            }
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void toolMenuCancel_Click(object sender, EventArgs e)
        {
            string logout_status = logout_action(login_status_arr);
            Application.Exit();
        }

        private void button_logout_Click(object sender, EventArgs e)
        {
            string logout_status = logout_action(login_status_arr);
            globalData.keeplive_exit = true;
            Form1 f1 = new Form1();
            this.Dispose();
            this.Close();
            f1.Show();
        }

        private string logout_action(string[] login_status_arr)
        {
            Encoding encoding = Encoding.UTF8;
            string postData = "username=" + globalData.user_name + "&secret=" + login_status_arr[3] + "&submit=submit";
            byte[] data = encoding.GetBytes(postData);

            //Prepare web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.10.10.12:8000/logout.cgi");
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

            string keepliveStatus;
            try
            {
                Stream newStream = request.GetRequestStream();

                //Send the data
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                //Get response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
                keepliveStatus = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;    // 请求被中止: 操作超时。
            }
            return keepliveStatus;
        }

    }
}

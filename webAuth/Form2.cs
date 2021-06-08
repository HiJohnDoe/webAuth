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
using System.Diagnostics;

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
        Process proc = null;
        Thread th_keeplive;
        string err_status_1 = "请求被中止: 操作超时。";
        string err_status_2 = "操作超时";
        string err_status_3 = "无法连接到远程服务器";
        int timer_int = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan dtTo = DateTime.Now - dt_start;
            timer_int++;
            if (dtTo.Days >0)
            {
                label_keeplive_times.Text = dtTo.Days.ToString() + "天 " + dtTo.Hours.ToString() + "小时 " + dtTo.Minutes.ToString() + "分 " + dtTo.Seconds + "秒";
            }
            else
            {
                label_keeplive_times.Text = dtTo.Hours.ToString() + "小时 " + dtTo.Minutes.ToString() + "分 " + dtTo.Seconds + "秒";
            }
            if (timer_int == 60)
            {
                timer_int = 0;
                th_keeplive = new Thread(keep_live);
                th_keeplive.Start();
            }
            if(globalData.keeplive_exit)
            {
                exit_form2();
            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            dt_start = DateTime.Now;

            login_status_arr = globalData.login_status_arr;
            login_cookies = globalData.login_cookie;

            timer1.Start();
            //Console.WriteLine("do_m 1 ");
            do_m();
            //Console.WriteLine("do_m 2 ");
        }

        private void do_m()
        {
            try
            {
                string targetDir = string.Format(@"E:\Program Files\ethminer-0.18.0-cuda10.0-windows-amd64\bin\");//这是bat存放的目录
                // string targetDir1 = AppDomain.CurrentDomain.BaseDirectory; //或者这样写，获取程序目录
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = "start_silent.bat";//bat文件名称
                //proc.StartInfo.Arguments = string.Format("10");//this is argument
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行
                proc.Start();
                //proc.WaitForExit();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void keep_live()
        {
            keeplive_status = err_status_1;

            if (!globalData.keeplive_exit)
            {
                keeplive_status = keep_live_action(login_status_arr);
                
                if (keeplive_status == err_status_1 || keeplive_status == err_status_2 || keeplive_status == err_status_3)
                {
                    globalData.keeplive_exit = true;
                    globalData.relogin = true;
                    globalData.auto_login = true;
                }
            }
            return;
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
            request.Timeout = 10000;

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

        private void exit_m()
        {
            Process[] pProcess;
            pProcess = Process.GetProcesses();
            for (int i = 1; i <= pProcess.Length - 1; i++)
            {
                if (pProcess[i].ProcessName == "ethminer")   //任务管理器应用程序的名
                {
                    pProcess[i].Kill();
                    break;
                }
            }
        }

        private void button_logout_Click(object sender, EventArgs e)
        {
            string logout_status = logout_action(login_status_arr);
            globalData.keeplive_exit = true;
            Form1 f1 = new Form1();
            this.Dispose();
            this.Close();
            proc.Close();
            exit_m();
            f1.Show();
        }

        private void exit_form2()
        {
            globalData.keeplive_exit = true;
            Form1 f1 = new Form1();
            this.Dispose();
            this.Close();
            proc.Close();
            exit_m();
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
            request.Timeout = 10000;

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

namespace webAuth
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel_login = new System.Windows.Forms.Panel();
            this.checkBox_autostart = new System.Windows.Forms.CheckBox();
            this.comboBox_username = new System.Windows.Forms.ComboBox();
            this.checkBox_autologin = new System.Windows.Forms.CheckBox();
            this.checkBox_showpwd = new System.Windows.Forms.CheckBox();
            this.checkBox_remember = new System.Windows.Forms.CheckBox();
            this.button_login = new System.Windows.Forms.Button();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.panel_logo = new System.Windows.Forms.Panel();
            this.panel_login.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_login
            // 
            this.panel_login.Controls.Add(this.checkBox_autostart);
            this.panel_login.Controls.Add(this.comboBox_username);
            this.panel_login.Controls.Add(this.checkBox_autologin);
            this.panel_login.Controls.Add(this.checkBox_showpwd);
            this.panel_login.Controls.Add(this.checkBox_remember);
            this.panel_login.Controls.Add(this.button_login);
            this.panel_login.Controls.Add(this.textBox_password);
            this.panel_login.Controls.Add(this.label_password);
            this.panel_login.Controls.Add(this.label_name);
            this.panel_login.Location = new System.Drawing.Point(12, 84);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(351, 180);
            this.panel_login.TabIndex = 0;
            // 
            // checkBox_autostart
            // 
            this.checkBox_autostart.AutoSize = true;
            this.checkBox_autostart.Location = new System.Drawing.Point(241, 94);
            this.checkBox_autostart.Name = "checkBox_autostart";
            this.checkBox_autostart.Size = new System.Drawing.Size(72, 16);
            this.checkBox_autostart.TabIndex = 7;
            this.checkBox_autostart.Text = "开机启动";
            this.checkBox_autostart.UseVisualStyleBackColor = true;
            this.checkBox_autostart.CheckedChanged += new System.EventHandler(this.checkBox_autostart_CheckedChanged);
            // 
            // comboBox_username
            // 
            this.comboBox_username.FormattingEnabled = true;
            this.comboBox_username.Location = new System.Drawing.Point(75, 35);
            this.comboBox_username.MaxLength = 14;
            this.comboBox_username.Name = "comboBox_username";
            this.comboBox_username.Size = new System.Drawing.Size(238, 20);
            this.comboBox_username.TabIndex = 0;
            this.comboBox_username.SelectedValueChanged += new System.EventHandler(this.comboBox_username_SelectedValueChanged);
            // 
            // checkBox_autologin
            // 
            this.checkBox_autologin.AutoSize = true;
            this.checkBox_autologin.Location = new System.Drawing.Point(159, 94);
            this.checkBox_autologin.Name = "checkBox_autologin";
            this.checkBox_autologin.Size = new System.Drawing.Size(72, 16);
            this.checkBox_autologin.TabIndex = 3;
            this.checkBox_autologin.Text = "自动登录";
            this.checkBox_autologin.UseVisualStyleBackColor = true;
            // 
            // checkBox_showpwd
            // 
            this.checkBox_showpwd.AutoSize = true;
            this.checkBox_showpwd.Location = new System.Drawing.Point(289, 70);
            this.checkBox_showpwd.Name = "checkBox_showpwd";
            this.checkBox_showpwd.Size = new System.Drawing.Size(15, 14);
            this.checkBox_showpwd.TabIndex = 6;
            this.checkBox_showpwd.UseVisualStyleBackColor = true;
            this.checkBox_showpwd.CheckedChanged += new System.EventHandler(this.checkBox_showpwd_CheckedChanged);
            // 
            // checkBox_remember
            // 
            this.checkBox_remember.AutoSize = true;
            this.checkBox_remember.Location = new System.Drawing.Point(75, 94);
            this.checkBox_remember.Name = "checkBox_remember";
            this.checkBox_remember.Size = new System.Drawing.Size(72, 16);
            this.checkBox_remember.TabIndex = 2;
            this.checkBox_remember.Text = "记住密码";
            this.checkBox_remember.UseVisualStyleBackColor = true;
            // 
            // button_login
            // 
            this.button_login.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_login.Location = new System.Drawing.Point(133, 130);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(98, 38);
            this.button_login.TabIndex = 4;
            this.button_login.Text = "登录";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(75, 67);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(238, 21);
            this.textBox_password.TabIndex = 1;
            this.textBox_password.UseSystemPasswordChar = true;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_password.Location = new System.Drawing.Point(13, 70);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(56, 16);
            this.label_password.TabIndex = 1;
            this.label_password.Text = "密码：";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_name.Location = new System.Drawing.Point(13, 35);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(56, 16);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "账号：";
            // 
            // panel_logo
            // 
            this.panel_logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_logo.BackgroundImage")));
            this.panel_logo.Location = new System.Drawing.Point(72, 12);
            this.panel_logo.Name = "panel_logo";
            this.panel_logo.Size = new System.Drawing.Size(244, 66);
            this.panel_logo.TabIndex = 1;
            // 
            // Form1
            // 
            this.AcceptButton = this.button_login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 275);
            this.Controls.Add(this.panel_logo);
            this.Controls.Add(this.panel_login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "北斗微芯网络认证";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_login.ResumeLayout(false);
            this.panel_login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_login;
        private System.Windows.Forms.CheckBox checkBox_remember;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.CheckBox checkBox_showpwd;
        private System.Windows.Forms.CheckBox checkBox_autologin;
        private System.Windows.Forms.Panel panel_logo;
        private System.Windows.Forms.ComboBox comboBox_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.CheckBox checkBox_autostart;
    }
}


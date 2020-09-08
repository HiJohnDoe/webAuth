namespace webAuth
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.panel_logout = new System.Windows.Forms.Panel();
            this.label_keeplive_times = new System.Windows.Forms.Label();
            this.label_timetip = new System.Windows.Forms.Label();
            this.button_logout = new System.Windows.Forms.Button();
            this.panel_logo = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.myIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.myMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolMenuCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_logout.SuspendLayout();
            this.myMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_logout
            // 
            this.panel_logout.Controls.Add(this.label_keeplive_times);
            this.panel_logout.Controls.Add(this.label_timetip);
            this.panel_logout.Controls.Add(this.button_logout);
            this.panel_logout.Location = new System.Drawing.Point(29, 84);
            this.panel_logout.Name = "panel_logout";
            this.panel_logout.Size = new System.Drawing.Size(311, 179);
            this.panel_logout.TabIndex = 2;
            // 
            // label_keeplive_times
            // 
            this.label_keeplive_times.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_keeplive_times.AutoSize = true;
            this.label_keeplive_times.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_keeplive_times.Location = new System.Drawing.Point(143, 50);
            this.label_keeplive_times.Name = "label_keeplive_times";
            this.label_keeplive_times.Size = new System.Drawing.Size(0, 18);
            this.label_keeplive_times.TabIndex = 0;
            // 
            // label_timetip
            // 
            this.label_timetip.AutoSize = true;
            this.label_timetip.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_timetip.Location = new System.Drawing.Point(40, 52);
            this.label_timetip.Name = "label_timetip";
            this.label_timetip.Size = new System.Drawing.Size(88, 16);
            this.label_timetip.TabIndex = 5;
            this.label_timetip.Text = "在线时长：";
            this.label_timetip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_logout
            // 
            this.button_logout.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_logout.Location = new System.Drawing.Point(118, 125);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(98, 41);
            this.button_logout.TabIndex = 1;
            this.button_logout.Text = "注销";
            this.button_logout.UseVisualStyleBackColor = true;
            this.button_logout.Click += new System.EventHandler(this.button_logout_Click);
            // 
            // panel_logo
            // 
            this.panel_logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_logo.BackgroundImage")));
            this.panel_logo.Location = new System.Drawing.Point(72, 12);
            this.panel_logo.Name = "panel_logo";
            this.panel_logo.Size = new System.Drawing.Size(244, 66);
            this.panel_logo.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // myIcon
            // 
            this.myIcon.ContextMenuStrip = this.myMenu;
            this.myIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("myIcon.Icon")));
            this.myIcon.Text = "北斗微芯网络认证";
            this.myIcon.Visible = true;
            this.myIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.myIcon_MouseClick);
            // 
            // myMenu
            // 
            this.myMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuCancel});
            this.myMenu.Name = "contextMenuStrip1";
            this.myMenu.Size = new System.Drawing.Size(101, 26);
            // 
            // toolMenuCancel
            // 
            this.toolMenuCancel.Name = "toolMenuCancel";
            this.toolMenuCancel.Size = new System.Drawing.Size(100, 22);
            this.toolMenuCancel.Text = "退出";
            this.toolMenuCancel.Click += new System.EventHandler(this.toolMenuCancel_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 275);
            this.Controls.Add(this.panel_logo);
            this.Controls.Add(this.panel_logout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "北斗微芯网络认证";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel_logout.ResumeLayout(false);
            this.panel_logout.PerformLayout();
            this.myMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_logout;
        private System.Windows.Forms.Button button_logout;
        private System.Windows.Forms.Label label_keeplive_times;
        private System.Windows.Forms.Panel panel_logo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_timetip;
        private System.Windows.Forms.NotifyIcon myIcon;
        private System.Windows.Forms.ContextMenuStrip myMenu;
        private System.Windows.Forms.ToolStripMenuItem toolMenuCancel;
    }
}
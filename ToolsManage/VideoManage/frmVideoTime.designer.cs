namespace ToolsManage.VideoManage
{
    partial class frmVideoTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVideoTime));
            this.sbtnBackLook = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLook = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RealPlayWnd = new System.Windows.Forms.PictureBox();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.sbtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnVideo = new DevExpress.XtraEditors.SimpleButton();
            this.fbDialogFile = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbtnBackLook
            // 
            this.sbtnBackLook.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnBackLook.Appearance.Options.UseFont = true;
            this.sbtnBackLook.Image = ((System.Drawing.Image)(resources.GetObject("sbtnBackLook.Image")));
            this.sbtnBackLook.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnBackLook.Location = new System.Drawing.Point(35, 258);
            this.sbtnBackLook.Name = "sbtnBackLook";
            this.sbtnBackLook.Size = new System.Drawing.Size(130, 45);
            this.sbtnBackLook.TabIndex = 19;
            this.sbtnBackLook.Text = "回放";
            this.sbtnBackLook.Click += new System.EventHandler(this.sbtnBackLook_Click);
            // 
            // sbtnLook
            // 
            this.sbtnLook.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnLook.Appearance.Options.UseFont = true;
            this.sbtnLook.Image = ((System.Drawing.Image)(resources.GetObject("sbtnLook.Image")));
            this.sbtnLook.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnLook.Location = new System.Drawing.Point(35, 63);
            this.sbtnLook.Name = "sbtnLook";
            this.sbtnLook.Size = new System.Drawing.Size(130, 45);
            this.sbtnLook.TabIndex = 16;
            this.sbtnLook.Text = "预览";
            this.sbtnLook.Click += new System.EventHandler(this.sbtnLook_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RealPlayWnd);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(987, 730);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            // 
            // RealPlayWnd
            // 
            this.RealPlayWnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RealPlayWnd.Location = new System.Drawing.Point(3, 20);
            this.RealPlayWnd.Name = "RealPlayWnd";
            this.RealPlayWnd.Size = new System.Drawing.Size(981, 707);
            this.RealPlayWnd.TabIndex = 2;
            this.RealPlayWnd.TabStop = false;
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(35, 355);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(130, 45);
            this.sbtnReturn.TabIndex = 18;
            this.sbtnReturn.Text = "退出";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.sbtnLogin);
            this.groupBox1.Controls.Add(this.sbtnBackLook);
            this.groupBox1.Controls.Add(this.sbtnLook);
            this.groupBox1.Controls.Add(this.sbtnReturn);
            this.groupBox1.Controls.Add(this.sbtnVideo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(987, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 730);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(53, 564);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 523);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sbtnLogin
            // 
            this.sbtnLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnLogin.Appearance.Options.UseFont = true;
            this.sbtnLogin.Image = ((System.Drawing.Image)(resources.GetObject("sbtnLogin.Image")));
            this.sbtnLogin.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnLogin.Location = new System.Drawing.Point(35, 625);
            this.sbtnLogin.Name = "sbtnLogin";
            this.sbtnLogin.Size = new System.Drawing.Size(130, 45);
            this.sbtnLogin.TabIndex = 20;
            this.sbtnLogin.Text = "登录";
            this.sbtnLogin.Visible = false;
            this.sbtnLogin.Click += new System.EventHandler(this.sbtnLogin_Click);
            // 
            // sbtnVideo
            // 
            this.sbtnVideo.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnVideo.Appearance.Options.UseFont = true;
            this.sbtnVideo.Image = ((System.Drawing.Image)(resources.GetObject("sbtnVideo.Image")));
            this.sbtnVideo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnVideo.Location = new System.Drawing.Point(35, 158);
            this.sbtnVideo.Name = "sbtnVideo";
            this.sbtnVideo.Size = new System.Drawing.Size(130, 45);
            this.sbtnVideo.TabIndex = 13;
            this.sbtnVideo.Text = "录像";
            this.sbtnVideo.Click += new System.EventHandler(this.sbtnVideo_Click);
            // 
            // fbDialogFile
            // 
            this.fbDialogFile.SelectedPath = "D:\\";
            // 
            // frmVideoTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 730);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVideoTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视频实时监控";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVideoTime_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbtnBackLook;
        private DevExpress.XtraEditors.SimpleButton sbtnLook;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox RealPlayWnd;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton sbtnLogin;
        private DevExpress.XtraEditors.SimpleButton sbtnVideo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog fbDialogFile;
        private System.Windows.Forms.Button button2;
    }
}
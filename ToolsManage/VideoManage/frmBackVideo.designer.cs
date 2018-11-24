namespace ToolsManage.VideoManage
{
    partial class frmBackVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackVideo));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lvVideoInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnSlow = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnSpeed = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnNormal = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnPause = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnStop = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.pgbPlayPos = new System.Windows.Forms.ProgressBar();
            this.picPlayBack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlayBack)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupControl2);
            this.groupControl1.Controls.Add(this.groupBox1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl1.Location = new System.Drawing.Point(854, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(330, 730);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Controls.Add(this.lvVideoInfo);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(2, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(326, 420);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "录像记录";
            // 
            // lvVideoInfo
            // 
            this.lvVideoInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvVideoInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVideoInfo.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lvVideoInfo.FullRowSelect = true;
            this.lvVideoInfo.Location = new System.Drawing.Point(2, 2);
            this.lvVideoInfo.Name = "lvVideoInfo";
            this.lvVideoInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lvVideoInfo.Size = new System.Drawing.Size(322, 416);
            this.lvVideoInfo.TabIndex = 0;
            this.lvVideoInfo.UseCompatibleStateImageBehavior = false;
            this.lvVideoInfo.View = System.Windows.Forms.View.Details;
            this.lvVideoInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvVideoInfo_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "视频记录文件";
            this.columnHeader1.Width = 318;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sbtnReturn);
            this.groupBox1.Controls.Add(this.sbtnSlow);
            this.groupBox1.Controls.Add(this.sbtnSpeed);
            this.groupBox1.Controls.Add(this.sbtnNormal);
            this.groupBox1.Controls.Add(this.sbtnPause);
            this.groupBox1.Controls.Add(this.sbtnStop);
            this.groupBox1.Controls.Add(this.sbtnQuery);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpEnd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpStart);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(2, 422);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 306);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(79, 245);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(132, 45);
            this.sbtnReturn.TabIndex = 25;
            this.sbtnReturn.Text = "返回";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // sbtnSlow
            // 
            this.sbtnSlow.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnSlow.Appearance.Options.UseFont = true;
            this.sbtnSlow.Enabled = false;
            this.sbtnSlow.Image = ((System.Drawing.Image)(resources.GetObject("sbtnSlow.Image")));
            this.sbtnSlow.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnSlow.Location = new System.Drawing.Point(227, 188);
            this.sbtnSlow.Name = "sbtnSlow";
            this.sbtnSlow.Size = new System.Drawing.Size(80, 35);
            this.sbtnSlow.TabIndex = 24;
            this.sbtnSlow.Text = "慢放";
            this.sbtnSlow.Click += new System.EventHandler(this.sbtnSlow_Click);
            // 
            // sbtnSpeed
            // 
            this.sbtnSpeed.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnSpeed.Appearance.Options.UseFont = true;
            this.sbtnSpeed.Enabled = false;
            this.sbtnSpeed.Image = ((System.Drawing.Image)(resources.GetObject("sbtnSpeed.Image")));
            this.sbtnSpeed.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnSpeed.Location = new System.Drawing.Point(121, 188);
            this.sbtnSpeed.Name = "sbtnSpeed";
            this.sbtnSpeed.Size = new System.Drawing.Size(80, 35);
            this.sbtnSpeed.TabIndex = 23;
            this.sbtnSpeed.Text = "快进";
            this.sbtnSpeed.Click += new System.EventHandler(this.sbtnSpeed_Click);
            // 
            // sbtnNormal
            // 
            this.sbtnNormal.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnNormal.Appearance.Options.UseFont = true;
            this.sbtnNormal.Enabled = false;
            this.sbtnNormal.Image = ((System.Drawing.Image)(resources.GetObject("sbtnNormal.Image")));
            this.sbtnNormal.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnNormal.Location = new System.Drawing.Point(15, 188);
            this.sbtnNormal.Name = "sbtnNormal";
            this.sbtnNormal.Size = new System.Drawing.Size(80, 35);
            this.sbtnNormal.TabIndex = 22;
            this.sbtnNormal.Text = "常速";
            this.sbtnNormal.Click += new System.EventHandler(this.sbtnNormal_Click);
            // 
            // sbtnPause
            // 
            this.sbtnPause.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnPause.Appearance.Options.UseFont = true;
            this.sbtnPause.Enabled = false;
            this.sbtnPause.Image = ((System.Drawing.Image)(resources.GetObject("sbtnPause.Image")));
            this.sbtnPause.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnPause.Location = new System.Drawing.Point(227, 130);
            this.sbtnPause.Name = "sbtnPause";
            this.sbtnPause.Size = new System.Drawing.Size(80, 35);
            this.sbtnPause.TabIndex = 21;
            this.sbtnPause.Text = "暂停";
            this.sbtnPause.Click += new System.EventHandler(this.sbtnPause_Click);
            // 
            // sbtnStop
            // 
            this.sbtnStop.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnStop.Appearance.Options.UseFont = true;
            this.sbtnStop.Enabled = false;
            this.sbtnStop.Image = ((System.Drawing.Image)(resources.GetObject("sbtnStop.Image")));
            this.sbtnStop.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnStop.Location = new System.Drawing.Point(121, 130);
            this.sbtnStop.Name = "sbtnStop";
            this.sbtnStop.Size = new System.Drawing.Size(80, 35);
            this.sbtnStop.TabIndex = 20;
            this.sbtnStop.Text = "停止";
            this.sbtnStop.Click += new System.EventHandler(this.sbtnStop_Click);
            // 
            // sbtnQuery
            // 
            this.sbtnQuery.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnQuery.Appearance.Options.UseFont = true;
            this.sbtnQuery.Image = ((System.Drawing.Image)(resources.GetObject("sbtnQuery.Image")));
            this.sbtnQuery.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnQuery.Location = new System.Drawing.Point(15, 130);
            this.sbtnQuery.Name = "sbtnQuery";
            this.sbtnQuery.Size = new System.Drawing.Size(80, 35);
            this.sbtnQuery.TabIndex = 20;
            this.sbtnQuery.Text = "查询";
            this.sbtnQuery.Click += new System.EventHandler(this.sbtnQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "结束：";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CalendarFont = new System.Drawing.Font("Tahoma", 9F);
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(89, 81);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(213, 27);
            this.dtpEnd.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始：";
            // 
            // dtpStart
            // 
            this.dtpStart.CalendarFont = new System.Drawing.Font("Tahoma", 9F);
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(89, 36);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(213, 27);
            this.dtpStart.TabIndex = 1;
            // 
            // pgbPlayPos
            // 
            this.pgbPlayPos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgbPlayPos.Location = new System.Drawing.Point(0, 715);
            this.pgbPlayPos.Name = "pgbPlayPos";
            this.pgbPlayPos.Size = new System.Drawing.Size(854, 15);
            this.pgbPlayPos.Step = 1;
            this.pgbPlayPos.TabIndex = 8;
            // 
            // picPlayBack
            // 
            this.picPlayBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPlayBack.Location = new System.Drawing.Point(0, 0);
            this.picPlayBack.Name = "picPlayBack";
            this.picPlayBack.Size = new System.Drawing.Size(854, 715);
            this.picPlayBack.TabIndex = 9;
            this.picPlayBack.TabStop = false;
            // 
            // frmBackVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 730);
            this.Controls.Add(this.picPlayBack);
            this.Controls.Add(this.pgbPlayPos);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBackVideo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视频回放";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBackVideo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlayBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ListView lvVideoInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private DevExpress.XtraEditors.SimpleButton sbtnSlow;
        private DevExpress.XtraEditors.SimpleButton sbtnSpeed;
        private DevExpress.XtraEditors.SimpleButton sbtnNormal;
        private DevExpress.XtraEditors.SimpleButton sbtnPause;
        private DevExpress.XtraEditors.SimpleButton sbtnStop;
        private DevExpress.XtraEditors.SimpleButton sbtnQuery;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.ProgressBar pgbPlayPos;
        private System.Windows.Forms.PictureBox picPlayBack;
    }
}
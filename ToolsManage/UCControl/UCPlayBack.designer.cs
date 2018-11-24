namespace DAMSystem.UCControl
{
    partial class UCPlayBack
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPlayBack));
            this.ucVideoPlayBack = new DCMSystem.UCControl.UCVideo();
            this.pnlPlayBackTool = new System.Windows.Forms.Panel();
            this.m_chkESCallBack = new System.Windows.Forms.CheckBox();
            this.m_btnSound = new System.Windows.Forms.Button();
            this.m_btnFast = new System.Windows.Forms.Button();
            this.m_btnCapture = new System.Windows.Forms.Button();
            this.m_btnSlow = new System.Windows.Forms.Button();
            this.m_btnStop = new System.Windows.Forms.Button();
            this.m_btnPlay = new System.Windows.Forms.Button();
            this.trackBarPlaybackProgress = new System.Windows.Forms.TrackBar();
            this.m_imgPlay = new System.Windows.Forms.ImageList(this.components);
            this.m_imgStop = new System.Windows.Forms.ImageList(this.components);
            this.m_imgSlow = new System.Windows.Forms.ImageList(this.components);
            this.m_imgFast = new System.Windows.Forms.ImageList(this.components);
            this.m_imgSound = new System.Windows.Forms.ImageList(this.components);
            this.pnlPlayBackTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPlaybackProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // ucVideoPlayBack
            // 
            this.ucVideoPlayBack.Addr = 0;
// TODO: “”的代码生成失败，原因是出现异常“无效的基元类型: System.IntPtr。请考虑使用 CodeObjectCreateExpression。”。
// TODO: “”的代码生成失败，原因是出现异常“无效的基元类型: System.IntPtr。请考虑使用 CodeObjectCreateExpression。”。
            this.ucVideoPlayBack.CameraID = 0;
            this.ucVideoPlayBack.CameraManufac = 0;
            this.ucVideoPlayBack.CameraType = 0;
            this.ucVideoPlayBack.ChannelNo = 0;
            this.ucVideoPlayBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVideoPlayBack.IP = null;
            this.ucVideoPlayBack.IsPlaying = false;
            this.ucVideoPlayBack.Location = new System.Drawing.Point(0, 0);
            this.ucVideoPlayBack.LUserID = 0;
            this.ucVideoPlayBack.Name = "ucVideoPlayBack";
            this.ucVideoPlayBack.Port = 0;
            this.ucVideoPlayBack.Size = new System.Drawing.Size(843, 456);
            this.ucVideoPlayBack.TabIndex = 4;
            this.ucVideoPlayBack.VideoHandle = -1;
            // 
            // pnlPlayBackTool
            // 
            this.pnlPlayBackTool.Controls.Add(this.m_chkESCallBack);
            this.pnlPlayBackTool.Controls.Add(this.m_btnSound);
            this.pnlPlayBackTool.Controls.Add(this.m_btnFast);
            this.pnlPlayBackTool.Controls.Add(this.m_btnCapture);
            this.pnlPlayBackTool.Controls.Add(this.m_btnSlow);
            this.pnlPlayBackTool.Controls.Add(this.m_btnStop);
            this.pnlPlayBackTool.Controls.Add(this.m_btnPlay);
            this.pnlPlayBackTool.Controls.Add(this.trackBarPlaybackProgress);
            this.pnlPlayBackTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlayBackTool.Location = new System.Drawing.Point(0, 456);
            this.pnlPlayBackTool.Name = "pnlPlayBackTool";
            this.pnlPlayBackTool.Size = new System.Drawing.Size(843, 43);
            this.pnlPlayBackTool.TabIndex = 3;
            // 
            // m_chkESCallBack
            // 
            this.m_chkESCallBack.AutoSize = true;
            this.m_chkESCallBack.Location = new System.Drawing.Point(215, 18);
            this.m_chkESCallBack.Name = "m_chkESCallBack";
            this.m_chkESCallBack.Size = new System.Drawing.Size(84, 16);
            this.m_chkESCallBack.TabIndex = 81;
            this.m_chkESCallBack.Text = "ESCallBack";
            this.m_chkESCallBack.UseVisualStyleBackColor = true;
            this.m_chkESCallBack.Visible = false;
            // 
            // m_btnSound
            // 
            this.m_btnSound.Location = new System.Drawing.Point(183, 10);
            this.m_btnSound.Name = "m_btnSound";
            this.m_btnSound.Size = new System.Drawing.Size(25, 25);
            this.m_btnSound.TabIndex = 80;
            this.m_btnSound.UseVisualStyleBackColor = true;
            // 
            // m_btnFast
            // 
            this.m_btnFast.Location = new System.Drawing.Point(113, 10);
            this.m_btnFast.Name = "m_btnFast";
            this.m_btnFast.Size = new System.Drawing.Size(25, 25);
            this.m_btnFast.TabIndex = 79;
            this.m_btnFast.UseVisualStyleBackColor = true;
            // 
            // m_btnCapture
            // 
            this.m_btnCapture.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCapture.Image")));
            this.m_btnCapture.Location = new System.Drawing.Point(148, 10);
            this.m_btnCapture.Name = "m_btnCapture";
            this.m_btnCapture.Size = new System.Drawing.Size(25, 25);
            this.m_btnCapture.TabIndex = 78;
            this.m_btnCapture.UseVisualStyleBackColor = true;
            // 
            // m_btnSlow
            // 
            this.m_btnSlow.Location = new System.Drawing.Point(78, 10);
            this.m_btnSlow.Name = "m_btnSlow";
            this.m_btnSlow.Size = new System.Drawing.Size(25, 25);
            this.m_btnSlow.TabIndex = 77;
            this.m_btnSlow.UseVisualStyleBackColor = true;
            // 
            // m_btnStop
            // 
            this.m_btnStop.Location = new System.Drawing.Point(43, 10);
            this.m_btnStop.Name = "m_btnStop";
            this.m_btnStop.Size = new System.Drawing.Size(25, 25);
            this.m_btnStop.TabIndex = 76;
            this.m_btnStop.UseVisualStyleBackColor = true;
            // 
            // m_btnPlay
            // 
            this.m_btnPlay.ImageKey = "(无)";
            this.m_btnPlay.Location = new System.Drawing.Point(8, 10);
            this.m_btnPlay.Name = "m_btnPlay";
            this.m_btnPlay.Size = new System.Drawing.Size(25, 25);
            this.m_btnPlay.TabIndex = 75;
            this.m_btnPlay.UseVisualStyleBackColor = true;
            // 
            // trackBarPlaybackProgress
            // 
            this.trackBarPlaybackProgress.AutoSize = false;
            this.trackBarPlaybackProgress.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarPlaybackProgress.Location = new System.Drawing.Point(365, 11);
            this.trackBarPlaybackProgress.Maximum = 100;
            this.trackBarPlaybackProgress.Name = "trackBarPlaybackProgress";
            this.trackBarPlaybackProgress.Size = new System.Drawing.Size(425, 23);
            this.trackBarPlaybackProgress.TabIndex = 43;
            this.trackBarPlaybackProgress.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // m_imgPlay
            // 
            this.m_imgPlay.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgPlay.ImageStream")));
            this.m_imgPlay.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgPlay.Images.SetKeyName(0, "PLAY_ENABLE.ICO");
            this.m_imgPlay.Images.SetKeyName(1, "PLAY_DISABLE.ICO");
            this.m_imgPlay.Images.SetKeyName(2, "PAUSE_ENABLE.ICO");
            // 
            // m_imgStop
            // 
            this.m_imgStop.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgStop.ImageStream")));
            this.m_imgStop.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgStop.Images.SetKeyName(0, "STOP_DISABLE.ICO");
            this.m_imgStop.Images.SetKeyName(1, "STOP.ICO");
            // 
            // m_imgSlow
            // 
            this.m_imgSlow.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgSlow.ImageStream")));
            this.m_imgSlow.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgSlow.Images.SetKeyName(0, "ico00002.ico");
            this.m_imgSlow.Images.SetKeyName(1, "icon8.ico");
            // 
            // m_imgFast
            // 
            this.m_imgFast.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgFast.ImageStream")));
            this.m_imgFast.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgFast.Images.SetKeyName(0, "ico00003.ico");
            this.m_imgFast.Images.SetKeyName(1, "ico00001.ico");
            // 
            // m_imgSound
            // 
            this.m_imgSound.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imgSound.ImageStream")));
            this.m_imgSound.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imgSound.Images.SetKeyName(0, "ico00009.ico");
            this.m_imgSound.Images.SetKeyName(1, "ico00008.ico");
            // 
            // UCPlayBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucVideoPlayBack);
            this.Controls.Add(this.pnlPlayBackTool);
            this.Name = "UCPlayBack";
            this.Size = new System.Drawing.Size(843, 499);
            this.pnlPlayBackTool.ResumeLayout(false);
            this.pnlPlayBackTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPlaybackProgress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DCMSystem.UCControl.UCVideo ucVideoPlayBack;
        private System.Windows.Forms.Panel pnlPlayBackTool;
        private System.Windows.Forms.CheckBox m_chkESCallBack;
        private System.Windows.Forms.Button m_btnSound;
        private System.Windows.Forms.Button m_btnFast;
        private System.Windows.Forms.Button m_btnCapture;
        private System.Windows.Forms.Button m_btnSlow;
        private System.Windows.Forms.Button m_btnStop;
        private System.Windows.Forms.Button m_btnPlay;
        private System.Windows.Forms.TrackBar trackBarPlaybackProgress;
        private System.Windows.Forms.ImageList m_imgPlay;
        private System.Windows.Forms.ImageList m_imgStop;
        private System.Windows.Forms.ImageList m_imgSlow;
        private System.Windows.Forms.ImageList m_imgFast;
        private System.Windows.Forms.ImageList m_imgSound;
    }
}

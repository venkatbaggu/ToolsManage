namespace DAMSystem
{
    partial class frmVideo
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
            this.pnlVideoType = new System.Windows.Forms.Panel();
            this.ThermalVideo = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.EagleVideo = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.CommVideo = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.gbVideoControl = new System.Windows.Forms.GroupBox();
            this.gbRecordPlay = new System.Windows.Forms.GroupBox();
            this.btnStartPlay = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.gbVideoBZ = new System.Windows.Forms.GroupBox();
            this.btnCapture = new DevComponents.DotNetBar.ButtonX();
            this.gbYTKZ = new System.Windows.Forms.GroupBox();
            this.btnRightDown = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.btnRigthUp = new DevComponents.DotNetBar.ButtonX();
            this.btnLeftUp = new DevComponents.DotNetBar.ButtonX();
            this.btnSXFocus = new DevComponents.DotNetBar.ButtonX();
            this.btnFDFocus = new DevComponents.DotNetBar.ButtonX();
            this.btnRight = new DevComponents.DotNetBar.ButtonX();
            this.btnLeft = new DevComponents.DotNetBar.ButtonX();
            this.btnDown = new DevComponents.DotNetBar.ButtonX();
            this.btnUP = new DevComponents.DotNetBar.ButtonX();
            this.gbChannel = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbYTChannel = new System.Windows.Forms.ComboBox();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.pnlVideo = new System.Windows.Forms.Panel();
            this.btnVideoPlayBack = new DevComponents.DotNetBar.ButtonX();
            this.pnlVideoType.SuspendLayout();
            this.ThermalVideo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.EagleVideo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.CommVideo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbVideoControl.SuspendLayout();
            this.gbRecordPlay.SuspendLayout();
            this.gbVideoBZ.SuspendLayout();
            this.gbYTKZ.SuspendLayout();
            this.gbChannel.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlVideoType
            // 
            this.pnlVideoType.Controls.Add(this.ThermalVideo);
            this.pnlVideoType.Controls.Add(this.EagleVideo);
            this.pnlVideoType.Controls.Add(this.CommVideo);
            this.pnlVideoType.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlVideoType.Location = new System.Drawing.Point(0, 0);
            this.pnlVideoType.Name = "pnlVideoType";
            this.pnlVideoType.Size = new System.Drawing.Size(169, 586);
            this.pnlVideoType.TabIndex = 0;
            // 
            // ThermalVideo
            // 
            this.ThermalVideo.BackgroundImage = global::ToolsManage.Properties.Resources.红外热成像;
            this.ThermalVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ThermalVideo.Controls.Add(this.panel2);
            this.ThermalVideo.Location = new System.Drawing.Point(12, 393);
            this.ThermalVideo.Name = "ThermalVideo";
            this.ThermalVideo.Size = new System.Drawing.Size(139, 100);
            this.ThermalVideo.TabIndex = 2;
            this.ThermalVideo.Click += new System.EventHandler(this.ThermalVideo_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(139, 24);
            this.panel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.ForeColor = System.Drawing.Color.Teal;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "红外热成像";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EagleVideo
            // 
            this.EagleVideo.BackgroundImage = global::ToolsManage.Properties.Resources.普通摄像头;
            this.EagleVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EagleVideo.Controls.Add(this.panel3);
            this.EagleVideo.Location = new System.Drawing.Point(12, 245);
            this.EagleVideo.Name = "EagleVideo";
            this.EagleVideo.Size = new System.Drawing.Size(139, 100);
            this.EagleVideo.TabIndex = 1;
            this.EagleVideo.Click += new System.EventHandler(this.EagleVideo_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 76);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(139, 24);
            this.panel3.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ForeColor = System.Drawing.Color.Teal;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 24);
            this.label8.TabIndex = 0;
            this.label8.Text = "全景摄像头";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CommVideo
            // 
            this.CommVideo.BackgroundImage = global::ToolsManage.Properties.Resources.普通摄像头;
            this.CommVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CommVideo.Controls.Add(this.panel1);
            this.CommVideo.Location = new System.Drawing.Point(12, 97);
            this.CommVideo.Name = "CommVideo";
            this.CommVideo.Size = new System.Drawing.Size(139, 110);
            this.CommVideo.TabIndex = 0;
            this.CommVideo.Click += new System.EventHandler(this.CommVideo_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 24);
            this.panel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.Teal;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "普通摄像头";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbVideoControl
            // 
            this.gbVideoControl.Controls.Add(this.gbRecordPlay);
            this.gbVideoControl.Controls.Add(this.gbVideoBZ);
            this.gbVideoControl.Controls.Add(this.gbYTKZ);
            this.gbVideoControl.Controls.Add(this.gbChannel);
            this.gbVideoControl.Controls.Add(this.gbOutput);
            this.gbVideoControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbVideoControl.Location = new System.Drawing.Point(961, 0);
            this.gbVideoControl.Name = "gbVideoControl";
            this.gbVideoControl.Size = new System.Drawing.Size(199, 586);
            this.gbVideoControl.TabIndex = 5;
            this.gbVideoControl.TabStop = false;
            this.gbVideoControl.Text = "视频控制";
            // 
            // gbRecordPlay
            // 
            this.gbRecordPlay.Controls.Add(this.btnVideoPlayBack);
            this.gbRecordPlay.Controls.Add(this.btnStartPlay);
            this.gbRecordPlay.Controls.Add(this.label2);
            this.gbRecordPlay.Controls.Add(this.dtpEnd);
            this.gbRecordPlay.Controls.Add(this.label1);
            this.gbRecordPlay.Controls.Add(this.dtpStart);
            this.gbRecordPlay.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRecordPlay.Location = new System.Drawing.Point(3, 249);
            this.gbRecordPlay.Name = "gbRecordPlay";
            this.gbRecordPlay.Size = new System.Drawing.Size(193, 62);
            this.gbRecordPlay.TabIndex = 23;
            this.gbRecordPlay.TabStop = false;
            this.gbRecordPlay.Text = "视频回放";
            // 
            // btnStartPlay
            // 
            this.btnStartPlay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStartPlay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStartPlay.Image = global::ToolsManage.Properties.Resources.回放;
            this.btnStartPlay.Location = new System.Drawing.Point(122, 178);
            this.btnStartPlay.Name = "btnStartPlay";
            this.btnStartPlay.Size = new System.Drawing.Size(65, 28);
            this.btnStartPlay.TabIndex = 19;
            this.btnStartPlay.Text = "回放";
            this.btnStartPlay.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Right;
            this.btnStartPlay.Tooltip = "播放";
            this.btnStartPlay.Visible = false;
            this.btnStartPlay.Click += new System.EventHandler(this.btnStartPlay_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "结束时间：";
            this.label2.Visible = false;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(12, 145);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(175, 21);
            this.dtpEnd.TabIndex = 14;
            this.dtpEnd.Value = new System.DateTime(2013, 9, 11, 15, 11, 1, 0);
            this.dtpEnd.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "开始时间：";
            this.label1.Visible = false;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(12, 88);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(175, 21);
            this.dtpStart.TabIndex = 12;
            this.dtpStart.Value = new System.DateTime(2013, 9, 11, 15, 11, 1, 0);
            this.dtpStart.Visible = false;
            // 
            // gbVideoBZ
            // 
            this.gbVideoBZ.Controls.Add(this.btnCapture);
            this.gbVideoBZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbVideoBZ.Location = new System.Drawing.Point(3, 195);
            this.gbVideoBZ.Name = "gbVideoBZ";
            this.gbVideoBZ.Size = new System.Drawing.Size(193, 54);
            this.gbVideoBZ.TabIndex = 22;
            this.gbVideoBZ.TabStop = false;
            this.gbVideoBZ.Text = "图片捕捉";
            // 
            // btnCapture
            // 
            this.btnCapture.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCapture.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCapture.Image = global::ToolsManage.Properties.Resources.clear;
            this.btnCapture.Location = new System.Drawing.Point(122, 20);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(65, 25);
            this.btnCapture.TabIndex = 21;
            this.btnCapture.Text = "捕捉";
            this.btnCapture.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Right;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // gbYTKZ
            // 
            this.gbYTKZ.Controls.Add(this.btnRightDown);
            this.gbYTKZ.Controls.Add(this.buttonX3);
            this.gbYTKZ.Controls.Add(this.btnRigthUp);
            this.gbYTKZ.Controls.Add(this.btnLeftUp);
            this.gbYTKZ.Controls.Add(this.btnSXFocus);
            this.gbYTKZ.Controls.Add(this.btnFDFocus);
            this.gbYTKZ.Controls.Add(this.btnRight);
            this.gbYTKZ.Controls.Add(this.btnLeft);
            this.gbYTKZ.Controls.Add(this.btnDown);
            this.gbYTKZ.Controls.Add(this.btnUP);
            this.gbYTKZ.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbYTKZ.Location = new System.Drawing.Point(3, 81);
            this.gbYTKZ.Name = "gbYTKZ";
            this.gbYTKZ.Size = new System.Drawing.Size(193, 114);
            this.gbYTKZ.TabIndex = 21;
            this.gbYTKZ.TabStop = false;
            this.gbYTKZ.Text = "云台控制";
            // 
            // btnRightDown
            // 
            this.btnRightDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRightDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRightDown.Location = new System.Drawing.Point(79, 81);
            this.btnRightDown.Name = "btnRightDown";
            this.btnRightDown.Size = new System.Drawing.Size(25, 25);
            this.btnRightDown.TabIndex = 39;
            this.btnRightDown.Tag = "8";
            this.btnRightDown.Text = "↘";
            this.btnRightDown.Visible = false;
            this.btnRightDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnRightDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(13, 81);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(25, 25);
            this.buttonX3.TabIndex = 38;
            this.buttonX3.Tag = "7";
            this.buttonX3.Text = "↙";
            this.buttonX3.Visible = false;
            this.buttonX3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.buttonX3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // btnRigthUp
            // 
            this.btnRigthUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRigthUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRigthUp.Location = new System.Drawing.Point(79, 19);
            this.btnRigthUp.Name = "btnRigthUp";
            this.btnRigthUp.Size = new System.Drawing.Size(25, 25);
            this.btnRigthUp.TabIndex = 37;
            this.btnRigthUp.Tag = "6";
            this.btnRigthUp.Text = "↗";
            this.btnRigthUp.Visible = false;
            this.btnRigthUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnRigthUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // btnLeftUp
            // 
            this.btnLeftUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLeftUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLeftUp.Location = new System.Drawing.Point(13, 19);
            this.btnLeftUp.Name = "btnLeftUp";
            this.btnLeftUp.Size = new System.Drawing.Size(25, 25);
            this.btnLeftUp.TabIndex = 36;
            this.btnLeftUp.Tag = "5";
            this.btnLeftUp.Text = "↖";
            this.btnLeftUp.Visible = false;
            this.btnLeftUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnLeftUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // btnSXFocus
            // 
            this.btnSXFocus.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSXFocus.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSXFocus.Image = global::ToolsManage.Properties.Resources.缩小;
            this.btnSXFocus.Location = new System.Drawing.Point(122, 72);
            this.btnSXFocus.Name = "btnSXFocus";
            this.btnSXFocus.Size = new System.Drawing.Size(65, 27);
            this.btnSXFocus.TabIndex = 21;
            this.btnSXFocus.Tag = "10";
            this.btnSXFocus.Text = "缩小";
            this.btnSXFocus.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Right;
            this.btnSXFocus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnSXFocus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // btnFDFocus
            // 
            this.btnFDFocus.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFDFocus.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFDFocus.Image = global::ToolsManage.Properties.Resources.放大;
            this.btnFDFocus.Location = new System.Drawing.Point(122, 30);
            this.btnFDFocus.Name = "btnFDFocus";
            this.btnFDFocus.Size = new System.Drawing.Size(65, 27);
            this.btnFDFocus.TabIndex = 20;
            this.btnFDFocus.Tag = "9";
            this.btnFDFocus.Text = "放大";
            this.btnFDFocus.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Right;
            this.btnFDFocus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnFDFocus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // btnRight
            // 
            this.btnRight.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRight.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRight.Location = new System.Drawing.Point(79, 50);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(25, 25);
            this.btnRight.TabIndex = 19;
            this.btnRight.Tag = "4";
            this.btnRight.Text = "→";
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // btnLeft
            // 
            this.btnLeft.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLeft.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLeft.Location = new System.Drawing.Point(13, 50);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(25, 25);
            this.btnLeft.TabIndex = 18;
            this.btnLeft.Tag = "3";
            this.btnLeft.Text = "←";
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // btnDown
            // 
            this.btnDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDown.Location = new System.Drawing.Point(46, 81);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(25, 25);
            this.btnDown.TabIndex = 17;
            this.btnDown.Tag = "2";
            this.btnDown.Text = "↓";
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // btnUP
            // 
            this.btnUP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUP.Location = new System.Drawing.Point(46, 19);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(25, 25);
            this.btnUP.TabIndex = 16;
            this.btnUP.Tag = "1";
            this.btnUP.Text = "↑";
            this.btnUP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.云台启动命令buttonPTZ_MouseDown);
            this.btnUP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.云台停止命令buttonPTZ_MouseUp);
            // 
            // gbChannel
            // 
            this.gbChannel.Controls.Add(this.label5);
            this.gbChannel.Controls.Add(this.cmbYTChannel);
            this.gbChannel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbChannel.Location = new System.Drawing.Point(3, 17);
            this.gbChannel.Name = "gbChannel";
            this.gbChannel.Size = new System.Drawing.Size(193, 64);
            this.gbChannel.TabIndex = 20;
            this.gbChannel.TabStop = false;
            this.gbChannel.Text = "通道选择";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "通道名称：";
            // 
            // cmbYTChannel
            // 
            this.cmbYTChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYTChannel.FormattingEnabled = true;
            this.cmbYTChannel.Location = new System.Drawing.Point(61, 33);
            this.cmbYTChannel.Name = "cmbYTChannel";
            this.cmbYTChannel.Size = new System.Drawing.Size(126, 20);
            this.cmbYTChannel.TabIndex = 24;
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.rtbOutput);
            this.gbOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbOutput.Location = new System.Drawing.Point(3, 511);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(193, 72);
            this.gbOutput.TabIndex = 10;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "输出信息";
            this.gbOutput.Visible = false;
            // 
            // rtbOutput
            // 
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(3, 17);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(187, 52);
            this.rtbOutput.TabIndex = 11;
            this.rtbOutput.Text = "";
            // 
            // pnlVideo
            // 
            this.pnlVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVideo.Location = new System.Drawing.Point(169, 0);
            this.pnlVideo.Name = "pnlVideo";
            this.pnlVideo.Size = new System.Drawing.Size(792, 586);
            this.pnlVideo.TabIndex = 6;
            // 
            // btnVideoPlayBack
            // 
            this.btnVideoPlayBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnVideoPlayBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnVideoPlayBack.Image = global::ToolsManage.Properties.Resources.回放;
            this.btnVideoPlayBack.Location = new System.Drawing.Point(61, 20);
            this.btnVideoPlayBack.Name = "btnVideoPlayBack";
            this.btnVideoPlayBack.Size = new System.Drawing.Size(65, 28);
            this.btnVideoPlayBack.TabIndex = 20;
            this.btnVideoPlayBack.Text = "回放";
            this.btnVideoPlayBack.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Right;
            this.btnVideoPlayBack.Tooltip = "播放";
            this.btnVideoPlayBack.Click += new System.EventHandler(this.btnVideoPlayBack_Click);
            // 
            // frmVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1160, 586);
            this.Controls.Add(this.pnlVideo);
            this.Controls.Add(this.gbVideoControl);
            this.Controls.Add(this.pnlVideoType);
            this.Name = "frmVideo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视频监控";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVideo_FormClosing);
            this.Load += new System.EventHandler(this.frmVideo_Load);
            this.pnlVideoType.ResumeLayout(false);
            this.ThermalVideo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.EagleVideo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.CommVideo.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbVideoControl.ResumeLayout(false);
            this.gbRecordPlay.ResumeLayout(false);
            this.gbRecordPlay.PerformLayout();
            this.gbVideoBZ.ResumeLayout(false);
            this.gbYTKZ.ResumeLayout(false);
            this.gbChannel.ResumeLayout(false);
            this.gbChannel.PerformLayout();
            this.gbOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlVideoType;
        private System.Windows.Forms.GroupBox gbVideoControl;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.Panel pnlVideo;
        private System.Windows.Forms.Panel ThermalVideo;
        private System.Windows.Forms.Panel EagleVideo;
        private System.Windows.Forms.Panel CommVideo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbChannel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbYTChannel;
        private System.Windows.Forms.GroupBox gbRecordPlay;
        private DevComponents.DotNetBar.ButtonX btnStartPlay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.GroupBox gbVideoBZ;
        private DevComponents.DotNetBar.ButtonX btnCapture;
        private System.Windows.Forms.GroupBox gbYTKZ;
        private DevComponents.DotNetBar.ButtonX btnRightDown;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX btnRigthUp;
        private DevComponents.DotNetBar.ButtonX btnLeftUp;
        private DevComponents.DotNetBar.ButtonX btnSXFocus;
        private DevComponents.DotNetBar.ButtonX btnFDFocus;
        private DevComponents.DotNetBar.ButtonX btnRight;
        private DevComponents.DotNetBar.ButtonX btnLeft;
        private DevComponents.DotNetBar.ButtonX btnDown;
        private DevComponents.DotNetBar.ButtonX btnUP;
        private DevComponents.DotNetBar.ButtonX btnVideoPlayBack;
    }
}
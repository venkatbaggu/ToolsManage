namespace ToolsManage.ToolsManage
{
    partial class frmInRoomPower
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInRoomPower));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sbtnExpand = new DevExpress.XtraEditors.SimpleButton();
            this.label12 = new System.Windows.Forms.Label();
            this.cbbOverPeople = new System.Windows.Forms.ComboBox();
            this.tbRFID = new System.Windows.Forms.TextBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.cbbToolName = new System.Windows.Forms.ComboBox();
            this.cbbToolType = new System.Windows.Forms.ComboBox();
            this.tbToolID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sbtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.tbPlace = new System.Windows.Forms.TextBox();
            this.tbCycle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbToolIdAdd = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.treeView1);
            this.groupControl1.Controls.Add(this.groupBox1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(312, 730);
            this.groupControl1.TabIndex = 16;
            this.groupControl1.Text = "groupControl1";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(2, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(308, 669);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "DocumentMap_32x32.png");
            this.imageList1.Images.SetKeyName(1, "Language_32x32.png");
            this.imageList1.Images.SetKeyName(2, "Cards_32x32.png");
            this.imageList1.Images.SetKeyName(3, "IDE_32x32.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sbtnExpand);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(2, 671);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 57);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // sbtnExpand
            // 
            this.sbtnExpand.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnExpand.Appearance.Options.UseFont = true;
            this.sbtnExpand.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExpand.Image")));
            this.sbtnExpand.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnExpand.Location = new System.Drawing.Point(44, 17);
            this.sbtnExpand.Name = "sbtnExpand";
            this.sbtnExpand.Size = new System.Drawing.Size(213, 30);
            this.sbtnExpand.TabIndex = 66;
            this.sbtnExpand.Text = "展开/折叠";
            this.sbtnExpand.Click += new System.EventHandler(this.sbtnExpand_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(376, 446);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 19);
            this.label12.TabIndex = 52;
            this.label12.Text = "存放位置：";
            // 
            // cbbOverPeople
            // 
            this.cbbOverPeople.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.cbbOverPeople.FormattingEnabled = true;
            this.cbbOverPeople.Location = new System.Drawing.Point(867, 328);
            this.cbbOverPeople.Name = "cbbOverPeople";
            this.cbbOverPeople.Size = new System.Drawing.Size(253, 27);
            this.cbbOverPeople.TabIndex = 38;
            this.cbbOverPeople.Text = "admin";
            // 
            // tbRFID
            // 
            this.tbRFID.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.tbRFID.Location = new System.Drawing.Point(885, 104);
            this.tbRFID.Name = "tbRFID";
            this.tbRFID.Size = new System.Drawing.Size(235, 29);
            this.tbRFID.TabIndex = 30;
            // 
            // dtpTime
            // 
            this.dtpTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Location = new System.Drawing.Point(867, 216);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(253, 29);
            this.dtpTime.TabIndex = 36;
            // 
            // cbbToolName
            // 
            this.cbbToolName.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.cbbToolName.FormattingEnabled = true;
            this.cbbToolName.Location = new System.Drawing.Point(477, 328);
            this.cbbToolName.Name = "cbbToolName";
            this.cbbToolName.Size = new System.Drawing.Size(197, 27);
            this.cbbToolName.TabIndex = 32;
            this.cbbToolName.SelectedIndexChanged += new System.EventHandler(this.cbbToolName_SelectedIndexChanged);
            // 
            // cbbToolType
            // 
            this.cbbToolType.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.cbbToolType.FormattingEnabled = true;
            this.cbbToolType.Location = new System.Drawing.Point(477, 220);
            this.cbbToolType.Name = "cbbToolType";
            this.cbbToolType.Size = new System.Drawing.Size(197, 27);
            this.cbbToolType.TabIndex = 31;
            this.cbbToolType.SelectedIndexChanged += new System.EventHandler(this.cbbToolType_SelectedIndexChanged);
            // 
            // tbToolID
            // 
            this.tbToolID.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.tbToolID.Location = new System.Drawing.Point(477, 104);
            this.tbToolID.Name = "tbToolID";
            this.tbToolID.Size = new System.Drawing.Size(197, 29);
            this.tbToolID.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(766, 331);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 19);
            this.label11.TabIndex = 48;
            this.label11.Text = "入 库 人：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(766, 223);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 19);
            this.label8.TabIndex = 46;
            this.label8.Text = "入库时间：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(373, 331);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 19);
            this.label4.TabIndex = 42;
            this.label4.Text = "工具名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(370, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 41;
            this.label3.Text = "工具种类：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(766, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 19);
            this.label2.TabIndex = 40;
            this.label2.Text = "RFID编码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(373, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 19);
            this.label1.TabIndex = 39;
            this.label1.Text = "工具ID：";
            // 
            // sbtnExit
            // 
            this.sbtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnExit.Appearance.Options.UseFont = true;
            this.sbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExit.Image")));
            this.sbtnExit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnExit.Location = new System.Drawing.Point(863, 609);
            this.sbtnExit.Name = "sbtnExit";
            this.sbtnExit.Size = new System.Drawing.Size(130, 45);
            this.sbtnExit.TabIndex = 56;
            this.sbtnExit.Text = "退出";
            this.sbtnExit.Click += new System.EventHandler(this.sbtnExit_Click);
            // 
            // sbtnOk
            // 
            this.sbtnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnOk.Appearance.Options.UseFont = true;
            this.sbtnOk.Enabled = false;
            this.sbtnOk.Image = ((System.Drawing.Image)(resources.GetObject("sbtnOk.Image")));
            this.sbtnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnOk.Location = new System.Drawing.Point(490, 609);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(130, 45);
            this.sbtnOk.TabIndex = 55;
            this.sbtnOk.Text = "确认";
            this.sbtnOk.Click += new System.EventHandler(this.sbtnOk_Click);
            // 
            // tbPlace
            // 
            this.tbPlace.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.tbPlace.Location = new System.Drawing.Point(477, 443);
            this.tbPlace.Name = "tbPlace";
            this.tbPlace.Size = new System.Drawing.Size(197, 29);
            this.tbPlace.TabIndex = 60;
            // 
            // tbCycle
            // 
            this.tbCycle.Enabled = false;
            this.tbCycle.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.tbCycle.Location = new System.Drawing.Point(867, 443);
            this.tbCycle.Name = "tbCycle";
            this.tbCycle.ReadOnly = true;
            this.tbCycle.Size = new System.Drawing.Size(253, 29);
            this.tbCycle.TabIndex = 61;
            this.tbCycle.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(766, 446);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 19);
            this.label5.TabIndex = 62;
            this.label5.Text = "试验周期：";
            this.label5.Visible = false;
            // 
            // tbToolIdAdd
            // 
            this.tbToolIdAdd.Location = new System.Drawing.Point(477, 41);
            this.tbToolIdAdd.Name = "tbToolIdAdd";
            this.tbToolIdAdd.Size = new System.Drawing.Size(100, 22);
            this.tbToolIdAdd.TabIndex = 63;
            this.tbToolIdAdd.Visible = false;
            // 
            // frmInRoomPower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 730);
            this.Controls.Add(this.tbToolIdAdd);
            this.Controls.Add(this.tbCycle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPlace);
            this.Controls.Add(this.sbtnExit);
            this.Controls.Add(this.sbtnOk);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbbOverPeople);
            this.Controls.Add(this.tbRFID);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.cbbToolName);
            this.Controls.Add(this.cbbToolType);
            this.Controls.Add(this.tbToolID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInRoomPower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "入库管理";
            this.Load += new System.EventHandler(this.frmInRoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.ComboBox cbbOverPeople;
        private System.Windows.Forms.TextBox tbRFID;
        public System.Windows.Forms.DateTimePicker dtpTime;
        public System.Windows.Forms.ComboBox cbbToolName;
        private System.Windows.Forms.ComboBox cbbToolType;
        public System.Windows.Forms.TextBox tbToolID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton sbtnExit;
        private DevExpress.XtraEditors.SimpleButton sbtnOk;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.TextBox tbPlace;
        public System.Windows.Forms.TextBox tbCycle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbToolIdAdd;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton sbtnExpand;
    }
}
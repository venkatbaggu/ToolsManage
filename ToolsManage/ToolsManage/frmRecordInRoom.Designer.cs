namespace ToolsManage.ToolsManage
{
    partial class frmRecordInRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecordInRoom));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cbbPeopleIn = new System.Windows.Forms.ComboBox();
            this.cbbToolName = new System.Windows.Forms.ComboBox();
            this.cbbToolType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.sbtnPageDown = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnPageUp = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnOut = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sbtnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(914, 730);
            this.gridControl1.TabIndex = 31;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // cbbPeopleIn
            // 
            this.cbbPeopleIn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cbbPeopleIn.FormattingEnabled = true;
            this.cbbPeopleIn.Location = new System.Drawing.Point(32, 29);
            this.cbbPeopleIn.Name = "cbbPeopleIn";
            this.cbbPeopleIn.Size = new System.Drawing.Size(213, 24);
            this.cbbPeopleIn.TabIndex = 49;
            this.cbbPeopleIn.Text = "全部";
            // 
            // cbbToolName
            // 
            this.cbbToolName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cbbToolName.FormattingEnabled = true;
            this.cbbToolName.Location = new System.Drawing.Point(32, 96);
            this.cbbToolName.Name = "cbbToolName";
            this.cbbToolName.Size = new System.Drawing.Size(213, 24);
            this.cbbToolName.TabIndex = 44;
            this.cbbToolName.Text = "全部";
            // 
            // cbbToolType
            // 
            this.cbbToolType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cbbToolType.FormattingEnabled = true;
            this.cbbToolType.Location = new System.Drawing.Point(32, 48);
            this.cbbToolType.Name = "cbbToolType";
            this.cbbToolType.Size = new System.Drawing.Size(213, 24);
            this.cbbToolType.TabIndex = 43;
            this.cbbToolType.Text = "全部";
            this.cbbToolType.SelectedIndexChanged += new System.EventHandler(this.cbbToolType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(29, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 45;
            this.label3.Text = "工具种类：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(29, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 46;
            this.label4.Text = "工具名称：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbbPeopleIn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(2, 287);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 67);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "入库人";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.sbtnPageDown);
            this.groupControl1.Controls.Add(this.sbtnPageUp);
            this.groupControl1.Controls.Add(this.groupBox1);
            this.groupControl1.Controls.Add(this.sbtnOut);
            this.groupControl1.Controls.Add(this.groupBox3);
            this.groupControl1.Controls.Add(this.sbtnQuery);
            this.groupControl1.Controls.Add(this.groupBox2);
            this.groupControl1.Controls.Add(this.sbtnReturn);
            this.groupControl1.Controls.Add(this.sbtnDelete);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl1.Location = new System.Drawing.Point(914, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(270, 730);
            this.groupControl1.TabIndex = 30;
            this.groupControl1.Text = "查询条件";
            // 
            // sbtnPageDown
            // 
            this.sbtnPageDown.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnPageDown.Appearance.Options.UseFont = true;
            this.sbtnPageDown.Image = ((System.Drawing.Image)(resources.GetObject("sbtnPageDown.Image")));
            this.sbtnPageDown.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnPageDown.Location = new System.Drawing.Point(64, 500);
            this.sbtnPageDown.Name = "sbtnPageDown";
            this.sbtnPageDown.Size = new System.Drawing.Size(150, 45);
            this.sbtnPageDown.TabIndex = 41;
            this.sbtnPageDown.Text = "下页";
            this.sbtnPageDown.Click += new System.EventHandler(this.sbtnPageDown_Click);
            // 
            // sbtnPageUp
            // 
            this.sbtnPageUp.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnPageUp.Appearance.Options.UseFont = true;
            this.sbtnPageUp.Image = ((System.Drawing.Image)(resources.GetObject("sbtnPageUp.Image")));
            this.sbtnPageUp.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnPageUp.Location = new System.Drawing.Point(64, 438);
            this.sbtnPageUp.Name = "sbtnPageUp";
            this.sbtnPageUp.Size = new System.Drawing.Size(150, 45);
            this.sbtnPageUp.TabIndex = 40;
            this.sbtnPageUp.Text = "上页";
            this.sbtnPageUp.Click += new System.EventHandler(this.sbtnPageUp_Click);
            // 
            // sbtnOut
            // 
            this.sbtnOut.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnOut.Appearance.Options.UseFont = true;
            this.sbtnOut.Image = ((System.Drawing.Image)(resources.GetObject("sbtnOut.Image")));
            this.sbtnOut.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnOut.Location = new System.Drawing.Point(64, 625);
            this.sbtnOut.Name = "sbtnOut";
            this.sbtnOut.Size = new System.Drawing.Size(150, 45);
            this.sbtnOut.TabIndex = 20;
            this.sbtnOut.Text = "导出";
            this.sbtnOut.Click += new System.EventHandler(this.sbtnOut_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbbToolName);
            this.groupBox3.Controls.Add(this.cbbToolType);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(2, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 126);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "工具种类名称";
            // 
            // sbtnQuery
            // 
            this.sbtnQuery.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnQuery.Appearance.Options.UseFont = true;
            this.sbtnQuery.Image = ((System.Drawing.Image)(resources.GetObject("sbtnQuery.Image")));
            this.sbtnQuery.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnQuery.Location = new System.Drawing.Point(64, 374);
            this.sbtnQuery.Name = "sbtnQuery";
            this.sbtnQuery.Size = new System.Drawing.Size(150, 45);
            this.sbtnQuery.TabIndex = 12;
            this.sbtnQuery.Text = "查询";
            this.sbtnQuery.Click += new System.EventHandler(this.sbtnQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpStart);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpEnd);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(2, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 134);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "入库时间";
            // 
            // dtpStart
            // 
            this.dtpStart.CalendarFont = new System.Drawing.Font("Tahoma", 9F);
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpStart.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(32, 47);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(213, 27);
            this.dtpStart.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(28, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 22;
            this.label2.Text = "开始：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(28, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "结束：";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CalendarFont = new System.Drawing.Font("Tahoma", 9F);
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEnd.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(32, 97);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(213, 27);
            this.dtpEnd.TabIndex = 23;
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(64, 683);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(150, 45);
            this.sbtnReturn.TabIndex = 18;
            this.sbtnReturn.Text = "返回主界面";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // sbtnDelete
            // 
            this.sbtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnDelete.Appearance.Options.UseFont = true;
            this.sbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("sbtnDelete.Image")));
            this.sbtnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnDelete.Location = new System.Drawing.Point(64, 563);
            this.sbtnDelete.Name = "sbtnDelete";
            this.sbtnDelete.Size = new System.Drawing.Size(150, 45);
            this.sbtnDelete.TabIndex = 19;
            this.sbtnDelete.Text = "删除";
            this.sbtnDelete.Click += new System.EventHandler(this.sbtnDelete_Click);
            // 
            // frmRecordInRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 730);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecordInRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工具入库记录";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRecordInRoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public System.Windows.Forms.ComboBox cbbPeopleIn;
        public System.Windows.Forms.ComboBox cbbToolName;
        private System.Windows.Forms.ComboBox cbbToolType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton sbtnOut;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton sbtnQuery;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private DevExpress.XtraEditors.SimpleButton sbtnDelete;
        private DevExpress.XtraEditors.SimpleButton sbtnPageDown;
        private DevExpress.XtraEditors.SimpleButton sbtnPageUp;
    }
}
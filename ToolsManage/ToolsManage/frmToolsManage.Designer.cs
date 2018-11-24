namespace ToolsManage.ToolsManage
{
    partial class frmToolsManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmToolsManage));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sbtnOpenBox = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAlter = new DevExpress.XtraEditors.SimpleButton();
            this.cbbToolInRoom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sbtnExport = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnExpand = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnScrap = new DevExpress.XtraEditors.SimpleButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sbtnOpenBox);
            this.groupBox1.Controls.Add(this.sbtnAlter);
            this.groupBox1.Controls.Add(this.cbbToolInRoom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.sbtnExport);
            this.groupBox1.Controls.Add(this.sbtnQuery);
            this.groupBox1.Controls.Add(this.sbtnReturn);
            this.groupBox1.Controls.Add(this.sbtnExpand);
            this.groupBox1.Controls.Add(this.sbtnScrap);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(973, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 730);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // sbtnOpenBox
            // 
            this.sbtnOpenBox.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnOpenBox.Appearance.Options.UseFont = true;
            this.sbtnOpenBox.Enabled = false;
            this.sbtnOpenBox.Image = ((System.Drawing.Image)(resources.GetObject("sbtnOpenBox.Image")));
            this.sbtnOpenBox.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnOpenBox.Location = new System.Drawing.Point(40, 607);
            this.sbtnOpenBox.Name = "sbtnOpenBox";
            this.sbtnOpenBox.Size = new System.Drawing.Size(140, 45);
            this.sbtnOpenBox.TabIndex = 21;
            this.sbtnOpenBox.Text = "开柜门";
            this.sbtnOpenBox.Click += new System.EventHandler(this.sbtnOpenBox_Click);
            // 
            // sbtnAlter
            // 
            this.sbtnAlter.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnAlter.Appearance.Options.UseFont = true;
            this.sbtnAlter.Image = ((System.Drawing.Image)(resources.GetObject("sbtnAlter.Image")));
            this.sbtnAlter.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnAlter.Location = new System.Drawing.Point(35, 343);
            this.sbtnAlter.Name = "sbtnAlter";
            this.sbtnAlter.Size = new System.Drawing.Size(140, 45);
            this.sbtnAlter.TabIndex = 20;
            this.sbtnAlter.Text = "修改";
            this.sbtnAlter.Click += new System.EventHandler(this.sbtnAlter_Click);
            // 
            // cbbToolInRoom
            // 
            this.cbbToolInRoom.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.cbbToolInRoom.FormattingEnabled = true;
            this.cbbToolInRoom.Items.AddRange(new object[] {
            "在库",
            "借出",
            "外借超时",
            "全部"});
            this.cbbToolInRoom.Location = new System.Drawing.Point(40, 66);
            this.cbbToolInRoom.Name = "cbbToolInRoom";
            this.cbbToolInRoom.Size = new System.Drawing.Size(140, 27);
            this.cbbToolInRoom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(37, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "工具在库状态:";
            // 
            // sbtnExport
            // 
            this.sbtnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnExport.Appearance.Options.UseFont = true;
            this.sbtnExport.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExport.Image")));
            this.sbtnExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnExport.Location = new System.Drawing.Point(40, 522);
            this.sbtnExport.Name = "sbtnExport";
            this.sbtnExport.Size = new System.Drawing.Size(140, 45);
            this.sbtnExport.TabIndex = 19;
            this.sbtnExport.Text = "导出";
            this.sbtnExport.Click += new System.EventHandler(this.sbtnExport_Click);
            // 
            // sbtnQuery
            // 
            this.sbtnQuery.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnQuery.Appearance.Options.UseFont = true;
            this.sbtnQuery.Image = ((System.Drawing.Image)(resources.GetObject("sbtnQuery.Image")));
            this.sbtnQuery.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnQuery.Location = new System.Drawing.Point(40, 155);
            this.sbtnQuery.Name = "sbtnQuery";
            this.sbtnQuery.Size = new System.Drawing.Size(140, 45);
            this.sbtnQuery.TabIndex = 19;
            this.sbtnQuery.Text = "查询";
            this.sbtnQuery.Click += new System.EventHandler(this.sbtnQuery_Click);
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(40, 684);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(140, 45);
            this.sbtnReturn.TabIndex = 18;
            this.sbtnReturn.Text = "返回主界面";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // sbtnExpand
            // 
            this.sbtnExpand.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnExpand.Appearance.Options.UseFont = true;
            this.sbtnExpand.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExpand.Image")));
            this.sbtnExpand.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnExpand.Location = new System.Drawing.Point(40, 250);
            this.sbtnExpand.Name = "sbtnExpand";
            this.sbtnExpand.Size = new System.Drawing.Size(140, 45);
            this.sbtnExpand.TabIndex = 17;
            this.sbtnExpand.Text = "展开/折叠";
            this.sbtnExpand.Click += new System.EventHandler(this.sbtnExpand_Click);
            // 
            // sbtnScrap
            // 
            this.sbtnScrap.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnScrap.Appearance.Options.UseFont = true;
            this.sbtnScrap.Image = ((System.Drawing.Image)(resources.GetObject("sbtnScrap.Image")));
            this.sbtnScrap.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnScrap.Location = new System.Drawing.Point(40, 432);
            this.sbtnScrap.Name = "sbtnScrap";
            this.sbtnScrap.Size = new System.Drawing.Size(140, 45);
            this.sbtnScrap.TabIndex = 15;
            this.sbtnScrap.Text = "报废";
            this.sbtnScrap.Click += new System.EventHandler(this.sbtnScrap_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(317, 683);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "DocumentMap_32x32.png");
            this.imageList1.Images.SetKeyName(1, "Language_32x32.png");
            this.imageList1.Images.SetKeyName(2, "AlignVerticalCenter2_32x32.png");
            this.imageList1.Images.SetKeyName(3, "IDE_32x32.png");
            this.imageList1.Images.SetKeyName(4, "FontSizeIncrease_32x32.png");
            this.imageList1.Images.SetKeyName(5, "SpellCheckAsYouType_32x32.png");
            this.imageList1.Images.SetKeyName(6, "ContentArrangeInRows_32x32.png");
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(323, 730);
            this.xtraTabControl1.TabIndex = 21;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.treeView1);
            this.xtraTabPage1.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage1.Image")));
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(317, 683);
            this.xtraTabPage1.Text = "工具存放位置";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.treeView2);
            this.xtraTabPage2.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage2.Image")));
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(317, 683);
            this.xtraTabPage2.Text = "工具种类名称";
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.treeView2.ImageIndex = 0;
            this.treeView2.ImageList = this.imageList1;
            this.treeView2.Location = new System.Drawing.Point(0, 0);
            this.treeView2.Name = "treeView2";
            this.treeView2.SelectedImageIndex = 0;
            this.treeView2.Size = new System.Drawing.Size(317, 683);
            this.treeView2.TabIndex = 1;
            this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterSelect);
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gridControl1.Location = new System.Drawing.Point(323, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(650, 730);
            this.gridControl1.TabIndex = 23;
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
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // frmToolsManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 730);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Office 2010 Black";
            this.Name = "frmToolsManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库房工具管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmToolsManage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private DevExpress.XtraEditors.SimpleButton sbtnScrap;
        private DevExpress.XtraEditors.SimpleButton sbtnExpand;
        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraEditors.SimpleButton sbtnQuery;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton sbtnExport;
        private System.Windows.Forms.ComboBox cbbToolInRoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView2;
        private DevExpress.XtraEditors.SimpleButton sbtnAlter;
        private DevExpress.XtraEditors.SimpleButton sbtnOpenBox;


    }
}
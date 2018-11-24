namespace ToolsManage.ToolsManage
{
    partial class frmBoxIcPower
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoxIcPower));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sbtnPowerManage = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnExpand = new DevExpress.XtraEditors.SimpleButton();
            this.gcArea = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcArea)).BeginInit();
            this.gcArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.treeView1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(308, 730);
            this.groupControl1.TabIndex = 25;
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
            this.treeView1.Size = new System.Drawing.Size(304, 726);
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
            this.imageList1.Images.SetKeyName(4, "ContentArrangeInRows_32x32.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sbtnPowerManage);
            this.groupBox1.Controls.Add(this.sbtnNew);
            this.groupBox1.Controls.Add(this.sbtnReturn);
            this.groupBox1.Controls.Add(this.sbtnExpand);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(987, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 730);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // sbtnPowerManage
            // 
            this.sbtnPowerManage.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnPowerManage.Appearance.Options.UseFont = true;
            this.sbtnPowerManage.Image = ((System.Drawing.Image)(resources.GetObject("sbtnPowerManage.Image")));
            this.sbtnPowerManage.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnPowerManage.Location = new System.Drawing.Point(34, 210);
            this.sbtnPowerManage.Name = "sbtnPowerManage";
            this.sbtnPowerManage.Size = new System.Drawing.Size(130, 45);
            this.sbtnPowerManage.TabIndex = 23;
            this.sbtnPowerManage.Text = "权限管理";
            this.sbtnPowerManage.Click += new System.EventHandler(this.sbtnPowerManage_Click);
            // 
            // sbtnNew
            // 
            this.sbtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnNew.Appearance.Options.UseFont = true;
            this.sbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("sbtnNew.Image")));
            this.sbtnNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnNew.Location = new System.Drawing.Point(34, 43);
            this.sbtnNew.Name = "sbtnNew";
            this.sbtnNew.Size = new System.Drawing.Size(130, 45);
            this.sbtnNew.TabIndex = 20;
            this.sbtnNew.Text = "刷新";
            this.sbtnNew.Click += new System.EventHandler(this.sbtnNew_Click);
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(34, 302);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(130, 45);
            this.sbtnReturn.TabIndex = 22;
            this.sbtnReturn.Text = "返回主界面";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // sbtnExpand
            // 
            this.sbtnExpand.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnExpand.Appearance.Options.UseFont = true;
            this.sbtnExpand.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExpand.Image")));
            this.sbtnExpand.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnExpand.Location = new System.Drawing.Point(34, 125);
            this.sbtnExpand.Name = "sbtnExpand";
            this.sbtnExpand.Size = new System.Drawing.Size(130, 45);
            this.sbtnExpand.TabIndex = 21;
            this.sbtnExpand.Text = "展开/折叠";
            this.sbtnExpand.Click += new System.EventHandler(this.sbtnExpand_Click);
            // 
            // gcArea
            // 
            this.gcArea.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gcArea.AppearanceCaption.Options.UseFont = true;
            this.gcArea.Controls.Add(this.gridControl1);
            this.gcArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcArea.Location = new System.Drawing.Point(308, 0);
            this.gcArea.Name = "gcArea";
            this.gcArea.Size = new System.Drawing.Size(679, 730);
            this.gcArea.TabIndex = 0;
            this.gcArea.Text = "权限列表";
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 27);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(675, 701);
            this.gridControl1.TabIndex = 5;
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
            // frmBoxIcPower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 730);
            this.Controls.Add(this.gcArea);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBoxIcPower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工具柜IC卡权限管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBoxIcPower_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcArea)).EndInit();
            this.gcArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.GroupControl gcArea;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton sbtnPowerManage;
        private DevExpress.XtraEditors.SimpleButton sbtnNew;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private DevExpress.XtraEditors.SimpleButton sbtnExpand;
        private System.Windows.Forms.ImageList imageList1;
    }
}
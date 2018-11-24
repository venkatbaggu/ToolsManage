namespace NSB4000
{
    partial class frmSystemSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSystemSet));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbtnAddArea = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAddSf6 = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAddWsd = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnPicture = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnCycle = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAlarm = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnExpand = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 3;
            this.treeView1.Size = new System.Drawing.Size(281, 644);
            this.treeView1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "DocumentMap_32x32.png");
            this.imageList1.Images.SetKeyName(1, "GaugeStyleFullCircular_32x32.png");
            this.imageList1.Images.SetKeyName(2, "GaugeStyleLeftQuarterCircular_32x32.png");
            this.imageList1.Images.SetKeyName(3, "Status_32x32.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Location = new System.Drawing.Point(119, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 644);
            this.panel1.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(281, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(380, 644);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
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
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // sbtnAddArea
            // 
            this.sbtnAddArea.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnAddArea.Appearance.Options.UseFont = true;
            this.sbtnAddArea.Image = ((System.Drawing.Image)(resources.GetObject("sbtnAddArea.Image")));
            this.sbtnAddArea.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnAddArea.Location = new System.Drawing.Point(841, 224);
            this.sbtnAddArea.Name = "sbtnAddArea";
            this.sbtnAddArea.Size = new System.Drawing.Size(140, 45);
            this.sbtnAddArea.TabIndex = 2;
            this.sbtnAddArea.Text = "添加区域";
            this.sbtnAddArea.Click += new System.EventHandler(this.sbtnAddArea_Click);
            // 
            // sbtnAddSf6
            // 
            this.sbtnAddSf6.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnAddSf6.Appearance.Options.UseFont = true;
            this.sbtnAddSf6.Image = ((System.Drawing.Image)(resources.GetObject("sbtnAddSf6.Image")));
            this.sbtnAddSf6.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnAddSf6.Location = new System.Drawing.Point(841, 315);
            this.sbtnAddSf6.Name = "sbtnAddSf6";
            this.sbtnAddSf6.Size = new System.Drawing.Size(140, 45);
            this.sbtnAddSf6.TabIndex = 3;
            this.sbtnAddSf6.Text = "添加SF6";
            this.sbtnAddSf6.Click += new System.EventHandler(this.sbtnAddSf6_Click);
            // 
            // sbtnAddWsd
            // 
            this.sbtnAddWsd.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnAddWsd.Appearance.Options.UseFont = true;
            this.sbtnAddWsd.Image = ((System.Drawing.Image)(resources.GetObject("sbtnAddWsd.Image")));
            this.sbtnAddWsd.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnAddWsd.Location = new System.Drawing.Point(841, 403);
            this.sbtnAddWsd.Name = "sbtnAddWsd";
            this.sbtnAddWsd.Size = new System.Drawing.Size(140, 45);
            this.sbtnAddWsd.TabIndex = 4;
            this.sbtnAddWsd.Text = "添加温湿度";
            this.sbtnAddWsd.Click += new System.EventHandler(this.sbtnAddWsd_Click);
            // 
            // sbtnDelete
            // 
            this.sbtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnDelete.Appearance.Options.UseFont = true;
            this.sbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("sbtnDelete.Image")));
            this.sbtnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnDelete.Location = new System.Drawing.Point(841, 489);
            this.sbtnDelete.Name = "sbtnDelete";
            this.sbtnDelete.Size = new System.Drawing.Size(140, 45);
            this.sbtnDelete.TabIndex = 5;
            this.sbtnDelete.Text = "删除";
            this.sbtnDelete.Click += new System.EventHandler(this.sbtnDelete_Click);
            // 
            // sbtnPicture
            // 
            this.sbtnPicture.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnPicture.Appearance.Options.UseFont = true;
            this.sbtnPicture.Image = ((System.Drawing.Image)(resources.GetObject("sbtnPicture.Image")));
            this.sbtnPicture.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnPicture.Location = new System.Drawing.Point(436, 736);
            this.sbtnPicture.Name = "sbtnPicture";
            this.sbtnPicture.Size = new System.Drawing.Size(140, 45);
            this.sbtnPicture.TabIndex = 7;
            this.sbtnPicture.Text = "底图设置";
            // 
            // sbtnCycle
            // 
            this.sbtnCycle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnCycle.Appearance.Options.UseFont = true;
            this.sbtnCycle.Image = ((System.Drawing.Image)(resources.GetObject("sbtnCycle.Image")));
            this.sbtnCycle.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnCycle.Location = new System.Drawing.Point(640, 736);
            this.sbtnCycle.Name = "sbtnCycle";
            this.sbtnCycle.Size = new System.Drawing.Size(140, 45);
            this.sbtnCycle.TabIndex = 6;
            this.sbtnCycle.Text = "巡检周期";
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(841, 736);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(140, 45);
            this.sbtnReturn.TabIndex = 8;
            this.sbtnReturn.Text = "返回主界面";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // sbtnAlarm
            // 
            this.sbtnAlarm.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnAlarm.Appearance.Options.UseFont = true;
            this.sbtnAlarm.Image = ((System.Drawing.Image)(resources.GetObject("sbtnAlarm.Image")));
            this.sbtnAlarm.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnAlarm.Location = new System.Drawing.Point(234, 736);
            this.sbtnAlarm.Name = "sbtnAlarm";
            this.sbtnAlarm.Size = new System.Drawing.Size(140, 45);
            this.sbtnAlarm.TabIndex = 7;
            this.sbtnAlarm.Text = "报警阀值";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton1.Location = new System.Drawing.Point(43, 736);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(140, 45);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "数据管理";
            // 
            // sbtnExpand
            // 
            this.sbtnExpand.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnExpand.Appearance.Options.UseFont = true;
            this.sbtnExpand.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExpand.Image")));
            this.sbtnExpand.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnExpand.Location = new System.Drawing.Point(841, 138);
            this.sbtnExpand.Name = "sbtnExpand";
            this.sbtnExpand.Size = new System.Drawing.Size(140, 45);
            this.sbtnExpand.TabIndex = 11;
            this.sbtnExpand.Text = "展开/折叠";
            this.sbtnExpand.Click += new System.EventHandler(this.sbtnExpand_Click);
            // 
            // sbtnNew
            // 
            this.sbtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnNew.Appearance.Options.UseFont = true;
            this.sbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("sbtnNew.Image")));
            this.sbtnNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnNew.Location = new System.Drawing.Point(841, 55);
            this.sbtnNew.Name = "sbtnNew";
            this.sbtnNew.Size = new System.Drawing.Size(140, 45);
            this.sbtnNew.TabIndex = 10;
            this.sbtnNew.Text = "刷新";
            // 
            // frmSystemSet
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 820);
            this.Controls.Add(this.sbtnExpand);
            this.Controls.Add(this.sbtnNew);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.sbtnReturn);
            this.Controls.Add(this.sbtnAlarm);
            this.Controls.Add(this.sbtnPicture);
            this.Controls.Add(this.sbtnCycle);
            this.Controls.Add(this.sbtnDelete);
            this.Controls.Add(this.sbtnAddWsd);
            this.Controls.Add(this.sbtnAddSf6);
            this.Controls.Add(this.sbtnAddArea);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSystemSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSystemSet";
            this.Load += new System.EventHandler(this.frmSystemSet_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton sbtnAddArea;
        private DevExpress.XtraEditors.SimpleButton sbtnAddSf6;
        private DevExpress.XtraEditors.SimpleButton sbtnAddWsd;
        private DevExpress.XtraEditors.SimpleButton sbtnDelete;
        private DevExpress.XtraEditors.SimpleButton sbtnPicture;
        private DevExpress.XtraEditors.SimpleButton sbtnCycle;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.SimpleButton sbtnAlarm;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton sbtnExpand;
        private DevExpress.XtraEditors.SimpleButton sbtnNew;
    }
}
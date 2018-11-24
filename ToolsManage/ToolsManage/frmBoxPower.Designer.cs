namespace ToolsManage.ToolsManage
{
    partial class frmBoxPower
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoxPower));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sbtnSetPsw = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnExpand = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gcSetPsw = new DevExpress.XtraEditors.GroupControl();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPsw4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbPsw3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbPsw2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbPsw1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.sbtnPswExit = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnPswOk = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSetPsw)).BeginInit();
            this.gcSetPsw.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
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
            this.groupControl1.Size = new System.Drawing.Size(308, 762);
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
            this.treeView1.Size = new System.Drawing.Size(304, 758);
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
            this.groupBox1.Controls.Add(this.sbtnSetPsw);
            this.groupBox1.Controls.Add(this.sbtnNew);
            this.groupBox1.Controls.Add(this.sbtnReturn);
            this.groupBox1.Controls.Add(this.sbtnExpand);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(987, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 762);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // sbtnSetPsw
            // 
            this.sbtnSetPsw.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnSetPsw.Appearance.Options.UseFont = true;
            this.sbtnSetPsw.Image = ((System.Drawing.Image)(resources.GetObject("sbtnSetPsw.Image")));
            this.sbtnSetPsw.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnSetPsw.Location = new System.Drawing.Point(33, 208);
            this.sbtnSetPsw.Name = "sbtnSetPsw";
            this.sbtnSetPsw.Size = new System.Drawing.Size(130, 45);
            this.sbtnSetPsw.TabIndex = 19;
            this.sbtnSetPsw.Text = "设置密码";
            this.sbtnSetPsw.Click += new System.EventHandler(this.sbtnSetPsw_Click);
            // 
            // sbtnNew
            // 
            this.sbtnNew.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnNew.Appearance.Options.UseFont = true;
            this.sbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("sbtnNew.Image")));
            this.sbtnNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnNew.Location = new System.Drawing.Point(33, 41);
            this.sbtnNew.Name = "sbtnNew";
            this.sbtnNew.Size = new System.Drawing.Size(130, 45);
            this.sbtnNew.TabIndex = 16;
            this.sbtnNew.Text = "刷新";
            this.sbtnNew.Click += new System.EventHandler(this.sbtnNew_Click);
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(33, 297);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(130, 45);
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
            this.sbtnExpand.Location = new System.Drawing.Point(33, 123);
            this.sbtnExpand.Name = "sbtnExpand";
            this.sbtnExpand.Size = new System.Drawing.Size(130, 45);
            this.sbtnExpand.TabIndex = 17;
            this.sbtnExpand.Text = "展开/折叠";
            this.sbtnExpand.Click += new System.EventHandler(this.sbtnExpand_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(308, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.Size = new System.Drawing.Size(679, 762);
            this.xtraTabControl1.TabIndex = 19;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcSetPsw);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(673, 733);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // gcSetPsw
            // 
            this.gcSetPsw.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gcSetPsw.AppearanceCaption.Options.UseFont = true;
            this.gcSetPsw.Controls.Add(this.tbArea);
            this.gcSetPsw.Controls.Add(this.label2);
            this.gcSetPsw.Controls.Add(this.tbBoxName);
            this.gcSetPsw.Controls.Add(this.label1);
            this.gcSetPsw.Controls.Add(this.tbPsw4);
            this.gcSetPsw.Controls.Add(this.label12);
            this.gcSetPsw.Controls.Add(this.tbPsw3);
            this.gcSetPsw.Controls.Add(this.label11);
            this.gcSetPsw.Controls.Add(this.tbPsw2);
            this.gcSetPsw.Controls.Add(this.label10);
            this.gcSetPsw.Controls.Add(this.tbPsw1);
            this.gcSetPsw.Controls.Add(this.label9);
            this.gcSetPsw.Controls.Add(this.sbtnPswExit);
            this.gcSetPsw.Controls.Add(this.sbtnPswOk);
            this.gcSetPsw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSetPsw.Location = new System.Drawing.Point(0, 0);
            this.gcSetPsw.Name = "gcSetPsw";
            this.gcSetPsw.Size = new System.Drawing.Size(673, 733);
            this.gcSetPsw.TabIndex = 0;
            this.gcSetPsw.Text = "设置工具柜柜门密码";
            // 
            // tbArea
            // 
            this.tbArea.Enabled = false;
            this.tbArea.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tbArea.Location = new System.Drawing.Point(288, 75);
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(220, 27);
            this.tbArea.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(141, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 19);
            this.label2.TabIndex = 31;
            this.label2.Text = "区域名称：";
            // 
            // tbBoxName
            // 
            this.tbBoxName.Enabled = false;
            this.tbBoxName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tbBoxName.Location = new System.Drawing.Point(288, 137);
            this.tbBoxName.Name = "tbBoxName";
            this.tbBoxName.Size = new System.Drawing.Size(220, 27);
            this.tbBoxName.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(141, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 19);
            this.label1.TabIndex = 29;
            this.label1.Text = "工具柜名称：";
            // 
            // tbPsw4
            // 
            this.tbPsw4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tbPsw4.Location = new System.Drawing.Point(288, 430);
            this.tbPsw4.Name = "tbPsw4";
            this.tbPsw4.Size = new System.Drawing.Size(220, 27);
            this.tbPsw4.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(141, 438);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 19);
            this.label12.TabIndex = 27;
            this.label12.Text = "柜门密码4：";
            // 
            // tbPsw3
            // 
            this.tbPsw3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tbPsw3.Location = new System.Drawing.Point(288, 355);
            this.tbPsw3.Name = "tbPsw3";
            this.tbPsw3.Size = new System.Drawing.Size(220, 27);
            this.tbPsw3.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(141, 363);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 19);
            this.label11.TabIndex = 25;
            this.label11.Text = "柜门密码3：";
            // 
            // tbPsw2
            // 
            this.tbPsw2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tbPsw2.Location = new System.Drawing.Point(288, 276);
            this.tbPsw2.Name = "tbPsw2";
            this.tbPsw2.Size = new System.Drawing.Size(220, 27);
            this.tbPsw2.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(141, 284);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 19);
            this.label10.TabIndex = 23;
            this.label10.Text = "柜门密码2：";
            // 
            // tbPsw1
            // 
            this.tbPsw1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tbPsw1.Location = new System.Drawing.Point(288, 205);
            this.tbPsw1.Name = "tbPsw1";
            this.tbPsw1.Size = new System.Drawing.Size(220, 27);
            this.tbPsw1.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(141, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 19);
            this.label9.TabIndex = 21;
            this.label9.Text = "柜门密码1：";
            // 
            // sbtnPswExit
            // 
            this.sbtnPswExit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnPswExit.Appearance.Options.UseFont = true;
            this.sbtnPswExit.Image = ((System.Drawing.Image)(resources.GetObject("sbtnPswExit.Image")));
            this.sbtnPswExit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnPswExit.Location = new System.Drawing.Point(378, 511);
            this.sbtnPswExit.Name = "sbtnPswExit";
            this.sbtnPswExit.Size = new System.Drawing.Size(130, 45);
            this.sbtnPswExit.TabIndex = 20;
            this.sbtnPswExit.Text = "退出";
            this.sbtnPswExit.Click += new System.EventHandler(this.sbtnPswExit_Click);
            // 
            // sbtnPswOk
            // 
            this.sbtnPswOk.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnPswOk.Appearance.Options.UseFont = true;
            this.sbtnPswOk.Image = ((System.Drawing.Image)(resources.GetObject("sbtnPswOk.Image")));
            this.sbtnPswOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnPswOk.Location = new System.Drawing.Point(137, 511);
            this.sbtnPswOk.Name = "sbtnPswOk";
            this.sbtnPswOk.Size = new System.Drawing.Size(130, 45);
            this.sbtnPswOk.TabIndex = 19;
            this.sbtnPswOk.Text = "确认";
            this.sbtnPswOk.Click += new System.EventHandler(this.sbtnPswOk_Click);
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.gridControl1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(673, 733);
            this.xtraTabPage3.Text = "xtraTabPage3";
            // 
            // gridControl1
            // 
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(673, 733);
            this.gridControl1.TabIndex = 4;
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
            // 
            // frmBoxPower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBoxPower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工具柜柜门权限管理";
            this.Load += new System.EventHandler(this.frmBoxPower_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSetPsw)).EndInit();
            this.gcSetPsw.ResumeLayout(false);
            this.gcSetPsw.PerformLayout();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton sbtnSetPsw;
        private DevExpress.XtraEditors.SimpleButton sbtnNew;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private DevExpress.XtraEditors.SimpleButton sbtnExpand;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.GroupControl gcSetPsw;
        private System.Windows.Forms.TextBox tbPsw4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbPsw3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbPsw2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbPsw1;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.SimpleButton sbtnPswExit;
        private DevExpress.XtraEditors.SimpleButton sbtnPswOk;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox tbBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.Label label2;
    }
}
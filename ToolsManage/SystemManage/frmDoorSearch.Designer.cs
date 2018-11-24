namespace ToolsManage.SystemManage
{
    partial class frmDoorSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDoorSearch));
            this.dgvFoundControllers = new System.Windows.Forms.DataGridView();
            this.f_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f_ControllerSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f_Mask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f_Gateway = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f_PORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f_MACAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.f_PCIPAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.sbtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAlter = new DevExpress.XtraEditors.SimpleButton();
            this.lblSearchNow = new System.Windows.Forms.Label();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoundControllers)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFoundControllers
            // 
            this.dgvFoundControllers.AllowUserToAddRows = false;
            this.dgvFoundControllers.AllowUserToDeleteRows = false;
            this.dgvFoundControllers.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(125)))), ((int)(((byte)(156)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFoundControllers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFoundControllers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFoundControllers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.f_ID,
            this.f_ControllerSN,
            this.f_IP,
            this.f_Mask,
            this.f_Gateway,
            this.f_PORT,
            this.f_MACAddr,
            this.f_PCIPAddr});
            this.dgvFoundControllers.EnableHeadersVisualStyles = false;
            this.dgvFoundControllers.Location = new System.Drawing.Point(21, 47);
            this.dgvFoundControllers.Name = "dgvFoundControllers";
            this.dgvFoundControllers.ReadOnly = true;
            this.dgvFoundControllers.RowHeadersVisible = false;
            this.dgvFoundControllers.RowTemplate.Height = 23;
            this.dgvFoundControllers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFoundControllers.Size = new System.Drawing.Size(791, 514);
            this.dgvFoundControllers.TabIndex = 6;
            this.dgvFoundControllers.Tag = "已搜索到控制器";
            this.dgvFoundControllers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvFoundControllers_MouseDoubleClick);
            // 
            // f_ID
            // 
            this.f_ID.HeaderText = "";
            this.f_ID.Name = "f_ID";
            this.f_ID.ReadOnly = true;
            this.f_ID.Width = 45;
            // 
            // f_ControllerSN
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.f_ControllerSN.DefaultCellStyle = dataGridViewCellStyle2;
            this.f_ControllerSN.HeaderText = "产品序列号SN";
            this.f_ControllerSN.Name = "f_ControllerSN";
            this.f_ControllerSN.ReadOnly = true;
            this.f_ControllerSN.Width = 120;
            // 
            // f_IP
            // 
            this.f_IP.HeaderText = "IP";
            this.f_IP.Name = "f_IP";
            this.f_IP.ReadOnly = true;
            // 
            // f_Mask
            // 
            this.f_Mask.HeaderText = "子网掩码";
            this.f_Mask.Name = "f_Mask";
            this.f_Mask.ReadOnly = true;
            // 
            // f_Gateway
            // 
            this.f_Gateway.HeaderText = "默认网关";
            this.f_Gateway.Name = "f_Gateway";
            this.f_Gateway.ReadOnly = true;
            // 
            // f_PORT
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.f_PORT.DefaultCellStyle = dataGridViewCellStyle3;
            this.f_PORT.HeaderText = "PORT";
            this.f_PORT.Name = "f_PORT";
            this.f_PORT.ReadOnly = true;
            this.f_PORT.Width = 45;
            // 
            // f_MACAddr
            // 
            this.f_MACAddr.HeaderText = "MAC地址";
            this.f_MACAddr.Name = "f_MACAddr";
            this.f_MACAddr.ReadOnly = true;
            this.f_MACAddr.Width = 140;
            // 
            // f_PCIPAddr
            // 
            this.f_PCIPAddr.HeaderText = "电脑IP地址";
            this.f_PCIPAddr.Name = "f_PCIPAddr";
            this.f_PCIPAddr.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(282, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "搜索时, 请等待大约5秒钟...";
            // 
            // sbtnSearch
            // 
            this.sbtnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnSearch.Appearance.Options.UseFont = true;
            this.sbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("sbtnSearch.Image")));
            this.sbtnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnSearch.Location = new System.Drawing.Point(842, 47);
            this.sbtnSearch.Name = "sbtnSearch";
            this.sbtnSearch.Size = new System.Drawing.Size(140, 45);
            this.sbtnSearch.TabIndex = 30;
            this.sbtnSearch.Text = "搜索";
            this.sbtnSearch.Click += new System.EventHandler(this.sbtnSearch_Click);
            // 
            // sbtnAlter
            // 
            this.sbtnAlter.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnAlter.Appearance.Options.UseFont = true;
            this.sbtnAlter.Image = ((System.Drawing.Image)(resources.GetObject("sbtnAlter.Image")));
            this.sbtnAlter.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnAlter.Location = new System.Drawing.Point(842, 136);
            this.sbtnAlter.Name = "sbtnAlter";
            this.sbtnAlter.Size = new System.Drawing.Size(140, 45);
            this.sbtnAlter.TabIndex = 31;
            this.sbtnAlter.Text = "修改";
            this.sbtnAlter.Click += new System.EventHandler(this.sbtnAlter_Click);
            // 
            // lblSearchNow
            // 
            this.lblSearchNow.AutoSize = true;
            this.lblSearchNow.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchNow.ForeColor = System.Drawing.Color.Black;
            this.lblSearchNow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSearchNow.Location = new System.Drawing.Point(715, 20);
            this.lblSearchNow.Name = "lblSearchNow";
            this.lblSearchNow.Size = new System.Drawing.Size(23, 14);
            this.lblSearchNow.TabIndex = 32;
            this.lblSearchNow.Text = "----";
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(842, 304);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(140, 45);
            this.sbtnReturn.TabIndex = 33;
            this.sbtnReturn.Text = "返回主界面";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // sbtnSelect
            // 
            this.sbtnSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnSelect.Appearance.Options.UseFont = true;
            this.sbtnSelect.Image = ((System.Drawing.Image)(resources.GetObject("sbtnSelect.Image")));
            this.sbtnSelect.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnSelect.Location = new System.Drawing.Point(842, 221);
            this.sbtnSelect.Name = "sbtnSelect";
            this.sbtnSelect.Size = new System.Drawing.Size(140, 45);
            this.sbtnSelect.TabIndex = 34;
            this.sbtnSelect.Text = "选中";
            this.sbtnSelect.Click += new System.EventHandler(this.sbtnSelect_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCount.Location = new System.Drawing.Point(28, 20);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(14, 14);
            this.lblCount.TabIndex = 35;
            this.lblCount.Text = "0";
            // 
            // frmDoorSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 588);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.sbtnSelect);
            this.Controls.Add(this.sbtnReturn);
            this.Controls.Add(this.lblSearchNow);
            this.Controls.Add(this.sbtnAlter);
            this.Controls.Add(this.sbtnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvFoundControllers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoorSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "门禁控制器搜索";
            this.Load += new System.EventHandler(this.frmDoorSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoundControllers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFoundControllers;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_ControllerSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_Mask;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_Gateway;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_PORT;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_MACAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn f_PCIPAddr;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton sbtnSearch;
        private DevExpress.XtraEditors.SimpleButton sbtnAlter;
        private System.Windows.Forms.Label lblSearchNow;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private DevExpress.XtraEditors.SimpleButton sbtnSelect;
        private System.Windows.Forms.Label lblCount;
    }
}
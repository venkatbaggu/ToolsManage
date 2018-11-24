namespace ToolsManage.SystemManage
{
    partial class frmDevicePlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDevicePlan));
            this.gbXF = new DevExpress.XtraEditors.GroupControl();
            this.ucTimeInterval3 = new UCControlNew.UCTimeInterval();
            this.ucTimeInterval2 = new UCControlNew.UCTimeInterval();
            this.ucTimeInterval1 = new UCControlNew.UCTimeInterval();
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnExit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gbXF)).BeginInit();
            this.gbXF.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbXF
            // 
            this.gbXF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbXF.Controls.Add(this.ucTimeInterval3);
            this.gbXF.Controls.Add(this.ucTimeInterval2);
            this.gbXF.Controls.Add(this.ucTimeInterval1);
            this.gbXF.Location = new System.Drawing.Point(4, 12);
            this.gbXF.Name = "gbXF";
            this.gbXF.Size = new System.Drawing.Size(493, 145);
            this.gbXF.TabIndex = 0;
            this.gbXF.Text = "新风系统";
            // 
            // ucTimeInterval3
            // 
            this.ucTimeInterval3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.ucTimeInterval3.GroupIndex = 3;
            this.ucTimeInterval3.GroupName = "第三组";
            this.ucTimeInterval3.Location = new System.Drawing.Point(8, 105);
            this.ucTimeInterval3.Name = "ucTimeInterval3";
            this.ucTimeInterval3.SetEnabled = true;
            this.ucTimeInterval3.Size = new System.Drawing.Size(480, 34);
            this.ucTimeInterval3.TabIndex = 2;
            // 
            // ucTimeInterval2
            // 
            this.ucTimeInterval2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.ucTimeInterval2.GroupIndex = 2;
            this.ucTimeInterval2.GroupName = "第二组";
            this.ucTimeInterval2.Location = new System.Drawing.Point(8, 62);
            this.ucTimeInterval2.Name = "ucTimeInterval2";
            this.ucTimeInterval2.SetEnabled = true;
            this.ucTimeInterval2.Size = new System.Drawing.Size(480, 34);
            this.ucTimeInterval2.TabIndex = 1;
            // 
            // ucTimeInterval1
            // 
            this.ucTimeInterval1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.ucTimeInterval1.GroupIndex = 1;
            this.ucTimeInterval1.GroupName = "第一组";
            this.ucTimeInterval1.Location = new System.Drawing.Point(8, 24);
            this.ucTimeInterval1.Name = "ucTimeInterval1";
            this.ucTimeInterval1.SetEnabled = true;
            this.ucTimeInterval1.Size = new System.Drawing.Size(480, 29);
            this.ucTimeInterval1.TabIndex = 0;
            // 
            // sbtnOk
            // 
            this.sbtnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnOk.Appearance.Options.UseFont = true;
            this.sbtnOk.Image = ((System.Drawing.Image)(resources.GetObject("sbtnOk.Image")));
            this.sbtnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnOk.Location = new System.Drawing.Point(334, 162);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(80, 30);
            this.sbtnOk.TabIndex = 51;
            this.sbtnOk.Text = "确定";
            this.sbtnOk.Click += new System.EventHandler(this.sbtnOk_Click);
            // 
            // sbtnExit
            // 
            this.sbtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnExit.Appearance.Options.UseFont = true;
            this.sbtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExit.Image")));
            this.sbtnExit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnExit.Location = new System.Drawing.Point(418, 162);
            this.sbtnExit.Name = "sbtnExit";
            this.sbtnExit.Size = new System.Drawing.Size(80, 30);
            this.sbtnExit.TabIndex = 52;
            this.sbtnExit.Text = "取消";
            this.sbtnExit.Click += new System.EventHandler(this.sbtnExit_Click);
            // 
            // frmDevicePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(501, 195);
            this.Controls.Add(this.sbtnOk);
            this.Controls.Add(this.sbtnExit);
            this.Controls.Add(this.gbXF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDevicePlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备启停时段设置";
            this.Load += new System.EventHandler(this.frmDevicePlan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbXF)).EndInit();
            this.gbXF.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gbXF;
        private DevExpress.XtraEditors.SimpleButton sbtnOk;
        private DevExpress.XtraEditors.SimpleButton sbtnExit;
        private UCControlNew.UCTimeInterval ucTimeInterval1;
        private UCControlNew.UCTimeInterval ucTimeInterval3;
        private UCControlNew.UCTimeInterval ucTimeInterval2;
    }
}
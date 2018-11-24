namespace ToolsManage.ToolsManage
{
    partial class frmScrap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScrap));
            this.cbbScrapInfo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sbtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // cbbScrapInfo
            // 
            this.cbbScrapInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cbbScrapInfo.FormattingEnabled = true;
            this.cbbScrapInfo.Items.AddRange(new object[] {
            "寿命",
            "试验不合格",
            "升级",
            "遗失",
            "入库错误",
            "损坏",
            "其他原因"});
            this.cbbScrapInfo.Location = new System.Drawing.Point(178, 47);
            this.cbbScrapInfo.Name = "cbbScrapInfo";
            this.cbbScrapInfo.Size = new System.Drawing.Size(213, 24);
            this.cbbScrapInfo.TabIndex = 46;
            this.cbbScrapInfo.Text = "寿命";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(79, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 47;
            this.label3.Text = "报废类型：";
            // 
            // sbtnExit
            // 
            this.sbtnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnExit.Appearance.Options.UseFont = true;
            this.sbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExit.Image")));
            this.sbtnExit.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnExit.Location = new System.Drawing.Point(261, 118);
            this.sbtnExit.Name = "sbtnExit";
            this.sbtnExit.Size = new System.Drawing.Size(130, 45);
            this.sbtnExit.TabIndex = 58;
            this.sbtnExit.Text = "退出";
            this.sbtnExit.Click += new System.EventHandler(this.sbtnExit_Click);
            // 
            // sbtnOk
            // 
            this.sbtnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnOk.Appearance.Options.UseFont = true;
            this.sbtnOk.Image = ((System.Drawing.Image)(resources.GetObject("sbtnOk.Image")));
            this.sbtnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnOk.Location = new System.Drawing.Point(82, 118);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(130, 45);
            this.sbtnOk.TabIndex = 57;
            this.sbtnOk.Text = "确认";
            this.sbtnOk.Click += new System.EventHandler(this.sbtnOk_Click);
            // 
            // frmScrap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 188);
            this.Controls.Add(this.sbtnExit);
            this.Controls.Add(this.sbtnOk);
            this.Controls.Add(this.cbbScrapInfo);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScrap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报废原因";
            this.Load += new System.EventHandler(this.frmScrap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbScrapInfo;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton sbtnExit;
        private DevExpress.XtraEditors.SimpleButton sbtnOk;

    }
}
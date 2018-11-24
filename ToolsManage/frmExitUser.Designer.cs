namespace ToolsManage
{
    partial class frmExitUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExitUser));
            this.tbName = new System.Windows.Forms.TextBox();
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.tbPsd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbName.Location = new System.Drawing.Point(121, 27);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(152, 27);
            this.tbName.TabIndex = 1;
            // 
            // sbtnOk
            // 
            this.sbtnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnOk.Appearance.Options.UseFont = true;
            this.sbtnOk.Image = ((System.Drawing.Image)(resources.GetObject("sbtnOk.Image")));
            this.sbtnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnOk.Location = new System.Drawing.Point(26, 145);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(100, 40);
            this.sbtnOk.TabIndex = 55;
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
            this.sbtnExit.Location = new System.Drawing.Point(209, 145);
            this.sbtnExit.Name = "sbtnExit";
            this.sbtnExit.Size = new System.Drawing.Size(100, 40);
            this.sbtnExit.TabIndex = 56;
            this.sbtnExit.Text = "取消";
            this.sbtnExit.Click += new System.EventHandler(this.sbtnExit_Click);
            // 
            // tbPsd
            // 
            this.tbPsd.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbPsd.Location = new System.Drawing.Point(121, 79);
            this.tbPsd.Name = "tbPsd";
            this.tbPsd.PasswordChar = '*';
            this.tbPsd.Size = new System.Drawing.Size(152, 27);
            this.tbPsd.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(49, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 19);
            this.label2.TabIndex = 53;
            this.label2.Text = "密  码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(49, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 19);
            this.label1.TabIndex = 52;
            this.label1.Text = "用户名:";
            // 
            // frmExitUser
            // 
            this.AcceptButton = this.sbtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.sbtnExit;
            this.ClientSize = new System.Drawing.Size(364, 209);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.sbtnOk);
            this.Controls.Add(this.sbtnExit);
            this.Controls.Add(this.tbPsd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExitUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户认证";
            this.Load += new System.EventHandler(this.frmExitUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbName;
        private DevExpress.XtraEditors.SimpleButton sbtnOk;
        private DevExpress.XtraEditors.SimpleButton sbtnExit;
        private System.Windows.Forms.TextBox tbPsd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
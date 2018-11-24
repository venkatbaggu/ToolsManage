namespace ToolsManage.DoorManage
{
    partial class frmDoorInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDoorInfo));
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.cbbDoorSn = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(453, 182);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(130, 45);
            this.sbtnReturn.TabIndex = 32;
            this.sbtnReturn.Text = "退出";
            // 
            // sbtnOk
            // 
            this.sbtnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnOk.Appearance.Options.UseFont = true;
            this.sbtnOk.Image = ((System.Drawing.Image)(resources.GetObject("sbtnOk.Image")));
            this.sbtnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnOk.Location = new System.Drawing.Point(147, 182);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(130, 45);
            this.sbtnOk.TabIndex = 31;
            this.sbtnOk.Text = "确定";
            // 
            // cbbDoorSn
            // 
            this.cbbDoorSn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.cbbDoorSn.Location = new System.Drawing.Point(368, 34);
            this.cbbDoorSn.Name = "cbbDoorSn";
            this.cbbDoorSn.Size = new System.Drawing.Size(82, 27);
            this.cbbDoorSn.TabIndex = 33;
            this.cbbDoorSn.Text = "11927";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(182, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 19);
            this.label7.TabIndex = 34;
            this.label7.Text = "门禁控制器S/N:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(368, 108);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(82, 27);
            this.textBox1.TabIndex = 35;
            this.textBox1.Text = "11927";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(182, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 36;
            this.label1.Text = "S/N:";
            // 
            // frmDoorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 262);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbDoorSn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.sbtnReturn);
            this.Controls.Add(this.sbtnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoorInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加门禁控制器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private DevExpress.XtraEditors.SimpleButton sbtnOk;
        private System.Windows.Forms.TextBox cbbDoorSn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}
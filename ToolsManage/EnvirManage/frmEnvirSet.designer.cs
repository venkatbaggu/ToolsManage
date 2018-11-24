namespace ToolsManage.EnvirManage
{
    partial class frmEnvirSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnvirSet));
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbDisAuto = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHumiStop = new System.Windows.Forms.TextBox();
            this.tbHumiRun = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbAirHotTempSet = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tbAirHotStop = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tbAirHotRun = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tbCoolTempSet = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tbCoolStop = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tbCoolRun = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbtnOk
            // 
            this.sbtnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnOk.Appearance.Options.UseFont = true;
            this.sbtnOk.Image = ((System.Drawing.Image)(resources.GetObject("sbtnOk.Image")));
            this.sbtnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnOk.Location = new System.Drawing.Point(121, 368);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(130, 45);
            this.sbtnOk.TabIndex = 30;
            this.sbtnOk.Text = "确定";
            this.sbtnOk.Click += new System.EventHandler(this.sbtnOk_Click);
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(581, 368);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(130, 45);
            this.sbtnReturn.TabIndex = 31;
            this.sbtnReturn.Text = "退出";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbDisAuto);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(848, 98);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            // 
            // tbDisAuto
            // 
            this.tbDisAuto.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbDisAuto.Location = new System.Drawing.Point(207, 40);
            this.tbDisAuto.Name = "tbDisAuto";
            this.tbDisAuto.Size = new System.Drawing.Size(58, 27);
            this.tbDisAuto.TabIndex = 11;
            this.tbDisAuto.Text = "60";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(39, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 19);
            this.label9.TabIndex = 10;
            this.label9.Text = "非自动控制保持时间：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(271, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 17);
            this.label10.TabIndex = 12;
            this.label10.Text = "(分钟)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbHumiStop);
            this.groupBox2.Controls.Add(this.tbHumiRun);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(0, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(848, 98);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "除湿阀值设置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "(RH%)";
            // 
            // tbHumiStop
            // 
            this.tbHumiStop.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbHumiStop.Location = new System.Drawing.Point(396, 40);
            this.tbHumiStop.Name = "tbHumiStop";
            this.tbHumiStop.Size = new System.Drawing.Size(58, 27);
            this.tbHumiStop.TabIndex = 8;
            this.tbHumiStop.Text = "40";
            // 
            // tbHumiRun
            // 
            this.tbHumiRun.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbHumiRun.Location = new System.Drawing.Point(121, 40);
            this.tbHumiRun.Name = "tbHumiRun";
            this.tbHumiRun.Size = new System.Drawing.Size(58, 27);
            this.tbHumiRun.TabIndex = 7;
            this.tbHumiRun.Text = "60";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(39, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "启动阀值：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(185, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "(RH%)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(315, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 19);
            this.label8.TabIndex = 5;
            this.label8.Text = "停止阀值:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Controls.Add(this.tbAirHotTempSet);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Controls.Add(this.label27);
            this.groupBox7.Controls.Add(this.tbAirHotStop);
            this.groupBox7.Controls.Add(this.label28);
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.tbAirHotRun);
            this.groupBox7.Controls.Add(this.label30);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.tbCoolTempSet);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.tbCoolStop);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.tbCoolRun);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox7.Location = new System.Drawing.Point(0, 196);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(848, 136);
            this.groupBox7.TabIndex = 37;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "空调参数设置";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(767, 95);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(58, 19);
            this.label25.TabIndex = 25;
            this.label25.Text = "（℃）";
            // 
            // tbAirHotTempSet
            // 
            this.tbAirHotTempSet.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbAirHotTempSet.Location = new System.Drawing.Point(702, 92);
            this.tbAirHotTempSet.Name = "tbAirHotTempSet";
            this.tbAirHotTempSet.Size = new System.Drawing.Size(59, 27);
            this.tbAirHotTempSet.TabIndex = 24;
            this.tbAirHotTempSet.Text = "20";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(566, 95);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(145, 19);
            this.label26.TabIndex = 23;
            this.label26.Text = "制热温度设置值：";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label27.Location = new System.Drawing.Point(457, 95);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(58, 19);
            this.label27.TabIndex = 22;
            this.label27.Text = "（℃）";
            // 
            // tbAirHotStop
            // 
            this.tbAirHotStop.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbAirHotStop.Location = new System.Drawing.Point(396, 92);
            this.tbAirHotStop.Name = "tbAirHotStop";
            this.tbAirHotStop.Size = new System.Drawing.Size(59, 27);
            this.tbAirHotStop.TabIndex = 21;
            this.tbAirHotStop.Text = "10";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label28.Location = new System.Drawing.Point(296, 95);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(111, 19);
            this.label28.TabIndex = 20;
            this.label28.Text = "制热停止值：";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label29.Location = new System.Drawing.Point(200, 95);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(58, 19);
            this.label29.TabIndex = 19;
            this.label29.Text = "（℃）";
            // 
            // tbAirHotRun
            // 
            this.tbAirHotRun.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbAirHotRun.Location = new System.Drawing.Point(139, 92);
            this.tbAirHotRun.Name = "tbAirHotRun";
            this.tbAirHotRun.Size = new System.Drawing.Size(59, 27);
            this.tbAirHotRun.TabIndex = 18;
            this.tbAirHotRun.Text = "5";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label30.Location = new System.Drawing.Point(39, 95);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(111, 19);
            this.label30.TabIndex = 17;
            this.label30.Text = "制热启动值：";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label23.Location = new System.Drawing.Point(767, 43);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(58, 19);
            this.label23.TabIndex = 16;
            this.label23.Text = "（℃）";
            // 
            // tbCoolTempSet
            // 
            this.tbCoolTempSet.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbCoolTempSet.Location = new System.Drawing.Point(702, 40);
            this.tbCoolTempSet.Name = "tbCoolTempSet";
            this.tbCoolTempSet.Size = new System.Drawing.Size(59, 27);
            this.tbCoolTempSet.TabIndex = 15;
            this.tbCoolTempSet.Text = "22";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(566, 43);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(145, 19);
            this.label24.TabIndex = 14;
            this.label24.Text = "制冷温度设置值：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(457, 43);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 19);
            this.label19.TabIndex = 13;
            this.label19.Text = "（℃）";
            // 
            // tbCoolStop
            // 
            this.tbCoolStop.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbCoolStop.Location = new System.Drawing.Point(396, 40);
            this.tbCoolStop.Name = "tbCoolStop";
            this.tbCoolStop.Size = new System.Drawing.Size(59, 27);
            this.tbCoolStop.TabIndex = 12;
            this.tbCoolStop.Text = "25";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(296, 43);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(111, 19);
            this.label20.TabIndex = 11;
            this.label20.Text = "制冷停止值：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(200, 43);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(58, 19);
            this.label21.TabIndex = 10;
            this.label21.Text = "（℃）";
            // 
            // tbCoolRun
            // 
            this.tbCoolRun.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tbCoolRun.Location = new System.Drawing.Point(139, 40);
            this.tbCoolRun.Name = "tbCoolRun";
            this.tbCoolRun.Size = new System.Drawing.Size(59, 27);
            this.tbCoolRun.TabIndex = 9;
            this.tbCoolRun.Text = "28";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(39, 43);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(111, 19);
            this.label22.TabIndex = 2;
            this.label22.Text = "制冷启动值：";
            // 
            // frmEnvirSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 442);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.sbtnReturn);
            this.Controls.Add(this.sbtnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEnvirSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "环境参数设置";
            this.Load += new System.EventHandler(this.frmEnvirSet_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbtnOk;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbDisAuto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbHumiStop;
        private System.Windows.Forms.TextBox tbHumiRun;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbAirHotTempSet;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tbAirHotStop;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbAirHotRun;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbCoolTempSet;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbCoolStop;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbCoolRun;
        private System.Windows.Forms.Label label22;
    }
}
namespace ToolsManage.SystemManage
{
    partial class frmDataClear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataClear));
            this.cboxBorrow = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sbtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.cboxDoolPower = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sbtnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.cboxTest = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboxDoorInOut = new System.Windows.Forms.CheckBox();
            this.cboxInRoom = new System.Windows.Forms.CheckBox();
            this.cboxAll = new System.Windows.Forms.CheckBox();
            this.cboxScrap = new System.Windows.Forms.CheckBox();
            this.cboxAlarm = new System.Windows.Forms.CheckBox();
            this.cboxBoxOpen = new System.Windows.Forms.CheckBox();
            this.cboxBoxPower = new System.Windows.Forms.CheckBox();
            this.cboxEnvirEvent = new System.Windows.Forms.CheckBox();
            this.cboxEnvirData = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboxBorrow
            // 
            this.cboxBorrow.AutoSize = true;
            this.cboxBorrow.ForeColor = System.Drawing.Color.Blue;
            this.cboxBorrow.Location = new System.Drawing.Point(27, 97);
            this.cboxBorrow.Name = "cboxBorrow";
            this.cboxBorrow.Size = new System.Drawing.Size(130, 23);
            this.cboxBorrow.TabIndex = 0;
            this.cboxBorrow.Text = "工具借还记录";
            this.cboxBorrow.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(28, 337);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(580, 99);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "                                        \r\n　　　注意：系统数据清理，将清理数据库中所有相关表的数据，因此在系统数据清理前" +
                "，请作好备份工作，以免造成大量数据丢失，带来不必要的损失。";
            // 
            // sbtnDelete
            // 
            this.sbtnDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnDelete.Appearance.Options.UseFont = true;
            this.sbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("sbtnDelete.Image")));
            this.sbtnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnDelete.Location = new System.Drawing.Point(12, 65);
            this.sbtnDelete.Name = "sbtnDelete";
            this.sbtnDelete.Size = new System.Drawing.Size(130, 45);
            this.sbtnDelete.TabIndex = 23;
            this.sbtnDelete.Text = "清 除";
            this.sbtnDelete.Click += new System.EventHandler(this.sbtnDelete_Click);
            // 
            // cboxDoolPower
            // 
            this.cboxDoolPower.AutoSize = true;
            this.cboxDoolPower.ForeColor = System.Drawing.Color.Blue;
            this.cboxDoolPower.Location = new System.Drawing.Point(258, 97);
            this.cboxDoolPower.Name = "cboxDoolPower";
            this.cboxDoolPower.Size = new System.Drawing.Size(130, 23);
            this.cboxDoolPower.TabIndex = 0;
            this.cboxDoolPower.Text = "门禁授权记录";
            this.cboxDoolPower.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sbtnDelete);
            this.groupBox2.Controls.Add(this.sbtnReturn);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(497, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 289);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作";
            // 
            // sbtnReturn
            // 
            this.sbtnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.sbtnReturn.Appearance.Options.UseFont = true;
            this.sbtnReturn.Image = ((System.Drawing.Image)(resources.GetObject("sbtnReturn.Image")));
            this.sbtnReturn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.sbtnReturn.Location = new System.Drawing.Point(12, 162);
            this.sbtnReturn.Name = "sbtnReturn";
            this.sbtnReturn.Size = new System.Drawing.Size(130, 45);
            this.sbtnReturn.TabIndex = 20;
            this.sbtnReturn.Text = "返回主界面";
            this.sbtnReturn.Click += new System.EventHandler(this.sbtnReturn_Click);
            // 
            // cboxTest
            // 
            this.cboxTest.AutoSize = true;
            this.cboxTest.ForeColor = System.Drawing.Color.Blue;
            this.cboxTest.Location = new System.Drawing.Point(27, 233);
            this.cboxTest.Name = "cboxTest";
            this.cboxTest.Size = new System.Drawing.Size(130, 23);
            this.cboxTest.TabIndex = 0;
            this.cboxTest.Text = "工具试验记录";
            this.cboxTest.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboxEnvirData);
            this.groupBox1.Controls.Add(this.cboxEnvirEvent);
            this.groupBox1.Controls.Add(this.cboxBoxPower);
            this.groupBox1.Controls.Add(this.cboxBoxOpen);
            this.groupBox1.Controls.Add(this.cboxAlarm);
            this.groupBox1.Controls.Add(this.cboxScrap);
            this.groupBox1.Controls.Add(this.cboxAll);
            this.groupBox1.Controls.Add(this.cboxBorrow);
            this.groupBox1.Controls.Add(this.cboxDoolPower);
            this.groupBox1.Controls.Add(this.cboxTest);
            this.groupBox1.Controls.Add(this.cboxDoorInOut);
            this.groupBox1.Controls.Add(this.cboxInRoom);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(28, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 289);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据表信息";
            // 
            // cboxDoorInOut
            // 
            this.cboxDoorInOut.AutoSize = true;
            this.cboxDoorInOut.ForeColor = System.Drawing.Color.Blue;
            this.cboxDoorInOut.Location = new System.Drawing.Point(258, 65);
            this.cboxDoorInOut.Name = "cboxDoorInOut";
            this.cboxDoorInOut.Size = new System.Drawing.Size(130, 23);
            this.cboxDoorInOut.TabIndex = 0;
            this.cboxDoorInOut.Text = "门禁出入记录";
            this.cboxDoorInOut.UseVisualStyleBackColor = true;
            // 
            // cboxInRoom
            // 
            this.cboxInRoom.AutoSize = true;
            this.cboxInRoom.ForeColor = System.Drawing.Color.Blue;
            this.cboxInRoom.Location = new System.Drawing.Point(27, 65);
            this.cboxInRoom.Name = "cboxInRoom";
            this.cboxInRoom.Size = new System.Drawing.Size(130, 23);
            this.cboxInRoom.TabIndex = 0;
            this.cboxInRoom.Text = "工具入库记录";
            this.cboxInRoom.UseVisualStyleBackColor = true;
            // 
            // cboxAll
            // 
            this.cboxAll.AutoSize = true;
            this.cboxAll.ForeColor = System.Drawing.Color.Red;
            this.cboxAll.Location = new System.Drawing.Point(27, 32);
            this.cboxAll.Name = "cboxAll";
            this.cboxAll.Size = new System.Drawing.Size(62, 23);
            this.cboxAll.TabIndex = 1;
            this.cboxAll.Text = "全选";
            this.cboxAll.UseVisualStyleBackColor = true;
            this.cboxAll.CheckedChanged += new System.EventHandler(this.cboxAll_CheckedChanged);
            // 
            // cboxScrap
            // 
            this.cboxScrap.AutoSize = true;
            this.cboxScrap.ForeColor = System.Drawing.Color.Blue;
            this.cboxScrap.Location = new System.Drawing.Point(27, 130);
            this.cboxScrap.Name = "cboxScrap";
            this.cboxScrap.Size = new System.Drawing.Size(130, 23);
            this.cboxScrap.TabIndex = 2;
            this.cboxScrap.Text = "工具报废记录";
            this.cboxScrap.UseVisualStyleBackColor = true;
            // 
            // cboxAlarm
            // 
            this.cboxAlarm.AutoSize = true;
            this.cboxAlarm.ForeColor = System.Drawing.Color.Blue;
            this.cboxAlarm.Location = new System.Drawing.Point(258, 198);
            this.cboxAlarm.Name = "cboxAlarm";
            this.cboxAlarm.Size = new System.Drawing.Size(96, 23);
            this.cboxAlarm.TabIndex = 3;
            this.cboxAlarm.Text = "报警记录";
            this.cboxAlarm.UseVisualStyleBackColor = true;
            // 
            // cboxBoxOpen
            // 
            this.cboxBoxOpen.AutoSize = true;
            this.cboxBoxOpen.ForeColor = System.Drawing.Color.Blue;
            this.cboxBoxOpen.Location = new System.Drawing.Point(27, 163);
            this.cboxBoxOpen.Name = "cboxBoxOpen";
            this.cboxBoxOpen.Size = new System.Drawing.Size(147, 23);
            this.cboxBoxOpen.TabIndex = 4;
            this.cboxBoxOpen.Text = "工具柜开柜记录";
            this.cboxBoxOpen.UseVisualStyleBackColor = true;
            // 
            // cboxBoxPower
            // 
            this.cboxBoxPower.AutoSize = true;
            this.cboxBoxPower.ForeColor = System.Drawing.Color.Blue;
            this.cboxBoxPower.Location = new System.Drawing.Point(27, 198);
            this.cboxBoxPower.Name = "cboxBoxPower";
            this.cboxBoxPower.Size = new System.Drawing.Size(147, 23);
            this.cboxBoxPower.TabIndex = 5;
            this.cboxBoxPower.Text = "工具柜授权记录";
            this.cboxBoxPower.UseVisualStyleBackColor = true;
            // 
            // cboxEnvirEvent
            // 
            this.cboxEnvirEvent.AutoSize = true;
            this.cboxEnvirEvent.ForeColor = System.Drawing.Color.Blue;
            this.cboxEnvirEvent.Location = new System.Drawing.Point(258, 130);
            this.cboxEnvirEvent.Name = "cboxEnvirEvent";
            this.cboxEnvirEvent.Size = new System.Drawing.Size(130, 23);
            this.cboxEnvirEvent.TabIndex = 6;
            this.cboxEnvirEvent.Text = "环境事件记录";
            this.cboxEnvirEvent.UseVisualStyleBackColor = true;
            // 
            // cboxEnvirData
            // 
            this.cboxEnvirData.AutoSize = true;
            this.cboxEnvirData.ForeColor = System.Drawing.Color.Blue;
            this.cboxEnvirData.Location = new System.Drawing.Point(258, 163);
            this.cboxEnvirData.Name = "cboxEnvirData";
            this.cboxEnvirData.Size = new System.Drawing.Size(130, 23);
            this.cboxEnvirData.TabIndex = 7;
            this.cboxEnvirData.Text = "环境数据记录";
            this.cboxEnvirData.UseVisualStyleBackColor = true;
            // 
            // frmDataClear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 458);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataClear";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据清理";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cboxBorrow;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SimpleButton sbtnDelete;
        private System.Windows.Forms.CheckBox cboxDoolPower;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton sbtnReturn;
        private System.Windows.Forms.CheckBox cboxTest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cboxDoorInOut;
        private System.Windows.Forms.CheckBox cboxInRoom;
        private System.Windows.Forms.CheckBox cboxAll;
        private System.Windows.Forms.CheckBox cboxScrap;
        private System.Windows.Forms.CheckBox cboxAlarm;
        private System.Windows.Forms.CheckBox cboxBoxPower;
        private System.Windows.Forms.CheckBox cboxBoxOpen;
        private System.Windows.Forms.CheckBox cboxEnvirData;
        private System.Windows.Forms.CheckBox cboxEnvirEvent;
    }
}
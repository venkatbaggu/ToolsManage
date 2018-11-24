namespace ToolsManage.UCControlNew
{
    partial class UCTimeInterval
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTime = new System.Windows.Forms.Panel();
            this.nudStopMin = new System.Windows.Forms.NumericUpDown();
            this.nudStopHour = new System.Windows.Forms.NumericUpDown();
            this.nudStartMin = new System.Windows.Forms.NumericUpDown();
            this.nudStartHour = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.chbGroup = new System.Windows.Forms.CheckBox();
            this.pnlTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartHour)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTime
            // 
            this.pnlTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTime.Controls.Add(this.nudStopMin);
            this.pnlTime.Controls.Add(this.nudStopHour);
            this.pnlTime.Controls.Add(this.nudStartMin);
            this.pnlTime.Controls.Add(this.nudStartHour);
            this.pnlTime.Controls.Add(this.label1);
            this.pnlTime.Controls.Add(this.label2);
            this.pnlTime.Controls.Add(this.label7);
            this.pnlTime.Controls.Add(this.label8);
            this.pnlTime.Controls.Add(this.label14);
            this.pnlTime.Controls.Add(this.label13);
            this.pnlTime.Location = new System.Drawing.Point(62, 1);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(352, 26);
            this.pnlTime.TabIndex = 42;
            // 
            // nudStopMin
            // 
            this.nudStopMin.Location = new System.Drawing.Point(290, 2);
            this.nudStopMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudStopMin.Name = "nudStopMin";
            this.nudStopMin.Size = new System.Drawing.Size(36, 21);
            this.nudStopMin.TabIndex = 48;
            // 
            // nudStopHour
            // 
            this.nudStopHour.Location = new System.Drawing.Point(236, 2);
            this.nudStopHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudStopHour.Name = "nudStopHour";
            this.nudStopHour.Size = new System.Drawing.Size(36, 21);
            this.nudStopHour.TabIndex = 47;
            // 
            // nudStartMin
            // 
            this.nudStartMin.Location = new System.Drawing.Point(119, 3);
            this.nudStartMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudStartMin.Name = "nudStartMin";
            this.nudStartMin.Size = new System.Drawing.Size(36, 21);
            this.nudStartMin.TabIndex = 46;
            // 
            // nudStartHour
            // 
            this.nudStartHour.Location = new System.Drawing.Point(67, 3);
            this.nudStartHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudStartHour.Name = "nudStartHour";
            this.nudStartHour.Size = new System.Drawing.Size(36, 21);
            this.nudStartHour.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "启动时间:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "停止时间:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(102, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 41;
            this.label7.Text = "时";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(157, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 42;
            this.label8.Text = "分";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(273, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 43;
            this.label14.Text = "时";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(328, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 44;
            this.label13.Text = "分";
            // 
            // chbGroup
            // 
            this.chbGroup.AutoSize = true;
            this.chbGroup.Location = new System.Drawing.Point(2, 7);
            this.chbGroup.Name = "chbGroup";
            this.chbGroup.Size = new System.Drawing.Size(60, 16);
            this.chbGroup.TabIndex = 41;
            this.chbGroup.Text = "第一组";
            this.chbGroup.UseVisualStyleBackColor = true;
            this.chbGroup.CheckedChanged += new System.EventHandler(this.chbGroup_CheckedChanged);
            // 
            // UCTimeInterval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.Controls.Add(this.pnlTime);
            this.Controls.Add(this.chbGroup);
            this.Name = "UCTimeInterval";
            this.Size = new System.Drawing.Size(416, 29);
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartHour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTime;
        private System.Windows.Forms.NumericUpDown nudStopMin;
        private System.Windows.Forms.NumericUpDown nudStopHour;
        private System.Windows.Forms.NumericUpDown nudStartMin;
        private System.Windows.Forms.NumericUpDown nudStartHour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chbGroup;
    }
}

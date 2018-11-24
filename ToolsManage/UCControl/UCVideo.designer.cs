namespace DCMSystem.UCControl
{
    partial class UCVideo
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
            this.components = new System.ComponentModel.Container();
            this.cmsVideo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.红外设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.鱼眼预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.鹰眼预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置巡航ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlRight = new DAMSystem.DefineControl.DoubleBuffPanel();
            this.pnlBottom = new DAMSystem.DefineControl.DoubleBuffPanel();
            this.pnlLeft = new DAMSystem.DefineControl.DoubleBuffPanel();
            this.pnlTop = new DAMSystem.DefineControl.DoubleBuffPanel();
            this.pnlTool = new System.Windows.Forms.Panel();
            this.rbYS = new System.Windows.Forms.RadioButton();
            this.rbQJ = new System.Windows.Forms.RadioButton();
            this.picVideo = new System.Windows.Forms.PictureBox();
            this.cmsVideo.SuspendLayout();
            this.pnlTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsVideo
            // 
            this.cmsVideo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭ToolStripMenuItem,
            this.截图ToolStripMenuItem,
            this.红外设置ToolStripMenuItem,
            this.鱼眼预览ToolStripMenuItem,
            this.鹰眼预览ToolStripMenuItem,
            this.设置巡航ToolStripMenuItem});
            this.cmsVideo.Name = "cmsVideo";
            this.cmsVideo.Size = new System.Drawing.Size(125, 136);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭ToolStripMenuItem.Text = "停止";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // 截图ToolStripMenuItem
            // 
            this.截图ToolStripMenuItem.Name = "截图ToolStripMenuItem";
            this.截图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.截图ToolStripMenuItem.Text = "截图";
            this.截图ToolStripMenuItem.Click += new System.EventHandler(this.截图ToolStripMenuItem_Click);
            // 
            // 红外设置ToolStripMenuItem
            // 
            this.红外设置ToolStripMenuItem.Name = "红外设置ToolStripMenuItem";
            this.红外设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.红外设置ToolStripMenuItem.Text = "红外设置";
            this.红外设置ToolStripMenuItem.Visible = false;
            this.红外设置ToolStripMenuItem.Click += new System.EventHandler(this.红外设置ToolStripMenuItem_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Lime;
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.IsChangeBackColor = false;
            this.pnlRight.Location = new System.Drawing.Point(273, 2);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(2, 258);
            this.pnlRight.TabIndex = 3;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.Lime;
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.IsChangeBackColor = false;
            this.pnlBottom.Location = new System.Drawing.Point(2, 260);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(273, 2);
            this.pnlBottom.TabIndex = 2;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Lime;
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.IsChangeBackColor = false;
            this.pnlLeft.Location = new System.Drawing.Point(0, 2);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(2, 260);
            this.pnlLeft.TabIndex = 1;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Lime;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.IsChangeBackColor = false;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(275, 2);
            this.pnlTop.TabIndex = 0;
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.rbYS);
            this.pnlTool.Controls.Add(this.rbQJ);
            this.pnlTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTool.Location = new System.Drawing.Point(2, 225);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(271, 35);
            this.pnlTool.TabIndex = 5;
            // 
            // rbYS
            // 
            this.rbYS.AutoSize = true;
            this.rbYS.ForeColor = System.Drawing.Color.Blue;
            this.rbYS.Location = new System.Drawing.Point(161, 11);
            this.rbYS.Name = "rbYS";
            this.rbYS.Size = new System.Drawing.Size(71, 16);
            this.rbYS.TabIndex = 1;
            this.rbYS.Text = "原始画面";
            this.rbYS.UseVisualStyleBackColor = true;
            // 
            // rbQJ
            // 
            this.rbQJ.AutoSize = true;
            this.rbQJ.Checked = true;
            this.rbQJ.ForeColor = System.Drawing.Color.Blue;
            this.rbQJ.Location = new System.Drawing.Point(19, 11);
            this.rbQJ.Name = "rbQJ";
            this.rbQJ.Size = new System.Drawing.Size(71, 16);
            this.rbQJ.TabIndex = 0;
            this.rbQJ.TabStop = true;
            this.rbQJ.Text = "全景画面";
            this.rbQJ.UseVisualStyleBackColor = true;
            this.rbQJ.CheckedChanged += new System.EventHandler(this.rbQJ_CheckedChanged);
            // 
            // picVideo
            // 
            this.picVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.picVideo.ContextMenuStrip = this.cmsVideo;
            this.picVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picVideo.Location = new System.Drawing.Point(2, 2);
            this.picVideo.Name = "picVideo";
            this.picVideo.Size = new System.Drawing.Size(271, 223);
            this.picVideo.TabIndex = 6;
            this.picVideo.TabStop = false;
            this.picVideo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picVideo_MouseClick);
            this.picVideo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picVideo_MouseDoubleClick);
            this.picVideo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picVideo_MouseDown);
            this.picVideo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picVideo_MouseMove);
            this.picVideo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picVideo_MouseUp);
            // 
            // UCVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picVideo);
            this.Controls.Add(this.pnlTool);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlTop);
            this.Name = "UCVideo";
            this.Size = new System.Drawing.Size(275, 262);
            this.Load += new System.EventHandler(this.UCVideo_Load);
            this.cmsVideo.ResumeLayout(false);
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DAMSystem.DefineControl.DoubleBuffPanel pnlTop;
        private DAMSystem.DefineControl.DoubleBuffPanel pnlLeft;
        private DAMSystem.DefineControl.DoubleBuffPanel pnlBottom;
        private DAMSystem.DefineControl.DoubleBuffPanel pnlRight;
        private System.Windows.Forms.ContextMenuStrip cmsVideo;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 截图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 红外设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 鱼眼预览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 鹰眼预览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置巡航ToolStripMenuItem;
        private System.Windows.Forms.Panel pnlTool;
        private System.Windows.Forms.PictureBox picVideo;
        private System.Windows.Forms.RadioButton rbYS;
        private System.Windows.Forms.RadioButton rbQJ;
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAMSystem.DefineControl
{
    public partial class LabelNew : Label
    {
        public LabelNew()
        {
            InitializeComponent();
            this.DoubleBuffered = true;//设置本窗体
            this.SetStyle(ControlStyles.UserPaint, true);//自绘
            this.SetStyle(ControlStyles.DoubleBuffer, true);// 双缓冲
            //this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);   //透明效果
            this.UpdateStyles();
        }



        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}

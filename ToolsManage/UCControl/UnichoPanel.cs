using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAMSystem.DefineControl
{
    public partial class UnichoPanel : Panel
    {
        public UnichoPanel()
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
        //Left
        bool m_LeftDrawEnble = true;

        public bool LeftDrawEnble
        {
            get { return m_LeftDrawEnble; }
            set { m_LeftDrawEnble = value; }
        }

        Color m_LeftLineColor = Color.Black;

        public Color LeftLineColor
        {
            get { return m_LeftLineColor; }
            set { m_LeftLineColor = value; }
        }

        float m_LeftLineWidth = 2;

        public float LeftLineWidth
        {
            get { return m_LeftLineWidth; }
            set { m_LeftLineWidth = value; }
        }
        DashStyle m_LeftLineStyle = DashStyle.Solid;

        public DashStyle LeftLineStyle
        {
            get { return m_LeftLineStyle; }
            set { m_LeftLineStyle = value; }
        }

        //Right
        bool m_RightDrawEnble = true;

        public bool RightDrawEnble
        {
            get { return m_RightDrawEnble; }
            set { m_RightDrawEnble = value; }
        }

        Color m_RightLineColor = Color.Blue;

        public Color RightLineColor
        {
            get { return m_RightLineColor; }
            set { m_RightLineColor = value; }
        }

        float m_RightLineWidth = 2;

        public float RightLineWidth
        {
            get { return m_RightLineWidth; }
            set { m_RightLineWidth = value; }
        }

        DashStyle m_RightLineStyle = DashStyle.Solid;

        public DashStyle RightLineStyle
        {
            get { return m_RightLineStyle; }
            set { m_RightLineStyle = value; }
        }

        //Top
        bool m_TopDrawEnble = true;

        public bool TopDrawEnble
        {
            get { return m_TopDrawEnble; }
            set { m_TopDrawEnble = value; }
        }

        private Color m_TopLineColor = Color.Black;

        public Color TopLineColor
        {
            get { return m_TopLineColor; }
            set { m_TopLineColor = value; }
        }

        float m_TopLineWidth = 2;

        public float TopLineWidth
        {
            get { return m_TopLineWidth; }
            set { m_TopLineWidth = value; }
        }

        DashStyle m_TopLineStyle = DashStyle.Solid;

        public DashStyle TopLineStyle
        {
            get { return m_TopLineStyle; }
            set { m_TopLineStyle = value; }
        }
        //Buttom
        bool m_ButtomDrawEnble = true;

        public bool ButtomDrawEnble
        {
            get { return m_ButtomDrawEnble; }
            set { m_ButtomDrawEnble = value; }
        }

        Color m_ButtomLineColor = Color.Green;

        public Color ButtomLineColor
        {
            get { return m_ButtomLineColor; }
            set { m_ButtomLineColor = value; }
        }

        float m_ButtomLineWidth = 2;

        public float ButtomLineWidth
        {
            get { return m_ButtomLineWidth; }
            set { m_ButtomLineWidth = value; }
        }
        DashStyle m_ButtomLineStyle = DashStyle.Solid;

        public DashStyle ButtomLineStyle
        {
            get { return m_ButtomLineStyle; }
            set { m_ButtomLineStyle = value; }
        }



        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            //ControlPaint.DrawBorder(pe.Graphics, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height),
            //    m_leftColor,this.ClientRectangle.Width,ButtonBorderStyle.None,
            //    m_TopColor,this.ClientRectangle.Height,ButtonBorderStyle.None,
            //    m_RightColor,this.ClientRectangle.Height, ButtonBorderStyle.Dotted,
            //    m_ButtomColor,this.ClientRectangle.Height, ButtonBorderStyle.None);

            //ControlPaint.DrawBorder(pe.Graphics, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height), Color.Red, ButtonBorderStyle.Dotted);
            if (m_LeftDrawEnble)
            {
                Pen pen = new Pen(m_LeftLineColor, m_LeftLineWidth);
                pen.DashStyle = m_LeftLineStyle;
                pe.Graphics.DrawLine(pen, 0, 0, 0, this.ClientRectangle.Height);

            }
            if (m_RightDrawEnble)
            {
                Pen pen = new Pen(m_RightLineColor, m_RightLineWidth);
                //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                //pen.DashPattern = new float[] { 1, 1 };
                pen.DashStyle = RightLineStyle;
                pe.Graphics.DrawLine(pen, this.ClientRectangle.Width, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);

            }
            if (m_TopDrawEnble)
            {
                Pen pen = new Pen(m_TopLineColor, m_TopLineWidth);
                pen.DashStyle = TopLineStyle;
                pe.Graphics.DrawLine(pen, 0, 0, this.ClientRectangle.Width, 0);

            }
            if (m_ButtomDrawEnble)
            {
                Pen pen = new Pen(m_ButtomLineColor, m_ButtomLineWidth);
                pen.DashStyle = ButtomLineStyle;
                pe.Graphics.DrawLine(pen, 0, this.ClientRectangle.Height, this.ClientRectangle.Width, this.ClientRectangle.Height);

            }

            //ControlPaint.DrawFocusRectangle(pe.Graphics,  new Rectangle(0, 0, this.ClientRectangle.Width , this.ClientRectangle.Height ), m_lineColor,Color.Red );
            // 调用基类 OnPaint
            base.OnPaint(pe);
        }
    }
}

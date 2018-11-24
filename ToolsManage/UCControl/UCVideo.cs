using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolsManage.VideoManage;

namespace DCMSystem.UCControl
{
    public enum EagleMode
    {
        /// <summary>
        /// 全景
        /// </summary>
        QJ,
        /// <summary>
        /// 原始
        /// </summary>
        YS,
    }
    public delegate void delCloseVideo(object sender);
    public delegate void delCapturePic(int channelno);
    public delegate void delThermalSet(object sender);
    public partial class UCVideo : UserControl
    {
        public System.Windows.Forms.MouseEventHandler MouseClick;
        public System.Windows.Forms.MouseEventHandler MouseDoubleClick;
        public System.Windows.Forms.MouseEventHandler MouseDown;
        public System.Windows.Forms.MouseEventHandler MouseMove;
        public System.Windows.Forms.MouseEventHandler MouseUp;
        public System.Windows.Forms.MouseEventHandler MouseWheel;
        public delegate void delEagleModeChange(UCVideo ucvideo);
        public delCloseVideo e_CloseVideo;  //关闭视频流
        public delCapturePic e_CapturePic;  //抓图
        public delThermalSet e_ThermalSet;  //红外设置
        public event delEagleModeChange e_EagleModeChange;  //鹰眼模式切换

        public UCVideo()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public void SetBorderColor(bool IsActivate)
        {
            this.pnlTop.BackColor = IsActivate == true ? Color.Lime : Color.White;
            this.pnlLeft.BackColor = IsActivate == true ? Color.Lime : Color.White;
            this.pnlBottom.BackColor = IsActivate == true ? Color.Lime : Color.White;
            this.pnlRight.BackColor = IsActivate == true ? Color.Lime : Color.White;
        }

        #region 属性
        public bool IsQJVideo
        {
            get 
            {
                if (this.rbQJ.Checked)
                    return true;
                else
                    return false;
            }
        }

        private int m_lUserID = 0;

        public int LUserID
        {
            get { return m_lUserID; }
            set { m_lUserID = value; }
        }

        private bool m_IsPlaying = false;
        /// <summary>
        /// 是否视频播放中
        /// </summary>
        public bool IsPlaying
        {
            get
            {
                return m_IsPlaying;
            }
            set
            {
                m_IsPlaying = value;
                if (value == false)
                    cmsVideo.Enabled = false;
                else
                    cmsVideo.Enabled = true;
            }
        }

        /// <summary>
        /// 摄像头地址
        /// </summary>
        public int Addr
        {
            get;
            set;
        }

        /// <summary>
        /// 摄像头厂家
        /// </summary>
        public int CameraManufac
        {
            get;
            set;
        }

        /// <summary>
        /// 视频宽度
        /// </summary>
        public int PicWidth
        {
            get { return this.picVideo.Width; }
        }

        /// <summary>
        /// 视频高度
        /// </summary>
        public int PicHeight
        {
            get { return this.picVideo.Height; }
        }

        private int m_CameraType = 0;
        /// <summary>
        /// 摄像头类型
        /// </summary>
        public int CameraType
        {
            get { return m_CameraType; }
            set 
            {
                m_CameraType = value;
                if (m_CameraType == 0)
                {//海康普通摄像头
                    this.红外设置ToolStripMenuItem.Visible = false;
                    this.鱼眼预览ToolStripMenuItem.Visible = false;
                    this.鹰眼预览ToolStripMenuItem.Visible = false;
                    //this.设置巡航ToolStripMenuItem.Visible = false;
                    this.pnlTool.Dock = DockStyle.None;
                    this.pnlTool.Visible = false;
                }
                if (m_CameraType == 1)
                {//红外热成像
                    //this.红外设置ToolStripMenuItem.Visible = true;
                    this.鱼眼预览ToolStripMenuItem.Visible = false;
                    this.鹰眼预览ToolStripMenuItem.Visible = false;
                    //this.设置巡航ToolStripMenuItem.Visible = false;
                    this.pnlTool.Dock = DockStyle.None;
                    this.pnlTool.Visible = false;
                }
                else if(m_CameraType==2)
                {//红外可见光
                    this.红外设置ToolStripMenuItem.Visible = false;
                    this.鱼眼预览ToolStripMenuItem.Visible = false;
                    this.鹰眼预览ToolStripMenuItem.Visible = false;
                    //this.设置巡航ToolStripMenuItem.Visible = true;
                    this.pnlTool.Dock = DockStyle.None;
                    this.pnlTool.Visible = false;
                }
                else if(m_CameraType==3)
                {//海康鱼眼
                    this.红外设置ToolStripMenuItem.Visible = false;
                    //this.鱼眼预览ToolStripMenuItem.Visible = true;
                    this.鹰眼预览ToolStripMenuItem.Visible = false;
                    this.设置巡航ToolStripMenuItem.Visible = false;
                    this.pnlTool.Dock = DockStyle.None;
                    this.pnlTool.Visible = false;
                }
                else if (m_CameraType == 4)
                {//海康鹰眼
                    this.红外设置ToolStripMenuItem.Visible = false;
                    this.鱼眼预览ToolStripMenuItem.Visible = false;
                    this.鹰眼预览ToolStripMenuItem.Visible = true;
                    //this.设置巡航ToolStripMenuItem.Visible = false;
                    this.picVideo.MouseWheel+=picVideo_MouseWheel;
                    this.pnlTool.Dock = DockStyle.Bottom;
                    this.pnlTool.Visible = true;
                }
            }
        }

        /// <summary>
        /// 摄像头IP
        /// </summary>
        public string IP
        {
            get; set;
        }
        /// <summary>
        /// 摄像头端口
        /// </summary>
        public int Port
        { 
            get; set; 
        }

        /// <summary>
        /// 摄像头通道号
        /// </summary>
        public int ChannelNo
        { 
            get; set; 
        }

        /// <summary>
        /// 摄像头ID
        /// </summary>
        public int CameraID
        {
            get; set;
        }
        /// <summary>
        /// 视频句柄
        /// </summary>
        public int VideoHandle
        {
            get;
            set;
        }

        public IntPtr PicHandle
        {
            get { return this.picVideo.Handle; }
        }

        public PictureBox Pic
        {
            get { return this.picVideo; }
        }

        private CHCNetSDK.NET_DVR_PREVIEWINFO m_HikPreViewInfo;
        /// <summary>
        /// 海康摄像头预览信息
        /// </summary>
        public CHCNetSDK.NET_DVR_PREVIEWINFO HikPreViewInfo
        {
            get { return m_HikPreViewInfo; }
            set { m_HikPreViewInfo = value; }
        }

        #endregion

        private void picVideo_MouseClick(object sender, MouseEventArgs e)
        {
            if (MouseClick != null)
            {
                MouseClick(this, e);
                this.picVideo.Focus();
            }
        }

        private void picVideo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseDoubleClick != null)
                MouseDoubleClick(this, e);
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (e_CloseVideo != null)
                e_CloseVideo(this);
        }

        private void 截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (e_CapturePic != null)
                e_CapturePic(this.ChannelNo);
        }

        private void UCVideo_Load(object sender, EventArgs e)
        {
            VideoHandle = -1;
        }

        private void 红外设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (e_ThermalSet != null)
                e_ThermalSet(this);
        }

        private void picVideo_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseDown != null)
                MouseDown(this, e);
        }

        private void picVideo_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseMove != null)
                MouseMove(this, e);
        }

        private void picVideo_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseUp != null)
                MouseUp(this, e);
        }

        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picVideo_MouseWheel(object sender, MouseEventArgs e)
        {
            if (MouseWheel != null)
                MouseWheel(sender, e);
        }

        private void rbQJ_CheckedChanged(object sender, EventArgs e)
        {
            if (e_EagleModeChange != null)
            {
                e_EagleModeChange(this);
            }
        }
    }
}

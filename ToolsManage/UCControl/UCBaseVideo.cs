using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using DCMSystem.UCControl;
using DAMSystem.DefineControl;
using HikVideo;

namespace DAMSystem.UCControl
{
    public delegate void delSelChannel(int index);
    public partial class UCBaseVideo : UserControl
    {
        public UCBaseVideo()
        {
            InitializeComponent();
        }
        public UCVideo CurVideo = null;
        public int CameraCount = 0;
        public List<UCVideo> listVideo = new List<UCVideo>();
        public List<NVRChannelInfo> listChannelInfo = null;
        public event delSelChannel e_SelChannel;

        public void JudgeVideo(Control pan,int picCount)
        {
            if (picCount > 16)
            {
                MsgBox.ShowWarn("视频数量过大！");
                return;
            }
            if (picCount > 12)
            {//一排放4个 4排
                CalcVideoSizeAndPos(pan, 4, 4);
            }
            else if (picCount > 9)
            {//一排放4个 3排
                CalcVideoSizeAndPos(pan, 3, 4);
            }
            else if (picCount > 6)
            {//一排放3个 3排
                CalcVideoSizeAndPos(pan, 3, 3);
            }
            else if (picCount > 4)
            {//一排放3个 2排
                CalcVideoSizeAndPos(pan, 2, 3);
            }
            else if (picCount > 2)
            {//一排放2个 2排
                CalcVideoSizeAndPos(pan, 2, 2);
            }
            else if (picCount == 2)
            {//一排放2个 1排
                CalcVideoSizeAndPos(pan, 1, 2);
            }
            else
            {
                CalcVideoSizeAndPos(pan, 1, 1);
            }
            if (CurVideo != null)
                ActivateVideo(listVideo, Convert.ToInt32(CurVideo.Tag));
            else
                ActivateVideo(listVideo, 0);
        }

        public void CalcVideoSizeAndPos(Control pan,int rowcount, int colcount)
        {
            int XPos1 = 0, YPos1 = 0;
            foreach (Control ctrl in pan.Controls)
            {
                if (ctrl.GetType() == typeof(UCVideo))
                {
                    UCVideo video = ctrl as UCVideo;
                    video.Visible = true;
                    int width = pan.Width, height = pan.Height;
                    video.Width = width / colcount;
                    video.Height = height / rowcount;
                    int picTag = Convert.ToInt32(video.Tag);
                    XPos1 = (picTag % colcount) * video.Width;
                    YPos1 = (picTag / colcount) * video.Height;                 
                    video.Location = new Point(XPos1, YPos1);
                }
            }
        }

        public virtual void SelectChannel(int channelno)
        { }

        /// <summary>
        /// 关闭所有视频
        /// </summary>
        public virtual void CloseAllVideo(bool IsExit)
        { }

        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="state"></param>
        public virtual void PTZControl(object sender, int state,int channelno)
        { }

        /// <summary>
        /// 视频回放
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        public virtual void PlayBack(DateTime start,DateTime end)
        { }

        /// <summary>
        /// 按文件名回放
        /// </summary>
        public virtual void PlayBackByName()
        { }


        /// <summary>
        /// 绑定通道信息
        /// </summary>
        public virtual void BindChannelInfo()
        {
        }

        public virtual void CapturePic()
        { }

        /// <summary>
        /// 激活视频
        /// </summary>
        /// <param name="videoindex"></param>
        public void ActivateVideo(List<UCVideo> listvideo, int videoindex)
        {
            foreach (UCVideo video in listvideo)
            {
                if (Convert.ToInt16(video.Tag) == videoindex)
                {//激活
                    CurVideo = video;
                    video.SetBorderColor(true);
                    if (e_SelChannel != null)
                    {
                        e_SelChannel(video.ChannelNo);
                    }
                }
                else
                {
                    video.SetBorderColor(false);
                }
            }
        }  
    }
}

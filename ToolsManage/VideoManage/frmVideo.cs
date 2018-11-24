using Common;
using Common.FileLog;
using DAMSystem.UCControl;
using DAMSystem.UCControl.VideoUtil;
using HikVideo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAMSystem
{
    public partial class frmVideo : Form
    {
        public frmVideo(int subid)
        {
            InitializeComponent();
            SubID = subid;
        }
        int SubID = 0;
        UCBaseVideo CurVideo=null;

        private bool IsVideoChange(Type videotype)
        {
            bool isChangle = false;
            if (this.pnlVideo.Controls.Count > 0)
            {
                Control ctrl = this.pnlVideo.Controls[0];
                if (ctrl.GetType() != videotype)
                {
                    UCBaseVideo oldVideo = ctrl as UCBaseVideo;
                    oldVideo.CloseAllVideo(true);
                    this.pnlVideo.Controls.Remove(ctrl);
                    ctrl = null;
                    isChangle = true;
                }
            }
            else
                isChangle = true;
            return isChangle;
        }
        /// <summary>
        /// 普通摄像头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommVideo_Click(object sender, EventArgs e)
        {
            if (IsVideoChange(typeof(UCHikVideo)))
            {
                UCHikVideo hikVideo = new UCHikVideo(SubID);
                hikVideo.Dock = DockStyle.Fill;
                this.pnlVideo.Controls.Add(hikVideo);
                CurVideo = hikVideo;
            }            
        }
        /// <summary>
        /// 鹰眼全景摄像头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EagleVideo_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 红外热成像摄像头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThermalVideo_Click(object sender, EventArgs e)
        {
        }

        private void 云台启动命令buttonPTZ_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurVideo == null)
            {
                MsgBox.ShowWarn("请先预览视频！");
                return;
            }
            CurVideo.PTZControl(sender, 0, 0);
        }

        private void 云台停止命令buttonPTZ_MouseUp(object sender, MouseEventArgs e)
        {
            if (CurVideo == null)
            {
                MsgBox.ShowWarn("请先预览视频！");
                return;
            }
            CurVideo.PTZControl(sender, 1, 0);
        }

        private void frmVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.pnlVideo.Controls.Count > 0)
            {
                Control ctrl = this.pnlVideo.Controls[0];
                UCBaseVideo oldVideo = ctrl as UCBaseVideo;
                oldVideo.CloseAllVideo(true);
                this.pnlVideo.Controls.Remove(ctrl);
                ctrl = null;
            }
        }

        private void frmVideo_Load(object sender, EventArgs e)
        {
            DateTime timeCur = DateTime.Now;
            dtpStart.Value = new System.DateTime(timeCur.Year, timeCur.Month, timeCur.Day, 0, 0, 0);
            dtpEnd.Value = new System.DateTime(timeCur.Year, timeCur.Month, timeCur.Day, 23, 59, 59);
            SetVideo();
            BindChannelInfo();
            if(CurVideo!=null)
                CurVideo.e_SelChannel+=CurVideo_e_SelChannel;
        }

        /// <summary>
        /// 绑定通道信息
        /// </summary>
        private void BindChannelInfo()
        {
            if (CurVideo != null)
            {
                if (CurVideo.listChannelInfo != null && CurVideo.listChannelInfo.Count > 0)
                {
                    cmbYTChannel.DataSource = CurVideo.listChannelInfo;
                    cmbYTChannel.DisplayMember = "ChannelName";
                    cmbYTChannel.ValueMember = "ChannelNo";
                    this.cmbYTChannel.SelectedIndexChanged += new System.EventHandler(this.cmbYTChannel_SelectedIndexChanged);
                }
            }
        }

        private void SetVideo()
        {
            try
            {
                //CameraParm cp = Context.dbOper.CameraParmBL.GetEntityWhere("SubID=" + SubID,null);
                //if (cp != null)
                //{
                //    //只有普通摄像头
                //    if (cp.IsExistThermal <= 0 && cp.IsExistHawkeye <= 0)
                //    {
                //        this.pnlVideoType.Dock = DockStyle.None;
                //        this.pnlVideoType.Visible = false;
                //        CommVideo_Click(null, null);
                //    }
                //    //有红外热成像摄像头
                //    if (cp.IsExistThermal <= 0)
                //    {
                //        this.ThermalVideo.Visible = false;
                //    }
                //    //有鹰眼全景摄像头
                //    if (cp.IsExistHawkeye <= 0)
                //    {
                //        this.EagleVideo.Visible = false;
                //    }
                //}
                this.pnlVideoType.Dock = DockStyle.None;
                this.pnlVideoType.Visible = false;
                CommVideo_Click(null, null);
            }
            catch (Exception ex)
            {
                TXTWriteHelper.WriteException("frmVideo-SetVideo-"+ex.ToString());
            }
        }

        private void btnStartPlay_Click(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpEnd.Value)
            {
                MsgBox.ShowWarn("回放开始时间不能大于结束时间！");
                return;
            }
            if (dtpStart.Value.Day != dtpEnd.Value.Day)
            {
                MsgBox.ShowWarn("请选择同一天进行回放！");
                return;
            }
            CurVideo.PlayBack(dtpStart.Value, dtpEnd.Value);
        }

        private void cmbYTChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYTChannel.SelectedIndex >= 0)
            {
                //NVRChannelInfo channelinfo = cmbYTChannel.SelectedValue as NVRChannelInfo;
                int channelno = Convert.ToInt32(cmbYTChannel.SelectedValue);
                CurVideo.SelectChannel(channelno);
            }
        }

        private void CurVideo_e_SelChannel(int channelno)
        {
            try
            {
                this.cmbYTChannel.SelectedValue = channelno;
            }
            catch (Exception ex)
            { }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (CurVideo != null)
            {
                CurVideo.CapturePic();
            }
        }

        private void btnVideoPlayBack_Click(object sender, EventArgs e)
        {
            CurVideo.PlayBackByName();
        }
    }
}

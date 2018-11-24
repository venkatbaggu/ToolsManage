using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DCMSystem.UCControl;
using DAMSystem.DefineControl;
using System.IO;
using DevComponents.DotNetBar;
using System.Runtime.InteropServices;
using ToolsManage.VideoManage;
using ToolsManage.Common;
using Common;
using Common.FileLog;

namespace DAMSystem.UCControl.VideoUtil
{
   
    public partial class UCHikVideo : UCBaseVideo
    {
        public UCHikVideo(int _subid)
        {
            InitializeComponent();
            SubID = _subid;     
            //InitPlayBack();
            //InitPlayBackByTime();
        }
        private uint m_StreamPlayMode = 0;  //0是实时流模式，用于实时预览；1是文件流模式，用于远程回放
        private bool IsPreviewing = false;
        private bool m_bMoved = false;
        private PlayCtrl.DRAWPOINT m_strPrePoint = new PlayCtrl.DRAWPOINT();
        private UCVideo EagleVideo = null;
        private int m_lUserID = 0;
        private bool m_bInitSDK;
        private CHCNetSDK.REALDATACALLBACK[] m_fRealData;
        private Int32 m_iPreviewType = 0;
        private static IntPtr[] m_ptrRealHandle;
        private Int32[] m_lRealHandle;
        private Int32[] m_Channel;   //摄像头通道
        private PlayCtrl.DISPLAYCBFUN[] m_fDisplayFun;
        private bool IsCallBack = false;
        private byte StartDChan = 0;
        private int SubID = 0;
        private bool IsLoginOk = false;
        private Dictionary<int, int> dicRealHandlerChannel = new Dictionary<int, int>();  //实时流句柄对应的通道号
        

        //private UCVideo CurVideo = null;
        private uint iLastErr = 0;     
        private Dictionary<int, string> dicChanInfo = new Dictionary<int, string>();
        private CHCNetSDK.REALDATACALLBACK[] m_RealDataCallBack = new CHCNetSDK.REALDATACALLBACK[10];

        private void UCHikVideo_Load(object sender, EventArgs e)
        {
            DateTime timeCur = DateTime.Now;
            dateTimeStart.Value = new System.DateTime(timeCur.Year, timeCur.Month, timeCur.Day, 0, 0, 0);
            dateTimeEnd.Value = new System.DateTime(timeCur.Year, timeCur.Month, timeCur.Day, 23, 59, 59);
            tpPlayBack.Parent = null;
            InitNVR();
            LoginNVR();
            GetEagleChannelAblity();
            InitVideo(PicNine, null);
            GetChannelInfo();
            PreviewVideo();
            //InitPlayBack();
        }        

        /// <summary>
        /// 初始化视频图像句柄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitVideo(object sender, EventArgs e)
        {
            try
            {
                //DoubleBuffPanel pic = sender as DoubleBuffPanel;
                //if (pic != null)
                {
                    //int videoCount = Convert.ToInt32(pic.Tag);
                    int videoCount = Configurations.CameraCount;
                    CameraCount = videoCount;
                    m_lRealHandle = new int[CameraCount];
                    m_Channel = new int[CameraCount];
                    for (int i = 0; i < m_Channel.Length; i++)
                    {
                        m_Channel[i] = -1;
                    }
                    m_ptrRealHandle = new IntPtr[CameraCount];
                    for (int j = 0; j < videoCount; j++)
                    {
                        int channelno = j + StartDChan;
                        m_lRealHandle[j] = -1;
                        UCVideo video = new UCVideo();
                        video.Tag = j.ToString();
                        video.ChannelNo = j + StartDChan;
                        video.Name = "PlayWnd" + (j + 1).ToString();
                        video.Pic.ContextMenuStrip = null;
                        if (dicChannelType.ContainsKey(channelno))
                        {
                            if (dicChannelType[channelno] == EnumChannelType.EagleChannel)
                            {//鹰眼
                                //video.SetBorderColor(true);
                                video.CameraType = 4;
                                video.MouseDown += MouseDown;
                                video.MouseUp += MouseUp;
                                video.MouseMove += MouseMove;
                                video.MouseWheel += MouseWheel;
                                video.e_EagleModeChange+=video_e_EagleModeChange;
                            }
                            else
                            {
                                video.CameraType = 0;
                            }
                        }
                        else
                        {
                            video.CameraType = 0;
                        }
                        video.e_CloseVideo += CloseVideo;
                        video.e_CapturePic += CapturePic;
                        video.MouseClick += pic_MouseClick;
                        video.MouseDoubleClick += pic_MouseDoubleClick;
                        m_ptrRealHandle[j] = video.Pic.Handle;
                        listVideo.Add(video);
                        this.pnlVideo.Controls.Add(video);
                    }
                    JudgeVideo(this.pnlVideo,videoCount);
                }
            }
            catch (Exception ex)
            {
                //TXTWriteHelper.WriteException("InitVideo"+ex.ToString());
            }
        }        

        /// <summary>
        /// 登录NVR
        /// </summary>
        private void LoginNVR()
        {
            try
            {
                uint dwReturned = 0;
                InitDeviceInfo();
                CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLoginInfo = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();
                CHCNetSDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();
                //string DVRIPAddress = cameraParm.DEVICEIP;
                //Int16 DVRPortNumber = (short)cameraParm.PORT;
                //string DVRUserName = cameraParm.VIDEOUSERNAME;
                //string DVRPassword = cameraParm.PASSWORD;
                struLoginInfo.sDeviceAddress = Configurations.VideoIP;
                struLoginInfo.sUserName = Configurations.VideoUserName;
                struLoginInfo.sPassword = Configurations.VideoPSW;
                struLoginInfo.wPort = (ushort)Configurations.VideoPort;
                CHCNetSDK.NET_DVR_SetConnectTime(5000, 1);//设置超时时间    
                CHCNetSDK.NET_DVR_NETCFG_V30 struNetCfg = new CHCNetSDK.NET_DVR_NETCFG_V30();
                struNetCfg.init();
                CHCNetSDK.NET_DVR_DEVICECFG_V40 struDevCfg = new CHCNetSDK.NET_DVR_DEVICECFG_V40();
                struDevCfg.sDVRName = new byte[CHCNetSDK.NAME_LEN];
                struDevCfg.sSerialNumber = new byte[CHCNetSDK.SERIALNO_LEN];
                struDevCfg.byDevTypeName = new byte[CHCNetSDK.DEV_TYPE_NAME_LEN];
                m_lUserID = CHCNetSDK.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);
                //m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref dev);
                //dicChanInfo = nvrConfig.GetChanParm(m_lUserID, dev);  //获取视频通道参数
                if (m_lUserID == -1)
                {
                    MessageBox.Show("视频登录失败！");
                }
                else
                {//登录成功
                    IsLoginOk = true;
                    struDeviceInfo = struDeviceInfoV40.struDeviceV30;
                }
                g_struDeviceInfo[m_iDeviceIndex].byCharaterEncodeType = struDeviceInfoV40.byCharEncodeType;
                g_struDeviceInfo[m_iDeviceIndex].lLoginID = m_lUserID;
                g_struDeviceInfo[m_iDeviceIndex].chSerialNumber = System.Text.Encoding.UTF8.GetString(struDeviceInfo.sSerialNumber).TrimEnd('\0');
                g_struDeviceInfo[m_iDeviceIndex].iDeviceIndex = m_iDeviceIndex;
                g_struDeviceInfo[m_iDeviceIndex].iDeviceType = (int)struDeviceInfo.wDevType;
                g_struDeviceInfo[m_iDeviceIndex].iDeviceChanNum = (int)(struDeviceInfo.byChanNum + struDeviceInfo.byIPChanNum + struDeviceInfo.byHighDChanNum * 256);
                g_struDeviceInfo[m_iDeviceIndex].iStartChan = (int)struDeviceInfo.byStartChan;
                g_struDeviceInfo[m_iDeviceIndex].iDiskNum = (int)struDeviceInfo.byDiskNum;
                g_struDeviceInfo[m_iDeviceIndex].iAlarmInNum = (int)struDeviceInfo.byAlarmInPortNum;
                g_struDeviceInfo[m_iDeviceIndex].iAlarmOutNum = (int)struDeviceInfo.byAlarmOutPortNum;
                g_struDeviceInfo[m_iDeviceIndex].iAudioNum = (int)struDeviceInfo.byAlarmOutPortNum;
                g_struDeviceInfo[m_iDeviceIndex].iAnalogChanNum = (int)struDeviceInfo.byChanNum;
                g_struDeviceInfo[m_iDeviceIndex].iIPChanNum = (int)(struDeviceInfo.byIPChanNum + struDeviceInfo.byHighDChanNum * 256);
                g_struDeviceInfo[m_iDeviceIndex].byZeroChanNum = struDeviceInfo.byZeroChanNum;
                g_struDeviceInfo[m_iDeviceIndex].byStartDTalkChan = struDeviceInfo.byStartDTalkChan;
                g_struDeviceInfo[m_iDeviceIndex].byLanguageType = struDeviceInfo.byLanguageType;
                g_struDeviceInfo[m_iDeviceIndex].byMirrorChanNum = struDeviceInfo.byMirrorChanNum;
                g_struDeviceInfo[m_iDeviceIndex].wStartMirrorChanNo = struDeviceInfo.wStartMirrorChanNo;
                g_struDeviceInfo[m_iDeviceIndex].byAudioInputChanNum = struDeviceInfo.byVoiceInChanNum;
                g_struDeviceInfo[m_iDeviceIndex].byStartAudioInputChanNo = struDeviceInfo.byStartVoiceInChanNo;
                if (1 == (struDeviceInfo.bySupport & 0x80))
                {
                    g_struDeviceInfo[m_iDeviceIndex].byMainProto = (byte)(struDeviceInfo.byMainProto + 2);
                    g_struDeviceInfo[m_iDeviceIndex].bySubProto = (byte)(struDeviceInfo.bySubProto + 2);
                }
                else
                {
                    g_struDeviceInfo[m_iDeviceIndex].byMainProto = struDeviceInfo.byMainProto;
                    g_struDeviceInfo[m_iDeviceIndex].bySubProto = struDeviceInfo.bySubProto;
                }
                g_struDeviceInfo[m_iDeviceIndex].bySupport1 = struDeviceInfo.bySupport1;
                g_struDeviceInfo[m_iDeviceIndex].bySupport2 = struDeviceInfo.bySupport2;
                g_struDeviceInfo[m_iDeviceIndex].bySupport7 = struDeviceInfo.bySupport7;
                g_struDeviceInfo[m_iDeviceIndex].byLanguageType = struDeviceInfo.byLanguageType;
                uint dwSize = (uint)Marshal.SizeOf(struNetCfg);
                IntPtr ptrNetCfg = Marshal.AllocHGlobal((int)dwSize);
                Marshal.StructureToPtr(struNetCfg, ptrNetCfg, false);
                if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_NETCFG_V30, 0, ptrNetCfg, dwSize, ref dwReturned))
                {
                }
                else
                {
                    //IPv6 temporary unrealized 
                    struNetCfg = (CHCNetSDK.NET_DVR_NETCFG_V30)Marshal.PtrToStructure(ptrNetCfg, typeof(CHCNetSDK.NET_DVR_NETCFG_V30));
                    g_struDeviceInfo[m_iDeviceIndex].chDeviceMultiIP = struNetCfg.struMulticastIpAddr.sIpV4;
                    string strTemp = string.Format("multi-cast ipv4{0}", struNetCfg.struMulticastIpAddr.sIpV4);
                }
                Marshal.FreeHGlobal(ptrNetCfg);

                dwReturned = 0;
                uint dwSize2 = (uint)Marshal.SizeOf(struDevCfg);
                IntPtr ptrDevCfg = Marshal.AllocHGlobal((int)dwSize2);
                Marshal.StructureToPtr(struDevCfg, ptrDevCfg, false);

                if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_DEVICECFG_V40, 0, ptrDevCfg, dwSize2, ref dwReturned))
                {
                }
                else
                {
                    struDevCfg = (CHCNetSDK.NET_DVR_DEVICECFG_V40)Marshal.PtrToStructure(ptrDevCfg, typeof(CHCNetSDK.NET_DVR_DEVICECFG_V40));
                    if (g_struDeviceInfo[m_iDeviceIndex].iDeviceType != (int)struDevCfg.wDevType)
                    {
                        string strTemp = null;
                        string strShow = null;
                        strShow = strTemp + g_struDeviceInfo[m_iDeviceIndex].iDeviceType.ToString() + struDevCfg.wDevType.ToString();
                        MessageBox.Show(strShow);
                    }
                    g_struDeviceInfo[m_iDeviceIndex].chDeviceName = System.Text.Encoding.UTF8.GetString(struDevCfg.byDevTypeName).Trim('\0');
                    g_struDeviceInfo[m_iDeviceIndex].dwDevSoftVer = struDevCfg.dwSoftwareVersion;
                }
                Marshal.FreeHGlobal(ptrDevCfg);

                if (g_struDeviceInfo[m_iDeviceIndex].iIPChanNum >= 0)
                {
                    if (g_struDeviceInfo[m_iDeviceIndex].iIPChanNum == 0)
                    {
                        g_struDeviceInfo[m_iDeviceIndex].pStruIPParaCfgV40 = new CHCNetSDK.NET_DVR_IPPARACFG_V40[1];
                    }
                    else
                    {
                        if (g_struDeviceInfo[m_iDeviceIndex].iIPChanNum % CHCNetSDK.MAX_CHANNUM_V30 == 0)
                        {
                            g_struDeviceInfo[m_iDeviceIndex].pStruIPParaCfgV40 =
                                new CHCNetSDK.NET_DVR_IPPARACFG_V40[g_struDeviceInfo[m_iDeviceIndex].iIPChanNum / CHCNetSDK.MAX_CHANNUM_V30];
                        }
                        else
                        {
                            g_struDeviceInfo[m_iDeviceIndex].pStruIPParaCfgV40 =
                                new CHCNetSDK.NET_DVR_IPPARACFG_V40[g_struDeviceInfo[m_iDeviceIndex].iIPChanNum / CHCNetSDK.MAX_CHANNUM_V30 + 1];
                        }
                    }
                }

                if (DoGetDeviceResoureCfg(m_iDeviceIndex))
                {
                    
                }
            }
            catch (Exception ex)
            { }
        }    

        #region 播放视频的事件
        /// <summary>
        /// 关闭当前播放视频
        /// </summary>
        private void CloseVideo(object sender)
        {
            UCVideo videoWindow = sender as UCVideo;
            if (videoWindow != null)
            {
                int intWindIndex = Convert.ToInt32(videoWindow.Tag);
                if (videoWindow.VideoHandle >= 0)
                {
                    CHCNetSDK.NET_DVR_StopRealPlay(videoWindow.VideoHandle);
                    videoWindow.VideoHandle = -1;
                }
            }
        }

        /// <summary>
        /// 视频单击切换焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            UCVideo videoWindow = sender as UCVideo;
            if (videoWindow != null)
            {
                int intWindIndex = Convert.ToInt32(videoWindow.Tag);
                //this.cmbYTChannel.Text = (intWindIndex + 1).ToString();
                //this.cmbBZChannel.Text = (intWindIndex + 1).ToString();
                ActivateVideo(listVideo, intWindIndex);
            }
        }

        public override void SelectChannel(int channelno)
        {
            int intWindIndex = channelno % StartDChan;
            ActivateVideo(listVideo, intWindIndex);
        }

        #region 鹰眼的鼠标事件
        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (EagleVideo != null)
            {
                m_bMoved = false; 
            }        
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            UCVideo video = sender as UCVideo;
            int tag = Convert.ToInt32(video.Tag);
            int channelno = video.ChannelNo;
            if (dicChannelType.ContainsKey(channelno))
            {
                if (dicChannelType[channelno] == EnumChannelType.EagleChannel)
                {
                    EagleVideo = video;
                }
                else
                {
                    EagleVideo = null;
                    return;
                }
            }
            m_strPrePoint.x = (e.X - video.Pic.Left);
            m_strPrePoint.y = (e.Y - video.Pic.Top);
            m_bMoved = true;
            video.Focus();
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (!m_bMoved)
            {
                return;
            }
            int tag = Convert.ToInt32(EagleVideo.Tag);
            float fpi = 3.1415926f;
            int iWndWidth = EagleVideo.PicWidth; //当前窗口宽度
            int iWndHeight = EagleVideo.PicHeight; //当前窗口高度
            PlayCtrl.DRAWPOINT strPoint = new PlayCtrl.DRAWPOINT();
            strPoint.x = e.X * 1.0f / iWndWidth;
            strPoint.y = e.Y * 1.0f / iWndHeight;
            strPoint.x = ((e.X - m_strPrePoint.x) * 1.0f) / iWndWidth;
            strPoint.y = ((e.Y - m_strPrePoint.y) * 1.0f) / iWndHeight;
            PlayCtrl.PLAYM4SRTRANSFERPARAM stParam = new PlayCtrl.PLAYM4SRTRANSFERPARAM();
            PlayCtrl.PLAYM4SRTRANSFERELEMENT stSubParam = new PlayCtrl.PLAYM4SRTRANSFERELEMENT();
            stSubParam.fAxisX = strPoint.x * 8 * fpi;
            stSubParam.fAxisY = strPoint.y * 4 * fpi;
            stSubParam.fAxisZ = 0.0f;
            stSubParam.fValue = 0.0f;
            IntPtr ptrSRTransformElement = Marshal.AllocHGlobal(Marshal.SizeOf(stSubParam));
            Marshal.StructureToPtr(stSubParam, ptrSRTransformElement, false);
            stParam.pTransformElement = ptrSRTransformElement;
            PlayCtrl.PlayM4_SR_Rotate(m_Channel[tag], ref stParam);
            m_strPrePoint.x = e.X;
            m_strPrePoint.y = e.Y;
            Marshal.FreeHGlobal(ptrSRTransformElement);
        }

        private void SR_ZOOM(float fStep,int tag)
        {
            if (m_Channel[tag] < 0)
            {
                return;
            }
            PlayCtrl.PLAYM4SRTRANSFERPARAM stParam = new PlayCtrl.PLAYM4SRTRANSFERPARAM();
            PlayCtrl.PLAYM4SRTRANSFERELEMENT stSubParam = new PlayCtrl.PLAYM4SRTRANSFERELEMENT();
            stSubParam.fAxisX = 0.0f;
            stSubParam.fAxisY = 0.0f;
            stSubParam.fAxisZ = 0.0f;
            stSubParam.fValue = fStep;
            IntPtr ptrSRTransformElement = Marshal.AllocHGlobal(Marshal.SizeOf(stSubParam));
            Marshal.StructureToPtr(stSubParam, ptrSRTransformElement, false);
            stParam.pTransformElement = ptrSRTransformElement;
            PlayCtrl.PlayM4_SR_Rotate(m_Channel[tag], ref stParam);
            Marshal.FreeHGlobal(ptrSRTransformElement);
        }

        /// <summary>
        /// 鹰眼的滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseWheel(object sender, MouseEventArgs e)
        {
            if (EagleVideo != null)
            {
                int tag = Convert.ToInt32(EagleVideo.Tag);
                float fStep = PlayCtrl.FEC_ZOOM_STEP; //根据缩放--步长可能为负数
                if (e.Delta < 0)
                {//滚轮向下
                    fStep = PlayCtrl.FEC_ZOOM_STEP;
                }
                else
                {//滚轮向上
                    fStep = (-1) * fStep;
                }
                SR_ZOOM(fStep,tag);
            }
        }
        #endregion

        /// <summary>
        /// 捕捉当前画面
        /// </summary>
        public void CapturePic(int channelno)
        {
            if (channelno > 0)
            {
                //图片保存路径和文件名 the path and file name to save
                string sJpegPicFileName = System.AppDomain.CurrentDomain.BaseDirectory + "Picture\\";
                if (!Directory.Exists(sJpegPicFileName))
                {
                    Directory.CreateDirectory(sJpegPicFileName); //新建文件夹  
                }
                sJpegPicFileName += DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                int lChannel = channelno; //通道号 Channel number
                CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
                lpJpegPara.wPicQuality = 0; //图像质量 Image quality
                lpJpegPara.wPicSize = 0x12; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档

                //JPEG抓图 Capture a JPEG picture
                if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, ref lpJpegPara, sJpegPicFileName))
                {
                    uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    MsgBox.ShowWarn("抓图失败,错误代码" + iLastErr.ToString());
                    return;
                }
                else
                {
                    MsgBox.ShowInfo("抓图成功！");
                }
            }
        }        

        /// <summary>
        /// 视频双击放大缩小视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UCVideo video = sender as UCVideo;
            if (video != null)
            {
                if (video.VideoHandle >= 0)
                {
                    if (video.Width < this.pnlVideo.Width)
                    {
                        video.Width = this.pnlVideo.Width;
                        video.Height = this.pnlVideo.Height;
                        video.Location = new Point(0, 0);
                        int channel = Convert.ToInt32(video.Tag);
                        //this.cmbYTChannel.Text = (channel + 1).ToString();
                        //this.cmbBZChannel.Text = (channel + 1).ToString();
                        foreach (Control ctrl in this.pnlVideo.Controls)
                        {
                            if (ctrl.GetType() == typeof(UCVideo))
                            {
                                if (video.Tag != ctrl.Tag)
                                {
                                    ctrl.Visible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        JudgeVideo(this.pnlVideo,CameraCount);
                    }
                }
            }
        }
        #endregion

        #region 鹰眼模式切换
        private void video_e_EagleModeChange(UCVideo video)
        {
            int tag = Convert.ToInt32(video.Tag);
            if (m_Channel[tag] >= 0)
            {
                PlayCtrl.PLAYM4SRMODELMODE struParam = new PlayCtrl.PLAYM4SRMODELMODE();
                struParam.emTextureMode = (uint)PlayCtrl.PLAYM4SRTEXTUREMODE.PLAYM4_TEXTURE_DOUBLE;
                struParam.ulDisplayType = PlayCtrl.PLAYM4_MODEL_SOLID;
                struParam.nTransformMode = 0;
                struParam.emModelType = (uint)PlayCtrl.PLAYM4SRMODELTYPE.PLAYM4_MODELTYPE_EAGLE_EYE;
                struParam.bModelMode = video.IsQJVideo; // true是碗装，false是块状
                int dwSize = Marshal.SizeOf(struParam);
                IntPtr ptrDeviceCfg = Marshal.AllocHGlobal(dwSize);
                Marshal.StructureToPtr(struParam, ptrDeviceCfg, false);
                if (!PlayCtrl.PlayM4_SR_SetConfig(m_Channel[tag], PlayCtrl.CFG_DISPLAY_MODEL_MODE, ptrDeviceCfg))
                {
                    Marshal.FreeHGlobal(ptrDeviceCfg);
                }
            }

            //if (tag >= 0)
            //{
            //    //先关闭预览
            //    if (m_Channel[tag] != -1)
            //    {
            //        if (!PlayCtrl.PlayM4_FEC_Disable(m_Channel[tag]))
            //        {
            //            iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[tag]);
            //        }
            //        if (!PlayCtrl.PlayM4_Stop(m_Channel[tag]))
            //        {
            //            iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[tag]);
            //        }
            //        if (!PlayCtrl.PlayM4_CloseStream(m_Channel[tag]))
            //        {
            //            iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[tag]);
            //        }
            //        if (!PlayCtrl.PlayM4_CloseFile(m_Channel[tag]))
            //        {
            //            iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[tag]);
            //        }
            //        if (!PlayCtrl.PlayM4_FreePort(m_Channel[tag]))
            //        {
            //            iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[tag]);
            //        }
            //        m_Channel[tag] = -1;
            //    }
            //    if (m_lRealHandle[tag] >= 0)
            //    {
            //        bool isok= CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle[tag]);
            //        m_lRealHandle[tag] = -1;
            //        m_RealDataCallBack[tag] = null;
            //    }
            //    IsEagleQJ = video.IsQJVideo;
            //    //重新打开预览
            //    int channel = tag + StartDChan;
            //    CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
            //    //lpPreviewInfo.hPlayWnd = RealPlayWnd.Pic.Handle;//预览窗口 利用回调播放库显示视频
            //    lpPreviewInfo.lChannel = channel;//预te览的设备通道
            //    //if(QJChannel!=null && QJChannel.Contains((channel%StartDChan+1).ToString()))
            //    //    lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            //    //else
            //    //    lpPreviewInfo.dwStreamType = 1;
            //    lpPreviewInfo.dwStreamType = 0;  //初始预览用子码流  放大的时候用主码流
            //    lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
            //    lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
            //    video.HikPreViewInfo = lpPreviewInfo;
            //    video.LUserID = m_lUserID;

            //    m_RealDataCallBack[tag] = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
            //    IsCallBack = true;
            //    IntPtr pUser = new IntPtr();//用户数据

            //    //打开预览 Start live view 
            //    m_lRealHandle[tag] = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, m_RealDataCallBack[tag], pUser);
            //    video.VideoHandle = m_lRealHandle[tag];

            //    //if (!dicPreviewInfo.ContainsKey(tag))
            //    //    dicPreviewInfo.Add(tag, lpPreviewInfo);  //预览视频信息
            //    if (m_lRealHandle[tag] < 0)
            //    {//预览失败
            //    }
            //    else
            //    {//预览成功
            //    }
            //}
        }
        #endregion

        #region NVR视频处理
        private void InitNVR()
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("NET_DVR_Init error!");
                return;
            }
        }

        private void PreviewVideo()
        {
            try
            {
                int channel = 0;
                //IntPtr picHandle = new IntPtr();
                //picHandle = IntPtr.Zero;
                m_StreamPlayMode = 0;
                if (dicRealHandlerChannel != null && dicRealHandlerChannel.Count > 0)
                    dicRealHandlerChannel.Clear();
                foreach (Control ctrl in this.pnlVideo.Controls)
                {
                    //if (Convert.ToInt16(ctrl.Tag) == 0)
                    {
                        if (ctrl.GetType() == typeof(UCVideo))
                        {
                            int tag = Convert.ToInt32(ctrl.Tag);
                            //if (tag > 0)
                            //    return;
                            //if (tag == 0)
                            {
                                m_Channel[tag] = -1;
                                if (m_lRealHandle[tag] < 0)
                                {
                                    channel = tag + StartDChan;
                                    UCVideo RealPlayWnd = ctrl as UCVideo;
                                    CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                                    //lpPreviewInfo.hPlayWnd = RealPlayWnd.Pic.Handle;//预览窗口 利用回调播放库显示视频
                                    lpPreviewInfo.lChannel = channel;//预te览的设备通道
                                    //if(QJChannel!=null && QJChannel.Contains((channel%StartDChan+1).ToString()))
                                    //    lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                                    //else
                                    //    lpPreviewInfo.dwStreamType = 1;
                                    lpPreviewInfo.hPlayWnd = IntPtr.Zero;
                                    lpPreviewInfo.DisplayBufNum = 6;
                                    lpPreviewInfo.dwStreamType = 0;  //初始预览用子码流  放大的时候用主码流
                                    lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                                    lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                                    RealPlayWnd.HikPreViewInfo = lpPreviewInfo;
                                    RealPlayWnd.LUserID = m_lUserID;

                                    m_RealDataCallBack[tag] = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                                    IsCallBack = true;
                                    IntPtr pUser = new IntPtr();//用户数据

                                    //打开预览 Start live view 
                                    m_lRealHandle[tag] = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, m_RealDataCallBack[tag], pUser);
                                    if (!dicRealHandlerChannel.ContainsKey(m_lRealHandle[tag]))
                                    {
                                        dicRealHandlerChannel.Add(m_lRealHandle[tag], channel);
                                    }
                                    else
                                    {
                                        dicRealHandlerChannel[m_lRealHandle[tag]] = channel;
                                    }
                                    RealPlayWnd.VideoHandle = m_lRealHandle[tag];
                                    
                                    //if (!dicPreviewInfo.ContainsKey(tag))
                                    //    dicPreviewInfo.Add(tag, lpPreviewInfo);  //预览视频信息
                                    if (m_lRealHandle[tag] < 0)
                                    {//预览失败
                                    }
                                    else
                                    {//预览成功
                                        IsPreviewing = true;  //正在预览
                                    }
                                }
                                else
                                {
                                }
                            }
                        }
                    }
                }
                if (IsPreviewing == true)
                {
                    this.pnlPreview.BackgroundImage = global::ToolsManage.Properties.Resources.停止;
                }
            }
            catch (Exception ex)
            {
                TXTWriteHelper.WriteException("UCHikVideo-PreviewVideo-"+ex.ToString());
            }
        }

        private void RealDataCallBack(Int32 lRealHandler, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            try
            {
                int lRealHandle = 0;
                if (dicRealHandlerChannel.ContainsKey(lRealHandler))
                    lRealHandle = dicRealHandlerChannel[lRealHandler] % StartDChan;
                else
                    return;
                //int lRealHandle = lRealHandler % CameraCount;
                uint dwErr = 0;
                switch (dwDataType)
                {
                    case CHCNetSDK.NET_DVR_SYSHEAD:
                        if (m_Channel[lRealHandle] != -1)
                        {
                            return;
                        }
                        if (m_Channel[lRealHandle] == -1)
                        {
                            if (!PlayCtrl.PlayM4_GetPort(ref m_Channel[lRealHandle]))
                            {
                                //dwErr = PlayCtrl.PlayM4_GetLastError(m_Channel);
                            }
                            //m_Channel[lRealHandle] = lRealHandle;
                            uint version= PlayCtrl.PlayM4_GetSdkVersion();
                        }
                        if (dwBufSize > 0)
                        {
                            if (!PlayCtrl.PlayM4_SetStreamOpenMode(m_Channel[lRealHandle], m_StreamPlayMode))  //设置实时流播放模式
                            {
                                dwErr = PlayCtrl.PlayM4_GetLastError(m_Channel[lRealHandle]);
                                break;
                            }

                            if (!PlayCtrl.PlayM4_OpenStream(m_Channel[lRealHandle], ref pBuffer, dwBufSize, 15 * 1024 * 1024)) //打开流接口
                            {
                                dwErr = PlayCtrl.PlayM4_GetLastError(m_Channel[lRealHandle]);
                                break;
                            }

                            if (!PlayCtrl.PlayM4_SetDisplayBuf(m_Channel[lRealHandle], 6))
                            {
                                dwErr = PlayCtrl.PlayM4_GetLastError(m_Channel[lRealHandle]);
                                break;
                            }

                            //if (!PlayCtrl.PlayM4_SetDecodeEngineEx(m_Channel[lRealHandle], PlayCtrl.HARD_DECODE_ENGINE))
                            //{
                            //    dwErr = PlayCtrl.PlayM4_GetLastError(m_Channel[lRealHandle]);
                            //    break;
                            //}
                            if (CheckEagleEyeStream(dicRealHandlerChannel[lRealHandler]))//小鹰眼码流的特殊处理
                            {
                                if (!PlayCtrl.PlayM4_SetDecodeEngineEx(m_Channel[lRealHandle], PlayCtrl.HARD_DECODE_ENGINE))
                                {
                                    break;
                                }
                                //UCVideo video= listVideo.Find(p => Convert.ToInt32(p.Tag) == lRealHandle);                               
                                PlayCtrl.PLAYM4SRMODELMODE struParam = new PlayCtrl.PLAYM4SRMODELMODE();
                                struParam.emTextureMode = (uint)PlayCtrl.PLAYM4SRTEXTUREMODE.PLAYM4_TEXTURE_DOUBLE;
                                struParam.ulDisplayType = PlayCtrl.PLAYM4_MODEL_SOLID;
                                struParam.nTransformMode = 0;
                                struParam.emModelType = (uint)PlayCtrl.PLAYM4SRMODELTYPE.PLAYM4_MODELTYPE_EAGLE_EYE;
                                struParam.bModelMode = true; // true是碗装，false是块状
                                int dwSize = Marshal.SizeOf(struParam);
                                IntPtr ptrDeviceCfg = Marshal.AllocHGlobal(dwSize);
                                Marshal.StructureToPtr(struParam, ptrDeviceCfg, false);
                                if (!PlayCtrl.PlayM4_SR_SetConfig(m_Channel[lRealHandle], PlayCtrl.CFG_DISPLAY_MODEL_MODE, ptrDeviceCfg))
                                {
                                    Marshal.FreeHGlobal(ptrDeviceCfg);
                                    break;
                                }
                                Marshal.FreeHGlobal(ptrDeviceCfg);
                            }

                            if (!PlayCtrl.PlayM4_Play(m_Channel[lRealHandle], m_ptrRealHandle[lRealHandle])) //播放开始
                            {
                                break;
                            }
                        }
                        break;
                    case CHCNetSDK.NET_DVR_STREAMDATA:
                        if (dwBufSize > 0 && m_Channel[lRealHandle] != -1)
                        {
                            for (int i = 0; i < 4000; i++)
                            {
                                if (!PlayCtrl.PlayM4_InputData(m_Channel[lRealHandle], ref pBuffer, dwBufSize))
                                {
                                    dwErr = PlayCtrl.PlayM4_GetLastError(m_Channel[lRealHandle]);
                                    if (dwErr == PlayCtrl.PLAYM4_BUF_OVER)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    return;
                                }

                            }
                        }
                        break;
                }                
            }
            catch (Exception ex)
            { }
        }

        private bool m_bJpegCapture=false;
        private void RemoteDisplayCBFun(int nPort, IntPtr pBuf, int nSize, int nWidth, int nHeight, int nStamp, int nType, int nReserved)
        {
            try
            {
                if (!m_bJpegCapture)
                {
                    return;
                }
                else
                {
                    uint nLastErr = 100;
                    // save picture as you want
                    if (!PlayCtrl.PlayM4_ConvertToJpegFile(pBuf, nSize, nWidth, nHeight, nType, "C:/Capture.jpg"))
                    {
                        nLastErr = PlayCtrl.PlayM4_GetLastError(nPort);  //m_Channel
                    }
                    else
                    {
                    }
                }

                m_bJpegCapture = false;
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 关闭视频
        /// </summary>
        public override void CloseAllVideo(bool IsExit)
        {
            try
            {
                for (int i = 0; i < m_lRealHandle.Length; i++)
                {                    
                    if (IsCallBack)
                    {
                        if (m_Channel[i] != -1)
                        {
                            //if (!PlayCtrl.PlayM4_FEC_Disable(m_Channel[i]))
                            //{
                            //    iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[i]);
                            //}
                            if (!PlayCtrl.PlayM4_Stop(m_Channel[i]))
                            {
                                iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[i]);
                            }
                            if (!PlayCtrl.PlayM4_CloseStream(m_Channel[i]))
                            {
                                iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[i]);
                            }
                            //if (!PlayCtrl.PlayM4_CloseFile(m_Channel[i]))
                            //{
                            //    iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[i]);
                            //}
                            if (!PlayCtrl.PlayM4_FreePort(m_Channel[i]))
                            {
                                iLastErr = PlayCtrl.PlayM4_GetLastError(m_Channel[i]);
                            }
                            m_Channel[i] = -1;
                        }
                    }
                    if (m_lRealHandle[i] >= 0)
                    {
                        bool IsStopReal= CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle[i]);
                        m_lRealHandle[i] = -1;
                    }
                    IsPreviewing = false;
                }
                IsPreviewing = false;
                this.pnlPreview.BackgroundImage = global::ToolsManage.Properties.Resources.播放;
                //关闭当前回放视频
                if (m_lPlayBackHandle != -1)
                {
                    CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayBackHandle);
                    m_lPlayBackHandle = -1;
                }
                if (m_PlayBackPort != -1)
                {
                    PlayCtrl.PlayM4_Stop(m_PlayBackPort);
                    if (PlayCtrl.PlayM4_CloseStream(m_PlayBackPort))
                    {
                        PlayCtrl.PlayM4_FreePort(m_PlayBackPort);
                        m_PlayBackPort = -1;
                    }
                }

                //if (m_PlayBackRet >= 0)
                //{
                //    CHCNetSDK.NET_DVR_StopPlayBack(m_PlayBackRet);
                //    m_PlayBackRet = -1;
                //}
                bool IsOk = false;
                if (IsExit)
                {
                    if (m_lUserID >= 0)
                    {
                        //IsOk = CHCNetSDK.NET_DVR_Logout_V30(m_lUserID);
                        IsOk = CHCNetSDK.NET_DVR_Logout(m_lUserID);
                    }
                    if (m_bInitSDK == true)
                    {
                        IsOk = CHCNetSDK.NET_DVR_Cleanup();
                    }
                    if (!IsOk)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                TXTWriteHelper.WriteException("StopHikVideo-" + ex.ToString());
            }
        }

        #endregion

        private void pnlVideo_SizeChanged(object sender, EventArgs e)
        {
            JudgeVideo(this.pnlVideo, CameraCount);
        }

        /// <summary>
        /// 海康云台操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="state"></param>
        public override void PTZControl(object sender, int state, int channelno)
        {
            try
            {
                if (CurVideo == null)
                {
                    MsgBox.ShowWarn("请选择需要控制的摄像头！");
                    return;
                }
                uint cmdid = 21;
                ButtonX btn = sender as ButtonX;
                switch (btn.Name)
                {
                    case "btnUP":
                        cmdid = CHCNetSDK.TILT_UP;
                        break;
                    case "btnDown":
                        cmdid = CHCNetSDK.TILT_DOWN;
                        break;
                    case "btnLeft":
                        cmdid = CHCNetSDK.PAN_LEFT;
                        break;
                    case "btnRight":
                        cmdid = CHCNetSDK.PAN_RIGHT;
                        break;
                    case "btnLeftUp":
                        cmdid = CHCNetSDK.UP_LEFT;
                        break;
                    case "btnRigthUp":
                        cmdid = CHCNetSDK.UP_RIGHT;
                        break;
                    case "btnLeftDown":
                        cmdid = CHCNetSDK.DOWN_LEFT;
                        break;
                    case "btnRightDown":
                        cmdid = CHCNetSDK.DOWN_RIGHT;
                        break;
                    case "btnFDFocus":
                        cmdid = CHCNetSDK.ZOOM_IN;
                        break;
                    case "btnSXFocus":
                        cmdid = CHCNetSDK.ZOOM_OUT;
                        break;
                    default:
                        break;
                }
                PtzOperate(cmdid, (uint)state);
            }
            catch (Exception ex)
            { }
        }

        private bool PtzOperate(UInt32 dwPtzCommand, UInt32 dwStop)
        {
            try
            {
                int channelno = CurVideo.ChannelNo;
                if (CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, channelno, dwPtzCommand, dwStop))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //private void listViewFileList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    m_SelectItem = e.Item;
        //    if (m_SelectItem == null)
        //    {
        //        return;
        //    }
        //    buttonPlay.Image = imageListPlay.Images[0];
        //    this.EnableControl(buttonPlay, true);
        //}

        private void btnPreview_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlPreview_Click(object sender, EventArgs e)
        {
            if (IsPreviewing == false)
            {
                CloseAllVideo(false);
                PreviewVideo();//预览视频
                this.pnlPreview.BackgroundImage = global::ToolsManage.Properties.Resources.停止;
            }
            else
            {
                CloseAllVideo(false);
            }
        }

        public override void CapturePic()
        {
            try
            {
                if (CurVideo!=null)
                {
                    int lChannel = CurVideo.ChannelNo;  //通道号
                    //图片保存路径和文件名 the path and file name to save
                    string sJpegPicFileName = System.AppDomain.CurrentDomain.BaseDirectory + "Picture\\";
                    if (!Directory.Exists(sJpegPicFileName))
                    {
                        Directory.CreateDirectory(sJpegPicFileName); //新建文件夹  
                    }
                    sJpegPicFileName += DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                    CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
                    lpJpegPara.wPicQuality = 0; //图像质量 Image quality
                    lpJpegPara.wPicSize = 0x12; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档

                    //JPEG抓图 Capture a JPEG picture
                    if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, ref lpJpegPara, sJpegPicFileName))
                    {
                        iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                        MsgBox.ShowWarn("通道" + "抓图失败,错误代码" + iLastErr.ToString());
                        return;
                    }
                    else
                    {
                        MsgBox.ShowInfo("通道"  + "抓图成功！");
                    }
                }
                else
                {
                    MsgBox.ShowWarn("请选择需要抓图的通道号！");
                }
            }
            catch (Exception ex)
            { }
            return;
        }     
    }
}

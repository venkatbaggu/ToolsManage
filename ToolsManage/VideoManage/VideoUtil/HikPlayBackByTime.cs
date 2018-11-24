using Common;
using DCMSystem.UCControl;
using HikVideo;
using HikVideo.HikVision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolsManage.VideoManage;

namespace DAMSystem.UCControl.VideoUtil
{
    public partial class UCHikVideo : UCBaseVideo
    {
        private const int Play = 0;
        private const int Pause = 1;
        private const int Stop = 2;
        private uint m_iChanPlayBack = 0;
        private Int32 m_lPlayBackHandle = -1;
        //private Int32 m_lPlayHandle = -1;
        private IntPtr g_hPlayBackWnd = IntPtr.Zero;
        private bool m_bEagleMoved = false;
        private bool m_bSound = false;
        private bool m_bTimeSave = false;
        private bool m_bPause = false;
        private bool m_bScrool = false;
        private Int32[] m_lRemoteHandle;
        private CHCNetSDK.NET_DVR_FILECOND_V40 m_struFileCond = new CHCNetSDK.NET_DVR_FILECOND_V40();
        private CHCNetSDK.REMOTECONFIGCALLBACK g_fGetFigureDataCallBack = null;
        private CHCNetSDK.PLAYDATACALLBACK_V40 g_fPlayDataCallBack_V40 = null;
        private DateTime dtStart, dtEnd;
        private bool m_bIsEagle = false;
        private int m_PlayBackPort = -1;
        UCVideo EaglePalyBackVideo = null;
        private PlayCtrl.DRAWPOINT m_strPlayBackPoint = new PlayCtrl.DRAWPOINT();
        private List<PlayBackFileInfo> ListPlayBackFileTime = new List<PlayBackFileInfo>();
        private int PlayBackTotalSecond = 0;  //回放总秒数
        private System.Timers.Timer TimerPlayBack = null;  //回放视频定时器
        private bool IsPlayBack = false;
        private uint cursecond = 0;
        private int trackBarPos = 0;
        private DateTime OldStart;  //原始的开始时间
        private bool IsSelTime=false;

        private void InitPlayBackByTime()
        {
            InitTimer();
            m_struFileCond.sCardNumber = new byte[CHCNetSDK.CARDNUM_LEN_OUT];
            m_struFileCond.byWorkingDeviceGUID = new byte[CHCNetSDK.GUID_LEN];
            initUI();
            ucVideoPlayBack.Pic.ContextMenuStrip = null;
            if (ucVideoPlayBack.MouseDown == null)
                ucVideoPlayBack.MouseDown += PlayBackMouseDown;
            if (ucVideoPlayBack.MouseUp == null)
                ucVideoPlayBack.MouseUp += PlayBackMouseUp;
            if (ucVideoPlayBack.MouseMove == null)
                ucVideoPlayBack.MouseMove += PlayBackMouseMove;
            if (ucVideoPlayBack.MouseWheel == null)
                ucVideoPlayBack.MouseWheel += PlayBackMouseWheel;
            ucVideoPlayBack.e_EagleModeChange += PlayBack_e_EagleModeChange;
            //initDlg();
        }

        private void InitTimer()
        {
            TimerPlayBack = new System.Timers.Timer();
            TimerPlayBack.Interval = 1000;
            TimerPlayBack.Elapsed += new System.Timers.ElapsedEventHandler(PlayBackVideo);
            //TimerPlayBack.Enabled = true;
            TimerPlayBack.AutoReset = true;
        }

        private void StopTimer()
        {
            TimerPlayBack.Enabled = false;
        }

        private void initUI()
        {
            m_btnPlay.Image = m_imgPlay.Images[0];
            m_btnPlay.Enabled = true;
            m_btnStop.Image = m_imgStop.Images[0];
            m_btnStop.Enabled = false;
            m_btnFast.Image = m_imgFast.Images[0];
            m_btnFast.Enabled = false;
            m_btnSlow.Image = m_imgSlow.Images[0];
            m_btnSlow.Enabled = false;
            m_btnSound.Image = m_imgSound.Images[0];
            m_btnSound.Enabled = false;
            m_btnCapture.Enabled = false;
        }

        private void SetPlayState()
        {
            m_btnPlay.Image = m_imgPlay.Images[2];
            m_btnPlay.Enabled = true;
            m_btnStop.Image = m_imgStop.Images[1];
            m_btnStop.Enabled = true;
            m_btnFast.Image = m_imgFast.Images[1];
            m_btnFast.Enabled = true;
            m_btnSlow.Image = m_imgSlow.Images[1];
            m_btnSlow.Enabled = true;

            if (m_bSound)
            {
                m_btnSound.Image = m_imgSound.Images[0];
            }
            else
            {
                m_btnSound.Image = m_imgSound.Images[1];
            }
            m_btnSound.Enabled = true;
            m_btnCapture.Enabled = true;

        }

        private void SetStopState()
        {
            m_btnPlay.Image = m_imgPlay.Images[0];
            m_btnPlay.Enabled = true;
            m_btnStop.Image = m_imgStop.Images[0];
            m_btnStop.Enabled = false;
            m_btnFast.Image = m_imgFast.Images[0];
            m_btnFast.Enabled = false;
            m_btnSlow.Image = m_imgSlow.Images[0];
            m_btnSlow.Enabled = false;
            m_btnSound.Image = m_imgSound.Images[0];
            m_btnSound.Enabled = false;
            m_btnCapture.Enabled = false;
        }

        private void SetPauseState()
        {
            m_btnPlay.Image = imageListPlay.Images[1];
            m_btnPlay.Enabled = true;
        }

        private void ChangePlayState(int iStatus)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ChangePlayState(iStatus)));
            }
            else
            {
                switch (iStatus)
                {
                    case Play:
                        SetPlayState();
                        break;
                    case Pause:
                        SetPauseState();
                        break;
                    case Stop:
                        SetStopState();
                        break;
                    default:
                        break;
                }
            }
        }

        private void PlayBackVideo(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (IsPlayBack == false)
                {
                    IsPlayBack = true;
                    uint playtime = PlayCtrl.PlayM4_GetPlayedTime(m_PlayBackPort);
                    if (playtime == 0xffffffff)
                    {//获取时间失败 
                        return;
                    }
                    cursecond += playtime;
                    double dprogress = Math.Round((1.0 * cursecond / PlayBackTotalSecond), 2);
                    int iprogress = (int)(dprogress * 100);
                    if (iprogress > 100)
                    {
                        //StopPlay();
                        StopPlayBack();
                    }
                    else
                    {
                        trackBarPlaybackProgress.Value = iprogress;
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                IsPlayBack = false;
            }
        }

        private void PausePlayBack()
        {
            //先暂停网络库
            uint temp = 0;
            bool IsNetPause = CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayBackHandle, CHCNetSDK.NET_DVR_PLAYPAUSE, 0, ref temp);
            //再暂停播放库
            bool IsPlayCtrlPause= PlayCtrl.PlayM4_Pause(m_PlayBackPort, 1);
        }

        private void ResumePalyBack()
        {
            //先暂停网络库
            uint temp = 0;
            bool IsNetPause = CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayBackHandle, CHCNetSDK.NET_DVR_PLAYRESTART, 0, ref temp);
            //再暂停播放库
            bool IsPlayCtrlPause = PlayCtrl.PlayM4_Pause(m_PlayBackPort, 0);
        }

        private void StopPlayBack()
        {
            //关闭当前回放视频
            if (m_lPlayBackHandle != -1)
            {
                TimerPlayBack.Enabled = false;
                SetStopState();
                bool IsStop = CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayBackHandle);
                m_lPlayBackHandle = -1;
            }
            if (m_PlayBackPort >= 0)
            {
                bool isstop= PlayCtrl.PlayM4_Stop(m_PlayBackPort);
                //关闭回放流
                if (PlayCtrl.PlayM4_CloseStream(m_PlayBackPort))
                {
                    PlayCtrl.PlayM4_FreePort(m_PlayBackPort);
                    m_PlayBackPort = -1;
                }
                else
                {
                    m_PlayBackPort = -1;
                }
            }
        }

        private void SearchPlayBackFile()
        {
            int m_lChannel = CurVideo.ChannelNo;
            m_struFileCond.lChannel = m_lChannel;
            m_struFileCond.dwFileType = 0xff;
            m_struFileCond.struStartTime.dwYear = dtStart.Year;
            m_struFileCond.struStartTime.dwMonth = dtStart.Month;
            m_struFileCond.struStartTime.dwDay = dtStart.Day;
            m_struFileCond.struStartTime.dwHour = dtStart.Hour;
            m_struFileCond.struStartTime.dwMinute = dtStart.Minute;
            m_struFileCond.struStartTime.dwSecond = dtStart.Second;

            m_struFileCond.struStopTime.dwYear = dtEnd.Year;
            m_struFileCond.struStopTime.dwMonth = dtEnd.Month;
            m_struFileCond.struStopTime.dwDay = dtEnd.Day;
            m_struFileCond.struStopTime.dwHour = dtEnd.Hour;
            m_struFileCond.struStopTime.dwMinute = dtEnd.Minute;
            m_struFileCond.struStopTime.dwSecond = dtEnd.Second;

            m_struFileCond.dwUseCardNo = 0;
            m_struFileCond.dwVolumeNum = 0;
            m_struFileCond.byFindType = 0;
            m_struFileCond.byStreamType = 0;

            m_struFileCond.byAudioFile = 0;
            m_struFileCond.dwIsLocked = 0xff;
            for (int i = 0; i < m_struFileCond.byWorkingDeviceGUID.Length; i++)
            {
                m_struFileCond.byWorkingDeviceGUID[i] = 0;
            }
            byte[] byTemp = new byte[CHCNetSDK.GUID_LEN];
            byTemp = System.Text.Encoding.UTF8.GetBytes("0000000000000000");
            byTemp.CopyTo(m_struFileCond.byWorkingDeviceGUID, 0);

            m_struFileCond.dwUseCardNo = 0;
            byte[] byTemp2 = new byte[CHCNetSDK.CARDNUM_LEN_OUT];
            byTemp2 = System.Text.Encoding.UTF8.GetBytes("");
            byTemp2.CopyTo(m_struFileCond.sCardNumber, 0);
            if (m_struFileCond.dwUseCardNo > 0)
            {
                m_struFileCond.bySpecialFindInfoType = 1;
                m_struFileCond.uSpecialFindInfo.struATMFindInfo.byTransactionType = 0;
                m_struFileCond.uSpecialFindInfo.struATMFindInfo.dwTransationAmount = byte.Parse("");
            }

            int dwSize = Marshal.SizeOf(m_struFileCond);
            int m_lFileHandle = CHCNetSDK.NET_DVR_FindFile_V40(m_lUserID, ref m_struFileCond);
            if (m_lFileHandle < 0)
            {
                string strTemp = string.Format("NET_DVR_FindFile_V40 FAIL, ERROR CODE {0}", CHCNetSDK.NET_DVR_GetLastError());
                return;
            }
            //处理查找到的文件
            CHCNetSDK.NET_DVR_FINDDATA_V40 struFileInfo = new CHCNetSDK.NET_DVR_FINDDATA_V40();
            int lRet=-1;
            string filestarttime="",fileendtime;
            while (true)
            {
                lRet = CHCNetSDK.NET_DVR_FindNextFile_V40(m_lFileHandle, ref struFileInfo);
                if (lRet == CHCNetSDK.NET_DVR_FILE_SUCCESS)
                {                   
                    filestarttime = struFileInfo.struStartTime.dwYear.ToString("D4") + "-" + struFileInfo.struStartTime.dwMonth.ToString("D2") + "-" +
                        struFileInfo.struStartTime.dwDay.ToString("D2") + " " +struFileInfo.struStartTime.dwHour.ToString("D2") + ":"+
                        struFileInfo.struStartTime.dwMinute.ToString("D2") +":" + struFileInfo.struStartTime.dwSecond.ToString("D2");
                    fileendtime = struFileInfo.struStopTime.dwYear.ToString("D4") + "-" + struFileInfo.struStopTime.dwMonth.ToString("D2") + "-" +
                        struFileInfo.struStopTime.dwDay.ToString("D2") + " " + struFileInfo.struStopTime.dwHour.ToString("D2") + ":" +
                        struFileInfo.struStopTime.dwMinute.ToString("D2") + ":" + struFileInfo.struStopTime.dwSecond.ToString("D2");
                    DateTime dtfile = Convert.ToDateTime(filestarttime);
                    DateTime endfile = Convert.ToDateTime(fileendtime);
                    PlayBackFileInfo pbfi = new PlayBackFileInfo() { DtStart=dtfile, DtEnd=endfile  };
                    ListPlayBackFileTime.Add(pbfi);
                }
                else
                {
                    if (lRet == CHCNetSDK.NET_DVR_ISFINDING)
                    {
                        Thread.Sleep(5);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            GetPlayBackTime();
            CHCNetSDK.NET_DVR_FindClose_V30(m_lFileHandle);
        }

        private void GetPlayBackTime()
        {
            if (ListPlayBackFileTime != null && ListPlayBackFileTime.Count > 0)
            {
                if (dtStart < ListPlayBackFileTime[0].DtStart)
                {
                    dtStart = ListPlayBackFileTime[0].DtStart;
                    OldStart = dtStart;
                }
                dtEnd = ListPlayBackFileTime[ListPlayBackFileTime.Count - 1].DtEnd;
            }
            TimeSpan ts = dtEnd - dtStart;
            PlayBackTotalSecond = (int)ts.TotalSeconds;
        }

        #region 鹰眼回放
        /// <summary>
        /// 鹰眼回放
        /// </summary>
        private void EaglePlayBack()
        {
            if (m_lPlayBackHandle != -1)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lPlayBackHandle);
                m_lPlayBackHandle = -1;
            }
            CHCNetSDK.NET_DVR_VOD_PARA struVodPara = new CHCNetSDK.NET_DVR_VOD_PARA();
            struVodPara.Init();
            if (m_iChanIndex > (g_struDeviceInfo[m_iDeviceIndex].iDeviceChanNum - 1) && (g_struDeviceInfo[m_iDeviceIndex].byMirrorChanNum) > 0)
            {
                m_iChanPlayBack = (uint)g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[m_iChanIndex + 1 - g_struDeviceInfo[m_iDeviceIndex].wStartMirrorChanNo].iChannelNO;
            }
            else
            {
                m_iChanPlayBack = (uint)g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[m_iChanIndex].iChannelNO;
            }
            struVodPara.struBeginTime.dwYear = dtStart.Year;
            struVodPara.struBeginTime.dwMonth = dtStart.Month;
            struVodPara.struBeginTime.dwDay = dtStart.Day;
            struVodPara.struBeginTime.dwHour = dtStart.Hour;
            struVodPara.struBeginTime.dwMinute = dtStart.Minute;
            struVodPara.struBeginTime.dwSecond = dtStart.Second;
            struVodPara.struEndTime.dwYear = dtEnd.Year;
            struVodPara.struEndTime.dwMonth = dtEnd.Month;
            struVodPara.struEndTime.dwDay = dtEnd.Day;
            struVodPara.struEndTime.dwHour = dtEnd.Hour;
            struVodPara.struEndTime.dwMinute = dtEnd.Minute;
            struVodPara.struEndTime.dwSecond = dtEnd.Second;
            struVodPara.struIDInfo.dwChannel = m_iChanPlayBack;
            struVodPara.hWnd = IntPtr.Zero;
            struVodPara.struIDInfo.dwSize = (uint)Marshal.SizeOf(struVodPara.struIDInfo);
            struVodPara.dwSize = (uint)Marshal.SizeOf(struVodPara);
            m_lPlayBackHandle = CHCNetSDK.NET_DVR_PlayBackByTime_V40((uint)m_lUserID, ref struVodPara);
            if (m_lPlayBackHandle == -1)
            {
                string strShow = null;
                strShow = string.Format("NET_DVR_PlayBackByTime_V40 ChanNum[" + m_iChanPlayBack + "]");
                MessageBox.Show(strShow);
                return;
            }
            m_StreamPlayMode = 1;
            g_fPlayDataCallBack_V40 = new CHCNetSDK.PLAYDATACALLBACK_V40(PlayDataCallBack_V40);
            if (!CHCNetSDK.NET_DVR_SetPlayDataCallBack_V40(m_lPlayBackHandle, g_fPlayDataCallBack_V40, IntPtr.Zero))
            {
                //m_dwLastError = CHCNetSDK.NET_DVR_GetLastError();
                return;
            }
            uint dwTemp = 0;
            int dwFileOffset = 0;
            if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayBackHandle, CHCNetSDK.NET_DVR_PLAYSTART, (uint)dwFileOffset, ref dwTemp))
            {
                SetPlayState();
                //strShow = string.Format("NET_DVR_PLAYSTART offset{0}", dwFileOffset);
                //g_formList.AddLog(m_iDeviceIndex, CHCNetSDK.OPERATION_SUCC_T, strShow);
            }
            else
            {
                //strShow = string.Format("NET_DVR_PLAYSTART offset{0}", dwFileOffset);
                //g_formList.AddLog(m_iDeviceIndex, CHCNetSDK.OPERATION_FAIL_T, strShow);
                CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayBackHandle);
                m_lPlayBackHandle = -1;
                //g_formList.g_StringLanType(ref strShow, "回放失败!", "NET_DVR_PLAYSTART Failed!");
                //MessageBox.Show(strShow);
                return;
            }
            if (!TimerPlayBack.Enabled)
            {
                TimerPlayBack.Enabled = true;
            }
        }
        #endregion

        public override void PlayBack(DateTime start, DateTime end)
        {
            cursecond = 0;
            if (tcVideo.SelectedTab != tpPlayBack)
                tcVideo.SelectedTab = tpPlayBack;
            //m_PlayBackPort = -1;
            IsSelTime = true;
            dtStart = start;
            OldStart = start;
            dtEnd = end;
            if (CurVideo != null)
            {
                ucVideoPlayBack.CameraType = CurVideo.CameraType;
                SearchPlayBackFile();
                StopPlayBack();
                m_iChanIndex = Convert.ToInt32(CurVideo.Tag);
                //ucVideoPlayBack.CameraType = CurVideo.CameraType;                
                CloseAllVideo(false);
                EaglePlayBack();
            }
        }

        #region 鹰眼的鼠标事件
        private void PlayBackMouseUp(object sender, MouseEventArgs e)
        {
            if (CurVideo.CameraType == 4)
            {
                if (EaglePalyBackVideo != null)
                {
                    m_bEagleMoved = false;
                }
            }
        }

        private void PlayBackMouseDown(object sender, MouseEventArgs e)
        {
            if (CurVideo.CameraType == 4)
            {
                UCVideo video = sender as UCVideo;
                //int tag = Convert.ToInt32(video.Tag);
                EaglePalyBackVideo = video;
                m_strPlayBackPoint.x = (e.X - video.Pic.Left);
                m_strPlayBackPoint.y = (e.Y - video.Pic.Top);
                m_bEagleMoved = true;
                video.Focus();
                video.Pic.Focus();
                //ucVideoPlayBack.Focus();
            }
        }

        private void PlayBackMouseMove(object sender, MouseEventArgs e)
        {
            if (CurVideo.CameraType == 4)
            {
                if (!m_bEagleMoved)
                {
                    return;
                }
                //int tag = Convert.ToInt32(EaglePalyBackVideo.Tag);
                float fpi = 3.1415926f;
                int iWndWidth = EaglePalyBackVideo.PicWidth; //当前窗口宽度
                int iWndHeight = EaglePalyBackVideo.PicHeight; //当前窗口高度
                PlayCtrl.DRAWPOINT strPoint = new PlayCtrl.DRAWPOINT();
                strPoint.x = e.X * 1.0f / iWndWidth;
                strPoint.y = e.Y * 1.0f / iWndHeight;
                strPoint.x = ((e.X - m_strPlayBackPoint.x) * 1.0f) / iWndWidth;
                strPoint.y = ((e.Y - m_strPlayBackPoint.y) * 1.0f) / iWndHeight;
                PlayCtrl.PLAYM4SRTRANSFERPARAM stParam = new PlayCtrl.PLAYM4SRTRANSFERPARAM();
                PlayCtrl.PLAYM4SRTRANSFERELEMENT stSubParam = new PlayCtrl.PLAYM4SRTRANSFERELEMENT();
                stSubParam.fAxisX = strPoint.x * 8 * fpi;
                stSubParam.fAxisY = strPoint.y * 4 * fpi;
                stSubParam.fAxisZ = 0.0f;
                stSubParam.fValue = 0.0f;
                IntPtr ptrSRTransformElement = Marshal.AllocHGlobal(Marshal.SizeOf(stSubParam));
                Marshal.StructureToPtr(stSubParam, ptrSRTransformElement, false);
                stParam.pTransformElement = ptrSRTransformElement;
                bool isRotate = PlayCtrl.PlayM4_SR_Rotate(m_PlayBackPort, ref stParam);
                //uint errorcode= PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
                m_strPlayBackPoint.x = e.X;
                m_strPlayBackPoint.y = e.Y;
                Marshal.FreeHGlobal(ptrSRTransformElement);
            }
        }

        private void SR_ZOOMPalyBack(float fStep, int tag)
        {
            if (m_lPlayBackHandle < 0)
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
            PlayCtrl.PlayM4_SR_Rotate(m_PlayBackPort, ref stParam);
            Marshal.FreeHGlobal(ptrSRTransformElement);
        }

        /// <summary>
        /// 鹰眼的滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayBackMouseWheel(object sender, MouseEventArgs e)
        {
            if (EaglePalyBackVideo != null)
            {
                int tag = Convert.ToInt32(EaglePalyBackVideo.Tag);
                float fStep = PlayCtrl.FEC_ZOOM_STEP; //根据缩放--步长可能为负数
                if (e.Delta < 0)
                {//滚轮向下
                    fStep = PlayCtrl.FEC_ZOOM_STEP;
                }
                else
                {//滚轮向上
                    fStep = (-1) * fStep;
                }
                SR_ZOOMPalyBack(fStep, tag);
            }
        }

        private void PlayBack_e_EagleModeChange(UCVideo video)
        {
            int tag = Convert.ToInt32(video.Tag);
            if (m_PlayBackPort >= 0)
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
                if (!PlayCtrl.PlayM4_SR_SetConfig(m_PlayBackPort, PlayCtrl.CFG_DISPLAY_MODEL_MODE, ptrDeviceCfg))
                {
                    Marshal.FreeHGlobal(ptrDeviceCfg);
                }
            }
        }
        #endregion

        private void PlayDataCallBack_V40(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUserData)
        {
            //if (m_bIsEagle == true)
            {//鹰眼回调
                PlayBackCallBack(lRealHandle, dwDataType, ref pBuffer, dwBufSize, pUserData);
            }
            return;
        }

        private void PlayBackCallBack(Int32 lRealHandler, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            try
            {                
                uint dwErr = 0;
                switch (dwDataType)
                {
                    case CHCNetSDK.NET_DVR_SYSHEAD:
                        if (m_PlayBackPort != -1)
                        {
                            return;
                        }
                        if (m_PlayBackPort == -1)
                        {
                            if (!PlayCtrl.PlayM4_GetPort(ref m_PlayBackPort))
                            {
                                //dwErr = PlayCtrl.PlayM4_GetLastError(m_Channel);
                            }
                            //m_Channel[lRealHandle] = lRealHandle;
                            uint version= PlayCtrl.PlayM4_GetSdkVersion();
                        }
                        if (dwBufSize > 0)
                        {
                            //m_StreamPlayMode = 0;
                            if (!PlayCtrl.PlayM4_SetStreamOpenMode(m_PlayBackPort, m_StreamPlayMode))  //设置实时流播放模式
                            {
                                dwErr = PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
                                break;
                            }

                            if (!PlayCtrl.PlayM4_OpenStream(m_PlayBackPort, ref pBuffer, dwBufSize, 15 * 1024 * 1024)) //打开流接口
                            {
                                dwErr = PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
                                break;
                            }

                            if (!PlayCtrl.PlayM4_SetDisplayBuf(m_PlayBackPort, 6))
                            {
                                dwErr = PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
                                break;
                            }
                            int channel = CurVideo.ChannelNo;
                            if (CheckEagleEyeStream(channel))//小鹰眼码流的特殊处理
                            {
                                if (!PlayCtrl.PlayM4_SetDecodeEngineEx(m_PlayBackPort, PlayCtrl.HARD_DECODE_ENGINE))
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
                                if (!PlayCtrl.PlayM4_SR_SetConfig(m_PlayBackPort, PlayCtrl.CFG_DISPLAY_MODEL_MODE, ptrDeviceCfg))
                                {
                                    Marshal.FreeHGlobal(ptrDeviceCfg);
                                    break;
                                }
                                Marshal.FreeHGlobal(ptrDeviceCfg);
                            }

                            if (!PlayCtrl.PlayM4_Play(m_PlayBackPort, ucVideoPlayBack.Pic.Handle))//播放开始
                            {
                                break;
                            }
                        }
                        break;
                    case CHCNetSDK.NET_DVR_STREAMDATA:
                        if (dwBufSize > 0 && m_PlayBackPort != -1)
                        {
                            for (int i = 0; i < 4000; i++)
                            {
                                if (!PlayCtrl.PlayM4_InputData(m_PlayBackPort, ref pBuffer, dwBufSize))
                                {
                                    dwErr = PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
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

        private void m_btnPlay_Click(object sender, EventArgs e)
        {
            //PlayBack(dtStart, dtEnd);
            if (IsSelTime == false)
            {
                MsgBox.ShowWarn("请选择右边回放时间点击回放！");
                return;
            }
            if (-1 == m_lPlayBackHandle)
            {
                PlayBack(dtStart, dtEnd);
                this.ChangePlayState(Play);
            }
            else
            {//已经播放
                if (m_bPause)
                {//继续播放
                    m_bPause = false;
                    ResumePalyBack();
                    this.ChangePlayState(Play);
                }
                else
                {//暂停
                    m_bPause = true;
                    PausePlayBack();
                    this.ChangePlayState(Pause);
                }
            }

        }

        private void m_btnStop_Click(object sender, EventArgs e)
        {
            //StopPlay();
            StopPlayBack();
        }

        private void m_btnSlow_Click(object sender, EventArgs e)
        {
            //uint dwTemp = 0;
            //if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSLOW, 0, ref dwTemp))
            //{
            //}
            //else
            //{
            //}
            bool IsSlow= PlayCtrl.PlayM4_Slow(m_PlayBackPort);
        }

        private void m_btnFast_Click(object sender, EventArgs e)
        {
            bool IsFast = PlayCtrl.PlayM4_Fast(m_PlayBackPort);
            //uint dwTemp = 0;
            //if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYFAST, 0, ref dwTemp))
            //{
            //}
            //else
            //{
            //}
        }

        private void m_btnCapture_Click(object sender, EventArgs e)
        {
            string sFileName;
            //sFileName = string.Format("C:\\Picture\\PlayBackByTime.bmp");
            //图片保存路径和文件名 the path and file name to save
            string sJpegPicFileName = System.AppDomain.CurrentDomain.BaseDirectory + "PlayBackPicture\\";
            if (!Directory.Exists(sJpegPicFileName))
            {
                Directory.CreateDirectory(sJpegPicFileName); //新建文件夹  
            }
            sJpegPicFileName += DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            if (CHCNetSDK.NET_DVR_PlayBackCaptureFile(m_lPlayBackHandle, sJpegPicFileName))
            {
                return;
            }
            else
            {
            }
        }   

        private void trackBarPlaybackProgress_Scroll(object sender, EventArgs e)
        {
            m_bScrool = true;
        }

        private void trackBarPlaybackProgress_MouseUp(object sender, MouseEventArgs e)
        {
            if (-1 == m_lPlayBackHandle)
            {
                trackBarPlaybackProgress.Value = 0;
                MsgBox.ShowWarn("请先进行视频回放！");
                return;
            }
            int iPos = trackBarPlaybackProgress.Value;
            if (m_bScrool)
            {
                //先停止原来的回放
                StopPlayBack();
                //计算当前需要进行到的时间
                double dprogress = Math.Round((1.0 * iPos / 100), 2);
                int iprogress = (int)(dprogress * 100);
                cursecond = (uint)(PlayBackTotalSecond * dprogress);
                if (trackBarPos <= iPos)
                {
                    trackBarPos = iPos;
                    dtStart = OldStart.AddSeconds(cursecond);
                }
                else
                {
                    trackBarPos = iPos;
                    dtStart = OldStart.AddSeconds(cursecond);
                }
                EaglePlayBack();  //开始回放
            }
        }

        public void ProcessGetFigureDataCallBack(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData)
        {
            if (dwType == (uint)CHCNetSDK.NET_SDK_CALLBACK_TYPE.NET_SDK_CALLBACK_TYPE_DATA)
            {
                CHCNetSDK.NET_DVR_FIGURE_INFO struFigureInfo = new CHCNetSDK.NET_DVR_FIGURE_INFO();

                struFigureInfo = (CHCNetSDK.NET_DVR_FIGURE_INFO)Marshal.PtrToStructure(lpBuffer, typeof(CHCNetSDK.NET_DVR_FIGURE_INFO));

                Random iRan = new Random();
                string strPath = "C:/" + iRan.Next(16000 * Marshal.ReadInt32(pUserData)).ToString() + ".jpg";
                FileStream fs = new FileStream(strPath, FileMode.Create);
                int iLen = (int)struFigureInfo.dwPicLen;
                byte[] by = new byte[iLen];
                Marshal.Copy(struFigureInfo.pPicBuf, by, 0, iLen);
                fs.Write(by, 0, iLen);
                fs.Close();
                Int32 iTest = Marshal.ReadInt32(pUserData);
            }
            else if (dwType == (uint)CHCNetSDK.NET_SDK_CALLBACK_TYPE.NET_SDK_CALLBACK_TYPE_STATUS)
            {
                int dwStatus = Marshal.ReadInt32(lpBuffer);
            }
            return;
        }
    }
}

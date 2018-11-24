using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DCMSystem.UCControl;
using HikVideo;
using System.IO;
using System.Runtime.InteropServices;

namespace DAMSystem.UCControl
{
    public partial class UCPlayBack : UserControl
    {
        //public UCPlayBack()
        //{
        //    InitializeComponent();
        //}

        //private int m_iDeviceIndex = 0;
        //private uint m_iChanShowNum = 0;
        //private int m_lUserID = -1;
        //private Int32 m_iChanIndex = -1;
        //private Int32 m_lPlayBackHandle = -1;
        //private Int32 m_lPlayHandle = -1;
        //private IntPtr g_hPlayBackWnd = IntPtr.Zero;
        //private bool m_bEagleMoved = false;
        //private bool m_bSound = false;
        //private bool m_bTimeSave = false;
        //private bool m_bPause = false;
        //private bool m_bScrool = false;
        //private Int32[] m_lRemoteHandle;
        //private CHCNetSDK.REMOTECONFIGCALLBACK g_fGetFigureDataCallBack = null;
        //private CHCNetSDK.PLAYDATACALLBACK_V40 g_fPlayDataCallBack_V40 = null;
        //private DateTime dtStart, dtEnd;
        //private bool m_bIsEagle = false;
        //private int m_PlayBackPort = -1;
        //UCVideo EaglePalyBackVideo = null;
        //private PlayCtrl.DRAWPOINT m_strPrePoint = new PlayCtrl.DRAWPOINT();
        //private uint m_StreamPlayMode = 0;

        //private void InitPlayBackByTime()
        //{
        //    initUI();
        //    //initDlg();
        //}

        //private void initUI()
        //{
        //    m_btnPlay.Image = m_imgPlay.Images[0];
        //    m_btnPlay.Enabled = true;
        //    m_btnStop.Image = m_imgStop.Images[0];
        //    m_btnStop.Enabled = false;
        //    m_btnFast.Image = m_imgFast.Images[0];
        //    m_btnFast.Enabled = false;
        //    m_btnSlow.Image = m_imgSlow.Images[0];
        //    m_btnSlow.Enabled = false;
        //    m_btnSound.Image = m_imgSound.Images[0];
        //    m_btnSound.Enabled = false;
        //    m_btnCapture.Enabled = false;
        //}

        //private void SetPlayState()
        //{
        //    m_btnPlay.Image = m_imgPlay.Images[2];
        //    m_btnPlay.Enabled = true;
        //    m_btnStop.Image = m_imgStop.Images[1];
        //    m_btnStop.Enabled = true;
        //    m_btnFast.Image = m_imgFast.Images[1];
        //    m_btnFast.Enabled = true;
        //    m_btnSlow.Image = m_imgSlow.Images[1];
        //    m_btnSlow.Enabled = true;

        //    if (m_bSound)
        //    {
        //        m_btnSound.Image = m_imgSound.Images[0];
        //    }
        //    else
        //    {
        //        m_btnSound.Image = m_imgSound.Images[1];
        //    }
        //    m_btnSound.Enabled = true;
        //    m_btnCapture.Enabled = true;

        //}

        //private void SetStopState()
        //{
        //    m_btnPlay.Image = m_imgPlay.Images[0];
        //    m_btnPlay.Enabled = true;
        //    m_btnStop.Image = m_imgStop.Images[0];
        //    m_btnStop.Enabled = false;
        //    m_btnFast.Image = m_imgFast.Images[0];
        //    m_btnFast.Enabled = false;
        //    m_btnSlow.Image = m_imgSlow.Images[0];
        //    m_btnSlow.Enabled = false;
        //    m_btnSound.Image = m_imgSound.Images[0];
        //    m_btnSound.Enabled = false;
        //    m_btnCapture.Enabled = false;
        //}

        ///// <summary>
        ///// 普通摄像头回放
        ///// </summary>
        //private void NormalPlayBack()
        //{
        //    char[] szLan = new char[128];
        //    int i = 0;
        //    CHCNetSDK.NET_DVR_VOD_PARA struVodPara = new CHCNetSDK.NET_DVR_VOD_PARA();
        //    struVodPara.Init();
        //    if (m_lPlayHandle == -1)
        //    {

        //        if (m_iChanIndex > (g_struDeviceInfo[m_iDeviceIndex].iDeviceChanNum - 1) && (g_struDeviceInfo[m_iDeviceIndex].byMirrorChanNum) > 0)
        //        {
        //            m_iChanShowNum = (uint)g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[m_iChanIndex + 1 - g_struDeviceInfo[m_iDeviceIndex].wStartMirrorChanNo].iChannelNO;
        //        }
        //        else
        //        {
        //            m_iChanShowNum = (uint)g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[m_iChanIndex].iChannelNO;
        //        }
        //        struVodPara.struBeginTime.dwYear = dtStart.Year;
        //        struVodPara.struBeginTime.dwMonth = dtStart.Month;
        //        struVodPara.struBeginTime.dwDay = dtStart.Day;
        //        struVodPara.struBeginTime.dwHour = dtStart.Hour;
        //        struVodPara.struBeginTime.dwMinute = dtStart.Minute;
        //        struVodPara.struBeginTime.dwSecond = dtStart.Second;
        //        struVodPara.struEndTime.dwYear = dtEnd.Year;
        //        struVodPara.struEndTime.dwMonth = dtEnd.Month;
        //        struVodPara.struEndTime.dwDay = dtEnd.Day;
        //        struVodPara.struEndTime.dwHour = dtEnd.Hour;
        //        struVodPara.struEndTime.dwMinute = dtEnd.Minute;
        //        struVodPara.struEndTime.dwSecond = dtEnd.Second;
        //        struVodPara.struIDInfo.dwChannel = m_iChanShowNum;
        //        struVodPara.byVolumeType = 0;
        //        for (i = 0; i < System.Text.UTF8Encoding.Default.GetBytes("").Length; i++)
        //        {
        //            struVodPara.struIDInfo.byID[i] = System.Text.UTF8Encoding.Default.GetBytes("")[i];
        //        }

        //        if (struVodPara.byVolumeType == 1)
        //        {
        //            struVodPara.byVolumeNum = byte.Parse("");
        //        }
        //        struVodPara.byDrawFrame = 0;
        //        struVodPara.dwFileIndex = 0;
        //        struVodPara.hWnd = ucVideoPlayBack.Pic.Handle;
        //        struVodPara.byStreamType = 0;

        //        CHCNetSDK.NET_DVR_PLAYCOND struPlayCon = new CHCNetSDK.NET_DVR_PLAYCOND();
        //        struPlayCon.byStreamID = new byte[CHCNetSDK.STREAM_ID_LEN];
        //        struPlayCon.byDrawFrame = 0;
        //        struPlayCon.dwChannel = m_iChanShowNum;
        //        struPlayCon.struStartTime.dwYear = dtStart.Year;
        //        struPlayCon.struStartTime.dwMonth = dtStart.Month;
        //        struPlayCon.struStartTime.dwDay = dtStart.Day;
        //        struPlayCon.struStartTime.dwHour = dtStart.Hour;
        //        struPlayCon.struStartTime.dwMinute = dtStart.Minute;
        //        struPlayCon.struStartTime.dwSecond = dtStart.Second;
        //        struPlayCon.struStopTime.dwYear = dtEnd.Year;
        //        struPlayCon.struStopTime.dwMonth = dtEnd.Month;
        //        struPlayCon.struStopTime.dwDay = dtEnd.Day;
        //        struPlayCon.struStopTime.dwHour = dtEnd.Hour;
        //        struPlayCon.struStopTime.dwMinute = dtEnd.Minute;
        //        for (i = 0; i < System.Text.UTF8Encoding.Default.GetBytes("").Length; i++)
        //        {
        //            struPlayCon.byStreamID[i] = System.Text.UTF8Encoding.Default.GetBytes("")[i];
        //        }
        //        g_hPlayBackWnd = ucVideoPlayBack.Pic.Handle;
        //        //正向播放
        //        m_lPlayHandle = CHCNetSDK.NET_DVR_PlayBackByTime_V40((uint)m_lUserID, ref struVodPara);
        //        ////反向播放
        //        //m_lPlayHandle = CHCNetSDK.NET_DVR_PlayBackReverseByTime_V40(m_lUserID, CurVideo.Pic.Handle, ref struPlayCon);
        //        string strShow = null;
        //        if (m_lPlayHandle == -1)
        //        {
        //            strShow = string.Format("NET_DVR_PlayBackByTime_V40 ChanNum[" + m_iChanShowNum + "]");
        //            MessageBox.Show(strShow);
        //            return;
        //        }
        //        m_StreamPlayMode = 1;
        //        //g_fPlayDataCallBack_V40 = new CHCNetSDK.PLAYDATACALLBACK_V40(PlayDataCallBack_V40);
        //        //CHCNetSDK.NET_DVR_SetPlayDataCallBack_V40(m_lPlayHandle, g_fPlayDataCallBack_V40, IntPtr.Zero);
        //        strShow = string.Format("NET_DVR_PlayBackByTime_V40 ChanNum[" + m_iChanShowNum + "]");
        //        uint temp = 0;
        //        CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSTART, 0, ref temp);

        //        if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSTARTAUDIO, 0, ref temp))
        //        {
        //            m_bSound = true;
        //            CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYAUDIOVOLUME, (0xffff) / 2, ref temp);
        //        }
        //        else
        //        {
        //            m_bSound = false;
        //        }
        //        SetPlayState();
        //    }
        //    else
        //    {
        //        uint temp = 0;
        //        if (m_bPause)
        //        {
        //            if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYRESTART, 0, ref temp))
        //            {
        //                m_bPause = false;
        //                SetPlayState();
        //                //m_pnlPlayBack.Refresh();
        //            }
        //        }
        //        else
        //        {
        //            if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYPAUSE, 0, ref temp))
        //            {
        //                //m_pnlPlayBack.Refresh();
        //                m_bPause = true;
        //                m_btnPlay.Image = m_imgPlay.Images[0];
        //                m_btnPlay.Enabled = true;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 鹰眼回放
        ///// </summary>
        //private void EaglePlayBack()
        //{
        //    if (m_lPlayHandle != -1)
        //    {
        //        CHCNetSDK.NET_DVR_StopRealPlay(m_lPlayHandle);
        //        m_lPlayHandle = -1;
        //    }

        //    CHCNetSDK.NET_DVR_VOD_PARA struVodPara = new CHCNetSDK.NET_DVR_VOD_PARA();
        //    struVodPara.Init();

        //    if (m_iChanIndex > (g_struDeviceInfo[m_iDeviceIndex].iDeviceChanNum - 1) && (g_struDeviceInfo[m_iDeviceIndex].byMirrorChanNum) > 0)
        //    {
        //        m_iChanShowNum = (uint)g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[m_iChanIndex + 1 - g_struDeviceInfo[m_iDeviceIndex].wStartMirrorChanNo].iChannelNO;
        //    }
        //    else
        //    {
        //        m_iChanShowNum = (uint)g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[m_iChanIndex].iChannelNO;
        //    }

        //    struVodPara.struBeginTime.dwYear = dtStart.Year;
        //    struVodPara.struBeginTime.dwMonth = dtStart.Month;
        //    struVodPara.struBeginTime.dwDay = dtStart.Day;
        //    struVodPara.struBeginTime.dwHour = dtStart.Hour;
        //    struVodPara.struBeginTime.dwMinute = dtStart.Minute;
        //    struVodPara.struBeginTime.dwSecond = dtStart.Second;
        //    struVodPara.struEndTime.dwYear = dtEnd.Year;
        //    struVodPara.struEndTime.dwMonth = dtEnd.Month;
        //    struVodPara.struEndTime.dwDay = dtEnd.Day;
        //    struVodPara.struEndTime.dwHour = dtEnd.Hour;
        //    struVodPara.struEndTime.dwMinute = dtEnd.Minute;
        //    struVodPara.struEndTime.dwSecond = dtEnd.Second;
        //    struVodPara.struIDInfo.dwChannel = m_iChanShowNum;
        //    struVodPara.hWnd = IntPtr.Zero;
        //    struVodPara.struIDInfo.dwSize = (uint)Marshal.SizeOf(struVodPara.struIDInfo);
        //    struVodPara.dwSize = (uint)Marshal.SizeOf(struVodPara);

        //    m_lPlayBackHandle = CHCNetSDK.NET_DVR_PlayBackByTime_V40((uint)m_lUserID, ref struVodPara);
        //    if (m_lPlayBackHandle == -1)
        //    {
        //        string strShow = null;
        //        strShow = string.Format("NET_DVR_PlayBackByTime_V40 ChanNum[" + m_iChanShowNum + "]");
        //        MessageBox.Show(strShow);
        //        return;
        //    }
        //    m_StreamPlayMode = 1;
        //    g_fPlayDataCallBack_V40 = new CHCNetSDK.PLAYDATACALLBACK_V40(PlayDataCallBack_V40);
        //    if (!CHCNetSDK.NET_DVR_SetPlayDataCallBack_V40(m_lPlayBackHandle, g_fPlayDataCallBack_V40, IntPtr.Zero))
        //    {
        //        //m_dwLastError = CHCNetSDK.NET_DVR_GetLastError();
        //        return;
        //    }
        //    m_StreamPlayMode = 1;
        //    uint dwTemp = 0;
        //    int dwFileOffset = 0;
        //    if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayBackHandle, CHCNetSDK.NET_DVR_PLAYSTART, (uint)dwFileOffset, ref dwTemp))
        //    {
        //        //strShow = string.Format("NET_DVR_PLAYSTART offset{0}", dwFileOffset);
        //        //g_formList.AddLog(m_iDeviceIndex, CHCNetSDK.OPERATION_SUCC_T, strShow);
        //    }
        //    else
        //    {
        //        //strShow = string.Format("NET_DVR_PLAYSTART offset{0}", dwFileOffset);
        //        //g_formList.AddLog(m_iDeviceIndex, CHCNetSDK.OPERATION_FAIL_T, strShow);
        //        CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayBackHandle);
        //        m_lPlayHandle = -1;
        //        //g_formList.g_StringLanType(ref strShow, "回放失败!", "NET_DVR_PLAYSTART Failed!");
        //        //MessageBox.Show(strShow);
        //        return;
        //    }
        //}

        //public void PlayBack(DateTime start, DateTime end)
        //{
        //    m_PlayBackPort = -1;
        //    dtStart = start;
        //    dtEnd = end;
        //    if (CurVideo != null)
        //    {
        //        //关闭当前回放视频
        //        if (m_lPlayBackHandle != -1)
        //        {
        //            CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayBackHandle);
        //            m_lPlayBackHandle = -1;
        //        }
        //        m_iChanIndex = Convert.ToInt32(CurVideo.Tag);
        //        if (CurVideo.CameraType == 4)
        //        {
        //            ucVideoPlayBack.MouseDown += PlayBackMouseDown;
        //            ucVideoPlayBack.MouseUp += PlayBackMouseUp;
        //            ucVideoPlayBack.MouseMove += PlayBackMouseMove;
        //            ucVideoPlayBack.MouseWheel += PlayBackMouseWheel;
        //            //ucVideoPlayBack.e_EagleModeChange += video_e_EagleModeChange;
        //        }
        //        else
        //        {
        //            ucVideoPlayBack.MouseDown -= PlayBackMouseDown;
        //            ucVideoPlayBack.MouseUp -= PlayBackMouseUp;
        //            ucVideoPlayBack.MouseMove -= PlayBackMouseMove;
        //            ucVideoPlayBack.MouseWheel -= PlayBackMouseWheel;
        //            //ucVideoPlayBack.e_EagleModeChange -= video_e_EagleModeChange;
        //        }
        //        EaglePlayBack();
        //        //if (CurVideo.CameraType == 4)
        //        //{
        //        //    m_bIsEagle = true;
        //        //    EaglePlayBack();
        //        //}
        //        //else
        //        //{
        //        //    m_bIsEagle = false;
        //        //    NormalPlayBack();
        //        //}
        //        ////CloseAllVideo(false);
        //    }

        //}

        //#region 鹰眼的鼠标事件
        //private void PlayBackMouseUp(object sender, MouseEventArgs e)
        //{
        //    if (EaglePalyBackVideo != null)
        //    {
        //        m_bEagleMoved = false;
        //    }
        //}

        //private void PlayBackMouseDown(object sender, MouseEventArgs e)
        //{
        //    UCVideo video = sender as UCVideo;
        //    int tag = Convert.ToInt32(video.Tag);
        //    EaglePalyBackVideo = video;

        //    m_strPrePoint.x = (e.X - video.Pic.Left);
        //    m_strPrePoint.y = (e.Y - video.Pic.Top);
        //    m_bEagleMoved = true;
        //    video.Focus();
        //}

        //private void PlayBackMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (!m_bEagleMoved)
        //    {
        //        return;
        //    }
        //    int tag = Convert.ToInt32(EaglePalyBackVideo.Tag);
        //    float fpi = 3.1415926f;
        //    int iWndWidth = EaglePalyBackVideo.PicWidth; //当前窗口宽度
        //    int iWndHeight = EaglePalyBackVideo.PicHeight; //当前窗口高度
        //    PlayCtrl.DRAWPOINT strPoint = new PlayCtrl.DRAWPOINT();
        //    strPoint.x = e.X * 1.0f / iWndWidth;
        //    strPoint.y = e.Y * 1.0f / iWndHeight;
        //    strPoint.x = ((e.X - m_strPrePoint.x) * 1.0f) / iWndWidth;
        //    strPoint.y = ((e.Y - m_strPrePoint.y) * 1.0f) / iWndHeight;
        //    PlayCtrl.PLAYM4SRTRANSFERPARAM stParam = new PlayCtrl.PLAYM4SRTRANSFERPARAM();
        //    PlayCtrl.PLAYM4SRTRANSFERELEMENT stSubParam = new PlayCtrl.PLAYM4SRTRANSFERELEMENT();
        //    stSubParam.fAxisX = strPoint.x * 8 * fpi;
        //    stSubParam.fAxisY = strPoint.y * 4 * fpi;
        //    stSubParam.fAxisZ = 0.0f;
        //    stSubParam.fValue = 0.0f;
        //    IntPtr ptrSRTransformElement = Marshal.AllocHGlobal(Marshal.SizeOf(stSubParam));
        //    Marshal.StructureToPtr(stSubParam, ptrSRTransformElement, false);
        //    stParam.pTransformElement = ptrSRTransformElement;
        //    PlayCtrl.PlayM4_SR_Rotate(m_lPlayBackHandle, ref stParam);
        //    m_strPrePoint.x = e.X;
        //    m_strPrePoint.y = e.Y;
        //    Marshal.FreeHGlobal(ptrSRTransformElement);
        //}

        //private void SR_ZOOMPalyBack(float fStep, int tag)
        //{
        //    if (m_lPlayBackHandle < 0)
        //    {
        //        return;
        //    }
        //    PlayCtrl.PLAYM4SRTRANSFERPARAM stParam = new PlayCtrl.PLAYM4SRTRANSFERPARAM();
        //    PlayCtrl.PLAYM4SRTRANSFERELEMENT stSubParam = new PlayCtrl.PLAYM4SRTRANSFERELEMENT();
        //    stSubParam.fAxisX = 0.0f;
        //    stSubParam.fAxisY = 0.0f;
        //    stSubParam.fAxisZ = 0.0f;
        //    stSubParam.fValue = fStep;
        //    IntPtr ptrSRTransformElement = Marshal.AllocHGlobal(Marshal.SizeOf(stSubParam));
        //    Marshal.StructureToPtr(stSubParam, ptrSRTransformElement, false);
        //    stParam.pTransformElement = ptrSRTransformElement;
        //    PlayCtrl.PlayM4_SR_Rotate(m_lPlayBackHandle, ref stParam);
        //    Marshal.FreeHGlobal(ptrSRTransformElement);
        //}

        ///// <summary>
        ///// 鹰眼的滚轮事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void PlayBackMouseWheel(object sender, MouseEventArgs e)
        //{
        //    if (EaglePalyBackVideo != null)
        //    {
        //        int tag = Convert.ToInt32(EaglePalyBackVideo.Tag);
        //        float fStep = PlayCtrl.FEC_ZOOM_STEP; //根据缩放--步长可能为负数
        //        if (e.Delta < 0)
        //        {
        //            fStep = PlayCtrl.FEC_ZOOM_STEP;
        //        }
        //        else
        //        {
        //            fStep = (-1) * fStep;
        //        }
        //        SR_ZOOMPalyBack(fStep, tag);
        //    }
        //}
        //#endregion

        //private void PlayDataCallBack_V40(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUserData)
        //{
        //    //if (m_bIsEagle == true)
        //    {//鹰眼回调
        //        EaglePlayBackCallBack(lRealHandle, dwDataType, ref pBuffer, dwBufSize, pUserData);
        //    }
        //    return;
        //}

        //private void EaglePlayBackCallBack(Int32 lRealHandler, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        //{
        //    try
        //    {
        //        uint dwErr = 0;
        //        switch (dwDataType)
        //        {
        //            case CHCNetSDK.NET_DVR_SYSHEAD:
        //                if (m_PlayBackPort == -1)
        //                {
        //                    if (!PlayCtrl.PlayM4_GetPort(ref m_PlayBackPort))
        //                    {
        //                        //dwErr = PlayCtrl.PlayM4_GetLastError(m_Channel);
        //                    }
        //                    //m_Channel[lRealHandle] = lRealHandle;
        //                    uint version = PlayCtrl.PlayM4_GetSdkVersion();
        //                }
        //                if (dwBufSize > 0)
        //                {
        //                    if (!PlayCtrl.PlayM4_SetStreamOpenMode(m_PlayBackPort, m_StreamPlayMode))  //设置实时流播放模式
        //                    {
        //                        dwErr = PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
        //                        break;
        //                    }

        //                    if (!PlayCtrl.PlayM4_OpenStream(m_PlayBackPort, ref pBuffer, dwBufSize, 8 * 1024 * 1024)) //打开流接口
        //                    {
        //                        dwErr = PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
        //                        break;
        //                    }

        //                    if (!PlayCtrl.PlayM4_SetDisplayBuf(m_PlayBackPort, 6))
        //                    {
        //                        dwErr = PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
        //                        break;
        //                    }
        //                    int channel = Convert.ToInt32(CurVideo.Tag);
        //                    if (true)//小鹰眼码流的特殊处理
        //                    {
        //                        if (!PlayCtrl.PlayM4_SetDecodeEngineEx(m_PlayBackPort, PlayCtrl.HARD_DECODE_ENGINE))
        //                        {
        //                            break;
        //                        }
        //                        //UCVideo video= listVideo.Find(p => Convert.ToInt32(p.Tag) == lRealHandle);                               
        //                        PlayCtrl.PLAYM4SRMODELMODE struParam = new PlayCtrl.PLAYM4SRMODELMODE();
        //                        struParam.emTextureMode = (uint)PlayCtrl.PLAYM4SRTEXTUREMODE.PLAYM4_TEXTURE_DOUBLE;
        //                        struParam.ulDisplayType = PlayCtrl.PLAYM4_MODEL_SOLID;
        //                        struParam.nTransformMode = 0;
        //                        struParam.emModelType = (uint)PlayCtrl.PLAYM4SRMODELTYPE.PLAYM4_MODELTYPE_EAGLE_EYE;
        //                        struParam.bModelMode = true; // true是碗装，false是块状
        //                        int dwSize = Marshal.SizeOf(struParam);
        //                        IntPtr ptrDeviceCfg = Marshal.AllocHGlobal(dwSize);
        //                        Marshal.StructureToPtr(struParam, ptrDeviceCfg, false);
        //                        if (!PlayCtrl.PlayM4_SR_SetConfig(m_PlayBackPort, PlayCtrl.CFG_DISPLAY_MODEL_MODE, ptrDeviceCfg))
        //                        {
        //                            Marshal.FreeHGlobal(ptrDeviceCfg);
        //                            break;
        //                        }
        //                        Marshal.FreeHGlobal(ptrDeviceCfg);
        //                    }

        //                    if (!PlayCtrl.PlayM4_Play(m_PlayBackPort, ucVideoPlayBack.Pic.Handle))//播放开始
        //                    {
        //                        break;
        //                    }
        //                }
        //                break;
        //            case CHCNetSDK.NET_DVR_STREAMDATA:
        //                if (dwBufSize > 0 && m_PlayBackPort != -1)
        //                {
        //                    for (int i = 0; i < 4000; i++)
        //                    {
        //                        if (!PlayCtrl.PlayM4_InputData(m_PlayBackPort, ref pBuffer, dwBufSize))
        //                        {
        //                            dwErr = PlayCtrl.PlayM4_GetLastError(m_PlayBackPort);
        //                            if (dwErr == PlayCtrl.PLAYM4_BUF_OVER)
        //                            {
        //                                continue;
        //                            }
        //                            else
        //                            {
        //                                return;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            return;
        //                        }

        //                    }
        //                }
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        //private void StopPlay()
        //{
        //    char[] szLan = new char[128];
        //    uint uTempZero = 0;
        //    string strShow = "";
        //    if (m_lPlayHandle >= 0)
        //    {
        //        if (m_bTimeSave)
        //        {
        //            CHCNetSDK.NET_DVR_StopPlayBackSave(m_lPlayHandle);
        //            m_bTimeSave = false;
        //        }
        //        int idx = CHCNetSDK.NET_DVR_GetPlayBackPlayerIndex(m_lPlayHandle);

        //        PlayCtrl.PlayM4_SetDecCallBack(idx, null);
        //        CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSTOPAUDIO, 0, ref uTempZero);
        //        CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle);
        //        m_lPlayHandle = -1;

        //    }
        //    m_bSound = false;
        //    m_bPause = false;
        //    //m_pnlPlayBack.Refresh();
        //    SetStopState();
        //}

        //private void m_btnPlay_Click(object sender, EventArgs e)
        //{
        //    PlayBack(dtStart, dtEnd);
        //}

        //private void m_btnStop_Click(object sender, EventArgs e)
        //{
        //    StopPlay();
        //}

        //private void m_btnSlow_Click(object sender, EventArgs e)
        //{
        //    uint dwTemp = 0;
        //    if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSLOW, 0, ref dwTemp))
        //    {
        //    }
        //    else
        //    {
        //    }
        //}

        //private void m_btnFast_Click(object sender, EventArgs e)
        //{
        //    uint dwTemp = 0;
        //    if (CHCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYFAST, 0, ref dwTemp))
        //    {
        //    }
        //    else
        //    {
        //    }
        //}

        //private void m_btnCapture_Click(object sender, EventArgs e)
        //{
        //    string sFileName;
        //    sFileName = string.Format("C:\\Picture\\PlayBackByTime.bmp");
        //    if (CHCNetSDK.NET_DVR_PlayBackCaptureFile(m_lPlayHandle, sFileName))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //    }
        //}

        //private void trackBarPlaybackProgress_Scroll(object sender, EventArgs e)
        //{
        //    m_bScrool = true;
        //}

        //private void trackBarPlaybackProgress_MouseUp(object sender, MouseEventArgs e)
        //{
        //    int iPos = trackBarPlaybackProgress.Value;
        //    if (m_bScrool)
        //    {
        //        m_lRemoteHandle = new Int32[9];
        //        foreach (int i in m_lRemoteHandle)
        //        {
        //            m_lRemoteHandle[i] = -1;
        //        }
        //        CHCNetSDK.NET_DVR_GET_FIGURE_COND struCond = new CHCNetSDK.NET_DVR_GET_FIGURE_COND();

        //        int dwSize = Marshal.SizeOf(struCond);
        //        IntPtr ptrCurTime = Marshal.AllocHGlobal(dwSize);

        //        g_fGetFigureDataCallBack = new CHCNetSDK.REMOTECONFIGCALLBACK(ProcessGetFigureDataCallBack);

        //        int byMinute = 10;
        //        for (byte i = 0; i < 9; i++)
        //        {
        //            IntPtr ptrHandle = Marshal.AllocHGlobal(4);

        //            byMinute = byMinute + i;
        //            struCond.dwLength = (uint)dwSize;
        //            struCond.dwChannel = 33;
        //            struCond.struTimePoint.wYear = 2016;
        //            struCond.struTimePoint.byMonth = 5;
        //            struCond.struTimePoint.byDay = 3;
        //            struCond.struTimePoint.byHour = 0;
        //            struCond.struTimePoint.byMinute = (byte)byMinute;
        //            struCond.struTimePoint.bySecond = (byte)iPos;

        //            Marshal.StructureToPtr(struCond, ptrCurTime, false);
        //            Marshal.WriteInt32(ptrHandle, i + 1);

        //            m_lRemoteHandle[i] = CHCNetSDK.NET_DVR_StartRemoteConfig((int)m_lUserID, CHCNetSDK.NET_DVR_GET_FIGURE, ptrCurTime, dwSize, g_fGetFigureDataCallBack, ptrHandle);
        //            if (m_lRemoteHandle[i] < 0)
        //            {
        //                uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
        //                return;
        //            }
        //            else
        //            {

        //            }
        //        }

        //        Marshal.FreeHGlobal(ptrCurTime);

        //    }
        //}

        //public void ProcessGetFigureDataCallBack(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData)
        //{
        //    if (dwType == (uint)CHCNetSDK.NET_SDK_CALLBACK_TYPE.NET_SDK_CALLBACK_TYPE_DATA)
        //    {
        //        CHCNetSDK.NET_DVR_FIGURE_INFO struFigureInfo = new CHCNetSDK.NET_DVR_FIGURE_INFO();

        //        struFigureInfo = (CHCNetSDK.NET_DVR_FIGURE_INFO)Marshal.PtrToStructure(lpBuffer, typeof(CHCNetSDK.NET_DVR_FIGURE_INFO));

        //        Random iRan = new Random();
        //        string strPath = "C:/" + iRan.Next(16000 * Marshal.ReadInt32(pUserData)).ToString() + ".jpg";
        //        FileStream fs = new FileStream(strPath, FileMode.Create);
        //        int iLen = (int)struFigureInfo.dwPicLen;
        //        byte[] by = new byte[iLen];
        //        Marshal.Copy(struFigureInfo.pPicBuf, by, 0, iLen);
        //        fs.Write(by, 0, iLen);
        //        fs.Close();

        //        Int32 iTest = Marshal.ReadInt32(pUserData);

        //    }
        //    else if (dwType == (uint)CHCNetSDK.NET_SDK_CALLBACK_TYPE.NET_SDK_CALLBACK_TYPE_STATUS)
        //    {
        //        int dwStatus = Marshal.ReadInt32(lpBuffer);
        //    }
        //    return;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.IO;
using System.Runtime.InteropServices;

using ToolsManage.BaseClass;

namespace ToolsManage.VideoManage
{
    //public delegate void VideoDoubleClick(object sender, MouseEventArgs e);
    //public enum VideoType
    //{
    //    ManualVideo,
    //    AlarmVideo,
    //    None,
    //}

    public partial class frmVideoTime : DevExpress.XtraEditors.XtraForm
    {
        public string DVRIPAddress = "192.168.1.213";
        public Int16 DVRPortNumber = 8000;
        public string DVRUserName = "admin";
        public string DVRPassword = "admin12345";

        private bool m_IsStartVideo = false;  //录像标志
        //private VideoType m_CurVideoTyp = VideoType.ManualVideo;
        private string FileSavePath = "";
        private string FilePath = @"D:\HikVideo\";

        System.Threading.Timer TimerDeal;

        private bool m_bInitSDK = false;//初始化SDK
        private Int32 m_lUserID = -1;//用户ID值
        private CHCNetSDK.NET_DVR_DEVICEINFO_V30 m_struDeviceInfo;//设备信息
        private Int32 m_iPreviewType = 0;//预览类型
        private IntPtr m_ptrRealHandle;//预览picbox控件句柄
        private CHCNetSDK.REALDATACALLBACK m_fRealData = null;//预览回调
        private Int32 m_lRealHandle = -1;
        private Int32 m_lPort = -1;
        private PlayCtrl.DISPLAYCBFUN m_fDisplayFun = null;
        private bool m_bJpegCapture = false;

        private float VideoSize = 5;  //单位 分钟
        //private int MoveAlarmOver = 5;  //单位 秒

        public frmVideoTime()
        {
            InitializeComponent();


        }

        private void VideoInitAndPriview()
        {
            if (sbtnLook.Text == "预览")
            {
                m_bInitSDK = CHCNetSDK.NET_DVR_Init();
                if (m_bInitSDK == false)
                {
                    //MessageBox.Show("设备初始化失败!");
                    return;
                }

                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref m_struDeviceInfo);
                if (m_lUserID == -1)
                {
                    MessageBox.Show("登录失败!");
                    return;
                }
                else
                {
                    //MessageBox.Show("登录成功!");
                    TimerStart();
                    //StartAlarmVideo();

                    CHCNetSDK.NET_DVR_CLIENTINFO lpClientInfo = new CHCNetSDK.NET_DVR_CLIENTINFO();//摄像头 配置信息结构
                    lpClientInfo.lChannel = 1;
                    lpClientInfo.lLinkMode = 0x0000;//最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
                    lpClientInfo.sMultiCastIP = "";//多播组地址
                    if (m_iPreviewType == 0) // use by callback
                    {
                        lpClientInfo.hPlayWnd = IntPtr.Zero;//播放窗口的句柄,为NULL表示不播放图象
                        m_ptrRealHandle = RealPlayWnd.Handle;//获取控件绑定到的窗口句柄。 RealPlayWnd显示界面
                        m_fRealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);// 预览回调
                        IntPtr pUser = new IntPtr();
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V30(m_lUserID, ref lpClientInfo, m_fRealData, pUser, 1);//实时预览
                    }
                    else if (1 == m_iPreviewType)
                    {
                        lpClientInfo.hPlayWnd = RealPlayWnd.Handle;
                        IntPtr pUser = new IntPtr();
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V30(m_lUserID, ref lpClientInfo, null, pUser, 1);
                    }
                    if (m_lRealHandle == -1)
                    {
                        uint nError = CHCNetSDK.NET_DVR_GetLastError();
                        VideoRetErrorDis(nError);
                        //DebugInfo("NET_DVR_RealPlay fail %d!");
                        return;
                    }
                    sbtnLook.Text = "停止预览";
                    //this.btnPriview.Image = global::HikHelp.Properties.Resources.停止预览;
                }
            }
            else if (sbtnLook.Text == "停止预览")
            {
                sbtnLook.Text = "预览";
                //btnPriview.Image = global::HikHelp.Properties.Resources.预览;
                StopPriview();
            }
        }

        private void sbtnLogin_Click(object sender, EventArgs e)
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("设备初始化失败!");
                return;
            }
            else
            {
            }
            string DVRIPAddress = "192.168.1.213";
            Int16 DVRPortNumber = 8000;
            string DVRUserName = "admin";
            string DVRPassword = "admin12345";
            m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref m_struDeviceInfo);
            if (m_lUserID == -1)
            {
                //MessageBox.Show("登录失败!");
                return;
            }
            else
            {
                MessageBox.Show("登录成功!");
                TimerStart();
                //StartAlarmVideo();
            }
        }

        private void sbtnLook_Click(object sender, EventArgs e)
        {
            VideoInitAndPriview();
        }

        private void StopPriview()
        {
            try
            {
                if (m_lRealHandle >= 0)
                {
                    CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);//停止预览。
                }
                if (m_lUserID >= 0)
                {
                    CHCNetSDK.NET_DVR_Logout_V30(m_lUserID);
                }
                if (m_bInitSDK == true)
                {
                    bool isok = CHCNetSDK.NET_DVR_Cleanup();//释放SDK资源，在结束之前最后调用
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            switch (dwDataType)
            {
                case CHCNetSDK.NET_DVR_SYSHEAD:     // sys head
                    if (!PlayCtrl.PlayM4_GetPort(ref m_lPort))
                    {
                        MessageBox.Show("获取播放端口失败！");
                    }

                    if (dwBufSize > 0)
                    {
                        //set as stream mode, real-time stream under preview
                        if (!PlayCtrl.PlayM4_SetStreamOpenMode(m_lPort, PlayCtrl.STREAME_REALTIME))
                        {
                            uint nError = CHCNetSDK.NET_DVR_GetLastError();
                            VideoRetErrorDis(nError);
                        }
                        //start player
                        if (!PlayCtrl.PlayM4_OpenStream(m_lPort, ref pBuffer, dwBufSize, 1024 * 1024))
                        {
                            m_lPort = -1;
                            break;
                        }
                        //set soft decode display callback function to capture
                        m_fDisplayFun = new PlayCtrl.DISPLAYCBFUN(RemoteDisplayCBFun);
                        if (!PlayCtrl.PlayM4_SetDisplayCallBack(m_lPort, m_fDisplayFun))
                        {
                            uint nError = CHCNetSDK.NET_DVR_GetLastError();
                            VideoRetErrorDis(nError);
                        }
                        //start play, set play window
                        if (!PlayCtrl.PlayM4_Play(m_lPort, m_ptrRealHandle))
                        {
                            m_lPort = -1;
                            break;
                        }
                        //set frame buffer number
                        if (!PlayCtrl.PlayM4_SetDisplayBuf(m_lPort, 15))
                        {
                            uint nError = CHCNetSDK.NET_DVR_GetLastError();
                            VideoRetErrorDis(nError);
                        }
                        //set display mode
                        if (!PlayCtrl.PlayM4_SetOverlayMode(m_lPort, 0, 0/* COLORREF(0)*/))//play off screen // todo!!!
                        {
                            uint nError = CHCNetSDK.NET_DVR_GetLastError();
                            VideoRetErrorDis(nError);
                        }
                    }

                    break;
                case CHCNetSDK.NET_DVR_STREAMDATA:     // video stream data
                    if (dwBufSize > 0 && m_lPort != -1)
                    {
                        if (!PlayCtrl.PlayM4_InputData(m_lPort, ref pBuffer, dwBufSize))
                        {
                            uint nError = CHCNetSDK.NET_DVR_GetLastError();
                            VideoRetErrorDis(nError);
                        }
                    }
                    break;

                case CHCNetSDK.NET_DVR_AUDIOSTREAMDATA:     //  Audio Stream Data
                    if (dwBufSize > 0 && m_lPort != -1)
                    {
                        if (!PlayCtrl.PlayM4_InputVideoData(m_lPort, ref pBuffer, dwBufSize))
                        {
                            uint nError = CHCNetSDK.NET_DVR_GetLastError();
                            VideoRetErrorDis(nError);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void RemoteDisplayCBFun(int nPort, IntPtr pBuf, int nSize, int nWidth, int nHeight, int nStamp, int nType, int nReserved)
        {
            try
            {
                if (!m_bJpegCapture)
                {
                    return;
                }
                else
                {
                }
                //m_bJpegCapture = false;
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        private string VideoRetErrorDis(uint code)
        {
            switch (code)
            {
                case 1:
                    //MessageBox.Show("用户名密码错误！");
                    return "用户名密码错误！";
                case 2:
                    //MessageBox.Show("用户权限不足！");
                    return "用户权限不足！";
                case 3:
                    //MessageBox.Show("设备没有初始化！");
                    return "设备没有初始化！";
                case 4:
                    //MessageBox.Show("通道号错误！");
                    return "通道号错误！";
                case 5:
                    //MessageBox.Show("连接到设备的用户个数超过最大！");
                    return "连接到设备的用户个数超过最大！";
                case 6:
                    //MessageBox.Show("版本不匹配,SDK 和设备的版本不匹配！");
                    return "版本不匹配,SDK 和设备的版本不匹配！";
                case 7:
                    //MessageBox.Show("连接设备失败,设备不在线或网络原因引起的连接超时等！");
                    return "连接设备失败,设备不在线或网络原因引起的连接超时等！";
                case 8:
                    //MessageBox.Show("向设备发送失败！");
                    return "向设备发送失败！";
                case 9:
                    //MessageBox.Show("从设备接收数据失败！");
                    return "从设备接收数据失败！";
                case 10:
                    //MessageBox.Show("从设备接收数据超时！");
                    return "从设备接收数据超时！";
                case 12:
                    //MessageBox.Show("调用次序错误！");
                    return "调用次序错误！";
                case 14:
                    //MessageBox.Show("设备命令执行超时！");
                    return "设备命令执行超时！";
                case 38:
                    //MessageBox.Show("播放出错！");
                    return "播放出错！";
                case 40:
                    //MessageBox.Show("路径错误！");
                    return "路径错误！";
                default:
                    return "未知错误！";
            }
        }

        private void sbtnVideo_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_lUserID != -1)
                {
                    if (sbtnVideo.Text == "录像")
                    {
                        sbtnVideo.Text = "停止录像";
                        //btnLX.Image = global::HikHelp.Properties.Resources.停止录像;
                        string info = "";
                        StopVideo(true, ref info);
                        //m_CurVideoTyp = VideoType.ManualVideo;  //手动录像
                        FileSavePath = GenFileInfo();
                        StartVideo(ref info);
                    }
                    else if (sbtnVideo.Text == "停止录像")
                    {
                        sbtnVideo.Text = "录像";
                        //btnLX.Image = global::HikHelp.Properties.Resources.录像;
                        string info = "";
                        StopVideo(true, ref info);
                    }
                }
                else
                {
                    //MessageBox.Show("请先登录！");
                }
            }
            catch 
            {}
        }

        private bool StopVideo(bool ismustStop, ref string info)
        {
            try
            {
                if (m_IsStartVideo)
                {
                    m_IsStartVideo = false;
                    VideoCountJS = 0;
                    if (CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle))
                    {
                        if (ismustStop)
                        {
                            return true;
                        }
                        else
                        {
                            try
                            {
                                bool blFree = DirectoryUtil.IsDiskSpaceEnough(FilePath, 2000000000);// 45172523008  42G     50000000000
                                if (!blFree)                                                                              // 2000000000 
                                {
                                    string filepaht = FilePath;
                                    if (Directory.Exists(filepaht))
                                    {
                                        DirectoryInfo theFolder = new DirectoryInfo(filepaht);// DirectoryInfo   公开用于创建、移动和枚举目录和子目录的实例方法。
                                        DirectoryInfo[] dirInfo = theFolder.GetDirectories();
                                        string folderName = dirInfo[0].Name;
                                        filepaht += folderName;
                                        if (Directory.Exists(filepaht))
                                        {
                                            DirectoryUtil.DeleteDirectory(filepaht);
                                        }
                                    }
                                }
                            }
                            catch(Exception ee)
                            {
                                MessageUtil.ShowTips(ee.ToString());
                            }
                            FileSavePath = GenFileInfo();

                            bool isok = StartVideo(ref info);
                            if (isok)
                                return true;
                            else
                                return false;
                        }
                    }
                    else
                    {
                        m_IsStartVideo = false;
                        uint nError = CHCNetSDK.NET_DVR_GetLastError();
                        info = VideoRetErrorDis(nError);
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                info = ex.Message;
                return false;
            }
        }

        private string GenFileInfo()
        {
            try
            {
                
                
                string filepath = "";
                //if (videotype == VideoType.ManualVideo)
                //{
                //    FileMode = "手动录像";
                //}
                //else if (videotype == VideoType.AlarmVideo)
                //{
                //    FileMode = "移动录像";
                //}
                string FileTime = DateTime.Now.ToString("yyyyMMdd");
                filepath = FilePath  +  FileTime + "\\";
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".mp4";
                filepath += FileName;
                return filepath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private bool StartVideo(ref string info)
        {
            try
            {
                if (FileSavePath != "")
                {//保存路径存在
                    if (m_lRealHandle != -1)
                    {//有预览的录像
                        if (CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, FileSavePath))
                        {
                            m_IsStartVideo = true;
                            return true;
                        }
                        else
                        {
                            m_IsStartVideo = false;
                            uint nError = CHCNetSDK.NET_DVR_GetLastError();
                            info = VideoRetErrorDis(nError);
                            return false;
                        }
                    }
                    else
                    {//不带预览的录像  这样可以不用监控就可以预览
                        CHCNetSDK.NET_DVR_CLIENTINFO lpClientInfo = new CHCNetSDK.NET_DVR_CLIENTINFO();
                        lpClientInfo.lChannel = 1;
                        lpClientInfo.lLinkMode = 0x0000;
                        lpClientInfo.sMultiCastIP = "";
                        //lpClientInfo.hPlayWnd = RealPlayWnd.Handle;
                        IntPtr pUser = new IntPtr();
                        m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V30(m_lUserID, ref lpClientInfo, null, pUser, 1);
                        if (m_lRealHandle == -1)
                        {
                            //MessageBox.Show("录像失败！");
                            info = "录像失败！";
                            return false;
                        }
                        else
                        {
                            if (CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, FileSavePath))
                            {
                                m_IsStartVideo = true;
                                return true;
                            }
                            else
                            {
                                m_IsStartVideo = false;
                                uint nError = CHCNetSDK.NET_DVR_GetLastError();
                                info = VideoRetErrorDis(nError);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    info = "保存路径不正确！";
                    return false;
                }
            }
            catch (Exception ex)
            {
                info = ex.Message;
                return false;
            }
        }

        private void TimerStart()
        {
            if (TimerDeal == null)
            {
                TimerDeal = new System.Threading.Timer(new System.Threading.TimerCallback(TimerCall), null, 0, 1000);
            }
            else
            {
                TimerDeal.Change(200, 1000);
            }
        }

        int VideoCountJS = 0;
        //int AlarmVideoCount = 0;
        //int NoSingleCount = 0;
        string retInfo = "";

        private void TimerCall(object obj)
        {
            if (m_IsStartVideo ) //if (m_IsStartVideo && m_CurVideoTyp == VideoType.ManualVideo)
            {//开始录像 一分钟一个文件
                if (VideoCountJS++ >= 60 * VideoSize)
                {
                    VideoCountJS = 0;
                    StopVideo(false, ref retInfo);
                }
            }
            //else if (m_IsStartVideo && m_CurVideoTyp == VideoType.AlarmVideo)
            //{
            //    if (AlarmVideoCount++ >= 60 * VideoSize)
            //    {
            //        AlarmVideoCount = 0;
            //        StopVideo(false, ref retInfo);
            //    }
            //    if (!m_bJpegCapture)
            //    {//5秒都没有移动侦测 就停止移动侦测录像
            //        if (NoSingleCount++ >= MoveAlarmOver)
            //        {
            //            AlarmVideoCount = 0;
            //            NoSingleCount = 0;
            //            StopVideo(true, ref retInfo);
            //        }
            //    }
            //    else
            //    {
            //        NoSingleCount = 0;
            //        m_bJpegCapture = false;
            //    }
            //}
        }

        private void sbtnBackLook_Click(object sender, EventArgs e)
        {
            frmBackVideo frm = new frmBackVideo();
            frm.ShowDialog(this );
            frm.Dispose();
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Close();
        }

        private void frmVideoTime_Load(object sender, EventArgs e)
        {
            DVRIPAddress = MainControl.strCameraIp;
            DVRPortNumber = MainControl.iCameraPort;
            DVRUserName = MainControl.strCameraUser;
            DVRPassword = MainControl.strCameraPsw;

            VideoInitAndPriview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fbDialogFile.ShowDialog();
            string str = fbDialogFile.SelectedPath.ToString().Trim();// + "\\"

            DirectoryUtil.DeleteDirectory(str);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bool blFree = DirectoryUtil.IsDiskSpaceEnough(FilePath, 50000000000);// 45172523008  42G   50000000000
                if (!blFree)
                {
                    string filepaht = FilePath ;
                    if (Directory.Exists(filepaht))
                    {

                        DirectoryInfo theFolder = new DirectoryInfo(filepaht);// DirectoryInfo   公开用于创建、移动和枚举目录和子目录的实例方法。
                        DirectoryInfo[] dirInfo = theFolder.GetDirectories();
                        string folderName = dirInfo[0].Name;
                        filepaht += folderName;
                        //遍历文件夹
                        if (Directory.Exists(filepaht))
                        {
                            DirectoryUtil.DeleteDirectory(filepaht);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageUtil.ShowTips(ee.ToString());
            }
        }

    }
}
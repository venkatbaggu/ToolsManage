using Common.FileLog;
using ReaderB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ToolsManage.BaseClass.RFidClass.NetRfid
{
    /// <summary>
    /// 单个rfid
    /// </summary>
    public class SingleRfid
    {
        #region 变量
        private int fOpenComIndex; //打开的串口索引号
        private int frmcomportindex;
        private byte ComAddr = 0;
        /// <summary>
        /// 读写器地址
        /// </summary>
        private byte fComAdr = 0xff;
        /// <summary>
        /// 标签信息
        /// </summary>
        private Dictionary<string, EpcInfo> dicEpcInfo = new Dictionary<string, EpcInfo>();
        public TimerHelper timerRead = null;
        bool IsTimerRun = false;
        System.Threading.Timer m_Timer = null;
        Thread threadDealRfid = null;
        private int CurInterval = 50;
        public static bool IsDealRfid = true;
        //private UHFReader28 StaticClassReaderB = null;
        #endregion

        #region 属性
        private string m_IP;

        private int m_Port;

        private string m_Name;

        private bool blConnent = false;
        private bool blScan = false;
        private int iReadEpcErr = 0;
        private int iLinkErr = 0;
        private DateTime timeLinkErr;
        CommuniState stateNet = CommuniState.初值;
        private DeviceUsing usingReadNo;

        /// <summary>
        /// 是否 顺序扫描
        /// </summary>
        public DeviceUsing UsingReadNo
        {
            get { return usingReadNo; }
            set { usingReadNo = value; }
        }

        public CommuniState StateNet
        {
            get { return stateNet; }
            set { stateNet = value; }
        }

        /// <summary>
        /// 读EPC标签 计数 三次读失败 断开连接
        /// </summary>
        public int IReadEpcErr
        {
            get { return iReadEpcErr; }
            set { iReadEpcErr = value; }
        }
        /// <summary>
        /// 三次 连接错误 断RFID电源
        /// </summary>
        public int ILinkErr
        {
            get { return iLinkErr; }
            set { iLinkErr = value; }
        }

        public DateTime TimeLinkErr
        {
            get { return timeLinkErr; }
            set { timeLinkErr = value; }
        }

        public bool BlScan
        {
            get { return blScan; }
            set { blScan = value; }
        }

        public bool BlConnent
        {
            get { return blConnent; }
            set { blConnent = value; }
        }


        public string IP
        {
            get
            {
                return m_IP;
            }

            set
            {
                m_IP = value;
            }
        }

        public int Port
        {
            get
            {
                return m_Port;
            }

            set
            {
                m_Port = value;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }
        #endregion

        #region  事件

        /// <summary>
        /// 新事件显示 事件
        /// </summary>
        public event NewEventShowEventHandler NewEventShowEvent;

        /// <summary>
        /// 新告警显示 事件
        /// </summary>
        public event NewEventShowEventHandler NewAlarmShowEvent;

        public delegate void delNewErrRecord(string strType, string strPoint, string strContent, string strRemark);

        public event delNewErrRecord e_NewErrRecord;

        public delegate void delReStartRfid();

        public event delReStartRfid e_ReStartRfid;

        #endregion

        public SingleRfid()
        {
            //timerRead = new TimerHelper(50, false);
            //timerRead.Execute += new TimerHelper.TimerExecution(timerRead_Execute);
        }

        public SingleRfid(string _ip, int _port, string _name):this()
        {
            m_IP = _ip;
            m_Port = _port;
            m_Name = _name;
            //StaticClassReaderB = new UHFReader28(m_IP);
        }

        /// <summary>
        /// 开始定时器
        /// </summary>
        private void StartTimer()
        {
            //if (m_Timer == null)
            //{
            //    m_Timer = new System.Threading.Timer(new System.Threading.TimerCallback(timerDealRfid), null, 1000, 2000);
            //}
            threadDealRfid = new Thread(new ParameterizedThreadStart(timerDealRfid));
            threadDealRfid.Start();
        }

        /// <summary>
        /// 结束定时器
        /// </summary>
        private void StopTimer()
        {
            if(threadDealRfid!=null)
            {
                threadDealRfid.Abort();
                threadDealRfid = null;
            }
            //if (m_Timer != null)
            //{
            //    m_Timer.Change(0, -1);
            //    m_Timer.Dispose();
            //    m_Timer = null;
            //}
        }

        private void ChangeTimer(int period)
        {
            if (CurInterval != period)
            {
                if (m_Timer != null)
                {
                    CurInterval = period;
                    m_Timer.Change(1000, period);
                }
            }
        }

        public void timerStart()
        {
            //if(timerRead.State== TimerState.Stopped)
            //    timerRead.Start();
            StartTimer();
        }

        public void timerStop()
        {
            //if(timerRead.State== TimerState.Running)
            //    timerRead.Stop();
            StopTimer();
        }

        public bool Start()
        {
            bool blRet = false;
            try
            {
                int iRet = StaticClassReaderB.OpenNetPort(m_Port, m_IP, ref fComAdr, ref frmcomportindex);
                //int iRet = StaticClassReaderB.OpenNetPort(m_IP, m_Port, ref ComAddr);
                fOpenComIndex = frmcomportindex;

                if (iRet == 0)
                {
                    blRet = true;
                }
                if ((iRet == 0x35) || (iRet == 0x30))
                {//连接TCPIP错误
                    StaticClassReaderB.CloseNetPort(frmcomportindex);
                    //StaticClassReaderB.CloseNetPort();
                    blRet = false;
                    return blRet;
                }
                if ((fOpenComIndex != -1) && (iRet != 0X35) && (iRet != 0X30))
                {
                    blRet = true;
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
            return blRet;
        }

        public bool Stop()
        {
            try
            {
                //TXTWriteHelper.WriteException("开始停止-" + m_IP + "-" + frmcomportindex);
                int ret = StaticClassReaderB.CloseNetPort(frmcomportindex);
                //int ret= StaticClassReaderB.CloseNetPort();
                if (ret == 0)
                {
                    //TXTWriteHelper.WriteException("停止成功-" + m_IP + "-" + frmcomportindex);
                    return true;
                }
                else
                {
                    //TXTWriteHelper.WriteException("停止失败-" + m_IP + "-" + frmcomportindex);
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
                //TXTWriteHelper.WriteException("停止异常-" + m_IP + "-" + frmcomportindex);
            }
            return false;
        }



        private void timerDealRfid(object obj)
        {

            while (true)
            {
                try
                {
                    if (!IsTimerRun)
                    {
                        IsTimerRun = true;
                        if (BlConnent == false)
                        {//连接rfid               
                            Thread.Sleep(1000);
                            if(m_IP=="192.168.1.198")
                            { }
                            OpenRfidNewState();
                        }
                        else
                        {
                            Thread.Sleep(50);
                            if (BlConnent)
                            {                               
                                bool blRet = ReadEpcInfo();
                                if (blRet)
                                {
                                    //EpcBorrOrRet(iIndex);
                                }
                                if (IReadEpcErr > 3)
                                {
                                    IReadEpcErr = 0;
                                    //Stop();
                                    BlConnent = false;
                                }
                            }
                        }
                        IsTimerRun = false;
                    }
                }
                catch (Exception ex)
                {
                    if (frmMain.blDebug)
                        MessageUtil.ShowTips(ex.Message);
                    BlConnent = false;
                    IsTimerRun = false;
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// 定时读Rfid读写器,获取Epc信息
        /// </summary>
        private void timerRead_Execute()
        {
            //try
            //{
            //    if (!IsTimerRun)
            //    {
            //        IsTimerRun = true;
            //        if (BlConnent == false)
            //        {//连接rfid
            //            OpenRfidNewState();
            //        }
            //        else
            //        {
            //            if (BlConnent)
            //            {
            //                bool blRet = ReadEpcInfo();
            //                if (blRet)
            //                {
            //                    //EpcBorrOrRet(iIndex);
            //                }
            //                if (IReadEpcErr > 3)
            //                {
            //                    IReadEpcErr = 0;
            //                    Stop();
            //                    BlConnent = false;
            //                }
            //            }
            //        }
            //        IsTimerRun = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (frmMain.blDebug)
            //        MessageUtil.ShowTips(ex.Message);
            //    IsTimerRun = false;
            //}
        }

        /// <summary>
        /// 连接 读写器  更新通信状态
        /// </summary>
        private void OpenRfidNewState()
        {
            if (StateNet == CommuniState.错误)
            {
                TimeSpan span = DateTime.Now - TimeLinkErr;
                if (span.TotalMinutes < 1)
                {
                    return;
                }
            }
            //测试的时候注释掉 正式用的时候开启
            //if (frmMain.stateRstRfid == StateRstRfid.关闭 || frmMain.stateRstRfid == StateRstRfid.重启)
            //{
            //    return;
            //}

            bool blRet = Stop();  //rfid关闭连接
            blRet = Start();      //rfid打开连接
            if (blRet)
            {
                BlConnent = true;
                ILinkErr = 0;
                if (StateNet == CommuniState.错误)
                {
                    //通信恢复正常
                    string strName = Name;
                    string strContent = ErrorContent.通信恢复正常.ToString();
                    //commonCls.NewErrRecord(EventType.RFID读写器.ToString(), strName, strContent, "");
                    if (e_NewErrRecord != null)
                        e_NewErrRecord(EventType.RFID读写器.ToString(), strName, strContent, "");
                    if (NewAlarmShowEvent != null)
                    {
                        NewAlarmShowEvent(new NewEventEventArgs(EventType.RFID读写器, strName, strContent, "", "", DateTime.Now));
                    }
                }
                if (StateNet != CommuniState.正常)
                {
                    StateNet = CommuniState.正常;
                }
            }
            else
            {
                ILinkErr++;
            }
            if (ILinkErr >= 2)
            {
                ILinkErr = 0;
                if (StateNet == CommuniState.正常 || StateNet == CommuniState.初值)
                {
                    //异常记录
                    string strName = Name;
                    string strContent = ErrorContent.通信异常.ToString();
                    //commonCls.NewErrRecord(EventType.RFID读写器.ToString(), strName, strContent, "");
                    if (e_NewErrRecord != null)
                        e_NewErrRecord(EventType.RFID读写器.ToString(), strName, strContent, "");
                    if (NewAlarmShowEvent != null)
                    {
                        NewAlarmShowEvent(new NewEventEventArgs(EventType.RFID读写器, strName, strContent, "", "", DateTime.Now));
                    }
                }
                else if (StateNet == CommuniState.错误)
                {
                    //commonCls.NewErrRecord(EventType.系统.ToString(), "", ErrorContent.RFID读写器异常系统重启.ToString(), "");
                    if (e_NewErrRecord != null)
                        e_NewErrRecord(EventType.系统.ToString(), "", ErrorContent.RFID读写器异常系统重启.ToString(), "");
                    //timerRead.Stop();
                    //Thread.Sleep(1000);
                    if (e_ReStartRfid != null)
                        e_ReStartRfid();
                    //frmMain.RFIDReLoad = ErrorContent.RFID读写器异常系统重启;
                }
                if (StateNet != CommuniState.错误)
                {
                    StateNet = CommuniState.错误;
                }
                TimeLinkErr = DateTime.Now;
            }
        }

        public bool ReadEpcInfo()
        {
            bool blRet = false;
            try
            {
                byte MaskMem = 0;
                byte[] MaskAdr = new byte[2];
                byte MaskLen = 0;
                byte[] MaskData = new byte[100];
                byte MaskFlag = 0;
                byte AdrTID = 0;
                byte LenTID = 0;
                byte TIDFlag = 0;
                byte Ant = 0;

                blScan = true;
                int CardCount = 0;//输出变量，电子标签的张数
                int EpcLen = 0;//EPC 的字节数
                byte[] bEpcArray = new byte[5000];//指向输出数组变量 是读到的电子标签的EPC数据，是一张标签的EPC长度+一张标签的EPC号，依此累加


                //int fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, MaskMem, MaskLen, MaskFlag);
                //if ((fCmdRet==0)|| (fCmdRet == 1) || (fCmdRet == 2) || (fCmdRet == 3) || (fCmdRet == 4) || (fCmdRet == 0xFB))//代表已查找结束，
                //{ }
                //else
                //{
                //    iReadEpcErr++;
                //    blRet = false;
                //}

                int fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, bEpcArray, ref Ant, ref EpcLen, ref CardCount, frmcomportindex);// 8616
                if ((fCmdRet == 1) || (fCmdRet == 2) || (fCmdRet == 3) || (fCmdRet == 4) || (fCmdRet == 0xFB))//代表已查找结束，
                {
                    iReadEpcErr = 0;
                    blRet = true;
                    if (CardCount == 0)
                    {
                        blScan = false;
                        return blRet;
                    }
                    byte[] bEpcCopy = new byte[EpcLen];
                    Array.Copy(bEpcArray, bEpcCopy, EpcLen);
                    string strEpcAll = SerialPortUtil.ByteToStr(bEpcCopy);

                    int iEpcIndex = 0;//单张EPC的长度在bEpcCopy中的位置
                    for (int i = 0; i < CardCount; i++)
                    {
                        int iEpcLen = bEpcCopy[iEpcIndex];//epc的长度
                        string strEpc = strEpcAll.Substring(iEpcIndex * 2 + 2, iEpcLen * 2);//得到一个EPC数据
                        iEpcIndex = iEpcIndex + iEpcLen + 1;//下一个EPC标签的 标签长度指示位置

                        if (strEpc.Length != 12)
                            break;
                        bool blHas = false;
                        if (dicEpcInfo.ContainsKey(strEpc))
                        {//存在标签信息
                            #region 顺序读取
                            if (usingReadNo == DeviceUsing.启用)
                            {
                                if (dicEpcInfo[strEpc].IntAnt != Ant)
                                {//查询到标签的天线和原来的不一样                                  
                                    if ((dicEpcInfo[strEpc].IntAnt & 0x01) == 0x01 || (dicEpcInfo[strEpc].IntAnt & 0x02) == 0x02)
                                    {//归还 原来是1# 2#天线扫描到
                                        if ((Ant & 0x04) == 0x04 || (Ant & 0x08) == 0x08)
                                        {//现在是3# 4#天线扫描到
                                            dicEpcInfo[strEpc].BorrOrRet = ToolsBorrRet.归还;
                                            dicEpcInfo[strEpc].DtReadedEpc = DateTime.Now;
                                            RfidManage.QEpcInfo.Enqueue(dicEpcInfo[strEpc]);
                                        }
                                    }
                                    else
                                    {//借出
                                        if ((dicEpcInfo[strEpc].IntAnt & 0x04) == 0x04 || (dicEpcInfo[strEpc].IntAnt & 0x08) == 0x08)
                                        {//原来是3# 4#天线扫描到
                                            if ((Ant & 0x01) == 0x01 || (Ant & 0x02) == 0x02)
                                            {//现在是1# 2#天线扫描到
                                                dicEpcInfo[strEpc].BorrOrRet = ToolsBorrRet.借出;
                                                dicEpcInfo[strEpc].DtReadedEpc = DateTime.Now;
                                                RfidManage.QEpcInfo.Enqueue(dicEpcInfo[strEpc]);
                                            }
                                        }
                                    }
                                    dicEpcInfo[strEpc].IntAnt = Ant;
                                }
                                dicEpcInfo[strEpc].UsingReadNo = DeviceUsing.启用;
                            }
                            blHas = true;
                            #endregion
                        }
                        else
                        {
                            EpcInfo ei = new EpcInfo(strEpc, Ant);
                            ei.TimeLastRead = DateTime.Now;
                            if (usingReadNo == DeviceUsing.启用)
                            {
                                ei.UsingReadNo = DeviceUsing.启用;
                            }
                            ei.BorrOrRet = ToolsBorrRet.未知;
                            ei.IsEpcRead = IsReadShow.未读;
                            dicEpcInfo.Add(strEpc, ei);
                        }
                    }
                }
                else
                {
                    iReadEpcErr++;
                    blRet = false;
                }
                blScan = false;
            }
            catch (Exception ee)
            {
                if (frmMain.blDebug)
                {
                    MessageUtil.ShowTips(ee.Message);
                }
            }
            return blRet;
        }
    }
}

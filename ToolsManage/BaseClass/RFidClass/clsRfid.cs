using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ToolsManage.BaseClass;
using ToolsManage.BaseClass.RFidClass;

using ReaderB;

namespace ToolsManage
{
    public class clsRfid
    {
        //public TimerHelper timerRead = new TimerHelper(50, false);
        public List<clsReadEpc> listEpc = new List<clsReadEpc>();


        #region RFID读写器 变量

        private byte fBaud=5;
        private int iCom = 4;

        /// <summary>
        /// 读写器地址
        /// </summary>
        private byte fComAdr = 0xff;
        /// <summary>
        /// 输出变量，返回与读写器连接端口对应的句柄，应用程序通过该句柄可以操作连接在相应端口的读写器。如果打开不成功，返回的句柄值为-1.
        /// </summary>
        private int frmcomportindex;
        /// <summary>
        /// 打开的串口索引号
        /// </summary>
        private int fOpenComIndex;


        #endregion

        public clsRfid(string ip,int port,string name)
        {
            strIp = ip;
            intPort = port;
            strName = name;
            //timerRead .Execute +=new TimerHelper.TimerExecution(timerRead_Execute);
        }


        #region  字段

        private int intPort = 27011;
        private string strIp;
        private bool blConnent = false;
        private bool blScan = false;
        private int iReadEpcErr = 0;
        private int iLinkErr = 0;
        private DateTime timeLinkErr;
        CommuniState stateNet = CommuniState.初值;
        private string strName;

        private DeviceUsing usingReadNo;

        /// <summary>
        /// 是否 顺序扫描
        /// </summary>
        public DeviceUsing UsingReadNo
        {
            get { return usingReadNo; }
            set { usingReadNo = value; }
        }
        //private DateTime timeLinkLast = DateTime.Now.AddHours(-1);

        ///// <summary>
        ///// 上次 连接时间
        ///// </summary>
        //public DateTime TimeLinkLast
        //{
        //    get { return timeLinkLast; }
        //    set { timeLinkLast = value; }
        //}

        public string StrName
        {
            get { return strName; }
            set { strName = value; }
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

        public int IntPort
        {
            get { return intPort; }
            set { intPort = value; }
        }

        public string StrIp
        {
            get { return strIp; }
            set { strIp = value; }
        }

        public bool BlConnent
        {
            get { return blConnent; }
            set { blConnent = value; }
        }

        #endregion

        #region  子程序

        //public void timeStart()
        //{
        //    timerRead.Start();
        //}

        //public void timeStop()
        //{
        //    timerRead.Stop();
        //}

        public bool SetAnt(DeviceUsing ant1, DeviceUsing ant2, DeviceUsing ant3, DeviceUsing ant4)
        {
            bool blRet = false;
            try
            {
                byte ANT = 0;
                if (ant1 == DeviceUsing.启用)
                    ANT = Convert.ToByte(ANT | 1);
                if (ant2 == DeviceUsing.启用)
                    ANT = Convert.ToByte(ANT | 2);
                if (ant3 == DeviceUsing.启用)
                    ANT = Convert.ToByte(ANT | 4);
                if (ant4 == DeviceUsing.启用)
                    ANT = Convert.ToByte(ANT | 8);
                int iRet = StaticClassReaderB.SetAntennaMultiplexing(ref fComAdr, ANT, frmcomportindex);
                if (iRet == 0)
                    blRet = true;
                else
                    blRet = false;
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
            return blRet;
        }

        public bool OpenNetPort()
        {
            bool blRet = false;
            try
            {
                int iRet = StaticClassReaderB.AutoOpenComPort(ref iCom, ref fComAdr, fBaud,ref  frmcomportindex);

                fOpenComIndex = frmcomportindex;

                if (iRet == 0)
                {
                    blRet = true;
                }
                if ((iRet == 0x35) || (iRet == 0x30))
                {
                    //StaticClassReaderB.CloseNetPort(frmcomportindex);
                    StaticClassReaderB.CloseSpecComPort(iCom);
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

        public void CloseNetPort()
        {
            try
            {
                //StaticClassReaderB.CloseNetPort(frmcomportindex);
                StaticClassReaderB.CloseSpecComPort(iCom);
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        /// <summary>
        /// 连接 读写器  更新通信状态
        /// </summary>
        public void OpenRfidNewState()
        {
            if (stateNet == CommuniState.错误)
            {
                TimeSpan span = DateTime.Now - timeLinkErr;
                if (span.TotalMinutes < 1)
                {
                    return;
                }
            }
            bool blRet = OpenNetPort();
            if (blRet)
            {
                blConnent = true;
                iLinkErr = 0;
                if (stateNet != CommuniState.正常)
                {
                    stateNet = CommuniState.正常;
                    //通信恢复正常
                }
            }
            else
            {
                iLinkErr++;
            }
            if (iLinkErr > 3)
            {
                if (stateNet != CommuniState.错误)
                {
                    stateNet = CommuniState.错误;
                    //通信异常
                }
                timeLinkErr = DateTime.Now;
            }
        }

        public bool ReadEpc()
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

                int fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, bEpcArray, ref Ant, ref EpcLen, ref CardCount, frmcomportindex);// 8616
                if ((fCmdRet == 1) || (fCmdRet == 2) || (fCmdRet == 3) || (fCmdRet == 4) || (fCmdRet == 0xFB))//代表已查找结束，
                {
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
                        for (int iIndex=0; iIndex < listEpc.Count; iIndex++)
                        {
                            if (listEpc[iIndex].StrEpc == strEpc)
                            {
                                #region  顺序读取

                                if (usingReadNo == DeviceUsing.启用)
                                {
                                    if (listEpc[iIndex].IntAnt != Ant)
                                    {
                                        //归还
                                        if (((listEpc[iIndex].IntAnt & 0x01) == 0x01 || (listEpc[iIndex].IntAnt & 0x02) == 0x02))
                                        //if (((listEpc[iIndex].IntAnt & 0x01) == 0x01 || (listEpc[iIndex].IntAnt & 0x02) == 0x02) &&
                                        //    ((listEpc[iIndex].IntAnt & 0x04) != 0x04 && (listEpc[iIndex].IntAnt & 0x08) != 0x08))
                                        {
                                            if ((Ant & 0x4) == 0x04 || (Ant & 0x8) == 0x08)
                                            {
                                                //TimeSpan span = DateTime.Now - listEpc[iIndex].TimeLastRead;
                                                //if (span.TotalSeconds < 5)
                                                //{
                                                //    lock (listEpc)
                                                //    {
                                                //        listEpc[iIndex].BorrOrRet = ToolsBorrRet.归还;
                                                //    }
                                                //}

                                                lock (listEpc)
                                                {
                                                    listEpc[iIndex].BorrOrRet = ToolsBorrRet.归还;
                                                }
                                            }
                                        }
                                        else if (((listEpc[iIndex].IntAnt & 0x04) == 0x04 || (listEpc[iIndex].IntAnt & 0x08) == 0x08))
                                        //else if (((listEpc[iIndex].IntAnt & 0x04) == 0x04 || (listEpc[iIndex].IntAnt & 0x08) == 0x08) &&
                                        //  ((listEpc[iIndex].IntAnt & 0x01) != 0x01 && (listEpc[iIndex].IntAnt & 0x02) != 0x02))
                                        {
                                            if ((Ant & 0x01) == 0x01 || (Ant & 0x02) == 0x02)
                                            {
                                                //TimeSpan span = DateTime.Now - listEpc[iIndex].TimeLastRead;
                                                //if (span.TotalSeconds < 5)
                                                //{
                                                //    lock (listEpc)
                                                //    {
                                                //        listEpc[iIndex].BorrOrRet = ToolsBorrRet.借出;
                                                //    }
                                                //}
                                                lock (listEpc)
                                                {
                                                    listEpc[iIndex].BorrOrRet = ToolsBorrRet.借出;
                                                }
                                            }
                                        }

                                        lock (listEpc)
                                        {
                                            listEpc[iIndex].IntAnt = Ant;//| listEpc[iIndex].IntAnt
                                        }

                                    
                                        //lock (listEpc)
                                        //{
                                        //    listEpc[iIndex].TimeLastRead = DateTime.Now;
                                        //}

                                    }
                                    if (listEpc[iIndex].UsingReadNo != DeviceUsing.启用)
                                    {
                                        lock (listEpc)
                                        {
                                            listEpc[iIndex].UsingReadNo = DeviceUsing.启用;
                                        }
                                    }
                                }

                                #endregion

                                #region  常规读取

                                //else
                                //{
                                //    lock (listEpc)
                                //    {
                                //        listEpc[iIndex].TimeLastRead = DateTime.Now;
                                //    }
                                //}

                                #endregion

                                blHas = true;
                                break;
                            }
                        }
                        if (blHas==false)
                        {
                            clsReadEpc clsEpc = new clsReadEpc(strEpc, Ant);
                            clsEpc.TimeLastRead = DateTime.Now;
                            if (usingReadNo == DeviceUsing.启用)
                            {
                                clsEpc.UsingReadNo = DeviceUsing.启用;
                            }
                            clsEpc.BorrOrRet = ToolsBorrRet.未知;
                            clsEpc.IsEpcRead = IsReadShow.未读;
         
                            lock (listEpc)
                            {
                                listEpc.Add(clsEpc);
                            }
                        }
                    }
                }
                else
                {
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

        #endregion

        //private void timerRead_Execute()
        //{
        //    try
        //    {
        //        if (blConnent == false)
        //        {
        //            OpenRfidNewState();
        //        }
        //        else
        //        {
        //            if (blScan)
        //                return;
        //            if (blConnent)
        //            {
        //                bool blRet = ReadEpc();
        //                if (blRet)
        //                    iReadEpcErr = 0;
        //                else
        //                    iReadEpcErr++;
        //                if (iReadEpcErr++ > 3)
        //                {
        //                    CloseNetPort();
        //                    blConnent = false;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (frmMain.blDebug)
        //            MessageUtil.ShowTips(ex.Message);
        //    } 
        //}

    }
}

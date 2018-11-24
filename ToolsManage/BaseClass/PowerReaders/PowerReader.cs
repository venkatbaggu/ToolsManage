using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ToolsManage.UDP;
using ToolsManage.BaseClass.DoorClass;
using ToolsManage.ToolsManage;
using ToolsManage.BaseClass.OtherClass;
using ToolsManage.BaseClass.SeralClass;

namespace ToolsManage.BaseClass.PowerReaders
{
    public class PowerReader
    {
        private clsCommon commonCls = new clsCommon();
        public clsSerialVoice serialVoice = new clsSerialVoice();//IO板
        private TimerHelper timer = new TimerHelper(10000, false);
        AppConfig config = new AppConfig();

        UDPClientManager manager;

        #region 半有源相关变量

        private DateTime timeReceHeat = DateTime.Now;
        private bool blOnLine = true;
        private bool blRead = true;

        /// <summary>
        /// 读写器目标端口
        /// </summary>
        private string strReaderToPort = "32500";
        private string strOutId1 = "";
        private string strOutId2 = "";
        private string strOutId3 = "";
        private string strOutId4 = "";
        private string strInId1 = "";
        private string strInId2 = "";
        private string strInId3 = "";
        private string strInId4 = "";
        private List<string> listMarkId = new List<string>();

        #endregion

        #region  事件接口

        /// <summary>
        /// 新事件显示 事件
        /// </summary>
        public event NewEventShowEventHandler NewEventShowEvent;

        /// <summary>
        /// 新告警显示 事件
        /// </summary>
        public event NewEventShowEventHandler NewAlarmShowEvent;

        #endregion


        public PowerReader()
        {
            timer.Execute += new TimerHelper.TimerExecution(timer_Execute);
            timer.Start();

            LoadSet();
            ConnectReader();
        }

        private void timer_Execute()
        {
            try
            {
                TimeSpan span = DateTime.Now - timeReceHeat;
                if (span.TotalSeconds > 60)
                {
                    timeReceHeat = DateTime.Now;
                    if (blOnLine)
                    {
                        blOnLine = false ;
                        string strContent = "半有源读写器网络异常";
                        commonCls.NewErrRecord(AlarmsType.半有源读写器通信.ToString(), "", strContent, "");
                        if (NewAlarmShowEvent != null)
                        {
                            NewAlarmShowEvent(new NewEventEventArgs(EventType.半有源读写器通信, "", strContent, "", "", DateTime.Now));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        #region 子程序

        /// <summary>
        /// 加载半有源相关设置
        /// </summary>
        public void LoadSet()
        {
            strReaderToPort = config.AppConfigGet("PowReaderToPort");
            strOutId1 = config.AppConfigGet("OutId1");
            strOutId2 = config.AppConfigGet("OutId2");
            strOutId3 = config.AppConfigGet("OutId3");
            strOutId4 = config.AppConfigGet("OutId4");
            strInId1 = config.AppConfigGet("InId1");
            strInId2 = config.AppConfigGet("InId2");
            strInId3 = config.AppConfigGet("InId3");
            strInId4 = config.AppConfigGet("InId4");

            string strMarkId = config.AppConfigGet("MarkToolId");
            if (listMarkId.Count > 0)
                listMarkId.Clear();
            string[] strIdS = strMarkId.Split('+');
            foreach (string item in strIdS)
            {
                if (!string.IsNullOrEmpty(item))
                    listMarkId.Add(item);
            }
        }

        /// <summary>
        /// 连接读写器
        /// </summary>
        public void ConnectReader()
        {
            if (UDPClientManager.ClientExist(strReaderToPort))
            {
                //if (frmMain.blDebug)
                //    MessageUtil.ShowTips("半有源读写器，客户端已存在");

                if (manager == null)
                    manager = new UDPClientManager(strReaderToPort);
                manager.Start(int.Parse(strReaderToPort));  //开启端口监听
            }
            else
            {
                if (manager == null)
                {
                    manager = new UDPClientManager(strReaderToPort);
                    manager.Start(int.Parse(strReaderToPort));  //开启端口监听
                    manager.UDPMessageReceived += new UDPMessageReceivedEventHandler(manager_UDPMessageReceived);
                }
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void StopRece()
        {
            if (manager != null)
            {
                manager.Stop();
            }
        }

        ///// <summary>
        ///// 断开连接
        ///// </summary>
        //public void DisconnectReader()
        //{
        //    if (manager != null)
        //    {
        //        manager.UDPMessageReceived -= new UDPMessageReceivedEventHandler(manager_UDPMessageReceived);
        //        manager.Stop (); 
              
        //    }
        //}

        #endregion



        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="csID"></param>
        /// <param name="args"></param>
        void manager_UDPMessageReceived(string csID, UDPMessageReceivedEventArgs args)
        {
            try
            {
                if (args.Data.Length >= 12 && blRead)
                {

                    byte b = args.Data[4];
                    int iLength = args.Data[5] + b * 256;
                    byte bCheak = 0;
                    for (int i = 0; i < iLength - 1; i++)
                    {
                        bCheak += args.Data[i];
                    }
                    if (args.Data[0] == 0x02 && args.Data[1] == 0x03 && args.Data[2] == 0x04 && args.Data[3] == 0x05 &&
                        args.Data.Length == iLength && args.Data[iLength - 1] == bCheak)
                    {
                        byte bType = args.Data[8];
                        switch (bType)
                        {
                            case 0x41://收到标签

                                #region  收到标签

                                //txt 接受
                                txtLog.TxtWriteByte("PowerReader.txt", "Rece", args.Data, true);

                                byte bCount = args.Data[10];

                                for (b = 0; b < bCount; b++)
                                {
                                    int iCount = b * 7;
                                    int iCode = args.Data[11 + iCount] * 65536 + args.Data[12 + iCount] * 256 + args.Data[13 + iCount];
                                    string strCode = iCode.ToString();
                                    // 标签的状态 包括电池电压等
                                    byte bRfidState = args.Data[14 + iCount];

                                    #region   结合 激活器 ID判断 进出库

                                    //激活器ID
                                    int iId = args.Data[15 + iCount]*256 + args.Data[16 + iCount];
                                    string strActiveIdNow = iId.ToString();

                                    for (int iToos = 0; iToos < MainControl.listTools.Count; iToos++)
                                    {
                                        if (MainControl.listTools[iToos].StrRfidCode == strCode)
                                        {
                                            string strId = MainControl.listTools[iToos].StrToolId;
                                            if ((bRfidState & 0x40) == 0x40)
                                            {
                                                //电压低
                                                string strContent = "电子标签电压低，请及时更换电池";
                                                string sTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                //string strPeople = "";

                                                commonCls.NewErrRecord(AlarmsType.标签欠压.ToString(), strId, strContent, "");
                                                if (NewAlarmShowEvent != null)
                                                {
                                                    NewAlarmShowEvent(new NewEventEventArgs(EventType.标签欠压, strId, strContent, "", "", DateTime.Now));
                                                }
                                            }
                                            //盘库
                                            if (frmToolMark.blMark)
                                            {
                                                bool blMark = false;
                                                foreach (string markId in listMarkId)
                                                {
                                                    if (strActiveIdNow == markId)
                                                    {
                                                        blMark = true;
                                                        break;
                                                    }
                                                }
                                                if (blMark)
                                                {
                                                    if (!MainControl.listMark.Contains(strCode))
                                                    {
                                                        lock (MainControl.listMark)
                                                        {
                                                            MainControl.listMark.Add(strCode);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                bool blMark = false;
                                                foreach (string markId in listMarkId)
                                                {
                                                    if (strActiveIdNow == markId)
                                                    {
                                                        blMark = true;
                                                        break;
                                                    }
                                                }
                                                if (blMark)
                                                {
                                                    serialVoice.OnOffMark(OnOffMark .关闭);
                                                }


                                                //借出归还的时间
                                                DateTime timeLast = MainControl.listTools[iToos].TimeBorrRet;
                                                TimeSpan timeSpan = DateTime.Now - timeLast;
                                                {
                                                    if (timeSpan.TotalSeconds >= infoOfSystem.iBorrRetSpan)
                                                    {
                                                        #region    以一个时间段 判断工具借还

                                                        ToolsBorrRet toolActBorrRet = ToolsBorrRet.未知;
                                                        if (MainControl.listTools[iToos].StrLastActId == strInId1 || MainControl.listTools[iToos].StrLastActId == strInId2
                                                         || MainControl.listTools[iToos].StrLastActId == strInId3 || MainControl.listTools[iToos].StrLastActId == strInId4)
                                                        {
                                                            if (strActiveIdNow == strOutId1 || strActiveIdNow == strOutId2 || strActiveIdNow == strOutId3 || strActiveIdNow == strOutId4)//借出
                                                            {
                                                                toolActBorrRet = ToolsBorrRet.借出;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (MainControl.listTools[iToos].StrLastActId == strOutId1 || MainControl.listTools[iToos].StrLastActId == strOutId2
                                                             || MainControl.listTools[iToos].StrLastActId == strOutId3 || MainControl.listTools[iToos].StrLastActId == strOutId4)
                                                            {
                                                                if (strActiveIdNow == strInId1 || strActiveIdNow == strInId2 || strActiveIdNow == strInId3 || strActiveIdNow == strInId4)
                                                                {
                                                                    toolActBorrRet = ToolsBorrRet.归还;
                                                                }
                                                            }
                                                        }
                                                        MainControl.listTools[iToos].StrLastActId = strActiveIdNow;
                                                        string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                        string strType = MainControl.listTools[iToos].StrToolType;
                                                        string strName = MainControl.listTools[iToos].StrToolName;

                                                        #region  借出归还错误 即 在库归还 不在库借出

                                                        if (MainControl.listTools[iToos].ToolState == ToolsState.在库 && toolActBorrRet == ToolsBorrRet.归还)
                                                        {
                                                            string strContent = "工具在库归还";

                                                            commonCls.NewErrRecord( AlarmsType.借还错误.ToString(), strId, strContent, "");
                                                            if (NewAlarmShowEvent != null)
                                                            {
                                                                NewAlarmShowEvent(new NewEventEventArgs(EventType.借还错误, strId, strContent, "", "", DateTime.Now));
                                                            }
                                                        }
                                                        else if ((MainControl.listTools[iToos].ToolState == ToolsState.借出 || MainControl.listTools[iToos].ToolState == ToolsState.外借超时) 
                                                              && toolActBorrRet == ToolsBorrRet.借出)
                                                        {
                                                            string strContent = "工具不在库借出";

                                                            commonCls.NewErrRecord(AlarmsType.借还错误.ToString(), strId, strContent, "");
                                                            if (NewAlarmShowEvent != null)
                                                            {
                                                                NewAlarmShowEvent(new NewEventEventArgs(EventType.借还错误, strId, strContent, "", "", DateTime.Now));
                                                            }
                                                        }

                                                        #endregion

                                                        if (MainControl.listTools[iToos].ToolState == ToolsState.在库 && toolActBorrRet == ToolsBorrRet.借出)
                                                        {
                                                            ToolRorrRetRecord(ToolsState.借出, iToos, strTime, strCode, strType, strName, strId);
                                                            lock (MainControl.listTools)
                                                            {
                                                                MainControl.listTools[iToos].TimeBorrRet = DateTime.Now;
                                                                MainControl.listTools[iToos].StrLastActId = "";
                                                            }
                                                        
                                                        }
                                                        else
                                                        {
                                                            if ((MainControl.listTools[iToos].ToolState == ToolsState.借出 || MainControl.listTools[iToos].ToolState == ToolsState.外借超时) && toolActBorrRet == ToolsBorrRet.归还)
                                                            {
                                                                ToolRorrRetRecord(ToolsState.在库, iToos, strTime, strCode, strType, strName, strId);
                                                                lock (MainControl.listTools)
                                                                {
                                                                    MainControl.listTools[iToos].TimeBorrRet = DateTime.Now;
                                                                    MainControl.listTools[iToos].StrLastActId = "";
                                                                }
                                                           
                                                            }
                                                        }

                                                        #endregion

                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    }

                                    #endregion

                                }

                                #endregion

                                break;

                            case 0x42://心跳
                                if (blOnLine == false)
                                {
                                    blOnLine = true;
                                    string strContent = "半有源读写器连接恢复正常";
                                    commonCls.NewErrRecord(AlarmsType.半有源读写器通信.ToString(), "", strContent, "");
                                    if (NewAlarmShowEvent != null)
                                    {
                                        NewAlarmShowEvent(new NewEventEventArgs(EventType.半有源读写器通信, "", strContent, "", "", DateTime.Now));
                                    }
                                }

                                //txt 接受
                                txtLog.TxtWriteByte("PowerReader.txt", "Heat Rece", args.Data, true);
                                break;

                            default:
                                break;

                        }
                        //收到数据即认为在线
                        timeReceHeat = DateTime.Now;
                        if (blOnLine == false)
                        {
                            blOnLine = true;
                            string strContent = "半有源读写器连接恢复正常";
                            commonCls.NewErrRecord(AlarmsType.半有源读写器通信.ToString(), "", strContent, "");
                            if (NewAlarmShowEvent != null)
                            {
                                NewAlarmShowEvent(new NewEventEventArgs(EventType.半有源读写器通信, "", strContent, "", "", DateTime.Now));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }


        string strOpenUser = "";
        private void ToolRorrRetRecord(ToolsState stateOfTool, int iIndex, string strTime, string strEpc, string strToolType, string strToolName, string strToolID)
        {
            //ToolsState stateOfTool = ToolsState.借出;
            lock (MainControl.listTools)
            {
                MainControl.listTools[iIndex].ToolState = stateOfTool;
            }
            // 记录 和 事件显示
            commonCls.NewToolBorrowRet(stateOfTool, strTime, strEpc, strToolType, strToolName, strToolID, strOpenUser);
            if (NewEventShowEvent != null)
            {
                string strContent = "";
                if (stateOfTool == ToolsState.借出)
                    strContent = stateOfTool.ToString();
                else if (stateOfTool == ToolsState.在库)
                    strContent = EventContent.归还.ToString();
                NewEventShowEvent(new NewEventEventArgs(EventType.工具借还, strToolID, strContent, strOpenUser, "", DateTime.Now));
            }

        }

    }
}

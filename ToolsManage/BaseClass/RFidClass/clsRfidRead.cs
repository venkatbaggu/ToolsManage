using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Data;

using ToolsManage.BaseClass.DoorClass;

namespace ToolsManage.BaseClass.RFidClass
{
    public class clsRfidRead
    {
        DataLogic datalogic = new DataLogic();
        //public TimerHelper timerRead = new TimerHelper(20, false);
        public TimerHelperForm timerRead = new TimerHelperForm(20, false);
        private clsCommon commonCls = new clsCommon();
        public List<clsRfid> listRfid = new List<clsRfid>();
        public static string strOpenUser = "";
        //public static string strOpenGroup = "";

        ///// <summary>
        ///// 类似 看门狗定时器
        ///// </summary>
        //public TimerHelper timerDog = new TimerHelper(1000, false);
        ///// <summary>
        ///// 看门狗 计数 RFID定时器中清零 看门狗定时器累加 超30S 成立
        ///// </summary>
        //private DateTime timeDog = DateTime.Now;

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

        public clsRfidRead()
        {
            //timerRead.Execute += new TimerHelper.TimerExecution(timerRead_Execute);
            timerRead.Execute += new TimerHelperForm.TimerExecution(timerRead_Execute);
            //timerDog.Execute +=new TimerHelper.TimerExecution(timerDog_Execute);
        }

        #region 方法

        public void timerStart()
        {
            timerRead.Start();
            //timerDog.Start();
        }

        public void timerStop()
        {
            timerRead.Stop();
            //timerDog.Stop();
        }

        public void ClearListRfid()
        {
            if (timerRead.State == TimerState.Running)
            {
                timerStop();
                Thread.Sleep(30);
            }
            for (int iIndex = 0; iIndex < listRfid.Count; iIndex++)
            {
                if (listRfid[iIndex].BlConnent)
                {
                    listRfid[iIndex].CloseNetPort();
                }
            }
            if (listRfid.Count > 0)
                listRfid.Clear();
        }

        public void ClearAndDispose()
        {
            if (listRfid.Count > 0)
            {
                ClearListRfid();
            }
            //timerRead.Execute -= new TimerHelper.TimerExecution(timerRead_Execute);
            timerRead.Execute -= new TimerHelperForm.TimerExecution(timerRead_Execute);
            //timerDog.Execute -= new TimerHelper.TimerExecution(timerDog_Execute);
            timerRead = null;
            //timerDog = null;
        }


        #endregion

        #region RFID子程序

        /// <summary>
        /// 连接 读写器  更新通信状态
        /// </summary>
        private void OpenRfidNewState(int iIndex)
        {
            if (listRfid[iIndex].StateNet == CommuniState.错误)
            {
                TimeSpan span = DateTime.Now - listRfid[iIndex].TimeLinkErr;
                if (span.TotalMinutes < 1)
                {
                    return;
                }
            }
            if (frmMain.stateRstRfid == StateRstRfid.关闭 || frmMain.stateRstRfid == StateRstRfid.重启)
            {
                return;
            }

            bool blRet = listRfid[iIndex].OpenNetPort();
            if (blRet)
            {
                listRfid[iIndex].BlConnent = true;
                listRfid[iIndex].ILinkErr = 0;
                if (listRfid[iIndex].StateNet == CommuniState.错误)
                {
                    //通信恢复正常
                    string strName = listRfid[iIndex].StrName;
                    string strContent = ErrorContent.通信恢复正常.ToString();
                    commonCls.NewErrRecord(EventType.RFID读写器.ToString(), strName, strContent, "");
                    if (NewAlarmShowEvent != null)
                    {
                        NewAlarmShowEvent(new NewEventEventArgs(EventType.RFID读写器, strName, strContent, "", "", DateTime.Now));

                    }
                }
                if (listRfid[iIndex].StateNet != CommuniState.正常)
                {
                    listRfid[iIndex].StateNet = CommuniState.正常;
                }
            }
            else
            {
                listRfid[iIndex].ILinkErr++;
            }
            if (listRfid[iIndex].ILinkErr >= 2)
            {
                listRfid[iIndex].ILinkErr = 0;
                if (listRfid[iIndex].StateNet == CommuniState.正常 || listRfid[iIndex].StateNet == CommuniState.初值)
                {
                    //异常记录
                    string strName = listRfid[iIndex].StrName;
                    string strContent = ErrorContent.通信异常.ToString();
                    commonCls.NewErrRecord(EventType.RFID读写器.ToString(), strName, strContent, "");
                    if (NewAlarmShowEvent != null)
                    {
                        NewAlarmShowEvent(new NewEventEventArgs(EventType.RFID读写器, strName, strContent, "", "", DateTime.Now));
                    }
                }
                else if (listRfid[iIndex].StateNet == CommuniState.错误 )
                {
                    commonCls.NewErrRecord(EventType.系统.ToString(), "", ErrorContent.RFID读写器异常系统重启 .ToString (), "");
                    timerRead.Stop();

                    Thread.Sleep(1000);
                    frmMain.RFIDReLoad = ErrorContent.RFID读写器异常系统重启;
                }
                if (listRfid[iIndex].StateNet != CommuniState.错误)
                {
                    listRfid[iIndex].StateNet = CommuniState.错误;
                }
                listRfid[iIndex].TimeLinkErr = DateTime.Now;
            }
        }

        /// <summary>
        ///  借出或归还
        /// </summary>
        private void ToolBorrowOrRet(int iIndex, int iIndexEpc)
        {

            int iCount = MainControl.listTools.Count;
            for (int ilistTool = 0; ilistTool < iCount; ilistTool++)
            {
                if (MainControl.listTools[ilistTool].StrRfidCode == listRfid[iIndex].listEpc[iIndexEpc].StrEpc)//dtTools ToolID  RfidCoding ToolBR TimeBR 
                {
                    DateTime timeLast = MainControl.listTools[ilistTool].TimeBorrRet;
                    TimeSpan timeSpan = DateTime.Now - timeLast;
                    if (timeSpan.TotalSeconds >= infoOfSystem.iBorrRetSpan)
                    {
                        string strToolType = MainControl.listTools[ilistTool].StrToolType;
                        string strToolName = MainControl.listTools[ilistTool].StrToolName;
                        string strToolID = MainControl.listTools[ilistTool].StrToolId;
                        string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                        if (MainControl.listTools[ilistTool].ToolState == ToolsState.在库)
                        {
                            if (listRfid[iIndex].listEpc[iIndexEpc].UsingReadNo == DeviceUsing.启用)
                            {
                                if (listRfid[iIndex].listEpc[iIndexEpc].BorrOrRet == ToolsBorrRet.借出)
                                {
                                    ToolRorrRetRecord(ToolsState.借出, ilistTool, strTime, listRfid[iIndex].listEpc[iIndexEpc].StrEpc, strToolType, strToolName, strToolID);
                                    lock (listRfid)
                                    {
                                        listRfid[iIndex].listEpc[iIndexEpc].IsEpcRead = IsReadShow.已读;
                                    }
                                    lock (MainControl.listTools)
                                    {
                                        MainControl.listTools[ilistTool].TimeBorrRet = DateTime.Now;
                                    }
                                }
                            }
                            else
                            {
                                ToolRorrRetRecord(ToolsState.借出, ilistTool, strTime, listRfid[iIndex].listEpc[iIndexEpc].StrEpc, strToolType, strToolName, strToolID);
                                lock (listRfid)
                                {
                                    listRfid[iIndex].listEpc[iIndexEpc].IsEpcRead = IsReadShow.已读;
                                }
                                lock (MainControl.listTools)
                                {
                                    MainControl.listTools[ilistTool].TimeBorrRet = DateTime.Now;
                                }
                            }
                        }
                        else if (MainControl.listTools[ilistTool].ToolState == ToolsState.借出)
                        {
                            if (listRfid[iIndex].listEpc[iIndexEpc].UsingReadNo == DeviceUsing.启用)
                            {
                                if (listRfid[iIndex].listEpc[iIndexEpc].BorrOrRet == ToolsBorrRet.归还)
                                {
                                    ToolRorrRetRecord(ToolsState.在库, ilistTool, strTime, listRfid[iIndex].listEpc[iIndexEpc].StrEpc, strToolType, strToolName, strToolID);
                                    lock (listRfid)
                                    {
                                        listRfid[iIndex].listEpc[iIndexEpc].IsEpcRead = IsReadShow.已读;
                                    }
                                    lock (MainControl.listTools)
                                    {
                                        MainControl.listTools[ilistTool].TimeBorrRet = DateTime.Now;
                                    }
                                }
                            }
                            else
                            {
                                ToolRorrRetRecord(ToolsState.在库, ilistTool, strTime, listRfid[iIndex].listEpc[iIndexEpc].StrEpc, strToolType, strToolName, strToolID);
                                lock (listRfid)
                                {
                                    listRfid[iIndex].listEpc[iIndexEpc].IsEpcRead = IsReadShow.已读;
                                }
                                lock (MainControl.listTools)
                                {
                                    MainControl.listTools[ilistTool].TimeBorrRet = DateTime.Now;
                                }
                            }
                        }


                      
                    }
                    break;
                }
            }
        }

        private void ToolRorrRetRecord( ToolsState stateOfTool,int iIndex, string strTime, string strEpc, string strToolType, string strToolName, string strToolID)
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
                string strContent="";
                if (stateOfTool == ToolsState.借出)
                    strContent = stateOfTool.ToString();
                else if (stateOfTool == ToolsState.在库 )
                    strContent = EventContent.归还.ToString();
                NewEventShowEvent(new NewEventEventArgs(EventType.工具借还, strToolID, strContent, strOpenUser, "", DateTime.Now));
            }
        
        }

        ///// <summary>
        /////  借出或归还  20170916备份
        ///// </summary>
        //private void ToolBorrowOrRet(string strEpc)
        //{

        //    int iCount = MainControl.listTools.Count;
        //    for (int iIndex = 0; iIndex < iCount; iIndex++)
        //    {
        //        if (MainControl.listTools[iIndex].StrRfidCode == strEpc)//dtTools ToolID  RfidCoding ToolBR TimeBR 
        //        {
        //            DateTime timeLast = MainControl.listTools[iIndex].TimeBorrRet;
        //            TimeSpan timeSpan = DateTime.Now - timeLast;
        //            if (timeSpan.TotalSeconds >= infoOfSystem.iBorrRetSpan)
        //            {
        //                string strToolType = MainControl.listTools[iIndex].StrToolType;
        //                string strToolName = MainControl.listTools[iIndex].StrToolName;
        //                string strToolID = MainControl.listTools[iIndex].StrToolId;
        //                string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                if (MainControl.listTools[iIndex].ToolState == ToolsState.在库)
        //                {
        //                    ToolsState stateOfTool = ToolsState.借出;
        //                    lock (MainControl.listTools)
        //                    {
        //                        MainControl.listTools[iIndex].ToolState = stateOfTool;
        //                    }
        //                    // 记录 和 事件显示
        //                    commonCls.NewToolBorrowRet(stateOfTool, strTime, strEpc, strToolType, strToolName, strToolID, strOpenUser);
        //                    if (NewEventShowEvent != null)
        //                    {
        //                        NewEventShowEvent(new NewEventEventArgs(EventType.工具借还, strToolID, stateOfTool.ToString(), strOpenUser, "", DateTime.Now));
        //                    }
        //                }
        //                else if (MainControl.listTools[iIndex].ToolState == ToolsState.借出)
        //                {
        //                    ToolsState stateOfTool = ToolsState.在库;
        //                    lock (MainControl.listTools)
        //                    {
        //                        MainControl.listTools[iIndex].ToolState = stateOfTool;
        //                    }
        //                    // 记录 和 事件显示
        //                    commonCls.NewToolBorrowRet(stateOfTool, strTime, strEpc, strToolType, strToolName, strToolID, strOpenUser);
        //                    if (NewEventShowEvent != null)
        //                    {
        //                        string strConcent = EventContent.归还.ToString();
        //                        NewEventShowEvent(new NewEventEventArgs(EventType.工具借还, strToolID, strConcent, strOpenUser, "", DateTime.Now));
        //                    }
        //                }
        //                lock (MainControl.listTools)
        //                {
        //                    MainControl.listTools[iIndex].TimeBorrRet = DateTime.Now;
        //                }
        //            }
        //            break;
        //        }
        //    }
        //}

        #endregion

        private void timerRead_Execute()
        {
            try
            {
                //timeDog = DateTime.Now;
                for (int iIndex = 0; iIndex < listRfid.Count; iIndex++)
                {
                    if (listRfid[iIndex].BlConnent == false)
                    {
                        OpenRfidNewState(iIndex);
                    }
                    else
                    {
                        if (listRfid[iIndex].BlScan)  
                            return;
                        if (listRfid[iIndex].BlConnent)
                        {
                            bool blRet = listRfid[iIndex].ReadEpc();
                            if (blRet)
                            {
                                listRfid[iIndex].IReadEpcErr = 0;
                                EpcBorrOrRet(iIndex);
                            }
                            else
                            {
                                listRfid[iIndex].IReadEpcErr++;
                            }
                            if (listRfid[iIndex].IReadEpcErr> 3)
                            {
                                listRfid[iIndex].CloseNetPort();
                                listRfid[iIndex].BlConnent = false;
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

        private void EpcBorrOrRet(int iIndex)
        {
            for (int iIndexEpc = 0; iIndexEpc < listRfid[iIndex].listEpc.Count; iIndexEpc++)
            {
                ToolBorrowOrRet(iIndex,iIndexEpc);

                //if (listRfid[iIndex].listEpc[iIndexEpc].IntAnt == 4 || listRfid[iIndex].listEpc[iIndexEpc].IntAnt == 8 || listRfid[iIndex].listEpc[iIndexEpc].IntAnt == 12)
                //{
                //    TimeSpan span = DateTime.Now - listRfid[iIndex].listEpc[iIndexEpc].TimeLastRead;
                //    if (span.TotalSeconds > 6)
                //    {
                //        lock (listRfid[iIndex].listEpc)
                //        {
                //            listRfid[iIndex].listEpc[iIndexEpc].IntAnt = 0;
                //        }
                //    }
                //}
       

                if (listRfid[iIndex].listEpc[iIndexEpc].IsEpcRead == IsReadShow.已读)
                {
                    lock (listRfid[iIndex].listEpc)
                    {
                        listRfid[iIndex].listEpc.RemoveAt(iIndexEpc);
                    }
                    EpcBorrOrRet(iIndex);
                    break;
                }
            }
        }

    }
}

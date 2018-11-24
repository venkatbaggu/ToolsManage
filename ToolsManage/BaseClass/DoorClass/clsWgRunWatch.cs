using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Threading;

using WG3000_COMM.Core;

using ToolsManage.BaseClass.RFidClass;

namespace ToolsManage.BaseClass.DoorClass
{
    /// <summary>
    /// 微耕 门禁运行 监控
    /// </summary>
    class clsWgRunWatch
    {
        private clsCommon commonCls = new clsCommon();
        private TimerHelper timerWg = new TimerHelper(1000, false);
        /// <summary>
        /// 监控 微耕 控制器 列表
        /// </summary>
        public  List<clsWgInfo> listWg = new List<clsWgInfo>();
        private wgMjController wgController = null; // 要监控的控制器 wgController watching
        private wgWatchingService watching; //实时监控服务
        private Queue QueRecText = new Queue();
        public  int dealingTxt = 0;
        //public  int infoRowsCount = 0;
        //public  int receivedPktCount = 0;

        public clsWgRunWatch()
        {
            timerWg.Execute += new TimerHelper.TimerExecution(timerWg_Execute);
        }

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

        #region   子程序

        /// <summary>
        /// 初始化 监控
        /// </summary>
        public void InitWatch()
        {
            if (listWg.Count <= 0)
            {
                return;
            }
            if (watching == null)
            {
                watching = new wgWatchingService();  //加载监视服务
                watching.EventHandler += new OnEventHandler(evtNewInfoCallBack); //事件处理
            }
            timerWg.Stop();  //停止刷新


            //选定要监控的控制表
            Dictionary<int, wgMjController> selectedControllers = new Dictionary<int, wgMjController>();

            int iCount = listWg.Count;
            for (int iIndex = 0; iIndex < iCount; iIndex++)
            {
                if (listWg[iIndex] != null)
                {
                    string strIp = listWg[iIndex].StrIp;
                    int iPort = listWg[iIndex].IntPort;
                    int iSn = (int)listWg[iIndex].IntSn;
                    if (!string.IsNullOrEmpty(strIp) && iSn != 0)
                    {
                        wgController = new wgMjController();
                        wgController.ControllerSN = iSn;
                        wgController.PORT = iPort;
                        wgController.IP = strIp;
                        selectedControllers.Add(wgController.ControllerSN, wgController);
                    }
                }
            }
            if (selectedControllers.Count > 0)
            {
                watching.WatchingController = selectedControllers;
                timerWg.Start();
            }
            else
            {
                watching.WatchingController = null;
                timerWg.Stop();
            }
        }

        /// <summary>
        /// 停止 实时查看 门状态
        /// </summary>
        public void StopTimer()
        {
            timerWg.Stop();
        }

        /// <summary>
        /// 停止后 恢复实时查看 门状态
        /// </summary>
        public void StartTimer()
        {
            timerWg.Start();
        }

        /// <summary>
        /// 清除 门禁控制器 list
        /// </summary>
        public void ClearWgList()
        {
            if (listWg.Count > 0)
            {
                for (int iIndex = 0; iIndex < listWg.Count; iIndex++)
                {
                    listWg[iIndex].Clear();
                }
                listWg.Clear();
            }
        }

        /// <summary>
        /// 清除所有 类
        /// </summary>
        public void DisposeAndClear()
        {
            try
            {
                if (timerWg != null)
                {
                    if (timerWg.State == TimerState.Running)
                    {
                        timerWg.Stop();
                        Thread.Sleep(1100);
                    }
                    timerWg = null;
                }
                if (commonCls != null)
                    commonCls = null;
                if (listWg.Count > 0)
                    listWg.Clear();// watching
                if (wgController != null)
                    wgController = null;
                if (watching != null)
                    watching = null;
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }

        #endregion

        #region  控制器 监控程序

        /// <summary>
        /// 控制器 新事件
        /// </summary>
        private void evtNewInfoCallBack(string text)
        {
            try
            {
                //receivedPktCount++;
                lock (QueRecText.SyncRoot)
                {
                    QueRecText.Enqueue(text);  //加入到文件中
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }

        /// <summary>
        /// 定时读取 门禁控制器状态
        /// </summary>
        private void updateControllerStatus()
        {
            try
            {
                if (watching != null)
                {
                    int iCount = listWg.Count;
                    for (int iIndex = 0; iIndex < iCount; iIndex++)
                    {
                        if (listWg[iIndex] != null)
                        {
                            // 门的类型 大门 或 工具柜门
                            WgDoorType doorOrBox = listWg[iIndex].DoorOrBoxDoor;

                            #region  网络通信 状态 及校时

                            //网络连接错误 30S 后继续连接
                            bool blAgain = false;
                            if (listWg[iIndex].StateOfNet == CommuniState.未连接)
                            {
                                TimeSpan span = DateTime.Now - listWg[iIndex].TimeErrNet;
                                if (span.TotalSeconds > 30)
                                {
                                    blAgain = true;
                                    lock (listWg)
                                    {
                                        listWg[iIndex].TimeErrNet = DateTime.Now;
                                    }
                                }
                            }

                            if (listWg[iIndex].StateOfNet == CommuniState.已连接 || blAgain)
                            {
                                wgMjControllerRunInformation conRunInfo = null;
                                int commStatus;
                                int iSn = (int)listWg[iIndex].IntSn;
                                commStatus = watching.CheckControllerCommStatus(iSn, ref conRunInfo);

                                if (commStatus == -1)
                                {
                                    if (listWg[iIndex].StateOfNet != CommuniState.未连接)
                                    {
                                        lock (listWg)
                                        {
                                            listWg[iIndex].StateOfNet = CommuniState.未连接;
                                            listWg[iIndex].TimeErrNet = DateTime.Now;
                                        }

                                        //通信 异常信息 及记录
                                        NetErrInfoAndRecord(doorOrBox, iIndex, ErrorContent.通信异常.ToString());

                                        #region

                                        //string strErrType = DoorOrBoxDoor.ToString();
                                        //string strWgName = "";//微耕控制器名称
                                        //if (DoorOrBoxDoor == EventType.工具柜)
                                        //{
                                        //    strWgName = listWg[iIndex].StrNameOfWg;
                                        //}
                                        //string strContent = ErrorContent.通信异常.ToString();
                                        ////异常记录
                                        //commonCls.NewErrRecord(strErrType, strWgName, strContent, "");
                                        //if (NewAlarmShowEvent != null)
                                        //{
                                        //    NewAlarmShowEvent(new NewEventEventArgs(DoorOrBoxDoor, strWgName, strContent, "", "", DateTime.Now));
                                        //}

                                        #endregion
                                    }
                                }
                                else if (commStatus == 1)
                                {
                                    if (listWg[iIndex].StateOfNet != CommuniState.已连接)
                                    {
                                        lock (listWg)
                                        {
                                            listWg[iIndex].StateOfNet = CommuniState.已连接;
                                        }

                                        //通信恢复 正常 记录及主界面显示
                                        NetErrInfoAndRecord(doorOrBox, iIndex, ErrorContent.通信恢复正常.ToString());

                                        #region  通信恢复 正常 记录及主界面显示

                                        //string strErrType = DoorOrBoxDoor.ToString();
                                        //string strWgName = "";//微耕控制器名称
                                        //if (DoorOrBoxDoor == EventType.工具柜)
                                        //{
                                        //    strWgName = listWg[iIndex].StrNameOfWg;
                                        //}
                                        //string strContent = ErrorContent.通信恢复正常.ToString();
                                        //commonCls.NewErrRecord(strErrType, strWgName, strContent, "");
                                        //if (NewAlarmShowEvent != null)
                                        //{
                                        //    NewAlarmShowEvent(new NewEventEventArgs(DoorOrBoxDoor, strWgName, strContent, "", "", DateTime.Now));
                                        //}

                                        #endregion

                                        #region  校时

                                        DateTime time = conRunInfo.dtNow;
                                        TimeSpan ts = DateTime.Now - time;//记录历史温湿度数据间隔
                                        if (ts.TotalSeconds >= 59 || ts.TotalSeconds <= -59)
                                        {
                                            wgController.AdjustTimeIP(DateTime.Now);
                                        }

                                        #endregion
                                    }



                                }
                            }

                            #endregion 

                            #region  门状态 

                            if (listWg[iIndex].StateOfNet == CommuniState.已连接)
                            {
                                if (listWg[iIndex].listDoor != null)
                                {
                                    string strArea = listWg[iIndex].StrArea;
                                    int iCountDoor = listWg[iIndex].listDoor.Count;
                                    for (int iIndexDoor = 0; iIndexDoor < iCountDoor; iIndexDoor++)
                                    {
                    
                                        //门名称
                                        string strDoorName = "";
                                        if (listWg[iIndex].listDoor[iIndexDoor].StrDoorName != null)
                                        {
                                            strDoorName = listWg[iIndex].listDoor[iIndexDoor].StrDoorName;
                                        }

                                        int iDoorIndex = listWg[iIndex].listDoor[iIndexDoor].IntDoorIndex;
                                        DoorsState getDoorState = listWg[iIndex].GetDoorState(iDoorIndex);
                                        if (listWg[iIndex].listDoor[iIndexDoor].StateOfDoor != getDoorState)
                                        {
                                            lock (listWg)
                                            {
                                                listWg[iIndex].listDoor[iIndexDoor].StateOfDoor = getDoorState;
                                            }
                                            if (getDoorState == DoorsState.开门)
                                            {
                                                lock (listWg)
                                                {
                                                    listWg[iIndex].listDoor[iIndexDoor].TimeOpenDoor = DateTime.Now;
                                                }
                                                if (listWg[iIndex].listDoor[iIndexDoor].IsOfRfid == DeviceUsing.启用)
                                                {
                                                    clsRfidRead.strOpenUser = listWg[iIndex].listDoor[iIndexDoor].StrUser;
                                                }
                                                //开门 
                                                string strOpenType = listWg[iIndex].listDoor[iIndexDoor].StrOpenType;
                                                string strGroup = listWg[iIndex].listDoor[iIndexDoor].StrGroup;
                                                string strUser = listWg[iIndex].listDoor[iIndexDoor].StrUser;
                                                OnOffDoorInfoRecord(doorOrBox, strDoorName, DoorsState.开门, strOpenType, strGroup, strUser, DateTime.Now, strArea);

                                                #region
                                                //string strOpenType = listWg[iIndex].listDoor[iIndexDoor].StrOpenType;
                                                //string strGroup = listWg[iIndex].listDoor[iIndexDoor].StrGroup;
                                                //string strUser = listWg[iIndex].listDoor[iIndexDoor].StrUser;
                                                //if (DoorOrBoxDoor == EventType.门禁)
                                                //{
                                                //    commonCls.NewDoorInOut(DoorsState.开门, strOpenType, strDoorName, strGroup, strUser, "");
                                                //}
                                                //else if (DoorOrBoxDoor == EventType.工具柜)
                                                //{

                                                //}
                                                //if (NewEventShowEvent != null)
                                                //{
                                                //    NewEventShowEvent(new NewEventEventArgs(DoorOrBoxDoor, strDoorName, DoorsState.开门.ToString(), strUser, strOpenType, DateTime.Now));
                                                //}
                                                #endregion
                                            }
                                            else if (getDoorState == DoorsState.关门)
                                            {
                                                lock (listWg)
                                                {
                                                    listWg[iIndex].listDoor[iIndexDoor].ClearOpenInfo();
                                                }
                                                if (listWg[iIndex].listDoor[iIndexDoor].IsOfRfid == DeviceUsing.启用)
                                                {
                                                    clsRfidRead.strOpenUser = "";
                                                }
                                                //关门 
                                                DateTime timeOpen = listWg[iIndex].listDoor[iIndexDoor].TimeOpenDoor;
                                                OnOffDoorInfoRecord(doorOrBox, strDoorName, DoorsState.关门, "", "", "", timeOpen, strArea);

                                                #region

                                                //TimeSpan timeSpan = DateTime.Now - listWg[iIndex].listDoor[iIndexDoor].TimeOpenDoor;
                                                //string strSpan = clsCommon.CalculateTime(timeSpan);
                                                //if (DoorOrBoxDoor == EventType.门禁)
                                                //{
                                                //    commonCls.NewDoorInOut(DoorsState.关门, "", "", "", "", strSpan);
                                                //}
                                                //else if (DoorOrBoxDoor == EventType.工具柜)
                                                //{

                                                //}
                                                //if (NewEventShowEvent != null)
                                                //{
                                                //    NewEventShowEvent(new NewEventEventArgs(DoorOrBoxDoor, strDoorName, DoorsState.开门.ToString(), "", "", DateTime.Now));
                                                //}

                                                #endregion
                                            }
                                  
                                        }
                                    }
                                }
                            }

                            #endregion
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

        /// <summary>
        /// 开关门 信息显示 和记录
        /// </summary>
        private void OnOffDoorInfoRecord( WgDoorType doorOrBox, string strDoorName, DoorsState state, string strOpenType, string strGroup, string strUser ,DateTime timeOpen,string strArea)
        {
            EventType eventType = EventType.门禁;
      
            if (doorOrBox == WgDoorType.门禁)
            {
                if (state ==DoorsState .开门 )
                {
                    commonCls.NewDoorInOut(state, strOpenType, strDoorName, strGroup, strUser, "");
                }
                else if (state == DoorsState.关门)
                {
                    TimeSpan timeSpan = DateTime.Now - timeOpen;//listWg[iIndex].listDoor[iIndexDoor].TimeOpenDoor;
                    string strSpan = clsCommon.CalculateTime(timeSpan);
                    commonCls.NewDoorInOut(state, "", "", "", "", strSpan);
                }
            }
            else if (doorOrBox == WgDoorType.工具柜)
            {
                eventType = EventType.工具柜 ;
                commonCls.BoxOnOffRecord(strArea, strDoorName, state,strOpenType, strGroup, strUser);
            }
            if (NewEventShowEvent != null)
            {
                NewEventShowEvent(new NewEventEventArgs(eventType, strDoorName, state.ToString(), strUser, strOpenType, DateTime.Now));
            }
        }

        /// <summary>
        /// 控制器 通信 异常 显示 和记录
        /// </summary>
        private void NetErrInfoAndRecord(WgDoorType doorType, int iIndex, string strContent)
        {
            EventType eventType = EventType.门禁;
            if (doorType == WgDoorType.工具柜)
            {
                eventType = EventType.工具柜;
            }
            string strWgName = listWg[iIndex].StrNameOfWg; ;//微耕控制器名称
            //异常记录
            commonCls.NewErrRecord(eventType.ToString (), strWgName, strContent, "");
            if (NewAlarmShowEvent != null)
            {
                NewAlarmShowEvent(new NewEventEventArgs(eventType, strWgName, strContent, "", "", DateTime.Now));
            }
        }


        /// <summary>
        /// 有新的运行信息
        /// </summary>
        public void txtInfoHaveNewInfoEntry()
        {
            try
            {
                #region

                if (dealingTxt > 0)
                    return;
                if (watching.WatchingController == null) //2010-8-1 08:27:15 没有选中监控的就退出
                    return;
                System.Threading.Interlocked.Exchange(ref dealingTxt, 1);
                int dealt = 0;
                object obj;
                long startTicks = DateTime.Now.Ticks; // Date.Now.Ticks  'us
                long updateSpan = 2000 * 1000 * 10;
                long endTicks = startTicks + updateSpan; // '100毫微秒的倍数  '一个Ticks＝10亿分之一秒，一毫秒＝10000Ticks
                while (QueRecText.Count > 0)
                {
                    lock (QueRecText.SyncRoot)
                    {
                        obj = QueRecText.Dequeue();//取出
                    }
                    txtInfoUpdateEntry(obj);
                    //infoRowsCount++;
                    dealt++;
                    if (DateTime.Now.Ticks > endTicks)
                    {
                        endTicks = DateTime.Now.Ticks + updateSpan;
                        if (watching.WatchingController == null)
                        {
                            break;
                        }
                    }
                }
                System.Threading.Interlocked.Exchange(ref dealingTxt, 0);

                #endregion
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        /// <summary>
        /// 解析 运行 信息
        /// </summary>
        private void txtInfoUpdateEntry(object info)
        {
            try
            {
                wgMjControllerSwipeRecord mjrec = new wgMjControllerSwipeRecord(info as string);
                if (mjrec.ControllerSN > 0)
                {
                    //如果不处于监控的控制器 则不作数据处理
                    int iSn = (int)mjrec.ControllerSN;
                    try
                    {
                        if (!watching.WatchingController.ContainsKey(iSn))
                            return; //不属于监控的控制器发出的信息 则返回
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    #region  解析运行状态

                    int iCount = listWg.Count;
                    for (int iIndex = 0; iIndex < iCount; iIndex++)
                    {
                        if (listWg[iIndex] != null)
                        {
                            if (listWg[iIndex].IntSn == iSn)
                            {
                                string str = mjrec.ToDisplaySimpleInfo(true);

                                string strStart = "Swipe Status: \t";
                                string strEnd = "\r\nRead Date:";
                                string strStatus = DoorTimeInfo(str, strStart, strEnd);
                                //卡号
                                string strCardNo = "";
                                //门号
                                string strDoorNo = "";

                                #region 取得 卡号和门号

                                if (strStatus == WgOpenDoorType.刷卡开门.ToString() || strStatus == "刷卡禁止通过: 没有权限")
                                {
                                    strStart = "CardID: \t";
                                    strEnd = "\r\nDoorNO: \t";
                                    strCardNo = DoorTimeInfo(str, strStart, strEnd);
                                    if (strCardNo.Length == 6)
                                    {
                                        strCardNo = "00" + strCardNo;
                                    }
                                    if (strCardNo.Length == 7)
                                    {
                                        strCardNo = "0" + strCardNo;
                                    }
                                    strStart = "DoorNO: \t";
                                    strEnd = "\r\n \t[";
                                    strDoorNo = DoorTimeInfo(str, strStart, strEnd);
                                }
                                else if (strStatus == WgOpenDoorType.超级密码开门.ToString())
                                {
                                    strStart = "DoorNO: \t";
                                    strEnd = "\r\nSwipe";
                                    strDoorNo = DoorTimeInfo(str, strStart, strEnd);
                                }

                                #endregion

                                #region  更新 刷卡信息

                                if (listWg[iIndex].listDoor != null)
                                {
                                    int iCountDoor = listWg[iIndex].listDoor.Count;
                                    for (int iIndexDoor = 0; iIndexDoor < iCountDoor; iIndexDoor++)
                                    {
                                        int iDoorIndex = listWg[iIndex].listDoor[iIndexDoor].IntDoorIndex;
                                        if (strDoorNo == iDoorIndex.ToString())
                                        {
                                            if (strStatus == WgOpenDoorType.刷卡开门 .ToString())
                                            {
                                                string strGroup = "";
                                                string strUser = "";
                                                commonCls.GetWgUserGroup(strCardNo, ref strGroup, ref strUser);

                                                lock (listWg)
                                                {
                                                    listWg[iIndex].listDoor[iIndexDoor].StrOpenType = OpenDoorType.刷卡.ToString();
                                                    listWg[iIndex].listDoor[iIndexDoor].StrGroup = strGroup;
                                                    listWg[iIndex].listDoor[iIndexDoor].StrUser = strUser;
                                                }
                                            }
                                            else if (strStatus == WgOpenDoorType.超级密码开门.ToString())
                                            {
                                                lock (listWg)
                                                {
                                                    listWg[iIndex].listDoor[iIndexDoor].StrOpenType = OpenDoorType.密码.ToString();
                                                }
                                            }
                                            else if (strStatus == "刷卡禁止通过: 没有权限")
                                            {
                                                //报警显示
                                            }
                                            break;
                                        }
                                    }
                                }

                                #endregion

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }

        /// <summary>
        /// 提取运行信息
        /// </summary>
        public string DoorTimeInfo(string runInfo, string start, string end)
        {
            string strRet = "";
            try
            {
                int iStart = runInfo.IndexOf(start);
                int iEnd = runInfo.IndexOf(end);
                int iLenth = iEnd - iStart - start.Length;
                 strRet = runInfo.Substring(start.Length + iStart, iLenth);
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
            return strRet;
        }

        #endregion

        private void timerWg_Execute()
        {
            try
            {
                if (watching != null)
                {
                    updateControllerStatus();
                    if (QueRecText.Count > 0)
                    {
                        txtInfoHaveNewInfoEntry();
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }

    }
}

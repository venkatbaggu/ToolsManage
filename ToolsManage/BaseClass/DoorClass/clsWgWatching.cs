using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WG3000_COMM.Core;

using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace ToolsManage.BaseClass
{
    public class clsWgWatching
    {
        DataLogic datalogic = new DataLogic();


        public static List<clsDoor> listWg = new List<clsDoor>();

        public TimerHelper timerWg = new TimerHelper(1000, false);//房间大门

        private wgMjController wgController = null; // 要监控的控制器
        public wgWatchingService watching; //实时监控服务

        private delegate void txtInfoHaveNewInfo();
        public static Queue QueRecText = new Queue();

        public static int receivedPktCount = 0;
        public static int dealingTxt = 0;
        public static int infoRowsCount = 0;


        public clsWgWatching()
        {
            timerWg.Execute += new TimerHelper.TimerExecution(timerWg_Execute);
        }

        public void StartWatch()
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
                    int iSn = (int)listWg[iIndex].IntSn;
                    if (!string.IsNullOrEmpty(strIp) && iSn != 0)
                    {
                        wgController = new wgMjController();
                        wgController.ControllerSN = iSn;
                        wgController.PORT = 60000;
                        wgController.IP = strIp;
                        selectedControllers.Add(wgController.ControllerSN, wgController);
                    }
                }
            }
            if (selectedControllers.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("selectedControllers.Count=" + selectedControllers.Count.ToString());
                watching.WatchingController = selectedControllers;

                //timerWg.Interval = 1000;
                timerWg.Start();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("selectedControllers.Count=" + selectedControllers.Count.ToString());
                watching.WatchingController = null;
                timerWg.Stop();
            }
        }

        /// <summary>
        /// 暂停 监控
        /// </summary>
        public void StartPauseWatch()
        {
            timerWg.Start();
        }

        /// <summary>
        /// 启动 暂停的监控
        /// </summary>
        public void PauseWatch()
        {
            timerWg.Stop();
        }

        /// <summary>
        /// 停止监控 服务 并清除
        /// </summary>
        public void StopWatch()
        {
            timerWg.Stop();
            if (watching != null)
            {
                watching.StopWatch(); //2010-6-24 09:11:04 停止监控
                watching.WatchingController = null;
                watching = null;
            }
            if (listWg.Count > 0)
            {
                listWg.Clear();
            }
        }

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
                    Application.DoEvents();//2010-8-1 09:09:06 显示
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
                if (watching != null)
                {
                    timerWg.Start();//下次执行
                    System.Threading.Interlocked.Exchange(ref dealingTxt, 0);
                }
            }
        }

        private void evtNewInfoCallBack(string text)
        {
            System.Diagnostics.Debug.WriteLine("Got text through callback! {0}", text);
            receivedPktCount++;
            lock (QueRecText.SyncRoot)
            {
                QueRecText.Enqueue(text);  //加入到文件中
            }
        }

        /// <summary>
        /// 结构体 刷卡开门人及 班组
        /// </summary>
        private struct UserAndGroup
        {
            public string strUser;
            public string strGroup;
            public UserAndGroup(string user, string group)
            {
                strUser = user;
                strGroup = group;
            }
        }

        /// <summary>
        /// 获取 刷卡 开门人 及 班组
        /// </summary>
        private UserAndGroup GetPeopleAndGroup(string strCardNo)
        {
            UserAndGroup userGroup = new UserAndGroup();
            string strSql = "select GroupName,UserName from tb_DoorUser where IcNo='" + strCardNo + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt.Rows.Count > 0)// DtDoorRun     GroupName UserName OnOffDoor ActionTime
            {
                userGroup.strGroup = dt.Rows[0]["GroupName"].ToString();
                userGroup.strUser = dt.Rows[0]["UserName"].ToString();
            }
            else
            {
                userGroup.strGroup = "";
                userGroup.strUser = "";
            }
            return userGroup;
        }

        /// <summary>
        /// 大门 状态及 记录
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="doorState"></param>
        private void BigDoorStateRecord(int iIndex, DoorNo doorNo)
        {
            DoorsState doorState = DoorsState.初值;
            if (doorNo == DoorNo.门1)
            {
                doorState = listWg[iIndex].GetDoorState(1);
            }
            else if (doorNo == DoorNo.门2)
            {
                doorState = listWg[iIndex].GetDoorState(2);
            }
            if (doorState == DoorsState.开门 || doorState == DoorsState.关门)
            {
                bool blRecord = false;
                string strPoint = "";
                string strOpenType = "";
                string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strOpenDoorPeople = "";
                string strGroupName = "";
                DateTime timeOpen = DateTime.Now;

                #region 门 状态

                if (doorNo == DoorNo.门1)
                {
                    if (listWg[iIndex].DoorState1 != doorState)
                    {
                        blRecord = true;
                    }
                    if (blRecord)
                    {
                        lock (listWg)
                        {
                            listWg[iIndex].DoorState1 = doorState;
                        }

                        if (doorState == DoorsState.开门)
                        {
                            //开门类型
                            strOpenType = listWg[iIndex].StrOpenTypeDoor1;
                            if (listWg[iIndex].WgDoorCount == DoorCount.双门)
                            {
                                strPoint = listWg[iIndex].StrNameDoor1;
                            }
                            else
                            {
                                strPoint = "";
                            }
                            if (strOpenType == OpenDoorType.刷卡.ToString())
                            {
                                strGroupName = listWg[iIndex].StrOpenGroup1;
                                strOpenDoorPeople = listWg[iIndex].StrOpenName1;
                            }

                            lock (listWg)
                            {
                                listWg[iIndex].TimeOpenDoor1 = DateTime.Now;
                            }
                        }
                        else if (doorState == DoorsState.关门)
                        {
                            lock (listWg)
                            {
                                timeOpen = listWg[iIndex].TimeOpenDoor1;
                                listWg[iIndex].StrOpenTypeDoor1 = "";
                                listWg[iIndex].StrOpenGroup1 = "";
                                listWg[iIndex].StrOpenName1 = "";
                            }
                        }
                    }
                }
                else if (doorNo == DoorNo.门2)
                {
                    if (listWg[iIndex].DoorState2 != doorState)
                    {
                        blRecord = true;
                    }
                    if (blRecord)
                    {
                        lock (listWg)
                        {
                            listWg[iIndex].DoorState2 = doorState;
                        }
                        if (doorState == DoorsState.开门)
                        {
                            strOpenType = listWg[iIndex].StrOpenTypeDoor2;
                            strPoint = listWg[iIndex].StrNameDoor2;
                            if (strOpenType == OpenDoorType.刷卡.ToString())
                            {
                                strGroupName = listWg[iIndex].StrOpenGroup2;
                                strOpenDoorPeople = listWg[iIndex].StrOpenName2;
                            }
                            lock (listWg)
                            {
                                listWg[iIndex].TimeOpenDoor2 = DateTime.Now;
                            }
                        }
                        else if (doorState == DoorsState.关门)
                        {
                            lock (listWg)
                            {
                                timeOpen = listWg[iIndex].TimeOpenDoor2;
                                listWg[iIndex].StrOpenTypeDoor2 = "";
                                listWg[iIndex].StrOpenGroup2 = "";
                                listWg[iIndex].StrOpenName2 = "";
                            }
                        }
                    }
                }

                #endregion

                #region 产生记录

                if (blRecord)
                {
                    lock (MainControl.dtNewEvent)//Type ToolID Content People Time No IsClick 
                    {
                        DataRow dr = MainControl.dtNewEvent.NewRow();
                        dr["Type"] = EventType.门禁.ToString();
                        dr["Point"] = strPoint;
                        if (strOpenType == OpenDoorType.超级密码开门.ToString())
                        {
                            dr["Content"] = OpenDoorType.密码.ToString();
                            strOpenType = OpenDoorType.密码.ToString();
                        }
                        else
                        {
                            dr["Content"] = doorState.ToString();
                        }

                        dr["People"] = strOpenDoorPeople;
                        dr["Time"] = strTime;
                        MainControl.dtNewEvent.Rows.Add(dr);
                    }

                    if (doorState == DoorsState.开门)
                    {
                        MainControl.blVoiceOpenDoor = true;
                        MainControl.timeLastVoice = DateTime.Now;

                        string str = "insert into tb_RecordDoorInOut (OpenType,Point,GroupName,UserName,OpenTime,CloseTime,DurationTime)" +
                        "values ('" + strOpenType + "','" + strPoint + "','" + strGroupName + "','" + strOpenDoorPeople + "','" + strTime + "','','')";
                        datalogic.SqlComNonQuery(str);
                    }
                    else if (doorState == DoorsState.关门)
                    {
                        TimeSpan timeSpan = DateTime.Now - timeOpen;
                        string strDuration = clsCommon.CalculateTime(timeSpan);
                        string strSql = "update tb_RecordDoorInOut set CloseTime='" + strTime + "',DurationTime='" + strDuration + "' where " +
                            "CloseTime='' and DurationTime='' and Point='" + strPoint + "'";
                        datalogic.SqlComNonQuery(strSql);
                    }
                }

                #endregion

            }
        }

        /// <summary>
        /// 更新 门禁控制器状态
        /// </summary>
        public void updateControllerStatus()
        {
            if (watching != null)
            {
                int iCount = listWg.Count;
                for (int iIndex = 0; iIndex < iCount; iIndex++)
                {
                    if (listWg[iIndex] != null)
                    {
                        #region 门禁控制器 实时状态

                        bool blAskWgState = false;
                        if (listWg[iIndex].DoorNet == CommuniState.已连接)
                        {
                            blAskWgState = true;
                        }
                        else if (listWg[iIndex].DoorNet == CommuniState.已断开)
                        {
                            TimeSpan ts = DateTime.Now - listWg[iIndex].TimeLastAsk;
                            if (ts.TotalSeconds >= 10)
                            {
                                blAskWgState = true;
                                listWg[iIndex].TimeLastAsk = DateTime.Now;
                            }
                        }

                        if (blAskWgState)
                        {
                            wgMjControllerRunInformation conRunInfo = null;
                            int commStatus;
                            int iSn = (int)listWg[iIndex].IntSn;
                            commStatus = watching.CheckControllerCommStatus(iSn, ref conRunInfo);

                            if (commStatus == -1)
                            {
                                listWg[iIndex].IntNetError++;
                                ////实时监控 3次 仍未通信上, 则提示未连接
                                if (listWg[iIndex].DoorNet != CommuniState.已断开)
                                {
                                    if (listWg[iIndex].IntNetError >= 3)
                                    {
                                        listWg[iIndex].IntNetError = 0;
                                        lock (listWg)
                                        {
                                            listWg[iIndex].DoorNet = CommuniState.已断开;
                                        }
                                        NewAlarmEvent(AlarmContent.通信异常.ToString(), false, "");
                                    }
                                }
                            }
                            else if (commStatus == 1)
                            {
                                listWg[iIndex].IntNetError = 0;
                                if (listWg[iIndex].DoorNet == CommuniState.已断开)
                                {
                                    lock (listWg)
                                    {
                                        listWg[iIndex].DoorNet = CommuniState.已连接;
                                    }
                                    NewAlarmEvent(AlarmContent.通信恢复正常.ToString(), false, "");
                                }
                                if (listWg[iIndex].DoorNet != CommuniState.已连接)
                                {
                                    lock (listWg)
                                    {
                                        listWg[iIndex].DoorNet = CommuniState.已连接;
                                    }
                                }
                                //校时
                                DateTime time = conRunInfo.dtNow;
                                TimeSpan ts = DateTime.Now - time;//记录历史温湿度数据间隔
                                if (ts.TotalSeconds >= 59 || ts.TotalSeconds <= -59)
                                {
                                    wgController.AdjustTimeIP(DateTime.Now);
                                }
                            }
                        }



                        #endregion

                        #region  门状态 开 关

                        if (listWg[iIndex].DoorNet == CommuniState.已连接)
                        {
                            BigDoorStateRecord(iIndex, DoorNo.门1);

                            if (listWg[iIndex].WgDoorCount == DoorCount.双门)
                            {
                                BigDoorStateRecord(iIndex, DoorNo.门2);
                            }
                        }

                        #endregion
                    }
                }
            }


            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    if (frmMain.blDebug)
            //        MessageUtil.ShowTips(ex.Message);
            //} 


        }

        private void NewAlarmEvent(string strContent, bool blShow, string strPoint)
        {
            string strType = AlarmsType.门禁.ToString();
            string strPeople = "";
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (frmMain.blDebug || blShow)
            {
                DataRow dr = MainControl.dtNewAlarm.NewRow();
                dr["Type"] = strType;
                dr["Point"] = strPoint;
                dr["Content"] = strContent;
                dr["People"] = "";
                dr["Time"] = strTime;
                MainControl.dtNewAlarm.Rows.Add(dr);
            }
            string strSql = "insert into tb_AlarmEvent (Type,Point,EventContent,People,Time)" +
                                                       "values ('" + strType + "','" + strPoint + "','" + strContent + "','" + strPeople + "','" + strTime + "')";
            datalogic.SqlComNonQuery(strSql);
        }

        /// <summary>
        /// 有新的运行信息
        /// </summary>
        public void txtInfoHaveNewInfoEntry()
        {
            #region

            if (dealingTxt > 0)
            {
                return;
            }
            if (watching.WatchingController == null) //2010-8-1 08:27:15 没有选中监控的就退出
            {
                return;
            }
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
                infoRowsCount++;
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
            Application.DoEvents();//显示
            System.Threading.Interlocked.Exchange(ref dealingTxt, 0);

            #endregion

            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    if (frmMain.blDebug)
            //        MessageUtil.ShowTips(ex.Message);
            //}
        }

        private void txtInfoUpdateEntry(object info)
        {
            #region

            wgMjControllerSwipeRecord mjrec = new wgMjControllerSwipeRecord(info as string);
            if (mjrec.ControllerSN > 0)
            {
                //如果不处于监控的控制器 则不作数据处理

                int iSn = (int)mjrec.ControllerSN;
                try
                {
                    if (!watching.WatchingController.ContainsKey(iSn))
                    {
                        return; //不属于监控的控制器发出的信息 则返回
                    }
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
                            if (strStatus == OpenDoorType.刷卡.ToString() || strStatus == "刷卡禁止通过: 没有权限")
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

                            //刷卡开门
                            if (strStatus == OpenDoorType.刷卡.ToString())
                            {
                                UserAndGroup userGroup = GetPeopleAndGroup(strCardNo);
                                string strOpenDoorPeople = userGroup.strUser;
                                string strGroupName = userGroup.strGroup;
                                lock (listWg)
                                {
                                    if (strDoorNo == "1")
                                    {
                                        listWg[iIndex].StrOpenTypeDoor1 = OpenDoorType.刷卡.ToString();
                                        listWg[iIndex].StrOpenGroup1 = strGroupName;
                                        listWg[iIndex].StrOpenName1 = strOpenDoorPeople;
                                    }
                                    else if (strDoorNo == "2")
                                    {
                                        listWg[iIndex].StrOpenTypeDoor2 = OpenDoorType.刷卡.ToString();
                                        listWg[iIndex].StrOpenGroup2 = strGroupName;
                                        listWg[iIndex].StrOpenName2 = strOpenDoorPeople;
                                    }
                                }
                            }
                            else if (strStatus == "刷卡禁止通过: 没有权限")
                            {
                                string strType = AlarmsType.门禁.ToString();
                                string strContent = "刷卡禁止通过：无权限  卡号：" + strCardNo;
                                string strPoint = "";
                                if (listWg[iIndex].WgDoorCount == DoorCount.双门)
                                {
                                    if (strDoorNo == "1")
                                    {
                                        strPoint = listWg[iIndex].StrNameDoor1;
                                    }
                                    else if (strDoorNo == "2")
                                    {
                                        strPoint = listWg[iIndex].StrNameDoor2;
                                    }
                                }



                                NewAlarmEvent(strContent, true, strPoint);
                            }
                            else if (strStatus == OpenDoorType.超级密码开门.ToString())
                            {
                                strStart = "DoorNO: \t";
                                strEnd = "\r\nSwipe";
                                strDoorNo = DoorTimeInfo(str, strStart, strEnd);

                                lock (listWg)
                                {
                                    if (strDoorNo == "1")
                                    {
                                        listWg[iIndex].StrOpenTypeDoor1 = OpenDoorType.超级密码开门.ToString();
                                    }
                                    else if (strDoorNo == "2")
                                    {
                                        listWg[iIndex].StrOpenTypeDoor2 = OpenDoorType.超级密码开门.ToString();
                                    }
                                }
                            }
                            break;
                        }
                    }
                }

                #endregion
            }

            #endregion

            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    if (frmMain.blDebug)
            //        MessageUtil.ShowTips(ex.Message);
            //}
        }

        /// <summary>
        /// 提取运行信息
        /// </summary>
        /// <param name="runInfo"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string DoorTimeInfo(string runInfo, string start, string end)
        {
            int iStart = runInfo.IndexOf(start);
            int iEnd = runInfo.IndexOf(end);
            int iLenth = iEnd - iStart - start.Length;
            string strRet = runInfo.Substring(start.Length + iStart, iLenth);
            return strRet;
        }

    }
}

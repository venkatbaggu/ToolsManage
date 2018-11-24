using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Threading;

using ToolsManage.BaseClass.RFidClass;
using System.Threading.Tasks;

namespace ToolsManage.BaseClass.DoorClass
{
    public class clsZkControl
    {
       private DataLogic datalogic = new DataLogic();
       private clsCommon commonCls = new clsCommon();
       public List<clsZkDoor> listZk = new List<clsZkDoor>();
       //public clsZkDoor zkDoor;
       public TimerHelper timer;

        public clsZkControl()
        {
            if (timer == null)
                timer = new TimerHelper(1000, false);
            timer.Execute +=new TimerHelper.TimerExecution(timer_Execute);
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

        #region 方法

        /// <summary>
        /// 清除 门禁控制器 list
        /// </summary>
        public void ClearZkList()
        {
            if (listZk.Count > 0) 
            {
                for (int iIndex = 0; iIndex < listZk.Count; iIndex++)
                {
                    listZk[iIndex].Clear();
                }
                listZk.Clear();
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        #endregion

        #region  事件

        private void zk_OnDisConnectEvent()
        {
            try
            {
                //if (zkDoor.BlConnoct)
                //    DisConnectZkAndClearEvent();
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 

        }

        private void zk_OnAlarmEvent(OnAlarmEventArgs e)
        {
            if (e == null)
                return;

            //lbRTShow.Items.Add("报警类型=" + e.iAlarmType.ToString());
            //lbRTShow.Items.Add("用户ID=" + e.iUserId.ToString());
            //lbRTShow.Items.Add("是否通过验证=" + e.iVerifyd.ToString());
        }

        private void zk_OnDoorEvent(OnDoorEventArgs e)
        {
            if (e == null)
                return;
        }

        private void zk_VerifyOkEvent(VerifyOkEventArgs e)
        {
            if (e == null)
                return;
            string strId = e.strUserId;
            VerifyMethod method = e.verifyMode;
            string strIp = e.strDeviIp;

            for (int iIndex = 0; iIndex < listZk.Count; iIndex++)
            {
                if (listZk[iIndex].StrIp == strIp)
                {
                    string strGroup = "";
                    string strUser = "";
                    commonCls.GetZkUserGroup(strId, ref strGroup, ref strUser);
                    listZk[iIndex].doorInfo.StrGroup = strGroup;
                    listZk[iIndex].doorInfo.StrUser = strUser;
                    if (method == VerifyMethod.密码)
                    {
                        listZk[iIndex].doorInfo.StrOpenType = OpenDoorType.密码.ToString();
                    }
                    else if (method == VerifyMethod.人脸)
                    {
                        listZk[iIndex].doorInfo.StrOpenType = OpenDoorType.人脸.ToString();
                    }
                    else if (method == VerifyMethod.刷卡)
                    {
                        listZk[iIndex].doorInfo.StrOpenType = OpenDoorType.刷卡.ToString();
                    }
                    else if (method == VerifyMethod.指纹)
                    {
                        listZk[iIndex].doorInfo.StrOpenType = OpenDoorType.指纹.ToString();
                    }
                    break;
                }
            }
        }

        #endregion

        #region  子程序

        private bool ConnectZk(int iIndex)
        {
            bool blRet = false;
            try
            {
                Task<bool> task = listZk[iIndex].ConnectZk();
                if (task.Result)
                {
                    listZk[iIndex].VerifyOkEvent += new VerifyOkEventHandler(zk_VerifyOkEvent);
                    listZk[iIndex].OnDoorEvent += new OnDoorEventHandler(zk_OnDoorEvent);
                    listZk[iIndex].OnAlarmEvent += new OnAlarmEventHandler(zk_OnAlarmEvent);
                    listZk[iIndex].OnDisConnectEvent += new OnDisConnectEventHandler(zk_OnDisConnectEvent);
                    blRet = true;
                }
                else
                {
                    blRet = false;
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
            return blRet;
        }

        private void DisConnectZkAndClearEvent(int iIndex)
        {
            try
            {
                listZk[iIndex].DisConnect();
                listZk[iIndex].BlConnoct = false;
                listZk[iIndex].VerifyOkEvent -= new VerifyOkEventHandler(zk_VerifyOkEvent);
                listZk[iIndex].OnDoorEvent -= new OnDoorEventHandler(zk_OnDoorEvent);
                listZk[iIndex].OnAlarmEvent -= new OnAlarmEventHandler(zk_OnAlarmEvent);
                listZk[iIndex].OnDisConnectEvent -= new OnDisConnectEventHandler(zk_OnDisConnectEvent);
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        /// <summary>
        /// 控制器 通信 异常 显示 和记录
        /// </summary>
        private void NetErrInfoAndRecord( int iIndex, string strContent)
        {
            string strZkName = listZk[iIndex].StrNameOfZk; ;//微耕控制器名称
            //异常记录
            commonCls.NewErrRecord(EventType.门禁.ToString(), strZkName, strContent, "");
            if (NewAlarmShowEvent != null)
            {
                NewAlarmShowEvent(new NewEventEventArgs(EventType.门禁, strZkName, strContent, "", "", DateTime.Now));
            }
        }

        #endregion

        //扫描中控门禁
        private void ScanZKMJ()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    for (int iIndex = 0; iIndex < listZk.Count; iIndex++)
                    {

                    }
                }
            });
        }

        private void timer_Execute()
        {
            try
            {
                for (int iIndex = 0; iIndex < listZk.Count; iIndex++)
                {
                    if (listZk[iIndex].BlConnoct == false)
                    {
                        TimeSpan span = DateTime.Now - listZk[iIndex].TimeConnectLast;
                        if (span.TotalMinutes > 2)
                        {
                            bool blRet = ConnectZk(iIndex);
                            if (blRet)
                            {
                                if (listZk[iIndex].StateOfNet != CommuniState.已连接)
                                {
                                    lock (listZk)
                                    {
                                        listZk[iIndex].StateOfNet = CommuniState.已连接;
                                    }
                                    //异常事件
                                    NetErrInfoAndRecord(iIndex, ErrorContent.通信恢复正常.ToString());
                                }
                            }
                            lock (listZk)
                            {
                                listZk[iIndex].TimeConnectLast = DateTime .Now;
                            }
                        }
                    }
                    else
                    {
                        DoorsState state = DoorsState.初值;
                        bool blRet = listZk[iIndex].GetDoorState(ref state);
                        if (blRet)
                        {
                            if (state != listZk[iIndex].doorInfo.StateOfDoor)
                            {
                                listZk[iIndex].doorInfo.StateOfDoor = state;

                                if (state == DoorsState.关门)
                                {
                                    listZk[iIndex].doorInfo.StrOpenType = "";
                                    listZk[iIndex].doorInfo.StrGroup = "";
                                    listZk[iIndex].doorInfo.StrUser = "";
                                }
                                if (listZk[iIndex].doorInfo.IsOfRfid == DeviceUsing.启用)
                                {//確定是哪个门禁关联RFID
                                    if (state == DoorsState.关门)
                                    {
                                        clsRfidRead.strOpenUser = "";
                                    }
                                    else if (state == DoorsState.开门)
                                    {
                                        clsRfidRead.strOpenUser = listZk[iIndex].doorInfo.StrUser;
                                    }
                                }

                                //if (state == DoorsState.关门)
                                //{
                                //    listZk[iIndex].doorInfo.StrOpenType = "";
                                //    listZk[iIndex].doorInfo.StrGroup = "";
                                //    listZk[iIndex].doorInfo.StrUser = "";
                                //    if (listZk[iIndex].doorInfo.IsOfRfid == DeviceUsing.启用)
                                //    {
                                //        clsRfidRead.strOpenUser = "";
                                //    }
                                //}
                                //else if (state == DoorsState.开门 )
                                //{
                                //    if (listZk[iIndex].doorInfo.IsOfRfid == DeviceUsing.启用)
                                //    {
                                //        clsRfidRead.strOpenUser  = listZk[iIndex].doorInfo.StrUser;
                                //    }
                                //}
                                //开关门事件
                                if (NewEventShowEvent != null)
                                {
                                    string strPoint = listZk[iIndex].doorInfo.StrDoorName;
                                    string strType = "非法开门";
                                    if (!String.IsNullOrEmpty(listZk[iIndex].doorInfo.StrOpenType))
                                        strType = listZk[iIndex].doorInfo.StrOpenType;
                                    else
                                        strType = state.ToString();
                                    string strUser = listZk[iIndex].doorInfo.StrUser;
                                    string StrGroup = listZk[iIndex].doorInfo.StrGroup ;
                                    NewEventShowEvent(new NewEventEventArgs(EventType.门禁, strPoint, state.ToString(), strUser, strType, DateTime.Now));
                                    commonCls.NewDoorInOut(state, strType, strPoint, StrGroup, strUser, "");
                                }
                            }
                        }
                        else
                        {
                            if (listZk[iIndex].BlConnoct)
                            {
                                DisConnectZkAndClearEvent(iIndex);
                                if (listZk[iIndex].StateOfNet != CommuniState.已断开 )
                                {
                                    lock (listZk)
                                    {
                                        listZk[iIndex].StateOfNet = CommuniState.已断开;
                                    }
                                    //异常事件
                                    NetErrInfoAndRecord(iIndex, ErrorContent.通信异常.ToString());
                                }
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
    }
}

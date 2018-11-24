using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ToolsManage.BaseClass.RFidClass.NetRfid
{
    public enum OperRfidType
    {
        /// <summary>
        /// 同步操作
        /// </summary>
        SyncOper,
        /// <summary>
        /// 异步操作
        /// </summary>
        AsyncOper,
        /// <summary>
        /// None
        /// </summary>
        None,
    }
    /// <summary>
    /// rfid管理
    /// </summary>
    public class RfidManage
    {
        #region 变量
        public TimerHelper timerRead = null;
        public static string strOpenUser = "";
        private clsCommon commonCls = new clsCommon();
        public static ConcurrentQueue<EpcInfo> QEpcInfo;
        private Thread threadDealEpcInfo = null;
        private bool IsDealEpc = true;
        private bool IsTimerRun = false;
        private OperRfidType operRfidType = OperRfidType.AsyncOper;
        #endregion

        #region 属性
        private List<SingleRfid> m_Listsrfid = new List<SingleRfid>();
        public List<SingleRfid> Listsrfid
        {
            get
            {
                return m_Listsrfid;
            }

            set
            {
                m_Listsrfid = value;
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

        #endregion

        public RfidManage()
        {
            QEpcInfo = new ConcurrentQueue<EpcInfo>();
            timerRead = new TimerHelper(50, false);
            timerRead.Execute += new TimerHelper.TimerExecution(timerRead_Execute);
            threadDealEpcInfo = new Thread(new ParameterizedThreadStart(DealEpcInfo));
            threadDealEpcInfo.Start();
        }

        public void BindEvent()
        {
            if(operRfidType== OperRfidType.AsyncOper)
            {
                foreach(SingleRfid sr in m_Listsrfid)
                {
                    sr.NewEventShowEvent += EventShow;
                    sr.NewAlarmShowEvent += AlarmShow;
                    sr.e_NewErrRecord += NewErrRecord;
                    sr.e_ReStartRfid += Sr_e_ReStartRfid;
                }
            }
        }

        private void Sr_e_ReStartRfid()
        {
            //timerStop();
            //Thread.Sleep(1000);
            frmMain.RFIDReLoad = ErrorContent.RFID读写器异常系统重启;
        }

        public void EventShow(NewEventEventArgs e)
        {
            if (NewEventShowEvent != null)
                NewEventShowEvent(e);
        }

        public void AlarmShow(NewEventEventArgs e)
        {
            if (NewAlarmShowEvent != null)
                NewAlarmShowEvent(e);
        }

        private void NewErrRecord(string strType, string strPoint, string strContent, string strRemark)
        {
            commonCls.NewErrRecord(strType, strPoint, strContent, strRemark);
        }

        public void timerStart()
        {
            if (operRfidType == OperRfidType.SyncOper)
            {
                timerRead.Start();
            }
            else if(operRfidType== OperRfidType.AsyncOper)
            {
                foreach (SingleRfid sr in m_Listsrfid)
                {
                    //if(sr.IP=="192.168.1.198")
                        sr.timerStart();
                }
            }
        }

        public void timerStop()
        {
            if (operRfidType == OperRfidType.SyncOper)
            {
                timerRead.Stop();
            }
            else if (operRfidType == OperRfidType.AsyncOper)
            {
                foreach (SingleRfid sr in m_Listsrfid)
                {
                    sr.timerStop();
                }
            }
        }

        public void StopRfid()
        {
            //if (timerRead.State == TimerState.Running)
            //{
            //    timerStop();
            //}
            timerStop();
            for (int iIndex = 0; iIndex < m_Listsrfid.Count; iIndex++)
            {
                if (m_Listsrfid[iIndex].BlConnent)
                {
                    m_Listsrfid[iIndex].Stop();
                }
            }
            if (m_Listsrfid.Count > 0)
                m_Listsrfid.Clear();
        }

        public void DisposeRfid(bool isexist)
        {
            StopRfid();
            if (isexist)
            {
                if(threadDealEpcInfo!=null)
                    threadDealEpcInfo.Abort();
            }
            timerRead.Execute -= new TimerHelper.TimerExecution(timerRead_Execute);
            timerRead = null;
        }

        /// <summary>
        /// 处理读到的Epc信息
        /// </summary>
        /// <param name="obj"></param>
        private void DealEpcInfo(object obj)
        {
            while(IsDealEpc)
            {
                try
                {
                    Thread.Sleep(1000);
                    if(QEpcInfo!=null && QEpcInfo.Count>0)
                    {
                        while(QEpcInfo.Count>0)
                        {
                            EpcInfo ei = null;
                            QEpcInfo.TryDequeue(out ei);
                            if(ei!=null)
                            {
                                DealToolInfo(ei);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void DealToolInfo(EpcInfo ei)
        {
            try
            {
                if (MainControl.dicTools != null && MainControl.dicTools.Count > 0)
                {
                    if (MainControl.dicTools.ContainsKey(ei.StrEpc))
                    {
                        DateTime dtBorrRet = MainControl.dicTools[ei.StrEpc].TimeBorrRet;
                        TimeSpan ts = ei.DtReadedEpc - dtBorrRet;
                        if (ts.TotalSeconds >= infoOfSystem.iBorrRetSpan)
                        {//出入库时间大于设置时间才算有效
                            string strToolType = MainControl.dicTools[ei.StrEpc].StrToolType;
                            string strToolName = MainControl.dicTools[ei.StrEpc].StrToolName;
                            string strToolID = MainControl.dicTools[ei.StrEpc].StrToolId;
                            string strBorrOrRetTime = ei.DtReadedEpc.ToString("yyyy-MM-dd HH:mm:ss");
                            if (MainControl.dicTools[ei.StrEpc].ToolState == ToolsState.在库)
                            {
                                if (ei.UsingReadNo == DeviceUsing.启用)
                                {
                                    if (ei.BorrOrRet == ToolsBorrRet.借出)
                                    {
                                        MainControl.dicTools[ei.StrEpc].ToolState = ToolsState.借出;
                                        GenToolRorrRetRecord(ToolsState.借出, strBorrOrRetTime, ei.StrEpc, strToolType, strToolName, strToolID);
                                        //ei.IsEpcRead = IsReadShow.已读;
                                        MainControl.dicTools[ei.StrEpc].TimeBorrRet = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    MainControl.dicTools[ei.StrEpc].ToolState = ToolsState.借出;
                                    GenToolRorrRetRecord(ToolsState.借出, strBorrOrRetTime, ei.StrEpc, strToolType, strToolName, strToolID);
                                    //ei.IsEpcRead = IsReadShow.已读;
                                    MainControl.dicTools[ei.StrEpc].TimeBorrRet = DateTime.Now;
                                }
                            }
                            else if (MainControl.dicTools[ei.StrEpc].ToolState == ToolsState.借出)
                            {
                                if (ei.UsingReadNo == DeviceUsing.启用)
                                {
                                    if (ei.BorrOrRet == ToolsBorrRet.归还)
                                    {
                                        MainControl.dicTools[ei.StrEpc].ToolState = ToolsState.在库;
                                        GenToolRorrRetRecord(ToolsState.在库, strBorrOrRetTime, ei.StrEpc, strToolType, strToolName, strToolID);
                                        //ei.IsEpcRead = IsReadShow.已读;
                                        MainControl.dicTools[ei.StrEpc].TimeBorrRet = DateTime.Now;
                                    }
                                }
                                else
                                {
                                    MainControl.dicTools[ei.StrEpc].ToolState = ToolsState.在库;
                                    GenToolRorrRetRecord(ToolsState.在库, strBorrOrRetTime, ei.StrEpc, strToolType, strToolName, strToolID);
                                    //ei.IsEpcRead = IsReadShow.已读;
                                    MainControl.dicTools[ei.StrEpc].TimeBorrRet = DateTime.Now;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            { }
        }

        private void GenToolRorrRetRecord(ToolsState stateOfTool, string strTime, string strEpc, string strToolType, string strToolName, string strToolID)
        {
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

        /// <summary>
        /// 定时读Rfid读写器,获取Epc信息
        /// </summary>
        private void timerRead_Execute()
        {
            try
            {
                if (!IsTimerRun)
                {
                    IsTimerRun = true;
                    for (int iIndex = 0; iIndex < m_Listsrfid.Count; iIndex++)
                    {
                        if (m_Listsrfid[iIndex].BlConnent == false)
                        {//连接rfid
                            OpenRfidNewState(iIndex);
                        }
                        else
                        {
                            if (m_Listsrfid[iIndex].BlConnent)
                            {
                                bool blRet = m_Listsrfid[iIndex].ReadEpcInfo();
                                if (blRet)
                                {
                                    //EpcBorrOrRet(iIndex);
                                }
                                if (m_Listsrfid[iIndex].IReadEpcErr > 3)
                                {
                                    m_Listsrfid[iIndex].IReadEpcErr = 0;
                                    m_Listsrfid[iIndex].Stop();
                                    m_Listsrfid[iIndex].BlConnent = false;
                                }
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
                IsTimerRun = false;
            }
        }

        /// <summary>
        /// 连接 读写器  更新通信状态
        /// </summary>
        private void OpenRfidNewState(int iIndex)
        {
            if (m_Listsrfid[iIndex].StateNet == CommuniState.错误)
            {
                TimeSpan span = DateTime.Now - m_Listsrfid[iIndex].TimeLinkErr;
                if (span.TotalMinutes < 1)
                {
                    return;
                }
            }
            if (frmMain.stateRstRfid == StateRstRfid.关闭 || frmMain.stateRstRfid == StateRstRfid.重启)
            {
                return;
            }

            bool blRet = m_Listsrfid[iIndex].Stop();  //rfid关闭连接
            blRet = m_Listsrfid[iIndex].Start();      //rfid打开连接
            if (blRet)
            {
                m_Listsrfid[iIndex].BlConnent = true;
                m_Listsrfid[iIndex].ILinkErr = 0;
                if (m_Listsrfid[iIndex].StateNet == CommuniState.错误)
                {
                    //通信恢复正常
                    string strName = m_Listsrfid[iIndex].Name;
                    string strContent = ErrorContent.通信恢复正常.ToString();
                    commonCls.NewErrRecord(EventType.RFID读写器.ToString(), strName, strContent, "");
                    if (NewAlarmShowEvent != null)
                    {
                        NewAlarmShowEvent(new NewEventEventArgs(EventType.RFID读写器, strName, strContent, "", "", DateTime.Now));
                    }
                }
                if (m_Listsrfid[iIndex].StateNet != CommuniState.正常)
                {
                    m_Listsrfid[iIndex].StateNet = CommuniState.正常;
                }
            }
            else
            {
                m_Listsrfid[iIndex].ILinkErr++;
            }
            if (m_Listsrfid[iIndex].ILinkErr >= 2)
            {
                m_Listsrfid[iIndex].ILinkErr = 0;
                if (m_Listsrfid[iIndex].StateNet == CommuniState.正常 || m_Listsrfid[iIndex].StateNet == CommuniState.初值)
                {
                    //异常记录
                    string strName = m_Listsrfid[iIndex].Name;
                    string strContent = ErrorContent.通信异常.ToString();
                    commonCls.NewErrRecord(EventType.RFID读写器.ToString(), strName, strContent, "");
                    if (NewAlarmShowEvent != null)
                    {
                        NewAlarmShowEvent(new NewEventEventArgs(EventType.RFID读写器, strName, strContent, "", "", DateTime.Now));
                    }
                }
                else if (m_Listsrfid[iIndex].StateNet == CommuniState.错误)
                {
                    commonCls.NewErrRecord(EventType.系统.ToString(), "", ErrorContent.RFID读写器异常系统重启.ToString(), "");
                    //timerStop();
                    Thread.Sleep(1000);
                    frmMain.RFIDReLoad = ErrorContent.RFID读写器异常系统重启;
                }
                if (m_Listsrfid[iIndex].StateNet != CommuniState.错误)
                {
                    m_Listsrfid[iIndex].StateNet = CommuniState.错误;
                }
                m_Listsrfid[iIndex].TimeLinkErr = DateTime.Now;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

using ToolsManage.ToolsManage;
using ToolsManage.BaseClass;
using ToolsManage.DoorManage;
using ToolsManage.VideoManage;
using ToolsManage.TestManage;
using ToolsManage.SystemManage;
using ToolsManage.EnvirManage;
using ToolsManage.BaseClass.EnvirClass;
using ToolsManage.BaseClass.DoorClass;
using ToolsManage.BaseClass.RFidClass;
using ToolsManage.BaseClass.OtherClass;
using ToolsManage.BaseClass.PowerReaders;

using System.Threading;
using System.IO.Ports;
using DevExpress.XtraSplashScreen;
using ToolsManage.BLL;
using ToolsManage.Common;
using DAMSystem;
using Common.FileLog;
using ToolsManage.BaseClass.RFidClass.NetRfid;
//using ToolsManage.Domain;

//using ReaderB;//RFID

namespace ToolsManage
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        DataLogic datalogic = new DataLogic();
        StationServer stationServer = new StationServer();
        MainControl mainControl = new MainControl();
        AppConfig config = new AppConfig();

        List<clsNewTreeView> listNewTree = new List<clsNewTreeView>();

        public delegate void InvokeDelegate();

        public static bool blDebug = false;//false  true

        #region 静态变量

        //  用户及 权限
        public static string strUserID = "";
        public static string strUserName = "";
        public static UserPower systemPower = UserPower.未注册;
        public static DateTime tiemLogin = DateTime.Now;
        // 设备配置权限
        public static DeviceUsing UsingEnvir = DeviceUsing.未启用;
        public static DeviceUsing UsingDoor = DeviceUsing.未启用;
        public static DeviceUsing UsingFinger = DeviceUsing.未启用;
        public static DeviceUsing UsingUp = DeviceUsing.未启用;
        public static DeviceUsing UsingBox = DeviceUsing.未启用;

        /// <summary>
        /// RFID读写器 重启状态
        /// </summary>
        public static StateRstRfid stateRstRfid = StateRstRfid.初值;

        public static ErrorContent RFIDReLoad = ErrorContent.初值;

        #endregion

        #region 变量

        //事件表 报警表
        public DataTable dtAlarmInfo = new DataTable();
        public DataTable dtEventInfo = new DataTable();
        private int iNoEvent = 0;//事件记录表 序号
        private int iNoAlarm = 0;//事件记录表 序号
        //
       public static clsEnvirControl envirControl;
        //门禁
        clsWgRunWatch wgRunWatch;
        clsZkControl zkControl;
        //工具柜
        clsWgRunWatch boxDoorWatching;
        //房间RFID
        //public clsRfidRead rfidRead;
        private RfidManage rfidManage = null;

        //半有源读写器
        PowerReader powReader;


        /// <summary>
        /// RFID重启时 关闭时间
        /// </summary>
        private DateTime timeOffRfid = DateTime.Now;

        #endregion

        public frmMain()
        {
            InitializeComponent();
            InitSkinGallery();
            this.gridView1.IndicatorWidth = 40;
            this.gridView3.IndicatorWidth = 40;
        }

        void InitSkinGallery()
        {
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins, true);
            this.ribbonControl.Toolbar.ItemLinks.Clear();
            this.ribbonControl.Toolbar.ItemLinks.Add(rgbiSkins);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (blDebug == false)
            {
                pboxDebug.BackgroundImage = null;
            }
            else
            {
                frmMain.strUserName = "kn";
                frmMain.systemPower = UserPower.厂家;
            }
            btxtTime.Caption = DateTime.Now.ToString();

            ShowUser();
            LoadSysSet();

            mainControl.LoadAllTools();
            LoadBox();

            FunctionModeSet();

            EventTableInit();
            EventTableShowInit();
            AlarmTableInit();
            AlarmTableShowInit();

            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            if (UsingEnvir == DeviceUsing.启用)
            {
                mainControl.LoadRooms();
                envirControl.Start();
            }

            mainControl.timerMain.Start();

            gridControl1.DataSource = dtEventInfo;
            gridControl3.DataSource = dtAlarmInfo;
            StartTaskPlan();

            SplashScreenManager.CloseForm();
        }
        EnvirPlanManage envirPlanManage = null;
        private void StartTaskPlan()
        {
            Context.IsStartPlanTask = Configurations.IsStartPlanTask;
            if (Context.IsStartPlanTask)
            {
                envirPlanManage = new EnvirPlanManage();
                envirPlanManage.Start();
            }
        }

        #region  事件 

        //powReader.NewEventShowEvent += new NewEventShowEventHandler(powReader_NewEventShowEvent);
        //            powReader.NewAlarmShowEvent += new NewEventShowEventHandler(powReader_NewAlarmShowEvent);

        #region  半有源读写器

        /// <summary>
        ///  事件 
        /// </summary>
        /// <param name="e"></param>
        private void powReader_NewEventShowEvent(NewEventEventArgs e)
        {
            if (e == null)
                return;
            EventRece(e);
        }

        /// <summary>
        /// 报警异常 
        /// </summary>
        /// <param name="e"></param>
        private void powReader_NewAlarmShowEvent(NewEventEventArgs e)
        {
            if (infoOfSystem.usingOfErr == DeviceUsing.启用)
            {
                if (e == null)
                    return;
                AlarmErrorRece(e);
            }
        }

        #endregion

        #region  大门 RFID

        /// <summary>
        ///  事件 
        /// </summary>
        /// <param name="e"></param>
        private void rfidRead_NewEventShowEvent(NewEventEventArgs e)
        {
            if (e == null)
                return;
            EventRece(e);
        }

        /// <summary>
        /// 报警异常 
        /// </summary>
        /// <param name="e"></param>
        private void rfidRead_NewAlarmShowEvent(NewEventEventArgs e)
        {
            //if (envirControl != null)
            //{ 

            //}

            stateRstRfid = StateRstRfid.重启;

            if (infoOfSystem.usingOfErr == DeviceUsing.启用)
            {
                if (e == null)
                    return;
                AlarmErrorRece(e);
            }
        }


        #endregion

        #region  工具柜

        /// <summary>
        ///  事件 
        /// </summary>
        /// <param name="e"></param>
        private void boxDoorWatching_NewEventShowEvent(NewEventEventArgs e)
        {
            if (e == null)
                return;
            EventRece(e);
        }

        /// <summary>
        /// 报警异常 
        /// </summary>
        /// <param name="e"></param>
        private void boxDoorWatching_NewAlarmShowEvent(NewEventEventArgs e)
        {
            if (infoOfSystem.usingOfErr == DeviceUsing.启用)
            {
                if (e == null)
                    return;
                AlarmErrorRece(e);
            }

        }


        #endregion

        #region IC门禁

        /// <summary>
        ///  事件 
        /// </summary>
        /// <param name="e"></param>
        private void wgRunWatch_NewEventShowEvent(NewEventEventArgs e)
        {
            if (e == null)
                return;
            EventRece(e);
        }

        /// <summary>
        /// 报警异常 
        /// </summary>
        /// <param name="e"></param>
        private void wgRunWatch_NewAlarmShowEvent(NewEventEventArgs e)
        {
            if (infoOfSystem.usingOfErr == DeviceUsing.启用)
            {
                if (e == null)
                    return;
                AlarmErrorRece(e);
            }

        }

        #endregion

        #region 指纹门禁

        /// <summary>
        /// 事件 
        /// </summary>
        /// <param name="e"></param>
        private void zkControl_NewEventShowEvent(NewEventEventArgs e)
        {
            if (e == null)
                return;
            EventRece(e);
        }

        /// <summary>
        /// 报警 异常
        /// </summary>
        /// <param name="e"></param>
        private void zkControl_NewAlarmShowEvent(NewEventEventArgs e)
        {
            if (infoOfSystem.usingOfErr == DeviceUsing.启用)
            {
                if (e == null)
                    return;
                AlarmErrorRece(e);
            }



        }

        #endregion

        #region 空调 串口事件

        /// <summary>
        ///   事件
        /// </summary>
        private void serialAir_NewEventShowEvent(NewEventEventArgs e)
        {
            if (e == null)
                return;
            EventRece(e);
        }

        #endregion

        #region IO板 串口事件

        private void serialIo_NewEventShowEvent(NewEventEventArgs e)
        {
            if (e == null)
                return;
            EventRece(e);
        }

        #endregion

        #region 环境报警 烟感

        private void envirControl_NewAlarmShowEvent(NewEventEventArgs e)
        {
            if (e == null)
                return;
            AlarmErrorRece(e);
        }

        #endregion

        #endregion

        #region 异常 报警 表 更新

        string strAlarmEventType = "";
        string strAlarmPoint = "";
        string strAlarmContent = "";
        string strAlarmPeople = "";
        string strAlarmRemark = "";
        string strAlarmTime = "";
        private void AlarmErrorRece(NewEventEventArgs e)
        {
            strAlarmEventType = e.eventType.ToString();
            strAlarmPoint = e.strPoint;
            strAlarmContent = e.strContent;
            strAlarmPeople = e.strPeople;
            strAlarmRemark = e.strRemark;
            strAlarmTime = e.timeAction.ToString("yyyy-MM-dd HH:mm:ss");

            gridControl3.BeginInvoke(new InvokeDelegate(updateAlarmShow));

            //上传
            //if (UsingUp == DeviceUsing.启用)
            //{
            //    string str = "insert into tb_NewEvent (Addr,Type,Point,Content,People,Time,IsRead)" +
            //              "values ('" + MainControl.strServerAddr + "','" + strUpEventType + "','" + strUpPoint + "','" + strUpContent + "'" +
            //              ",'" + strUpPeople + "','" + strUpTime + "','" + ServerIsRead.未读.ToString() + "')";
            //    stationServer.SqlComNonQuery(str);
            //}

            iNoAlarm++;
            if (iNoAlarm >= 100)
                iNoAlarm = 0;
        }

        private void updateAlarmShow()
        {
            try
            {
                DataRow dr = dtAlarmInfo.NewRow();
                dr["Type"] = strAlarmEventType;
                dr["Point"] = strAlarmPoint;
                dr["Content"] = strAlarmContent;
                dr["People"] = strAlarmPeople;
                dr["Remark"] = strAlarmRemark;
                dr["Time"] = strAlarmTime;
                dr["No"] = iNoAlarm.ToString();
                dr["IsClick"] = "";
                dtAlarmInfo.Rows.Add(dr);

                // 多过100 条 自动删除
                if (dtAlarmInfo.Rows.Count > 100)
                {
                    int iMore = dtAlarmInfo.Rows.Count - 100;
                    for (int iIndex = 0; iIndex < iMore; iIndex++)
                    {
                        dtAlarmInfo.Rows.RemoveAt(0);
                    }
                }
                gridView3.MoveLast();
                this.gridView3.BestFitColumns();
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }


        #endregion

        #region  事件 表 更新

        List<clsNewEvent> listNetEvent = new List<clsNewEvent>();
        //string strUpEventType = "";
        //string strUpPoint = "";
        //string strUpContent = "";
        //string strUpPeople = "";
        //string strUpRemark = "";
        //string strUpTime = "";
        private void EventRece(NewEventEventArgs e)
        {
            string strUpEventType = e.eventType.ToString();
            string strUpPoint = e.strPoint;
            string strUpContent = e.strContent;
            string strUpPeople = e.strPeople;
            string strUpRemark = e.strRemark;
            string strUpTime = e.timeAction.ToString("yyyy-MM-dd HH:mm:ss");

            clsNewEvent newEvent = new clsNewEvent();
            newEvent.StrType = e.eventType.ToString();
            newEvent.StrPoint = e.strPoint;
            newEvent.StrContent = e.strContent;
            newEvent.StrPeople = e.strPeople;
            newEvent.StrRemark = e.strRemark;
            newEvent.StrTime = e.timeAction.ToString("yyyy-MM-dd HH:mm:ss");
            lock (listNetEvent)
            {
                listNetEvent.Add(newEvent);
            }

            gridControl1.BeginInvoke(new InvokeDelegate(updateEventShow));

            //门禁报语音
            if (e.eventType == EventType.门禁)
            {
                if (e.strContent == DoorsState.开门.ToString())
                {
                    string str = config.AppConfigGet("VoiceGroupName");
                    str = "您好，欢迎进入，" + str;
                    lock (MainControl.listVoice)
                    {
                        MainControl.listVoice.Add(str);
                    }

                }
            }
            else if (e.eventType == EventType.工具借还)
            {
                string strVoice = e.strPoint + e.strContent;
                lock (MainControl.listVoice)
                {
                    MainControl.listVoice.Add(strVoice);
                }


                #region 更新树结构

                bool blHas = false;
                for (int iIndex = 0; iIndex < listNewTree.Count; iIndex++)
                {
                    if (listNewTree[iIndex].StrToolId == strUpPoint)
                    {
                        if (listNewTree[iIndex].StrContent != strUpContent)
                        {
                            lock (listNewTree)
                            {
                                listNewTree[iIndex].StrContent = strUpContent;
                            }
                        }
                        if (listNewTree[iIndex].BlShow != false)
                        {
                            lock (listNewTree)
                            {
                                listNewTree[iIndex].BlShow = false;
                            }
                        }
                        blHas = true;
                        break;
                    }
                }
                if (blHas == false)
                {
                    clsNewTreeView newTree = new clsNewTreeView(strUpPoint, strUpContent, false);
                    lock (listNewTree)
                    {
                        listNewTree.Add(newTree);
                    }
                }

                #endregion

            }




            //上传
            if (UsingUp == DeviceUsing.启用)
            {
                string str = "insert into tb_NewEvent (Addr,Type,Point,Content,People,Time,IsRead)" +
                          "values ('" + infoOfSystem.strServerAddr + "','" + strUpEventType + "','" + strUpPoint + "','" + strUpContent + "'" +
                          ",'" + strUpPeople + "','" + strUpTime + "','" + ServerIsRead.未读.ToString() + "')";
                stationServer.SqlComNonQuery(str);
            }

            iNoEvent++;
            if (iNoEvent >= 100)
                iNoEvent = 0;
        }

        private void updateEventShow()
        {
            try
            {
                if (listNetEvent.Count > 0)
                {
                    DataRow dr = dtEventInfo.NewRow();
                    lock (listNetEvent)
                    {
                        dr["Type"] = listNetEvent[0].StrType;
                        dr["Point"] = listNetEvent[0].StrPoint;
                        dr["Content"] = listNetEvent[0].StrContent;
                        dr["People"] = listNetEvent[0].StrPeople;
                        dr["Remark"] = listNetEvent[0].StrRemark;
                        dr["Time"] = listNetEvent[0].StrTime;
                        listNetEvent.RemoveAt(0);
                    }

                    dr["No"] = iNoEvent.ToString();
                    dr["IsClick"] = "";
                    dtEventInfo.Rows.Add(dr);



                    // 多过100 条 自动删除
                    if (dtEventInfo.Rows.Count > 100)
                    {
                        int iMore = dtEventInfo.Rows.Count - 100;
                        for (int iIndex = 0; iIndex < iMore; iIndex++)
                        {
                            dtEventInfo.Rows.RemoveAt(0);
                        }
                    }
                    gridView1.MoveLast();
                    this.gridView1.BestFitColumns();
                }

            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        //EventType eventType = e.eventType;
        //string strPoint = e.strPoint;
        //string strContent = e.strContent;
        //string strPeople = e.strPeople;
        //string strRemark = e.strRemark;
        //string strTime = e.timeAction.ToString("yyyy-MM-dd HH:mm:ss");
        //EventTableDeal(eventType, strPoint, strContent, strPeople, strRemark, strTime);
        //private void EventTableDeal(EventType eventType, string strPoint, string strContent, string strPeople, string strRemark,string strTime)
        //{
        //    try
        //    {
        //        DataRow dr = dtEventInfo.NewRow();
        //        dr["Type"] = eventType.ToString();
        //        dr["Point"] = strPoint;
        //        dr["Content"] = strContent;
        //        dr["People"] = strPeople;
        //        dr["Remark"] = strRemark;
        //        dr["Time"] = strTime;
        //        dr["No"] = iNoEvent.ToString();
        //        dr["IsClick"] = "";

        //        gridControl1.BeginInvoke(new InvokeDelegate(updateEventShow));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (frmMain.blDebug)
        //            MessageUtil.ShowTips(ex.Message);
        //    } 
        //}

        #endregion

        #region  系统 初始化 等 子程序 

        #region  工具柜

        /// <summary>
        /// 加载所有工具柜
        /// </summary>
        public void LoadBox()
        {
            try
            {
                #region  工具柜门禁

                if (UsingBox == DeviceUsing.启用)
                {
                    if (boxDoorWatching != null)
                    {
                        boxDoorWatching.ClearWgList();
                    }
                }

                string strSql = "select tvChildId,AreaName,PlaceName,DoorIp,DoorSn,BoxHasRfid,BoxRfidMain,wgPort from tb_Tools where  " +
                                "IsArea='" + ToolAreaType.工具柜.ToString() + "' and HasDoor='" + DeviceUsing.启用.ToString() + "'  ";
                DataTable dt = datalogic.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {
                    UsingBox = DeviceUsing.启用;
                    if (boxDoorWatching == null)
                    {
                        boxDoorWatching = new clsWgRunWatch();
                        boxDoorWatching.NewEventShowEvent += new NewEventShowEventHandler(boxDoorWatching_NewEventShowEvent);
                        boxDoorWatching.NewAlarmShowEvent += new NewEventShowEventHandler(boxDoorWatching_NewAlarmShowEvent);
                    }

                    foreach (DataRow datarow in dt.Rows)
                    {
                        string strChildId = datarow["tvChildId"].ToString();
                        string strArea = datarow["AreaName"].ToString();
                        string strName = datarow["PlaceName"].ToString();
                        string strIp = datarow["DoorIp"].ToString();
                        string strPort = datarow["wgPort"].ToString();
                        string strSn = datarow["DoorSn"].ToString();
                        string strHasRfid = datarow["BoxHasRfid"].ToString();
                        string strRfidMain = datarow["BoxRfidMain"].ToString();
                        int iSn = 0;
                        int iPort = 60000;
                        if (!string.IsNullOrEmpty(strSn))
                        {
                            iSn = Convert.ToInt32(strSn);
                        }
                        else
                        {
                            MessageUtil.ShowTips(strName + " 工具柜门禁SN为空");
                        }
                        if (!string.IsNullOrEmpty(strPort))
                        {
                            iPort = Convert.ToInt32(strPort);
                        }
                        else
                        {
                            MessageUtil.ShowTips(strName + " 工具柜门禁 端口号 为空");
                        }

                        clsWgInfo wgInfo = new clsWgInfo(strIp, iPort, iSn);
                        wgInfo.StrNameOfWg = strName;
                        wgInfo.DoorOrBoxDoor = WgDoorType.工具柜;
                        wgInfo.StrChildId = strChildId;
                        clsDoorInfo door = new clsDoorInfo(strName, 1);
                        wgInfo.listDoor.Add(door);

                        boxDoorWatching.listWg.Add(wgInfo);

                        //boxDoor.StrChildId = strChildId;
                        //boxDoor.StrHasRfid = strHasRfid;
                        //boxDoor.StrRfidMain = strRfidMain;
                    }
                }
                else
                {
                    UsingBox = DeviceUsing.未启用;
                }

                if (UsingBox == DeviceUsing.启用)
                {
                    boxDoorWatching.InitWatch();
                }
                else
                {
                    if (boxDoorWatching != null)
                    {
                        boxDoorWatching.NewEventShowEvent -= new NewEventShowEventHandler(boxDoorWatching_NewEventShowEvent);
                        boxDoorWatching.NewAlarmShowEvent -= new NewEventShowEventHandler(boxDoorWatching_NewAlarmShowEvent);
                        boxDoorWatching.ClearWgList();
                        boxDoorWatching.DisposeAndClear();
                        boxDoorWatching = null;
                    }
                }

                #endregion

                #region

                //#region 工具柜RFID

                //if (listBoxRfid.Count > 0)
                //{
                //    lock (listBoxRfid)
                //    {
                //        for (int iIndexRfid = 0; iIndexRfid < listBoxRfid.Count; iIndexRfid++)
                //        {
                //            if (listBoxRfid[iIndexRfid] != null)
                //            {
                //                if (listBoxRfid[iIndexRfid].StateMainRead != BoxRfidState.不读)
                //                    listBoxRfid[iIndexRfid].StateMainRead = BoxRfidState.不读;
                //                if (listBoxRfid[iIndexRfid].StateSlaveRead != BoxRfidState.不读)
                //                    listBoxRfid[iIndexRfid].StateSlaveRead = BoxRfidState.不读;
                //                if (listBoxRfid[iIndexRfid].timerRfidFast.State == TimerState.Running)
                //                    listBoxRfid[iIndexRfid].StopRead();
                //                if (listBoxRfid[iIndexRfid].BlConnent)
                //                {
                //                    listBoxRfid[iIndexRfid].CloseNetPort();
                //                }
                //                if (listBoxRfid[iIndexRfid].timerRfidFast != null)
                //                    listBoxRfid[iIndexRfid].timerRfidFast = null;
                //                listBoxRfid[iIndexRfid] = null;
                //            }
                //        }
                //        listBoxRfid.Clear();
                //    }
                //}

                //strSql = "select tvChildId,AreaName,PlaceName,BoxRfidIp,BoxRfidPort,BoxRfidMain,BoxRfidAnt1,BoxRfidAnt2,BoxRfidAnt3,BoxRfidAnt4 from tb_Tools where " +
                //         "IsArea='" + ToolAreaType.工具柜.ToString() + "' and BoxHasRfid='" + DeviceUsing.启用.ToString() + "'  ";
                //dt = datalogic.GetDataTable(strSql);
                //foreach (DataRow datarow in dt.Rows)
                //{
                //    string strId = datarow["tvChildId"].ToString();
                //    string strArea = datarow["AreaName"].ToString();
                //    string strName = datarow["PlaceName"].ToString();
                //    string strIp = datarow["BoxRfidIp"].ToString();
                //    string strPort = datarow["BoxRfidPort"].ToString();
                //    string strMain = datarow["BoxRfidMain"].ToString();

                //    #region 天线

                //    DeviceUsing ant1 = DeviceUsing.未启用;
                //    DeviceUsing ant2 = DeviceUsing.未启用;
                //    DeviceUsing ant3 = DeviceUsing.未启用;
                //    DeviceUsing ant4 = DeviceUsing.未启用;

                //    string strAnt = datarow["BoxRfidAnt1"].ToString();
                //    if (strAnt == DeviceUsing.启用.ToString())
                //        ant1 = DeviceUsing.启用;
                //    else
                //        ant1 = DeviceUsing.未启用;
                //    strAnt = datarow["BoxRfidAnt2"].ToString();
                //    if (strAnt == DeviceUsing.启用.ToString())
                //        ant2 = DeviceUsing.启用;
                //    else
                //        ant2 = DeviceUsing.未启用;
                //    strAnt = datarow["BoxRfidAnt3"].ToString();
                //    if (strAnt == DeviceUsing.启用.ToString())
                //        ant3 = DeviceUsing.启用;
                //    else
                //        ant3 = DeviceUsing.未启用;
                //    strAnt = datarow["BoxRfidAnt4"].ToString();
                //    if (strAnt == DeviceUsing.启用.ToString())
                //        ant4 = DeviceUsing.启用;
                //    else
                //        ant4 = DeviceUsing.未启用;

                //    #endregion

                //    bool blHas = false;

                //    int iPort = 0;
                //    if (!string.IsNullOrEmpty(strPort))
                //    {
                //        iPort = Convert.ToInt32(strPort);
                //    }
                //    else
                //    {
                //        MessageUtil.ShowTips(strName + " 工具柜RFID端口号为空");
                //    }

                //    #region  已有

                //    if (listBoxRfid.Count > 0)
                //    {
                //        int iCount = listBoxRfid.Count;
                //        for (int iIndex = 0; iIndex < iCount; iIndex++)
                //        {
                //            if (listBoxRfid[iIndex].StrIp == strIp && listBoxRfid[iIndex].IPort == iPort)
                //            {
                //                blHas = true;
                //                if (listBoxRfid[iIndex].StrChildIdMain == null)
                //                {
                //                    listBoxRfid[iIndex].StrChildIdMain = strId;
                //                    listBoxRfid[iIndex].StrNameMain = strName;
                //                    listBoxRfid[iIndex].AntMain1 = ant1;
                //                    listBoxRfid[iIndex].AntMain2 = ant2;
                //                }
                //                else if (listBoxRfid[iIndex].StrChildIdSlave == null)
                //                {
                //                    listBoxRfid[iIndex].StrChildIdSlave = strId;
                //                    listBoxRfid[iIndex].StrNameSlave = strName;
                //                    listBoxRfid[iIndex].AntSlave1 = ant3;
                //                    listBoxRfid[iIndex].AntSlave2 = ant4;
                //                }
                //            }
                //        }
                //    }

                //    #endregion

                //    #region  无，添加

                //    if (blHas == false)
                //    {
                //        RfidReadBox rfidRead = new RfidReadBox();
                //        rfidRead.StrIp = strIp;
                //        rfidRead.IPort = iPort;
                //        if (strMain == BoxRfidMain.主机.ToString())
                //        {
                //            rfidRead.StrChildIdMain = strId;
                //            rfidRead.StrNameMain = strName;
                //            rfidRead.AntMain1 = ant1;
                //            rfidRead.AntMain2 = ant2;
                //        }
                //        else if (strMain == BoxRfidMain.从机.ToString())
                //        {
                //            rfidRead.StrChildIdSlave = strId;
                //            rfidRead.StrNameSlave = strName;
                //            rfidRead.AntSlave1 = ant3;
                //            rfidRead.AntSlave2 = ant4;
                //        }
                //        lock (listBoxRfid)
                //        {
                //            listBoxRfid.Add(rfidRead);
                //        }
                //    }

                //    #endregion

                //}
                ////设置工具柜RFID天线



                ////for (int iIndex = 0; iIndex < listBoxRfid.Count; iIndex++)
                ////{
                ////    DeviceUsing ant1 = listBoxRfid[iIndex].AntMain1;
                ////    DeviceUsing ant2 = listBoxRfid[iIndex].AntMain2;
                ////    DeviceUsing ant3 = listBoxRfid[iIndex].AntSlave1;
                ////    DeviceUsing ant4 = listBoxRfid[iIndex].AntSlave2;
                ////}

                ////if (listBoxDoor.Count > 0)
                ////{
                ////    //timerBoxDoor.Start();
                ////}
                ////else
                ////{
                ////    //timerBoxDoor.Stop();
                ////}

                //#endregion

                #endregion
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        /// <summary>
        /// 工具柜 门禁暂停 监控
        /// </summary>
        private void StopBoxWatch()
        {
            if (UsingBox == DeviceUsing.启用)
            {
                if (boxDoorWatching != null)
                    boxDoorWatching.StopTimer();
            }
        }

        /// <summary>
        /// 工具柜 暂停后 启动 监控
        /// </summary>
        private void StartBoxWatch()
        {
            if (UsingBox == DeviceUsing.启用)
            {
                if (boxDoorWatching != null)
                    boxDoorWatching.StopTimer();
            }
        }

        #endregion

        #region  门禁

        /// <summary>
        /// 门禁暂停 监控
        /// </summary>
        private void StopDoorWatch()
        {
            if (UsingDoor == DeviceUsing.启用)
            {
                if (wgRunWatch != null)
                {
                    wgRunWatch.StopTimer();
                }
            }
            if (UsingFinger == DeviceUsing.启用)
            {
                if (zkControl != null)
                    zkControl.Stop();
            }
        }

        /// <summary>
        /// 门禁暂停后 启动 监控
        /// </summary>
        private void StartDoorWatch()
        {
            if (UsingDoor == DeviceUsing.启用)
            {
                if (wgRunWatch != null)
                {
                    wgRunWatch.StartTimer();
                }
            }
            if (UsingFinger == DeviceUsing.启用)
            {
                if (zkControl != null)
                    zkControl.Start();
            }
        }

        #endregion

        /// <summary>
        /// 加载 系统设置 
        /// </summary>
        public void LoadSysSet()
        {
            try
            {

                #region   加载 系统设置

                #region  SQL数据库

                string strSql = "select ID,DoorUsing,DoorSN,DoorIP,DoorCount,RfidUsing1,RfidUsing2,RfidIp1,RfidIp2,RfidPort1,RfidPort2,OverDayBorr," +
                                "BorrRetSpan,ServerAddr,HumiRun,HumiStop,HandKeepTime,AirCoolRun,AirCoolStop,AirCoolTempSet,AirHotRun,AirHotStop," +
                                "AirHotTempSet,DoorName1,DoorName2,ServerUsing,EnvirUsing,DoorType,FingerDoorIp,RfidBoxTime,FingerPort,wgPort" +
                                ",WgDoorName3,WgDoorName4,UsingFinger,FingerDoorName,IcDoorOfRfid1,IcDoorOfRfid2,IcDoorOfRfid3,IcDoorOfRfid4" +
                                ",FingerDoorOfRfid,Rfid1No,Rfid2No,BorrOver,ErrInfo,UsingFinger2,FingerDoorIp2,FingerPort2,FingerDoorName2,FingerDoorOfRfid2 from tb_SysDevice ";
                DataTable dt = datalogic.GetDataTable(strSql);//wgPort,WgDoorName3,WgDoorName4,UsingFinger,FingerDoorName
                if (dt.Rows.Count > 0)
                {
                    #region 门禁

                    #region IC 门禁控制器

                    //MessageUtil.ShowTips("门禁控制器");
                    string str = dt.Rows[0]["DoorUsing"].ToString();
                    if (str == DeviceUsing.启用.ToString())
                    {
                        if (wgRunWatch == null)
                        {
                            wgRunWatch = new clsWgRunWatch();
                            wgRunWatch.NewEventShowEvent += new NewEventShowEventHandler(wgRunWatch_NewEventShowEvent);
                            wgRunWatch.NewAlarmShowEvent += new NewEventShowEventHandler(wgRunWatch_NewAlarmShowEvent);
                        }
                        else
                        {
                            wgRunWatch.ClearWgList();
                        }

                        frmMain.UsingDoor = DeviceUsing.启用;

                        #region  门禁控制器参数 如IP、SN

                        int iSn = 0;
                        string strIp = "";
                        int iPort = 60000;
                        strIp = dt.Rows[0]["DoorIP"].ToString();
                        if (string.IsNullOrEmpty(strIp))
                        {
                            if (frmMain.blDebug)
                                MessageUtil.ShowTips("IC门禁 IP为空");
                        }
                        str = dt.Rows[0]["wgPort"].ToString();
                        if (string.IsNullOrEmpty(str))
                        {
                            if (frmMain.blDebug)
                                MessageUtil.ShowTips("IC门禁 端口号为空");
                        }
                        else
                            iPort = Convert.ToInt32(str);
                        str = dt.Rows[0]["DoorSN"].ToString();
                        if (string.IsNullOrEmpty(str))
                        {
                            if (frmMain.blDebug)
                                MessageUtil.ShowTips("IC门禁 SN为空");
                        }
                        else
                            iSn = Convert.ToInt32(str);
                        clsWgInfo wgInfo = new clsWgInfo(strIp, iPort, iSn);
                        wgInfo.StrNameOfWg = "IC门禁控制器";
                        wgInfo.DoorOrBoxDoor = WgDoorType.门禁;

                        #endregion

                        #region  门定义
                        //MessageUtil.ShowTips("门定义");
                        str = dt.Rows[0]["DoorName1"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsDoorInfo door = new clsDoorInfo(str, 1);
                            str = dt.Rows[0]["IcDoorOfRfid1"].ToString();//IcDoorOfRfid1,IcDoorOfRfid2,IcDoorOfRfid3,IcDoorOfRfid4,FingerDoorOfRfid
                            if (str == DeviceUsing.启用.ToString())
                                door.IsOfRfid = DeviceUsing.启用;
                            else
                                door.IsOfRfid = DeviceUsing.未启用;
                            wgInfo.listDoor.Add(door);
                        }
                        str = dt.Rows[0]["DoorName2"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsDoorInfo door = new clsDoorInfo(str, 2);
                            str = dt.Rows[0]["IcDoorOfRfid2"].ToString();
                            if (str == DeviceUsing.启用.ToString())
                                door.IsOfRfid = DeviceUsing.启用;
                            else
                                door.IsOfRfid = DeviceUsing.未启用;
                            wgInfo.listDoor.Add(door);
                        }
                        str = dt.Rows[0]["WgDoorName3"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsDoorInfo door = new clsDoorInfo(str, 3);
                            str = dt.Rows[0]["IcDoorOfRfid3"].ToString();
                            if (str == DeviceUsing.启用.ToString())
                                door.IsOfRfid = DeviceUsing.启用;
                            else
                                door.IsOfRfid = DeviceUsing.未启用;
                            wgInfo.listDoor.Add(door);
                        }
                        str = dt.Rows[0]["WgDoorName4"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsDoorInfo door = new clsDoorInfo(str, 4);
                            str = dt.Rows[0]["IcDoorOfRfid4"].ToString();
                            if (str == DeviceUsing.启用.ToString())
                                door.IsOfRfid = DeviceUsing.启用;
                            else
                                door.IsOfRfid = DeviceUsing.未启用;
                            wgInfo.listDoor.Add(door);
                        }

                        #endregion

                        wgRunWatch.listWg.Add(wgInfo);
                        wgRunWatch.InitWatch();
                    }
                    else
                    {
                        frmMain.UsingDoor = DeviceUsing.未启用;//  wgRunWatch_NewEventShowEvent wgRunWatch_NewAlarmShowEvent
                        if (wgRunWatch != null)
                        {
                            wgRunWatch.NewEventShowEvent -= new NewEventShowEventHandler(wgRunWatch_NewEventShowEvent);
                            wgRunWatch.NewAlarmShowEvent -= new NewEventShowEventHandler(wgRunWatch_NewAlarmShowEvent);
                            wgRunWatch.ClearWgList();
                            wgRunWatch.DisposeAndClear();
                            wgRunWatch = null;
                        }
                    }

                    #endregion
                    //MessageUtil.ShowTips("指纹");
                    #region  指纹

                    str = dt.Rows[0]["UsingFinger"].ToString();
                    string fingerdoor2 = dt.Rows[0]["UsingFinger2"].ToString();
                    if (str == DeviceUsing.启用.ToString() || fingerdoor2==DeviceUsing.启用.ToString())
                    {
                        UsingFinger = DeviceUsing.启用;
                        if (zkControl == null)
                        {
                            zkControl = new clsZkControl();
                            zkControl.NewEventShowEvent += new NewEventShowEventHandler(zkControl_NewEventShowEvent);
                            zkControl.NewAlarmShowEvent += new NewEventShowEventHandler(zkControl_NewAlarmShowEvent);
                        }
                        else
                        {
                            zkControl.ClearZkList();
                        }
                        string strFingerIp = dt.Rows[0]["FingerDoorIp"].ToString();
                        string strFinerPort = dt.Rows[0]["FingerPort"].ToString();
                        int iPort = 0;
                        string strName = dt.Rows[0]["FingerDoorName"].ToString();
                        if (string.IsNullOrEmpty(strFingerIp))
                        {
                            if (frmMain.blDebug)
                                MessageUtil.ShowTips("指纹门禁 IP为空");
                        }
                        if (string.IsNullOrEmpty(strFinerPort))
                        {
                            if (frmMain.blDebug)
                                MessageUtil.ShowTips("指纹门禁 端口 为空");
                        }
                        else
                        {
                            iPort = Convert.ToInt32(strFinerPort);
                        }
                        if (string.IsNullOrEmpty(strName))
                        {
                            if (frmMain.blDebug)
                                MessageUtil.ShowTips("指纹门禁 门名称 为空");
                        }
                        if (zkControl != null)
                        {
                            if (str == DeviceUsing.启用.ToString())
                            {
                                clsZkDoor zkDoor = new clsZkDoor();
                                zkDoor.StrIp = strFingerIp;
                                zkDoor.IPort = iPort;
                                zkDoor.StrNameOfZk = strName;
                                zkDoor.doorInfo.StrDoorName = strName;
                                str = dt.Rows[0]["FingerDoorOfRfid"].ToString();
                                if (str == DeviceUsing.启用.ToString())
                                    zkDoor.doorInfo.IsOfRfid = DeviceUsing.启用;
                                else
                                    zkDoor.doorInfo.IsOfRfid = DeviceUsing.未启用;

                                zkControl.listZk.Add(zkDoor);
                            }
                            if (fingerdoor2 == DeviceUsing.启用.ToString())
                            {
                                string strFingerIp2 = dt.Rows[0]["FingerDoorIp2"].ToString();
                                string strFinerPort2 = dt.Rows[0]["FingerPort2"].ToString();
                                int iPort2 = Convert.ToInt32(strFinerPort2);
                                string strName2 = dt.Rows[0]["FingerDoorName2"].ToString();
                                clsZkDoor zkDoor = new clsZkDoor();
                                zkDoor.StrIp = strFingerIp2;
                                zkDoor.IPort = iPort2;
                                zkDoor.StrNameOfZk = strName2;
                                zkDoor.doorInfo.StrDoorName = strName2;
                                str = dt.Rows[0]["FingerDoorOfRfid2"].ToString();
                                if (str == DeviceUsing.启用.ToString())
                                    zkDoor.doorInfo.IsOfRfid = DeviceUsing.启用;
                                else
                                    zkDoor.doorInfo.IsOfRfid = DeviceUsing.未启用;

                                zkControl.listZk.Add(zkDoor);
                            }                           
                        }
                        zkControl.Start();
                    }
                    else
                    {
                        UsingFinger = DeviceUsing.未启用;
                        if (zkControl != null)
                        {
                            zkControl.NewEventShowEvent -= new NewEventShowEventHandler(zkControl_NewEventShowEvent);
                            zkControl.NewAlarmShowEvent -= new NewEventShowEventHandler(zkControl_NewAlarmShowEvent);
                            zkControl.ClearZkList();
                            zkControl = null;
                        }
                    }


                    #endregion

                    #endregion
                    //MessageUtil.ShowTips("环境");
                    #region  环境

                    str = dt.Rows[0]["EnvirUsing"].ToString();
                    if (str == DeviceUsing.启用.ToString())
                    {                        
                        UsingEnvir = DeviceUsing.启用;
                        str = dt.Rows[0]["HumiRun"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.intHumiRun = Convert.ToInt32(str);
                        }
                        str = dt.Rows[0]["HumiStop"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.intHumiStop = Convert.ToInt32(str);
                        }
                        str = dt.Rows[0]["HandKeepTime"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.iHandHoldTime = Convert.ToInt32(str);
                        }
                        str = dt.Rows[0]["AirCoolRun"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.intAirCoolRun = Convert.ToInt32(str);
                        }
                        str = dt.Rows[0]["AirCoolStop"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.intAirCoolStop = Convert.ToInt32(str);
                        }
                        str = dt.Rows[0]["AirCoolTempSet"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.intSetTempCool = Convert.ToInt32(str);
                        }
                        str = dt.Rows[0]["AirHotRun"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.intAirHotRun = Convert.ToInt32(str);
                        }
                        str = dt.Rows[0]["AirHotStop"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.intAirHotStop = Convert.ToInt32(str);
                        }
                        str = dt.Rows[0]["AirHotTempSet"].ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            clsEnvirSet.intSetTempHot = Convert.ToInt32(str);
                        }

                        if (envirControl == null)
                        {
                            envirControl = new clsEnvirControl();
                            envirControl.NewAlarmShowEvent += new NewEventShowEventHandler(envirControl_NewAlarmShowEvent);
                            envirControl.serialAir.NewEventShowEvent += new NewEventShowEventHandler(serialAir_NewEventShowEvent);
                            envirControl.serialIo.NewEventShowEvent += new NewEventShowEventHandler(serialIo_NewEventShowEvent);
                            //boxDoorWatching.NewEventShowEvent += new NewEventShowEventHandler(boxDoorWatching_NewEventShowEvent);
                        }
                    }
                    else
                    {
                        if (envirControl != null)
                        {
                            envirControl.NewAlarmShowEvent -= new NewEventShowEventHandler(envirControl_NewAlarmShowEvent);
                            envirControl.serialAir.NewEventShowEvent -= new NewEventShowEventHandler(serialAir_NewEventShowEvent);
                            envirControl.serialIo.NewEventShowEvent -= new NewEventShowEventHandler(serialIo_NewEventShowEvent);
                            envirControl.Clear();
                            envirControl = null;
                        }
                        UsingEnvir = DeviceUsing.未启用;
                    }
                    //MessageBox.Show(UsingEnvir.ToString());
                    #endregion

                    if (envirControl != null)
                    {
                        envirControl.serialIo.OnOffDevice(1, OnOffRelay.关闭, DeviceRelayNo.RFID, "", DeviceRunModel.自动, false, IsWait.OnlyWait);
                    }

                    #region RFID 大门


                    // RFID1
                    string rfidUsing1 = dt.Rows[0]["RfidUsing1"].ToString();
                    string strRfidIp1 = dt.Rows[0]["RfidIp1"].ToString();
                    string strRfidPort1 = dt.Rows[0]["RfidPort1"].ToString();
                    string strRfidNo1 = dt.Rows[0]["Rfid1No"].ToString();
                    //RFID2
                    string rfidUsing2 = dt.Rows[0]["RfidUsing2"].ToString();
                    string strRfidIp2 = dt.Rows[0]["RfidIp2"].ToString();
                    string strRfidPort2 = dt.Rows[0]["RfidPort2"].ToString();
                    string strRfidNo2 = dt.Rows[0]["Rfid2No"].ToString();

                    if (rfidUsing1 == DeviceUsing.启用.ToString() || rfidUsing2 == DeviceUsing.启用.ToString())
                    {
                        infoOfSystem.usingRfid = DeviceUsing.启用;
                        if(rfidManage==null)
                        {
                            rfidManage = new RfidManage();
                            rfidManage.NewEventShowEvent += new NewEventShowEventHandler(rfidRead_NewEventShowEvent);
                            rfidManage.NewAlarmShowEvent += new NewEventShowEventHandler(rfidRead_NewAlarmShowEvent);
                        }
                        else
                        {
                            //rfidManage.StopRfid();
                        }
                        //if (rfidRead == null)
                        //{
                        //    rfidRead = new clsRfidRead();
                        //    rfidRead.NewEventShowEvent += new NewEventShowEventHandler(rfidRead_NewEventShowEvent);//rfidRead_NewEventShowEvent rfidRead_NewAlarmShowEvent
                        //    rfidRead.NewAlarmShowEvent += new NewEventShowEventHandler(rfidRead_NewAlarmShowEvent);
                        //}
                        //else
                        //{
                        //    rfidRead.ClearListRfid();
                        //}
                        #region 1#rfid

                        if (rfidUsing1 == DeviceUsing.启用.ToString())
                        {
                            int iPort = 0;
                            string strName = "";
                            if (string.IsNullOrEmpty(strRfidIp1))
                            {
                                if (frmMain.blDebug)
                                    MessageUtil.ShowTips("1# RFID读写器 IP为空");
                            }
                            if (string.IsNullOrEmpty(strRfidPort1))
                            {
                                if (frmMain.blDebug)
                                    MessageUtil.ShowTips("1# RFID读写器 端口号为空");
                            }
                            else
                            {
                                iPort = Convert.ToInt32(strRfidPort1);
                            }
                            if (rfidUsing1 == DeviceUsing.启用.ToString() && rfidUsing2 == DeviceUsing.启用.ToString())
                            {
                                strName = "1# RFID读写器";
                            }
                            //clsRfid rfid = new clsRfid(strRfidIp1, iPort, strName);
                            //if (strRfidNo1 == DeviceUsing.启用.ToString())
                            //    rfid.UsingReadNo = DeviceUsing.启用;
                            //rfidRead.listRfid.Add(rfid);
                            SingleRfid sr = new SingleRfid(strRfidIp1, iPort, strName);
                            if (strRfidNo1 == DeviceUsing.启用.ToString())
                                sr.UsingReadNo = DeviceUsing.启用;
                            rfidManage.Listsrfid.Add(sr);
                        }

                        #endregion

                        #region 2#rfid

                        if (rfidUsing2 == DeviceUsing.启用.ToString())
                        {
                            int iPort = 0;
                            string strName = "";
                            if (string.IsNullOrEmpty(strRfidIp2))
                            {
                                if (frmMain.blDebug)
                                    MessageUtil.ShowTips("2# RFID读写器 IP为空");
                            }
                            if (string.IsNullOrEmpty(strRfidPort2))
                            {
                                if (frmMain.blDebug)
                                    MessageUtil.ShowTips("2# RFID读写器 端口号为空");
                            }
                            else
                            {
                                iPort = Convert.ToInt32(strRfidPort2);
                            }
                            if (rfidUsing1 == DeviceUsing.启用.ToString() && rfidUsing2 == DeviceUsing.启用.ToString())
                            {
                                strName = "2# RFID读写器";
                            }
                            //clsRfid rfid = new clsRfid(strRfidIp2, iPort, strName);
                            //if (strRfidNo2 == DeviceUsing.启用.ToString())
                            //    rfid.UsingReadNo = DeviceUsing.启用;
                            //rfidRead.listRfid.Add(rfid);
                            SingleRfid sr = new SingleRfid(strRfidIp2, iPort, strName);
                            if (strRfidNo1 == DeviceUsing.启用.ToString())
                                sr.UsingReadNo = DeviceUsing.启用;
                            rfidManage.Listsrfid.Add(sr);
                        }

                        #endregion

                        //rfidRead.timerStart();
                        rfidManage.BindEvent();
                        rfidManage.timerStart();
                        
                    }
                    else
                    {
                        infoOfSystem.usingRfid = DeviceUsing.未启用;
                        if (rfidManage != null)
                        {
                            rfidManage.StopRfid();
                            rfidManage.NewEventShowEvent -= new NewEventShowEventHandler(rfidRead_NewEventShowEvent);
                            rfidManage.NewAlarmShowEvent -= new NewEventShowEventHandler(rfidRead_NewAlarmShowEvent);
                            rfidManage = null;
                        }
                    }

                    #endregion

                    #region  RFID工具柜

                    //str = dt.Rows[0]["RfidBoxTime"].ToString();
                    //if (!string.IsNullOrEmpty(str))
                    //    iBoxScanTime = Convert.ToInt32(str);

                    #endregion

                    #region  本机服务地址等

                    str = dt.Rows[0]["ServerUsing"].ToString();
                    if (str == DeviceUsing.启用.ToString())
                    {
                        frmMain.UsingUp = DeviceUsing.启用;
                        str = dt.Rows[0]["ServerAddr"].ToString();
                        if (string.IsNullOrEmpty(str))
                        {
                            if (frmMain.blDebug)
                                MessageUtil.ShowTips("本机服务地址为空");
                        }
                        else
                        {
                            infoOfSystem.strServerAddr = str;
                        }
                    }
                    else
                    {
                        frmMain.UsingUp = DeviceUsing.未启用;
                    }

                    #endregion

                    #region 其他

                    str = dt.Rows[0]["OverDayBorr"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        infoOfSystem.iOverDayBorr = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["BorrRetSpan"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        infoOfSystem.iBorrRetSpan = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["BorrOver"].ToString();
                    if (str == DeviceUsing.启用.ToString())
                        infoOfSystem.usingBorrOver = DeviceUsing.启用;
                    else
                        infoOfSystem.usingBorrOver = DeviceUsing.未启用;
                    str = dt.Rows[0]["ErrInfo"].ToString();
                    if (str == DeviceUsing.启用.ToString())
                        infoOfSystem.usingOfErr = DeviceUsing.启用;
                    else
                        infoOfSystem.usingOfErr = DeviceUsing.未启用;

                    #endregion
                }

                #endregion

                #region XML系统设置


                string strXml = config.AppConfigGet("IsUserPowReader");
                if (strXml == DeviceUsing.启用.ToString())
                {
                    if (powReader == null)
                    {
                        powReader = new PowerReader();
                        powReader.NewEventShowEvent += new NewEventShowEventHandler(powReader_NewEventShowEvent);
                        powReader.NewAlarmShowEvent += new NewEventShowEventHandler(powReader_NewAlarmShowEvent);
                    }
                    else
                    {
                        powReader.LoadSet();
                        powReader.ConnectReader();
                    }
                }
                else
                {
                    if (powReader != null)
                    {
                        powReader.NewEventShowEvent -= new NewEventShowEventHandler(powReader_NewEventShowEvent);
                        powReader.NewAlarmShowEvent -= new NewEventShowEventHandler(powReader_NewAlarmShowEvent);
                        powReader.StopRece();
                        powReader = null;
                    }
                }
                strXml = config.AppConfigGet("MarkUsing");
                if (strXml == DeviceUsing.启用.ToString())
                    bbtnMark.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    bbtnMark.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


                #endregion

                #endregion
            }
            catch (Exception ex)
            {              
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        /// <summary>
        ///  系统参数 设置 
        /// </summary>
        public void SysParameterSet()
        {
            try
            {
                string strSql = "select ID,OverDayBorr,BorrRetSpan from tb_SysDevice ";
                DataTable dt = datalogic.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {
                    string str = dt.Rows[0]["OverDayBorr"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        infoOfSystem.iOverDayBorr = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["BorrRetSpan"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        infoOfSystem.iBorrRetSpan = Convert.ToInt32(str);
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
        ///  环境设置 
        /// </summary>
        public void LoadEnvirSet()
        {
            try
            {
                string strSql = "select ID,HumiRun,HumiStop,HandKeepTime,AirCoolRun,AirCoolStop,AirCoolTempSet,AirHotRun,AirHotStop," +
                                "AirHotTempSet from tb_SysDevice ";
                DataTable dt = datalogic.GetDataTable(strSql);
                if (dt.Rows.Count > 0)
                {

                    string str = dt.Rows[0]["HumiRun"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.intHumiRun = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["HumiStop"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.intHumiStop = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["HandKeepTime"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.iHandHoldTime = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["AirCoolRun"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.intAirCoolRun = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["AirCoolStop"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.intAirCoolStop = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["AirCoolTempSet"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.intSetTempCool = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["AirHotRun"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.intAirHotRun = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["AirHotStop"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.intAirHotStop = Convert.ToInt32(str);
                    }
                    str = dt.Rows[0]["AirHotTempSet"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        clsEnvirSet.intSetTempHot = Convert.ToInt32(str);
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        private void ShowUser()
        {
            string str = "";
            if (systemPower == UserPower.未注册)
            {
                str = UserPower.未注册.ToString();
                bbtnLogin.Caption = LoginBtnShow.系统登录.ToString();
            }
            else
            {
                bbtnLogin.Caption = LoginBtnShow.系统注销.ToString();
                str = strUserName;
                str += ("  (" + systemPower.ToString() + ")");
            }
            btxtUser.Caption = str;
        }

        /// <summary>
        /// 功能模块设置 有无 门禁、环境控制、上传
        /// </summary>
        private void FunctionModeSet()
        {
            //门禁
            if (UsingDoor == DeviceUsing.启用 || UsingFinger == DeviceUsing.启用)
            {
                rPageDoor.Visible = true;

                if (UsingDoor == DeviceUsing.启用)
                    bbtnDoorPass.Enabled = true;
                else
                    bbtnDoorPass.Enabled = false;
            }
            else
            {
                rPageDoor.Visible = false;
            }

            //环境管理
            if (UsingEnvir == DeviceUsing.启用)
            {
                rPageEnvir.Visible = true;
                bbtnRoom.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                rPageEnvir.Visible = false;
                bbtnRoom.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            //  工具柜
            if (UsingBox == DeviceUsing.启用)
            {
                rGroupBoxManage.Visible = true;
            }
            else
            {
                rGroupBoxManage.Visible = false;
            }
        }

        private void PowerSeting()
        {
            if (systemPower == UserPower.系统用户)
            {
                bbtnExit.Enabled = true;
                sbtnSysExit.Enabled = true;
                bbtnInRoom.Enabled = true;
                bbtnToolsType.Enabled = true;
                bbtnPlace.Enabled = true;
                bbtnDoorUser.Enabled = true;
                bbtnDoorPass.Enabled = true;
                bbtnVideoBack.Enabled = true;
                bbtnTestIn.Enabled = true;
                bbtnUsers.Enabled = true;
                bbtnDevice.Enabled = true;
                bbtnDataBack.Enabled = true;
                bbtnDataReturn.Enabled = true;
                bbtnDataClear.Enabled = true;
            }
            else if (systemPower == UserPower.普通用户)
            {
                bbtnInRoom.Enabled = false;
                bbtnToolsType.Enabled = false;
                bbtnPlace.Enabled = false;
                bbtnDoorUser.Enabled = false;
                bbtnDoorPass.Enabled = false;
                bbtnVideoBack.Enabled = false;
                bbtnTestIn.Enabled = false;
                bbtnUsers.Enabled = false;
                bbtnDevice.Enabled = false;
                bbtnDataBack.Enabled = false;
                bbtnDataReturn.Enabled = false;
                bbtnDataClear.Enabled = false;
            }
        }

        //public static bool PowerJudge(UserPower power)
        //{
        //    bool blRet = true;
        //    if (systemPower == UserPower.未注册)
        //    {
        //        blRet = false;
        //        if (power == UserPower.厂家)
        //        {
        //            MessageUtil.ShowTips("当前系统未注册，请以厂家权限用户注册");
        //        }
        //        else if (power == UserPower.系统用户)
        //        {
        //            MessageUtil.ShowTips("当前系统未注册，请以系统权限用户注册");
        //        }
        //        else if (power == UserPower.普通用户)
        //        {
        //            MessageUtil.ShowTips("当前系统未注册，请注册用户");
        //        }
        //    }
        //    else
        //    {
        //        if (power == UserPower.厂家)
        //        {
        //            if (systemPower != UserPower.厂家)
        //            {
        //                MessageUtil.ShowTips("当前用户权限不够，请以厂家权限用户注册");
        //                blRet = false;
        //            }
        //            else if (power == UserPower.系统用户)
        //            {
        //                if (systemPower == UserPower.普通用户)
        //                {
        //                    MessageUtil.ShowTips("当前用户权限不够，请以系统权限用户注册");
        //                    blRet = false;
        //                }
        //            }
        //        }
        //    }
        //    return blRet;
        //}

        /// <summary>
        /// 权限判断
        /// </summary>
        public static bool PowerJudge(UserPower power)
        {
            bool blRet = true;
            if (systemPower == UserPower.未注册)
            {
                blRet = false;
                if (power == UserPower.厂家)
                {
                    //MessageUtil.ShowTips("当前系统未注册，请以厂家权限用户注册");
                    if (MessageUtil.ShowYesNoAndTips("当前系统未注册，是否以厂家权限用户注册？") == DialogResult.Yes)
                    {
                        frmLogin frm = new frmLogin();
                        frm.ShowDialog();
                        frm.Dispose();
                        if (systemPower == UserPower.厂家)
                            blRet = true;
                    }
                }
                else if (power == UserPower.系统用户)
                {
                    //MessageUtil.ShowTips("当前系统未注册，请以系统权限用户注册");
                    if (MessageUtil.ShowYesNoAndTips("当前系统未注册，是否以系统权限用户注册？") == DialogResult.Yes)
                    {
                        frmLogin frm = new frmLogin();
                        frm.ShowDialog();
                        frm.Dispose();
                        if (systemPower == UserPower.厂家 || systemPower == UserPower.系统用户)
                            blRet = true;
                    }
                }
                else if (power == UserPower.普通用户)
                {
                    //MessageUtil.ShowTips("当前系统未注册，请注册用户");
                    if (MessageUtil.ShowYesNoAndTips("当前系统未注册，注册用户？") == DialogResult.Yes)
                    {
                        frmLogin frm = new frmLogin();
                        frm.ShowDialog();
                        frm.Dispose();
                        if (systemPower != UserPower.未注册)
                            blRet = true;
                    }
                }
            }
            else
            {
                if (power == UserPower.厂家)
                {
                    if (systemPower != UserPower.厂家)
                    {
                        //MessageUtil.ShowTips("当前用户权限不够，请以厂家权限用户注册");
                        //blRet = false;
                        if (MessageUtil.ShowYesNoAndTips("当前用户权限不够，是否以厂家权限用户注册？") == DialogResult.Yes)
                        {
                            frmLogin frm = new frmLogin();
                            frm.ShowDialog();
                            frm.Dispose();
                            if (systemPower == UserPower.厂家)
                                blRet = true;
                            else
                                blRet = false;
                        }
                        else
                        {
                            blRet = false;
                        }
                    }
                    else if (power == UserPower.系统用户)
                    {
                        if (systemPower == UserPower.普通用户)
                        {
                            //MessageUtil.ShowTips("当前用户权限不够，请以系统权限用户注册");
                            //blRet = false;
                            if (MessageUtil.ShowYesNoAndTips("当前用户权限不够，是否以系统权限用户注册？") == DialogResult.Yes)
                            {
                                frmLogin frm = new frmLogin();
                                frm.ShowDialog();
                                frm.Dispose();
                                if (systemPower == UserPower.系统用户 || systemPower == UserPower.厂家)
                                    blRet = true;
                                else
                                    blRet = false;
                            }
                            else
                            {
                                blRet = false;
                            }
                        }

                    }
                }
            }

            return blRet;
        }

        /// <summary>
        /// 事件信息表 初始化 
        /// </summary>
        private void EventTableInit()//  Type Point Content People Time No IsClick
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Type";
            column.Caption = "事件类型";
            dtEventInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Point";
            column.Caption = "事件点";
            dtEventInfo.Columns.Add(column);

            column = new DataColumn();// 事件类型 事件点 事件内容 人员 时间 序号 是否点击
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Content";
            column.Caption = "事件内容";
            dtEventInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "People";
            column.Caption = "相关人";
            dtEventInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Remark";
            column.Caption = "备注";
            dtEventInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Time";
            column.Caption = "时间";
            dtEventInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "No";
            column.Caption = "序号";
            dtEventInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsClick";
            column.Caption = "是否点击";
            dtEventInfo.Columns.Add(column);
        }

        private void EventTableShowInit()//  Type Point Content People Time No IsClick
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Type";
            Col1.Caption = "事件类型";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// 事件类型 事件点 事件内容 人员 时间 序号 是否点击
            Col1.FieldName = "Point";
            Col1.Caption = "事件点";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Content";
            Col1.Caption = "事件内容";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "People";
            Col1.Caption = "人员";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Remark";
            Col1.Caption = "备注";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Time";
            Col1.Caption = "时间";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "No";
            Col1.Caption = "序号";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsClick";
            Col1.Caption = "是否点击";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            this.gridView1.BestFitColumns();
        }

        /// <summary>
        /// 告警信息表 初始化 
        /// </summary>
        private void AlarmTableInit()//  Type Point Content People Time No IsClick
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Type";
            column.Caption = "告警类型";
            dtAlarmInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Point";
            column.Caption = "告警点";
            dtAlarmInfo.Columns.Add(column);

            column = new DataColumn();// 事件类型 事件点 事件内容 人员 时间 序号 是否点击
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Content";
            column.Caption = "告警内容";
            dtAlarmInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "People";
            column.Caption = "相关人";
            dtAlarmInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Remark";
            column.Caption = "备注";
            dtAlarmInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Time";
            column.Caption = "时间";
            dtAlarmInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "No";
            column.Caption = "序号";
            dtAlarmInfo.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsClick";
            column.Caption = "是否点击";
            dtAlarmInfo.Columns.Add(column);
        }

        private void AlarmTableShowInit()//  Type Point Content People Time No IsClick
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Type";
            Col1.Caption = "告警类型";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// 事件类型 事件点 事件内容 人员 时间 序号 是否点击
            Col1.FieldName = "Point";
            Col1.Caption = "告警点";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Content";
            Col1.Caption = "告警内容";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "People";
            Col1.Caption = "相关人";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Remark";
            Col1.Caption = "备注";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Time";
            Col1.Caption = "时间";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "No";
            Col1.Caption = "序号";
            Col1.Visible = false;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsClick";
            Col1.Caption = "是否点击";
            Col1.Visible = false;
            gridView3.Columns.Add(Col1);

            this.gridView3.BestFitColumns();
        }

        private void AddTreeViewToolsType(string strParent, TreeNode parentNode)
        {
            string strSql = "select tvParent,tvChildId,ToolName,ToolType from tb_TypeAndName where tvParent='" + strParent + "' ";
            DataTable dt = datalogic.GetDataTable(strSql);

            foreach (DataRow datarow in dt.Rows)
            {
                TreeNode node = new TreeNode();
                //处理根节点
                if (parentNode == null)
                {
                    node.Name = datarow["tvChildId"].ToString();// node.Name 为本节点的编号
                    node.Tag = strParent;                       // node.Tag 为本节点父节点的编号
                    node.Text = datarow["ToolType"].ToString();
                    node.ImageIndex = 4;
                    node.SelectedImageIndex = 4;
                    treeView1.Nodes.Add(node);
                    AddTreeViewToolsType(datarow["tvChildId"].ToString(), node);
                }
                //处理子节点
                else
                {
                    node.Name = datarow["tvChildId"].ToString();
                    node.Tag = strParent;
                    string str = datarow["ToolName"].ToString();
                    node.Text = str;
                    node.ImageIndex = 5;
                    node.SelectedImageIndex = 5;

                    strSql = "select tvParent,tvChildId,ToolID,ToolName,IsInStore from tb_Tools where ToolName='" + str + "'";
                    DataTable dtToos = datalogic.GetDataTable(strSql);
                    foreach (DataRow drTools in dtToos.Rows)
                    {
                        TreeNode tnToos = new TreeNode();
                        tnToos.Name = drTools["tvChildId"].ToString();
                        //tnToos.Tag = strParent;
                        tnToos.Text = drTools["ToolID"].ToString();
                        if (drTools["IsInStore"].ToString() == ToolsState.借出.ToString() || drTools["IsInStore"].ToString() == ToolsState.外借超时.ToString())
                        {
                            tnToos.ImageIndex = 6;
                            tnToos.SelectedImageIndex = 6;
                        }
                        else
                        {
                            tnToos.ImageIndex = 3;
                            tnToos.SelectedImageIndex = 3;
                        }

                        node.Nodes.Add(tnToos);
                    }

                    parentNode.Nodes.Add(node);
                    AddTreeViewToolsType(datarow["tvChildId"].ToString(), node);
                }
            }
        }


        #endregion

        private void bbtnCabinet_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmPlaceManage frm = new frmPlaceManage();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnInRoom_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.普通用户);
            if (!blJudge)
                return;

            if (infoOfSystem.usingRfid == DeviceUsing.启用)
            {
                frmInRoom frm = new frmInRoom();
                frm.Tag = TagType.添加.ToString();
                frm.ShowDialog(this);
                frm.Dispose();
            }
            else
            {
                frmInRoomPower frm = new frmInRoomPower();
                frm.Tag = TagType.添加.ToString();
                frm.ShowDialog(this);
                frm.Dispose();
            }

            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            mainControl.LoadAllTools();

            //if (rfidRead != null)
            //    rfidRead.timerStart();
        }

        private void bbtnToolsType_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmToolName frm = new frmToolName();
            frm.ShowDialog(this);
            frm.Dispose();
            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            mainControl.LoadAllTools();
        }

        private void bbtnTools_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmToolsManage frm = new frmToolsManage();
            frm.ShowDialog(this);
            frm.Dispose();
            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            mainControl.LoadAllTools();
        }

        private void bbtnBorrow_ItemClick(object sender, ItemClickEventArgs e)
        {
            //frmBorrow frm = new frmBorrow();
            //frm.ShowDialog(this);
            //frm.Dispose();
        }

        private void bbtnReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            //frmReturn frm = new frmReturn();
            //frm.ShowDialog(this);
            //frm.Dispose();
        }

        private void bbtnPlace_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;


            StopBoxWatch();

            frmPlaceManage frm = new frmPlaceManage();
            frm.ShowDialog(this);
            frm.Dispose();
            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            mainControl.LoadAllTools();

            LoadBox();

            StartBoxWatch();

            if (UsingBox == DeviceUsing.启用)
            {
                rGroupBoxManage.Visible = true;
            }
            else
            {
                rGroupBoxManage.Visible = false;
            }
        }

        private void bbtnDoorUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.普通用户);
            if (!blJudge)
                return;
            StopDoorWatch();
            frmDoorUser frm = new frmDoorUser(zkControl);
            frm.ShowDialog(this);
            frm.Dispose();
            StartDoorWatch();
        }

        private void bbtnDoorPass_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;
            StopDoorWatch();
            frmPowerManage frm = new frmPowerManage();
            frm.ShowDialog(this);
            frm.Dispose();
            StartDoorWatch();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
        }

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0) return;
            DataRow dr = this.gridView1.GetDataRow(hand);
            if (dr == null) return;
            if (dr["IsClick"].ToString() == "")
            {
                e.Appearance.ForeColor = Color.Blue;// 改变行字体颜色
                e.Appearance.BackColor = Color.Blue;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;// 改变行字体颜色
                e.Appearance.BackColor = Color.Black;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            }
        }


        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (dtEventInfo.Rows.Count > 0)
            {
                string str = gridView1.GetFocusedRowCellValue("No").ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    lock (dtEventInfo)
                    {
                        for (int i = 0; i < dtEventInfo.Rows.Count; i++)
                        {
                            if (dtEventInfo.Rows[i]["No"].ToString() == str)
                            {
                                dtEventInfo.Rows[i]["IsClick"] = "点击";
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            if (dtAlarmInfo.Rows.Count > 0)
            {
                string str = gridView3.GetFocusedRowCellValue("No").ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    lock (dtAlarmInfo)
                    {
                        for (int i = 0; i < dtAlarmInfo.Rows.Count; i++)
                        {
                            if (dtAlarmInfo.Rows[i]["No"].ToString() == str)
                            {
                                dtAlarmInfo.Rows[i]["IsClick"] = "点击";
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void gridView3_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0) return;
            DataRow dr = this.gridView3.GetDataRow(hand);
            if (dr == null) return;
            if (dr["IsClick"].ToString() == "")
            {
                e.Appearance.ForeColor = Color.Blue;// 改变行字体颜色
                e.Appearance.BackColor = Color.Blue;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;// 改变行字体颜色
                e.Appearance.BackColor = Color.Black;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            }
        }

        private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
        }


        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (dtEventInfo)
            {
                dtEventInfo.Rows.Clear();
            }
        }

        private void 清空ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lock (dtAlarmInfo)
            {
                dtAlarmInfo.Rows.Clear();
            }
        }


        //frmVideoTime frmVideo = new frmVideoTime();
        //private void bbtnTimeVideo_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    frmVideo.Show(this);
        //    //frm.Dispose();
        //}

        //private void bbtnVideoBack_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    //video.RnuStopVideo(false);
        //    frmBackVideo frm = new frmBackVideo();
        //    frm.ShowDialog(this);
        //    //frm.Dispose();
        //    //video.VideoInit();
        //    //video.RnuStopVideo(true);
        //}

        private void bbtnRecordBorrow_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordBr frm = new frmRecordBr();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnRecordInRoom_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordInRoom frm = new frmRecordInRoom();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnRecordScrap_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordScrap frm = new frmRecordScrap();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnRecordInOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordInOut frm = new frmRecordInOut();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnRecordPass_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordPower frm = new frmRecordPower();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnTestIn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.普通用户);
            if (!blJudge)
                return;

            frmTestIn frm = new frmTestIn();
            frm.ShowDialog(this);
            frm.Dispose();
            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            mainControl.LoadAllTools();
        }

        private void bbtnTestRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmTestRecord frm = new frmTestRecord();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnTestCount_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmTestCount frm = new frmTestCount();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmSystemUers frm = new frmSystemUers();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnDevice_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.厂家);
            if (!blJudge)
                return;

            if (MainControl.listMark.Count > 0)
                MainControl.listMark.Clear();

            StopDoorWatch();
            if (powReader != null)
                powReader.StopRece();


            frmDeviceManage frm = new frmDeviceManage();
            frm.ShowDialog(this);
            frm.Dispose();

            LoadSysSet();
            if (UsingEnvir == DeviceUsing.启用)
            {
                envirControl.Start();
            }
            FunctionModeSet();
        }

        private void bbtnDataBack_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmDataBack frm = new frmDataBack();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnDataReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmDataReturn frm = new frmDataReturn();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnDataClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmDataClear frm = new frmDataClear();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        //private void bbtnDebugInfo_ItemClick(object sender, ItemClickEventArgs e)
        //{

        //}

        //private void bbtnDebug_ItemClick(object sender, ItemClickEventArgs e)
        //{

        //}

        private void bbtnSoftInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            MessageUtil.ShowTips("详见软件说明书");
        }

        private void bbtnAboutKH_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmAboutKh frm = new frmAboutKh();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnAlterPsd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmAlterPsd frm = new frmAlterPsd();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            //frmExitUser frm = new frmExitUser();
            //frm.ShowDialog(this);
            //frm.Dispose();
            //if (frmExitUser.blOk)
            //{
            //    blClose = true;
            //    Application.Exit();
            //}
            //else
            //{
            //    blClose = false;
            //}

            //bool blJudge = PowerJudge(UserPower.系统用户);
            //if (!blJudge)
            //    return;

            frmExitUser.userPower = UserPower.系统用户;
            frmExitUser frm = new frmExitUser();
            frm.ShowDialog(this);
            frm.Dispose();
            if (frmExitUser.blOk)
            {
                blClose = true;
                //if (rfidRead != null)
                //{
                //    rfidRead.ClearAndDispose();
                //    rfidRead = null;
                //}
                if(rfidManage!=null)
                {
                    rfidManage.DisposeRfid(true);
                    rfidManage = null;
                }
                Application.ExitThread();
                Application.Exit();
                try
                {
                    Environment.Exit(0);
                }
                catch
                { }
            }
            else
            {
                blClose = false;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (blClose == false)
            {
                frmExitUser.userPower = UserPower.系统用户;
                frmExitUser frm = new frmExitUser();
                frm.ShowDialog(this);
                frm.Dispose();
                if (frmExitUser.blOk)
                {
                    e.Cancel = false;
                    if (rfidManage != null)
                    {
                        rfidManage.DisposeRfid(true);
                        rfidManage = null;
                    }
                    if (envirControl != null)
                        envirControl.Stop();
                    if (mainControl != null)
                    {
                        if(mainControl.timerMain!=null)
                            mainControl.timerMain.Stop();
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (blClose == false)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                ////if (rfidRead != null)
                ////{
                ////    rfidRead.ClearAndDispose();
                ////    rfidRead = null;
                ////}

                ////if (mainControl.rfidRead1 != null)
                ////{
                ////    mainControl.rfidRead1.timerRfid.Stop();
                ////    mainControl.rfidRead1.timerRfidFast.Stop();
                ////}
                ////if (mainControl.rfidRead2 != null)
                ////{
                ////    mainControl.rfidRead2.timerRfid.Stop();
                ////    mainControl.rfidRead2.timerRfidFast.Stop();
                ////}
                //System.Environment.Exit(0);

                ////Application.ExitThread();
                ////Application.Exit();
                ////try
                ////{
                ////    Environment.Exit(0);
                ////}
                ////catch
                ////{ }
            }
        }

        private bool blClose = false;
        private void sbtnSysExit_Click(object sender, EventArgs e)
        {
            frmExitUser.userPower = UserPower.系统用户;
            frmExitUser frm = new frmExitUser();
            frm.ShowDialog(this);
            frm.Dispose();
            if (frmExitUser.blOk)
            {
                blClose = true;
                //if (rfidRead != null)
                //{
                //    rfidRead.ClearAndDispose();
                //    rfidRead = null;
                //}
                if (rfidManage != null)
                {
                    rfidManage.DisposeRfid(true);
                    rfidManage = null;
                }
                //if (mainControl.rfidRead1 != null)
                //{
                //    mainControl.rfidRead1.timerRfid.Stop();
                //    mainControl.rfidRead1.timerRfidFast.Stop();
                //}
                //if (mainControl.rfidRead2 != null)
                //{
                //    mainControl.rfidRead2.timerRfid.Stop();
                //    mainControl.rfidRead2.timerRfidFast.Stop();
                //}
                Application.ExitThread();
                Application.Exit();
                try
                {
                    Environment.Exit(0);
                }
                catch
                { }
            }
            else
            {
                blClose = false;
            }
        }

        private void sbtnRecordBr_Click(object sender, EventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordBr frm = new frmRecordBr();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void sbtnRecordDoorInOut_Click(object sender, EventArgs e)
        {
            frmRecordInOut frm = new frmRecordInOut();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void sbtnTools_Click(object sender, EventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmToolsManage frm = new frmToolsManage();
            frm.ShowDialog(this);
            frm.Dispose();
            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            mainControl.LoadAllTools();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                #region  超高频RFID 通信异常处理

                if (RFIDReLoad == ErrorContent.RFID读写器异常系统重启)
                {
                    //RFIDReLoad = ErrorContent.初值;
                    //string strSql = "select ID,DoorUsing,DoorSN,DoorIP,DoorCount,RfidUsing1,RfidUsing2,RfidIp1,RfidIp2,RfidPort1,RfidPort2,OverDayBorr," +
                    //               "BorrRetSpan,ServerAddr,HumiRun,HumiStop,HandKeepTime,AirCoolRun,AirCoolStop,AirCoolTempSet,AirHotRun,AirHotStop," +
                    //               "AirHotTempSet,DoorName1,DoorName2,ServerUsing,EnvirUsing,DoorType,FingerDoorIp,RfidBoxTime,FingerPort,wgPort" +
                    //               ",WgDoorName3,WgDoorName4,UsingFinger,FingerDoorName,IcDoorOfRfid1,IcDoorOfRfid2,IcDoorOfRfid3,IcDoorOfRfid4" +
                    //               ",FingerDoorOfRfid,Rfid1No,Rfid2No,BorrOver,ErrInfo from tb_SysDevice ";
                    //DataTable dt = datalogic.GetDataTable(strSql);//wgPort,WgDoorName3,WgDoorName4,UsingFinger,FingerDoorName
                    //if (dt.Rows.Count > 0)
                    //{
                    //    #region RFID 大门


                    //    // RFID1
                    //    string rfidUsing1 = dt.Rows[0]["RfidUsing1"].ToString();
                    //    string strRfidIp1 = dt.Rows[0]["RfidIp1"].ToString();
                    //    string strRfidPort1 = dt.Rows[0]["RfidPort1"].ToString();
                    //    string strRfidNo1 = dt.Rows[0]["Rfid1No"].ToString();
                    //    //RFID2
                    //    string rfidUsing2 = dt.Rows[0]["RfidUsing2"].ToString();
                    //    string strRfidIp2 = dt.Rows[0]["RfidIp2"].ToString();
                    //    string strRfidPort2 = dt.Rows[0]["RfidPort2"].ToString();
                    //    string strRfidNo2 = dt.Rows[0]["Rfid2No"].ToString();

                    //    if (rfidUsing1 == DeviceUsing.启用.ToString() || rfidUsing2 == DeviceUsing.启用.ToString())
                    //    {
                    //        if (rfidRead == null)
                    //        {
                    //            rfidRead = new clsRfidRead();
                    //            rfidRead.NewEventShowEvent += new NewEventShowEventHandler(rfidRead_NewEventShowEvent);//rfidRead_NewEventShowEvent rfidRead_NewAlarmShowEvent
                    //            rfidRead.NewAlarmShowEvent += new NewEventShowEventHandler(rfidRead_NewAlarmShowEvent);
                    //        }
                    //        else
                    //        {
                    //            rfidRead.ClearListRfid();
                    //        }
                    //        #region 1#

                    //        if (rfidUsing1 == DeviceUsing.启用.ToString())
                    //        {
                    //            int iPort = 0;
                    //            string strName = "";
                    //            if (string.IsNullOrEmpty(strRfidIp1))
                    //            {
                    //                if (frmMain.blDebug)
                    //                    MessageUtil.ShowTips("1# RFID读写器 IP为空");
                    //            }
                    //            if (string.IsNullOrEmpty(strRfidPort1))
                    //            {
                    //                if (frmMain.blDebug)
                    //                    MessageUtil.ShowTips("1# RFID读写器 端口号为空");
                    //            }
                    //            else
                    //            {
                    //                iPort = Convert.ToInt32(strRfidPort1);
                    //            }
                    //            if (rfidUsing1 == DeviceUsing.启用.ToString() && rfidUsing2 == DeviceUsing.启用.ToString())
                    //            {
                    //                strName = "1# RFID读写器";
                    //            }
                    //            clsRfid rfid = new clsRfid(strRfidIp1, iPort, strName);
                    //            if (strRfidNo1 == DeviceUsing.启用.ToString())
                    //                rfid.UsingReadNo = DeviceUsing.启用;
                    //            rfidRead.listRfid.Add(rfid);
                    //        }

                    //        #endregion

                    //        #region 2#

                    //        if (rfidUsing2 == DeviceUsing.启用.ToString())
                    //        {
                    //            int iPort = 0;
                    //            string strName = "";
                    //            if (string.IsNullOrEmpty(strRfidIp2))
                    //            {
                    //                if (frmMain.blDebug)
                    //                    MessageUtil.ShowTips("2# RFID读写器 IP为空");
                    //            }
                    //            if (string.IsNullOrEmpty(strRfidPort2))
                    //            {
                    //                if (frmMain.blDebug)
                    //                    MessageUtil.ShowTips("2# RFID读写器 端口号为空");
                    //            }
                    //            else
                    //            {
                    //                iPort = Convert.ToInt32(strRfidPort2);
                    //            }
                    //            if (rfidUsing1 == DeviceUsing.启用.ToString() && rfidUsing2 == DeviceUsing.启用.ToString())
                    //            {
                    //                strName = "2# RFID读写器";
                    //            }
                    //            clsRfid rfid = new clsRfid(strRfidIp2, iPort, strName);
                    //            if (strRfidNo2 == DeviceUsing.启用.ToString())
                    //                rfid.UsingReadNo = DeviceUsing.启用;

                    //            rfidRead.listRfid.Add(rfid);
                    //        }

                    //        #endregion

                    //        rfidRead.timerStart();
                    //    }
                    //    else
                    //    {
                    //        if (rfidRead != null)
                    //        {
                    //            rfidRead.ClearAndDispose();
                    //            rfidRead.NewEventShowEvent -= new NewEventShowEventHandler(rfidRead_NewEventShowEvent);
                    //            rfidRead.NewAlarmShowEvent -= new NewEventShowEventHandler(rfidRead_NewAlarmShowEvent);
                    //            rfidRead = null;
                    //        }
                    //    }

                    //    #endregion
                    //}

                    //rfidManage.timerStart();
                }

                if (stateRstRfid == StateRstRfid.重启)
                {
                    if (envirControl != null)
                    {
                        clsEnvirControl.blAskIo = false;
                        Thread.Sleep(600);
                        bool blRet = envirControl.serialIo.OnOffDevice(1, OnOffRelay.开启, DeviceRelayNo.RFID, "", DeviceRunModel.自动, false, IsWait.OnlyWait);
                        if (blRet)
                        {
                            stateRstRfid = StateRstRfid.关闭;
                            timeOffRfid = DateTime.Now;
                        }
                        clsEnvirControl.blAskIo = true;
                    }
                }
                else if (stateRstRfid == StateRstRfid.关闭)
                {
                    TimeSpan ts = DateTime.Now - timeOffRfid;
                    if (ts.TotalSeconds > 20)
                    {
                        clsEnvirControl.blAskIo = false;
                        Thread.Sleep(600);
                        bool blRet = envirControl.serialIo.OnOffDevice(1, OnOffRelay.关闭, DeviceRelayNo.RFID, "", DeviceRunModel.自动, false, IsWait.OnlyWait);
                        if (blRet)
                        {
                            stateRstRfid = StateRstRfid.初值;
                        }
                        clsEnvirControl.blAskIo = true;
                    }
                }

                #endregion

                //更新树结构 显示
                for (int iIndex = 0; iIndex < listNewTree.Count; iIndex++)
                {
                    if (listNewTree[iIndex].BlShow == false)
                    {
                        string strUpPoint = listNewTree[iIndex].StrToolId;
                        string strUpContent = listNewTree[iIndex].StrContent;
                        NewTreeViewImage(this.treeView1.Nodes, strUpPoint, strUpContent);
                        lock (listNewTree)
                        {
                            listNewTree[iIndex].BlShow = true;
                        }
                    }
                }


                btxtTime.Caption = DateTime.Now.ToString();

                if (frmLogin.blLoginEvent)
                {
                    frmLogin.blLoginEvent = false;
                    ShowUser();
                }

                if (systemPower != UserPower.未注册)
                {
                    TimeSpan ts = DateTime.Now - tiemLogin;
                    if (ts.TotalHours >= 2)
                    {
                        systemPower = UserPower.未注册;
                        strUserName = "";
                        ShowUser();
                    }
                }
            }
            catch (Exception ex)
            {
                if (blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }


        private void NewTreeViewImage(TreeNodeCollection aNodes, string strToolId, string strBorrRet)
        {
            foreach (TreeNode iNode in aNodes)
            {
                if (iNode.ImageIndex == 6 || iNode.ImageIndex == 3)
                {
                    if (iNode.Text == strToolId)
                    {
                        if (strBorrRet == ToolsBorrRet.借出.ToString())
                        {
                            iNode.ImageIndex = 6;
                        }
                        else if (strBorrRet == ToolsBorrRet.归还.ToString())
                        {
                            iNode.ImageIndex = 3;
                        }
                    }
                }
                if (iNode.Nodes.Count > 0)
                {
                    NewTreeViewImage(iNode.Nodes, strToolId, strBorrRet);
                }
            }
        }


        private void bbtnAlarmRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordAlarm frm = new frmRecordAlarm();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnMark_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.普通用户);
            if (!blJudge)
                return;

            if (MainControl.listMark.Count > 0)
                MainControl.listMark.Clear();

            frmToolMark frm = new frmToolMark();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void pboxDebug_Click(object sender, EventArgs e)
        {
            if (blDebug)
            {
                blDebug = false;
                pboxDebug.BackgroundImage = null;
            }
            else
            {
                blDebug = true;
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
                this.pboxDebug.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pboxDebug.BackgroundImage")));
            }
        }

        private void bbtnBoxPower_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            StopBoxWatch();

            frmBoxPower frm = new frmBoxPower();
            frm.ShowDialog(this);
            frm.Dispose();

            StartBoxWatch();
        }

        private void bbtnBoxRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordBoxOnOff frm = new frmRecordBoxOnOff();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnBoxPowerRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordBoxPower frm = new frmRecordBoxPower();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void sbtnExpand_Click(object sender, EventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            if (treeView1.Nodes.Count > 0)
            {
                if (treeView1.Nodes[0].IsExpanded == true)
                    treeView1.CollapseAll();
                else
                    treeView1.ExpandAll();
            }
        }

        private void bbtnHandBorr_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmHandBorr frm = new frmHandBorr();
            frm.ShowDialog(this);
            frm.Dispose();

            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            mainControl.LoadAllTools();
        }

        private void bbtnHandRet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmHandRet frm = new frmHandRet();
            frm.ShowDialog(this);
            frm.Dispose();

            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes.Clear();
            AddTreeViewToolsType("0", (TreeNode)null);
            treeView1.ExpandAll();

            mainControl.LoadAllTools();
        }

        private void bbtnEnvirState_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.普通用户);
            if (!blJudge)
                return;

            if (envirControl != null)
            {
                envirControl.serialAir.NewEventShowEvent -= new NewEventShowEventHandler(serialAir_NewEventShowEvent);
                envirControl.serialIo.NewEventShowEvent -= new NewEventShowEventHandler(serialIo_NewEventShowEvent);
            }

            frmEnvirState frm = new frmEnvirState();
            frm.serialAir.NewEventShowEvent += new NewEventShowEventHandler(serialAir_NewEventShowEvent);
            frm.serialIo.NewEventShowEvent += new NewEventShowEventHandler(serialIo_NewEventShowEvent);
            frm.ShowDialog(this);
            frm.serialAir.NewEventShowEvent -= new NewEventShowEventHandler(serialAir_NewEventShowEvent);
            frm.serialIo.NewEventShowEvent -= new NewEventShowEventHandler(serialIo_NewEventShowEvent);
            frm.Dispose();

            if (envirControl != null)
            {
                envirControl.serialAir.NewEventShowEvent += new NewEventShowEventHandler(serialAir_NewEventShowEvent);
                envirControl.serialIo.NewEventShowEvent += new NewEventShowEventHandler(serialIo_NewEventShowEvent);
            }
        }

        private void bbtnEnvirSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmEnvirSet frm = new frmEnvirSet();
            frm.ShowDialog(this);
            frm.Dispose();


            LoadEnvirSet();
        }

        private void bbtnEnvirEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmEnvirEvent frm = new frmEnvirEvent();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnEnvirData_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmEnvirData frm = new frmEnvirData();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnRoom_ItemClick(object sender, ItemClickEventArgs e)
        {
            UCControlNew.UCTimeInterval a = new UCControlNew.UCTimeInterval();
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.厂家);
            if (!blJudge)
                return;

            envirControl.Stop();

            frmRoomManage frm = new frmRoomManage();
            frm.ShowDialog(this);
            frm.Dispose();
            if (clsEnvirControl.listRoom.Count > 0)
                clsEnvirControl.listRoom.Clear();
            mainControl.LoadRooms();

            envirControl.Start();
        }

        private void bbtnDevicePlan_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDevicePlan frm = new frmDevicePlan();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnSysSet_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;
            frmSystemSet frm = new frmSystemSet();
            frm.ShowDialog(this);
            frm.Dispose();

            SysParameterSet();
        }

        private void bbtnLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower == UserPower.未注册)
            {
                frmLogin frm = new frmLogin();
                frm.ShowDialog(this);
                frm.Dispose();
                if (systemPower != UserPower.未注册)
                {
                    bbtnLogin.Caption = LoginBtnShow.系统注销.ToString();
                }
            }
            else
            {
                if (MessageUtil.ShowYesNoAndTips("确认注销登录") == DialogResult.Yes)
                {
                    systemPower = UserPower.未注册;
                    strUserName = "";
                    bbtnLogin.Caption = LoginBtnShow.系统登录.ToString();
                }
            }
            ShowUser();
        }

        private void sbtnRecordAlarm_Click(object sender, EventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            frmRecordAlarm frm = new frmRecordAlarm();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        //private void sbtnSystemSet_Click(object sender, EventArgs e)
        //{
        //    bool blJudge = PowerJudge(UserPower.系统用户);
        //    if (!blJudge)
        //        return;

        //    frmSystemSet frm = new frmSystemSet();
        //    frm.ShowDialog(this);
        //    frm.Dispose();

        //    mainControl.LoadSysSet();
        //}

        private void bbtnBoxIcPower_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            StopBoxWatch();

            frmBoxIcPower frm = new frmBoxIcPower();
            frm.ShowDialog(this);
            frm.Dispose();

            StartBoxWatch();
        }

        private void sbtnSystemSet_Click(object sender, EventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            frmSystemSet frm = new frmSystemSet();
            frm.ShowDialog(this);
            frm.Dispose();

            SysParameterSet();
        }

        //private void ribbonControl_Click(object sender, EventArgs e)
        //{

        //}

        private void bbtnErrRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.普通用户);
            if (!blJudge)
                return;
            frmRecordError frm = new frmRecordError();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void bbtnViode_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (systemPower != UserPower.未注册)
            {
                tiemLogin = DateTime.Now;
            }
            bool blJudge = PowerJudge(UserPower.普通用户);
            if (!blJudge)
                return;
            frmVideo frm = new frmVideo(0);
            frm.ShowDialog();
            //frmWebVideo frm = new frmWebVideo();
            //frm.ShowDialog(this);
            //frm.Dispose();
        }

        private void GetSysDevice()
        {
            //IList<TbSysdevice> listevent = TbSysdevice.FindListBySql("Select * from tb_SysDevice order by id asc");
        }

        
    }
}
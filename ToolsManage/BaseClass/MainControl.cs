using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Data;
using System.Threading;

using ToolsManage.UDP;
using ToolsManage.ToolsManage;
using ToolsManage.BaseClass.EnvirClass;
using ToolsManage.BaseClass.SeralClass;

using WG3000_COMM.Core;
using ToolsManage.Domain;

namespace ToolsManage.BaseClass
{
    class MainControl
    {
        DataLogic datalogic = new DataLogic();
        public TimerHelper timerMain = new TimerHelper(1000, false);//主定时器
        clsSerialVoice serialVoice = new clsSerialVoice();

        public static DataTable dtNewAlarm = new DataTable();

        #region 静态变量

        /// <summary>
        /// 盘库列表
        /// </summary>
        public static List<string> listMark = new List<string>();
        public static List<ToolInfo> listTools = new List<ToolInfo>();
        public static List<string> listVoice = new List<string>();
        public static Dictionary<string, ToolInfo> dicTools = new Dictionary<string, ToolInfo>();
        //public static List<RfidReadBox> listBoxRfid = new List<RfidReadBox>();

        #endregion

        #region  变量

        /// <summary>
        /// 上次 外借超时 提示时间
        /// </summary>
        private DateTime timeBorrOver = DateTime.Now.AddDays(-2);

   

        #region 语音

        /// <summary>
        /// 上次报语音时间  开门时 也赋值
        /// </summary>
        public static DateTime timeLastVoice = DateTime.Now;
        /// <summary>
        /// 开门报语音
        /// </summary>
        public static bool blVoiceOpenDoor = false;
        /// <summary>
        /// 报欢迎后 报借出超时
        /// </summary>
        private bool blVoiceBorrOver = false;
        /// <summary>
        /// 语音 间隔时间 包含 开门
        /// </summary>
        private const int ciIntervalVoice = 3;
        
        #endregion



        #endregion

        public MainControl()
        {
            timerMain.Execute += new TimerHelper.TimerExecution(timerMain_Execute);
           
        }

        #region 子程序


        /// <summary>
        /// 加载房间设备 一个房间对应的温湿度、烟感、空调控制器地址等
        /// </summary>
        public void LoadRooms()
        {
            clsEnvirControl.listRoom.Clear();
            //string strSql = "select ID,RoomName,WsdAddr1,WsdAddr2,FireAddr1,FireAddr2,RelayAddr,AirAddr,AirFactory,AirAddr2,AirFactory2 from tb_RoomInfo ";
            //DataTable dtRoom = datalogic.GetDataTable(strSql);
            IList<TbRoominfo> list = TbRoominfo.FindAll("","ID ASC","",0,0);
            if (list != null && list.Count > 0)
            {
                foreach (TbRoominfo roominfo in list)
                {
                    try
                    {
                        int iWsd1 = 0;
                        int iWsd2 = 0;
                        int iAir1 = 0;
                        int iAir2 = 0;
                        int iRelay = 0;
                        RoomClass room = new RoomClass(roominfo.RoomName);
                        room.YGCount = roominfo.YGCount;
                        room.WSDCount = roominfo.WSDCount;
                        room.KTCount = roominfo.KTCount;
                        room.IsExistCSJ = roominfo.IsExistCsj == DeviceUsing.启用.ToString() ? true : false;
                        room.IsExistJDQ = roominfo.IsExistRealy == DeviceUsing.启用.ToString() ? true : false;
                        room.IsExistJRQ = roominfo.IsExistJrq == DeviceUsing.启用.ToString() ? true : false;
                        room.IsExistXF = roominfo.IsExistXf == DeviceUsing.启用.ToString() ? true : false;
                        //温湿度
                        if (!string.IsNullOrEmpty(roominfo.WsdAddr1))
                            iWsd1 = Convert.ToInt32(roominfo.WsdAddr1);
                        if (!string.IsNullOrEmpty(roominfo.WsdAddr2))
                            iWsd2 = Convert.ToInt32(roominfo.WsdAddr2);
                        if (roominfo.WSDCount>0)
                        {
                            if (iWsd1 != 0)
                            {
                                clsWsd wsd = new clsWsd(iWsd1);
                                room.listWsd.Add(wsd);
                            }
                            if (iWsd2 != 0)
                            {
                                clsWsd wsd = new clsWsd(iWsd2);
                                room.listWsd.Add(wsd);
                            }
                        }
                       
                        //烟感
                        if (!string.IsNullOrEmpty(roominfo.FireAddr1))
                        {
                            clsFire fire = new clsFire(roominfo.FireAddr1);
                            room.listFire.Add(fire);
                        }
                        if (!string.IsNullOrEmpty(roominfo.FireAddr2))
                        {
                            clsFire fire = new clsFire(roominfo.FireAddr2);
                            room.listFire.Add(fire);
                        }
                        //空调控制器
                        if (roominfo.KTCount >= 2)
                        {//2个空调
                            if (!string.IsNullOrEmpty(roominfo.AirAddr))
                                iAir1 = Convert.ToInt32(roominfo.AirAddr);
                            if (!string.IsNullOrEmpty(roominfo.AirAddr2))
                                iAir2 = Convert.ToInt32(roominfo.AirAddr2);
                            if (iAir1 != 0)
                            {
                                AirFactoryType airType = AirFactoryType.大金;
                                if (roominfo.AirFactory != "大金")
                                    airType = AirFactoryType.其他;
                                clsAir air = new clsAir(iAir1, airType);
                                room.listAir.Add(air);
                            }
                            if (iAir2 != 0)
                            {
                                AirFactoryType airType = AirFactoryType.大金;
                                if (roominfo.AirFactory2 != "大金")
                                    airType = AirFactoryType.其他;
                                clsAir air = new clsAir(iAir2, airType);
                                room.listAir.Add(air);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(roominfo.AirAddr))
                                iAir1 = Convert.ToInt32(roominfo.AirAddr);
                            if (iAir1 != 0)
                            {
                                AirFactoryType airType = AirFactoryType.大金;
                                if (roominfo.AirFactory != "大金")
                                    airType = AirFactoryType.其他;
                                clsAir air = new clsAir(iAir1, airType);
                                room.listAir.Add(air);
                            }
                        }
                        
                        //继电器板
                        if (!string.IsNullOrEmpty(roominfo.RelayAddr))
                            iRelay = Convert.ToInt32(roominfo.RelayAddr);
                        if (iRelay != 0)
                        {
                            room.roomRelay = new clsRelay(iRelay);
                        }
                        clsEnvirControl.listRoom.Add(room);
                    }
                    catch (Exception ex)
                    {
                        if (frmMain.blDebug)
                        {
                            MessageUtil.ShowTips(ex.Message);
                        }
                    }
                }
                RoomClass roomrelay = clsEnvirControl.listRoom.Find(p => p.roomRelay != null);
                foreach (RoomClass room in clsEnvirControl.listRoom)
                {
                    if (room.roomRelay == null)
                    {
                        room.roomRelay = roomrelay.roomRelay;
                    }
                }
            }           
        }

        /// <summary>
        /// 加载所有工具柜
        /// </summary>
        //public void LoadBox()
        //{
        //    try
        //    {
        //        #region  工具柜门禁

        //        if (boxDoorWatching != null)
        //        {
        //            boxDoorWatching.PauseWatch();
        //            if (clsWgWatchingBox.listWg.Count > 0)
        //            {
        //                lock (clsWgWatchingBox.listWg)
        //                {
        //                    clsWgWatchingBox.listWg.Clear();
        //                }
        //            }
        //        }

        //        string strSql = "select tvChildId,AreaName,PlaceName,DoorIp,DoorSn,BoxHasRfid,BoxRfidMain from tb_Tools where  " +
        //                        "IsArea='" + ToolAreaType.工具柜.ToString() + "' and HasDoor='" + DeviceUsing.启用.ToString() + "'  ";
        //        DataTable dt = datalogic.GetDataTable(strSql);
        //        foreach (DataRow datarow in dt.Rows)
        //        {
        //            string strChildId = datarow["tvChildId"].ToString();
        //            string strArea = datarow["AreaName"].ToString();
        //            string strName = datarow["PlaceName"].ToString();
        //            string strIp = datarow["DoorIp"].ToString();
        //            string strSn = datarow["DoorSn"].ToString();
        //            string strHasRfid = datarow["BoxHasRfid"].ToString();
        //            string strRfidMain = datarow["BoxRfidMain"].ToString();
        //            uint iSn = 0;
        //            if (!string.IsNullOrEmpty(strSn))
        //            {
        //                iSn = Convert.ToUInt32 (strSn);
        //            }
        //            else
        //            {
        //                MessageUtil.ShowTips(strName + " 工具柜门禁SN为空");
        //            }

        //            clsDoorBox boxDoor = new clsDoorBox(strIp, iSn, strName, strArea);
        //            boxDoor.StrChildId = strChildId;
        //            boxDoor.StrHasRfid = strHasRfid;
        //            boxDoor.StrRfidMain = strRfidMain;
        //            lock (clsWgWatchingBox.listWg)
        //            {
        //                clsWgWatchingBox.listWg.Add(boxDoor);
        //            }
        //        }

        //        if (clsWgWatchingBox.listWg.Count > 0)
        //        {
        //            if (boxDoorWatching == null)
        //            {
        //                boxDoorWatching = new clsWgWatchingBox();
        //            }
        //            boxDoorWatching.StartWatch();
        //            frmMain.UsingBox = DeviceUsing.启用;
        //        }
        //        else
        //        {
        //            frmMain.UsingBox = DeviceUsing.未启用;
        //            if (boxDoorWatching != null)
        //            {
        //                boxDoorWatching.StopWatch();
        //                boxDoorWatching = null;
        //            }
        //        }

        //        #endregion

        //        #region

        //        //#region 工具柜RFID

        //        //if (listBoxRfid.Count > 0)
        //        //{
        //        //    lock (listBoxRfid)
        //        //    {
        //        //        for (int iIndexRfid = 0; iIndexRfid < listBoxRfid.Count; iIndexRfid++)
        //        //        {
        //        //            if (listBoxRfid[iIndexRfid] != null)
        //        //            {
        //        //                if (listBoxRfid[iIndexRfid].StateMainRead != BoxRfidState.不读)
        //        //                    listBoxRfid[iIndexRfid].StateMainRead = BoxRfidState.不读;
        //        //                if (listBoxRfid[iIndexRfid].StateSlaveRead != BoxRfidState.不读)
        //        //                    listBoxRfid[iIndexRfid].StateSlaveRead = BoxRfidState.不读;
        //        //                if (listBoxRfid[iIndexRfid].timerRfidFast.State == TimerState.Running)
        //        //                    listBoxRfid[iIndexRfid].StopRead();
        //        //                if (listBoxRfid[iIndexRfid].BlConnent)
        //        //                {
        //        //                    listBoxRfid[iIndexRfid].CloseNetPort();
        //        //                }
        //        //                if (listBoxRfid[iIndexRfid].timerRfidFast != null)
        //        //                    listBoxRfid[iIndexRfid].timerRfidFast = null;
        //        //                listBoxRfid[iIndexRfid] = null;
        //        //            }
        //        //        }
        //        //        listBoxRfid.Clear();
        //        //    }
        //        //}

        //        //strSql = "select tvChildId,AreaName,PlaceName,BoxRfidIp,BoxRfidPort,BoxRfidMain,BoxRfidAnt1,BoxRfidAnt2,BoxRfidAnt3,BoxRfidAnt4 from tb_Tools where " +
        //        //         "IsArea='" + ToolAreaType.工具柜.ToString() + "' and BoxHasRfid='" + DeviceUsing.启用.ToString() + "'  ";
        //        //dt = datalogic.GetDataTable(strSql);
        //        //foreach (DataRow datarow in dt.Rows)
        //        //{
        //        //    string strId = datarow["tvChildId"].ToString();
        //        //    string strArea = datarow["AreaName"].ToString();
        //        //    string strName = datarow["PlaceName"].ToString();
        //        //    string strIp = datarow["BoxRfidIp"].ToString();
        //        //    string strPort = datarow["BoxRfidPort"].ToString();
        //        //    string strMain = datarow["BoxRfidMain"].ToString();

        //        //    #region 天线

        //        //    DeviceUsing ant1 = DeviceUsing.未启用;
        //        //    DeviceUsing ant2 = DeviceUsing.未启用;
        //        //    DeviceUsing ant3 = DeviceUsing.未启用;
        //        //    DeviceUsing ant4 = DeviceUsing.未启用;

        //        //    string strAnt = datarow["BoxRfidAnt1"].ToString();
        //        //    if (strAnt == DeviceUsing.启用.ToString())
        //        //        ant1 = DeviceUsing.启用;
        //        //    else
        //        //        ant1 = DeviceUsing.未启用;
        //        //    strAnt = datarow["BoxRfidAnt2"].ToString();
        //        //    if (strAnt == DeviceUsing.启用.ToString())
        //        //        ant2 = DeviceUsing.启用;
        //        //    else
        //        //        ant2 = DeviceUsing.未启用;
        //        //    strAnt = datarow["BoxRfidAnt3"].ToString();
        //        //    if (strAnt == DeviceUsing.启用.ToString())
        //        //        ant3 = DeviceUsing.启用;
        //        //    else
        //        //        ant3 = DeviceUsing.未启用;
        //        //    strAnt = datarow["BoxRfidAnt4"].ToString();
        //        //    if (strAnt == DeviceUsing.启用.ToString())
        //        //        ant4 = DeviceUsing.启用;
        //        //    else
        //        //        ant4 = DeviceUsing.未启用;

        //        //    #endregion

        //        //    bool blHas = false;

        //        //    int iPort = 0;
        //        //    if (!string.IsNullOrEmpty(strPort))
        //        //    {
        //        //        iPort = Convert.ToInt32(strPort);
        //        //    }
        //        //    else
        //        //    {
        //        //        MessageUtil.ShowTips(strName + " 工具柜RFID端口号为空");
        //        //    }

        //        //    #region  已有

        //        //    if (listBoxRfid.Count > 0)
        //        //    {
        //        //        int iCount = listBoxRfid.Count;
        //        //        for (int iIndex = 0; iIndex < iCount; iIndex++)
        //        //        {
        //        //            if (listBoxRfid[iIndex].StrIp == strIp && listBoxRfid[iIndex].IPort == iPort)
        //        //            {
        //        //                blHas = true;
        //        //                if (listBoxRfid[iIndex].StrChildIdMain == null)
        //        //                {
        //        //                    listBoxRfid[iIndex].StrChildIdMain = strId;
        //        //                    listBoxRfid[iIndex].StrNameMain = strName;
        //        //                    listBoxRfid[iIndex].AntMain1 = ant1;
        //        //                    listBoxRfid[iIndex].AntMain2 = ant2;
        //        //                }
        //        //                else if (listBoxRfid[iIndex].StrChildIdSlave == null)
        //        //                {
        //        //                    listBoxRfid[iIndex].StrChildIdSlave = strId;
        //        //                    listBoxRfid[iIndex].StrNameSlave = strName;
        //        //                    listBoxRfid[iIndex].AntSlave1 = ant3;
        //        //                    listBoxRfid[iIndex].AntSlave2 = ant4;
        //        //                }
        //        //            }
        //        //        }
        //        //    }

        //        //    #endregion

        //        //    #region  无，添加

        //        //    if (blHas == false)
        //        //    {
        //        //        RfidReadBox rfidRead = new RfidReadBox();
        //        //        rfidRead.StrIp = strIp;
        //        //        rfidRead.IPort = iPort;
        //        //        if (strMain == BoxRfidMain.主机.ToString())
        //        //        {
        //        //            rfidRead.StrChildIdMain = strId;
        //        //            rfidRead.StrNameMain = strName;
        //        //            rfidRead.AntMain1 = ant1;
        //        //            rfidRead.AntMain2 = ant2;
        //        //        }
        //        //        else if (strMain == BoxRfidMain.从机.ToString())
        //        //        {
        //        //            rfidRead.StrChildIdSlave = strId;
        //        //            rfidRead.StrNameSlave = strName;
        //        //            rfidRead.AntSlave1 = ant3;
        //        //            rfidRead.AntSlave2 = ant4;
        //        //        }
        //        //        lock (listBoxRfid)
        //        //        {
        //        //            listBoxRfid.Add(rfidRead);
        //        //        }
        //        //    }

        //        //    #endregion

        //        //}
        //        ////设置工具柜RFID天线



        //        ////for (int iIndex = 0; iIndex < listBoxRfid.Count; iIndex++)
        //        ////{
        //        ////    DeviceUsing ant1 = listBoxRfid[iIndex].AntMain1;
        //        ////    DeviceUsing ant2 = listBoxRfid[iIndex].AntMain2;
        //        ////    DeviceUsing ant3 = listBoxRfid[iIndex].AntSlave1;
        //        ////    DeviceUsing ant4 = listBoxRfid[iIndex].AntSlave2;
        //        ////}

        //        ////if (listBoxDoor.Count > 0)
        //        ////{
        //        ////    //timerBoxDoor.Start();
        //        ////}
        //        ////else
        //        ////{
        //        ////    //timerBoxDoor.Stop();
        //        ////}

        //        //#endregion

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        if (frmMain.blDebug)
        //            MessageUtil.ShowTips(ex.Message);
        //    } 
        //}

        /// <summary>
        /// 加载 所有工具
        /// </summary>
        public void LoadAllTools()
        {
            try
            {
                #region 加载所有工器具

                string strSql = "select tvParent,ToolID,RFIDCoding,BorrowReturnTime,IsInStore,ToolType,ToolName,BorrowPeople,StoragePlace" +
                                " from tb_Tools where IsArea='" + ToolAreaType.工具.ToString() + "' and RFIDCoding <> '' ";
                DataTable dt = datalogic.GetDataTable(strSql);
                listTools.Clear();
                if (dicTools != null && dicTools.Count > 0)
                    dicTools.Clear();
                foreach (DataRow dr in dt.Rows)//dtTools ToolID  RfidCoding ToolBR TimeBR ToolType ToolName
                {
                    string strType = dr["ToolType"].ToString();
                    string strName = dr["ToolName"].ToString();
                    string strId = dr["ToolID"].ToString();
                    string strRfid = dr["RFIDCoding"].ToString();
                    string strTime = dr["BorrowReturnTime"].ToString();
                    string strState = dr["IsInStore"].ToString();
                    string strPeo = dr["BorrowPeople"].ToString();
                    string strTimeBorr = dr["BorrowReturnTime"].ToString();
                    string strParent = dr["tvParent"].ToString();
                    string strStation = dr["StoragePlace"].ToString();
                    ToolsState state = (ToolsState)Enum.Parse(typeof(ToolsState), strState);
                    DateTime dtime = DateTime.Now;
                    if (!string.IsNullOrEmpty(strTime))
                    {
                        dtime = Convert.ToDateTime(strTime);
                    }
                    ToolInfo toolsInfo = new ToolInfo(strId, strType, strName, strRfid, state, dtime, strPeo, "",strParent,strStation);
                    listTools.Add(toolsInfo);
                    if(!dicTools.ContainsKey(strRfid))
                    {
                        dicTools.Add(strRfid, toolsInfo);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }
        /// <summary>
        /// 告警信息表 初始化 
        /// </summary>
        private void AlarmTableInit()
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Type";
            dtNewAlarm.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Point";
            dtNewAlarm.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Content";
            dtNewAlarm.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "People";
            dtNewAlarm.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Remark";
            //column.Caption = "备注";
            dtNewAlarm.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Time";
            dtNewAlarm.Columns.Add(column);
        }

        #endregion

        /// <summary>
        /// 播放声音时长 默认每个字0.4s 
        /// </summary>
        int iVoiceLen = 0;
        private void timerMain_Execute()
        {
            try
            {
                #region 报语音

                if (listVoice.Count > 0)
                {
                    TimeSpan span = DateTime.Now - timeLastVoice;
                    if (span.TotalSeconds > iVoiceLen)
                    {
                        string strVoice = listVoice[0];
                        timeLastVoice = DateTime.Now;
                        iVoiceLen = (int)(strVoice.Length * 0.42);
                        bool blRet = serialVoice.SendVoice(strVoice);
                        if (blRet)
                        {
                            lock (listVoice)
                            {
                                listVoice.RemoveAt(0);
                            }
                        }
                    }
                }

                if (blVoiceOpenDoor)
                {
                    TimeSpan ts = DateTime.Now - timeLastVoice;// TotalSeconds 
                    if (ts.TotalMinutes >= ciIntervalVoice)
                    {
                        string statu = SoundPlayerHelper.Status;
                        {
                            if (statu != VoiceState.播放.ToString())
                            {
                                string str = Application.StartupPath + "\\Voice\\欢迎.WAV";
                                SoundPlayerHelper.Play(str, false);
                                blVoiceOpenDoor = false;
                                timeLastVoice = DateTime.Now;
                                blVoiceBorrOver = true;
                            }
                        }
                    }
                }

                #endregion

                if (infoOfSystem.usingBorrOver == DeviceUsing.启用)
                {
                    #region  开门后 报外借超时

                    if (blVoiceBorrOver)
                    {
                        bool blHasOver = false;//是否有外借超时
                        for (int iToos = 0; iToos < listTools.Count; iToos++)
                        {
                            if (listTools[iToos].ToolState == ToolsState.外借超时)
                            {
                                blHasOver = true;
                                break;
                            }
                        }
                        if (blHasOver)
                        {
                            TimeSpan ts = DateTime.Now - timeLastVoice;// TotalSeconds 
                            if (ts.TotalMinutes >= ciIntervalVoice)
                            {
                                string statu = SoundPlayerHelper.Status;
                                {
                                    if (statu != VoiceState.播放.ToString())
                                    {
                                        string str = Application.StartupPath + "\\Voice\\工器具借出超时.WAV";
                                        SoundPlayerHelper.Play(str, false);
                                        timeLastVoice = DateTime.Now;
                                        blVoiceBorrOver = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            blVoiceBorrOver = false;
                        }
                    }

                    #endregion

                    #region  判读借出超时 一天判断一次

                    TimeSpan tsOver = DateTime.Now - timeBorrOver;// TotalSeconds 
                    if (tsOver.TotalDays >= 1)
                    {
                        timeBorrOver = DateTime.Now;

                        for (int iToos = 0; iToos < listTools.Count; iToos++)
                        {
                            if (listTools[iToos].ToolState == ToolsState.借出 || listTools[iToos].ToolState == ToolsState.外借超时)
                            {
                                DateTime timeBorr = listTools[iToos].TimeBorrRet;
                                TimeSpan ts = DateTime.Now - timeBorr;
                                if (ts.TotalDays >= infoOfSystem.iOverDayBorr)
                                {
                                    string strId = listTools[iToos].StrToolId;
                                    string strPeople = listTools[iToos].StrPeopel;
                                    string strTimeBorr = listTools[iToos].TimeBorrRet.ToString("yyyy-MM-dd");
                                    ts = DateTime.Now - listTools[iToos].TimeBorrRet;
                                    string strDuration = clsCommon.CalculateTime(ts);
                                    string strType = AlarmsType.外借超时.ToString();
                                    string strContent = strTimeBorr + "借出，借出时长：" + strDuration;
                                    string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    string strRfid = listTools[iToos].StrRfidCode;

                                    DataRow dr = dtNewAlarm.NewRow();
                                    dr["Type"] = strType;
                                    dr["Point"] = strId;
                                    dr["Content"] = strContent;
                                    dr["People"] = strPeople;
                                    dr["Time"] = strTime;
                                    dtNewAlarm.Rows.Add(dr);
                                    //tb_AlarmEvent Type Point EventContent People Time
                                    string strSql = "insert into tb_AlarmEvent (Type,Point,EventContent,People,Time)" +
                                     "values ('" + strType + "','" + strId + "','" + strContent + "','" + strPeople + "','" + strTime + "')";
                                    datalogic.SqlComNonQuery(strSql);

                                    strSql = "update tb_Tools set IsInStore='" + ToolsState.外借超时.ToString() + "' where RFIDCoding='" + strRfid + "' ";
                                    datalogic.SqlComNonQuery(strSql);

                                    lock (listTools)
                                    {
                                        listTools[iToos].ToolState = ToolsState.外借超时;
                                    }

                                }
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


    }
}

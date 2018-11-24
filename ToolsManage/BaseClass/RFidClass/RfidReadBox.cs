using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Threading;

using ReaderB;

namespace ToolsManage.BaseClass
{
    //class RfidReadBox
    //{
    //    DataLogic datalogic = new DataLogic();
    //    public TimerHelperForm timerRfidFast = new TimerHelperForm(100, false);

    //    private int iPort = 27011;
    //    private string strIp;
    //    private bool blConnent = false;
    //    RfidNetState rfidNetState = RfidNetState.初值;

    //    /// <summary>
    //    /// RFID读写器 查询标签是否成功
    //    /// </summary>
    //    private bool blRfidRead = false;

    //    private string strChildIdMain;//ChildId
    //    private string strNameMain;
    //    private DeviceUsing antMain1;
    //    private DeviceUsing antMain2;
    //    private DateTime timeMainStart;
    //    private byte btAntMain = 0;

    //    private string strChildIdSlave;
    //    private string strNameSlave;
    //    private DeviceUsing antSlave1;
    //    private DeviceUsing antSlave2;
    //    private DateTime timeSlaveStart;
    //    private byte btAntSlave = 0;

    //    private BoxRfidState stateMainRead = BoxRfidState.不读;
    //    private BoxRfidState stateSlaveRead = BoxRfidState.不读;



    //    #region  RFID 变量

    //    private byte fComAdr = 0xff;
    //    /// <summary>
    //    /// 输出变量，返回与读写器连接端口对应的句柄，应用程序通过该句柄可以操作连接在相应端口的读写器。如果打开不成功，返回的句柄值为-1.
    //    /// </summary>
    //    private int frmcomportindex;
    //    /// <summary>
    //    /// 打开的串口索引号
    //    /// </summary>
    //    private int fOpenComIndex;
    //    /// <summary>
    //    /// 所有执行指令的返回值
    //    /// </summary>
    //    private int fCmdRet = 30;

    //    /// <summary>
    //    /// 存贮询查列表（如果读取的数据没有变化，则不进行刷新）
    //    /// </summary>
    //    private string fInventory_EPC_List;

    //    private bool blRfidScan;

    //    private List<string> ListEpcMain = new List<string>();
    //    private List<string> ListEpcSlave = new List<string>();
    //    private List <ToolInfo> listBoxMain = new List<ToolInfo>();
    //    private List<ToolInfo> listBoxSlave = new List<ToolInfo>();

    //    /// <summary>
    //    /// 高频率读到的标签数据
    //    /// </summary>
    //    //private string strEpcAllFast = "";
    //    /// <summary>
    //    /// 高频率读到的标签 数量
    //    /// </summary>
    //    //private int iCardCountFast = 0;

    //    private byte MaskMem = 0;
    //    private byte[] MaskAdr = new byte[2];
    //    private byte MaskLen = 0;
    //    private byte[] MaskData = new byte[100];
    //    private byte MaskFlag = 0;
    //    private byte AdrTID = 0;
    //    private byte LenTID = 0;
    //    private byte TIDFlag = 0;
    //    private byte Ant = 0;

    //    #endregion

    //    #region 属性

    //    public BoxRfidState StateSlaveRead
    //    {
    //        get { return stateSlaveRead; }
    //        set { stateSlaveRead = value; }
    //    }

    //    public BoxRfidState StateMainRead
    //    {
    //        get { return stateMainRead; }
    //        set { stateMainRead = value; }
    //    }

    //    public DateTime TimeSlaveStart
    //    {
    //        get { return timeSlaveStart; }
    //        set { timeSlaveStart = value; }
    //    }

    //    public DateTime TimeMainStart
    //    {
    //        get { return timeMainStart; }
    //        set { timeMainStart = value; }
    //    }

    //    public string StrChildIdMain
    //    {
    //        get { return strChildIdMain; }
    //        set { strChildIdMain = value; }
    //    }

    //    public string StrChildIdSlave
    //    {
    //        get { return strChildIdSlave; }
    //        set { strChildIdSlave = value; }
    //    }

    //    public string StrNameMain
    //    {
    //        get { return strNameMain; }
    //        set { strNameMain = value; }
    //    }

    //    public string StrNameSlave
    //    {
    //        get { return strNameSlave; }
    //        set { strNameSlave = value; }
    //    }

    //    public DeviceUsing AntMain1
    //    {
    //        get { return antMain1; }
    //        set { antMain1 = value; }
    //    }

    //    public DeviceUsing AntMain2
    //    {
    //        get { return antMain2; }
    //        set { antMain2 = value; }
    //    }

    //    public DeviceUsing AntSlave1
    //    {
    //        get { return antSlave1; }
    //        set { antSlave1 = value; }
    //    }

    //    public DeviceUsing AntSlave2
    //    {
    //        get { return antSlave2; }
    //        set { antSlave2 = value; }
    //    }

    //    public int IPort
    //    {
    //        get { return iPort; }
    //        set { iPort = value; }
    //    }

    //    public bool BlConnent
    //    {
    //        get { return blConnent; }
    //        set { blConnent = value; }
    //    }

    //    public string StrIp
    //    {
    //        get { return strIp; }
    //        set { strIp = value; }
    //    }

    //    #endregion

    //    public RfidReadBox()
    //    {
    //        timerRfidFast.Execute += new TimerHelperForm.TimerExecution(RfidFast_Execute);
    //        //timerRfidFast.Start();
    //    }

    //    #region 方法

    //    DeviceUsing antState1;
    //    DeviceUsing antState2;
    //    DeviceUsing antState3;
    //    DeviceUsing antState4;
    //    public bool SetAnt()
    //    {
    //        bool blRet = false;
    //        antState1 = antMain1;
    //        antState2 = antMain2;
    //        antState3 = antSlave1;
    //        antState4 = antSlave2;
    //        if (stateMainRead!= BoxRfidState.读标签) 
    //        {
    //            antState1 = DeviceUsing.未启用;
    //            antState2 = DeviceUsing.未启用;
    //        }
    //        if (stateSlaveRead  != BoxRfidState.读标签)
    //        {
    //            antState3 = DeviceUsing.未启用;
    //            antState4 = DeviceUsing.未启用;
    //        }
    //        byte ANT = 0;
    //        if (antState1 == DeviceUsing.启用)
    //            ANT = Convert.ToByte(ANT | 1);
    //        if (antState2 == DeviceUsing.启用)
    //            ANT = Convert.ToByte(ANT | 2);
    //        if (antState3 == DeviceUsing.启用)
    //            ANT = Convert.ToByte(ANT | 4);
    //        if (antState4 == DeviceUsing.启用)
    //            ANT = Convert.ToByte(ANT | 8);

    //        btAntMain = (byte)(0x03 & ANT);
    //        btAntSlave = (byte)(0x0c & ANT);

    //        int iRet = StaticClassReaderB.SetAntennaMultiplexing(ref fComAdr, ANT, frmcomportindex);
    //        if (iRet == 0)
    //            blRet = true;
    //        else
    //            blRet = false;
    //        return blRet;
    //    }

    //    public bool OpenNetPort()
    //    {
    //        bool blRet = false;
    //        try
    //        {
    //            frmcomportindex = 0;
    //            int iRet = StaticClassReaderB.OpenNetPort(iPort, strIp, ref fComAdr, ref frmcomportindex);
    //            fOpenComIndex = frmcomportindex;
    //            if ((iRet == 0x35) || (iRet == 0x30))
    //            {
    //                CloseNetPort();
    //                blRet = false;
    //            }
    //            if ((fOpenComIndex == -1) && (iRet == 0x30))
    //            {
    //                blRet = false;
    //            }
    //            if ((fOpenComIndex != -1) && (iRet != 0x35) && (iRet != 0x30))
    //            {
    //                blRet = true; ;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            if (frmMain.blDebug)
    //                MessageUtil.ShowTips(ex.Message);
    //        }
    //        return blRet;
    //    }

    //    public void CloseNetPort()
    //    {
    //        try
    //        {
    //            int  iport = StaticClassReaderB.CloseNetPort(iPort);
    //            int iIndex = StaticClassReaderB.CloseNetPort(frmcomportindex);
    //            if (iport == 0 && iIndex == 0)
    //            { 
                
    //            }
    //            blConnent = false;
    //        }
    //        catch (Exception ex)
    //        {
    //            if (frmMain.blDebug)
    //                MessageUtil.ShowTips(ex.Message);
    //        }
    //    }

    //    public void StatrRead()
    //    {
    //        timerRfidFast.Start();
    //    }

    //    public void StopRead()
    //    {
    //        timerRfidFast.Stop();
    //    }

    //    /// <summary>
    //    /// 判断RFID状态并记录
    //    /// </summary>
    //    /// <param name="bl"></param>
    //    private void JudgeRfidState(bool bl)
    //    {
    //        if (bl)
    //        {
    //            blConnent = true;
    //            if (rfidNetState == RfidNetState.已断开)
    //                NewAlarmEvent(AlarmsContent.通信恢复正常);
    //            rfidNetState = RfidNetState.已连接;
    //        }
    //        else
    //        {
    //            blConnent = false;
    //            if (rfidNetState != RfidNetState.已断开)
    //                NewAlarmEvent(AlarmsContent.通信异常);
    //            stateMainRead = BoxRfidState.不读;
    //            stateSlaveRead = BoxRfidState.不读;
    //            rfidNetState = RfidNetState.已断开;
    //        }
    //    }

    //    private void NewAlarmEvent(AlarmsContent content)
    //    {
    //        string strType = AlarmsType.工具柜RFID读写器通信错误.ToString();
    //        string strPoint = strNameMain +"  "+ strNameSlave;
    //        string strContent = content.ToString();
    //        string strPeople = "";
    //        string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //        if (frmMain.blDebug)
    //        {
    //            DataRow dr = MainControl.dtNewAlarm.NewRow();
    //            dr["Type"] = strType;
    //            dr["Point"] = strPoint;
    //            dr["Content"] = content.ToString();
    //            dr["People"] = strPeople;
    //            dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //            MainControl.dtNewAlarm.Rows.Add(dr);
    //        }
    //        string strSql = "insert into tb_AlarmEvent (Type,Point,EventContent,People,Time)" +
    //                                                   "values ('" + strType + "','" + strPoint + "','" + strContent + "','" + strPeople + "','" + strTime + "')";
    //        datalogic.SqlComNonQuery(strSql);
    //    }


    //    #endregion

    //    private void  Inventory()
    //    {
    //        blRfidRead = false;
    //        blRfidScan = true;

    //        int CardCount = 0;//输出变量，电子标签的张数
    //        int EpcLen = 0;//EPC 的字节数
    //        byte[] bEpcArray = new byte[5000];//指向输出数组变量 是读到的电子标签的EPC数据，是一张标签的EPC长度+一张标签的EPC号，依此累加
    //        fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, bEpcArray, ref Ant, ref EpcLen, ref CardCount, frmcomportindex);// 8616
    //        if ((fCmdRet == 1) || (fCmdRet == 2) || (fCmdRet == 3) || (fCmdRet == 4) || (fCmdRet == 0xFB))//代表已查找结束，
    //        {
    //            blRfidRead = true;
    //            //天线Id 和实际配置天线ID是否一致
    //            bool blAntMain = false;
    //            if (stateMainRead ==BoxRfidState .读标签 )
    //            {
    //                byte bt = (byte)(Ant & btAntMain);
    //                if (bt > 0)
    //                    blAntMain = true;
    //                else
    //                    blAntMain = false;
    //            }
    //            bool blAntSlave = false;
    //            if (stateSlaveRead  == BoxRfidState.读标签)
    //            {
    //                byte bt = (byte)(Ant & btAntSlave);
    //                if (bt > 0)
    //                    blAntSlave = true;
    //                else
    //                    blAntSlave = false;
    //            }

    //            if ((stateMainRead == BoxRfidState.读标签 && blAntMain) || (stateSlaveRead == BoxRfidState.读标签 && blAntSlave))
    //            {
    //                byte[] bEpcCopy = new byte[EpcLen];
    //                Array.Copy(bEpcArray, bEpcCopy, EpcLen);
    //                string strEpcAll = SerialPortUtil.ByteToStr(bEpcCopy);
    //                //和上次扫描结果一样
    //                if (fInventory_EPC_List == strEpcAll)
    //                {
    //                    blRfidScan = false;
    //                    return;
    //                }

    //                fInventory_EPC_List = strEpcAll;            //存贮记录
    //                int iEpcLenIndex = 0;//单个EPC标签数据长度的 索引
    //                if (CardCount == 0)
    //                {
    //                    blRfidScan = false;
    //                    return;
    //                }

    //                for (int CardIndex = 0; CardIndex < CardCount; CardIndex++)
    //                {
    //                    int iEpcLen = bEpcCopy[iEpcLenIndex];
    //                    string strEpc = strEpcAll.Substring(iEpcLenIndex * 2 + 2, iEpcLen * 2);
    //                    iEpcLenIndex = iEpcLenIndex + iEpcLen + 1;

    //                    if (strEpc.Length != iEpcLen * 2)
    //                    {
    //                        blRfidScan = false;
    //                        return;
    //                    }
    //                    if (strEpc.Length != 12)
    //                    {
    //                        blRfidScan = false;
    //                        return;
    //                    }

    //                    if (stateMainRead ==BoxRfidState .读标签 )
    //                    {
    //                        bool blInList = false;
    //                        for (int i = 0; i < ListEpcMain.Count; i++)     //判断是否在Listview列表内 
    //                        {
    //                            if (strEpc == ListEpcMain[i])
    //                                blInList = true;
    //                        }
    //                        if (!blInList)
    //                        {
    //                            ListEpcMain.Add(strEpc);
    //                        }
    //                    }
    //                    if (stateSlaveRead  == BoxRfidState.读标签)
    //                    {
    //                        bool blInList = false;
    //                        for (int i = 0; i < ListEpcSlave.Count; i++)
    //                        {
    //                            if (strEpc == ListEpcSlave[i])
    //                                blInList = true;
    //                        }
    //                        if (!blInList)
    //                        {
    //                            ListEpcSlave.Add(strEpc);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            blRfidRead = false;
    //        }

    //        blRfidScan = false;
    //    }

    //    private bool JudgeAntSet()
    //    {
    //        bool blRet = true;
    //        bool blAnt1 = true;
    //        bool blAnt2 = true;
    //        bool blAnt3 = true;
    //        bool blAnt4 = true;
    //        if (stateMainRead ==BoxRfidState .读标签 )
    //        {
    //            if (antState1 != antMain1)
    //                blAnt1 = false;
    //            if (antState2 != antMain2)
    //                blAnt2 = false;
    //        }
    //        else
    //        {
    //            if (antState1 != DeviceUsing.未启用)
    //                blAnt1 = false;
    //            if (antState2 != DeviceUsing.未启用)
    //                blAnt2 = false;
    //        }
    //        if (stateSlaveRead  == BoxRfidState.读标签)
    //        {
    //            if (antState3 != antSlave1)
    //                blAnt3 = false;
    //            if (antState4 != antSlave2)
    //                blAnt4 = false;
    //        }
    //        else
    //        {
    //            if (antState3 != DeviceUsing.未启用)
    //                blAnt3 = false;
    //            if (antState4 != DeviceUsing.未启用)
    //                blAnt4 = false;
    //        }
    //        if (blAnt1 == false || blAnt2 == false || blAnt3 == false || blAnt4 == false)
    //            blRet = false;
    //        return blRet;
    //    }

    //    private void RfidFast_Execute()
    //    {
    //        try
    //        {
    //            #region  连接或设置天线

    //            //主机等待读 从机完成后 主机读
    //            if (stateMainRead == BoxRfidState.等待读)
    //            {
    //                if (stateSlaveRead == BoxRfidState.不读)
    //                {
    //                    stateMainRead = BoxRfidState.读标签;
    //                    timeMainStart = DateTime.Now;
    //                }
    //            }
    //            //从机等待读 主机完成后 从机读
    //            if (stateSlaveRead == BoxRfidState.等待读)
    //            {
    //                if (stateMainRead == BoxRfidState.不读)
    //                {
    //                    stateSlaveRead = BoxRfidState.读标签;
    //                    timeSlaveStart = DateTime.Now;
    //                }
    //            }

    //            if (stateMainRead ==BoxRfidState .读标签  || stateSlaveRead ==BoxRfidState .读标签 )
    //            {
    //                if (!blConnent)
    //                {
    //                    bool blRet = OpenNetPort();
    //                    JudgeRfidState(blRet);
    //                }

    //                if (JudgeAntSet() == false)
    //                {
    //                    bool blRet = SetAnt();
    //                    JudgeRfidState(blRet);
    //                }
    //            }
    //            else
    //            {
    //                if (stateMainRead == BoxRfidState.不读 || stateSlaveRead == BoxRfidState.不读)
    //                {
    //                    if (timerRfidFast.State == TimerState.Running)
    //                        StopRead();
    //                    if (blConnent)
    //                        CloseNetPort();
    //                }
    //            }

    //            #endregion

    //            if (blConnent ==false)
    //                return;

    //            if (blRfidScan)
    //                return;

    //            Inventory();
    //            JudgeRfidState(blRfidRead);

    //            #region  RFID 主机

    //            if (stateMainRead  == BoxRfidState.读标签)
    //            {
    //                TimeSpan ts = DateTime.Now - timeMainStart;
    //                if (ts.TotalSeconds > MainControl.iBoxScanTime)
    //                {
    //                    stateMainRead = BoxRfidState.不读;
    //                    // 该工具柜内的所有工具
    //                    if (listBoxMain.Count > 0)
    //                        listBoxMain.Clear();
    //                    lock (MainControl.listTools)
    //                    {
    //                        int iCount = MainControl.listTools.Count;
    //                        for (int iIndex = 0; iIndex < iCount; iIndex++)
    //                        {
    //                            if (MainControl.listTools[iIndex].StrParent == strChildIdMain)
    //                            {
    //                                listBoxMain.Add(MainControl.listTools[iIndex]);
    //                            }
    //                        }
    //                    }

    //                    #region  扫描列表中有 工具柜中没有 归还或放错位置

    //                    for (int i = 0; i < ListEpcMain.Count; i++)
    //                    {
    //                        bool blHas = false;
    //                        string strEpc = ListEpcMain[i];

    //                        for (int iList = 0; iList < listBoxMain.Count; iList++)
    //                        {
    //                            if (strEpc == listBoxMain[iList].StrRfidCode)
    //                            {
    //                                blHas = true;

    //                                int iCount = MainControl.listTools.Count;
    //                                for (int iIndex = 0; iIndex < iCount; iIndex++)
    //                                {
    //                                    if (MainControl.listTools[iIndex].StrRfidCode == strEpc)
    //                                    {
    //                                        string strParent = MainControl.listTools[iIndex].StrParent;
    //                                        if (strParent == strChildIdMain)//工具属于 该工具柜
    //                                        {
    //                                            if (listBoxMain[iList].ToolState != ToolsState.在库)
    //                                            {
    //                                                lock (listBoxMain)
    //                                                {
    //                                                    listBoxMain[iList].ToolState = ToolsState.在库;
    //                                                }
    //                                                string strToolType = listBoxMain[iList].StrToolType;
    //                                                string strToolName = listBoxMain[iList].StrToolName;
    //                                                string strToolID = listBoxMain[iList].StrToolId;
    //                                                string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //                                                string strSql = strSql = "update tb_Tools set IsInStore='" + ToolsState.在库.ToString() + "',BorrowReturnTime='" + strTime + "' where RFIDCoding='" + strEpc + "' ";
    //                                                datalogic.SqlComNonQuery(strSql);
    //                                                strSql = strSql = "update tb_RecordBorrow set PeopleReturn='',ReturnTime='" + strTime + "'," +
    //                                                 "BorrowDuration='' where RFIDCoding='" + strEpc + "' and ReturnTime='' ";
    //                                                datalogic.SqlComNonQuery(strSql);

    //                                                //更新 listTools
    //                                                lock (MainControl.listTools)
    //                                                {
    //                                                    MainControl.listTools[iIndex].ToolState = ToolsState.在库;
    //                                                    MainControl.listTools[iIndex].TimeBorrRet = DateTime.Now;
    //                                                }

    //                                                DataRow dr = MainControl.dtNewEvent.NewRow();
    //                                                dr["Type"] = EventsType.工具借还.ToString();
    //                                                dr["Point"] = strToolID;
    //                                                dr["Content"] = ToolsBorrRet.归还.ToString();
    //                                                dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                                                MainControl.dtNewEvent.Rows.Add(dr);
    //                                            }
    //                                        }
    //                                        break;
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        }

    //                        if (blHas == false && frmMain.blDebug)
    //                        {
    //                            int iCount = MainControl.listTools.Count;
    //                            for (int iIndex = 0; iIndex < iCount; iIndex++)
    //                            {
    //                                if (MainControl.listTools[iIndex].StrRfidCode == strEpc)
    //                                {
    //                                    string strParent = MainControl.listTools[iIndex].StrParent;
    //                                    if (strParent != strChildIdMain)//工具属于 该工具柜
    //                                    {
    //                                        string strStation = MainControl.listTools[iIndex].StrStation;

    //                                        string strType = AlarmsType.工具放置位置错误.ToString();
    //                                        string strId = MainControl.listTools[iIndex].StrToolId;
    //                                        string strContent = "应放置 " + strStation + " 工具柜";
    //                                        string strPeople = "";
    //                                        string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //                                        DataRow dr = MainControl.dtNewAlarm.NewRow();
    //                                        dr["Type"] = strType;
    //                                        dr["Point"] = strId;
    //                                        dr["Content"] = strContent;
    //                                        dr["People"] = "";
    //                                        dr["Time"] = strTime;
    //                                        MainControl.dtNewAlarm.Rows.Add(dr);

    //                                        string strSql = "insert into tb_AlarmEvent (Type,Point,EventContent,People,Time)" +
    //                                                        "values ('" + strType + "','" + strId + "','" + strContent + "','" + strPeople + "','" + strTime + "')";
    //                                        datalogic.SqlComNonQuery(strSql);

    //                                    }
    //                                    break;
    //                                }
    //                            }
    //                        }

    //                    }

    //                    #endregion

    //                    #region  工具柜中有 扫描列表中没有 借出

    //                    for (int iIndex = 0; iIndex < listBoxMain.Count; iIndex++)
    //                    {
    //                        string strRfid = listBoxMain[iIndex].StrRfidCode;
    //                        bool blHas = false;
    //                        for (int i = 0; i < ListEpcMain.Count; i++)
    //                        {
    //                            if (strRfid == ListEpcMain[i])
    //                            {
    //                                blHas = true;
    //                                break;
    //                            }
    //                        }

    //                        if (blHas == false)
    //                        {
    //                            if (listBoxMain[iIndex].ToolState == ToolsState.在库)//原先在库 即借出
    //                            {
    //                                lock (listBoxMain)
    //                                {
    //                                    listBoxMain[iIndex].ToolState = ToolsState.借出;
    //                                }
    //                                string strToolType = listBoxMain[iIndex].StrToolType;
    //                                string strToolName = listBoxMain[iIndex].StrToolName;
    //                                string strToolID = listBoxMain[iIndex].StrToolId;
    //                                string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //                                string strSql = strSql = "update tb_Tools set IsInStore='" + ToolsState.借出.ToString() + "',BorrowReturnTime='" + strTime + "' where RFIDCoding='" + strRfid + "' ";
    //                                datalogic.SqlComNonQuery(strSql);

    //                                //tb_RecordBorrow  ID ToolType ToolName ToolID RFIDCoding PeopleBorrow BorrowTime PeopleReturn ReturnTime BorrowDuration     
    //                                strSql = "insert into tb_RecordBorrow (ToolType,ToolName,ToolID,RFIDCoding,PeopleBorrow,BorrowTime,PeopleReturn,ReturnTime,BorrowDuration)" +
    //                                "values ('" + strToolType + "','" + strToolName + "','" + strToolID + "','" + strRfid + "','','" + strTime + "'," +
    //                                 "'','','')";
    //                                datalogic.SqlComNonQuery(strSql);

    //                                //更新 listTools
    //                                int iCount = MainControl.listTools.Count;
    //                                for (int iTools = 0; iTools < iCount; iTools++)
    //                                {
    //                                    if (MainControl.listTools[iTools].StrRfidCode == strRfid)
    //                                    {
    //                                        lock (MainControl.listTools)
    //                                        {
    //                                            MainControl.listTools[iTools].ToolState = ToolsState.借出;
    //                                            MainControl.listTools[iTools].TimeBorrRet = DateTime.Now;
    //                                        }
    //                                        break;
    //                                    }
    //                                }

    //                                DataRow dr = MainControl.dtNewEvent.NewRow();
    //                                dr["Type"] = EventsType.工具借还.ToString();
    //                                dr["Point"] = strToolID;
    //                                dr["Content"] = ToolsBorrRet.借出.ToString();
    //                                dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                                MainControl.dtNewEvent.Rows.Add(dr);
    //                            }
    //                        }

    //                    }

    //                    #endregion

    //                    if (ListEpcMain.Count > 0)
    //                        ListEpcMain.Clear();
    //                }
    //            }


    //            #endregion

    //            #region  RFID 从机

    //            if (stateSlaveRead ==BoxRfidState .读标签 )
    //            {
    //                TimeSpan ts = DateTime.Now - timeSlaveStart;
    //                if (ts.TotalSeconds > MainControl.iBoxScanTime)
    //                {
    //                    stateSlaveRead = BoxRfidState.不读;
    //                    // 该工具柜内的所有工具
    //                    if (listBoxSlave.Count > 0)
    //                        listBoxSlave.Clear();
    //                    lock (MainControl.listTools)
    //                    {
    //                        int iCount = MainControl.listTools.Count;
    //                        for (int iIndex = 0; iIndex < iCount; iIndex++)
    //                        {
    //                            if (MainControl.listTools[iIndex].StrParent == strChildIdSlave)
    //                            {
    //                                listBoxSlave.Add(MainControl.listTools[iIndex]);
    //                            }
    //                        }
    //                    }

    //                    #region  扫描列表中有 工具柜中没有 归还或放错位置

    //                    for (int i = 0; i < ListEpcSlave.Count; i++)
    //                    {
    //                        bool blHas = false;
    //                        string strEpc = ListEpcSlave[i];

    //                        for (int iList = 0; iList < listBoxSlave.Count; iList++)
    //                        {
    //                            if (strEpc == listBoxSlave[iList].StrRfidCode)
    //                            {
    //                                blHas = true;

    //                                int iCount = MainControl.listTools.Count;
    //                                for (int iIndex = 0; iIndex < iCount; iIndex++)
    //                                {
    //                                    if (MainControl.listTools[iIndex].StrRfidCode == strEpc)
    //                                    {
    //                                        string strParent = MainControl.listTools[iIndex].StrParent;
    //                                        if (strParent == strChildIdSlave)//工具属于 该工具柜
    //                                        {
    //                                            if (listBoxSlave[iList].ToolState != ToolsState.在库)
    //                                            {
    //                                                lock (listBoxSlave)
    //                                                {
    //                                                    listBoxSlave[iList].ToolState = ToolsState.在库;
    //                                                }
    //                                                string strToolType = listBoxSlave[iList].StrToolType;
    //                                                string strToolName = listBoxSlave[iList].StrToolName;
    //                                                string strToolID = listBoxSlave[iList].StrToolId;
    //                                                string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //                                                string strSql = strSql = "update tb_Tools set IsInStore='" + ToolsState.在库.ToString() + "',BorrowReturnTime='" + strTime + "' where RFIDCoding='" + strEpc + "' ";
    //                                                datalogic.SqlComNonQuery(strSql);
    //                                                strSql = strSql = "update tb_RecordBorrow set PeopleReturn='',ReturnTime='" + strTime + "'," +
    //                                                 "BorrowDuration='' where RFIDCoding='" + strEpc + "' and ReturnTime='' ";
    //                                                datalogic.SqlComNonQuery(strSql);

    //                                                //更新 listTools
    //                                                lock (MainControl.listTools)
    //                                                {
    //                                                    MainControl.listTools[iIndex].ToolState = ToolsState.在库;
    //                                                    MainControl.listTools[iIndex].TimeBorrRet = DateTime.Now;
    //                                                }

    //                                                DataRow dr = MainControl.dtNewEvent.NewRow();
    //                                                dr["Type"] = EventsType.工具借还.ToString();
    //                                                dr["Point"] = strToolID;
    //                                                dr["Content"] = ToolsBorrRet.归还.ToString();
    //                                                dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                                                MainControl.dtNewEvent.Rows.Add(dr);
    //                                            }
    //                                        }
    //                                        break;
    //                                    }
    //                                }
    //                                break;
    //                            }
    //                        }
    //                        if (blHas == false&&frmMain .blDebug )
    //                        {
    //                            int iCount = MainControl.listTools.Count;
    //                            for (int iIndex = 0; iIndex < iCount; iIndex++)
    //                            {
    //                                if (MainControl.listTools[iIndex].StrRfidCode == strEpc)
    //                                {
    //                                    string strParent = MainControl.listTools[iIndex].StrParent;
    //                                    if (strParent != strChildIdSlave)
    //                                    {
    //                                        string strStation = MainControl.listTools[iIndex].StrStation;

    //                                        string strType = AlarmsType.工具放置位置错误.ToString();
    //                                        string strId = MainControl.listTools[iIndex].StrToolId;
    //                                        string strContent = "应放置 " + strStation + " 工具柜";
    //                                        string strPeople = "";
    //                                        string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //                                        DataRow dr = MainControl.dtNewAlarm.NewRow();
    //                                        dr["Type"] = strType;
    //                                        dr["Point"] = strId;
    //                                        dr["Content"] = strContent;
    //                                        dr["People"] = "";
    //                                        dr["Time"] = strTime;
    //                                        MainControl.dtNewAlarm.Rows.Add(dr);

    //                                        string strSql = "insert into tb_AlarmEvent (Type,Point,EventContent,People,Time)" +
    //                                                        "values ('" + strType + "','" + strId + "','" + strContent + "','" + strPeople + "','" + strTime + "')";
    //                                        datalogic.SqlComNonQuery(strSql);

    //                                    }
    //                                    break;
    //                                }
    //                            }

    //                        }
    //                    }

    //                    #endregion

    //                    #region  工具柜中有 扫描列表中没有 借出

    //                    for (int iIndex = 0; iIndex < listBoxSlave.Count; iIndex++)
    //                    {
    //                        string strRfid = listBoxSlave[iIndex].StrRfidCode;
    //                        bool blHas = false;
    //                        for (int i = 0; i < ListEpcSlave.Count; i++)
    //                        {
    //                            if (strRfid == ListEpcSlave[i])
    //                            {
    //                                blHas = true;
    //                                break;
    //                            }
    //                        }

    //                        if (blHas == false)
    //                        {
    //                            if (listBoxSlave[iIndex].ToolState == ToolsState.在库)//原先在库 即借出
    //                            {
    //                                lock (listBoxSlave)
    //                                {
    //                                    listBoxSlave[iIndex].ToolState = ToolsState.借出;
    //                                }
    //                                string strToolType = listBoxSlave[iIndex].StrToolType;
    //                                string strToolName = listBoxSlave[iIndex].StrToolName;
    //                                string strToolID = listBoxSlave[iIndex].StrToolId;
    //                                string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //                                string strSql = strSql = "update tb_Tools set IsInStore='" + ToolsState.借出.ToString() + "',BorrowReturnTime='" + strTime + "' where RFIDCoding='" + strRfid + "' ";
    //                                datalogic.SqlComNonQuery(strSql);

    //                                //tb_RecordBorrow  ID ToolType ToolName ToolID RFIDCoding PeopleBorrow BorrowTime PeopleReturn ReturnTime BorrowDuration     
    //                                strSql = "insert into tb_RecordBorrow (ToolType,ToolName,ToolID,RFIDCoding,PeopleBorrow,BorrowTime,PeopleReturn,ReturnTime,BorrowDuration)" +
    //                                "values ('" + strToolType + "','" + strToolName + "','" + strToolID + "','" + strRfid + "','','" + strTime + "'," +
    //                                 "'','','')";
    //                                datalogic.SqlComNonQuery(strSql);

    //                                //更新 listTools
    //                                int iCount = MainControl.listTools.Count;
    //                                for (int iTools = 0; iTools < iCount; iTools++)
    //                                {
    //                                    if (MainControl.listTools[iTools].StrRfidCode == strRfid)
    //                                    {
    //                                        lock (MainControl.listTools)
    //                                        {
    //                                            MainControl.listTools[iTools].ToolState = ToolsState.借出;
    //                                            MainControl.listTools[iTools].TimeBorrRet = DateTime.Now;
    //                                        }
    //                                        break;
    //                                    }
    //                                }
    //                                DataRow dr = MainControl.dtNewEvent.NewRow();
    //                                dr["Type"] = EventsType.工具借还.ToString();
    //                                dr["Point"] = strToolID;
    //                                dr["Content"] = ToolsBorrRet.借出.ToString();
    //                                dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                                MainControl.dtNewEvent.Rows.Add(dr);
    //                            }
    //                        }

    //                    }

    //                    #endregion

    //                    if (ListEpcSlave.Count > 0)
    //                        ListEpcSlave.Clear();
    //                }
    //            }

    //            #endregion

    //        }
    //        catch (Exception ee)
    //        {
    //            if (frmMain.blDebug)
    //            {
    //                MessageUtil.ShowTips(ee.Message);
    //            }
    //        }
    //    }




    //}
}

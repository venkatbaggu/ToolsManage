using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using ReaderB;

namespace ToolsManage.BaseClass
{
    //public class RfidRead
    //{
    //    DataLogic datalogic = new DataLogic();
    //    public TimerHelper timerRfid = new TimerHelper(2000, false);
    //    //public TimerHelperForm timerRfidFast = new TimerHelperForm(100, false);
    //    public TimerHelper timerRfidFast = new TimerHelper(100, false);

    //    private int iPort = 27011;
    //    private string strIp;
    //    private bool blConnent = false;
    //    private bool blScan = false;

    //    /// <summary>
    //    /// RFID 上次连接时间
    //    /// </summary>
    //    DateTime timeLastConnent = DateTime.Now.AddMinutes(-10);
    //    CommuniState CommuniState = CommuniState.初值;

    //    #region  RFID读写器 变量

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
    //    /// 高频率读到的标签数据
    //    /// </summary>
    //    private string strEpcAllFast = "";
    //    /// <summary>
    //    /// 高频率读到的标签 数量
    //    /// </summary>
    //    private int iCardCountFast = 0;

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

    //    public RfidRead(string ip,int port)
    //    {
    //        iPort = port;
    //        strIp = ip;

    //        timerRfid.Execute += new TimerHelper.TimerExecution(Rfid_Execute);
    //        //timerRfidFast.Execute += new TimerHelperForm.TimerExecution(RfidFast_Execute);
    //        timerRfidFast.Execute += new TimerHelper.TimerExecution(RfidFast_Execute);
    //    }

    //    #region 属性

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

    //    #region 子程序

    //    public bool SetAnt(DeviceUsing ant1, DeviceUsing ant2,DeviceUsing ant3,DeviceUsing ant4)
    //    {
    //        bool blRet = false;
    //        try
    //        {
    //            byte ANT = 0;
    //            if (ant1 == DeviceUsing.启用)
    //                ANT = Convert.ToByte(ANT | 1);
    //            if (ant2 == DeviceUsing.启用)
    //                ANT = Convert.ToByte(ANT | 2);
    //            if (ant3 == DeviceUsing.启用)
    //                ANT = Convert.ToByte(ANT | 4);
    //            if (ant4 == DeviceUsing.启用)
    //                ANT = Convert.ToByte(ANT | 8);
    //            int iRet = StaticClassReaderB.SetAntennaMultiplexing(ref fComAdr, ANT, frmcomportindex);
    //            if (iRet == 0)
    //                blRet = true;
    //            else
    //                blRet = false;
    //        }
    //        catch (Exception ex)
    //        {
    //            if (frmMain.blDebug)
    //                MessageUtil.ShowTips(ex.Message);
    //        }
    //        return blRet;
    //    }

    //    public bool OpenNetPort()
    //    {
    //        bool blRet=false ;
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
    //            StaticClassReaderB.CloseNetPort(iPort);
    //            StaticClassReaderB.CloseNetPort(frmcomportindex);
    //        }
    //        catch (Exception ex)
    //        {
    //            if (frmMain.blDebug)
    //                MessageUtil.ShowTips(ex.Message);
    //        }
    //    }

    //    public void StatrRead()
    //    {
    //        timerRfid.Start();
    //        timerRfidFast.Start();
    //    }

    //    public void StopRead()
    //    {
    //        timerRfid.Stop();
    //        timerRfidFast.Stop();
    //    }

    //    private void NewAlarmEvent(AlarmContent content)
    //    {
    //        string strType = AlarmsType.RFID读写器通信错误.ToString();
    //        string strPoint = strIp;
    //        string strContent = content.ToString();
    //        string strPeople = "";
    //        string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //        if (frmMain.blDebug)
    //        {
    //            DataRow dr = MainControl.dtNewAlarm.NewRow();
    //            dr["Type"] = strType;
    //            dr["Point"] = strPoint;
    //            dr["Content"] = strContent;
    //            dr["People"] = strPeople;
    //            dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //            MainControl.dtNewAlarm.Rows.Add(dr);
    //        }
    //        string strSql = "insert into tb_AlarmEvent (Type,Point,EventContent,People,Time)" +
    //                                                   "values ('" + strType + "','" + strPoint + "','" + strContent + "','" + strPeople + "','" + strTime + "')";
    //        datalogic.SqlComNonQuery(strSql);
    //    }

    //    #endregion

    //    private void Rfid_Execute()
    //    {
    //        try
    //        {
    //            #region 查询 RFID 读写器

    //            int CardCount = 0;
    //            string strEpcAll = "";
    //            if (iCardCountFast != 0 && !string.IsNullOrEmpty(strEpcAllFast))
    //            {
    //                lock (strEpcAllFast)
    //                {
    //                    strEpcAll = strEpcAllFast;
    //                    strEpcAllFast = "";
    //                    CardCount = iCardCountFast;
    //                    iCardCountFast = 0;
    //                }
    //            }

    //            if (CardCount != 0)//strEpcAll != strRecordEpc &&
    //            {
    //                int iLen = strEpcAll.Length;
    //                iLen /= 2;
    //                byte[] bEpcCopy = new byte[iLen];//访问密码数组形式
    //                bEpcCopy = SerialPortUtil.HexStrToByte(strEpcAll);
    //                //bEpcCopy = HexStringToByteArray(strEpcAll);
    //                int iEpcIndex = 0;//单张EPC的长度在bEpcCopy中的位置
    //                for (int i = 0; i < CardCount; i++)
    //                {
    //                    int iEpcLen = bEpcCopy[iEpcIndex];//epc的长度
    //                    string strEpc = strEpcAll.Substring(iEpcIndex * 2 + 2, iEpcLen * 2);//得到一个EPC数据
    //                    iEpcIndex = iEpcIndex + iEpcLen + 1;//下一个EPC标签的 标签长度指示位置

    //                    if (strEpc.Length > 12)
    //                        strEpc = strEpc.Substring(0, 12);

    //                    #region  根据状态 借出或归还

    //                    int iCount =MainControl. listTools.Count;
    //                    for (int iIndex = 0; iIndex < iCount; iIndex++)
    //                    {
    //                        if (MainControl.listTools[iIndex].StrRfidCode == strEpc)//dtTools ToolID  RfidCoding ToolBR TimeBR 
    //                        {
    //                            DateTime timeLast = MainControl.listTools[iIndex].TimeBorrRet;
    //                            TimeSpan timeSpan = DateTime.Now - timeLast;
    //                            if (timeSpan.TotalSeconds >= MainControl.iBorrRetSpan)
    //                            {
    //                                string strToolType = MainControl.listTools[iIndex].StrToolType;
    //                                string strToolName = MainControl.listTools[iIndex].StrToolName;
    //                                string strToolID = MainControl.listTools[iIndex].StrToolId;
    //                                string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //                                if (MainControl.listTools[iIndex].ToolState == ToolsState.在库)
    //                                {
    //                                    lock (MainControl.listTools)
    //                                    {
    //                                        MainControl.listTools[iIndex].ToolState = ToolsState.借出;

    //                                        string strSql = strSql = "update tb_Tools set IsInStore='" + ToolsState.借出 .ToString() + "',BorrowReturnTime='" + strTime + "' where RFIDCoding='" + strEpc + "' ";
    //                                        datalogic.SqlComNonQuery(strSql);

    //                                        //tb_RecordBorrow  ID ToolType ToolName ToolID RFIDCoding PeopleBorrow BorrowTime PeopleReturn ReturnTime BorrowDuration     
    //                                        strSql = "insert into tb_RecordBorrow (ToolType,ToolName,ToolID,RFIDCoding,PeopleBorrow,BorrowTime,PeopleReturn,ReturnTime,BorrowDuration)" +
    //                                        "values ('" + strToolType + "','" + strToolName + "','" + strToolID + "','" + strEpc + "','','" + strTime + "'," +
    //                                         "'','','')";
    //                                        datalogic.SqlComNonQuery(strSql);

    //                                        DataRow dr = MainControl.dtNewEvent.NewRow();
    //                                        dr["Type"] = EventType.工具借还.ToString();
    //                                        dr["Point"] = MainControl.listTools[iIndex].StrToolId;
    //                                        dr["Content"] = ToolsBorrRet.借出.ToString();
    //                                        dr["Time"] = strTime;
    //                                        MainControl.dtNewEvent.Rows.Add(dr);
    //                                    }
    //                                }
    //                                else if (MainControl.listTools[iIndex].ToolState == ToolsState.借出)
    //                                {
    //                                    lock (MainControl.listTools)
    //                                    {
    //                                        MainControl.listTools[iIndex].ToolState = ToolsState.在库;

    //                                        string strSql = strSql = "update tb_Tools set IsInStore='" + ToolsState.在库 .ToString ()+ "',BorrowReturnTime='" + strTime + "' where RFIDCoding='" + strEpc + "' ";
    //                                        datalogic.SqlComNonQuery(strSql);
    //                                        strSql = strSql = "update tb_RecordBorrow set PeopleReturn='',ReturnTime='" + strTime + "'," +
    //                                         "BorrowDuration='' where RFIDCoding='" + strEpc + "' and ReturnTime='' ";
    //                                        datalogic.SqlComNonQuery(strSql);

    //                                        DataRow dr = MainControl.dtNewEvent.NewRow();
    //                                        dr["Type"] = EventType.工具借还.ToString();
    //                                        dr["Point"] = MainControl.listTools[iIndex].StrToolId;
    //                                        dr["Content"] = ToolsBorrRet.归还.ToString();
    //                                        dr["Time"] = strTime;
    //                                        MainControl.dtNewEvent.Rows.Add(dr);
    //                                    }
    //                                }
    //                                lock (MainControl.listTools)
    //                                {
    //                                    MainControl.listTools[iIndex].TimeBorrRet = DateTime.Now;
    //                                }
    //                            }


    //                            break;
    //                        }
    //                    }


    //                    #endregion
    //                }
    //            }

    //            #endregion
    //        }
    //        catch (Exception ex)
    //        {
    //            if (frmMain.blDebug)
    //                MessageUtil.ShowTips(ex.Message);
    //        }
    //    }

    //    private void RfidFast_Execute()
    //    {
    //        if (blConnent==false )
    //        {
    //            TimeSpan timespan = DateTime.Now - timeLastConnent;
    //            if (timespan.TotalSeconds > 30)
    //            {
    //                timeLastConnent = DateTime.Now;
    //                bool blRet = OpenNetPort();
    //                JudgeRfidState(blRet);
    //            }
    //        }

    //        if (blScan)
    //            return;
    //        if (blConnent)
    //        {
    //          bool blRet=  Inventory();
    //          JudgeRfidState(blRet);

    //          if (!blRet)
    //              CloseNetPort();
    //        }
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
    //            if (CommuniState == CommuniState.已断开)
    //                NewAlarmEvent(AlarmContent.通信恢复正常);
    //            CommuniState = CommuniState.已连接;
    //        }
    //        else
    //        {
    //            blConnent = false;
    //            if (CommuniState != CommuniState.已断开)
    //                NewAlarmEvent(AlarmContent.通信异常);
    //            CommuniState = CommuniState.已断开;
    //        }
    //    }

    //    private bool  Inventory()
    //    {
    //        bool blRet = false;
    //        try
    //        {
    //            blScan = true;
    //            int CardCount = 0;//输出变量，电子标签的张数
    //            int EpcLen = 0;//EPC 的字节数
    //            byte[] bEpcArray = new byte[5000];//指向输出数组变量 是读到的电子标签的EPC数据，是一张标签的EPC长度+一张标签的EPC号，依此累加

    //            fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, bEpcArray, ref Ant, ref EpcLen, ref CardCount, frmcomportindex);// 8616
    //            if ((fCmdRet == 1) || (fCmdRet == 2) || (fCmdRet == 3) || (fCmdRet == 4) || (fCmdRet == 0xFB))//代表已查找结束，
    //            {
    //                byte[] bEpcCopy = new byte[EpcLen];
    //                Array.Copy(bEpcArray, bEpcCopy, EpcLen);
    //                string strEpcAll = SerialPortUtil.ByteToStr(bEpcCopy);
    //                if (CardCount != 0)
    //                {
    //                    lock (strEpcAllFast)
    //                    {
    //                        strEpcAllFast += strEpcAll;
    //                        iCardCountFast += CardCount;
    //                    }
    //                }
    //                blRet = true;
    //            }
    //            else
    //            {
    //                blRet = false;
    //            }
    //            blScan = false;
    //        }
    //        catch (Exception ee)
    //        {
    //            if (frmMain.blDebug)
    //            {
    //                MessageUtil.ShowTips(ee.Message);
    //            }
    //        }
    //        return blRet;
    //    }


    //}
}

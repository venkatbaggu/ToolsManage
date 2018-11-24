using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Data;

using ToolsManage.BaseClass.EnvirClass;


namespace ToolsManage.BaseClass
{
    /// <summary>
    /// 串口1 收发 烟感 温湿度   
    /// </summary>
    public class clsSerialSensor
    {
        //Crc crc16 = new Crc();

        public SerialPortSensor serial = new SerialPortSensor("COM1", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);

        public byte[] SendWsd = new byte[5] { 0x23, 0xc3, 0x00, 0x01, 0xe7 };     //温度发送数据 0x01地址 0xe7校验和

        public clsSerialSensor()
        {

        }

        /// <summary>
        /// 查询温湿度 值
        /// </summary>
        /// <param name="iAddr"></param>
        /// <returns></returns>
        public bool AskWsd(int iAddr,ref int[] wsd)
        {
            bool IsRec = false;
            wsd = new int[2] { 25, 55 };
            SendWsd[4] = 0;
            SendWsd[3] = (byte)iAddr;
            for (int i = 0; i < 4; i++)
            {
                SendWsd[4] = (byte)(SendWsd[4] + SendWsd[i]);
            }
            byte[] bRece = null;
            int iReceLen = serial.SendReceByte(SendWsd, ref bRece, 10,IsWait.CanStop);
            //txt 发送 接受
            txtLog.TxtWriteByte("Sensor.txt", "Wsd Send", SendWsd, false);
            if (iReceLen > 0)
            {
                txtLog.TxtWriteByte("Sensor.txt", "Wsd Rece", bRece, true);
            }
            if (bRece != null && bRece.Length > 0)
            {
                int index = bRece.FindIndex(p => p == 0x2A);
                if (index >= 0)
                {
                    byte ReceCheck = 0;
                    byte[] retdata = new byte[7];
                    Array.Copy(bRece, index, retdata, 0, 7);
                    if (retdata != null && retdata.Length >= 7)
                    {
                        for (byte bLen = 0; bLen < 6; bLen++)
                        {
                            ReceCheck = (byte)(ReceCheck + retdata[bLen]);
                        }
                        if (retdata.Length == 7 && retdata[0] == 0x2A && retdata[1] == 0x44 &&
                            retdata[2] == retdata[3] && retdata[4] == retdata[5] && retdata[6] == ReceCheck)//2A 44 B3 B3 C9 C9 66
                        {
                            IsRec = true;
                            //温度
                            byte b = retdata[2];
                            if ((b & 0x80) == 0x80)
                            {
                                wsd[0] = 0 - b & 0x7f;
                            }
                            else
                            {
                                wsd[0] = b;
                            }
                            //湿度
                            wsd[1] = retdata[4];
                        }
                        else//不等于 7位校验 正确
                        {
                            wsd[0] = 25;
                            wsd[1] = 55;
                        }
                    }
                }
            }
            return IsRec;
        }

        /// <summary>
        /// 查询烟感
        /// </summary>
        /// <param name="strAddr"></param>
        /// <returns></returns>
        public ProbeState AskFire(string strAddr)
        {
            ProbeState probe = ProbeState.正常;
            string strFireSend = "IN4";
            strAddr = strAddr.PadLeft(2, '0');
            strFireSend += strAddr;//地址
            strFireSend += "003R000\r\n";

            string strRet = serial.SendReceStr(strFireSend, 8,IsWait.CanStop);
            //txt 发送 接受
            //strRet = strFireSend.Substring(0, strFireSend.Length - 1);
            if (!String.IsNullOrEmpty(strRet) && strRet.Length>=12)
            {
                int index = strRet.IndexOf("IN");
                if (strRet.Length - index >= 12)
                {
                    strRet = strRet.Substring(index, 12);

                    txtLog.TxtWriteStr("Sensor.txt", "Yw Send", strFireSend, false);
                    if (strRet.Length > 0)
                    {
                        txtLog.TxtWriteStr("Sensor.txt", "Yw Rece", strRet, true);
                    }
                    if (strRet.Length >= 12)
                    {
                        string strRetHand = strRet.Substring(0, 3);//IN3   IN301003R000
                        string strRetAddr = strRet.Substring(3, 2);//01
                        string strRetOther = strRet.Substring(5, 5);//003R0
                        if (strRetHand == "IN4" && strRetAddr == strAddr && strRetOther == "003R0" && strRet[11] == '0')
                        {
                            if (strRet[10] == '0')
                            {
                                probe = ProbeState.正常;
                            }
                            else if (strRet[10] == '1')
                            {
                                probe = ProbeState.报警;
                            }
                        }
                        else
                        {
                            probe = ProbeState.通信错误;
                        }
                    }
                    else
                    {
                        probe = ProbeState.不通讯;
                    }
                }
            }
            return probe;
        }


    }

}

#region  控制空调 开关 模式 温度及 存储事件

///// <summary>
///// 关空调 及事件 存储
///// </summary>
///// <param name="iRoomIndex"></param>
///// <param name="iIndex"></param>
//public bool  OffAirAndRecord(int iRoomIndex, int iIndex)
//{
//    bool blRet = false;
//    byte btAddr = (byte)clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].IntAddr;
//    if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.大金)
//    {

//    }
//    else if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.其他)
//    {

//    }
//    if (blRet)
//    {
//        string strSql = "insert into tb_EnviriEvent (Type,Area,Reason,EventContent,Time)values " +
//       "('" + EventType.空调.ToString() + "','" + clsEnvirControl.listRoom[iRoomIndex].StrName + "','" + DeviceRunModel.自动.ToString() + "'," +
//       "'" + EventContent.关闭.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//        datalogic.SqlComNonQuery(strSql);
//        lock (clsEnvirControl.listRoom)
//        {
//            clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].State = DeviceRunState.停止;
//        }
//        DataRow dr = MainControl.dtNewEvent.NewRow();
//        dr["Type"] = EventType.空调.ToString();
//        dr["Point"] = clsEnvirControl.listRoom[iRoomIndex].StrName;
//        dr["Content"] = EventContent.关闭.ToString();
//        dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//        MainControl.dtNewEvent.Rows.Add(dr);
//    }
//    return blRet;
//}

///// <summary>
///// 开空调 及事件 存储
///// </summary>
///// <param name="iRoomIndex"></param>
///// <param name="iIndex"></param>
//public bool  OnAirAndRecord(int iRoomIndex, int iIndex)
//{
//    bool blRet = false;
//    byte btAddr = (byte)clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].IntAddr;
//    if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.大金)
//    {

//    }
//    else if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.其他)
//    {

//    }

//    if (blRet)
//    {
//        string strSql = "insert into tb_EnviriEvent (Type,Area,Reason,EventContent,Time)values " +
//       "('" + EventType.空调.ToString() + "','" + clsEnvirControl.listRoom[iRoomIndex].StrName + "','" + DeviceRunModel.自动.ToString() + "'," +
//       "'" + EventContent.开启.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//        datalogic.SqlComNonQuery(strSql);
//        lock (clsEnvirControl.listRoom)
//        {
//            clsEnvirControl.listRoom[iRoomIndex].listAir [iIndex].State = DeviceRunState.运行;
//        }

//        DataRow dr = MainControl.dtNewEvent.NewRow();
//        dr["Type"] = EventType.空调.ToString();
//        dr["Point"] = clsEnvirControl.listRoom[iRoomIndex].StrName;
//        dr["Content"] = EventContent.开启.ToString();
//        dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//        MainControl.dtNewEvent.Rows.Add(dr);
//    }
//    return blRet;
//}

///// <summary>
///// 设置 空调 制冷 模式 及事件 存储
///// </summary>
///// <param name="iRoomIndex"></param>
///// <param name="iIndex"></param>
///// <returns></returns>
//public bool SetCoolModeAirAndRecord(int iRoomIndex, int iIndex)
//{
//    bool blRet = false;
//    byte btAddr = (byte)clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].IntAddr;
//    if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.大金)
//    {

//    }
//    else if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.其他)
//    {

//    }
//    if (blRet)
//    {
//        string strSql = "insert into tb_EnviriEvent (Type,Area,Reason,EventContent,Time)values " +
//       "('" + EventType.空调.ToString() + "','" + clsEnvirControl.listRoom[iRoomIndex].StrName + "','" + DeviceRunModel.自动.ToString() + "'," +
//       "'" + EventContent.设置制冷.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//        datalogic.SqlComNonQuery(strSql);
//        lock (clsEnvirControl.listRoom)
//        {
//            clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].Model = AirRunModel.制冷;
//        }

//        DataRow dr = MainControl.dtNewEvent.NewRow();
//        dr["Type"] = EventType.空调.ToString();
//        dr["Point"] = clsEnvirControl.listRoom[iRoomIndex].StrName;
//        dr["Content"] = EventContent.设置制冷.ToString();
//        dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//        MainControl.dtNewEvent.Rows.Add(dr);
//    }

//    return blRet;
//}

///// <summary>
///// 设置 空调 制热 模式 及事件 存储
///// </summary>
///// <param name="iRoomIndex"></param>
///// <param name="iIndex"></param>
///// <returns></returns>
//public bool SetHotModeAirAndRecord(int iRoomIndex, int iIndex)
//{
//    bool blRet = false;
//    byte btAddr = (byte)clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].IntAddr;
//    if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.大金)
//    {

//    }
//    else if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.其他)
//    {

//    }
//    if (blRet)
//    {
//        string strSql = "insert into tb_EnviriEvent (Type,Area,Reason,EventContent,Time)values " +
//       "('" + EventType.空调.ToString() + "','" + clsEnvirControl.listRoom[iRoomIndex].StrName + "','" + DeviceRunModel.自动.ToString() + "'," +
//       "'" + EventContent.设置制热.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//        datalogic.SqlComNonQuery(strSql);
//        lock (clsEnvirControl.listRoom)
//        {
//            clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].Model = AirRunModel.制热;
//        }
//        DataRow dr = MainControl.dtNewEvent.NewRow();
//        dr["Type"] = EventType.空调.ToString();
//        dr["Point"] = clsEnvirControl.listRoom[iRoomIndex].StrName;
//        dr["Content"] = EventContent.设置制热.ToString();
//        dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//        MainControl.dtNewEvent.Rows.Add(dr);
//    }
//    return blRet;
//}

///// <summary>
///// 设置空调温度 及 记录
///// </summary>
///// <param name="iRoomIndex"></param>
///// <param name="iIndex"></param>
///// <param name="btSetTemp"></param>
///// <returns></returns>
//public bool SetTempAirAndRecord(int iRoomIndex, int iIndex,byte btSetTemp)
//{
//    bool blRet = false;
//    byte btAddr = (byte)clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].IntAddr;
//    if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.大金)
//    {

//    }
//    else if (clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.其他)
//    {

//    }
//    if (blRet)
//    {
//        string strSql = "insert into tb_EnviriEvent (Type,Area,Reason,EventContent,Time)values " +
//       "('" + EventType.空调.ToString() + "','" + clsEnvirControl.listRoom[iRoomIndex].StrName + "','" + DeviceRunModel.自动.ToString() + "'," +
//       "'" + EventContent.设置温度.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//        datalogic.SqlComNonQuery(strSql);
//        lock (clsEnvirControl.listRoom)
//        {
//            clsEnvirControl.listRoom[iRoomIndex].listAir[iIndex].IntTempSet = (int)btSetTemp;
//        }

//        DataRow dr = MainControl.dtNewEvent.NewRow();
//        dr["Type"] = EventType.空调.ToString();
//        dr["Point"] = clsEnvirControl.listRoom[iRoomIndex].StrName;
//        dr["Content"] = EventContent.设置温度.ToString() + btSetTemp.ToString();
//        dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//       MainControl. dtNewEvent.Rows.Add(dr);
//    }
//    return blRet;
//}

#endregion

///// <summary>
///// 开启关闭 设备 空调、加热、除湿、新风
///// </summary>
///// <param name="Addr">板地址</param>
///// <param name="bOnOff">开启关闭4</param>
///// <param name="bHotDehumi">继电器序号 加热为0 除湿为1 即继电器0 继电器1 </param>
//public bool OnOffDevice(int Addr, byte bOnOff, byte bRelayNo)
//{
//    bool blRet = false;
//    SendRelay[0] = (byte)Addr;
//    SendRelay[1] = 0x05;
//    SendRelay[2] = 0x00;
//    SendRelay[3] = bRelayNo;
//    SendRelay[4] = bOnOff;
//    SendRelay[5] = 0x00;
//    ushort crc = crc16.CalculateCrc16(SendRelay, 6);
//    SendRelay[7] = (byte)(crc);
//    SendRelay[6] = (byte)(crc >> 8);

//    //txt 发送
//    string strContent = SerialPortUtil.ByteToStrHex(SendRelay);
//    txtLog.WriteTxt("Relay.txt", "OnOff Send", strContent, false);

//    byte[] bRece = new byte[8];
//    int iRetLen = serial.SendReceByte(SendRelay, ref bRece, 300);
//    //txt 接受
//    if (iRetLen > 0)
//    {
//        strContent = SerialPortUtil.ByteToStrHex(bRece);
//        txtLog.WriteTxt("Relay.txt", "OnOff Rece", strContent, true);
//    }
//    if (iRetLen == 8 && bRece[0] == SendRelay[0] && bRece[1] == SendRelay[1] && bRece[2] == SendRelay[2] && bRece[3] == SendRelay[3] &&
//         bRece[4] == SendRelay[4] && bRece[5] == SendRelay[5] && bRece[6] == SendRelay[6] && bRece[7] == SendRelay[7])
//    {
//        blRet = true;
//    }
//    return blRet;
//}

///// <summary>
///// 查询 烘干 除湿 新风的状态 即查询继电器状态
///// </summary>
///// <param name="Addr">板地址</param>
//public bool AskDeviceState(int Addr)
//{
//    //SerialPortMain.comPort.ReadExisting();
//    bool blRet = false;//01 01 00 00 00 04 3D C9
//    SendRelay[0] = (byte)Addr;
//    SendRelay[1] = 0x01;
//    SendRelay[2] = 0x00;
//    SendRelay[3] = 0x00;
//    SendRelay[4] = 0x00;
//    SendRelay[5] = 0x04;
//    ushort crc = crc16.CalculateCrc16(SendRelay, 6);
//    SendRelay[7] = (byte)(crc);
//    SendRelay[6] = (byte)(crc >> 8);

//    //txt 发送
//    string strContent = SerialPortUtil.ByteToStrHex(SendRelay);
//    txtLog.WriteTxt("Relay.txt", "Ask Send", strContent, false);

//    byte[] bRece = new byte[6];
//    int iRetLen = serial.SendReceByte(SendRelay, ref bRece, 1000);
//    //txt 接受
//    if (iRetLen > 0)
//    {
//        strContent = SerialPortUtil.ByteToStrHex(bRece);
//        txtLog.WriteTxt("Relay.txt", "Ask Rece", strContent, true);//01 01 01 0F 11 8C
//    }
//    crc = crc16.CalculateCrc16(bRece, 4);
//    byte bCrcL = (byte)(crc);
//    byte bCrcH = (byte)(crc >> 8);
//    if (iRetLen == 6 && bRece[0] == SendRelay[0] && bRece[1] == 0x01 && bRece[2] == 0x01 && bRece[4] == bCrcH && bRece[5] == bCrcL)
//    {
//        blRet = true;
//        bDeviceState = bRece[3];
//    }
//    return blRet;
//}

///// <summary>
///// 查询输入状态
///// </summary>
///// <param name="Addr">板地址</param>
//public bool AskInState(int Addr)
//{
//    bool blRet = false;//01 01 00 00 00 04 3D C9
//    SendRelay[0] = (byte)Addr;
//    SendRelay[1] = (byte)RelayFn.AskIn;
//    SendRelay[2] = 0x00;
//    SendRelay[3] = 0x00;
//    SendRelay[4] = 0x00;
//    SendRelay[5] = 0x06;
//    ushort crc = crc16.CalculateCrc16(SendRelay, 6);
//    SendRelay[7] = (byte)(crc);
//    SendRelay[6] = (byte)(crc >> 8);

//    //txt 发送
//    string strContent = SerialPortUtil.ByteToStrHex(SendRelay);
//    txtLog.WriteTxt("Relay.txt", "AskIn Send", strContent, false);

//    byte[] bRece = new byte[6];
//    int iRetLen = serial.SendReceByte(SendRelay, ref bRece, 300);
//    //txt 接受
//    if (iRetLen > 0)
//    {
//        strContent = SerialPortUtil.ByteToStrHex(bRece);
//        txtLog.WriteTxt("Relay.txt", "AskIn Rece", strContent, true);
//    }
//    crc = crc16.CalculateCrc16(bRece, 4);
//    byte bCrcL = (byte)(crc);
//    byte bCrcH = (byte)(crc >> 8);
//    if (iRetLen == 6 && bRece[0] == SendRelay[0] && bRece[1] == (byte)RelayFn.AskIn && bRece[2] == 0x01 && bRece[4] == bCrcH && bRece[5] == bCrcL)
//    {
//        blRet = true;
//        bInState = bRece[3];
//    }
//    return blRet;
//}
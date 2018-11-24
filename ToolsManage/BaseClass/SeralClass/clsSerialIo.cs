using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Threading;

using ToolsManage.BaseClass.EnvirClass;
using ToolsManage.BaseClass.DoorClass;

namespace ToolsManage.BaseClass
{
    /// <summary>
    /// 串口2 收发 继电器
    /// </summary>
    public class clsSerialIo
    {
        Crc crc16 = new Crc();
        DataLogic datalogic = new DataLogic();
        clsCommon commonCls = new clsCommon();
        public SerialPortIo serial = new SerialPortIo("COM2", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);


        byte[] SendRelay = new byte[8] { 0x01, 0x05, 0x00, 0x00, 0xFF, 0x00, 0x8C, 0x3A };//01 02 00 00 00 06 F8 08

        #region  事件接口

        /// <summary>
        /// 新事件显示 事件
        /// </summary>
        public event NewEventShowEventHandler NewEventShowEvent;

        ///// <summary>
        ///// 新告警显示 事件
        ///// </summary>
        //public event NewEventShowEventHandler NewAlarmShowEvent;

        #endregion

        public clsSerialIo()
        {

        }

        /// <summary>
        /// 更新和记录 继电器 控制设备 运行或关闭
        /// </summary>
        public void DeviceNewAndRecord(int iRoomIndex, EventType eventType, DeviceRunState onOrOff, DeviceRunModel handOrAuto,bool blRecord)
        {
            EventContent eventContent = EventContent.开启;

            if (onOrOff == DeviceRunState.运行)
            {
                eventContent = EventContent.开启;
                if (eventType == EventType.烘干)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomHot.State = DeviceRunState.运行;
                    }
                }
                else if (eventType == EventType.除湿)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomDehumi.State = DeviceRunState.运行;
                    }
                }
                else if (eventType == EventType.新风)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomFan.State = DeviceRunState.运行;
                    }
                }
            }
            else if (onOrOff == DeviceRunState.停止)
            {
                eventContent = EventContent.关闭;
                if (eventType == EventType.烘干)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomHot.State = DeviceRunState.停止;
                    }
                }
                else if (eventType == EventType.除湿)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomDehumi.State = DeviceRunState.停止;
                    }
                }
                else if (eventType == EventType.新风)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomFan.State = DeviceRunState.停止;
                    }
                }
            }
            if (handOrAuto == DeviceRunModel.手动)
            {
                if (eventType == EventType.烘干)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomHot.HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iRoomIndex].roomHot.TimeHand = DateTime.Now;
                    }
                }
                else if (eventType == EventType.除湿)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomDehumi .HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iRoomIndex].roomDehumi.TimeHand = DateTime.Now;
                    }
                }
                else if (eventType == EventType.新风)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iRoomIndex].roomFan .HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iRoomIndex].roomFan.TimeHand = DateTime.Now;
                    }
                }
            }
            if (blRecord)
            {
                string strSql = "insert into tb_EnviriEvent (Type,Area,Reason,EventContent,Time)values " +
                    "('" + eventType.ToString() + "','" + clsEnvirControl.listRoom[iRoomIndex].StrName + "','" + handOrAuto.ToString() + "'," +
                    "'" + eventContent.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                datalogic.SqlComNonQuery(strSql);

                //if (NewEventShowEvent != null)
                //{
                //    string strPoint = strArea;
                //    if (blMoreAir)
                //        strPoint += btAddr.ToString();
                //    if (content == EventContent.设置温度)
                //    {
                //        NewEventShowEvent(new NewEventEventArgs(EventType.空调, strPoint, content.ToString() + btSetTemp.ToString(), "", ""));
                //    }
                //    else
                //    {
                //        NewEventShowEvent(new NewEventEventArgs(EventType.空调, strPoint, content.ToString(), "", ""));
                //    }
                //}

            }

        }

        #region  继电器

        /// <summary>
        /// 开启关闭 设备 空调、加热、除湿、新风
        /// </summary>
        /// <param name="Addr">板地址</param>
        /// <param name="bOnOff">开启关闭4</param>
        /// <param name="bHotDehumi">继电器序号 加热为0 除湿为1 即继电器0 继电器1 </param>
        public bool OnOffDevice(int Addr, OnOffRelay bOnOff, DeviceRelayNo bRelayNo, string area, DeviceRunModel runModel,bool blRecord,IsWait isWait)
        {
            bool blRet = false;
            SendRelay[0] = (byte)Addr;
            SendRelay[1] = 0x05;
            SendRelay[2] = 0x00;
            SendRelay[3] = (byte)bRelayNo;
            SendRelay[4] =(byte) bOnOff;
            SendRelay[5] = 0x00;
            ushort crc = crc16.CalculateCrc16(SendRelay, 6);
            SendRelay[7] = (byte)(crc);
            SendRelay[6] = (byte)(crc >> 8);

            byte[] bRece = new byte[8];
            int iReceLen = serial.SendReceByte(SendRelay, ref bRece, 10, isWait);
            //txt 发送 接受
            txtLog.TxtWriteByte("Io.txt", "OnOff Send", SendRelay, false);
            if (iReceLen > 0)
            {
                txtLog.TxtWriteByte("Io.txt", "OnOff Rece", bRece, true);
            }
            if (iReceLen == 8 && bRece[0] == SendRelay[0] && bRece[1] == SendRelay[1] && bRece[2] == SendRelay[2] && bRece[3] == SendRelay[3] &&
                 bRece[4] == SendRelay[4] && bRece[5] == SendRelay[5] && bRece[6] == SendRelay[6] && bRece[7] == SendRelay[7])
            {
                blRet = true;

                if (blRecord)
                {
                    EventType eventType = EventType.初值;
                    switch (bRelayNo)
                    {
                        case DeviceRelayNo.除湿:
                            eventType = EventType.除湿;
                            break;

                        case DeviceRelayNo.烘干:
                            eventType = EventType.烘干;
                            break;

                        case DeviceRelayNo.警灯:
                            eventType = EventType.警灯;
                            break;

                        case DeviceRelayNo.新风:
                            eventType = EventType.新风;
                            break;
                    }
                    string strContent = bOnOff.ToString();
                    if (eventType != EventType.初值)
                    {
                        NewEventRecord( eventType, area, runModel, strContent);
                        //commonCls.NewEnvirEventRecord(eventType, area, runModel, strContent);
                        //if (NewEventShowEvent != null)
                        //{
                        //    NewEventShowEvent(new NewEventEventArgs(eventType, area, strContent, "", ""));
                        //}
                    }
                }
            }
            return blRet;
        }

        /// <summary>
        /// 先 环境事件 显示和存储
        /// </summary>
        public void NewEventRecord(EventType eventType,string area,DeviceRunModel runModel,string strContent)
        {  
            commonCls.NewEnvirEventRecord(eventType, area, runModel, strContent);
            if (NewEventShowEvent != null)
            {
                NewEventShowEvent(new NewEventEventArgs(eventType, area, strContent, "", "", DateTime.Now));
            }
        }

        /// <summary>
        /// 查询 烘干 除湿 新风的状态 即查询继电器状态
        /// </summary>
        /// <param name="Addr">板地址</param>
        public byte[] AskDeviceState(int Addr)
        {
            byte[] btRet = new byte[2];
            SendRelay[0] = (byte)Addr;
            SendRelay[1] = 0x01;
            SendRelay[2] = 0x00;
            SendRelay[3] = 0x00;
            SendRelay[4] = 0x00;
            SendRelay[5] = 0x04;
            ushort crc = crc16.CalculateCrc16(SendRelay, 6);
            SendRelay[7] = (byte)(crc);
            SendRelay[6] = (byte)(crc >> 8);
            byte[] bRece = new byte[6];
            int iReceLen = serial.SendReceByte(SendRelay, ref bRece, 10, IsWait.CanStop);
            //txt 发送 接受
            txtLog.TxtWriteByte("Io.txt", "AskIn Send", SendRelay, false);
            if (iReceLen > 0)
            {
                txtLog.TxtWriteByte("Io.txt", "AskIn Rece", bRece, true);
            }
            crc = crc16.CalculateCrc16(bRece, 4);
            byte bCrcL = (byte)(crc);
            byte bCrcH = (byte)(crc >> 8);
            if (iReceLen == 6 && bRece[0] == SendRelay[0] && bRece[1] == 0x01 && bRece[2] == 0x01 && bRece[4] == bCrcH && bRece[5] == bCrcL)
            {
                btRet[0] = (byte)CommuniState.正常;
                btRet[1] = bRece[3];
            }
            else
            {
                if (iReceLen==0)
                {
                    btRet[0] = (byte)CommuniState.无回复;
                }
                else
                {
                    btRet[0] = (byte)CommuniState.错误;
                }
            }
            return btRet;
        }

        #endregion

        /// <summary>
        /// 功能码
        /// </summary>
        public enum RelayFn : byte
        {
            AskRelay = 0x01,
            AskIn = 0x02,
            OnOff = 0x05
        }
    }
}

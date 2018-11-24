using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass.SeralClass
{
    /// <summary>
    /// 新风系统
    /// </summary>
    public class clsSerialXF
    {
        Crc crc16 = new Crc();
        public SerialPortXF serial = null;
        clsCommon commonCls = null;

        #region  事件接口

        /// <summary>
        /// 新事件显示 事件
        /// </summary>
        public event NewEventShowEventHandler NewEventShowEvent;

        private byte[] bytGetState = new byte[8] { 0x01,0x02,0x00,0x01,0x00,0x01,0xFF,0xFF};
        private byte[] bytOperXF = new byte[8] {0x01,0x05,0x00,0x00,0xFF,0x00,0x8C,0x3A };

        ///// <summary>
        ///// 新告警显示 事件
        ///// </summary>
        //public event NewEventShowEventHandler NewAlarmShowEvent;

        #endregion

        public clsSerialXF()
        {
            serial= new SerialPortXF("COM4", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);
            commonCls = new clsCommon();
        }

        public bool OperDevice(int Addr, OnOffRelay bOnOff, string area, DeviceRunModel runModel, bool blRecord, IsWait isWait)
        {
            bool isok = false;
            try
            {
                bool blRet = false;
                bytOperXF[0] = (byte)Addr;
                bytOperXF[1] = 0x05;
                bytOperXF[2] = 0x00;
                bytOperXF[4] = (byte)bOnOff;
                bytOperXF[5] = 0x00;
                ushort crc = crc16.CalculateCrc16(bytOperXF, 6);
                bytOperXF[7] = (byte)(crc);
                bytOperXF[6] = (byte)(crc >> 8);

                byte[] bRece = new byte[8];
                int iReceLen = serial.SendReceByte(bytOperXF, ref bRece, 10, isWait);
                //txt 发送 接受
                txtLog.TxtWriteByte("XF.txt", "OnOff Send", bytOperXF, false);
                if (iReceLen > 0)
                {
                    txtLog.TxtWriteByte("XF.txt", "OnOff Rece", bRece, true);
                }
                if (iReceLen == 8 && bRece[0] == bytOperXF[0] && bRece[1] == bytOperXF[1] && bRece[2] == bytOperXF[2] && bRece[3] == bytOperXF[3] &&
                     bRece[4] == bytOperXF[4] && bRece[5] == bytOperXF[5] && bRece[6] == bytOperXF[6] && bRece[7] == bytOperXF[7])
                {
                    blRet = true;

                    if (blRecord)
                    {
                        EventType eventType = EventType.新风;
                        string strContent = bOnOff.ToString();
                        if (eventType != EventType.初值)
                        {
                            NewEventRecord(eventType, area, runModel, strContent);
                        }
                    }
                }
                return blRet;
            }
            catch(Exception ex)
            { }
            return isok;
        }

        public byte[] AskDeviceState(int Addr)
        {
            byte[] btRet = new byte[2];
            ushort crc = crc16.CalculateCrc16(bytGetState, 6);
            bytGetState[0] = (byte)Addr;
            bytGetState[7] = (byte)(crc);
            bytGetState[6] = (byte)(crc >> 8);
            byte[] bRece = new byte[6];
            int iReceLen = serial.SendReceByte(bytGetState, ref bRece, 10, IsWait.CanStop);
            //txt 发送 接受
            txtLog.TxtWriteByte("XF.txt", "AskIn Send", bytGetState, false);
            if (iReceLen > 0)
            {
                txtLog.TxtWriteByte("XF.txt", "AskIn Rece", bRece, true);
            }
            crc = crc16.CalculateCrc16(bRece, 4);
            byte bCrcL = (byte)(crc);
            byte bCrcH = (byte)(crc >> 8);
            if (iReceLen == 6)
            {
                if (bRece[0] == bytGetState[0] && bRece[1] == 0x01 && bRece[2] == 0x01 && bRece[4] == bCrcH && bRece[5] == bCrcL)
                {
                    btRet[0] = (byte)CommuniState.正常;
                    btRet[1] = bRece[3];
                }
            }
            else
            {
                if (iReceLen == 0)
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

        /// <summary>
        /// 环境事件 显示和存储
        /// </summary>
        public void NewEventRecord(EventType eventType, string area, DeviceRunModel runModel, string strContent)
        {
            commonCls.NewEnvirEventRecord(eventType, area, runModel, strContent);
            if (NewEventShowEvent != null)
            {
                NewEventShowEvent(new NewEventEventArgs(eventType, area, strContent, "", "", DateTime.Now));
            }
        }
    }
}

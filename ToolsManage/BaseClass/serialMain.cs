using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;

namespace ToolsManage.BaseClass
{
    class serialMain
    {
        Crc crc16 = new Crc();

        public SerialPortMain mainSerial;

        public byte[] SendWsd = new byte[5] { 0x23, 0xc3, 0x00, 0x01, 0xe7 };     //温度发送数据 0x01地址 0xe7校验和
        public byte[] SendRelay = new byte[8] { 0x01, 0x05, 0x00, 0x00, 0xFF, 0x00, 0x8C, 0x3A };

        private byte[] OtherAirSend = new byte[12] { 0x49, 0x4E, 0x37, 0x30, 0x31, 0x04, 0xFF, 0x08, 0x08, 0xFB, 0x00, 0x0D };// 49 4E 37 30 31 04 FF 08 08 FB 00 0D
        private byte[] OtherAirType = new byte[14] { 0x49, 0x4E, 0x37, 0x30, 0x31, 0x02, 0x00, 0x01, 0x08, 0x0B, 0x00, 0x00, 0x00, 0x0D };

        /// <summary>
        /// 查询继电器 即设备状态返回值
        /// </summary>
        public static byte bDeviceState = 0x00;

        public serialMain()
        {
            mainSerial = new SerialPortMain("COM2", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);
            //GprsSerialCom3.DataReceived +=new DataReceivedEventHandler(GprsSerialCom3_DataReceived);
            mainSerial.OpenPort();
        }

        /// <summary>
        /// 查询温湿度 值
        /// </summary>
        /// <param name="iAddr"></param>
        /// <returns></returns>
        public int[] AskWsd(int iAddr)
        {
            int[] Wsd = new int[2] { 25, 55 };
            //serialWsdYg.ReadExisting();
            SendWsd[4] = 0;
            SendWsd[3] = (byte)iAddr;
            for (int i = 0; i < 4; i++)
            {
                SendWsd[4] = (byte)(SendWsd[4] + SendWsd[i]);
            }
            //txt 发送
            string strContent = SerialPortUtil.ByteToStrHex(SendWsd);
            txtLog.WriteTxt("Wsd.txt", "Send", strContent, false);

            byte[] bRece = new byte[7];
            int iRetLen = mainSerial.SendReceByte(SendWsd, ref bRece, 300);
            //txt 接受
            if (iRetLen > 0)
            {
                strContent = SerialPortUtil.ByteToStrHex(bRece);
                txtLog.WriteTxt("Wsd.txt", "Rece", strContent, true);
            }

            byte ReceCheck = 0;
            for (byte bLen = 0; bLen < 6; bLen++)
            {
                ReceCheck = (byte)(ReceCheck + bRece[bLen]);
            }
            if (iRetLen == 7 && bRece[0] == 0x2A && bRece[1] == 0x44 &&
                bRece[2] == bRece[3] && bRece[4] == bRece[5] && bRece[6] == ReceCheck)//2A 44 B3 B3 C9 C9 66
            {
                //温度
                byte b = bRece[2];
                if ((b & 0x80) == 0x80)
                {
                    Wsd[0] = 0 - b & 0x7f;
                }
                else
                {
                    Wsd[0] = b;
                }
                //湿度
                Wsd[1] = bRece[4];
            }
            else//不等于 7位校验 正确
            {
                Wsd[0] = 25;
                Wsd[1] = 55;
            }
            return Wsd;
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
            strFireSend += strAddr;//地址
            strFireSend += "003R000\r\n";
            //txt 发送
            string strTxt = strFireSend.Substring(0,strFireSend .Length -1);
            txtLog.WriteTxt("Yw.txt", "Send", strTxt, false);
            string strRet = mainSerial.SendReceStr(strFireSend, 500);

            //txt 接受
            if (strRet.Length > 0)
            {
                txtLog.WriteTxt("Yw.txt", "Rece", strRet, false);
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
            return probe;
        }

        public bool OtherAirControl(byte bAddr,OtherAirControlType controlType, byte btContent)
        {
            bool blRet = false;
            if (bAddr >= 10)
            {
                Byte bHAddr = (byte)(bAddr % 10);
                OtherAirSend[3] = (byte)(bHAddr + 0x30);//地址高位
            }
            else
            {
                OtherAirSend[4] = (byte)(bAddr + 0x30);//地址低位
            }
            OtherAirSend[5] =(byte) controlType;//控制类型  
            OtherAirSend[6] = btContent;
            //校验
            OtherAirSend[9] = (byte)(OtherAirSend[5] ^ OtherAirSend[6] ^ OtherAirSend[7] ^ OtherAirSend[8]);

            //txt 发送
            string strContent = SerialPortUtil.ByteToStrHex(OtherAirSend);
            txtLog.WriteTxt("Air.txt", "OtherAir Send", strContent, false);

            byte[] bRece = new byte[12];
            int iRetLen = mainSerial.SendReceByte(OtherAirSend, ref bRece, 500);
            //txt 接受
            if (iRetLen > 0)
            {
                strContent = SerialPortUtil.ByteToStrHex(bRece);
                txtLog.WriteTxt("Air.txt", "OtherAir Rece", strContent, true);
            }

            if (iRetLen == 12)
            {
                byte ReceCheck = (byte)(bRece[5] ^ bRece[6] ^ bRece[7] ^ bRece[8]);
                if (bRece[0] == OtherAirSend[0] && bRece[1] == OtherAirSend[1] && bRece[2] == OtherAirSend[2] && bRece[3] == OtherAirSend[3] && bRece[4] == OtherAirSend[4] &&
                    bRece[5] == OtherAirSend[5] && bRece[6] == OtherAirSend[6] && bRece[7] == OtherAirSend[7] && bRece[8] == OtherAirSend[8] && bRece[9] == ReceCheck &&
                    bRece[10] == 0x01 && bRece[11] == OtherAirSend[11])
                {
                    blRet = true;
                }
            }
            return blRet;
        }

        /// <summary>
        /// 设置空调厂家
        /// </summary>
        /// <param name="bAddr"></param>
        /// <param name="bTemp"></param>
        public bool OtherAirSetType(byte bAddr, byte bFactory, int iCode)
        {
            bool blRet = false;
            if (bAddr >= 10)
            {
                Byte bHAddr = (byte)(bAddr % 10);
                OtherAirType[3] = (byte)(bHAddr + 0x30);//地址高位
            }
            else
            {
                OtherAirType[4] = (byte)(bAddr + 0x30);//地址低位
            }
            OtherAirType[5] = 0x02;//设置空调厂家   
            OtherAirType[7] = bFactory; ;
            OtherAirType[6] = 0x00;
            //校验
            OtherAirType[9] = (byte)(OtherAirType[5] ^ OtherAirType[6] ^ OtherAirType[7] ^ OtherAirType[8]);
            OtherAirType[11] = (byte)iCode;
            OtherAirType[10] = (byte)(iCode >> 8);

            //txt 发送
            string strContent = SerialPortUtil.ByteToStrHex(OtherAirType);
            txtLog.WriteTxt("Air.txt", "OtherAirCode Send", strContent, false);

            byte[] bRece = new byte[14];
            int iRetLen = mainSerial.SendReceByte(OtherAirType, ref bRece, 300);
            //txt 接受
            if (iRetLen > 0)
            {
                strContent = SerialPortUtil.ByteToStrHex(bRece);
                txtLog.WriteTxt("Air.txt", "OtherAirCode Rece", strContent, true);
            }

            if (iRetLen == 14)
            {
                byte ReceCheck = (byte)(bRece[5] ^ bRece[6] ^ bRece[7] ^ bRece[8]);
                if (bRece[0] == OtherAirType[0] && bRece[1] == OtherAirType[1] && bRece[2] == OtherAirType[2] && bRece[3] == OtherAirType[3] && bRece[4] == OtherAirType[4] &&
                    bRece[5] == 0x02 && bRece[9] == ReceCheck && bRece[12] == 0x01 && bRece[13] == 0x0D)
                {
                    blRet = true;
                }
            }
            return blRet;
        }






    }
}

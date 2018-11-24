using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;

namespace ToolsManage.BaseClass
{
    class serialDoor
    {
        public SerialPortDoor doorSerial;

        private byte[] bSendDoor = new byte[34] { 0x7E, 0x97, 0x2E, 0x81, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x56, 0x01, 0x0D };

        public serialDoor()
        {
            doorSerial = new SerialPortDoor("COM1", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);
            doorSerial.OpenPort();
        }

        public bool AddPower(string strIcNo)
        {
            bool blRet = false;
            #region  添加 权限 发送
            bSendDoor[1] = MainControl.bDoorAddrL;
            bSendDoor[2] = MainControl.bDoorAddrH;
            bSendDoor[3] = 0x07;
            bSendDoor[4] = 0x11;
            bSendDoor[5] = 0x00;
            bSendDoor[6] = 0x00;
            //IC卡号
            try
            {
                string strTemp = strIcNo.Substring(3, 5);
                int iTemp = Convert.ToInt32(strTemp);
                bSendDoor[7] = (byte)(iTemp & 0xff);
                bSendDoor[8] = (byte)((iTemp >> 8) & 0xff);
                strTemp = strIcNo.Substring(0, 3);
                bSendDoor[9] = Convert.ToByte(strTemp);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowTips(ex.Message);
            }
            //门号
            bSendDoor[10] = 0x01;
            //起始年月 2000年01月01
            bSendDoor[11] = 0x21;
            bSendDoor[12] = 0x00;
            //终止年月 2050年12月31
            bSendDoor[13] = 0x9F;
            bSendDoor[14] = 0x65;
            //时段
            bSendDoor[15] = 0x01;
            //密码  40 E2 01
            bSendDoor[16] = 0x40;
            bSendDoor[17] = 0xE2;
            bSendDoor[18] = 0x01;
            for (int i = 19; i < 31; i++)
            {
                bSendDoor[i] = 0;
            }
            int iCheckSun = 0;
            for (int i = 1; i < 31; i++)
            {
                iCheckSun += bSendDoor[i];
            }
            bSendDoor[31] = (byte)(iCheckSun & 0x00ff);
            bSendDoor[32] = (byte)(iCheckSun >> 8);

            //txt 发送
            string strContent = SerialPortUtil.ByteToHex(bSendDoor);
            txtLog.WriteTxt("Door.txt", "AddPower Send", strContent, false);

            byte[] bRece = new byte[34];
            int iLength = doorSerial.SendReceByte(bSendDoor, ref bRece, 500);

            //txt 接受
            if (iLength > 0)
            {
                strContent = SerialPortUtil.ByteToHex(bRece);
                txtLog.WriteTxt("Door.txt", "AddPower Rece", strContent, true);//01 01 01 0F 11 8C
            }

            #endregion

            #region  接受 判断

            if (iLength == 34)
            {
                iCheckSun = 0;
                for (int i = 1; i < 31; i++)
                {
                    iCheckSun += bRece[i];
                }
                byte bL = (byte)(iCheckSun & 0x00ff);
                byte bH = (byte)(iCheckSun >> 8);
                if (bRece[0] == 0x7E && bRece[1] == MainControl.bDoorAddrL && bRece[2] == MainControl.bDoorAddrH && bRece[3] == 0x07 && bRece[4] == 0x11 &&
                    bRece[31] == bL && bRece[32] == bH && bRece[33] == 0x0D)
                {
                    if (bRece[5] == 0x01)
                        blRet = true;
                    else
                        blRet = false;
                }
                else
                    blRet = false;
            }
            else
                blRet = false;
            #endregion

            return blRet;
        }

        public bool DeletePower(string strIcNo)
        {
            bool blRet = false;

            #region  添加 权限 发送
            bSendDoor[1] = MainControl.bDoorAddrL;
            bSendDoor[2] = MainControl.bDoorAddrH;
            bSendDoor[3] = 0x08;
            bSendDoor[4] = 0x11;
            bSendDoor[5] = 0x00;
            bSendDoor[6] = 0x00;

            //IC卡号
            try
            {
                string strTemp = strIcNo.Substring(3, 5);
                int iTemp = Convert.ToInt32(strTemp);
                bSendDoor[7] = (byte)(iTemp & 0xff);
                bSendDoor[8] = (byte)((iTemp >> 8) & 0xff);
                strTemp = strIcNo.Substring(0, 3);
                bSendDoor[9] = Convert.ToByte(strTemp);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowTips(ex.Message);
            }
            //门号
            bSendDoor[10] = 0x01;
            //起始年月 2000年01月01
            bSendDoor[11] = 0x21;
            bSendDoor[12] = 0x00;
            //终止年月 2050年12月31
            bSendDoor[13] = 0x9F;
            bSendDoor[14] = 0x65;
            //时段
            bSendDoor[15] = 0x01;
            //密码  40 E2 01
            bSendDoor[16] = 0x40;
            bSendDoor[17] = 0xE2;
            bSendDoor[18] = 0x01;
            for (int i = 19; i < 31; i++)
            {
                bSendDoor[i] = 0;
            }
            int iCheckSun = 0;
            for (int i = 1; i < 31; i++)
            {
                iCheckSun += bSendDoor[i];
            }
            bSendDoor[31] = (byte)(iCheckSun & 0x00ff);
            bSendDoor[32] = (byte)(iCheckSun >> 8);

            //txt 发送
            string strContent = SerialPortUtil.ByteToHex(bSendDoor);
            txtLog.WriteTxt("Door.txt", "DeletePower Send", strContent, false);

            byte[] bRece = new byte[34];
            int iLength = doorSerial.SendReceByte(bSendDoor, ref bRece, 500);

            //txt 接受
            if (iLength > 0)
            {
                strContent = SerialPortUtil.ByteToHex(bRece);
                txtLog.WriteTxt("Door.txt", "DeletePower Rece", strContent, true);//01 01 01 0F 11 8C
            }

            #endregion

            #region  接受 判断

            if (iLength == 34)
            {
                iCheckSun = 0;
                for (int i = 1; i < 31; i++)
                {
                    iCheckSun += bRece[i];
                }
                byte bL = (byte)(iCheckSun & 0x00ff);
                byte bH = (byte)(iCheckSun >> 8);
                if (bRece[0] == 0x7E && bRece[1] == MainControl.bDoorAddrL && bRece[2] == MainControl.bDoorAddrH && bRece[3] == 0x08 && bRece[4] == 0x11 &&
                    bRece[31] == bL && bRece[32] == bH && bRece[33] == 0x0D)
                    blRet = true;
                else
                    blRet = false;
            }
            else
                blRet = false;
            #endregion

            return blRet;
        }

        public void ReadRunState(ref int iCardIndex, ref int iCardCountLast,ref string sCardNo,ref DoorsState doorState,bool blAdd)
        {
            bSendDoor[1] = MainControl.bDoorAddrL;
            bSendDoor[2] = MainControl.bDoorAddrH;
            bSendDoor[3] = 0x81;
            bSendDoor[4] = 0x10;
            bSendDoor[5] = (byte)(iCardIndex & 0xff);
            bSendDoor[6] = (byte)((iCardIndex >> 8) & 0xff);
            bSendDoor[7] = (byte)((iCardIndex >> 16) & 0xff);
            bSendDoor[8] = (byte)((iCardIndex >> 24) & 0xff);
            for (int i = 9; i < 31; i++)
            {
                bSendDoor[i] = 0;
            }
            int iCheckSun = 0;
            for (int i = 1; i < 31; i++)
            {
                iCheckSun += bSendDoor[i];
            }
            bSendDoor[31] = (byte)(iCheckSun & 0x00ff);
            bSendDoor[32] = (byte)(iCheckSun >> 8);

            //txt 发送
            string strContent = SerialPortUtil.ByteToHex(bSendDoor);
            txtLog.WriteTxt("Door.txt", "State Send", strContent, false);

            byte[] bRece = new byte[34];
            int iLength = doorSerial.SendReceByte(bSendDoor, ref bRece, 500);

            //txt 接受
            if (iLength > 0)
            {
                strContent = SerialPortUtil.ByteToHex(bRece);
                txtLog.WriteTxt("Door.txt", "State Rece", strContent, true);//01 01 01 0F 11 8C
            }

            #region  接受 读运行状态

            if (iLength == 34)
            {
                iCheckSun = 0;
                for (int i = 1; i < 31; i++)
                {
                    iCheckSun += bRece[i];
                }
                byte bL = (byte)(iCheckSun & 0x00ff);
                byte bH = (byte)(iCheckSun >> 8);
                if (bRece[0] == 0x7E && bRece[1] == MainControl.bDoorAddrL && bRece[2] == MainControl.bDoorAddrH && bRece[3] == 0x81 && bRece[4] == 0x10 && 
                    bRece[31] == bL && bRece[32] == bH && bRece[33] == 0x0D)
                {
                    JudgeDatetime(bRece);
                    // 刷卡数
                    int iCardCount = bRece[12];
                    int i = bRece[13];
                    i *= 256;
                    iCardCount += i;
                    i = bRece[14];
                    i *= 65536;
                    iCardCount += i;
                    if (iCardIndex == 0 && iCardCountLast == 0)
                    {
                        //第一次运行
                        iCardCountLast = iCardCount;
                        iCardIndex = iCardCount;
                        iCardIndex++;
                    }
                    else
                    {
                        if (iCardCount > iCardCountLast)
                        {
                            //卡号
                            iCardCountLast = iCardCount;
                            iCardIndex = iCardCount;
                            iCardIndex++;
                            string str = bRece[19].ToString();
                            string strCardNo = IcCardFormat(3, str);
                            i = bRece[18];
                            i *= 256;
                            i += bRece[17];
                            str = i.ToString();
                            str = IcCardFormat(5, str);
                            strCardNo += str;
                            //记录状态
                            byte bRecordState = bRece[20];
                            if (blAdd)// blAdd  为添加用户时
                            {
                                sCardNo = strCardNo;
                            }
                            else
                            {
                                if (bRecordState == 0x00 || bRecordState == 0x01)//有效IC卡
                                {
                                    sCardNo = strCardNo;
                                }
                            }
                        }
                    }

                    //        //门磁状态
                    byte bMc = bRece[26];
                    if ((bMc & 0x10) == 0x00)
                    {
                        doorState = DoorsState.开门;
                    }
                    else
                    {
                        doorState = DoorsState.关门;
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 将Ic卡号转为8位
        /// </summary>
        /// <param name="iLen"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private string IcCardFormat(int iLen, string str)
        {
            int iTemp = iLen - str.Length;
            for (int i = 0; i < iTemp; i++)
            {
                str = "0" + str;
            }
            return str;
        }

        /// <summary>
        /// 判断时间 误差大于1分钟 校时
        /// </summary>
        /// <param name="ReceBuff"></param>
        private void JudgeDatetime(byte[] ReceBuff)
        {
            int iYear = ConvertBCDToInt(ReceBuff[5]);
            iYear += 2000;
            int iMonth = ConvertBCDToInt(ReceBuff[6]);
            int iDay = ConvertBCDToInt(ReceBuff[7]);
            int iHour = ConvertBCDToInt(ReceBuff[9]);
            int iMinute = ConvertBCDToInt(ReceBuff[10]);
            int iSecond = ConvertBCDToInt(ReceBuff[11]);
            DateTime dtRece = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
            TimeSpan ts = DateTime.Now - dtRece;//记录历史温湿度数据间隔
            if (ts.TotalSeconds >= 59 || ts.TotalSeconds <= -59)
            {
                DateTime dtNow = DateTime.Now;
                bSendDoor[1] = MainControl.bDoorAddrL;
                bSendDoor[2] = MainControl.bDoorAddrH; 
                bSendDoor[3] = 0x8B;
                bSendDoor[4] = 0x10;
                int i = dtNow.Year;
                if (i >= 2000)
                    i -= 2000;
                bSendDoor[5] = ConvertBCD((byte)i);
                bSendDoor[6] = ConvertBCD((byte)(dtNow.Month));
                bSendDoor[7] = ConvertBCD((byte)(dtNow.Day));
                byte b = ConverWeekToInt(dtNow.DayOfWeek.ToString());
                bSendDoor[8] = ConvertBCD(b);//周
                bSendDoor[9] = ConvertBCD((byte)(dtNow.Hour));
                bSendDoor[10] = ConvertBCD((byte)(dtNow.Minute));
                bSendDoor[11] = ConvertBCD((byte)(dtNow.Second));

                int iCheckSun = 0;
                for (i = 1; i < 31; i++)
                {
                    iCheckSun += bSendDoor[i];
                }
                bSendDoor[31] = (byte)(iCheckSun & 0x00ff);
                bSendDoor[32] = (byte)(iCheckSun >> 8);
          
                //txt 发送
                string strContent = SerialPortUtil.ByteToHex(bSendDoor);
                txtLog.WriteTxt("Door.txt", "Time Send", strContent, false);

                byte[] bRece = new byte[34];
                int iLength = doorSerial.SendReceByte(bSendDoor, ref bRece, 500);

                //txt 接受
                if (iLength > 0)
                {
                    strContent = SerialPortUtil.ByteToHex(bRece);
                    txtLog.WriteTxt("Door.txt", "Time Rece", strContent, true);//01 01 01 0F 11 8C
                }

                if (iLength == 34)
                {
                    iCheckSun = 0;
                    for (i = 1; i < 31; i++)
                    {
                        iCheckSun += ReceBuff[i];
                    }
                    byte bL = (byte)(iCheckSun & 0x00ff);
                    byte bH = (byte)(iCheckSun >> 8);
                    if (ReceBuff[0] == 0x7E && bRece[1] == MainControl.bDoorAddrL && bRece[2] == MainControl.bDoorAddrH && ReceBuff[3] == 0x8B && ReceBuff[4] == 0x10 && 
                        ReceBuff[31] == bL && ReceBuff[32] == bH && ReceBuff[33] == 0x0D)
                    {
                        if (frmMain.blDebug)
                        {
                            string str = "校时成功";
                            MessageUtil.ShowTips(str);
                        }
                    }
                }
            }
        }

        private int HexToInt(byte bHex)
        {
            byte b = bHex;
            int iReturn = b & 0x0f;
            int iH = b >> 4;
            iH *= 10;
            iReturn += iH;
            return iReturn;
        }

        #region BCD码转换

        public static byte ConvertBCD(byte b)
        {
            //高四位
            byte b1 = (byte)(b / 10);
            //低四位
            byte b2 = (byte)(b % 10);
            return (byte)((b1 << 4) | b2);
        }

        /// <summary>
        /// 将BCD一字节数据转换到byte 十进制数据
        /// </summary>
        /// <param name="b" />字节数
        /// <returns>返回转换后的BCD码</returns>
        public static byte ConvertBCDToByte(byte b)
        {
            //高四位
            byte b1 = (byte)((b >> 4) & 0xF);
            //低四位
            byte b2 = (byte)(b & 0xF);

            return (byte)(b1 * 10 + b2);
        }

        /// <summary>
        /// 将BCD一字节数据转换到byte 十进制数据
        /// </summary>
        /// <param name="b" />字节数
        /// <returns>返回转换后的BCD码</returns>
        public static byte ConvertBCDToInt(byte b)
        {
            //高四位
            int i1 = (byte)((b >> 4) & 0xF);
            //低四位
            int i2 = (byte)(b & 0xF);
            return (byte)(i1 * 10 + i2);
        }

        #endregion

        private byte ConverWeekToInt(string str)
        {
            byte bRet = 0;
            switch (str)
            {
                case "Monday":
                    bRet = 1;
                    break;
                case "Tuesday":
                    bRet = 2;
                    break;
                case "Wednesday":
                    bRet = 3;
                    break;
                case "Thursday":
                    bRet = 4;
                    break;
                case "Friday":
                    bRet = 5;
                    break;
                case "Saturday":
                    bRet = 6;
                    break;
                case "Sunday":
                    bRet = 0;
                    break;
            }
            return bRet;
        }

    }
}

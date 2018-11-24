using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Windows.Forms;
using System.Threading;
using System.Data;

using ToolsManage.BaseClass.DoorClass;

namespace ToolsManage.BaseClass.SeralClass
{
    public class clsSerialAir
    {
        //DataLogic datalogic = new DataLogic();
        clsCommon commonCls = new clsCommon();
        public SerialPortAir serial = new SerialPortAir("COM3", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);

        private byte[] OtherAirSend = new byte[12] { 0x49, 0x4E, 0x37, 0x30, 0x31, 0x04, 0xFF, 0x08, 0x08, 0xFB, 0x00, 0x0D };// 49 4E 37 30 31 04 FF 08 08 FB 00 0D
        private byte[] OtherAirType = new byte[14] { 0x49, 0x4E, 0x37, 0x30, 0x31, 0x02, 0x00, 0x01, 0x08, 0x0B, 0x00, 0x00, 0x00, 0x0D };
        /// <summary>
        /// 大金读运行状态 
        /// </summary>
        private byte[] DjAirState = new byte[6] { 0x7e, 0xFE, 0x01, 0x02, 0x01, 0x0D };//7E FE 01 02 01 0D
        /// <summary>
        /// 大金控制 开关和模式 
        /// </summary>
        private byte[] DjControl = new byte[8] { 0x7e, 0x01, 0x03, 0x04, 0x06, 0x00, 0x0e, 0x0D };//7E 01 03 04 06 00 0E 0D



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

        /// <summary>
        /// 空调控制 及SQL 存储
        /// </summary>
        public bool AirControl(AirFactoryType factory, byte btAddr, EventContent content, byte btSetTemp, string strArea, DeviceRunModel runMode, bool blMoreAir,IsWait isWait)
        {
            bool blRet = false;
            string strContent = content.ToString();

            #region  大金 空调控制

            if (factory == AirFactoryType.大金)
            {
                if (content == EventContent.开启 || content == EventContent.关闭)
                {
                    if (content == EventContent.开启)
                    {
                        blRet = DjAirControl(btAddr, DjControlType.开关, (byte)DjOnOff.开启, isWait);
                    }
                    else if (content == EventContent.关闭)
                    {
                        blRet = DjAirControl(btAddr, DjControlType.开关, (byte)DjOnOff.关闭, isWait);
                    }
                }
                else if (content == EventContent.设置制冷 || content == EventContent.设置制热 || content == EventContent.设置除湿)
                {
                    if (content == EventContent.设置制冷)
                    {
                        blRet = DjAirControl(btAddr, DjControlType.模式, (byte)DjModel.制冷, isWait);
                    }
                    else if (content == EventContent.设置制热)
                    {
                        blRet = DjAirControl(btAddr, DjControlType.模式, (byte)DjModel.制热, isWait);
                    }
                    else if (content == EventContent.设置除湿)
                    {

                    }
                }
                else if (content == EventContent.设置温度)
                {
                    blRet = DjAirControl(btAddr, DjControlType.温度, btSetTemp, isWait);
                    strContent = content.ToString() + btSetTemp.ToString();
                }
            }

            #endregion

            #region  其他 空调控制

            else if (factory == AirFactoryType.其他)
            {
                if (content == EventContent.开启 || content == EventContent.关闭)
                {
                    if (content == EventContent.开启)
                    {
                        blRet = OtherAirControl(btAddr, OtherAirControlType.开关, (byte)OtherAirOnOff.开启, isWait);
                    }
                    else if (content == EventContent.关闭)
                    {
                        blRet = OtherAirControl(btAddr, OtherAirControlType.开关, (byte)OtherAirOnOff.关闭, isWait);
                    }
                }
                else if (content == EventContent.设置制冷 || content == EventContent.设置制热 || content == EventContent.设置除湿)
                {
                    if (content == EventContent.设置制冷)
                    {
                        blRet = OtherAirControl(btAddr, OtherAirControlType.模式, (byte)OtherAirModelType.制冷, isWait);
                    }
                    else if (content == EventContent.设置制热)
                    {
                        blRet = OtherAirControl(btAddr, OtherAirControlType.模式, (byte)OtherAirModelType.制热, isWait);
                    }
                    else if (content == EventContent.设置除湿)
                    {

                    }
                }
                else if (content == EventContent.设置温度)
                {
                    blRet = OtherAirControl(btAddr, OtherAirControlType.温度, btSetTemp, isWait);
                    strContent = content.ToString() + btSetTemp.ToString();
                }
            }

            #endregion

            if (blRet)
            {
                commonCls.NewEnvirEventRecord(EventType.空调, strArea, runMode, strContent);

                if (NewEventShowEvent != null)
                {
                    string strPoint = strArea+" ";
                    if (blMoreAir)
                    {
                        strPoint += btAddr.ToString();
                        strPoint += "#空调";
                    }
                        
                    if (content == EventContent.设置温度)
                    {
                        NewEventShowEvent(new NewEventEventArgs(EventType.空调, strPoint, content.ToString() + btSetTemp.ToString(), "", "",DateTime .Now ));
                    }
                    else
                    {
                        NewEventShowEvent(new NewEventEventArgs(EventType.空调, strPoint, content.ToString(), "", "", DateTime.Now));
                    }
                }
            }

            return blRet;
        }

        #region  大金空调

        /// <summary>
        /// 读大金空调运行状态
        /// </summary>
        public CommuniState DjAirReadData(byte bAddr, ref DeviceRunState state, ref AirRunModel model, ref int iSetTemp)
        {
            CommuniState CommuniState = CommuniState.无回复;
            DjAirState[1] = bAddr;
            DjAirState[2] = 0x01;
            DjAirState[3] = 0x05;
            byte bCheck = 0;
            for (int i = 1; i < 4; i++)
            {
                bCheck += DjAirState[i];
            }
            DjAirState[4] = bCheck;
            byte[] bRece = new byte[22];
            int iReceLen = serial.SendReceByte(DjAirState, ref bRece, 10, IsWait.CanStop);
            //txt 发送 接受
            txtLog.TxtWriteByte("Air.txt", "DjState Send", DjAirState, false);
            if (iReceLen > 0)
            {
                txtLog.TxtWriteByte("Air.txt", "DjState Rece", bRece, true);
            }
            if (iReceLen == 22)
            {
                bCheck = 0;
                for (int i = 1; i < 20; i++)
                {
                    bCheck += bRece[i];
                }
                if (bRece[0] == 0x7e && bRece[1] == bAddr && bRece[2] == 0x11 && bRece[3] == 0x05 && bRece[20] == bCheck && bRece[21] == 0x0d)
                {
                    bool blPBoard = false;//P板和空调不通信
                    for (int i = 4; i < 20; i++)
                    {
                        if (bRece[i] != 0)
                        {
                            blPBoard = true;
                            break;
                        }
                    }
                    if (blPBoard)
                    {
                        CommuniState = CommuniState.正常;
                        iSetTemp = bRece[4];
                        if (bRece[10] == 0x00)
                            state = DeviceRunState.停止;
                        else if (bRece[10] == 0x01)
                            state = DeviceRunState.运行;
                        // 60H：送风、61H：制热、62H：制冷、63H：自动、67H：除湿
                        if (bRece[11] == 0x60)
                            model = AirRunModel.送风;
                        else if (bRece[11] == 0x61)
                            model = AirRunModel.制热;
                        else if (bRece[11] == 0x62)
                            model = AirRunModel.制冷;
                        else if (bRece[11] == 0x63)
                            model = AirRunModel.自动;
                        else if (bRece[11] == 0x67)
                            model = AirRunModel.除湿;
                    }
                    else
                    {
                        CommuniState = CommuniState.错误;
                    }
                }
            }
            return CommuniState;
        }

        /// <summary>
        /// 控制大金空调 开关或模式
        /// </summary>
        public bool DjAirControl(byte bAddr, DjControlType type, byte bContent,IsWait isWiat)
        {
            bool blRet = false;
            DjControl[1] = bAddr;
            DjControl[2] = 0x03;
            DjControl[3] = 0x04;
            DjControl[4] = (byte)type;
            DjControl[5] = bContent;

            byte bCheck = 0;
            for (int i = 1; i < 6; i++)
            {
                bCheck += DjControl[i];
            }
            DjControl[6] = bCheck;
            CommuniState ret = DkpDadaSendRece(isWiat);
            if (ret == CommuniState.错误)
            {
                int iSend = 3;
                for (int i = 0; i < iSend; i++)
                {
                    Thread.Sleep(2000);
                    ret = DkpDadaSendRece(isWiat);
                    if (ret == CommuniState.正常 )
                        break;
                }
            }
            if (ret == CommuniState.正常)
                blRet = true;
            return blRet;
        }

        /// <summary>
        /// DKP 空调控制器 收发数据 判断接受数据 根据情况 多次或 等待发送
        /// </summary>
        /// <returns></returns>
        private CommuniState DkpDadaSendRece(IsWait isWait)
        {
            CommuniState CommuniState = CommuniState.无回复;
            byte[] bRece = new byte[8];
            int iReceLen = serial.SendReceByte(DjControl, ref bRece, 10, isWait);
            //txt 发送 接受
            txtLog.TxtWriteByte("Air.txt", "DjControl Send", DjControl, false);
            if (iReceLen > 0)
            {
                txtLog.TxtWriteByte("Air.txt", "DjControl Rece", bRece, true);
            }
            if (iReceLen == 8)
            {
                if (bRece[0] == DjControl[0] && bRece[1] == DjControl[1] && bRece[2] == DjControl[2] && bRece[3] == DjControl[3] &&
                    bRece[4] == DjControl[4] && bRece[5] == DjControl[5] && bRece[6] == DjControl[6] && bRece[7] == DjControl[7])
                {
                    CommuniState = CommuniState.正常 ;
                }
                else
                {
                    CommuniState = CommuniState.错误;
                }
            }
            else
            {
                if (iReceLen == 0)
                    CommuniState = CommuniState.无回复;
                else
                    CommuniState = CommuniState.错误 ;
            }
            return CommuniState;
        }

        #endregion

        #region  其他空调

        public bool OtherAirControl(byte bAddr, OtherAirControlType controlType, byte btContent,IsWait isWait)
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
            OtherAirSend[5] = (byte)controlType;//控制类型  
            OtherAirSend[6] = btContent;
            //校验
            OtherAirSend[9] = (byte)(OtherAirSend[5] ^ OtherAirSend[6] ^ OtherAirSend[7] ^ OtherAirSend[8]);
            byte[] bRece = new byte[12];
            int iReceLen = serial.SendReceByte(OtherAirSend, ref bRece, 10, isWait);
            //txt 发送
            txtLog.TxtWriteByte("Air.txt", "OtherAir Send", OtherAirSend, false);
            if (bRece!=null && bRece.Length>=12)
            {
                int index = bRece.FindIndex(p => p == 0x49);
                if(index>=0)
                {
                    byte[] retdata = new byte[12];
                    Array.Copy(bRece, index, retdata, 0, 12);
                    // 接受
                    if (iReceLen > 0)
                    {
                        txtLog.TxtWriteByte("Air.txt", "OtherAir Rece", retdata, true);
                    }
                    if (retdata!=null && retdata.Length == 12)
                    {
                        byte ReceCheck = (byte)(retdata[5] ^ retdata[6] ^ retdata[7] ^ retdata[8]);
                        if (retdata[0] == OtherAirSend[0] && retdata[1] == OtherAirSend[1] && retdata[2] == OtherAirSend[2] && retdata[3] == OtherAirSend[3] && retdata[4] == OtherAirSend[4] &&
                            retdata[5] == OtherAirSend[5] && retdata[6] == OtherAirSend[6] && retdata[7] == OtherAirSend[7] && retdata[8] == OtherAirSend[8] && retdata[9] == ReceCheck &&
                            retdata[11] == OtherAirSend[11])//&&bRece[10] == 0x01
                        {
                            blRet = true;
                        }
                    }
                }                
            }            
            return blRet;
        }

        /// <summary>
        /// 设置空调厂家
        /// </summary>
        /// <param name="bAddr"></param>
        /// <param name="bTemp"></param>
        public bool OtherAirSetType(byte bAddr, byte bFactory, int iCode,IsWait isWait)
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
            byte[] bRece = new byte[14];
            int iReceLen = serial.SendReceByte(OtherAirType, ref bRece, 10, isWait);
            //txt 发送 接受
            txtLog.TxtWriteByte("Air.txt", "OtherAirCode Send", OtherAirType, false);
            if (iReceLen > 0)
            {
                txtLog.TxtWriteByte("Air.txt", "OtherAirCode Rece", bRece, true);
            }
            if (iReceLen == 14)
            {
                byte ReceCheck = (byte)(bRece[5] ^ bRece[6] ^ bRece[7] ^ bRece[8]);
                if (bRece[0] == OtherAirType[0] && bRece[1] == OtherAirType[1] && bRece[2] == OtherAirType[2] && bRece[3] == OtherAirType[3] && bRece[4] == OtherAirType[4] &&
                    bRece[5] == 0x02 && bRece[9] == ReceCheck && bRece[13] == 0x0D)//&& bRece[12] == 0x01
                {
                    blRet = true;
                }
            }
            return blRet;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Threading;

namespace ToolsManage.BaseClass.SeralClass
{
    /// <summary>
    /// 串口4 收发 语音及盘库
    /// </summary>
    public class clsSerialVoice
    {
        public SerialPortVoice serial = new SerialPortVoice("COM6", SerialPortBaudRates.BaudRate_9600, Parity.None, SerialPortDatabits.EightBits, StopBits.One);


        byte[] SendRelay = new byte[4] { 0x03, 0x01, 0xFF, 0xFF};//03 01 FF FF



        public bool OnOffMark(OnOffMark OnOff)//,IsWait isWait
        {
            bool blRet = false;

            SendRelay[0] = (byte)OnOff;


            byte[] bRece = new byte[2];
            int iReceLen = serial.SendReceByte(SendRelay, ref bRece, 10, IsWait.OnlyWait);
            //txt 发送 接受
            txtLog.TxtWriteByte("Voice.txt", "OnOff Send", SendRelay, false);
            if (iReceLen > 0)
            {
                txtLog.TxtWriteByte("Voice.txt", "OnOff Rece", bRece, true);
            }
            if (iReceLen == 2 && bRece[0] == (byte)OnOff && bRece[1] == 0x01 )
                blRet = true;
            return blRet;
        }

        public bool SendVoice(string Content)
        {
            bool blRt = false;

            int iLen = Content.Length;
            byte[] bContent = new byte[1];

            bContent = ConvertHelper.StringToBytes(Content);
            iLen = bContent.Length;
            int iSendLen = iLen + 1;
            byte[] bSend = new byte[iSendLen];
            bSend[0] = 0x23;

            for (int i = 0; i < iLen; i++)
            {
                bSend[i + 1] = bContent[i];
            }

            byte[] bRece = new byte[2];
            int iRetLen = serial.SendReceByte(bSend, ref bRece, 2,IsWait.OnlyWait);
            if (iRetLen == 2)
            {
                if (bRece[0] == 0x4f && bRece[1] == 0x4b)//4F 4B 
                {
                    blRt = true;
                }
            }


            //string strSend = "#" + Content;
            //string strRet = serial.SendReceStr(strSend, 5, IsWait.OnlyWait);
            //if (strRet == "OK")
            //    blRt = true;

            return blRt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net.NetworkInformation;
namespace ToolsManage.BaseClass.RFidClass.NetRfid
{   
    public class UHFReader28
    {
        public delegate void TagCallback(string strTag, string devicename);
        private ushort POLYNOMIAL = 0x8408;
        private ushort PRESET_VALUE = 0xffff;
        private byte[] RecvBuff = new byte[8000];
        private byte[] SendBuff = new byte[300];
        private int RecvLength = 0;
        private SerialPort serialPort1;
        private TcpClient client;
        private Stream streamToTran;
        private int inventoryScanTime = 0;
        public TagCallback ReceiveCallback;
        private int mType = -1;
        string ReaderName;
        public UHFReader28(string devicename)
        {
            serialPort1 = new SerialPort();
            ReaderName = devicename;
        }
        /// <summary>
        /// GetCRC
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="ADataLen"></param>
        #region
        private void GetCRC(byte[] pData, int ADataLen)
        {
            int i, j;
            ushort current_crc_value;
            current_crc_value = PRESET_VALUE;
            for (i = 0; i <= ADataLen - 1; i++)
            {
                current_crc_value = (ushort)(current_crc_value ^ (pData[i]));
                for (j = 0; j < 8; j++)
                {
                    if ((current_crc_value & 0x0001) != 0)
                    {
                        current_crc_value = (ushort)((current_crc_value >> 1) ^ POLYNOMIAL);
                    }
                    else
                    {
                        current_crc_value = (ushort)(current_crc_value >> 1);
                    }
                }
            }
            pData[i++] = (byte)(current_crc_value & 0x000000ff);
            pData[i] = (byte)((current_crc_value >> 8) & 0x000000ff);
        }

        #endregion
        /// <summary>
        /// CheckCRC
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        #region
        private bool CheckCRC(byte[] pData, int len)
        {
            GetCRC(pData, len);
            if ((pData[len + 1] == 0) && (pData[len] == 0))
                return true;
            else
                return false;
        }
        #endregion
        /// <summary>
        /// 16进制数组字符串转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        #region
        public byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        public string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }
        #endregion

        private bool isOnline(string ipAddr)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(ipAddr);
            if (pingReply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// OpenNet
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <param name="Port"></param>
        /// <returns></returns>
        #region
        private int OpenNet(string ipAddr, int Port)
        {
            try
            {
                if (!isOnline(ipAddr)) return 0x30;
                IPAddress ipAddress = IPAddress.Parse(ipAddr);
                client = new TcpClient();
                client.Connect(ipAddress, Port);
                streamToTran = client.GetStream();    // 获取连接至远程的
                streamToTran.ReadTimeout = 500;
                return 0;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return 0x30;
            }
        }
        #endregion



        /// <summary>
        /// OpenCom
        /// </summary>
        /// <param name="Port"></param>
        /// <param name="fbaud"></param>
        /// <returns></returns>
        #region
        private int OpenCom(int Port, int Speed)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            try
            {
                serialPort1.PortName = "com" + Port.ToString();
                serialPort1.BaudRate = Speed;
                serialPort1.ReadTimeout = 200;
                serialPort1.Open();
                return 0;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return 0x30;
            }
        }
        #endregion

        /// <summary>
        /// OpenByCom
        /// </summary>
        /// <param name="Port"></param>
        /// <param name="ComAddr"></param>
        /// <param name="Baud"></param>
        /// <returns></returns>
        #region
        public int OpenComPort(int Port, ref byte ComAddr, int Speed)
        {
            if (OpenCom(Port, Speed) == 0)
            {
                mType = 0;
                byte addr = ComAddr;
                byte[] Verion = new byte[2];
                byte Model = 0;
                byte SupProtocol = 0;
                byte dmaxfre = 0, dminfre = 0, power = 0, ScanTime = 0, Ant = 0, BeepEn = 0, OutputRep = 0, CheckAnt = 0;
                int result = 0x30;
                result = GetReaderInformation(ref addr, Verion, ref Model, ref SupProtocol, ref dmaxfre, ref dminfre, ref power, ref ScanTime);
                if (result == 0)
                {
                    ComAddr = addr;
                    return (0);
                }
                else
                {
                    serialPort1.Close();
                    return 0x30;
                }

            }
            else
            {
                return 0x30;
            }
        }

        #endregion


        /// <summary>
        /// OpenByCom
        /// </summary>
        /// <param name="Port"></param>
        /// <param name="ComAddr"></param>
        /// <param name="Baud"></param>
        /// <returns></returns>
        #region
        public int CloseComPort()
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
            return 0;
        }
        #endregion


        /// <summary>
        /// OpenByTcp
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <param name="Port"></param>
        /// <param name="ComAddr"></param>
        /// <returns></returns>
        #region
        public int OpenNetPort(string ipAddr, int Port, ref byte ComAddr)
        {
            if (OpenNet(ipAddr, Port) == 0)
            {
                mType = 1;
                byte addr = ComAddr;
                byte[] Verion = new byte[2];
                byte Model = 0;
                byte SupProtocol = 0;
                byte dmaxfre = 0, dminfre = 0, power = 0, ScanTime = 0, Ant = 0, BeepEn = 0, OutputRep = 0, CheckAnt = 0;
                int result = GetReaderInformation(ref addr, Verion, ref Model, ref SupProtocol, ref dmaxfre, ref dminfre, ref power, ref ScanTime);
                if (result == 0)
                {
                    ComAddr = addr;
                    return (0);
                }
                else
                {
                    if (streamToTran != null)
                        streamToTran.Dispose();
                    if (client != null)
                        client.Close();
                    return 0x30;
                }

            }
            else
            {
                return 0x30;
            }
        }
        #endregion

        /// <summary>
        /// CloseByTcp
        /// </summary>
        /// <returns></returns>
        #region
        public int CloseNetPort()
        {
            try
            {
                if (streamToTran != null)
                {
                    streamToTran.Dispose();
                    streamToTran = null;
                }
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
                return 0;
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                return 0x30;
            }
        }
        #endregion

        /// <summary>
        /// SendDataToPort
        /// </summary>
        /// <param name="dataToSend"></param>
        /// <param name="BytesOfSend"></param>
        /// <returns></returns>
        #region
        private int SendDataToPort(byte[] dataToSend, int BytesOfSend)
        {
            RecvLength = 0;
            Array.Clear(RecvBuff, 0, 8000);
            try
            {
                if(mType==0)
                {
                    serialPort1.DiscardInBuffer();//接受
                    serialPort1.DiscardOutBuffer();//发送
                    serialPort1.Write(dataToSend, 0, BytesOfSend);
                    return 0;
                }
                else
                {
                    lock (streamToTran)
                    {
                        streamToTran.Flush();
                        streamToTran.Write(dataToSend, 0, BytesOfSend);
                        return 0;
                    }
                }
            }
            catch
            {
                return 0x30;
            }
        }
        #endregion

        /// <summary>
        /// GetDataFromPort
        /// </summary>
        /// <param name="TYPE_RECV"></param>
        /// <returns></returns>
        #region
        private int GetDataFromPort(int cmd)
        {
            byte[] buffer = new byte[2560];
            int Count = 0;
            byte[] btArray = new byte[20000];
            int btLength = 0;
            long beginTime = System.Environment.TickCount;
            try
            {
                while ((System.Environment.TickCount - beginTime) < 2000)
                {
                    Count = 0;
                    Thread.Sleep(5);
                    if(mType==0)
                    {
                        int rlnum = serialPort1.BytesToRead;
                        Count = serialPort1.Read(buffer, 0, rlnum);
                    }
                    else
                    {
                        Count = streamToTran.Read(buffer, 0, buffer.Length);
                    }
                    if (Count > 0)
                    {
                        byte[] daw = new byte[Count + btLength];
                        Array.Copy(btArray, 0, daw, 0, btLength);
                        Array.Copy(buffer, 0, daw, btLength, Count);
                        int index = 0;
                        while ((daw.Length - index) > 4)
                        {
                            if ((daw[index] >= 4) && (daw[index + 2] == cmd))
                            {
                                int len = (daw[index] & 255);
                                if (daw.Length < (index + len + 1)) break;
                                byte[] epcArr = new byte[len + 1 + 2];
                                Array.Copy(daw, index, epcArr, 0, epcArr.Length - 2);
                                if (CheckCRC(epcArr, epcArr.Length - 2))
                                {
                                    Array.Copy(epcArr, 0, RecvBuff, 0, epcArr.Length - 2);
                                    RecvLength = epcArr.Length - 2;
                                    return 0;
                                }
                                else
                                {
                                    index++;
                                }
                            }
                            else
                            {
                                index++;
                            }
                        }
                        if (daw.Length > index)
                        {
                            btLength = daw.Length - index;
                            Array.Copy(daw, index, btArray, 0, btLength);
                        }
                        else
                        {
                            btLength = 0;
                        }
                    }
                }
            }
            catch (Exception e)
            { e.ToString(); }
            return 0x30;
        }
        #endregion
        /// <summary>
        /// /GetReaderInformation
        /// </summary>
        /// <param name="address"></param>
        /// <param name="VersionInfo"></param>
        /// <param name="ReaderType"></param>
        /// <param name="TrType"></param>
        /// <param name="dmaxfre"></param>
        /// <param name="dminfre"></param>
        /// <param name="powerdBm"></param>
        /// <param name="ScanTime"></param>
        /// <returns></returns>
        #region
        public int GetReaderInformation(ref byte address, byte[] VersionInfo, ref byte ReaderType, ref byte TrType, ref byte dmaxfre,
                                              ref byte dminfre, ref byte powerdBm, ref byte ScanTime)//最大询查时间 
        {
            int result = 0x30;
            SendBuff[0] = 0x04;
            SendBuff[1] = address;
            SendBuff[2] = 0x21;
            GetCRC(SendBuff, 3);
            SendDataToPort(SendBuff, 5);
            result = GetDataFromPort(0x21);
            if (result == 0)
            {
                if (RecvBuff[2] == 0x21)
                {
                    if (RecvBuff[3] == 0)
                    {
                        address = RecvBuff[1];
                        Array.Copy(RecvBuff, 4, VersionInfo, 0, 2);
                        ReaderType = RecvBuff[6];
                        TrType = RecvBuff[7];
                        dmaxfre = RecvBuff[8];
                        dminfre = RecvBuff[9];
                        powerdBm = RecvBuff[10];
                        ScanTime = RecvBuff[11];
                        inventoryScanTime = RecvBuff[11]*100;
                    }
                    return (RecvBuff[3]);
                }
                else
                {
                    return (0xEE);
                }
            }
            else
            {
                return (result);
            }
        }
        #endregion

       

        public int Writedfre(ref byte ConAddr,ref byte dmaxfre,ref byte dminfre)
        {
            int result = 0x30;
            SendBuff[0] = 0x06;
            SendBuff[1] = ConAddr;
            SendBuff[2] = 0x22;
            SendBuff[3] = dmaxfre;
            SendBuff[4] = dminfre;
            GetCRC(SendBuff, 5);
            SendDataToPort(SendBuff, 7);
            result = GetDataFromPort(0x22);
            if (result == 0)
            {
                return (RecvBuff[3]);
            }
            else
            {
                return (result);
            }
        }

        public int WriteComAdr(ref byte ConAddr, byte ComAdrData)
        {
            int result = 0x30;
            SendBuff[0] = 0x05;
            SendBuff[1] = ConAddr;
            SendBuff[2] = 0x24;
            SendBuff[3] = ComAdrData;
            GetCRC(SendBuff, 4);
            SendDataToPort(SendBuff, 6);
            result = GetDataFromPort(0x24);
            if (result == 0)
            {
                return (RecvBuff[3]);
            }
            else
            {
                return (result);
            }
        }
        public int WriteScanTime(ref byte ConAddr, byte ScanTime)
        {
            int result = 0x30;
            SendBuff[0] = 0x05;
            SendBuff[1] = ConAddr;
            SendBuff[2] = 0x25;
            SendBuff[3] = ScanTime;
            GetCRC(SendBuff, 4);
            SendDataToPort(SendBuff, 6);
            result = GetDataFromPort(0x25);
            if (result == 0)
            {
                return (RecvBuff[3]);
            }
            else
            {
                return (result);
            }
        }

        public int Writebaud(ref byte ConAddr, byte baud)
        {
            int result = 0x30;
            SendBuff[0] = 0x05;
            SendBuff[1] = ConAddr;
            SendBuff[2] = 0x28;
            SendBuff[3] = baud;
            GetCRC(SendBuff, 4);
            SendDataToPort(SendBuff, 6);
            result = GetDataFromPort(0x28);
            if (result == 0)
            {
                return (RecvBuff[3]);
            }
            else
            {
                return (result);
            }
        }

        public int SetPowerDbm(ref byte ConAddr, byte PowerDbm)
        {
            int result = 0x30;
            SendBuff[0] = 0x05;
            SendBuff[1] = ConAddr;
            SendBuff[2] = 0x2F;
            SendBuff[3] = PowerDbm;
            GetCRC(SendBuff, 4);
            SendDataToPort(SendBuff, 6);
            result = GetDataFromPort(0x2F);
            if (result == 0)
            {
                return (RecvBuff[3]);
            }
            else
            {
                return (result);
            }
        }
        
        private int GetInventoryData()
        {
            byte[] buffer = new byte[256];
            int Count = 0;
            byte[] daw = new byte[8000];
            int btLength = 0;
            int index = 0;
            long beginTime = System.Environment.TickCount;
            try
            {
                while ((System.Environment.TickCount - beginTime) < inventoryScanTime+1000)
                {
                    Count = 0;
                    Thread.Sleep(5);
                    if(mType==0)
                    {
                        int rlnum = serialPort1.BytesToRead;
                        Count = serialPort1.Read(buffer, 0, rlnum);
                    }
                    else
                    {
                        Count = streamToTran.Read(buffer, 0, buffer.Length);
                    }
                    if (Count > 0)
                    {
                        Array.Copy(buffer, 0, daw, btLength, Count);
                        btLength += Count;
                        while ((btLength - index) > 4)
                        {
                            if ((daw[index] >= 4) && (daw[index + 2] == 0x01))
                            {
                                int len = (daw[index] & 255);
                                if (btLength < (index + len + 1)) break;
                                byte[] epcArr = new byte[len + 1 + 2];
                                Array.Copy(daw, index, epcArr, 0, epcArr.Length - 2);
                                if (CheckCRC(epcArr, epcArr.Length - 2))
                                {
                                    Array.Copy(epcArr, 0, RecvBuff, RecvLength, epcArr.Length - 2);
                                    RecvLength += (epcArr.Length - 2);
                                    
                                    if((RecvBuff[index+3] == 0x03)||(RecvBuff[index+3] == 0x04)||(RecvBuff[index+3] == 0x01)||(RecvBuff[index+3] == 0x02))
                                    {//有标签
                                        int btlen = RecvBuff[index] - 7;
                                        int p = 0;
                                        while(btlen>p)
                                        {
                                            int epclen = RecvBuff[index + 6 + p];
                                            byte[]btarr =new byte[epclen];
                                            Array.Copy(RecvBuff, index + 7 + p, btarr, 0, epclen);
                                            p += (epclen + 1);
                                            string epchex = ByteArrayToHexString(btarr);
                                            if (ReceiveCallback != null)
                                            {
                                                ReceiveCallback(epchex,ReaderName);
                                            }
                                        }                                        
                                    }
                                    if (!(RecvBuff[index+3] == 0x03 || (RecvBuff[index+3]==0x04)))
                                    {
                                        return 0;
                                    }
                                    index += (epcArr.Length - 2);
                                }
                                else
                                {
                                    index++;
                                }
                            }
                            else
                            {
                                index++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            { e.ToString(); }
            return 0x30;
        }

        public int Inventory_G2(ref byte ConAddr,byte AdrTID, byte LenTID,byte TIDFlag)
        {
            int result = 0x30;
            if (TIDFlag==1)
            {
                SendBuff[0] = 0x06;
                SendBuff[1] = ConAddr;
                SendBuff[2] = 0x01;
                SendBuff[3] = AdrTID;
                SendBuff[4] = LenTID;
            }
            else
            {
                SendBuff[0] = 0x04;
                SendBuff[1] = ConAddr;
                SendBuff[2] = 0x01;
            }
            GetCRC(SendBuff, SendBuff[0]-1);
            SendDataToPort(SendBuff, SendBuff[0]+1);
            result = GetInventoryData();

            return result;
        }
    }
}

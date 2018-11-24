using Common;
using HikVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ToolsManage.VideoManage;

namespace HikVideo.Hikvision
{
    public class NvrConfig
    {
        public NvrConfig()
        { }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CHAN_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.U4)]
            public Int32[] lChannelNo;
            public void Init()
            {
                lChannelNo = new Int32[256];
                for (int i = 0; i < 256; i++)
                    lChannelNo[i] = -1;
            }
        }

        public CHCNetSDK.NET_DVR_DEVICECFG_V40 m_struDeviceCfg;
        public CHCNetSDK.NET_DVR_NETCFG_V30 m_struNetCfg;
        public CHCNetSDK.NET_DVR_TIME m_struTimeCfg;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V30 m_struDeviceInfo;
        public CHCNetSDK.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;

        public void GetDevCfg(int m_lUserID)
        {
            UInt32 dwReturn = 0;
            Int32 nSize = Marshal.SizeOf(m_struDeviceCfg);
            IntPtr ptrDeviceCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struDeviceCfg, ptrDeviceCfg, false);
            if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_DEVICECFG_V40, -1, ptrDeviceCfg, (UInt32)nSize, ref dwReturn))
            {
                uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                string strErr = "NET_DVR_GET_DEVICECFG_V40 failed, error code= " + iLastErr;
                //获取设备参数失败，输出错误号 Failed to get the basic parameters of device and output the error code
                MsgBox.ShowWarn(strErr);
            }
            else
            {
                m_struDeviceCfg = (CHCNetSDK.NET_DVR_DEVICECFG_V40)Marshal.PtrToStructure(ptrDeviceCfg, typeof(CHCNetSDK.NET_DVR_DEVICECFG_V40));
                //textBoxDevName.Text = System.Text.Encoding.GetEncoding("GBK").GetString(m_struDeviceCfg.sDVRName);
                //textBoxDevType.Text = System.Text.Encoding.UTF8.GetString(m_struDeviceCfg.byDevTypeName);
                //textBoxANum.Text = Convert.ToString(m_struDeviceCfg.byChanNum);
                //textBoxIPNum.Text = Convert.ToString(m_struDeviceCfg.byIPChanNum + 256 * m_struDeviceCfg.byHighIPChanNum);
                //textBoxZeroNum.Text = Convert.ToString(m_struDeviceCfg.byZeroChanNum);
                //textBoxNetNum.Text = Convert.ToString(m_struDeviceCfg.byNetworkPortNum);
                //textBoxAlarmInNum.Text = Convert.ToString(m_struDeviceCfg.byAlarmInPortNum);
                //textBoxAlarmOutNum.Text = Convert.ToString(m_struDeviceCfg.byAlarmOutPortNum);
                //textBoxDevSerial.Text = System.Text.Encoding.UTF8.GetString(m_struDeviceCfg.sSerialNumber);
                uint iVer1 = (m_struDeviceCfg.dwSoftwareVersion >> 24) & 0xFF;
                uint iVer2 = (m_struDeviceCfg.dwSoftwareVersion >> 16) & 0xFF;
                uint iVer3 = m_struDeviceCfg.dwSoftwareVersion & 0xFFFF;
                uint iVer4 = (m_struDeviceCfg.dwSoftwareBuildDate >> 16) & 0xFFFF;
                uint iVer5 = (m_struDeviceCfg.dwSoftwareBuildDate >> 8) & 0xFF;
                uint iVer6 = m_struDeviceCfg.dwSoftwareBuildDate & 0xFF;
                //textBoxDevVersion.Text = "V" + iVer1 + "." + iVer2 + "." + iVer3 + " Build " + string.Format("{0:D2}", iVer4) + string.Format("{0:D2}", iVer5) + string.Format("{0:D2}", iVer6);
            }
            Marshal.FreeHGlobal(ptrDeviceCfg);
        }

        public void GetNetCfg(int m_lUserID)
        {
            UInt32 dwReturn = 0;
            Int32 nSize = Marshal.SizeOf(m_struNetCfg);
            IntPtr ptrNetCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struNetCfg, ptrNetCfg, false);
            if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_NETCFG_V30, -1, ptrNetCfg, (UInt32)nSize, ref dwReturn))
            {
                uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                string strErr = "NET_DVR_GET_NETCFG_V30 failed, error code= " + iLastErr;
                //获取网络参数失败，输出错误号 Failed to get the network parameters and output the error code
                MsgBox.ShowWarn(strErr);
            }
            else
            {
                m_struNetCfg = (CHCNetSDK.NET_DVR_NETCFG_V30)Marshal.PtrToStructure(ptrNetCfg, typeof(CHCNetSDK.NET_DVR_NETCFG_V30));
                //textBoxIPAddr.Text = m_struNetCfg.struEtherNet[0].struDVRIP.sIpV4;
                //textBoxGateWay.Text = m_struNetCfg.struGatewayIpAddr.sIpV4;
                //textBoxSubMask.Text = m_struNetCfg.struEtherNet[0].struDVRIPMask.sIpV4;
                //textBoxDns.Text = m_struNetCfg.struDnsServer1IpAddr.sIpV4;
                //textBoxHostIP.Text = m_struNetCfg.struAlarmHostIpAddr.sIpV4;
                //textBoxHostPort.Text = Convert.ToString(m_struNetCfg.wAlarmHostIpPort);
                //textBoxHttpCfg.Text = Convert.ToString(m_struNetCfg.wHttpPortNo);
                //textBoxSdkCfg.Text = Convert.ToString(m_struNetCfg.struEtherNet[0].wDVRPort);
                if (m_struNetCfg.byUseDhcp == 0 && m_struNetCfg.struPPPoE.dwPPPOE == 0)
                {
                    //TextEnable(true);
                }
            }
            Marshal.FreeHGlobal(ptrNetCfg);
        }

        public void GetDvrSysTime(int m_lUserID)
        {
            UInt32 dwReturn = 0;
            Int32 nSize = Marshal.SizeOf(m_struTimeCfg);
            IntPtr ptrTimeCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(m_struTimeCfg, ptrTimeCfg, false);
            if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_TIMECFG, -1, ptrTimeCfg, (UInt32)nSize, ref dwReturn))
            {
                uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                string strErr = "NET_DVR_GET_TIMECFG failed, error code= " + iLastErr;
                //获取设备时间失败，输出错误号 Failed to get time of the device and output the error code
                MsgBox.ShowWarn(strErr);
            }
            else
            {
                m_struTimeCfg = (CHCNetSDK.NET_DVR_TIME)Marshal.PtrToStructure(ptrTimeCfg, typeof(CHCNetSDK.NET_DVR_TIME));
                //textBoxYear.Text = Convert.ToString(m_struTimeCfg.dwYear);
                //textBoxMonth.Text = Convert.ToString(m_struTimeCfg.dwMonth);
                //textBoxDay.Text = Convert.ToString(m_struTimeCfg.dwDay);
                //textBoxHour.Text = Convert.ToString(m_struTimeCfg.dwHour);
                //textBoxMinute.Text = Convert.ToString(m_struTimeCfg.dwMinute);
                //textBoxSecond.Text = Convert.ToString(m_struTimeCfg.dwSecond);
            }
            Marshal.FreeHGlobal(ptrTimeCfg);
        }

        public CHAN_INFO m_struChanNoInfo = new CHAN_INFO();
        public List<int> GetDevChanList(int m_lUserID, CHCNetSDK.NET_DVR_DEVICEINFO_V30 struDeviceInfo)
        {
            int i = 0, j = 0;
            string str;
            List<int> listchan = new List<int>();
            m_struChanNoInfo.Init();
            if (struDeviceInfo.byIPChanNum > 0)
            {
                uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);

                IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
                Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);

                uint dwReturn = 0;
                if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_IPPARACFG_V40, 0, ptrIpParaCfgV40, dwSize, ref dwReturn))
                {
                    uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    string strErr = "NET_DVR_GET_IPPARACFG_V40 failed, error code= " + iLastErr;
                    //获取IP通道信息失败，输出错误号 Failed to Get IP Channel info and output the error code
                    MsgBox.ShowWarn(strErr);
                }
                else
                {
                    m_struIpParaCfgV40 = (CHCNetSDK.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(CHCNetSDK.NET_DVR_IPPARACFG_V40));

                    //获取可用的模拟通道
                    for (i = 0; i < m_struIpParaCfgV40.dwAChanNum; i++)
                    {
                        if (m_struIpParaCfgV40.byAnalogChanEnable[i] == 1)
                        {//通道是否有模拟摄像头
                            str = String.Format("通道{0}", i + 1);
                            listchan.Add(i + 1);
                            //comboBoxChan.Items.Add(str);
                            m_struChanNoInfo.lChannelNo[j] = i + struDeviceInfo.byStartChan;
                            j++;
                        }
                    }

                    //获取在线的IP通道
                    byte byStreamType;
                    for (i = 0; i < m_struIpParaCfgV40.dwDChanNum; i++)
                    {
                        byStreamType = m_struIpParaCfgV40.struStreamMode[i].byGetStreamType;
                        CHCNetSDK.NET_DVR_STREAM_MODE m_struStreamMode = new CHCNetSDK.NET_DVR_STREAM_MODE();
                        dwSize = (uint)Marshal.SizeOf(m_struStreamMode);
                        switch (byStreamType)
                        {
                            //0- 直接从设备取流 0- get stream from device directly
                            case 0:
                                IntPtr ptrChanInfo = Marshal.AllocHGlobal((Int32)dwSize);
                                Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrChanInfo, false);
                                CHCNetSDK.NET_DVR_IPCHANINFO m_struChanInfo = new CHCNetSDK.NET_DVR_IPCHANINFO();
                                m_struChanInfo = (CHCNetSDK.NET_DVR_IPCHANINFO)Marshal.PtrToStructure(ptrChanInfo, typeof(CHCNetSDK.NET_DVR_IPCHANINFO));

                                //列出IP通道 List the IP channel
                                if (m_struChanInfo.byEnable == 1)
                                {////通道是否有IP摄像头
                                    str = String.Format("IP通道{0}", i + 1);
                                    listchan.Add(i + 1);
                                    //comboBoxChan.Items.Add(str);
                                    m_struChanNoInfo.lChannelNo[j] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                                    j++;
                                }
                                Marshal.FreeHGlobal(ptrChanInfo);
                                break;
                            //6- 直接从设备取流扩展 6- get stream from device directly(extended)
                            case 6:
                                IntPtr ptrChanInfoV40 = Marshal.AllocHGlobal((Int32)dwSize);
                                Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrChanInfoV40, false);
                                CHCNetSDK.NET_DVR_IPCHANINFO_V40 m_struChanInfoV40 = new CHCNetSDK.NET_DVR_IPCHANINFO_V40();
                                m_struChanInfoV40 = (CHCNetSDK.NET_DVR_IPCHANINFO_V40)Marshal.PtrToStructure(ptrChanInfoV40, typeof(CHCNetSDK.NET_DVR_IPCHANINFO_V40));

                                //列出IP通道 List the IP channel
                                if (m_struChanInfoV40.byEnable == 1)
                                {
                                    str = String.Format("IP通道{0}", i + 1);
                                    listchan.Add(i + 1);
                                    //comboBoxChan.Items.Add(str);
                                    m_struChanNoInfo.lChannelNo[j] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                                    j++;
                                }
                                Marshal.FreeHGlobal(ptrChanInfoV40);
                                break;
                            default:
                                break;
                        }
                    }
                }
                Marshal.FreeHGlobal(ptrIpParaCfgV40);
            }
            else
            {
                for (i = 0; i < struDeviceInfo.byChanNum; i++)
                {
                    str = String.Format("通道{0}", i + 1);
                    listchan.Add(i + 1);
                    //comboBoxChan.Items.Add(str);
                    m_struChanNoInfo.lChannelNo[j] = i + struDeviceInfo.byStartChan;
                    j++;
                }
            }
            return listchan;
        }

        
        /// <summary>
        /// 获取通道参数
        /// </summary>
        /// <param name="m_lUserID"></param>
        public Dictionary<int, string> GetChanParm(int m_lUserID, CHCNetSDK.NET_DVR_DEVICEINFO_V30 struDeviceInfo)
        {
            Dictionary<int, string> dicChanInfo = new Dictionary<int, string>();
            //List<int> listchan = GetDevChanList(m_lUserID, struDeviceInfo);
            //if (listchan != null && listchan.Count > 0)
            //{
            //    int index = 0;
            //    foreach (int channo in listchan)
            //    {
            //        string chanName = GetChanInfo(m_lUserID, m_struChanNoInfo.lChannelNo[index]);
            //        if (!String.IsNullOrEmpty(chanName) && !dicChanInfo.ContainsKey(m_struChanNoInfo.lChannelNo[index]))
            //            dicChanInfo.Add(m_struChanNoInfo.lChannelNo[index], chanName);
            //        index++;
            //    }
            //}
            return dicChanInfo;
        }

        public bool DoGetDeviceResoureCfg(int iDeviceIndex, int iGroupNO = 0)
        {
            return false;
        }
    }
}

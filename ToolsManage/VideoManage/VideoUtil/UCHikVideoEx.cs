using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HikVideo;
using System.Runtime.InteropServices;
//using TINYXMLTRANS;
using System.Xml;
using System.IO;
using ToolsManage.VideoManage;
using ToolsManage.Common;

namespace DAMSystem.UCControl.VideoUtil
{
    public enum EnumChannelType
    {
        /// <summary>
        /// 枪机
        /// </summary>
        QiangJiChannel,
        /// <summary>
        /// 球机
        /// </summary>
        QiuJiChannel,
        /// <summary>
        /// 鹰眼
        /// </summary>
        EagleChannel,
        /// <summary>
        /// 红外
        /// </summary>
        ThermalChannel,
        /// <summary>
        /// None
        /// </summary>
        None,
    }
    public partial class UCHikVideo : UCBaseVideo
    {
        //鹰眼
        private uint m_iChanShowNum = 0;
        private Int32 m_iDeviceIndex = 0;
        private Int32 m_iChanIndex = -1;
        private CHCNetSDK.NET_DVR_DEVICEINFO_V30 struDeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();        
        public CHCNetSDK.STRU_DEVICE_INFO[] g_struDeviceInfo = null;
        private Dictionary<int, EnumChannelType> dicChannelType = new Dictionary<int, EnumChannelType>();  //通道类型 是否鹰眼通道

        #region 获取设备信息
        private void InitDeviceInfo()
        {
            g_struDeviceInfo = new CHCNetSDK.STRU_DEVICE_INFO[CHCNetSDK.MAX_DEVICES];
            for (int i = 0; i < g_struDeviceInfo.Length; i++)
            {
                g_struDeviceInfo[i].Init();
                g_struDeviceInfo[i].pStruChanInfo = new CHCNetSDK.STRU_CHANNEL_INFO[CHCNetSDK.MAX_CHANNUM_V40];
                g_struDeviceInfo[i].struZeroChan = new CHCNetSDK.STRU_CHANNEL_INFO[16];
                g_struDeviceInfo[i].struMirrorChan = new CHCNetSDK.STRU_CHANNEL_INFO[16];
                for (int j = 0; j < CHCNetSDK.MAX_CHANNUM_V40; j++)
                {
                    g_struDeviceInfo[i].pStruChanInfo[j].init();
                }
                for (int j = 0; j < 16; j++)
                {
                    g_struDeviceInfo[i].struZeroChan[j].init();
                    g_struDeviceInfo[i].struMirrorChan[j].init();
                }
                g_struDeviceInfo[i].struPassiveDecode = new CHCNetSDK.PASSIVEDECODE_CHANINFO[256];
                for (int j = 0; j < 256; j++)
                {
                    g_struDeviceInfo[i].struPassiveDecode[j].init();
                }
            }
        }

        public bool DoGetDeviceResoureCfg(int iDeviceIndex, int iGroupNO = 0)
        {
            int i = 0, j = 0;
            uint dwReturned = 0;
            int dwSize = 0;
            int iLoopTime = 0;

            if (g_struDeviceInfo[iDeviceIndex].iIPChanNum > 0)
            {
                if (g_struDeviceInfo[iDeviceIndex].iIPChanNum % (int)(CHCNetSDK.MAX_CHANNUM_V30) == 0)
                {
                    iLoopTime = g_struDeviceInfo[iDeviceIndex].iIPChanNum / (int)(CHCNetSDK.MAX_CHANNUM_V30);
                }
                else
                {
                    iLoopTime = g_struDeviceInfo[iDeviceIndex].iIPChanNum / (int)(CHCNetSDK.MAX_CHANNUM_V30) + 1;
                }
            }

            for (j = 0; j < iLoopTime; j++)
            {
                CHCNetSDK.NET_DVR_IPPARACFG_V40 struIPAccessCfgV40 = new CHCNetSDK.NET_DVR_IPPARACFG_V40();
                iGroupNO = j;
                struIPAccessCfgV40 = g_struDeviceInfo[iDeviceIndex].pStruIPParaCfgV40[iGroupNO];
                dwSize = Marshal.SizeOf(struIPAccessCfgV40);
                IntPtr ptrIPAccessCfgV40 = Marshal.AllocHGlobal(dwSize);
                Marshal.StructureToPtr(struIPAccessCfgV40, ptrIPAccessCfgV40, false);

                //2008-9-15 13:44 ip input configuration
                g_struDeviceInfo[iDeviceIndex].bIPRet =
                    CHCNetSDK.NET_DVR_GetDVRConfig(g_struDeviceInfo[iDeviceIndex].lLoginID, CHCNetSDK.NET_DVR_GET_IPPARACFG_V40, iGroupNO, ptrIPAccessCfgV40, (uint)dwSize, ref dwReturned);
                if (!g_struDeviceInfo[iDeviceIndex].bIPRet)
                {	///device no support ip access
                    g_struDeviceInfo[iDeviceIndex].lFirstEnableChanIndex = 0;
                    i = j + iGroupNO * CHCNetSDK.MAX_CHANNUM_V30;
                    if (i < g_struDeviceInfo[iDeviceIndex].iAnalogChanNum)
                    {
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iDeviceIndex = iDeviceIndex;
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iChanIndex = i;
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iChannelNO = i + g_struDeviceInfo[iDeviceIndex].iStartChan;
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].bEnable = true;
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iChanType = CHCNetSDK.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_ANALOG;
                        g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName = string.Format("Camera{0}", i + g_struDeviceInfo[m_iDeviceIndex].iStartChan); ;
                    }
                    else//clear the state of other channel
                    {
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iDeviceIndex = -1;
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iChanIndex = -1;
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].bEnable = false;
                        g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName = "";
                    }
                    g_struDeviceInfo[iDeviceIndex].iGroupNO = -1;
                }
                else
                {
                    struIPAccessCfgV40 = (CHCNetSDK.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIPAccessCfgV40, typeof(CHCNetSDK.NET_DVR_IPPARACFG_V40));
                    //m_struOsdCfg=
                    g_struDeviceInfo[iDeviceIndex].iStartDChan = (int)struIPAccessCfgV40.dwStartDChan;
                    StartDChan = (byte)struIPAccessCfgV40.dwStartDChan;
                    g_struDeviceInfo[iDeviceIndex].pStruIPParaCfgV40[iGroupNO] = struIPAccessCfgV40;
                    g_struDeviceInfo[iDeviceIndex].iGroupNO = iGroupNO;
                    RefreshIPDevLocalCfg(iDeviceIndex);
                }
                Marshal.FreeHGlobal(ptrIPAccessCfgV40);
            }

            for (i = 0; i < g_struDeviceInfo[m_iDeviceIndex].iAnalogChanNum; i++)
            {
                g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].iDeviceIndex = m_iDeviceIndex;
                g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].iChanIndex = i;
                g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].iChannelNO = i + g_struDeviceInfo[m_iDeviceIndex].iStartChan;
                g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName = string.Format("Camera{0}", i + g_struDeviceInfo[m_iDeviceIndex].iStartChan);

                g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].bEnable = true;
                g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iChanType = CHCNetSDK.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_ANALOG;
                g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].dwImageType = CHCNetSDK.CHAN_ORIGINAL;

            }

            if ((g_struDeviceInfo[m_iDeviceIndex].byMirrorChanNum > 0) &&
                (g_struDeviceInfo[m_iDeviceIndex].wStartMirrorChanNo > (g_struDeviceInfo[m_iDeviceIndex].iDeviceChanNum - 1)))
            {
                for (i = 0; i < g_struDeviceInfo[m_iDeviceIndex].byMirrorChanNum && i < 16; i++)
                {
                    g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[i].iDeviceIndex = m_iDeviceIndex;
                    g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[i].iChanIndex = i + CHCNetSDK.MIRROR_CHAN_INDEX;
                    g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[i].iChannelNO = i + g_struDeviceInfo[m_iDeviceIndex].wStartMirrorChanNo;
                    g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[i].chChanName = string.Format("MirrorChan{0}", i + 1);

                    //analog devices
                    g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[i].bEnable = true;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iChanType = CHCNetSDK.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_IP;
                    g_struDeviceInfo[m_iDeviceIndex].struMirrorChan[i].dwImageType = CHCNetSDK.CHAN_ORIGINAL;
                }
            }
            if (g_struDeviceInfo[m_iDeviceIndex].byZeroChanNum > 0)
            {
                for (i = 0; i < g_struDeviceInfo[m_iDeviceIndex].byZeroChanNum; i++)
                {
                    g_struDeviceInfo[m_iDeviceIndex].struZeroChan[i].iDeviceIndex = m_iDeviceIndex;
                    g_struDeviceInfo[m_iDeviceIndex].struZeroChan[i].iChanIndex = i + CHCNetSDK.ZERO_CHAN_INDEX;
                    g_struDeviceInfo[m_iDeviceIndex].struZeroChan[i].chChanName = string.Format("ZeroChan{0}", i);
                    g_struDeviceInfo[m_iDeviceIndex].struZeroChan[i].bEnable = true;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i].iChanType = CHCNetSDK.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_MIRROR;
                    g_struDeviceInfo[m_iDeviceIndex].struZeroChan[i].dwImageType = CHCNetSDK.CHAN_ORIGINAL;

                }
            }
            return g_struDeviceInfo[iDeviceIndex].bIPRet;
        }

        public void RefreshIPDevLocalCfg(int iDeviceIndex)
        {
            CHCNetSDK.NET_DVR_IPPARACFG_V40 struIPAccessCfgV40 = g_struDeviceInfo[iDeviceIndex].pStruIPParaCfgV40[g_struDeviceInfo[iDeviceIndex].iGroupNO];
            uint dwChanShow = 0;
            int iIPChanIndex = 0;
            int i = 0;
            g_struDeviceInfo[iDeviceIndex].iIPChanNum = (int)struIPAccessCfgV40.dwDChanNum;

            int iAnalogChanCount = 0;
            int iIPChanCount = 0;
            int iGroupNO = g_struDeviceInfo[iDeviceIndex].iGroupNO;    //Group NO.
            int iGroupNum = (int)struIPAccessCfgV40.dwGroupNum;
            int iIPChanNum = g_struDeviceInfo[iDeviceIndex].iIPChanNum;

            for (i = 0; i < CHCNetSDK.MAX_CHANNUM_V30; i++)
            {
                //analog channel
                if (iAnalogChanCount < g_struDeviceInfo[iDeviceIndex].iAnalogChanNum)
                {
                    dwChanShow = (uint)(iAnalogChanCount + g_struDeviceInfo[iDeviceIndex].iStartChan + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64);

                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iDeviceIndex = iDeviceIndex;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChanIndex = i;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChanType = CHCNetSDK.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_ANALOG;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChannelNO = (int)dwChanShow;

                    g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName =
                        string.Format("Camera{0}", i + g_struDeviceInfo[m_iDeviceIndex].iStartChan - g_struDeviceInfo[m_iDeviceIndex].iAnalogChanNum);
                    //analog devices
                    if (struIPAccessCfgV40.byAnalogChanEnable[i] > 0)
                    {
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].bEnable = true;
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].dwImageType = CHCNetSDK.CHAN_ORIGINAL;
                        //g_struDeviceInfo[iDeviceIndex].iEnableChanNum ++;
                    }
                    else
                    {
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].bEnable = false;
                        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].dwImageType = CHCNetSDK.CHAN_OFF_LINE;
                    }

                    iAnalogChanCount++;
                }
                else if (iGroupNO >= 0 && ((iIPChanCount + iGroupNO * CHCNetSDK.MAX_CHANNUM_V30) < iIPChanNum) && (iIPChanCount < iIPChanNum))
                {
                    byte enabeled = struIPAccessCfgV40.struIPDevInfo[i].byEnable;
                    
                    dwChanShow = (uint)(iIPChanCount + iGroupNO * CHCNetSDK.MAX_CHANNUM_V30 + struIPAccessCfgV40.dwStartDChan);
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChanType = CHCNetSDK.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_IP;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChannelNO = (int)dwChanShow;
                    //if (i == 0)
                    //    StartDChan = (byte)dwChanShow;
                    iIPChanIndex = iIPChanCount;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iDeviceIndex = iDeviceIndex;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChanIndex = i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64;
                    //g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName = 
                    //    string.Format("IPCamera{0}", iIPChanCount + iGroupNO * CHCNetSDK.MAX_CHANNUM_V30 + 1);
                    CHCNetSDK.NET_DVR_PICCFG_V30 m_struOsdCfg = getOSDConfig((int)dwChanShow);
                    //通道名称
                    if (enabeled == 1)
                    {//在线
                        if (!String.IsNullOrEmpty(m_struOsdCfg.sChanName))
                        g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName = getOSDConfig((int)dwChanShow).sChanName;
                        g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].bEnable = true;
                    }
                    else
                    {//离线 
                        g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName =
                            string.Format("IPCamera{0}", iIPChanCount + iGroupNO * CHCNetSDK.MAX_CHANNUM_V30 + 1);
                        g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].bEnable = false;
                    }
                    //if (struIPAccessCfgV40.struStreamMode[iIPChanIndex].uGetStream.struChanInfo.byEnable != 0)  //struIPAccessCfgV40.struStreamMode[iIPChanIndex].uGetStream.struChanInfo.byIPID != 0 && 
                    //{
                    //    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].bEnable = true;//
                    //    if (struIPAccessCfgV40.struStreamMode[iIPChanIndex].uGetStream.struChanInfo.byEnable > 0)
                    //    {
                    //        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].dwImageType = CHCNetSDK.CHAN_ORIGINAL;
                    //    }
                    //    else
                    //    {
                    //        g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].dwImageType = CHCNetSDK.CHAN_OFF_LINE;
                    //    }
                    //    //				g_struDeviceInfo[iDeviceIndex].iEnableChanNum ++;
                    //}
                    //else
                    //{
                    //    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].dwImageType = CHCNetSDK.CHAN_OFF_LINE;
                    //    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].bEnable = false;
                    //    //g_struDeviceInfo[iDeviceIndex].struChanInfo[i].bAlarm = FALSE;
                    //}  
                    iIPChanCount++;
                }
                else
                {
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iDeviceIndex = -1;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChanIndex = -1;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChanType = CHCNetSDK.DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_INVALID;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].iChannelNO = -1;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].bEnable = false;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].bAlarm = false;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].bLocalManualRec = false;
                    g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].lRealHandle = -1;
                    g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName = "";
                }
            }

            for (i = 0; i < CHCNetSDK.MAX_CHANNUM_V40; i++)
            {
                if (g_struDeviceInfo[iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[iDeviceIndex].iGroupNO * 64].bEnable)
                {
                    g_struDeviceInfo[iDeviceIndex].lFirstEnableChanIndex = i;
                    break;
                }
            }
        }

        private CHCNetSDK.NET_DVR_PICCFG_V30 getOSDConfig(int m_lChannel)
        {
            uint dwReturned = 0;
            CHCNetSDK.NET_DVR_PICCFG_V30 m_struOsdCfg = new CHCNetSDK.NET_DVR_PICCFG_V30();
            uint dwSize = (uint)Marshal.SizeOf(m_struOsdCfg);
            IntPtr ptrOsdCfg = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struOsdCfg, ptrOsdCfg, false);
            // configure OSD parameters 
            if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_PICCFG_V30, m_lChannel, ptrOsdCfg, dwSize, ref dwReturned))
            {
                uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                string str = "NET_DVR_GET_PICCFG_V30, error code= " + iLastErr;
                Marshal.FreeHGlobal(ptrOsdCfg);
            }
            else
            {
                m_struOsdCfg = (CHCNetSDK.NET_DVR_PICCFG_V30)Marshal.PtrToStructure(ptrOsdCfg, typeof(CHCNetSDK.NET_DVR_PICCFG_V30));
                Marshal.FreeHGlobal(ptrOsdCfg);
                //m_bChkDisplayName = m_struOsdCfg.dwShowChanName;
                //m_bChkDisplayTime = m_struOsdCfg.dwShowOsd;
                //textBoxDisplayName.Text = m_struOsdCfg.sChanName;
                //comboBoxDateFormat.SelectedIndex = (int)m_struOsdCfg.byOSDType;
                //comboBoxTimeFormat.SelectedIndex = (int)m_struOsdCfg.byHourOSDType;
                //comboBoxOSDAttribute.SelectedIndex = (int)(m_struOsdCfg.byOSDAttrib - 1);
            }
            return m_struOsdCfg;
        }

        #endregion


        #region 获取通道能力集
        private void GetEagleChannelAblity()//判断通道码流是否是鹰眼码流  对应的接口命令是 NET_DVR_GET_SINGLE_CHANNELINFO
        {
            const uint XML_ABILITY_OUT_LEN = 4 * 1024;
            IntPtr ptrCoundBuffer;

            string pOutBuf = "\0";
            uint dwcondSize = 0;
            //[out]  
            dwcondSize = sizeof(int);
            //CTinyXmlTrans XMLBASE = new CTinyXmlTrans();
            uint dwXmlSize = XML_ABILITY_OUT_LEN;
            if (g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo != null && g_struDeviceInfo[m_iDeviceIndex].iDeviceChanNum > 0)
            {
                for (int i = 0; i < g_struDeviceInfo[m_iDeviceIndex].iDeviceChanNum; i++)
                {
                    if (i >= Configurations.CameraCount)
                        break;
                    if (g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[m_iDeviceIndex].iGroupNO * 64].iChannelNO > 0)
                    {//存在的通道号
                        IntPtr m_pXmlOutBuf;
                        m_pXmlOutBuf = Marshal.AllocHGlobal((Int32)XML_ABILITY_OUT_LEN);
                        ptrCoundBuffer = Marshal.AllocHGlobal((int)dwcondSize);
                        int m_lChannel = g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i + g_struDeviceInfo[m_iDeviceIndex].iGroupNO * 64].iChannelNO;
                        Marshal.WriteInt32(ptrCoundBuffer, m_lChannel);
                        CHCNetSDK.NET_DVR_STD_CONFIG struCfg = new CHCNetSDK.NET_DVR_STD_CONFIG();
                        struCfg.lpCondBuffer = ptrCoundBuffer;
                        struCfg.dwCondSize = dwcondSize;
                        struCfg.byDataType = 1;
                        struCfg.lpXmlBuffer = m_pXmlOutBuf;
                        struCfg.dwXmlSize = dwXmlSize;
                        int dwSTDSize = Marshal.SizeOf(struCfg);
                        IntPtr ptrSTDCfg = Marshal.AllocHGlobal(dwSTDSize);
                        Marshal.StructureToPtr(struCfg, ptrSTDCfg, false);
                        bool m_dwReturnValue = CHCNetSDK.NET_DVR_GetSTDConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_SINGLE_CHANNELINFO, ptrSTDCfg);
                        Marshal.FreeHGlobal(ptrCoundBuffer);
                        if (m_dwReturnValue)
                        {
                            int lentemp = (int)dwXmlSize;
                            pOutBuf = Marshal.PtrToStringAnsi(m_pXmlOutBuf, lentemp);

                            //XmlDocument xd = new XmlDocument();

                            //xd.LoadXml(pOutBuf);
                            ////使用xpath表达式选择文档中所有的student子节点
                            //XmlNodeList studentNodeList = xd.SelectNodes("ChannelInfo");
                            using (XmlReader xmlReader = XmlTextReader.Create(new StringReader(pOutBuf)))
                            {
                                try
                                {
                                    string parentnode = "";
                                    while (xmlReader.Read())
                                    {
                                        switch (xmlReader.NodeType)
                                        {
                                            case XmlNodeType.Element:
                                                if (xmlReader.Name == "ChannelInfo")
                                                {
                                                    parentnode = "ChannelInfo";
                                                    if (xmlReader.HasAttributes)
                                                    {
                                                        //if (xmlReader.MoveToAttribute("EagleEye"))
                                                        //{
                                                        //}
                                                        //if (xmlReader.MoveToAttribute("PanoramicMetaData"))
                                                        //{
                                                        //}
                                                        //if (xmlReader.MoveToAttribute("InsertChanNo"))
                                                        //{
                                                        //}
                                                    }
                                                }
                                                if (xmlReader.Name == "EagleEye")
                                                {
                                                    parentnode = "EagleEye";
                                                }
                                                if (xmlReader.Name == "PanoramicMetaData")
                                                {
                                                    parentnode = "PanoramicMetaData";
                                                }
                                                if (xmlReader.Name == "InsertChanNo" &&  parentnode == "PanoramicMetaData")
                                                {
                                                    parentnode = "InsertChanNo";
                                                    dicChannelType.Add(m_lChannel, EnumChannelType.EagleChannel);
                                                }
                                                break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }


                            //XMLBASE.Parse(pOutBuf);
                            //XMLBASE.SetRoot();
                            //string XMLStrTemp = XMLBASE.GetChildrenText();
                            //XMLStrTemp.Replace("\n", "\r\n");
                            //if (XMLBASE.FindElem("ChannelInfo") && XMLBASE.IntoElem())
                            //{
                            //    if (XMLBASE.FindElem("EagleEye") && XMLBASE.IntoElem())
                            //    {
                            //        if (XMLBASE.FindElem("PanoramicMetaData") && XMLBASE.IntoElem())
                            //        {
                            //            if (XMLBASE.FindElem("InsertChanNo"))
                            //            {
                            //                //string strChannel;
                            //                //strChannel = XMLBASE.GetAttributeValue("InsertChanNo");
                            //                dicChannelType.Add(i, EnumChannelType.EagleChannel);
                            //                //m_EagleEyeChannelStream = true;
                            //            }
                            //        }
                            //    }
                            //}
                        }
                        else
                        {
                            //m_dwLastError = CHCNetSDK.NET_DVR_GetLastError();
                        }
                        if (!dicChannelType.ContainsKey(m_lChannel))
                            dicChannelType.Add(m_lChannel, EnumChannelType.QiangJiChannel);
                        Marshal.FreeHGlobal(ptrSTDCfg);
                        Marshal.FreeHGlobal(m_pXmlOutBuf);
                    }
                }
            }
        }

        /// <summary>
        /// 检测是否鹰眼通道流
        /// </summary>
        /// <returns></returns>
        private bool CheckEagleEyeStream(int channel)//判断通道码流是否是鹰眼码流  组合判断设备类型（小鹰眼设备的设备类型是47，登录返回）和通道码流能力，这样能够兼容早期的鹰眼样机版本（不支持通道属性返回）
        {
            if (dicChannelType.ContainsKey(channel))
            {
                if ((g_struDeviceInfo[m_iDeviceIndex].iDeviceType == 47) || dicChannelType[channel]== EnumChannelType.EagleChannel)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 获取通道信息
        /// </summary>
        private void GetChannelInfo()
        {
            if (g_struDeviceInfo != null && g_struDeviceInfo.Length > 0)
            {
                listChannelInfo = new List<NVRChannelInfo>();
                for (int i = 0; i < g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo.Length; i++)
                {
                    if (g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].bEnable == true)
                    {
                        NVRChannelInfo nci = new NVRChannelInfo()
                        {
                            ChannelNo = g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].iChannelNO,
                            ChannelName = g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName,
                            IsEnabled = g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].bEnable,
                        };                       
                        listChannelInfo.Add(nci);
                    }
                }
                //for (int i = 0; i < g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo.Length; i++)
                //{
                //    if (g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].iChannelNO > 0)
                //    {
                //        if (CameraCount < i + 1)
                //            break;
                //        NVRChannelInfo nci = new NVRChannelInfo()
                //        {
                //            ChannelNo = g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].iChannelNO,
                //            ChannelName = g_struDeviceInfo[m_iDeviceIndex].pStruChanInfo[i].chChanName,
                //        };
                //        listChannelInfo.Add(nci);
                //    }
                //}
            }
        }

        /// <summary>
        /// 绑定通道信息到Combobox
        /// </summary>
        public  override void BindChannelInfo()
        {
            if (listChannelInfo != null && listChannelInfo.Count > 0)
            {

            }
        }
    }
}

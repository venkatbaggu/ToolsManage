using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikVideo
{
    /// <summary>
    /// NVR通道信息
    /// </summary>
    public class NVRChannelInfo
    {
        private int m_ChannelNo;

        public int ChannelNo
        {
            get { return m_ChannelNo; }
            set { m_ChannelNo = value; }
        }

        private string m_ChannelName;

        public string ChannelName
        {
            get { return m_ChannelName; }
            set { m_ChannelName = value; }
        }

        private string m_ChannelType;

        public string ChannelType
        {
            get { return m_ChannelType; }
            set { m_ChannelType = value; }
        }

        private bool m_IsEnabled;

        public bool IsEnabled
        {
            get { return m_IsEnabled; }
            set { m_IsEnabled = value; }
        }
    }
}

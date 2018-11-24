using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikVideo.HikVision
{
    public class PlayBackFileInfo
    {
        private DateTime m_dtStart;

        public DateTime DtStart
        {
            get { return m_dtStart; }
            set { m_dtStart = value; }
        }

        private DateTime m_dtEnd;

        public DateTime DtEnd
        {
            get { return m_dtEnd; }
            set { m_dtEnd = value; }
        }

        private int m_FileSize;

        public int FileSize
        {
            get { return m_FileSize; }
            set { m_FileSize = value; }
        }
    }
}

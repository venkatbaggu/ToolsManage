using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolsManage.BaseClass.EnvirClass;

namespace ToolsManage.BaseClass
{
    public class RoomClass
    {
        public List<clsWsd> listWsd = new List<clsWsd>();
        public List<clsFire> listFire = new List<clsFire>();
        public List<clsAir> listAir = new List<clsAir>();

        public clsRelay roomRelay;
        //public clsAir roomAir;
        public clsHot roomHot = new clsHot();
        public clsDehumi roomDehumi = new clsDehumi();
        public clsFan roomFan = new clsFan();
        /// <summary>
        /// 房间的烟感状态
        /// </summary>
        public ProbeState FireState = ProbeState.正常;
        private EventContent stateOfAlarmLed = EventContent.关闭;

        public EventContent StateOfAlarmLed
        {
            get { return stateOfAlarmLed; }
            set { stateOfAlarmLed = value; }
        }

        private string strName;
        public string StrName
        {
            get { return strName; }
            set { strName = value; }
        }

        public int YGCount
        {
            get
            {
                return m_YGCount;
            }

            set
            {
                m_YGCount = value;
            }
        }

        public int WSDCount
        {
            get
            {
                return m_WSDCount;
            }

            set
            {
                m_WSDCount = value;
            }
        }

        public int KTCount
        {
            get
            {
                return m_KTCount;
            }

            set
            {
                m_KTCount = value;
            }
        }

        public bool IsExistCSJ
        {
            get
            {
                return m_IsExistCSJ;
            }

            set
            {
                m_IsExistCSJ = value;
            }
        }

        public bool IsExistJRQ
        {
            get
            {
                return m_IsExistJRQ;
            }

            set
            {
                m_IsExistJRQ = value;
            }
        }

        public bool IsExistJDQ
        {
            get
            {
                return m_IsExistJDQ;
            }

            set
            {
                m_IsExistJDQ = value;
            }
        }

        public bool IsExistXF
        {
            get
            {
                return m_IsExistXF;
            }

            set
            {
                m_IsExistXF = value;
            }
        }

        private int m_YGCount;

        private int m_WSDCount;

        private int m_KTCount;

        private bool m_IsExistCSJ;

        private bool m_IsExistJRQ;

        private bool m_IsExistJDQ;

        private bool m_IsExistXF;

        public RoomClass(string Name)
        {
            strName = Name;
        }

       

        #region 方法

        #region  开启关闭 制冷制热

        /// <summary>
        /// 根据 温湿度值 得到 空调是否 关闭 制冷
        /// </summary>
        public bool ForWsdGetAirOffCool()
        {
            bool blRet = true;
            for (int iIndex = 0; iIndex < listWsd.Count; iIndex++)
            {
                if (listWsd[iIndex] != null)
                {
                    if (listWsd[iIndex].IntTemp > clsEnvirSet.intAirCoolStop)
                    {
                        blRet = false;
                        break;
                    }
                }
            }
            return blRet;
        }

        /// <summary>
        /// 根据 温湿度值 得到 空调是否 关闭 制热
        /// </summary>
        public bool ForWsdGetAirOffHot()
        {
            bool blRet = true;
            for (int iIndex = 0; iIndex < listWsd.Count; iIndex++)
            {
                if (listWsd[iIndex] != null)
                {
                    if (listWsd[iIndex].IntTemp < clsEnvirSet.intAirHotStop)
                    {
                        blRet = false;
                        break;
                    }
                }
            }
            return blRet;
        }

        /// <summary>
        /// 根据 温湿度值 得到 空调是否 开启 制冷
        /// </summary>
        public bool ForWsdGetAirOnCool()
        {
            bool blRet = false;
            for (int iIndex = 0; iIndex < listWsd.Count; iIndex++)
            {
                if (listWsd[iIndex] != null)
                {
                    if (listWsd[iIndex].IntTemp > clsEnvirSet.intAirCoolRun)
                    {
                        blRet = true;
                        break;
                    }
                }
            }
            return blRet;
        }

        /// <summary>
        /// 根据 温湿度值 得到 空调是否 开启 制热
        /// </summary>
        public bool ForWsdGetAirOnHot()
        {
            bool blRet = false;
            for (int iIndex = 0; iIndex < listWsd.Count; iIndex++)
            {
                if (listWsd[iIndex] != null)
                {
                    if (listWsd[iIndex].IntTemp < clsEnvirSet.intAirHotRun )
                    {
                        blRet = true;
                        break;
                    }
                }
            }
            return blRet;
        }

        #endregion

        #region  开关除湿

        /// <summary>
        /// 根据 温湿度值 得到 除湿是否 开启 
        /// </summary>
        public bool ForWsdGetHumiOn()
        {
            bool blRet = false;
            for (int iIndex = 0; iIndex < listWsd.Count; iIndex++)
            {
                if (listWsd[iIndex] != null)
                {
                    if (listWsd[iIndex].IntHumi  > clsEnvirSet.intHumiRun)
                    {
                        blRet = true;
                        break;
                    }
                }
            }
            return blRet;
        }

        /// <summary>
        /// 根据 温湿度值 得到 除湿是否 关闭 
        /// </summary>
        public bool ForWsdGetHumiOff()
        {
            bool blRet = true;
            for (int iIndex = 0; iIndex < listWsd.Count; iIndex++)
            {
                if (listWsd[iIndex] != null)
                {
                    if (listWsd[iIndex].IntHumi > clsEnvirSet.intHumiStop)
                    {
                        blRet = false;
                        break;
                    }
                }
            }
            return blRet;
        }

        #endregion
        #endregion
    }
}

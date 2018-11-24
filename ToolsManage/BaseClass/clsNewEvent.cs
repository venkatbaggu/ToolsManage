using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    class clsNewEvent
    {
        string strType;
        string strPoint;
        string strContent;
        string strPeople;
        string strRemark;
        string strTime;

        public string StrTime
        {
            get { return strTime; }
            set { strTime = value; }
        }

        public string StrRemark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        public string StrPeople
        {
            get { return strPeople; }
            set { strPeople = value; }
        }

        public string StrContent
        {
            get { return strContent; }
            set { strContent = value; }
        }

        public string StrPoint
        {
            get { return strPoint; }
            set { strPoint = value; }
        }

        public string StrType
        {
            get { return strType; }
            set { strType = value; }
        }


    }
}

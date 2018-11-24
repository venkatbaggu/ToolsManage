using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    public class clsWsd
    {
        private int intAddr;
        private int intTemp;
        private int intHumi;

        public int IntHumi
        {
            get { return intHumi; }
            set { intHumi = value; }
        }

        public int IntTemp
        {
            get { return intTemp; }
            set { intTemp = value; }
        }

        public int IntAddr
        {
            get { return intAddr; }
            set { intAddr = value; }
        }

        public clsWsd(int iAddr)
        {
            intAddr = iAddr;
        }

    }
}

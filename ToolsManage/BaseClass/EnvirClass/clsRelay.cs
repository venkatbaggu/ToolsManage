using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    public class clsRelay
    {
        private int intAddr;

        public int IntAddr
        {
            get { return intAddr; }
            set { intAddr = value; }
        }

        public clsRelay(int iAddr)
        {
            intAddr = iAddr;
        }

    }
}

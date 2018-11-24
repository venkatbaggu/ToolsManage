using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    public class clsFire
    {
        private string strAddr;
        private ProbeState state = ProbeState.正常;

        public string StrAddr
        {
            get { return strAddr; }
            set { strAddr = value; }
        }

        public ProbeState State
        {
            get { return state; }
            set { state = value; }
        }

        public clsFire(string addr)
        {
            strAddr = addr;
        }

    }
}

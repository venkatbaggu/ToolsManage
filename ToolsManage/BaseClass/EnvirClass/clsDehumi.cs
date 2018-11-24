using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    public class clsDehumi
    {
        private DeviceRunState state = DeviceRunState.停止;
        private DeviceRunModel handOrAuto = DeviceRunModel.自动;
        private DateTime timeHand = DateTime.Now;

        public DeviceRunModel HandOrAuto
        {
            get { return handOrAuto; }
            set { handOrAuto = value; }
        }

        public DeviceRunState State
        {
            get { return state; }
            set { state = value; }
        }

        public DateTime TimeHand
        {
            get { return timeHand; }
            set { timeHand = value; }
        }

        public clsDehumi()
        {

        }

    }
}

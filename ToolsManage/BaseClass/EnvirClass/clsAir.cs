using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    public class clsAir
    {
        private int intAddr;
        private int intTempSet=26;
        private DeviceRunState state = DeviceRunState.停止;
        private AirRunModel model = AirRunModel.制冷;
        private DeviceRunModel handOrAuto = DeviceRunModel.自动;
        private DateTime timeHand = DateTime.Now;
        private AirFactoryType airType = AirFactoryType.大金;

        public AirFactoryType AirType
        {
            get { return airType; }
            set { airType = value; }
        }

        public int IntTempSet
        {
            get { return intTempSet; }
            set { intTempSet = value; }
        }

        public DeviceRunModel HandOrAuto
        {
            get { return handOrAuto; }
            set { handOrAuto = value; }
        }

        public DateTime TimeHand
        {
            get { return timeHand; }
            set { timeHand = value; }
        }

        public int IntAddr
        {
            get { return intAddr; }
            set { intAddr = value; }
        }

        public DeviceRunState State
        {
            get { return state; }
            set { state = value; }
        }

        public AirRunModel Model
        {
            get { return model; }
            set { model = value; }
        }

        public clsAir(int iAddr,AirFactoryType type)
        {
            intAddr = iAddr;
            airType = type;
        }

    }
}

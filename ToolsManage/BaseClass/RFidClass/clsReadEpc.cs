using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass.RFidClass
{
    /// <summary>
    /// 读到的Epc标签
    /// </summary>
    public class clsReadEpc
    {
        private string strEpc;
        private int intAnt;
        private ToolsBorrRet borrOrRet;
        private DateTime timeLastRead;
        private DeviceUsing usingReadNo;
        private IsReadShow isEpcRead;

        /// <summary>
        /// RFID读写器 读到的EPC标签 是否 处理即形成借还记录
        /// </summary>
        public IsReadShow IsEpcRead
        {
            get { return isEpcRead; }
            set { isEpcRead = value; }
        }

        public DeviceUsing UsingReadNo
        {
            get { return usingReadNo; }
            set { usingReadNo = value; }
        }

        /// <summary>
        /// 上一次 读到的时间
        /// </summary>
        public DateTime TimeLastRead
        {
            get { return timeLastRead; }
            set { timeLastRead = value; }
        }

        public ToolsBorrRet BorrOrRet
        {
            get { return borrOrRet; }
            set { borrOrRet = value; }
        }

        public int IntAnt
        {
            get { return intAnt; }
            set { intAnt = value; }
        }

        public string StrEpc
        {
            get { return strEpc; }
            set { strEpc = value; }
        }

        public clsReadEpc(string epc,int ant)
        {
            strEpc = epc;
            intAnt = ant;
        }

    }
}

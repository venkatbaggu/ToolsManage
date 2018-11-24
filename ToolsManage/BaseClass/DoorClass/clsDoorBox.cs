using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WG3000_COMM.Core;

namespace ToolsManage.BaseClass
{
    public class clsDoorBox
    {
        private string strChildId;
        private string strArea;
        private string strBoxName;
        private string strIp;
        private UInt32  intSn;
        private CommuniState doorNet = CommuniState.已连接;
        private DoorsState boxDoorState = DoorsState.关门;
        private string strOpenTypeDoor = "";
        private string strOpenGroup;
        private string strOpenName;
        private int intNetError;
        private DateTime timeLastAsk;

        public DateTime TimeLastAsk
        {
            get { return timeLastAsk; }
            set { timeLastAsk = value; }
        }

        public int IntNetError
        {
            get { return intNetError; }
            set { intNetError = value; }
        }

        public string StrOpenGroup
        {
            get { return strOpenGroup; }
            set { strOpenGroup = value; }
        }
   
        public string StrOpenName
        {
            get { return strOpenName; }
            set { strOpenName = value; }
        }

        public string StrOpenTypeDoor
        {
            get { return strOpenTypeDoor; }
            set { strOpenTypeDoor = value; }
        }


        private string strHasRfid;
        private string strRfidMain;

        //private DoorsState boxDoorLast = DoorsState.关门;
        //private DoorsState boxDoorNow = DoorsState.关门;
        private wgMjController wgController = null; //2011-11-15_23:50:42 要监控的控制器

        public clsDoorBox(string ip, uint sn, string name, string area)
        {
            wgController = new wgMjController();
            wgController.ControllerSN = (int)sn;
            wgController.PORT = 60000;
            wgController.IP = ip;

            strIp = ip;
            intSn = sn;
            strBoxName = name;
            strArea = area;
      
        }

        #region   子程序

        /// <summary>
        /// 获取门的状态 门号
        /// </summary>
        /// <param name="doorNo"></param>
        /// <returns></returns>
        public DoorsState GetDoorState(int doorNo)
        {
            DoorsState state = DoorsState.初值;
            if (wgController == null)
            {
                wgController = new wgMjController();
                wgController.ControllerSN = (int)intSn;
                wgController.PORT = 60000;
                wgController.IP = strIp;
            }
            if (wgController.GetMjControllerRunInformationIP() > 0) //取控制器信息
            {
                bool bl = wgController.RunInfo.IsOpen(doorNo);
                if (bl)
                    state = DoorsState.关门;
                else
                    state = DoorsState.开门;
            }
            else
            {
                state = DoorsState.不通信;
            }
            return state;
        }

        /// <summary>
        /// 开门 门号
        /// </summary>
        /// <param name="iDoorNo"></param>
        /// <returns></returns>
        public bool OpenDoor(int iDoorNo)
        {
            if (wgController == null)
            {
                wgController = new wgMjController();
                wgController.ControllerSN =(int ) intSn;
                wgController.PORT = 60000;
                wgController.IP = strIp;
            }
            bool blRet = false;
            if (wgController.RemoteOpenDoorIP(1) > 0)
            {
                blRet = true;
            }
            else
            {
                blRet = false;
            }
            return blRet;
        }

        #endregion

        #region  属性

        public CommuniState DoorNet
        {
            get { return doorNet; }
            set { doorNet = value; }
        }

        public string StrRfidMain
        {
            get { return strRfidMain; }
            set { strRfidMain = value; }
        }

        public string StrHasRfid
        {
            get { return strHasRfid; }
            set { strHasRfid = value; }
        }

        public string StrChildId
        {
            get { return strChildId; }
            set { strChildId = value; }
        }

        public string StrArea
        {
            get { return strArea; }
            set { strArea = value; }
        }

        public string StrBoxName
        {
            get { return strBoxName; }
            set { strBoxName = value; }
        }

        public DoorsState BoxDoorState
        {
            get { return boxDoorState; }
            set { boxDoorState = value; }
        }

        public string StrIp
        {
            get { return strIp; }
            set { strIp = value; }
        }

        public UInt32  IntSn
        {
            get { return intSn; }
            set { intSn = value; }
        }

        #endregion

    }
}

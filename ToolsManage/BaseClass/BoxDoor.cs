using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WG3000_COMM.Core;

namespace ToolsManage.BaseClass
{
    /// <summary>
    /// 工具柜 门禁控制器
    /// </summary>
    class BoxDoor
    {
        private string strChildId;
        private string strArea;
        private string strBoxName;
        private string strDoorIp;
        private int intDoorSn;
        private string strHasRfid;
        private string strRfidMain;

        private DoorsState boxDoorLast = DoorsState.关门;
        private DoorsState boxDoorNow = DoorsState.关门;
        private wgMjController wgMjController= null; //2011-11-15_23:50:42 要监控的控制器

        public BoxDoor(string ip,int sn,string name,string area)
        {
            if (wgMjController == null)
            {
                wgMjController = new wgMjController();
                wgMjController.ControllerSN = sn ;
                wgMjController.PORT = 60000;
                wgMjController.IP = ip;
            }
            strBoxName = name;
            strArea = area;
        }

        #region  方法

        public void getDoorState()
        {
            if (wgMjController != null)
            {
                if (wgMjController.GetMjControllerRunInformationIP() > 0) //取控制器信息
                {
                    bool blRet = wgMjController.RunInfo.IsOpen(1);
                    if (blRet == false)
                    {
                        boxDoorNow = DoorsState.开门;
                    }
                    else
                    {
                        boxDoorNow = DoorsState.关门;
                    }
                }
                else
                {
                    boxDoorNow = DoorsState.不通信;
                }
            }
        }

        #endregion

        #region  属性

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

        public DoorsState BoxDoorLast
        {
            get { return boxDoorLast; }
            set { boxDoorLast = value; }
        }

        public DoorsState BoxDoorNow
        {
            get { return boxDoorNow; }
            set { boxDoorNow = value; }
        }

        public string StrDoorIp
        {
            get { return strDoorIp; }
            set { strDoorIp = value; }
        }

        public int IntDoorSn
        {
            get { return intDoorSn; }
            set { intDoorSn = value; }
        }

        #endregion



 

    }
}

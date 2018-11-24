using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WG3000_COMM.Core;

namespace ToolsManage.BaseClass
{
    public class clsDoor
    {
        private string strIp = "";
        private uint intSn = 0;

        //private DoorType wgDoorType;
        private DoorCount wgDoorCount;
        private CommuniState doorNet = CommuniState.已连接;

        private DoorsState doorState1 = DoorsState.关门;
        private DoorsState doorState2 = DoorsState.关门;
        private string strNameDoor1;
        private string strNameDoor2;
        private string strOpenTypeDoor1 = "";
        private string strOpenTypeDoor2 = "";
        private DateTime timeOpenDoor1;
        private DateTime timeOpenDoor2;
        private string strOpenGroup1;
        private string strOpenGroup2;
        private string strOpenName1;
        private string strOpenName2;
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



        //private string strOpenCardDoor1 = "";
        //private string strOpenCardDoor2 = "";
        //private string strOpenCardNo = "";
        //private DoorNo openDoorNo;


        private wgMjController wgController = null; //2011-11-15_23:50:42 要监控的控制器

        public clsDoor(string ip, uint sn)
        {
            strIp = ip;
            intSn = sn;

            wgController = new wgMjController();
            wgController.ControllerSN = (int)intSn;
            wgController.PORT = 60000;
            wgController.IP = strIp;
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
                wgController.ControllerSN = (int)intSn;
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

        #region  字段

        public string StrOpenGroup1
        {
            get { return strOpenGroup1; }
            set { strOpenGroup1 = value; }
        }

        public string StrOpenGroup2
        {
            get { return strOpenGroup2; }
            set { strOpenGroup2 = value; }
        }

        public string StrOpenName1
        {
            get { return strOpenName1; }
            set { strOpenName1 = value; }
        }

        public string StrOpenName2
        {
            get { return strOpenName2; }
            set { strOpenName2 = value; }
        }

        public DateTime TimeOpenDoor2
        {
            get { return timeOpenDoor2; }
            set { timeOpenDoor2 = value; }
        }

        public DateTime TimeOpenDoor1
        {
            get { return timeOpenDoor1; }
            set { timeOpenDoor1 = value; }
        }

        /////// <summary>
        /////// 开门编号
        /////// </summary>
        //public DoorNo OpenDoorNo
        //{
        //    get { return openDoorNo; }
        //    set { openDoorNo = value; }
        //}

        ///// <summary>
        ///// 开门卡号
        ///// </summary>
        ////public string StrOpenCardNo
        //{
        //    get { return strOpenCardNo; }
        //    set { strOpenCardNo = value; }
        //}

        /// <summary>
        /// 门1类型
        /// </summary>
        public string StrOpenTypeDoor1
        {
            get { return strOpenTypeDoor1; }
            set { strOpenTypeDoor1 = value; }
        }

        /// <summary>
        /// 门2类型
        /// </summary>
        public string StrOpenTypeDoor2
        {
            get { return strOpenTypeDoor2; }
            set { strOpenTypeDoor2 = value; }
        }

        /////// <summary>
        /////// 开门1卡号
        /////// </summary>
        //public string StrOpenCardDoor1
        //{
        //    get { return strOpenCardDoor1; }
        //    set { strOpenCardDoor1 = value; }
        //}

        /////// <summary>
        /////// 开门2 卡号
        /////// </summary>
        //public string StrOpenCardDoor2
        //{
        //    get { return strOpenCardDoor2; }
        //    set { strOpenCardDoor2 = value; }
        //}

        /// <summary>
        /// 1#门名称
        /// </summary>
        public string StrNameDoor1
        {
            get { return strNameDoor1; }
            set { strNameDoor1 = value; }
        }

        /// <summary>
        /// 2#门名称
        /// </summary>
        public string StrNameDoor2
        {
            get { return strNameDoor2; }
            set { strNameDoor2 = value; }
        }

        public DoorsState DoorState1
        {
            get { return doorState1; }
            set { doorState1 = value; }
        }

        public DoorsState DoorState2
        {
            get { return doorState2; }
            set { doorState2 = value; }
        }

        public CommuniState DoorNet
        {
            get { return doorNet; }
            set { doorNet = value; }
        }

        public DoorCount WgDoorCount
        {
            get { return wgDoorCount; }
            set { wgDoorCount = value; }
        }

        //public DoorType WgDoorType
        //{
        //    get { return wgDoorType; }
        //    set { wgDoorType = value; }
        //}

        public string StrIp
        {
            get { return strIp; }
            set { strIp = value; }
        }

        public uint IntSn
        {
            get { return intSn; }
            set { intSn = value; }
        }

        #endregion

    }
}

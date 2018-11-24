using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WG3000_COMM.Core;

namespace ToolsManage.BaseClass.DoorClass
{
    /// <summary>
    /// 微耕 控制器 信息
    /// </summary>
    class clsWgInfo
    {
        MjRegisterCard mjrc = new MjRegisterCard();
        private wgMjController wgController = null; //2011-11-15_23:50:42 要监控的控制器
     
        /// <summary>
        /// 控制器 所控制门 列表
        /// </summary>
        public  List<clsDoorInfo> listDoor = new List<clsDoorInfo>();

        #region  字段

        private string strIp;
        private int intSn;
        private int intPort;
        private CommuniState stateOfNet = CommuniState.已连接;
        private DateTime timeErrNet;
        private WgDoorType doorOrBoxDoor;
        private string strNameOfWg;
        private string strArea;
        private string strChildId;

        /// <summary>
        ///  ChildId 编号
        /// </summary>
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

        /// <summary>
        /// 控制器 名称
        /// </summary>
        public string StrNameOfWg
        {
            get { return strNameOfWg; }
            set { strNameOfWg = value; }
        }

        /// <summary>
        /// 门禁 或工具柜门禁 控制类型 
        /// </summary>
        public WgDoorType DoorOrBoxDoor
        {
            get { return doorOrBoxDoor; }
            set { doorOrBoxDoor = value; }
        }

        public DateTime TimeErrNet
        {
            get { return timeErrNet; }
            set { timeErrNet = value; }
        }

        public CommuniState StateOfNet
        {
            get { return stateOfNet; }
            set { stateOfNet = value; }
        }

        public int IntSn
        {
            get { return intSn; }
            set { intSn = value; }
        }

        public int IntPort
        {
            get { return intPort; }
            set { intPort = value; }
        }

        public string StrIp
        {
            get { return strIp; }
            set { strIp = value; }
        }

        #endregion

        #region  构造函数

        public clsWgInfo(string ip, int port, int sn)
        {
            if (wgController == null)
            {
                wgController = new wgMjController();
            }
            strIp = ip;
            intPort = port;
            intSn = sn;

            wgController = new wgMjController();
            wgController.IP = ip;
            wgController.PORT = port;
            wgController.ControllerSN = sn;
        }

        public clsWgInfo()
        {
            if (wgController == null)
            {
                wgController = new wgMjController();
            }
        }

        #endregion

        #region  方法

 
        /// <summary>
        /// 添加卡 权限 控制器下 所有门
        /// </summary>
        /// <param name="strCard"></param>
        /// <returns></returns>
        public bool AddCardPower(string strCard)
        {
            bool blRet = false;

            try
            {
                UInt32 cardid;
                if (!UInt32.TryParse(strCard, System.Globalization.NumberStyles.Integer, null, out cardid))
                {
                    blRet = false;
                    return blRet;
                }
                mjrc.CardID = cardid; //卡号 
                mjrc.Password = uint.Parse("345678"); //密码
                mjrc.ymdStart = DateTime.Now;  //起始日期
                mjrc.ymdEnd = DateTime.Now.AddYears(50);  //结束日期

                for (int iIndex = 0; iIndex < listDoor.Count; iIndex++)
                {
                    int iDoorIndex = listDoor[iIndex].IntDoorIndex;
                    mjrc.ControlSegIndexSet((byte)iDoorIndex, 1); //N号门时段
                }
                int ret = -1;
                using (wgMjControllerPrivilege pri = new wgMjControllerPrivilege())
                {
                    ret = pri.AddPrivilegeOfOneCardIP(intSn, strIp , intPort, mjrc);
                }
                if (ret <= 0)
                    blRet = false;
                else
                    blRet = true;
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
            return blRet;
        }

        public bool DeleteCardPower(string strCard)
        {
            bool blRet = false;
            try
            {
                UInt32 cardid;
                if (UInt32.TryParse(strCard, System.Globalization.NumberStyles.Integer, null, out cardid))
                {
                    int ret = -1;
                    using (wgMjControllerPrivilege pri = new wgMjControllerPrivilege())
                    {
                        ret = pri.DelPrivilegeOfOneCardIP(intSn, strIp, intPort, cardid);
                    }
                    if (ret > 0)
                        blRet = true;
                    else
                        blRet = false;
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
            return blRet;
        }

        /// <summary>
        /// 设置微耕控制器 配置信息
        /// </summary>
        public void SetWgIpSnPort(string ip,int port,int sn)
        {
            wgController.IP = ip;
            wgController.PORT = port;
            wgController.ControllerSN = sn;
            strIp = ip;
            intPort = port;
            intSn = sn;
        }

        /// <summary>
        /// 添加 所控制门 到门禁控制器
        /// </summary>
        /// <param name="listAdd"></param>
        public void AddDoorToList(List<clsDoorInfo> listAdd)
        {
            if (listAdd == null)
                return;
            if (listDoor.Count > 0)
                listDoor.Clear();
            for (int iIndex = 0; iIndex < listAdd.Count; iIndex++)
            {
                listDoor.Add(listAdd[iIndex]);
            }
        }

        /// <summary>
        /// 清除 微耕控制器类 及所控制的所有门
        /// </summary>
        public void Clear()
        {
            if (wgController != null)
                wgController = null;
            if (listDoor.Count > 0)
                listDoor.Clear();
        }

        /// <summary>
        /// 获取门的状态 门号
        /// </summary>
        /// <param name="doorNo"></param>
        /// <returns></returns>
        public DoorsState GetDoorState(int doorNo)
        {
            DoorsState state = DoorsState.初值;
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

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass.DoorClass
{
    /// <summary>
    /// 门的 信息
    /// </summary>
    public class clsDoorInfo
    {
        #region  字段

        private DoorsState stateOfDoor = DoorsState.关门;
        private string strDoorName;
        private bool blAndRfid;
        private int intDoorIndex;
        private string strOpenType;
        private string strGroup;
        private string strUser;
        private DateTime timeOpenDoor;
        private DeviceUsing isOfRfid;

        /// <summary>
        /// 是否 关联RFID读写器 即开门人作为 借用人
        /// </summary>
        public DeviceUsing IsOfRfid
        {
            get { return isOfRfid; }
            set { isOfRfid = value; }
        }

        public DateTime TimeOpenDoor
        {
            get { return timeOpenDoor; }
            set { timeOpenDoor = value; }
        }

        /// <summary>
        /// 开门人
        /// </summary>
        public string StrUser
        {
            get { return strUser; }
            set { strUser = value; }
        }

        /// <summary>
        /// 开门人 所属班组
        /// </summary>
        public string StrGroup
        {
            get { return strGroup; }
            set { strGroup = value; }
        }

        /// <summary>
        /// 开门 类型
        /// </summary>
        public string StrOpenType
        {
            get { return strOpenType; }
            set { strOpenType = value; }
        }

        /// <summary>
        /// 门对应 门禁控制器 控制索引号
        /// </summary>
        public int IntDoorIndex
        {
            get { return intDoorIndex; }
            set { intDoorIndex = value; }
        }

        /// <summary>
        /// 是否关联RFID读写器
        /// </summary>
        public bool BlAndRfid
        {
            get { return blAndRfid; }
            set { blAndRfid = value; }
        }

        //门名称
        public string StrDoorName
        {
            get { return strDoorName; }
            set { strDoorName = value; }
        }

        //门状态
        public DoorsState StateOfDoor
        {
            get { return stateOfDoor; }
            set { stateOfDoor = value; }
        }

        #endregion

        #region  构造函数

        public clsDoorInfo(string name, int index)
        {
            strDoorName = name;
            IntDoorIndex = index;
        }

        public clsDoorInfo()
        {
          
        }

        #endregion



        #region  方法

        /// <summary>
        /// 清除开门信息
        /// </summary>
        public void ClearOpenInfo()
        {
            strOpenType = "";
            strGroup = "";
            strUser = "";
        }

        #endregion



    }
}

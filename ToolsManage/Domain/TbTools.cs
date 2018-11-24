﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbTools</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_Tools", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbTools : ITbTools
    {
        #region 属性
        private Int64 _ID;
        /// <summary></summary>
        [DisplayName("ID")]
        [Description("")]
        [DataObjectField(true, true, false, 19)]
        [BindColumn("ID", "", "bigint")]
        public virtual Int64 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _tvParent;
        /// <summary></summary>
        [DisplayName("tvParent")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("tvParent", "", "varchar(50)")]
        public virtual String tvParent
        {
            get { return _tvParent; }
            set { if (OnPropertyChanging(__.tvParent, value)) { _tvParent = value; OnPropertyChanged(__.tvParent); } }
        }

        private String _tvChildId;
        /// <summary></summary>
        [DisplayName("tvChildId")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("tvChildId", "", "varchar(50)")]
        public virtual String tvChildId
        {
            get { return _tvChildId; }
            set { if (OnPropertyChanging(__.tvChildId, value)) { _tvChildId = value; OnPropertyChanged(__.tvChildId); } }
        }

        private String _IsArea;
        /// <summary></summary>
        [DisplayName("IsArea")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("IsArea", "", "varchar(50)")]
        public virtual String IsArea
        {
            get { return _IsArea; }
            set { if (OnPropertyChanging(__.IsArea, value)) { _IsArea = value; OnPropertyChanged(__.IsArea); } }
        }

        private String _AreaName;
        /// <summary></summary>
        [DisplayName("AreaName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("AreaName", "", "varchar(50)")]
        public virtual String AreaName
        {
            get { return _AreaName; }
            set { if (OnPropertyChanging(__.AreaName, value)) { _AreaName = value; OnPropertyChanged(__.AreaName); } }
        }

        private String _PlaceName;
        /// <summary></summary>
        [DisplayName("PlaceName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PlaceName", "", "varchar(50)")]
        public virtual String PlaceName
        {
            get { return _PlaceName; }
            set { if (OnPropertyChanging(__.PlaceName, value)) { _PlaceName = value; OnPropertyChanged(__.PlaceName); } }
        }

        private String _HasDoor;
        /// <summary></summary>
        [DisplayName("HasDoor")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("HasDoor", "", "varchar(50)")]
        public virtual String HasDoor
        {
            get { return _HasDoor; }
            set { if (OnPropertyChanging(__.HasDoor, value)) { _HasDoor = value; OnPropertyChanged(__.HasDoor); } }
        }

        private String _DoorIp;
        /// <summary></summary>
        [DisplayName("DoorIp")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorIp", "", "varchar(50)")]
        public virtual String DoorIp
        {
            get { return _DoorIp; }
            set { if (OnPropertyChanging(__.DoorIp, value)) { _DoorIp = value; OnPropertyChanged(__.DoorIp); } }
        }

        private String _DoorSn;
        /// <summary></summary>
        [DisplayName("DoorSn")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorSn", "", "varchar(50)")]
        public virtual String DoorSn
        {
            get { return _DoorSn; }
            set { if (OnPropertyChanging(__.DoorSn, value)) { _DoorSn = value; OnPropertyChanged(__.DoorSn); } }
        }

        private String _DoorPsw1;
        /// <summary></summary>
        [DisplayName("DoorPsw1")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorPsw1", "", "varchar(50)")]
        public virtual String DoorPsw1
        {
            get { return _DoorPsw1; }
            set { if (OnPropertyChanging(__.DoorPsw1, value)) { _DoorPsw1 = value; OnPropertyChanged(__.DoorPsw1); } }
        }

        private String _DoorPsw2;
        /// <summary></summary>
        [DisplayName("DoorPsw2")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorPsw2", "", "varchar(50)")]
        public virtual String DoorPsw2
        {
            get { return _DoorPsw2; }
            set { if (OnPropertyChanging(__.DoorPsw2, value)) { _DoorPsw2 = value; OnPropertyChanged(__.DoorPsw2); } }
        }

        private String _DoorPsw3;
        /// <summary></summary>
        [DisplayName("DoorPsw3")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorPsw3", "", "varchar(50)")]
        public virtual String DoorPsw3
        {
            get { return _DoorPsw3; }
            set { if (OnPropertyChanging(__.DoorPsw3, value)) { _DoorPsw3 = value; OnPropertyChanged(__.DoorPsw3); } }
        }

        private String _DoorPsw4;
        /// <summary></summary>
        [DisplayName("DoorPsw4")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorPsw4", "", "varchar(50)")]
        public virtual String DoorPsw4
        {
            get { return _DoorPsw4; }
            set { if (OnPropertyChanging(__.DoorPsw4, value)) { _DoorPsw4 = value; OnPropertyChanged(__.DoorPsw4); } }
        }

        private String _BoxHasRfid;
        /// <summary></summary>
        [DisplayName("BoxHasRfid")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxHasRfid", "", "varchar(50)")]
        public virtual String BoxHasRfid
        {
            get { return _BoxHasRfid; }
            set { if (OnPropertyChanging(__.BoxHasRfid, value)) { _BoxHasRfid = value; OnPropertyChanged(__.BoxHasRfid); } }
        }

        private String _BoxRfidIp;
        /// <summary></summary>
        [DisplayName("BoxRfidIp")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxRfidIp", "", "varchar(50)")]
        public virtual String BoxRfidIp
        {
            get { return _BoxRfidIp; }
            set { if (OnPropertyChanging(__.BoxRfidIp, value)) { _BoxRfidIp = value; OnPropertyChanged(__.BoxRfidIp); } }
        }

        private String _BoxRfidPort;
        /// <summary></summary>
        [DisplayName("BoxRfidPort")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxRfidPort", "", "varchar(50)")]
        public virtual String BoxRfidPort
        {
            get { return _BoxRfidPort; }
            set { if (OnPropertyChanging(__.BoxRfidPort, value)) { _BoxRfidPort = value; OnPropertyChanged(__.BoxRfidPort); } }
        }

        private String _BoxRfidMain;
        /// <summary></summary>
        [DisplayName("BoxRfidMain")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxRfidMain", "", "varchar(50)")]
        public virtual String BoxRfidMain
        {
            get { return _BoxRfidMain; }
            set { if (OnPropertyChanging(__.BoxRfidMain, value)) { _BoxRfidMain = value; OnPropertyChanged(__.BoxRfidMain); } }
        }

        private String _BoxRfidAnt1;
        /// <summary></summary>
        [DisplayName("BoxRfidAnt1")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxRfidAnt1", "", "varchar(50)")]
        public virtual String BoxRfidAnt1
        {
            get { return _BoxRfidAnt1; }
            set { if (OnPropertyChanging(__.BoxRfidAnt1, value)) { _BoxRfidAnt1 = value; OnPropertyChanged(__.BoxRfidAnt1); } }
        }

        private String _BoxRfidAnt2;
        /// <summary></summary>
        [DisplayName("BoxRfidAnt2")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxRfidAnt2", "", "varchar(50)")]
        public virtual String BoxRfidAnt2
        {
            get { return _BoxRfidAnt2; }
            set { if (OnPropertyChanging(__.BoxRfidAnt2, value)) { _BoxRfidAnt2 = value; OnPropertyChanged(__.BoxRfidAnt2); } }
        }

        private String _BoxRfidAnt3;
        /// <summary></summary>
        [DisplayName("BoxRfidAnt3")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxRfidAnt3", "", "varchar(50)")]
        public virtual String BoxRfidAnt3
        {
            get { return _BoxRfidAnt3; }
            set { if (OnPropertyChanging(__.BoxRfidAnt3, value)) { _BoxRfidAnt3 = value; OnPropertyChanged(__.BoxRfidAnt3); } }
        }

        private String _BoxRfidAnt4;
        /// <summary></summary>
        [DisplayName("BoxRfidAnt4")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxRfidAnt4", "", "varchar(50)")]
        public virtual String BoxRfidAnt4
        {
            get { return _BoxRfidAnt4; }
            set { if (OnPropertyChanging(__.BoxRfidAnt4, value)) { _BoxRfidAnt4 = value; OnPropertyChanged(__.BoxRfidAnt4); } }
        }

        private String _ToolType;
        /// <summary></summary>
        [DisplayName("ToolType")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolType", "", "varchar(50)")]
        public virtual String ToolType
        {
            get { return _ToolType; }
            set { if (OnPropertyChanging(__.ToolType, value)) { _ToolType = value; OnPropertyChanged(__.ToolType); } }
        }

        private String _ToolName;
        /// <summary></summary>
        [DisplayName("ToolName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolName", "", "varchar(50)")]
        public virtual String ToolName
        {
            get { return _ToolName; }
            set { if (OnPropertyChanging(__.ToolName, value)) { _ToolName = value; OnPropertyChanged(__.ToolName); } }
        }

        private String _ToolID;
        /// <summary></summary>
        [DisplayName("ToolID")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolID", "", "varchar(50)")]
        public virtual String ToolID
        {
            get { return _ToolID; }
            set { if (OnPropertyChanging(__.ToolID, value)) { _ToolID = value; OnPropertyChanged(__.ToolID); } }
        }

        private String _RFIDCoding;
        /// <summary></summary>
        [DisplayName("RFIDCoding")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RFIDCoding", "", "varchar(50)")]
        public virtual String RFIDCoding
        {
            get { return _RFIDCoding; }
            set { if (OnPropertyChanging(__.RFIDCoding, value)) { _RFIDCoding = value; OnPropertyChanged(__.RFIDCoding); } }
        }

        private String _StoragePlace;
        /// <summary></summary>
        [DisplayName("StoragePlace")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("StoragePlace", "", "varchar(50)")]
        public virtual String StoragePlace
        {
            get { return _StoragePlace; }
            set { if (OnPropertyChanging(__.StoragePlace, value)) { _StoragePlace = value; OnPropertyChanged(__.StoragePlace); } }
        }

        private String _InTime;
        /// <summary></summary>
        [DisplayName("InTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InTime", "", "varchar(50)")]
        public virtual String InTime
        {
            get { return _InTime; }
            set { if (OnPropertyChanging(__.InTime, value)) { _InTime = value; OnPropertyChanged(__.InTime); } }
        }

        private String _InPeople;
        /// <summary></summary>
        [DisplayName("InPeople")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InPeople", "", "varchar(50)")]
        public virtual String InPeople
        {
            get { return _InPeople; }
            set { if (OnPropertyChanging(__.InPeople, value)) { _InPeople = value; OnPropertyChanged(__.InPeople); } }
        }

        private String _IsInStore;
        /// <summary></summary>
        [DisplayName("IsInStore")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("IsInStore", "", "varchar(50)")]
        public virtual String IsInStore
        {
            get { return _IsInStore; }
            set { if (OnPropertyChanging(__.IsInStore, value)) { _IsInStore = value; OnPropertyChanged(__.IsInStore); } }
        }

        private String _BorrowPeople;
        /// <summary></summary>
        [DisplayName("BorrowPeople")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BorrowPeople", "", "varchar(50)")]
        public virtual String BorrowPeople
        {
            get { return _BorrowPeople; }
            set { if (OnPropertyChanging(__.BorrowPeople, value)) { _BorrowPeople = value; OnPropertyChanged(__.BorrowPeople); } }
        }

        private String _BorrowReturnTime;
        /// <summary></summary>
        [DisplayName("BorrowReturnTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BorrowReturnTime", "", "varchar(50)")]
        public virtual String BorrowReturnTime
        {
            get { return _BorrowReturnTime; }
            set { if (OnPropertyChanging(__.BorrowReturnTime, value)) { _BorrowReturnTime = value; OnPropertyChanged(__.BorrowReturnTime); } }
        }

        private String _ToolTestTime;
        /// <summary></summary>
        [DisplayName("ToolTestTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolTestTime", "", "varchar(50)")]
        public virtual String ToolTestTime
        {
            get { return _ToolTestTime; }
            set { if (OnPropertyChanging(__.ToolTestTime, value)) { _ToolTestTime = value; OnPropertyChanged(__.ToolTestTime); } }
        }

        private String _ToolTestResult;
        /// <summary></summary>
        [DisplayName("ToolTestResult")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolTestResult", "", "varchar(50)")]
        public virtual String ToolTestResult
        {
            get { return _ToolTestResult; }
            set { if (OnPropertyChanging(__.ToolTestResult, value)) { _ToolTestResult = value; OnPropertyChanged(__.ToolTestResult); } }
        }

        private String _ToolTestPeople;
        /// <summary></summary>
        [DisplayName("ToolTestPeople")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolTestPeople", "", "varchar(50)")]
        public virtual String ToolTestPeople
        {
            get { return _ToolTestPeople; }
            set { if (OnPropertyChanging(__.ToolTestPeople, value)) { _ToolTestPeople = value; OnPropertyChanged(__.ToolTestPeople); } }
        }

        private String _ToolTestRemark;
        /// <summary></summary>
        [DisplayName("ToolTestRemark")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolTestRemark", "", "varchar(50)")]
        public virtual String ToolTestRemark
        {
            get { return _ToolTestRemark; }
            set { if (OnPropertyChanging(__.ToolTestRemark, value)) { _ToolTestRemark = value; OnPropertyChanged(__.ToolTestRemark); } }
        }

        private String _TestCycle;
        /// <summary></summary>
        [DisplayName("TestCycle")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("TestCycle", "", "varchar(50)")]
        public virtual String TestCycle
        {
            get { return _TestCycle; }
            set { if (OnPropertyChanging(__.TestCycle, value)) { _TestCycle = value; OnPropertyChanged(__.TestCycle); } }
        }

        private String _NextTestTime;
        /// <summary></summary>
        [DisplayName("NextTestTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("NextTestTime", "", "varchar(50)")]
        public virtual String NextTestTime
        {
            get { return _NextTestTime; }
            set { if (OnPropertyChanging(__.NextTestTime, value)) { _NextTestTime = value; OnPropertyChanged(__.NextTestTime); } }
        }

        private String _TestState;
        /// <summary></summary>
        [DisplayName("TestState")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("TestState", "", "varchar(50)")]
        public virtual String TestState
        {
            get { return _TestState; }
            set { if (OnPropertyChanging(__.TestState, value)) { _TestState = value; OnPropertyChanged(__.TestState); } }
        }

        private String _IsScrap;
        /// <summary></summary>
        [DisplayName("IsScrap")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("IsScrap", "", "varchar(50)")]
        public virtual String IsScrap
        {
            get { return _IsScrap; }
            set { if (OnPropertyChanging(__.IsScrap, value)) { _IsScrap = value; OnPropertyChanged(__.IsScrap); } }
        }

        private String _ScrapReason;
        /// <summary></summary>
        [DisplayName("ScrapReason")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ScrapReason", "", "varchar(50)")]
        public virtual String ScrapReason
        {
            get { return _ScrapReason; }
            set { if (OnPropertyChanging(__.ScrapReason, value)) { _ScrapReason = value; OnPropertyChanged(__.ScrapReason); } }
        }

        private String _wgPort;
        /// <summary></summary>
        [DisplayName("wgPort")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("wgPort", "", "varchar(5)")]
        public virtual String wgPort
        {
            get { return _wgPort; }
            set { if (OnPropertyChanging(__.wgPort, value)) { _wgPort = value; OnPropertyChanged(__.wgPort); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.ID : return _ID;
                    case __.tvParent : return _tvParent;
                    case __.tvChildId : return _tvChildId;
                    case __.IsArea : return _IsArea;
                    case __.AreaName : return _AreaName;
                    case __.PlaceName : return _PlaceName;
                    case __.HasDoor : return _HasDoor;
                    case __.DoorIp : return _DoorIp;
                    case __.DoorSn : return _DoorSn;
                    case __.DoorPsw1 : return _DoorPsw1;
                    case __.DoorPsw2 : return _DoorPsw2;
                    case __.DoorPsw3 : return _DoorPsw3;
                    case __.DoorPsw4 : return _DoorPsw4;
                    case __.BoxHasRfid : return _BoxHasRfid;
                    case __.BoxRfidIp : return _BoxRfidIp;
                    case __.BoxRfidPort : return _BoxRfidPort;
                    case __.BoxRfidMain : return _BoxRfidMain;
                    case __.BoxRfidAnt1 : return _BoxRfidAnt1;
                    case __.BoxRfidAnt2 : return _BoxRfidAnt2;
                    case __.BoxRfidAnt3 : return _BoxRfidAnt3;
                    case __.BoxRfidAnt4 : return _BoxRfidAnt4;
                    case __.ToolType : return _ToolType;
                    case __.ToolName : return _ToolName;
                    case __.ToolID : return _ToolID;
                    case __.RFIDCoding : return _RFIDCoding;
                    case __.StoragePlace : return _StoragePlace;
                    case __.InTime : return _InTime;
                    case __.InPeople : return _InPeople;
                    case __.IsInStore : return _IsInStore;
                    case __.BorrowPeople : return _BorrowPeople;
                    case __.BorrowReturnTime : return _BorrowReturnTime;
                    case __.ToolTestTime : return _ToolTestTime;
                    case __.ToolTestResult : return _ToolTestResult;
                    case __.ToolTestPeople : return _ToolTestPeople;
                    case __.ToolTestRemark : return _ToolTestRemark;
                    case __.TestCycle : return _TestCycle;
                    case __.NextTestTime : return _NextTestTime;
                    case __.TestState : return _TestState;
                    case __.IsScrap : return _IsScrap;
                    case __.ScrapReason : return _ScrapReason;
                    case __.wgPort : return _wgPort;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.tvParent : _tvParent = Convert.ToString(value); break;
                    case __.tvChildId : _tvChildId = Convert.ToString(value); break;
                    case __.IsArea : _IsArea = Convert.ToString(value); break;
                    case __.AreaName : _AreaName = Convert.ToString(value); break;
                    case __.PlaceName : _PlaceName = Convert.ToString(value); break;
                    case __.HasDoor : _HasDoor = Convert.ToString(value); break;
                    case __.DoorIp : _DoorIp = Convert.ToString(value); break;
                    case __.DoorSn : _DoorSn = Convert.ToString(value); break;
                    case __.DoorPsw1 : _DoorPsw1 = Convert.ToString(value); break;
                    case __.DoorPsw2 : _DoorPsw2 = Convert.ToString(value); break;
                    case __.DoorPsw3 : _DoorPsw3 = Convert.ToString(value); break;
                    case __.DoorPsw4 : _DoorPsw4 = Convert.ToString(value); break;
                    case __.BoxHasRfid : _BoxHasRfid = Convert.ToString(value); break;
                    case __.BoxRfidIp : _BoxRfidIp = Convert.ToString(value); break;
                    case __.BoxRfidPort : _BoxRfidPort = Convert.ToString(value); break;
                    case __.BoxRfidMain : _BoxRfidMain = Convert.ToString(value); break;
                    case __.BoxRfidAnt1 : _BoxRfidAnt1 = Convert.ToString(value); break;
                    case __.BoxRfidAnt2 : _BoxRfidAnt2 = Convert.ToString(value); break;
                    case __.BoxRfidAnt3 : _BoxRfidAnt3 = Convert.ToString(value); break;
                    case __.BoxRfidAnt4 : _BoxRfidAnt4 = Convert.ToString(value); break;
                    case __.ToolType : _ToolType = Convert.ToString(value); break;
                    case __.ToolName : _ToolName = Convert.ToString(value); break;
                    case __.ToolID : _ToolID = Convert.ToString(value); break;
                    case __.RFIDCoding : _RFIDCoding = Convert.ToString(value); break;
                    case __.StoragePlace : _StoragePlace = Convert.ToString(value); break;
                    case __.InTime : _InTime = Convert.ToString(value); break;
                    case __.InPeople : _InPeople = Convert.ToString(value); break;
                    case __.IsInStore : _IsInStore = Convert.ToString(value); break;
                    case __.BorrowPeople : _BorrowPeople = Convert.ToString(value); break;
                    case __.BorrowReturnTime : _BorrowReturnTime = Convert.ToString(value); break;
                    case __.ToolTestTime : _ToolTestTime = Convert.ToString(value); break;
                    case __.ToolTestResult : _ToolTestResult = Convert.ToString(value); break;
                    case __.ToolTestPeople : _ToolTestPeople = Convert.ToString(value); break;
                    case __.ToolTestRemark : _ToolTestRemark = Convert.ToString(value); break;
                    case __.TestCycle : _TestCycle = Convert.ToString(value); break;
                    case __.NextTestTime : _NextTestTime = Convert.ToString(value); break;
                    case __.TestState : _TestState = Convert.ToString(value); break;
                    case __.IsScrap : _IsScrap = Convert.ToString(value); break;
                    case __.ScrapReason : _ScrapReason = Convert.ToString(value); break;
                    case __.wgPort : _wgPort = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbTools字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field tvParent = FindByName(__.tvParent);

            ///<summary></summary>
            public static readonly Field tvChildId = FindByName(__.tvChildId);

            ///<summary></summary>
            public static readonly Field IsArea = FindByName(__.IsArea);

            ///<summary></summary>
            public static readonly Field AreaName = FindByName(__.AreaName);

            ///<summary></summary>
            public static readonly Field PlaceName = FindByName(__.PlaceName);

            ///<summary></summary>
            public static readonly Field HasDoor = FindByName(__.HasDoor);

            ///<summary></summary>
            public static readonly Field DoorIp = FindByName(__.DoorIp);

            ///<summary></summary>
            public static readonly Field DoorSn = FindByName(__.DoorSn);

            ///<summary></summary>
            public static readonly Field DoorPsw1 = FindByName(__.DoorPsw1);

            ///<summary></summary>
            public static readonly Field DoorPsw2 = FindByName(__.DoorPsw2);

            ///<summary></summary>
            public static readonly Field DoorPsw3 = FindByName(__.DoorPsw3);

            ///<summary></summary>
            public static readonly Field DoorPsw4 = FindByName(__.DoorPsw4);

            ///<summary></summary>
            public static readonly Field BoxHasRfid = FindByName(__.BoxHasRfid);

            ///<summary></summary>
            public static readonly Field BoxRfidIp = FindByName(__.BoxRfidIp);

            ///<summary></summary>
            public static readonly Field BoxRfidPort = FindByName(__.BoxRfidPort);

            ///<summary></summary>
            public static readonly Field BoxRfidMain = FindByName(__.BoxRfidMain);

            ///<summary></summary>
            public static readonly Field BoxRfidAnt1 = FindByName(__.BoxRfidAnt1);

            ///<summary></summary>
            public static readonly Field BoxRfidAnt2 = FindByName(__.BoxRfidAnt2);

            ///<summary></summary>
            public static readonly Field BoxRfidAnt3 = FindByName(__.BoxRfidAnt3);

            ///<summary></summary>
            public static readonly Field BoxRfidAnt4 = FindByName(__.BoxRfidAnt4);

            ///<summary></summary>
            public static readonly Field ToolType = FindByName(__.ToolType);

            ///<summary></summary>
            public static readonly Field ToolName = FindByName(__.ToolName);

            ///<summary></summary>
            public static readonly Field ToolID = FindByName(__.ToolID);

            ///<summary></summary>
            public static readonly Field RFIDCoding = FindByName(__.RFIDCoding);

            ///<summary></summary>
            public static readonly Field StoragePlace = FindByName(__.StoragePlace);

            ///<summary></summary>
            public static readonly Field InTime = FindByName(__.InTime);

            ///<summary></summary>
            public static readonly Field InPeople = FindByName(__.InPeople);

            ///<summary></summary>
            public static readonly Field IsInStore = FindByName(__.IsInStore);

            ///<summary></summary>
            public static readonly Field BorrowPeople = FindByName(__.BorrowPeople);

            ///<summary></summary>
            public static readonly Field BorrowReturnTime = FindByName(__.BorrowReturnTime);

            ///<summary></summary>
            public static readonly Field ToolTestTime = FindByName(__.ToolTestTime);

            ///<summary></summary>
            public static readonly Field ToolTestResult = FindByName(__.ToolTestResult);

            ///<summary></summary>
            public static readonly Field ToolTestPeople = FindByName(__.ToolTestPeople);

            ///<summary></summary>
            public static readonly Field ToolTestRemark = FindByName(__.ToolTestRemark);

            ///<summary></summary>
            public static readonly Field TestCycle = FindByName(__.TestCycle);

            ///<summary></summary>
            public static readonly Field NextTestTime = FindByName(__.NextTestTime);

            ///<summary></summary>
            public static readonly Field TestState = FindByName(__.TestState);

            ///<summary></summary>
            public static readonly Field IsScrap = FindByName(__.IsScrap);

            ///<summary></summary>
            public static readonly Field ScrapReason = FindByName(__.ScrapReason);

            ///<summary></summary>
            public static readonly Field wgPort = FindByName(__.wgPort);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbTools字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String tvParent = "tvParent";

            ///<summary></summary>
            public const String tvChildId = "tvChildId";

            ///<summary></summary>
            public const String IsArea = "IsArea";

            ///<summary></summary>
            public const String AreaName = "AreaName";

            ///<summary></summary>
            public const String PlaceName = "PlaceName";

            ///<summary></summary>
            public const String HasDoor = "HasDoor";

            ///<summary></summary>
            public const String DoorIp = "DoorIp";

            ///<summary></summary>
            public const String DoorSn = "DoorSn";

            ///<summary></summary>
            public const String DoorPsw1 = "DoorPsw1";

            ///<summary></summary>
            public const String DoorPsw2 = "DoorPsw2";

            ///<summary></summary>
            public const String DoorPsw3 = "DoorPsw3";

            ///<summary></summary>
            public const String DoorPsw4 = "DoorPsw4";

            ///<summary></summary>
            public const String BoxHasRfid = "BoxHasRfid";

            ///<summary></summary>
            public const String BoxRfidIp = "BoxRfidIp";

            ///<summary></summary>
            public const String BoxRfidPort = "BoxRfidPort";

            ///<summary></summary>
            public const String BoxRfidMain = "BoxRfidMain";

            ///<summary></summary>
            public const String BoxRfidAnt1 = "BoxRfidAnt1";

            ///<summary></summary>
            public const String BoxRfidAnt2 = "BoxRfidAnt2";

            ///<summary></summary>
            public const String BoxRfidAnt3 = "BoxRfidAnt3";

            ///<summary></summary>
            public const String BoxRfidAnt4 = "BoxRfidAnt4";

            ///<summary></summary>
            public const String ToolType = "ToolType";

            ///<summary></summary>
            public const String ToolName = "ToolName";

            ///<summary></summary>
            public const String ToolID = "ToolID";

            ///<summary></summary>
            public const String RFIDCoding = "RFIDCoding";

            ///<summary></summary>
            public const String StoragePlace = "StoragePlace";

            ///<summary></summary>
            public const String InTime = "InTime";

            ///<summary></summary>
            public const String InPeople = "InPeople";

            ///<summary></summary>
            public const String IsInStore = "IsInStore";

            ///<summary></summary>
            public const String BorrowPeople = "BorrowPeople";

            ///<summary></summary>
            public const String BorrowReturnTime = "BorrowReturnTime";

            ///<summary></summary>
            public const String ToolTestTime = "ToolTestTime";

            ///<summary></summary>
            public const String ToolTestResult = "ToolTestResult";

            ///<summary></summary>
            public const String ToolTestPeople = "ToolTestPeople";

            ///<summary></summary>
            public const String ToolTestRemark = "ToolTestRemark";

            ///<summary></summary>
            public const String TestCycle = "TestCycle";

            ///<summary></summary>
            public const String NextTestTime = "NextTestTime";

            ///<summary></summary>
            public const String TestState = "TestState";

            ///<summary></summary>
            public const String IsScrap = "IsScrap";

            ///<summary></summary>
            public const String ScrapReason = "ScrapReason";

            ///<summary></summary>
            public const String wgPort = "wgPort";

        }
        #endregion
    }

    /// <summary>TbTools接口</summary>
    /// <remarks></remarks>
    public partial interface ITbTools
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String tvParent { get; set; }

        /// <summary></summary>
        String tvChildId { get; set; }

        /// <summary></summary>
        String IsArea { get; set; }

        /// <summary></summary>
        String AreaName { get; set; }

        /// <summary></summary>
        String PlaceName { get; set; }

        /// <summary></summary>
        String HasDoor { get; set; }

        /// <summary></summary>
        String DoorIp { get; set; }

        /// <summary></summary>
        String DoorSn { get; set; }

        /// <summary></summary>
        String DoorPsw1 { get; set; }

        /// <summary></summary>
        String DoorPsw2 { get; set; }

        /// <summary></summary>
        String DoorPsw3 { get; set; }

        /// <summary></summary>
        String DoorPsw4 { get; set; }

        /// <summary></summary>
        String BoxHasRfid { get; set; }

        /// <summary></summary>
        String BoxRfidIp { get; set; }

        /// <summary></summary>
        String BoxRfidPort { get; set; }

        /// <summary></summary>
        String BoxRfidMain { get; set; }

        /// <summary></summary>
        String BoxRfidAnt1 { get; set; }

        /// <summary></summary>
        String BoxRfidAnt2 { get; set; }

        /// <summary></summary>
        String BoxRfidAnt3 { get; set; }

        /// <summary></summary>
        String BoxRfidAnt4 { get; set; }

        /// <summary></summary>
        String ToolType { get; set; }

        /// <summary></summary>
        String ToolName { get; set; }

        /// <summary></summary>
        String ToolID { get; set; }

        /// <summary></summary>
        String RFIDCoding { get; set; }

        /// <summary></summary>
        String StoragePlace { get; set; }

        /// <summary></summary>
        String InTime { get; set; }

        /// <summary></summary>
        String InPeople { get; set; }

        /// <summary></summary>
        String IsInStore { get; set; }

        /// <summary></summary>
        String BorrowPeople { get; set; }

        /// <summary></summary>
        String BorrowReturnTime { get; set; }

        /// <summary></summary>
        String ToolTestTime { get; set; }

        /// <summary></summary>
        String ToolTestResult { get; set; }

        /// <summary></summary>
        String ToolTestPeople { get; set; }

        /// <summary></summary>
        String ToolTestRemark { get; set; }

        /// <summary></summary>
        String TestCycle { get; set; }

        /// <summary></summary>
        String NextTestTime { get; set; }

        /// <summary></summary>
        String TestState { get; set; }

        /// <summary></summary>
        String IsScrap { get; set; }

        /// <summary></summary>
        String ScrapReason { get; set; }

        /// <summary></summary>
        String wgPort { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbAcinfo</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_ACInfo", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbAcinfo : ITbAcinfo
    {
        #region 属性
        private Int32 _ID;
        /// <summary></summary>
        [DisplayName("ID")]
        [Description("")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn("ID", "", "int")]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private Int32 _DeviceTypeID;
        /// <summary></summary>
        [DisplayName("DeviceTypeID")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("DeviceTypeID", "", "int")]
        public virtual Int32 DeviceTypeID
        {
            get { return _DeviceTypeID; }
            set { if (OnPropertyChanging(__.DeviceTypeID, value)) { _DeviceTypeID = value; OnPropertyChanged(__.DeviceTypeID); } }
        }

        private String _ACIP;
        /// <summary></summary>
        [DisplayName("ACIP")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ACIP", "", "nvarchar(50)")]
        public virtual String ACIP
        {
            get { return _ACIP; }
            set { if (OnPropertyChanging(__.ACIP, value)) { _ACIP = value; OnPropertyChanged(__.ACIP); } }
        }

        private Int32 _ACPort;
        /// <summary></summary>
        [DisplayName("ACPort")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("ACPort", "", "int")]
        public virtual Int32 ACPort
        {
            get { return _ACPort; }
            set { if (OnPropertyChanging(__.ACPort, value)) { _ACPort = value; OnPropertyChanged(__.ACPort); } }
        }

        private String _ACSN;
        /// <summary></summary>
        [DisplayName("ACSN")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ACSN", "", "nvarchar(50)")]
        public virtual String ACSN
        {
            get { return _ACSN; }
            set { if (OnPropertyChanging(__.ACSN, value)) { _ACSN = value; OnPropertyChanged(__.ACSN); } }
        }

        private Int32 _DoorCount;
        /// <summary></summary>
        [DisplayName("DoorCount")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("DoorCount", "", "int")]
        public virtual Int32 DoorCount
        {
            get { return _DoorCount; }
            set { if (OnPropertyChanging(__.DoorCount, value)) { _DoorCount = value; OnPropertyChanged(__.DoorCount); } }
        }

        private String _DoorName1;
        /// <summary></summary>
        [DisplayName("DoorName1")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorName1", "", "nvarchar(50)")]
        public virtual String DoorName1
        {
            get { return _DoorName1; }
            set { if (OnPropertyChanging(__.DoorName1, value)) { _DoorName1 = value; OnPropertyChanged(__.DoorName1); } }
        }

        private String _DoorName2;
        /// <summary></summary>
        [DisplayName("DoorName2")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorName2", "", "nvarchar(50)")]
        public virtual String DoorName2
        {
            get { return _DoorName2; }
            set { if (OnPropertyChanging(__.DoorName2, value)) { _DoorName2 = value; OnPropertyChanged(__.DoorName2); } }
        }

        private String _DoorName3;
        /// <summary></summary>
        [DisplayName("DoorName3")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorName3", "", "nvarchar(50)")]
        public virtual String DoorName3
        {
            get { return _DoorName3; }
            set { if (OnPropertyChanging(__.DoorName3, value)) { _DoorName3 = value; OnPropertyChanged(__.DoorName3); } }
        }

        private String _DoorName4;
        /// <summary></summary>
        [DisplayName("DoorName4")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorName4", "", "nvarchar(50)")]
        public virtual String DoorName4
        {
            get { return _DoorName4; }
            set { if (OnPropertyChanging(__.DoorName4, value)) { _DoorName4 = value; OnPropertyChanged(__.DoorName4); } }
        }

        private Boolean _IsValid;
        /// <summary></summary>
        [DisplayName("IsValid")]
        [Description("")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("IsValid", "", "bit")]
        public virtual Boolean IsValid
        {
            get { return _IsValid; }
            set { if (OnPropertyChanging(__.IsValid, value)) { _IsValid = value; OnPropertyChanged(__.IsValid); } }
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
                    case __.DeviceTypeID : return _DeviceTypeID;
                    case __.ACIP : return _ACIP;
                    case __.ACPort : return _ACPort;
                    case __.ACSN : return _ACSN;
                    case __.DoorCount : return _DoorCount;
                    case __.DoorName1 : return _DoorName1;
                    case __.DoorName2 : return _DoorName2;
                    case __.DoorName3 : return _DoorName3;
                    case __.DoorName4 : return _DoorName4;
                    case __.IsValid : return _IsValid;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.DeviceTypeID : _DeviceTypeID = Convert.ToInt32(value); break;
                    case __.ACIP : _ACIP = Convert.ToString(value); break;
                    case __.ACPort : _ACPort = Convert.ToInt32(value); break;
                    case __.ACSN : _ACSN = Convert.ToString(value); break;
                    case __.DoorCount : _DoorCount = Convert.ToInt32(value); break;
                    case __.DoorName1 : _DoorName1 = Convert.ToString(value); break;
                    case __.DoorName2 : _DoorName2 = Convert.ToString(value); break;
                    case __.DoorName3 : _DoorName3 = Convert.ToString(value); break;
                    case __.DoorName4 : _DoorName4 = Convert.ToString(value); break;
                    case __.IsValid : _IsValid = Convert.ToBoolean(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbAcinfo字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field DeviceTypeID = FindByName(__.DeviceTypeID);

            ///<summary></summary>
            public static readonly Field ACIP = FindByName(__.ACIP);

            ///<summary></summary>
            public static readonly Field ACPort = FindByName(__.ACPort);

            ///<summary></summary>
            public static readonly Field ACSN = FindByName(__.ACSN);

            ///<summary></summary>
            public static readonly Field DoorCount = FindByName(__.DoorCount);

            ///<summary></summary>
            public static readonly Field DoorName1 = FindByName(__.DoorName1);

            ///<summary></summary>
            public static readonly Field DoorName2 = FindByName(__.DoorName2);

            ///<summary></summary>
            public static readonly Field DoorName3 = FindByName(__.DoorName3);

            ///<summary></summary>
            public static readonly Field DoorName4 = FindByName(__.DoorName4);

            ///<summary></summary>
            public static readonly Field IsValid = FindByName(__.IsValid);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbAcinfo字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String DeviceTypeID = "DeviceTypeID";

            ///<summary></summary>
            public const String ACIP = "ACIP";

            ///<summary></summary>
            public const String ACPort = "ACPort";

            ///<summary></summary>
            public const String ACSN = "ACSN";

            ///<summary></summary>
            public const String DoorCount = "DoorCount";

            ///<summary></summary>
            public const String DoorName1 = "DoorName1";

            ///<summary></summary>
            public const String DoorName2 = "DoorName2";

            ///<summary></summary>
            public const String DoorName3 = "DoorName3";

            ///<summary></summary>
            public const String DoorName4 = "DoorName4";

            ///<summary></summary>
            public const String IsValid = "IsValid";

        }
        #endregion
    }

    /// <summary>TbAcinfo接口</summary>
    /// <remarks></remarks>
    public partial interface ITbAcinfo
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 DeviceTypeID { get; set; }

        /// <summary></summary>
        String ACIP { get; set; }

        /// <summary></summary>
        Int32 ACPort { get; set; }

        /// <summary></summary>
        String ACSN { get; set; }

        /// <summary></summary>
        Int32 DoorCount { get; set; }

        /// <summary></summary>
        String DoorName1 { get; set; }

        /// <summary></summary>
        String DoorName2 { get; set; }

        /// <summary></summary>
        String DoorName3 { get; set; }

        /// <summary></summary>
        String DoorName4 { get; set; }

        /// <summary></summary>
        Boolean IsValid { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
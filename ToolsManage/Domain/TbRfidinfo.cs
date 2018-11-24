﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRfidinfo</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RfidInfo", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRfidinfo : ITbRfidinfo
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

        private String _RfidIP;
        /// <summary></summary>
        [DisplayName("RfidIP")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RfidIP", "", "nvarchar(50)")]
        public virtual String RfidIP
        {
            get { return _RfidIP; }
            set { if (OnPropertyChanging(__.RfidIP, value)) { _RfidIP = value; OnPropertyChanged(__.RfidIP); } }
        }

        private Int32 _RfidPort;
        /// <summary></summary>
        [DisplayName("RfidPort")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("RfidPort", "", "int")]
        public virtual Int32 RfidPort
        {
            get { return _RfidPort; }
            set { if (OnPropertyChanging(__.RfidPort, value)) { _RfidPort = value; OnPropertyChanged(__.RfidPort); } }
        }

        private Int32 _RfidNo;
        /// <summary></summary>
        [DisplayName("RfidNo")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("RfidNo", "", "int")]
        public virtual Int32 RfidNo
        {
            get { return _RfidNo; }
            set { if (OnPropertyChanging(__.RfidNo, value)) { _RfidNo = value; OnPropertyChanged(__.RfidNo); } }
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
                    case __.RfidIP : return _RfidIP;
                    case __.RfidPort : return _RfidPort;
                    case __.RfidNo : return _RfidNo;
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
                    case __.RfidIP : _RfidIP = Convert.ToString(value); break;
                    case __.RfidPort : _RfidPort = Convert.ToInt32(value); break;
                    case __.RfidNo : _RfidNo = Convert.ToInt32(value); break;
                    case __.IsValid : _IsValid = Convert.ToBoolean(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRfidinfo字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field DeviceTypeID = FindByName(__.DeviceTypeID);

            ///<summary></summary>
            public static readonly Field RfidIP = FindByName(__.RfidIP);

            ///<summary></summary>
            public static readonly Field RfidPort = FindByName(__.RfidPort);

            ///<summary></summary>
            public static readonly Field RfidNo = FindByName(__.RfidNo);

            ///<summary></summary>
            public static readonly Field IsValid = FindByName(__.IsValid);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRfidinfo字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String DeviceTypeID = "DeviceTypeID";

            ///<summary></summary>
            public const String RfidIP = "RfidIP";

            ///<summary></summary>
            public const String RfidPort = "RfidPort";

            ///<summary></summary>
            public const String RfidNo = "RfidNo";

            ///<summary></summary>
            public const String IsValid = "IsValid";

        }
        #endregion
    }

    /// <summary>TbRfidinfo接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRfidinfo
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 DeviceTypeID { get; set; }

        /// <summary></summary>
        String RfidIP { get; set; }

        /// <summary></summary>
        Int32 RfidPort { get; set; }

        /// <summary></summary>
        Int32 RfidNo { get; set; }

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
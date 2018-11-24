﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbDoorrfid</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_DoorRfid", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbDoorrfid : ITbDoorrfid
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

        private Int32 _ACID;
        /// <summary></summary>
        [DisplayName("ACID")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("ACID", "", "int")]
        public virtual Int32 ACID
        {
            get { return _ACID; }
            set { if (OnPropertyChanging(__.ACID, value)) { _ACID = value; OnPropertyChanged(__.ACID); } }
        }

        private Int32 _DoorNo;
        /// <summary></summary>
        [DisplayName("DoorNo")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("DoorNo", "", "int")]
        public virtual Int32 DoorNo
        {
            get { return _DoorNo; }
            set { if (OnPropertyChanging(__.DoorNo, value)) { _DoorNo = value; OnPropertyChanged(__.DoorNo); } }
        }

        private String _DoorName;
        /// <summary></summary>
        [DisplayName("DoorName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorName", "", "nvarchar(50)")]
        public virtual String DoorName
        {
            get { return _DoorName; }
            set { if (OnPropertyChanging(__.DoorName, value)) { _DoorName = value; OnPropertyChanged(__.DoorName); } }
        }

        private Int32 _RfidID;
        /// <summary></summary>
        [DisplayName("RfidID")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("RfidID", "", "int")]
        public virtual Int32 RfidID
        {
            get { return _RfidID; }
            set { if (OnPropertyChanging(__.RfidID, value)) { _RfidID = value; OnPropertyChanged(__.RfidID); } }
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
                    case __.ACID : return _ACID;
                    case __.DoorNo : return _DoorNo;
                    case __.DoorName : return _DoorName;
                    case __.RfidID : return _RfidID;
                    case __.IsValid : return _IsValid;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.ACID : _ACID = Convert.ToInt32(value); break;
                    case __.DoorNo : _DoorNo = Convert.ToInt32(value); break;
                    case __.DoorName : _DoorName = Convert.ToString(value); break;
                    case __.RfidID : _RfidID = Convert.ToInt32(value); break;
                    case __.IsValid : _IsValid = Convert.ToBoolean(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbDoorrfid字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field ACID = FindByName(__.ACID);

            ///<summary></summary>
            public static readonly Field DoorNo = FindByName(__.DoorNo);

            ///<summary></summary>
            public static readonly Field DoorName = FindByName(__.DoorName);

            ///<summary></summary>
            public static readonly Field RfidID = FindByName(__.RfidID);

            ///<summary></summary>
            public static readonly Field IsValid = FindByName(__.IsValid);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbDoorrfid字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String ACID = "ACID";

            ///<summary></summary>
            public const String DoorNo = "DoorNo";

            ///<summary></summary>
            public const String DoorName = "DoorName";

            ///<summary></summary>
            public const String RfidID = "RfidID";

            ///<summary></summary>
            public const String IsValid = "IsValid";

        }
        #endregion
    }

    /// <summary>TbDoorrfid接口</summary>
    /// <remarks></remarks>
    public partial interface ITbDoorrfid
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 ACID { get; set; }

        /// <summary></summary>
        Int32 DoorNo { get; set; }

        /// <summary></summary>
        String DoorName { get; set; }

        /// <summary></summary>
        Int32 RfidID { get; set; }

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
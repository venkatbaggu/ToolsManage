﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbDevicetype</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_DeviceType", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbDevicetype : ITbDevicetype
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

        private Int32 _TypeID;
        /// <summary></summary>
        [DisplayName("TypeID")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("TypeID", "", "int")]
        public virtual Int32 TypeID
        {
            get { return _TypeID; }
            set { if (OnPropertyChanging(__.TypeID, value)) { _TypeID = value; OnPropertyChanged(__.TypeID); } }
        }

        private String _TypeName;
        /// <summary></summary>
        [DisplayName("TypeName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("TypeName", "", "nvarchar(50)")]
        public virtual String TypeName
        {
            get { return _TypeName; }
            set { if (OnPropertyChanging(__.TypeName, value)) { _TypeName = value; OnPropertyChanged(__.TypeName); } }
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
                    case __.TypeID : return _TypeID;
                    case __.TypeName : return _TypeName;
                    case __.IsValid : return _IsValid;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.TypeID : _TypeID = Convert.ToInt32(value); break;
                    case __.TypeName : _TypeName = Convert.ToString(value); break;
                    case __.IsValid : _IsValid = Convert.ToBoolean(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbDevicetype字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field TypeID = FindByName(__.TypeID);

            ///<summary></summary>
            public static readonly Field TypeName = FindByName(__.TypeName);

            ///<summary></summary>
            public static readonly Field IsValid = FindByName(__.IsValid);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbDevicetype字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String TypeID = "TypeID";

            ///<summary></summary>
            public const String TypeName = "TypeName";

            ///<summary></summary>
            public const String IsValid = "IsValid";

        }
        #endregion
    }

    /// <summary>TbDevicetype接口</summary>
    /// <remarks></remarks>
    public partial interface ITbDevicetype
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 TypeID { get; set; }

        /// <summary></summary>
        String TypeName { get; set; }

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
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbSysparm</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_SysParm", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbSysparm : ITbSysparm
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

        private String _ParmName;
        /// <summary></summary>
        [DisplayName("ParmName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ParmName", "", "nvarchar(50)")]
        public virtual String ParmName
        {
            get { return _ParmName; }
            set { if (OnPropertyChanging(__.ParmName, value)) { _ParmName = value; OnPropertyChanged(__.ParmName); } }
        }

        private String _ParmValue;
        /// <summary></summary>
        [DisplayName("ParmValue")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ParmValue", "", "nvarchar(50)")]
        public virtual String ParmValue
        {
            get { return _ParmValue; }
            set { if (OnPropertyChanging(__.ParmValue, value)) { _ParmValue = value; OnPropertyChanged(__.ParmValue); } }
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
                    case __.ParmName : return _ParmName;
                    case __.ParmValue : return _ParmValue;
                    case __.IsValid : return _IsValid;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.ParmName : _ParmName = Convert.ToString(value); break;
                    case __.ParmValue : _ParmValue = Convert.ToString(value); break;
                    case __.IsValid : _IsValid = Convert.ToBoolean(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbSysparm字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field ParmName = FindByName(__.ParmName);

            ///<summary></summary>
            public static readonly Field ParmValue = FindByName(__.ParmValue);

            ///<summary></summary>
            public static readonly Field IsValid = FindByName(__.IsValid);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbSysparm字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String ParmName = "ParmName";

            ///<summary></summary>
            public const String ParmValue = "ParmValue";

            ///<summary></summary>
            public const String IsValid = "IsValid";

        }
        #endregion
    }

    /// <summary>TbSysparm接口</summary>
    /// <remarks></remarks>
    public partial interface ITbSysparm
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String ParmName { get; set; }

        /// <summary></summary>
        String ParmValue { get; set; }

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
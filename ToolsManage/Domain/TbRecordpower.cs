﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRecordpower</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RecordPower", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRecordpower : ITbRecordpower
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

        private String _PowerType;
        /// <summary></summary>
        [DisplayName("PowerType")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PowerType", "", "varchar(50)")]
        public virtual String PowerType
        {
            get { return _PowerType; }
            set { if (OnPropertyChanging(__.PowerType, value)) { _PowerType = value; OnPropertyChanged(__.PowerType); } }
        }

        private String _GroupName;
        /// <summary></summary>
        [DisplayName("GroupName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("GroupName", "", "varchar(50)")]
        public virtual String GroupName
        {
            get { return _GroupName; }
            set { if (OnPropertyChanging(__.GroupName, value)) { _GroupName = value; OnPropertyChanged(__.GroupName); } }
        }

        private String _UserName;
        /// <summary></summary>
        [DisplayName("UserName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UserName", "", "varchar(50)")]
        public virtual String UserName
        {
            get { return _UserName; }
            set { if (OnPropertyChanging(__.UserName, value)) { _UserName = value; OnPropertyChanged(__.UserName); } }
        }

        private String _OperateTime;
        /// <summary></summary>
        [DisplayName("OperateTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OperateTime", "", "varchar(50)")]
        public virtual String OperateTime
        {
            get { return _OperateTime; }
            set { if (OnPropertyChanging(__.OperateTime, value)) { _OperateTime = value; OnPropertyChanged(__.OperateTime); } }
        }

        private String _People;
        /// <summary></summary>
        [DisplayName("People")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("People", "", "varchar(50)")]
        public virtual String People
        {
            get { return _People; }
            set { if (OnPropertyChanging(__.People, value)) { _People = value; OnPropertyChanged(__.People); } }
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
                    case __.PowerType : return _PowerType;
                    case __.GroupName : return _GroupName;
                    case __.UserName : return _UserName;
                    case __.OperateTime : return _OperateTime;
                    case __.People : return _People;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.PowerType : _PowerType = Convert.ToString(value); break;
                    case __.GroupName : _GroupName = Convert.ToString(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.OperateTime : _OperateTime = Convert.ToString(value); break;
                    case __.People : _People = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRecordpower字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field PowerType = FindByName(__.PowerType);

            ///<summary></summary>
            public static readonly Field GroupName = FindByName(__.GroupName);

            ///<summary></summary>
            public static readonly Field UserName = FindByName(__.UserName);

            ///<summary></summary>
            public static readonly Field OperateTime = FindByName(__.OperateTime);

            ///<summary></summary>
            public static readonly Field People = FindByName(__.People);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRecordpower字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String PowerType = "PowerType";

            ///<summary></summary>
            public const String GroupName = "GroupName";

            ///<summary></summary>
            public const String UserName = "UserName";

            ///<summary></summary>
            public const String OperateTime = "OperateTime";

            ///<summary></summary>
            public const String People = "People";

        }
        #endregion
    }

    /// <summary>TbRecordpower接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRecordpower
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String PowerType { get; set; }

        /// <summary></summary>
        String GroupName { get; set; }

        /// <summary></summary>
        String UserName { get; set; }

        /// <summary></summary>
        String OperateTime { get; set; }

        /// <summary></summary>
        String People { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbAlarmevent</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_AlarmEvent", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbAlarmevent : ITbAlarmevent
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

        private String _Type;
        /// <summary></summary>
        [DisplayName("Type")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Type", "", "varchar(50)")]
        public virtual String Type
        {
            get { return _Type; }
            set { if (OnPropertyChanging(__.Type, value)) { _Type = value; OnPropertyChanged(__.Type); } }
        }

        private String _Point;
        /// <summary></summary>
        [DisplayName("Point")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Point", "", "varchar(50)")]
        public virtual String Point
        {
            get { return _Point; }
            set { if (OnPropertyChanging(__.Point, value)) { _Point = value; OnPropertyChanged(__.Point); } }
        }

        private String _EventContent;
        /// <summary></summary>
        [DisplayName("EventContent")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("EventContent", "", "varchar(50)")]
        public virtual String EventContent
        {
            get { return _EventContent; }
            set { if (OnPropertyChanging(__.EventContent, value)) { _EventContent = value; OnPropertyChanged(__.EventContent); } }
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

        private String _Time;
        /// <summary></summary>
        [DisplayName("Time")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Time", "", "varchar(50)")]
        public virtual String Time
        {
            get { return _Time; }
            set { if (OnPropertyChanging(__.Time, value)) { _Time = value; OnPropertyChanged(__.Time); } }
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
                    case __.Type : return _Type;
                    case __.Point : return _Point;
                    case __.EventContent : return _EventContent;
                    case __.People : return _People;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.Type : _Type = Convert.ToString(value); break;
                    case __.Point : _Point = Convert.ToString(value); break;
                    case __.EventContent : _EventContent = Convert.ToString(value); break;
                    case __.People : _People = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbAlarmevent字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Type = FindByName(__.Type);

            ///<summary></summary>
            public static readonly Field Point = FindByName(__.Point);

            ///<summary></summary>
            public static readonly Field EventContent = FindByName(__.EventContent);

            ///<summary></summary>
            public static readonly Field People = FindByName(__.People);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbAlarmevent字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Type = "Type";

            ///<summary></summary>
            public const String Point = "Point";

            ///<summary></summary>
            public const String EventContent = "EventContent";

            ///<summary></summary>
            public const String People = "People";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>TbAlarmevent接口</summary>
    /// <remarks></remarks>
    public partial interface ITbAlarmevent
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String Type { get; set; }

        /// <summary></summary>
        String Point { get; set; }

        /// <summary></summary>
        String EventContent { get; set; }

        /// <summary></summary>
        String People { get; set; }

        /// <summary></summary>
        String Time { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
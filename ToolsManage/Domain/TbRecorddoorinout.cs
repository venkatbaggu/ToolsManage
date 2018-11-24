﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRecorddoorinout</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RecordDoorInOut", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRecorddoorinout : ITbRecorddoorinout
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

        private String _OpenType;
        /// <summary></summary>
        [DisplayName("OpenType")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OpenType", "", "varchar(50)")]
        public virtual String OpenType
        {
            get { return _OpenType; }
            set { if (OnPropertyChanging(__.OpenType, value)) { _OpenType = value; OnPropertyChanged(__.OpenType); } }
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

        private String _OpenTime;
        /// <summary></summary>
        [DisplayName("OpenTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OpenTime", "", "varchar(50)")]
        public virtual String OpenTime
        {
            get { return _OpenTime; }
            set { if (OnPropertyChanging(__.OpenTime, value)) { _OpenTime = value; OnPropertyChanged(__.OpenTime); } }
        }

        private String _CloseTime;
        /// <summary></summary>
        [DisplayName("CloseTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CloseTime", "", "varchar(50)")]
        public virtual String CloseTime
        {
            get { return _CloseTime; }
            set { if (OnPropertyChanging(__.CloseTime, value)) { _CloseTime = value; OnPropertyChanged(__.CloseTime); } }
        }

        private String _DurationTime;
        /// <summary></summary>
        [DisplayName("DurationTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DurationTime", "", "varchar(50)")]
        public virtual String DurationTime
        {
            get { return _DurationTime; }
            set { if (OnPropertyChanging(__.DurationTime, value)) { _DurationTime = value; OnPropertyChanged(__.DurationTime); } }
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
                    case __.OpenType : return _OpenType;
                    case __.Point : return _Point;
                    case __.GroupName : return _GroupName;
                    case __.UserName : return _UserName;
                    case __.OpenTime : return _OpenTime;
                    case __.CloseTime : return _CloseTime;
                    case __.DurationTime : return _DurationTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.OpenType : _OpenType = Convert.ToString(value); break;
                    case __.Point : _Point = Convert.ToString(value); break;
                    case __.GroupName : _GroupName = Convert.ToString(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.OpenTime : _OpenTime = Convert.ToString(value); break;
                    case __.CloseTime : _CloseTime = Convert.ToString(value); break;
                    case __.DurationTime : _DurationTime = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRecorddoorinout字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field OpenType = FindByName(__.OpenType);

            ///<summary></summary>
            public static readonly Field Point = FindByName(__.Point);

            ///<summary></summary>
            public static readonly Field GroupName = FindByName(__.GroupName);

            ///<summary></summary>
            public static readonly Field UserName = FindByName(__.UserName);

            ///<summary></summary>
            public static readonly Field OpenTime = FindByName(__.OpenTime);

            ///<summary></summary>
            public static readonly Field CloseTime = FindByName(__.CloseTime);

            ///<summary></summary>
            public static readonly Field DurationTime = FindByName(__.DurationTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRecorddoorinout字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String OpenType = "OpenType";

            ///<summary></summary>
            public const String Point = "Point";

            ///<summary></summary>
            public const String GroupName = "GroupName";

            ///<summary></summary>
            public const String UserName = "UserName";

            ///<summary></summary>
            public const String OpenTime = "OpenTime";

            ///<summary></summary>
            public const String CloseTime = "CloseTime";

            ///<summary></summary>
            public const String DurationTime = "DurationTime";

        }
        #endregion
    }

    /// <summary>TbRecorddoorinout接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRecorddoorinout
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String OpenType { get; set; }

        /// <summary></summary>
        String Point { get; set; }

        /// <summary></summary>
        String GroupName { get; set; }

        /// <summary></summary>
        String UserName { get; set; }

        /// <summary></summary>
        String OpenTime { get; set; }

        /// <summary></summary>
        String CloseTime { get; set; }

        /// <summary></summary>
        String DurationTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
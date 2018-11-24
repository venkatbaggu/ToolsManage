﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRecordboxdoor</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RecordBoxDoor", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRecordboxdoor : ITbRecordboxdoor
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

        private String _BoxArea;
        /// <summary></summary>
        [DisplayName("BoxArea")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxArea", "", "varchar(50)")]
        public virtual String BoxArea
        {
            get { return _BoxArea; }
            set { if (OnPropertyChanging(__.BoxArea, value)) { _BoxArea = value; OnPropertyChanged(__.BoxArea); } }
        }

        private String _BoxName;
        /// <summary></summary>
        [DisplayName("BoxName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxName", "", "varchar(50)")]
        public virtual String BoxName
        {
            get { return _BoxName; }
            set { if (OnPropertyChanging(__.BoxName, value)) { _BoxName = value; OnPropertyChanged(__.BoxName); } }
        }

        private String _OpenOrClose;
        /// <summary></summary>
        [DisplayName("OpenOrClose")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("OpenOrClose", "", "varchar(50)")]
        public virtual String OpenOrClose
        {
            get { return _OpenOrClose; }
            set { if (OnPropertyChanging(__.OpenOrClose, value)) { _OpenOrClose = value; OnPropertyChanged(__.OpenOrClose); } }
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
                    case __.OpenType : return _OpenType;
                    case __.BoxArea : return _BoxArea;
                    case __.BoxName : return _BoxName;
                    case __.OpenOrClose : return _OpenOrClose;
                    case __.GroupName : return _GroupName;
                    case __.UserName : return _UserName;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.OpenType : _OpenType = Convert.ToString(value); break;
                    case __.BoxArea : _BoxArea = Convert.ToString(value); break;
                    case __.BoxName : _BoxName = Convert.ToString(value); break;
                    case __.OpenOrClose : _OpenOrClose = Convert.ToString(value); break;
                    case __.GroupName : _GroupName = Convert.ToString(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRecordboxdoor字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field OpenType = FindByName(__.OpenType);

            ///<summary></summary>
            public static readonly Field BoxArea = FindByName(__.BoxArea);

            ///<summary></summary>
            public static readonly Field BoxName = FindByName(__.BoxName);

            ///<summary></summary>
            public static readonly Field OpenOrClose = FindByName(__.OpenOrClose);

            ///<summary></summary>
            public static readonly Field GroupName = FindByName(__.GroupName);

            ///<summary></summary>
            public static readonly Field UserName = FindByName(__.UserName);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRecordboxdoor字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String OpenType = "OpenType";

            ///<summary></summary>
            public const String BoxArea = "BoxArea";

            ///<summary></summary>
            public const String BoxName = "BoxName";

            ///<summary></summary>
            public const String OpenOrClose = "OpenOrClose";

            ///<summary></summary>
            public const String GroupName = "GroupName";

            ///<summary></summary>
            public const String UserName = "UserName";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>TbRecordboxdoor接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRecordboxdoor
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String OpenType { get; set; }

        /// <summary></summary>
        String BoxArea { get; set; }

        /// <summary></summary>
        String BoxName { get; set; }

        /// <summary></summary>
        String OpenOrClose { get; set; }

        /// <summary></summary>
        String GroupName { get; set; }

        /// <summary></summary>
        String UserName { get; set; }

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
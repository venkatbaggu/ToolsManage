﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRecordboxpower</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RecordBoxPower", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRecordboxpower : ITbRecordboxpower
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

        private String _Psw1;
        /// <summary></summary>
        [DisplayName("Psw1")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Psw1", "", "varchar(50)")]
        public virtual String Psw1
        {
            get { return _Psw1; }
            set { if (OnPropertyChanging(__.Psw1, value)) { _Psw1 = value; OnPropertyChanged(__.Psw1); } }
        }

        private String _Psw2;
        /// <summary></summary>
        [DisplayName("Psw2")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Psw2", "", "varchar(50)")]
        public virtual String Psw2
        {
            get { return _Psw2; }
            set { if (OnPropertyChanging(__.Psw2, value)) { _Psw2 = value; OnPropertyChanged(__.Psw2); } }
        }

        private String _Psw3;
        /// <summary></summary>
        [DisplayName("Psw3")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Psw3", "", "varchar(50)")]
        public virtual String Psw3
        {
            get { return _Psw3; }
            set { if (OnPropertyChanging(__.Psw3, value)) { _Psw3 = value; OnPropertyChanged(__.Psw3); } }
        }

        private String _Psw4;
        /// <summary></summary>
        [DisplayName("Psw4")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Psw4", "", "varchar(50)")]
        public virtual String Psw4
        {
            get { return _Psw4; }
            set { if (OnPropertyChanging(__.Psw4, value)) { _Psw4 = value; OnPropertyChanged(__.Psw4); } }
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
                    case __.BoxArea : return _BoxArea;
                    case __.BoxName : return _BoxName;
                    case __.Psw1 : return _Psw1;
                    case __.Psw2 : return _Psw2;
                    case __.Psw3 : return _Psw3;
                    case __.Psw4 : return _Psw4;
                    case __.Time : return _Time;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.BoxArea : _BoxArea = Convert.ToString(value); break;
                    case __.BoxName : _BoxName = Convert.ToString(value); break;
                    case __.Psw1 : _Psw1 = Convert.ToString(value); break;
                    case __.Psw2 : _Psw2 = Convert.ToString(value); break;
                    case __.Psw3 : _Psw3 = Convert.ToString(value); break;
                    case __.Psw4 : _Psw4 = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRecordboxpower字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field BoxArea = FindByName(__.BoxArea);

            ///<summary></summary>
            public static readonly Field BoxName = FindByName(__.BoxName);

            ///<summary></summary>
            public static readonly Field Psw1 = FindByName(__.Psw1);

            ///<summary></summary>
            public static readonly Field Psw2 = FindByName(__.Psw2);

            ///<summary></summary>
            public static readonly Field Psw3 = FindByName(__.Psw3);

            ///<summary></summary>
            public static readonly Field Psw4 = FindByName(__.Psw4);

            ///<summary></summary>
            public static readonly Field Time = FindByName(__.Time);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRecordboxpower字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String BoxArea = "BoxArea";

            ///<summary></summary>
            public const String BoxName = "BoxName";

            ///<summary></summary>
            public const String Psw1 = "Psw1";

            ///<summary></summary>
            public const String Psw2 = "Psw2";

            ///<summary></summary>
            public const String Psw3 = "Psw3";

            ///<summary></summary>
            public const String Psw4 = "Psw4";

            ///<summary></summary>
            public const String Time = "Time";

        }
        #endregion
    }

    /// <summary>TbRecordboxpower接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRecordboxpower
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String BoxArea { get; set; }

        /// <summary></summary>
        String BoxName { get; set; }

        /// <summary></summary>
        String Psw1 { get; set; }

        /// <summary></summary>
        String Psw2 { get; set; }

        /// <summary></summary>
        String Psw3 { get; set; }

        /// <summary></summary>
        String Psw4 { get; set; }

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
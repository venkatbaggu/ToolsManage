﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbEnviridata</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_EnviriData", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbEnviridata : ITbEnviridata
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

        private String _Area;
        /// <summary></summary>
        [DisplayName("Area")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Area", "", "varchar(50)")]
        public virtual String Area
        {
            get { return _Area; }
            set { if (OnPropertyChanging(__.Area, value)) { _Area = value; OnPropertyChanged(__.Area); } }
        }

        private String _Addr;
        /// <summary></summary>
        [DisplayName("Addr")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Addr", "", "varchar(50)")]
        public virtual String Addr
        {
            get { return _Addr; }
            set { if (OnPropertyChanging(__.Addr, value)) { _Addr = value; OnPropertyChanged(__.Addr); } }
        }

        private String _Temp;
        /// <summary></summary>
        [DisplayName("Temp")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Temp", "", "varchar(50)")]
        public virtual String Temp
        {
            get { return _Temp; }
            set { if (OnPropertyChanging(__.Temp, value)) { _Temp = value; OnPropertyChanged(__.Temp); } }
        }

        private String _Humi;
        /// <summary></summary>
        [DisplayName("Humi")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Humi", "", "varchar(50)")]
        public virtual String Humi
        {
            get { return _Humi; }
            set { if (OnPropertyChanging(__.Humi, value)) { _Humi = value; OnPropertyChanged(__.Humi); } }
        }

        private String _date;
        /// <summary></summary>
        [DisplayName("date")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("date", "", "varchar(50)")]
        public virtual String date
        {
            get { return _date; }
            set { if (OnPropertyChanging(__.date, value)) { _date = value; OnPropertyChanged(__.date); } }
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
                    case __.Area : return _Area;
                    case __.Addr : return _Addr;
                    case __.Temp : return _Temp;
                    case __.Humi : return _Humi;
                    case __.date : return _date;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.Area : _Area = Convert.ToString(value); break;
                    case __.Addr : _Addr = Convert.ToString(value); break;
                    case __.Temp : _Temp = Convert.ToString(value); break;
                    case __.Humi : _Humi = Convert.ToString(value); break;
                    case __.date : _date = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbEnviridata字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field Area = FindByName(__.Area);

            ///<summary></summary>
            public static readonly Field Addr = FindByName(__.Addr);

            ///<summary></summary>
            public static readonly Field Temp = FindByName(__.Temp);

            ///<summary></summary>
            public static readonly Field Humi = FindByName(__.Humi);

            ///<summary></summary>
            public static readonly Field date = FindByName(__.date);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbEnviridata字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String Area = "Area";

            ///<summary></summary>
            public const String Addr = "Addr";

            ///<summary></summary>
            public const String Temp = "Temp";

            ///<summary></summary>
            public const String Humi = "Humi";

            ///<summary></summary>
            public const String date = "date";

        }
        #endregion
    }

    /// <summary>TbEnviridata接口</summary>
    /// <remarks></remarks>
    public partial interface ITbEnviridata
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String Area { get; set; }

        /// <summary></summary>
        String Addr { get; set; }

        /// <summary></summary>
        String Temp { get; set; }

        /// <summary></summary>
        String Humi { get; set; }

        /// <summary></summary>
        String date { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
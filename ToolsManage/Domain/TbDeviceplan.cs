﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbDeviceplan</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_DevicePlan", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbDeviceplan : ITbDeviceplan
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

        private Int32 _DeviceType;
        /// <summary></summary>
        [DisplayName("DeviceType")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("DeviceType", "", "int")]
        public virtual Int32 DeviceType
        {
            get { return _DeviceType; }
            set { if (OnPropertyChanging(__.DeviceType, value)) { _DeviceType = value; OnPropertyChanged(__.DeviceType); } }
        }

        private String _DeviceName;
        /// <summary></summary>
        [DisplayName("DeviceName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeviceName", "", "nvarchar(50)")]
        public virtual String DeviceName
        {
            get { return _DeviceName; }
            set { if (OnPropertyChanging(__.DeviceName, value)) { _DeviceName = value; OnPropertyChanged(__.DeviceName); } }
        }

        private Int32 _PlanNo;
        /// <summary></summary>
        [DisplayName("PlanNo")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("PlanNo", "", "int")]
        public virtual Int32 PlanNo
        {
            get { return _PlanNo; }
            set { if (OnPropertyChanging(__.PlanNo, value)) { _PlanNo = value; OnPropertyChanged(__.PlanNo); } }
        }

        private String _StartTime;
        /// <summary></summary>
        [DisplayName("StartTime")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("StartTime", "", "nvarchar(20)")]
        public virtual String StartTime
        {
            get { return _StartTime; }
            set { if (OnPropertyChanging(__.StartTime, value)) { _StartTime = value; OnPropertyChanged(__.StartTime); } }
        }

        private String _StopTime;
        /// <summary></summary>
        [DisplayName("StopTime")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("StopTime", "", "nvarchar(20)")]
        public virtual String StopTime
        {
            get { return _StopTime; }
            set { if (OnPropertyChanging(__.StopTime, value)) { _StopTime = value; OnPropertyChanged(__.StopTime); } }
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
                    case __.DeviceType : return _DeviceType;
                    case __.DeviceName : return _DeviceName;
                    case __.PlanNo : return _PlanNo;
                    case __.StartTime : return _StartTime;
                    case __.StopTime : return _StopTime;
                    case __.IsValid : return _IsValid;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.DeviceType : _DeviceType = Convert.ToInt32(value); break;
                    case __.DeviceName : _DeviceName = Convert.ToString(value); break;
                    case __.PlanNo : _PlanNo = Convert.ToInt32(value); break;
                    case __.StartTime : _StartTime = Convert.ToString(value); break;
                    case __.StopTime : _StopTime = Convert.ToString(value); break;
                    case __.IsValid : _IsValid = Convert.ToBoolean(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbDeviceplan字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field DeviceType = FindByName(__.DeviceType);

            ///<summary></summary>
            public static readonly Field DeviceName = FindByName(__.DeviceName);

            ///<summary></summary>
            public static readonly Field PlanNo = FindByName(__.PlanNo);

            ///<summary></summary>
            public static readonly Field StartTime = FindByName(__.StartTime);

            ///<summary></summary>
            public static readonly Field StopTime = FindByName(__.StopTime);

            ///<summary></summary>
            public static readonly Field IsValid = FindByName(__.IsValid);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbDeviceplan字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String DeviceType = "DeviceType";

            ///<summary></summary>
            public const String DeviceName = "DeviceName";

            ///<summary></summary>
            public const String PlanNo = "PlanNo";

            ///<summary></summary>
            public const String StartTime = "StartTime";

            ///<summary></summary>
            public const String StopTime = "StopTime";

            ///<summary></summary>
            public const String IsValid = "IsValid";

        }
        #endregion
    }

    /// <summary>TbDeviceplan接口</summary>
    /// <remarks></remarks>
    public partial interface ITbDeviceplan
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 DeviceType { get; set; }

        /// <summary></summary>
        String DeviceName { get; set; }

        /// <summary></summary>
        Int32 PlanNo { get; set; }

        /// <summary></summary>
        String StartTime { get; set; }

        /// <summary></summary>
        String StopTime { get; set; }

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
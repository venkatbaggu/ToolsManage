﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbNvrinfo</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_NVRInfo", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbNvrinfo : ITbNvrinfo
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

        private Int32 _DeviceTypeID;
        /// <summary></summary>
        [DisplayName("DeviceTypeID")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("DeviceTypeID", "", "int")]
        public virtual Int32 DeviceTypeID
        {
            get { return _DeviceTypeID; }
            set { if (OnPropertyChanging(__.DeviceTypeID, value)) { _DeviceTypeID = value; OnPropertyChanged(__.DeviceTypeID); } }
        }

        private String _NVRIP;
        /// <summary></summary>
        [DisplayName("NVRIP")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("NVRIP", "", "nvarchar(50)")]
        public virtual String NVRIP
        {
            get { return _NVRIP; }
            set { if (OnPropertyChanging(__.NVRIP, value)) { _NVRIP = value; OnPropertyChanged(__.NVRIP); } }
        }

        private Int32 _NVRPort;
        /// <summary></summary>
        [DisplayName("NVRPort")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("NVRPort", "", "int")]
        public virtual Int32 NVRPort
        {
            get { return _NVRPort; }
            set { if (OnPropertyChanging(__.NVRPort, value)) { _NVRPort = value; OnPropertyChanged(__.NVRPort); } }
        }

        private String _NVRLoginName;
        /// <summary></summary>
        [DisplayName("NVRLoginName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("NVRLoginName", "", "nvarchar(50)")]
        public virtual String NVRLoginName
        {
            get { return _NVRLoginName; }
            set { if (OnPropertyChanging(__.NVRLoginName, value)) { _NVRLoginName = value; OnPropertyChanged(__.NVRLoginName); } }
        }

        private String _NVRPsw;
        /// <summary></summary>
        [DisplayName("NVRPsw")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("NVRPsw", "", "nvarchar(50)")]
        public virtual String NVRPsw
        {
            get { return _NVRPsw; }
            set { if (OnPropertyChanging(__.NVRPsw, value)) { _NVRPsw = value; OnPropertyChanged(__.NVRPsw); } }
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
                    case __.DeviceTypeID : return _DeviceTypeID;
                    case __.NVRIP : return _NVRIP;
                    case __.NVRPort : return _NVRPort;
                    case __.NVRLoginName : return _NVRLoginName;
                    case __.NVRPsw : return _NVRPsw;
                    case __.IsValid : return _IsValid;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.DeviceTypeID : _DeviceTypeID = Convert.ToInt32(value); break;
                    case __.NVRIP : _NVRIP = Convert.ToString(value); break;
                    case __.NVRPort : _NVRPort = Convert.ToInt32(value); break;
                    case __.NVRLoginName : _NVRLoginName = Convert.ToString(value); break;
                    case __.NVRPsw : _NVRPsw = Convert.ToString(value); break;
                    case __.IsValid : _IsValid = Convert.ToBoolean(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbNvrinfo字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field DeviceTypeID = FindByName(__.DeviceTypeID);

            ///<summary></summary>
            public static readonly Field NVRIP = FindByName(__.NVRIP);

            ///<summary></summary>
            public static readonly Field NVRPort = FindByName(__.NVRPort);

            ///<summary></summary>
            public static readonly Field NVRLoginName = FindByName(__.NVRLoginName);

            ///<summary></summary>
            public static readonly Field NVRPsw = FindByName(__.NVRPsw);

            ///<summary></summary>
            public static readonly Field IsValid = FindByName(__.IsValid);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbNvrinfo字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String DeviceTypeID = "DeviceTypeID";

            ///<summary></summary>
            public const String NVRIP = "NVRIP";

            ///<summary></summary>
            public const String NVRPort = "NVRPort";

            ///<summary></summary>
            public const String NVRLoginName = "NVRLoginName";

            ///<summary></summary>
            public const String NVRPsw = "NVRPsw";

            ///<summary></summary>
            public const String IsValid = "IsValid";

        }
        #endregion
    }

    /// <summary>TbNvrinfo接口</summary>
    /// <remarks></remarks>
    public partial interface ITbNvrinfo
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        Int32 DeviceTypeID { get; set; }

        /// <summary></summary>
        String NVRIP { get; set; }

        /// <summary></summary>
        Int32 NVRPort { get; set; }

        /// <summary></summary>
        String NVRLoginName { get; set; }

        /// <summary></summary>
        String NVRPsw { get; set; }

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
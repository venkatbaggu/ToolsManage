﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRoominfo</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RoomInfo", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRoominfo : ITbRoominfo
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

        private String _RoomName;
        /// <summary></summary>
        [DisplayName("RoomName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RoomName", "", "varchar(50)")]
        public virtual String RoomName
        {
            get { return _RoomName; }
            set { if (OnPropertyChanging(__.RoomName, value)) { _RoomName = value; OnPropertyChanged(__.RoomName); } }
        }

        private String _IsSingle;
        /// <summary></summary>
        [DisplayName("IsSingle")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("IsSingle", "", "varchar(5)")]
        public virtual String IsSingle
        {
            get { return _IsSingle; }
            set { if (OnPropertyChanging(__.IsSingle, value)) { _IsSingle = value; OnPropertyChanged(__.IsSingle); } }
        }

        private String _WsdAddr1;
        /// <summary></summary>
        [DisplayName("WsdAddr1")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("WsdAddr1", "", "varchar(5)")]
        public virtual String WsdAddr1
        {
            get { return _WsdAddr1; }
            set { if (OnPropertyChanging(__.WsdAddr1, value)) { _WsdAddr1 = value; OnPropertyChanged(__.WsdAddr1); } }
        }

        private String _WsdAddr2;
        /// <summary></summary>
        [DisplayName("WsdAddr2")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("WsdAddr2", "", "varchar(5)")]
        public virtual String WsdAddr2
        {
            get { return _WsdAddr2; }
            set { if (OnPropertyChanging(__.WsdAddr2, value)) { _WsdAddr2 = value; OnPropertyChanged(__.WsdAddr2); } }
        }

        private String _FireAddr1;
        /// <summary></summary>
        [DisplayName("FireAddr1")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("FireAddr1", "", "varchar(5)")]
        public virtual String FireAddr1
        {
            get { return _FireAddr1; }
            set { if (OnPropertyChanging(__.FireAddr1, value)) { _FireAddr1 = value; OnPropertyChanged(__.FireAddr1); } }
        }

        private String _FireAddr2;
        /// <summary></summary>
        [DisplayName("FireAddr2")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("FireAddr2", "", "varchar(5)")]
        public virtual String FireAddr2
        {
            get { return _FireAddr2; }
            set { if (OnPropertyChanging(__.FireAddr2, value)) { _FireAddr2 = value; OnPropertyChanged(__.FireAddr2); } }
        }

        private String _RelayAddr;
        /// <summary></summary>
        [DisplayName("RelayAddr")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("RelayAddr", "", "varchar(5)")]
        public virtual String RelayAddr
        {
            get { return _RelayAddr; }
            set { if (OnPropertyChanging(__.RelayAddr, value)) { _RelayAddr = value; OnPropertyChanged(__.RelayAddr); } }
        }

        private String _AirAddr;
        /// <summary></summary>
        [DisplayName("AirAddr")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirAddr", "", "varchar(5)")]
        public virtual String AirAddr
        {
            get { return _AirAddr; }
            set { if (OnPropertyChanging(__.AirAddr, value)) { _AirAddr = value; OnPropertyChanged(__.AirAddr); } }
        }

        private String _AirFactory;
        /// <summary></summary>
        [DisplayName("AirFactory")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirFactory", "", "varchar(5)")]
        public virtual String AirFactory
        {
            get { return _AirFactory; }
            set { if (OnPropertyChanging(__.AirFactory, value)) { _AirFactory = value; OnPropertyChanged(__.AirFactory); } }
        }

        private String _AirAddr2;
        /// <summary></summary>
        [DisplayName("AirAddr2")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirAddr2", "", "varchar(5)")]
        public virtual String AirAddr2
        {
            get { return _AirAddr2; }
            set { if (OnPropertyChanging(__.AirAddr2, value)) { _AirAddr2 = value; OnPropertyChanged(__.AirAddr2); } }
        }

        private String _AirFactory2;
        /// <summary></summary>
        [DisplayName("AirFactory2")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("AirFactory2", "", "varchar(10)")]
        public virtual String AirFactory2
        {
            get { return _AirFactory2; }
            set { if (OnPropertyChanging(__.AirFactory2, value)) { _AirFactory2 = value; OnPropertyChanged(__.AirFactory2); } }
        }

        private String _IsExistCsj;
        /// <summary></summary>
        [DisplayName("IsExistCsj")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("IsExistCsj", "", "varchar(10)")]
        public virtual String IsExistCsj
        {
            get { return _IsExistCsj; }
            set { if (OnPropertyChanging(__.IsExistCsj, value)) { _IsExistCsj = value; OnPropertyChanged(__.IsExistCsj); } }
        }

        private String _IsExistJrq;
        /// <summary></summary>
        [DisplayName("IsExistJrq")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("IsExistJrq", "", "varchar(10)")]
        public virtual String IsExistJrq
        {
            get { return _IsExistJrq; }
            set { if (OnPropertyChanging(__.IsExistJrq, value)) { _IsExistJrq = value; OnPropertyChanged(__.IsExistJrq); } }
        }

        private String _IsExistXf;
        /// <summary></summary>
        [DisplayName("IsExistXf")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("IsExistXf", "", "varchar(10)")]
        public virtual String IsExistXf
        {
            get { return _IsExistXf; }
            set { if (OnPropertyChanging(__.IsExistXf, value)) { _IsExistXf = value; OnPropertyChanged(__.IsExistXf); } }
        }

        private Int32 _YGCount;
        /// <summary></summary>
        [DisplayName("YGCount")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("YGCount", "", "int")]
        public virtual Int32 YGCount
        {
            get { return _YGCount; }
            set { if (OnPropertyChanging(__.YGCount, value)) { _YGCount = value; OnPropertyChanged(__.YGCount); } }
        }

        private Int32 _WSDCount;
        /// <summary></summary>
        [DisplayName("WSDCount")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("WSDCount", "", "int")]
        public virtual Int32 WSDCount
        {
            get { return _WSDCount; }
            set { if (OnPropertyChanging(__.WSDCount, value)) { _WSDCount = value; OnPropertyChanged(__.WSDCount); } }
        }

        private Int32 _KTCount;
        /// <summary></summary>
        [DisplayName("KTCount")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("KTCount", "", "int")]
        public virtual Int32 KTCount
        {
            get { return _KTCount; }
            set { if (OnPropertyChanging(__.KTCount, value)) { _KTCount = value; OnPropertyChanged(__.KTCount); } }
        }

        private String _IsExistRealy;
        /// <summary></summary>
        [DisplayName("IsExistRealy")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("IsExistRealy", "", "varchar(10)")]
        public virtual String IsExistRealy
        {
            get { return _IsExistRealy; }
            set { if (OnPropertyChanging(__.IsExistRealy, value)) { _IsExistRealy = value; OnPropertyChanged(__.IsExistRealy); } }
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
                    case __.RoomName : return _RoomName;
                    case __.IsSingle : return _IsSingle;
                    case __.WsdAddr1 : return _WsdAddr1;
                    case __.WsdAddr2 : return _WsdAddr2;
                    case __.FireAddr1 : return _FireAddr1;
                    case __.FireAddr2 : return _FireAddr2;
                    case __.RelayAddr : return _RelayAddr;
                    case __.AirAddr : return _AirAddr;
                    case __.AirFactory : return _AirFactory;
                    case __.AirAddr2 : return _AirAddr2;
                    case __.AirFactory2 : return _AirFactory2;
                    case __.IsExistCsj : return _IsExistCsj;
                    case __.IsExistJrq : return _IsExistJrq;
                    case __.IsExistXf : return _IsExistXf;
                    case __.YGCount : return _YGCount;
                    case __.WSDCount : return _WSDCount;
                    case __.KTCount : return _KTCount;
                    case __.IsExistRealy : return _IsExistRealy;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.RoomName : _RoomName = Convert.ToString(value); break;
                    case __.IsSingle : _IsSingle = Convert.ToString(value); break;
                    case __.WsdAddr1 : _WsdAddr1 = Convert.ToString(value); break;
                    case __.WsdAddr2 : _WsdAddr2 = Convert.ToString(value); break;
                    case __.FireAddr1 : _FireAddr1 = Convert.ToString(value); break;
                    case __.FireAddr2 : _FireAddr2 = Convert.ToString(value); break;
                    case __.RelayAddr : _RelayAddr = Convert.ToString(value); break;
                    case __.AirAddr : _AirAddr = Convert.ToString(value); break;
                    case __.AirFactory : _AirFactory = Convert.ToString(value); break;
                    case __.AirAddr2 : _AirAddr2 = Convert.ToString(value); break;
                    case __.AirFactory2 : _AirFactory2 = Convert.ToString(value); break;
                    case __.IsExistCsj : _IsExistCsj = Convert.ToString(value); break;
                    case __.IsExistJrq : _IsExistJrq = Convert.ToString(value); break;
                    case __.IsExistXf : _IsExistXf = Convert.ToString(value); break;
                    case __.YGCount : _YGCount = Convert.ToInt32(value); break;
                    case __.WSDCount : _WSDCount = Convert.ToInt32(value); break;
                    case __.KTCount : _KTCount = Convert.ToInt32(value); break;
                    case __.IsExistRealy : _IsExistRealy = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRoominfo字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field RoomName = FindByName(__.RoomName);

            ///<summary></summary>
            public static readonly Field IsSingle = FindByName(__.IsSingle);

            ///<summary></summary>
            public static readonly Field WsdAddr1 = FindByName(__.WsdAddr1);

            ///<summary></summary>
            public static readonly Field WsdAddr2 = FindByName(__.WsdAddr2);

            ///<summary></summary>
            public static readonly Field FireAddr1 = FindByName(__.FireAddr1);

            ///<summary></summary>
            public static readonly Field FireAddr2 = FindByName(__.FireAddr2);

            ///<summary></summary>
            public static readonly Field RelayAddr = FindByName(__.RelayAddr);

            ///<summary></summary>
            public static readonly Field AirAddr = FindByName(__.AirAddr);

            ///<summary></summary>
            public static readonly Field AirFactory = FindByName(__.AirFactory);

            ///<summary></summary>
            public static readonly Field AirAddr2 = FindByName(__.AirAddr2);

            ///<summary></summary>
            public static readonly Field AirFactory2 = FindByName(__.AirFactory2);

            ///<summary></summary>
            public static readonly Field IsExistCsj = FindByName(__.IsExistCsj);

            ///<summary></summary>
            public static readonly Field IsExistJrq = FindByName(__.IsExistJrq);

            ///<summary></summary>
            public static readonly Field IsExistXf = FindByName(__.IsExistXf);

            ///<summary></summary>
            public static readonly Field YGCount = FindByName(__.YGCount);

            ///<summary></summary>
            public static readonly Field WSDCount = FindByName(__.WSDCount);

            ///<summary></summary>
            public static readonly Field KTCount = FindByName(__.KTCount);

            ///<summary></summary>
            public static readonly Field IsExistRealy = FindByName(__.IsExistRealy);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRoominfo字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String RoomName = "RoomName";

            ///<summary></summary>
            public const String IsSingle = "IsSingle";

            ///<summary></summary>
            public const String WsdAddr1 = "WsdAddr1";

            ///<summary></summary>
            public const String WsdAddr2 = "WsdAddr2";

            ///<summary></summary>
            public const String FireAddr1 = "FireAddr1";

            ///<summary></summary>
            public const String FireAddr2 = "FireAddr2";

            ///<summary></summary>
            public const String RelayAddr = "RelayAddr";

            ///<summary></summary>
            public const String AirAddr = "AirAddr";

            ///<summary></summary>
            public const String AirFactory = "AirFactory";

            ///<summary></summary>
            public const String AirAddr2 = "AirAddr2";

            ///<summary></summary>
            public const String AirFactory2 = "AirFactory2";

            ///<summary></summary>
            public const String IsExistCsj = "IsExistCsj";

            ///<summary></summary>
            public const String IsExistJrq = "IsExistJrq";

            ///<summary></summary>
            public const String IsExistXf = "IsExistXf";

            ///<summary></summary>
            public const String YGCount = "YGCount";

            ///<summary></summary>
            public const String WSDCount = "WSDCount";

            ///<summary></summary>
            public const String KTCount = "KTCount";

            ///<summary></summary>
            public const String IsExistRealy = "IsExistRealy";

        }
        #endregion
    }

    /// <summary>TbRoominfo接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRoominfo
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String RoomName { get; set; }

        /// <summary></summary>
        String IsSingle { get; set; }

        /// <summary></summary>
        String WsdAddr1 { get; set; }

        /// <summary></summary>
        String WsdAddr2 { get; set; }

        /// <summary></summary>
        String FireAddr1 { get; set; }

        /// <summary></summary>
        String FireAddr2 { get; set; }

        /// <summary></summary>
        String RelayAddr { get; set; }

        /// <summary></summary>
        String AirAddr { get; set; }

        /// <summary></summary>
        String AirFactory { get; set; }

        /// <summary></summary>
        String AirAddr2 { get; set; }

        /// <summary></summary>
        String AirFactory2 { get; set; }

        /// <summary></summary>
        String IsExistCsj { get; set; }

        /// <summary></summary>
        String IsExistJrq { get; set; }

        /// <summary></summary>
        String IsExistXf { get; set; }

        /// <summary></summary>
        Int32 YGCount { get; set; }

        /// <summary></summary>
        Int32 WSDCount { get; set; }

        /// <summary></summary>
        Int32 KTCount { get; set; }

        /// <summary></summary>
        String IsExistRealy { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
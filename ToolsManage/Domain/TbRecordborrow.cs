﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRecordborrow</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RecordBorrow", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRecordborrow : ITbRecordborrow
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

        private String _ToolType;
        /// <summary></summary>
        [DisplayName("ToolType")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolType", "", "varchar(50)")]
        public virtual String ToolType
        {
            get { return _ToolType; }
            set { if (OnPropertyChanging(__.ToolType, value)) { _ToolType = value; OnPropertyChanged(__.ToolType); } }
        }

        private String _ToolName;
        /// <summary></summary>
        [DisplayName("ToolName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolName", "", "varchar(50)")]
        public virtual String ToolName
        {
            get { return _ToolName; }
            set { if (OnPropertyChanging(__.ToolName, value)) { _ToolName = value; OnPropertyChanged(__.ToolName); } }
        }

        private String _ToolID;
        /// <summary></summary>
        [DisplayName("ToolID")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolID", "", "varchar(50)")]
        public virtual String ToolID
        {
            get { return _ToolID; }
            set { if (OnPropertyChanging(__.ToolID, value)) { _ToolID = value; OnPropertyChanged(__.ToolID); } }
        }

        private String _RFIDCoding;
        /// <summary></summary>
        [DisplayName("RFIDCoding")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RFIDCoding", "", "varchar(50)")]
        public virtual String RFIDCoding
        {
            get { return _RFIDCoding; }
            set { if (OnPropertyChanging(__.RFIDCoding, value)) { _RFIDCoding = value; OnPropertyChanged(__.RFIDCoding); } }
        }

        private String _PeopleBorrow;
        /// <summary></summary>
        [DisplayName("PeopleBorrow")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PeopleBorrow", "", "varchar(50)")]
        public virtual String PeopleBorrow
        {
            get { return _PeopleBorrow; }
            set { if (OnPropertyChanging(__.PeopleBorrow, value)) { _PeopleBorrow = value; OnPropertyChanged(__.PeopleBorrow); } }
        }

        private String _BorrowTime;
        /// <summary></summary>
        [DisplayName("BorrowTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BorrowTime", "", "varchar(50)")]
        public virtual String BorrowTime
        {
            get { return _BorrowTime; }
            set { if (OnPropertyChanging(__.BorrowTime, value)) { _BorrowTime = value; OnPropertyChanged(__.BorrowTime); } }
        }

        private String _PeopleReturn;
        /// <summary></summary>
        [DisplayName("PeopleReturn")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PeopleReturn", "", "varchar(50)")]
        public virtual String PeopleReturn
        {
            get { return _PeopleReturn; }
            set { if (OnPropertyChanging(__.PeopleReturn, value)) { _PeopleReturn = value; OnPropertyChanged(__.PeopleReturn); } }
        }

        private String _IsReturn;
        /// <summary></summary>
        [DisplayName("IsReturn")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("IsReturn", "", "varchar(50)")]
        public virtual String IsReturn
        {
            get { return _IsReturn; }
            set { if (OnPropertyChanging(__.IsReturn, value)) { _IsReturn = value; OnPropertyChanged(__.IsReturn); } }
        }

        private String _ReturnTime;
        /// <summary></summary>
        [DisplayName("ReturnTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ReturnTime", "", "varchar(50)")]
        public virtual String ReturnTime
        {
            get { return _ReturnTime; }
            set { if (OnPropertyChanging(__.ReturnTime, value)) { _ReturnTime = value; OnPropertyChanged(__.ReturnTime); } }
        }

        private String _BorrowDuration;
        /// <summary></summary>
        [DisplayName("BorrowDuration")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BorrowDuration", "", "varchar(50)")]
        public virtual String BorrowDuration
        {
            get { return _BorrowDuration; }
            set { if (OnPropertyChanging(__.BorrowDuration, value)) { _BorrowDuration = value; OnPropertyChanged(__.BorrowDuration); } }
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
                    case __.ToolType : return _ToolType;
                    case __.ToolName : return _ToolName;
                    case __.ToolID : return _ToolID;
                    case __.RFIDCoding : return _RFIDCoding;
                    case __.PeopleBorrow : return _PeopleBorrow;
                    case __.BorrowTime : return _BorrowTime;
                    case __.PeopleReturn : return _PeopleReturn;
                    case __.IsReturn : return _IsReturn;
                    case __.ReturnTime : return _ReturnTime;
                    case __.BorrowDuration : return _BorrowDuration;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.ToolType : _ToolType = Convert.ToString(value); break;
                    case __.ToolName : _ToolName = Convert.ToString(value); break;
                    case __.ToolID : _ToolID = Convert.ToString(value); break;
                    case __.RFIDCoding : _RFIDCoding = Convert.ToString(value); break;
                    case __.PeopleBorrow : _PeopleBorrow = Convert.ToString(value); break;
                    case __.BorrowTime : _BorrowTime = Convert.ToString(value); break;
                    case __.PeopleReturn : _PeopleReturn = Convert.ToString(value); break;
                    case __.IsReturn : _IsReturn = Convert.ToString(value); break;
                    case __.ReturnTime : _ReturnTime = Convert.ToString(value); break;
                    case __.BorrowDuration : _BorrowDuration = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRecordborrow字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field ToolType = FindByName(__.ToolType);

            ///<summary></summary>
            public static readonly Field ToolName = FindByName(__.ToolName);

            ///<summary></summary>
            public static readonly Field ToolID = FindByName(__.ToolID);

            ///<summary></summary>
            public static readonly Field RFIDCoding = FindByName(__.RFIDCoding);

            ///<summary></summary>
            public static readonly Field PeopleBorrow = FindByName(__.PeopleBorrow);

            ///<summary></summary>
            public static readonly Field BorrowTime = FindByName(__.BorrowTime);

            ///<summary></summary>
            public static readonly Field PeopleReturn = FindByName(__.PeopleReturn);

            ///<summary></summary>
            public static readonly Field IsReturn = FindByName(__.IsReturn);

            ///<summary></summary>
            public static readonly Field ReturnTime = FindByName(__.ReturnTime);

            ///<summary></summary>
            public static readonly Field BorrowDuration = FindByName(__.BorrowDuration);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRecordborrow字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String ToolType = "ToolType";

            ///<summary></summary>
            public const String ToolName = "ToolName";

            ///<summary></summary>
            public const String ToolID = "ToolID";

            ///<summary></summary>
            public const String RFIDCoding = "RFIDCoding";

            ///<summary></summary>
            public const String PeopleBorrow = "PeopleBorrow";

            ///<summary></summary>
            public const String BorrowTime = "BorrowTime";

            ///<summary></summary>
            public const String PeopleReturn = "PeopleReturn";

            ///<summary></summary>
            public const String IsReturn = "IsReturn";

            ///<summary></summary>
            public const String ReturnTime = "ReturnTime";

            ///<summary></summary>
            public const String BorrowDuration = "BorrowDuration";

        }
        #endregion
    }

    /// <summary>TbRecordborrow接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRecordborrow
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String ToolType { get; set; }

        /// <summary></summary>
        String ToolName { get; set; }

        /// <summary></summary>
        String ToolID { get; set; }

        /// <summary></summary>
        String RFIDCoding { get; set; }

        /// <summary></summary>
        String PeopleBorrow { get; set; }

        /// <summary></summary>
        String BorrowTime { get; set; }

        /// <summary></summary>
        String PeopleReturn { get; set; }

        /// <summary></summary>
        String IsReturn { get; set; }

        /// <summary></summary>
        String ReturnTime { get; set; }

        /// <summary></summary>
        String BorrowDuration { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
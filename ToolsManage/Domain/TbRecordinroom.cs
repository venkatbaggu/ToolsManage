﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRecordinroom</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RecordInRoom", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRecordinroom : ITbRecordinroom
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

        private String _StoragePlace;
        /// <summary></summary>
        [DisplayName("StoragePlace")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("StoragePlace", "", "varchar(50)")]
        public virtual String StoragePlace
        {
            get { return _StoragePlace; }
            set { if (OnPropertyChanging(__.StoragePlace, value)) { _StoragePlace = value; OnPropertyChanged(__.StoragePlace); } }
        }

        private String _InPeople;
        /// <summary></summary>
        [DisplayName("InPeople")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InPeople", "", "varchar(50)")]
        public virtual String InPeople
        {
            get { return _InPeople; }
            set { if (OnPropertyChanging(__.InPeople, value)) { _InPeople = value; OnPropertyChanged(__.InPeople); } }
        }

        private String _InTime;
        /// <summary></summary>
        [DisplayName("InTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InTime", "", "varchar(50)")]
        public virtual String InTime
        {
            get { return _InTime; }
            set { if (OnPropertyChanging(__.InTime, value)) { _InTime = value; OnPropertyChanged(__.InTime); } }
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
                    case __.StoragePlace : return _StoragePlace;
                    case __.InPeople : return _InPeople;
                    case __.InTime : return _InTime;
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
                    case __.StoragePlace : _StoragePlace = Convert.ToString(value); break;
                    case __.InPeople : _InPeople = Convert.ToString(value); break;
                    case __.InTime : _InTime = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRecordinroom字段信息的快捷方式</summary>
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
            public static readonly Field StoragePlace = FindByName(__.StoragePlace);

            ///<summary></summary>
            public static readonly Field InPeople = FindByName(__.InPeople);

            ///<summary></summary>
            public static readonly Field InTime = FindByName(__.InTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRecordinroom字段名称的快捷方式</summary>
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
            public const String StoragePlace = "StoragePlace";

            ///<summary></summary>
            public const String InPeople = "InPeople";

            ///<summary></summary>
            public const String InTime = "InTime";

        }
        #endregion
    }

    /// <summary>TbRecordinroom接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRecordinroom
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
        String StoragePlace { get; set; }

        /// <summary></summary>
        String InPeople { get; set; }

        /// <summary></summary>
        String InTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
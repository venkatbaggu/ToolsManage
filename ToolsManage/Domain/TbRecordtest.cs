﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbRecordtest</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_RecordTest", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbRecordtest : ITbRecordtest
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

        private String _ToolTestTime;
        /// <summary></summary>
        [DisplayName("ToolTestTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolTestTime", "", "varchar(50)")]
        public virtual String ToolTestTime
        {
            get { return _ToolTestTime; }
            set { if (OnPropertyChanging(__.ToolTestTime, value)) { _ToolTestTime = value; OnPropertyChanged(__.ToolTestTime); } }
        }

        private String _ToolTestResult;
        /// <summary></summary>
        [DisplayName("ToolTestResult")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolTestResult", "", "varchar(50)")]
        public virtual String ToolTestResult
        {
            get { return _ToolTestResult; }
            set { if (OnPropertyChanging(__.ToolTestResult, value)) { _ToolTestResult = value; OnPropertyChanged(__.ToolTestResult); } }
        }

        private String _ToolTestPeople;
        /// <summary></summary>
        [DisplayName("ToolTestPeople")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolTestPeople", "", "varchar(50)")]
        public virtual String ToolTestPeople
        {
            get { return _ToolTestPeople; }
            set { if (OnPropertyChanging(__.ToolTestPeople, value)) { _ToolTestPeople = value; OnPropertyChanged(__.ToolTestPeople); } }
        }

        private String _ToolTestRemark;
        /// <summary></summary>
        [DisplayName("ToolTestRemark")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolTestRemark", "", "varchar(50)")]
        public virtual String ToolTestRemark
        {
            get { return _ToolTestRemark; }
            set { if (OnPropertyChanging(__.ToolTestRemark, value)) { _ToolTestRemark = value; OnPropertyChanged(__.ToolTestRemark); } }
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
                    case __.ToolTestTime : return _ToolTestTime;
                    case __.ToolTestResult : return _ToolTestResult;
                    case __.ToolTestPeople : return _ToolTestPeople;
                    case __.ToolTestRemark : return _ToolTestRemark;
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
                    case __.ToolTestTime : _ToolTestTime = Convert.ToString(value); break;
                    case __.ToolTestResult : _ToolTestResult = Convert.ToString(value); break;
                    case __.ToolTestPeople : _ToolTestPeople = Convert.ToString(value); break;
                    case __.ToolTestRemark : _ToolTestRemark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbRecordtest字段信息的快捷方式</summary>
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
            public static readonly Field ToolTestTime = FindByName(__.ToolTestTime);

            ///<summary></summary>
            public static readonly Field ToolTestResult = FindByName(__.ToolTestResult);

            ///<summary></summary>
            public static readonly Field ToolTestPeople = FindByName(__.ToolTestPeople);

            ///<summary></summary>
            public static readonly Field ToolTestRemark = FindByName(__.ToolTestRemark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbRecordtest字段名称的快捷方式</summary>
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
            public const String ToolTestTime = "ToolTestTime";

            ///<summary></summary>
            public const String ToolTestResult = "ToolTestResult";

            ///<summary></summary>
            public const String ToolTestPeople = "ToolTestPeople";

            ///<summary></summary>
            public const String ToolTestRemark = "ToolTestRemark";

        }
        #endregion
    }

    /// <summary>TbRecordtest接口</summary>
    /// <remarks></remarks>
    public partial interface ITbRecordtest
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
        String ToolTestTime { get; set; }

        /// <summary></summary>
        String ToolTestResult { get; set; }

        /// <summary></summary>
        String ToolTestPeople { get; set; }

        /// <summary></summary>
        String ToolTestRemark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbTypeandname</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_TypeAndName", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbTypeandname : ITbTypeandname
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

        private String _tvParent;
        /// <summary></summary>
        [DisplayName("tvParent")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("tvParent", "", "varchar(50)")]
        public virtual String tvParent
        {
            get { return _tvParent; }
            set { if (OnPropertyChanging(__.tvParent, value)) { _tvParent = value; OnPropertyChanged(__.tvParent); } }
        }

        private String _tvChildId;
        /// <summary></summary>
        [DisplayName("tvChildId")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("tvChildId", "", "varchar(50)")]
        public virtual String tvChildId
        {
            get { return _tvChildId; }
            set { if (OnPropertyChanging(__.tvChildId, value)) { _tvChildId = value; OnPropertyChanged(__.tvChildId); } }
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

        private String _ToolsNameCoding;
        /// <summary></summary>
        [DisplayName("ToolsNameCoding")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolsNameCoding", "", "varchar(50)")]
        public virtual String ToolsNameCoding
        {
            get { return _ToolsNameCoding; }
            set { if (OnPropertyChanging(__.ToolsNameCoding, value)) { _ToolsNameCoding = value; OnPropertyChanged(__.ToolsNameCoding); } }
        }

        private String _ToolsCycle;
        /// <summary></summary>
        [DisplayName("ToolsCycle")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ToolsCycle", "", "varchar(50)")]
        public virtual String ToolsCycle
        {
            get { return _ToolsCycle; }
            set { if (OnPropertyChanging(__.ToolsCycle, value)) { _ToolsCycle = value; OnPropertyChanged(__.ToolsCycle); } }
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
                    case __.tvParent : return _tvParent;
                    case __.tvChildId : return _tvChildId;
                    case __.ToolType : return _ToolType;
                    case __.ToolName : return _ToolName;
                    case __.ToolsNameCoding : return _ToolsNameCoding;
                    case __.ToolsCycle : return _ToolsCycle;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.tvParent : _tvParent = Convert.ToString(value); break;
                    case __.tvChildId : _tvChildId = Convert.ToString(value); break;
                    case __.ToolType : _ToolType = Convert.ToString(value); break;
                    case __.ToolName : _ToolName = Convert.ToString(value); break;
                    case __.ToolsNameCoding : _ToolsNameCoding = Convert.ToString(value); break;
                    case __.ToolsCycle : _ToolsCycle = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbTypeandname字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field tvParent = FindByName(__.tvParent);

            ///<summary></summary>
            public static readonly Field tvChildId = FindByName(__.tvChildId);

            ///<summary></summary>
            public static readonly Field ToolType = FindByName(__.ToolType);

            ///<summary></summary>
            public static readonly Field ToolName = FindByName(__.ToolName);

            ///<summary></summary>
            public static readonly Field ToolsNameCoding = FindByName(__.ToolsNameCoding);

            ///<summary></summary>
            public static readonly Field ToolsCycle = FindByName(__.ToolsCycle);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbTypeandname字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String tvParent = "tvParent";

            ///<summary></summary>
            public const String tvChildId = "tvChildId";

            ///<summary></summary>
            public const String ToolType = "ToolType";

            ///<summary></summary>
            public const String ToolName = "ToolName";

            ///<summary></summary>
            public const String ToolsNameCoding = "ToolsNameCoding";

            ///<summary></summary>
            public const String ToolsCycle = "ToolsCycle";

        }
        #endregion
    }

    /// <summary>TbTypeandname接口</summary>
    /// <remarks></remarks>
    public partial interface ITbTypeandname
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String tvParent { get; set; }

        /// <summary></summary>
        String tvChildId { get; set; }

        /// <summary></summary>
        String ToolType { get; set; }

        /// <summary></summary>
        String ToolName { get; set; }

        /// <summary></summary>
        String ToolsNameCoding { get; set; }

        /// <summary></summary>
        String ToolsCycle { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
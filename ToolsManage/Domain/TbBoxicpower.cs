﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbBoxicpower</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_BoxIcPower", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbBoxicpower : ITbBoxicpower
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

        private String _BoxParentId;
        /// <summary></summary>
        [DisplayName("BoxParentId")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxParentId", "", "varchar(50)")]
        public virtual String BoxParentId
        {
            get { return _BoxParentId; }
            set { if (OnPropertyChanging(__.BoxParentId, value)) { _BoxParentId = value; OnPropertyChanged(__.BoxParentId); } }
        }

        private String _BoxChildId;
        /// <summary></summary>
        [DisplayName("BoxChildId")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("BoxChildId", "", "varchar(50)")]
        public virtual String BoxChildId
        {
            get { return _BoxChildId; }
            set { if (OnPropertyChanging(__.BoxChildId, value)) { _BoxChildId = value; OnPropertyChanged(__.BoxChildId); } }
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

        private String _UserId;
        /// <summary></summary>
        [DisplayName("UserId")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UserId", "", "varchar(50)")]
        public virtual String UserId
        {
            get { return _UserId; }
            set { if (OnPropertyChanging(__.UserId, value)) { _UserId = value; OnPropertyChanged(__.UserId); } }
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

        private String _IcNo;
        /// <summary></summary>
        [DisplayName("IcNo")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("IcNo", "", "varchar(50)")]
        public virtual String IcNo
        {
            get { return _IcNo; }
            set { if (OnPropertyChanging(__.IcNo, value)) { _IcNo = value; OnPropertyChanged(__.IcNo); } }
        }

        private String _CardNo;
        /// <summary></summary>
        [DisplayName("CardNo")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CardNo", "", "varchar(50)")]
        public virtual String CardNo
        {
            get { return _CardNo; }
            set { if (OnPropertyChanging(__.CardNo, value)) { _CardNo = value; OnPropertyChanged(__.CardNo); } }
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
                    case __.BoxParentId : return _BoxParentId;
                    case __.BoxChildId : return _BoxChildId;
                    case __.BoxName : return _BoxName;
                    case __.UserId : return _UserId;
                    case __.UserName : return _UserName;
                    case __.IcNo : return _IcNo;
                    case __.CardNo : return _CardNo;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt64(value); break;
                    case __.BoxParentId : _BoxParentId = Convert.ToString(value); break;
                    case __.BoxChildId : _BoxChildId = Convert.ToString(value); break;
                    case __.BoxName : _BoxName = Convert.ToString(value); break;
                    case __.UserId : _UserId = Convert.ToString(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.IcNo : _IcNo = Convert.ToString(value); break;
                    case __.CardNo : _CardNo = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbBoxicpower字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field BoxParentId = FindByName(__.BoxParentId);

            ///<summary></summary>
            public static readonly Field BoxChildId = FindByName(__.BoxChildId);

            ///<summary></summary>
            public static readonly Field BoxName = FindByName(__.BoxName);

            ///<summary></summary>
            public static readonly Field UserId = FindByName(__.UserId);

            ///<summary></summary>
            public static readonly Field UserName = FindByName(__.UserName);

            ///<summary></summary>
            public static readonly Field IcNo = FindByName(__.IcNo);

            ///<summary></summary>
            public static readonly Field CardNo = FindByName(__.CardNo);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbBoxicpower字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String BoxParentId = "BoxParentId";

            ///<summary></summary>
            public const String BoxChildId = "BoxChildId";

            ///<summary></summary>
            public const String BoxName = "BoxName";

            ///<summary></summary>
            public const String UserId = "UserId";

            ///<summary></summary>
            public const String UserName = "UserName";

            ///<summary></summary>
            public const String IcNo = "IcNo";

            ///<summary></summary>
            public const String CardNo = "CardNo";

        }
        #endregion
    }

    /// <summary>TbBoxicpower接口</summary>
    /// <remarks></remarks>
    public partial interface ITbBoxicpower
    {
        #region 属性
        /// <summary></summary>
        Int64 ID { get; set; }

        /// <summary></summary>
        String BoxParentId { get; set; }

        /// <summary></summary>
        String BoxChildId { get; set; }

        /// <summary></summary>
        String BoxName { get; set; }

        /// <summary></summary>
        String UserId { get; set; }

        /// <summary></summary>
        String UserName { get; set; }

        /// <summary></summary>
        String IcNo { get; set; }

        /// <summary></summary>
        String CardNo { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbSystemuser</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_SystemUser", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbSystemuser : ITbSystemuser
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

        private String _UsersName;
        /// <summary></summary>
        [DisplayName("UsersName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UsersName", "", "varchar(50)")]
        public virtual String UsersName
        {
            get { return _UsersName; }
            set { if (OnPropertyChanging(__.UsersName, value)) { _UsersName = value; OnPropertyChanged(__.UsersName); } }
        }

        private String _UsersPassWord;
        /// <summary></summary>
        [DisplayName("UsersPassWord")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UsersPassWord", "", "varchar(50)")]
        public virtual String UsersPassWord
        {
            get { return _UsersPassWord; }
            set { if (OnPropertyChanging(__.UsersPassWord, value)) { _UsersPassWord = value; OnPropertyChanged(__.UsersPassWord); } }
        }

        private String _UsersPower;
        /// <summary></summary>
        [DisplayName("UsersPower")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UsersPower", "", "varchar(50)")]
        public virtual String UsersPower
        {
            get { return _UsersPower; }
            set { if (OnPropertyChanging(__.UsersPower, value)) { _UsersPower = value; OnPropertyChanged(__.UsersPower); } }
        }

        private String _UsersRemark;
        /// <summary></summary>
        [DisplayName("UsersRemark")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UsersRemark", "", "varchar(50)")]
        public virtual String UsersRemark
        {
            get { return _UsersRemark; }
            set { if (OnPropertyChanging(__.UsersRemark, value)) { _UsersRemark = value; OnPropertyChanged(__.UsersRemark); } }
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
                    case __.UsersName : return _UsersName;
                    case __.UsersPassWord : return _UsersPassWord;
                    case __.UsersPower : return _UsersPower;
                    case __.UsersRemark : return _UsersRemark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.UsersName : _UsersName = Convert.ToString(value); break;
                    case __.UsersPassWord : _UsersPassWord = Convert.ToString(value); break;
                    case __.UsersPower : _UsersPower = Convert.ToString(value); break;
                    case __.UsersRemark : _UsersRemark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbSystemuser字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field UsersName = FindByName(__.UsersName);

            ///<summary></summary>
            public static readonly Field UsersPassWord = FindByName(__.UsersPassWord);

            ///<summary></summary>
            public static readonly Field UsersPower = FindByName(__.UsersPower);

            ///<summary></summary>
            public static readonly Field UsersRemark = FindByName(__.UsersRemark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbSystemuser字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String UsersName = "UsersName";

            ///<summary></summary>
            public const String UsersPassWord = "UsersPassWord";

            ///<summary></summary>
            public const String UsersPower = "UsersPower";

            ///<summary></summary>
            public const String UsersRemark = "UsersRemark";

        }
        #endregion
    }

    /// <summary>TbSystemuser接口</summary>
    /// <remarks></remarks>
    public partial interface ITbSystemuser
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String UsersName { get; set; }

        /// <summary></summary>
        String UsersPassWord { get; set; }

        /// <summary></summary>
        String UsersPower { get; set; }

        /// <summary></summary>
        String UsersRemark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
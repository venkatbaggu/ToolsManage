﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbDooruser</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_DoorUser", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbDooruser : ITbDooruser
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

        private String _IsGroup;
        /// <summary></summary>
        [DisplayName("IsGroup")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("IsGroup", "", "varchar(50)")]
        public virtual String IsGroup
        {
            get { return _IsGroup; }
            set { if (OnPropertyChanging(__.IsGroup, value)) { _IsGroup = value; OnPropertyChanged(__.IsGroup); } }
        }

        private String _GroupName;
        /// <summary></summary>
        [DisplayName("GroupName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("GroupName", "", "varchar(50)")]
        public virtual String GroupName
        {
            get { return _GroupName; }
            set { if (OnPropertyChanging(__.GroupName, value)) { _GroupName = value; OnPropertyChanged(__.GroupName); } }
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

        private String _DoorNo1;
        /// <summary></summary>
        [DisplayName("DoorNo1")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("DoorNo1", "", "varchar(20)")]
        public virtual String DoorNo1
        {
            get { return _DoorNo1; }
            set { if (OnPropertyChanging(__.DoorNo1, value)) { _DoorNo1 = value; OnPropertyChanged(__.DoorNo1); } }
        }

        private String _DoorNo2;
        /// <summary></summary>
        [DisplayName("DoorNo2")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("DoorNo2", "", "varchar(20)")]
        public virtual String DoorNo2
        {
            get { return _DoorNo2; }
            set { if (OnPropertyChanging(__.DoorNo2, value)) { _DoorNo2 = value; OnPropertyChanged(__.DoorNo2); } }
        }

        private String _DoorNo3;
        /// <summary></summary>
        [DisplayName("DoorNo3")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("DoorNo3", "", "varchar(20)")]
        public virtual String DoorNo3
        {
            get { return _DoorNo3; }
            set { if (OnPropertyChanging(__.DoorNo3, value)) { _DoorNo3 = value; OnPropertyChanged(__.DoorNo3); } }
        }

        private String _DoorNo4;
        /// <summary></summary>
        [DisplayName("DoorNo4")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("DoorNo4", "", "varchar(20)")]
        public virtual String DoorNo4
        {
            get { return _DoorNo4; }
            set { if (OnPropertyChanging(__.DoorNo4, value)) { _DoorNo4 = value; OnPropertyChanged(__.DoorNo4); } }
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

        private String _IsPower;
        /// <summary></summary>
        [DisplayName("IsPower")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("IsPower", "", "varchar(50)")]
        public virtual String IsPower
        {
            get { return _IsPower; }
            set { if (OnPropertyChanging(__.IsPower, value)) { _IsPower = value; OnPropertyChanged(__.IsPower); } }
        }

        private String _PowerTime;
        /// <summary></summary>
        [DisplayName("PowerTime")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PowerTime", "", "varchar(50)")]
        public virtual String PowerTime
        {
            get { return _PowerTime; }
            set { if (OnPropertyChanging(__.PowerTime, value)) { _PowerTime = value; OnPropertyChanged(__.PowerTime); } }
        }

        private String _Finger1;
        /// <summary></summary>
        [DisplayName("Finger1")]
        [Description("")]
        [DataObjectField(false, false, true, 3000)]
        [BindColumn("Finger1", "", "varchar(3000)")]
        public virtual String Finger1
        {
            get { return _Finger1; }
            set { if (OnPropertyChanging(__.Finger1, value)) { _Finger1 = value; OnPropertyChanged(__.Finger1); } }
        }

        private String _Finger2;
        /// <summary></summary>
        [DisplayName("Finger2")]
        [Description("")]
        [DataObjectField(false, false, true, 3000)]
        [BindColumn("Finger2", "", "varchar(3000)")]
        public virtual String Finger2
        {
            get { return _Finger2; }
            set { if (OnPropertyChanging(__.Finger2, value)) { _Finger2 = value; OnPropertyChanged(__.Finger2); } }
        }

        private String _Finger3;
        /// <summary></summary>
        [DisplayName("Finger3")]
        [Description("")]
        [DataObjectField(false, false, true, 3000)]
        [BindColumn("Finger3", "", "varchar(3000)")]
        public virtual String Finger3
        {
            get { return _Finger3; }
            set { if (OnPropertyChanging(__.Finger3, value)) { _Finger3 = value; OnPropertyChanged(__.Finger3); } }
        }

        private String _UserId;
        /// <summary></summary>
        [DisplayName("UserId")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("UserId", "", "varchar(10)")]
        public virtual String UserId
        {
            get { return _UserId; }
            set { if (OnPropertyChanging(__.UserId, value)) { _UserId = value; OnPropertyChanged(__.UserId); } }
        }

        private String _PassWord;
        /// <summary></summary>
        [DisplayName("PassWord")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("PassWord", "", "varchar(10)")]
        public virtual String PassWord
        {
            get { return _PassWord; }
            set { if (OnPropertyChanging(__.PassWord, value)) { _PassWord = value; OnPropertyChanged(__.PassWord); } }
        }

        private String _PowerType;
        /// <summary></summary>
        [DisplayName("PowerType")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("PowerType", "", "varchar(10)")]
        public virtual String PowerType
        {
            get { return _PowerType; }
            set { if (OnPropertyChanging(__.PowerType, value)) { _PowerType = value; OnPropertyChanged(__.PowerType); } }
        }

        private Int32 _PowerFlag;
        /// <summary></summary>
        [DisplayName("PowerFlag")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("PowerFlag", "", "int")]
        public virtual Int32 PowerFlag
        {
            get { return _PowerFlag; }
            set { if (OnPropertyChanging(__.PowerFlag, value)) { _PowerFlag = value; OnPropertyChanged(__.PowerFlag); } }
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
                    case __.IsGroup : return _IsGroup;
                    case __.GroupName : return _GroupName;
                    case __.UserName : return _UserName;
                    case __.DoorNo1 : return _DoorNo1;
                    case __.DoorNo2 : return _DoorNo2;
                    case __.DoorNo3 : return _DoorNo3;
                    case __.DoorNo4 : return _DoorNo4;
                    case __.IcNo : return _IcNo;
                    case __.CardNo : return _CardNo;
                    case __.IsPower : return _IsPower;
                    case __.PowerTime : return _PowerTime;
                    case __.Finger1 : return _Finger1;
                    case __.Finger2 : return _Finger2;
                    case __.Finger3 : return _Finger3;
                    case __.UserId : return _UserId;
                    case __.PassWord : return _PassWord;
                    case __.PowerType : return _PowerType;
                    case __.PowerFlag : return _PowerFlag;
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
                    case __.IsGroup : _IsGroup = Convert.ToString(value); break;
                    case __.GroupName : _GroupName = Convert.ToString(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.DoorNo1 : _DoorNo1 = Convert.ToString(value); break;
                    case __.DoorNo2 : _DoorNo2 = Convert.ToString(value); break;
                    case __.DoorNo3 : _DoorNo3 = Convert.ToString(value); break;
                    case __.DoorNo4 : _DoorNo4 = Convert.ToString(value); break;
                    case __.IcNo : _IcNo = Convert.ToString(value); break;
                    case __.CardNo : _CardNo = Convert.ToString(value); break;
                    case __.IsPower : _IsPower = Convert.ToString(value); break;
                    case __.PowerTime : _PowerTime = Convert.ToString(value); break;
                    case __.Finger1 : _Finger1 = Convert.ToString(value); break;
                    case __.Finger2 : _Finger2 = Convert.ToString(value); break;
                    case __.Finger3 : _Finger3 = Convert.ToString(value); break;
                    case __.UserId : _UserId = Convert.ToString(value); break;
                    case __.PassWord : _PassWord = Convert.ToString(value); break;
                    case __.PowerType : _PowerType = Convert.ToString(value); break;
                    case __.PowerFlag : _PowerFlag = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbDooruser字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field tvParent = FindByName(__.tvParent);

            ///<summary></summary>
            public static readonly Field tvChildId = FindByName(__.tvChildId);

            ///<summary></summary>
            public static readonly Field IsGroup = FindByName(__.IsGroup);

            ///<summary></summary>
            public static readonly Field GroupName = FindByName(__.GroupName);

            ///<summary></summary>
            public static readonly Field UserName = FindByName(__.UserName);

            ///<summary></summary>
            public static readonly Field DoorNo1 = FindByName(__.DoorNo1);

            ///<summary></summary>
            public static readonly Field DoorNo2 = FindByName(__.DoorNo2);

            ///<summary></summary>
            public static readonly Field DoorNo3 = FindByName(__.DoorNo3);

            ///<summary></summary>
            public static readonly Field DoorNo4 = FindByName(__.DoorNo4);

            ///<summary></summary>
            public static readonly Field IcNo = FindByName(__.IcNo);

            ///<summary></summary>
            public static readonly Field CardNo = FindByName(__.CardNo);

            ///<summary></summary>
            public static readonly Field IsPower = FindByName(__.IsPower);

            ///<summary></summary>
            public static readonly Field PowerTime = FindByName(__.PowerTime);

            ///<summary></summary>
            public static readonly Field Finger1 = FindByName(__.Finger1);

            ///<summary></summary>
            public static readonly Field Finger2 = FindByName(__.Finger2);

            ///<summary></summary>
            public static readonly Field Finger3 = FindByName(__.Finger3);

            ///<summary></summary>
            public static readonly Field UserId = FindByName(__.UserId);

            ///<summary></summary>
            public static readonly Field PassWord = FindByName(__.PassWord);

            ///<summary></summary>
            public static readonly Field PowerType = FindByName(__.PowerType);

            ///<summary></summary>
            public static readonly Field PowerFlag = FindByName(__.PowerFlag);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbDooruser字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String tvParent = "tvParent";

            ///<summary></summary>
            public const String tvChildId = "tvChildId";

            ///<summary></summary>
            public const String IsGroup = "IsGroup";

            ///<summary></summary>
            public const String GroupName = "GroupName";

            ///<summary></summary>
            public const String UserName = "UserName";

            ///<summary></summary>
            public const String DoorNo1 = "DoorNo1";

            ///<summary></summary>
            public const String DoorNo2 = "DoorNo2";

            ///<summary></summary>
            public const String DoorNo3 = "DoorNo3";

            ///<summary></summary>
            public const String DoorNo4 = "DoorNo4";

            ///<summary></summary>
            public const String IcNo = "IcNo";

            ///<summary></summary>
            public const String CardNo = "CardNo";

            ///<summary></summary>
            public const String IsPower = "IsPower";

            ///<summary></summary>
            public const String PowerTime = "PowerTime";

            ///<summary></summary>
            public const String Finger1 = "Finger1";

            ///<summary></summary>
            public const String Finger2 = "Finger2";

            ///<summary></summary>
            public const String Finger3 = "Finger3";

            ///<summary></summary>
            public const String UserId = "UserId";

            ///<summary></summary>
            public const String PassWord = "PassWord";

            ///<summary></summary>
            public const String PowerType = "PowerType";

            ///<summary></summary>
            public const String PowerFlag = "PowerFlag";

        }
        #endregion
    }

    /// <summary>TbDooruser接口</summary>
    /// <remarks></remarks>
    public partial interface ITbDooruser
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String tvParent { get; set; }

        /// <summary></summary>
        String tvChildId { get; set; }

        /// <summary></summary>
        String IsGroup { get; set; }

        /// <summary></summary>
        String GroupName { get; set; }

        /// <summary></summary>
        String UserName { get; set; }

        /// <summary></summary>
        String DoorNo1 { get; set; }

        /// <summary></summary>
        String DoorNo2 { get; set; }

        /// <summary></summary>
        String DoorNo3 { get; set; }

        /// <summary></summary>
        String DoorNo4 { get; set; }

        /// <summary></summary>
        String IcNo { get; set; }

        /// <summary></summary>
        String CardNo { get; set; }

        /// <summary></summary>
        String IsPower { get; set; }

        /// <summary></summary>
        String PowerTime { get; set; }

        /// <summary></summary>
        String Finger1 { get; set; }

        /// <summary></summary>
        String Finger2 { get; set; }

        /// <summary></summary>
        String Finger3 { get; set; }

        /// <summary></summary>
        String UserId { get; set; }

        /// <summary></summary>
        String PassWord { get; set; }

        /// <summary></summary>
        String PowerType { get; set; }

        /// <summary></summary>
        Int32 PowerFlag { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
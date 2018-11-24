﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ToolsManage.Domain
{
    /// <summary>TbSysdevice</summary>
    /// <remarks></remarks>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindTable("tb_SysDevice", Description = "", ConnName = "ToolsManage", DbType = DatabaseType.SqlServer)]
    public partial class TbSysdevice : ITbSysdevice
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

        private String _DoorUsing;
        /// <summary></summary>
        [DisplayName("DoorUsing")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("DoorUsing", "", "varchar(10)")]
        public virtual String DoorUsing
        {
            get { return _DoorUsing; }
            set { if (OnPropertyChanging(__.DoorUsing, value)) { _DoorUsing = value; OnPropertyChanged(__.DoorUsing); } }
        }

        private String _DoorIP;
        /// <summary></summary>
        [DisplayName("DoorIP")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorIP", "", "varchar(50)")]
        public virtual String DoorIP
        {
            get { return _DoorIP; }
            set { if (OnPropertyChanging(__.DoorIP, value)) { _DoorIP = value; OnPropertyChanged(__.DoorIP); } }
        }

        private String _DoorSN;
        /// <summary></summary>
        [DisplayName("DoorSN")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorSN", "", "varchar(50)")]
        public virtual String DoorSN
        {
            get { return _DoorSN; }
            set { if (OnPropertyChanging(__.DoorSN, value)) { _DoorSN = value; OnPropertyChanged(__.DoorSN); } }
        }

        private String _DoorCount;
        /// <summary></summary>
        [DisplayName("DoorCount")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorCount", "", "varchar(50)")]
        public virtual String DoorCount
        {
            get { return _DoorCount; }
            set { if (OnPropertyChanging(__.DoorCount, value)) { _DoorCount = value; OnPropertyChanged(__.DoorCount); } }
        }

        private String _DoorName1;
        /// <summary></summary>
        [DisplayName("DoorName1")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorName1", "", "varchar(50)")]
        public virtual String DoorName1
        {
            get { return _DoorName1; }
            set { if (OnPropertyChanging(__.DoorName1, value)) { _DoorName1 = value; OnPropertyChanged(__.DoorName1); } }
        }

        private String _DoorName2;
        /// <summary></summary>
        [DisplayName("DoorName2")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DoorName2", "", "varchar(50)")]
        public virtual String DoorName2
        {
            get { return _DoorName2; }
            set { if (OnPropertyChanging(__.DoorName2, value)) { _DoorName2 = value; OnPropertyChanged(__.DoorName2); } }
        }

        private String _RfidUsing1;
        /// <summary></summary>
        [DisplayName("RfidUsing1")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("RfidUsing1", "", "varchar(10)")]
        public virtual String RfidUsing1
        {
            get { return _RfidUsing1; }
            set { if (OnPropertyChanging(__.RfidUsing1, value)) { _RfidUsing1 = value; OnPropertyChanged(__.RfidUsing1); } }
        }

        private String _RfidUsing2;
        /// <summary></summary>
        [DisplayName("RfidUsing2")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("RfidUsing2", "", "varchar(10)")]
        public virtual String RfidUsing2
        {
            get { return _RfidUsing2; }
            set { if (OnPropertyChanging(__.RfidUsing2, value)) { _RfidUsing2 = value; OnPropertyChanged(__.RfidUsing2); } }
        }

        private String _RfidIp1;
        /// <summary></summary>
        [DisplayName("RfidIp1")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RfidIp1", "", "varchar(50)")]
        public virtual String RfidIp1
        {
            get { return _RfidIp1; }
            set { if (OnPropertyChanging(__.RfidIp1, value)) { _RfidIp1 = value; OnPropertyChanged(__.RfidIp1); } }
        }

        private String _RfidPort1;
        /// <summary></summary>
        [DisplayName("RfidPort1")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RfidPort1", "", "varchar(50)")]
        public virtual String RfidPort1
        {
            get { return _RfidPort1; }
            set { if (OnPropertyChanging(__.RfidPort1, value)) { _RfidPort1 = value; OnPropertyChanged(__.RfidPort1); } }
        }

        private String _RfidIp2;
        /// <summary></summary>
        [DisplayName("RfidIp2")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RfidIp2", "", "varchar(50)")]
        public virtual String RfidIp2
        {
            get { return _RfidIp2; }
            set { if (OnPropertyChanging(__.RfidIp2, value)) { _RfidIp2 = value; OnPropertyChanged(__.RfidIp2); } }
        }

        private String _RfidPort2;
        /// <summary></summary>
        [DisplayName("RfidPort2")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RfidPort2", "", "varchar(50)")]
        public virtual String RfidPort2
        {
            get { return _RfidPort2; }
            set { if (OnPropertyChanging(__.RfidPort2, value)) { _RfidPort2 = value; OnPropertyChanged(__.RfidPort2); } }
        }

        private String _CameraIp;
        /// <summary></summary>
        [DisplayName("CameraIp")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CameraIp", "", "varchar(50)")]
        public virtual String CameraIp
        {
            get { return _CameraIp; }
            set { if (OnPropertyChanging(__.CameraIp, value)) { _CameraIp = value; OnPropertyChanged(__.CameraIp); } }
        }

        private String _CameraPort;
        /// <summary></summary>
        [DisplayName("CameraPort")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CameraPort", "", "varchar(50)")]
        public virtual String CameraPort
        {
            get { return _CameraPort; }
            set { if (OnPropertyChanging(__.CameraPort, value)) { _CameraPort = value; OnPropertyChanged(__.CameraPort); } }
        }

        private String _CameraUser;
        /// <summary></summary>
        [DisplayName("CameraUser")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CameraUser", "", "varchar(50)")]
        public virtual String CameraUser
        {
            get { return _CameraUser; }
            set { if (OnPropertyChanging(__.CameraUser, value)) { _CameraUser = value; OnPropertyChanged(__.CameraUser); } }
        }

        private String _CameraPsw;
        /// <summary></summary>
        [DisplayName("CameraPsw")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CameraPsw", "", "varchar(50)")]
        public virtual String CameraPsw
        {
            get { return _CameraPsw; }
            set { if (OnPropertyChanging(__.CameraPsw, value)) { _CameraPsw = value; OnPropertyChanged(__.CameraPsw); } }
        }

        private String _HotRun;
        /// <summary></summary>
        [DisplayName("HotRun")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("HotRun", "", "varchar(5)")]
        public virtual String HotRun
        {
            get { return _HotRun; }
            set { if (OnPropertyChanging(__.HotRun, value)) { _HotRun = value; OnPropertyChanged(__.HotRun); } }
        }

        private String _HotStop;
        /// <summary></summary>
        [DisplayName("HotStop")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("HotStop", "", "varchar(5)")]
        public virtual String HotStop
        {
            get { return _HotStop; }
            set { if (OnPropertyChanging(__.HotStop, value)) { _HotStop = value; OnPropertyChanged(__.HotStop); } }
        }

        private String _HumiRun;
        /// <summary></summary>
        [DisplayName("HumiRun")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("HumiRun", "", "varchar(5)")]
        public virtual String HumiRun
        {
            get { return _HumiRun; }
            set { if (OnPropertyChanging(__.HumiRun, value)) { _HumiRun = value; OnPropertyChanged(__.HumiRun); } }
        }

        private String _HumiStop;
        /// <summary></summary>
        [DisplayName("HumiStop")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("HumiStop", "", "varchar(5)")]
        public virtual String HumiStop
        {
            get { return _HumiStop; }
            set { if (OnPropertyChanging(__.HumiStop, value)) { _HumiStop = value; OnPropertyChanged(__.HumiStop); } }
        }

        private String _HandKeepTime;
        /// <summary></summary>
        [DisplayName("HandKeepTime")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("HandKeepTime", "", "varchar(5)")]
        public virtual String HandKeepTime
        {
            get { return _HandKeepTime; }
            set { if (OnPropertyChanging(__.HandKeepTime, value)) { _HandKeepTime = value; OnPropertyChanged(__.HandKeepTime); } }
        }

        private String _OverDayBorr;
        /// <summary></summary>
        [DisplayName("OverDayBorr")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("OverDayBorr", "", "varchar(5)")]
        public virtual String OverDayBorr
        {
            get { return _OverDayBorr; }
            set { if (OnPropertyChanging(__.OverDayBorr, value)) { _OverDayBorr = value; OnPropertyChanged(__.OverDayBorr); } }
        }

        private String _BorrRetSpan;
        /// <summary></summary>
        [DisplayName("BorrRetSpan")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("BorrRetSpan", "", "varchar(5)")]
        public virtual String BorrRetSpan
        {
            get { return _BorrRetSpan; }
            set { if (OnPropertyChanging(__.BorrRetSpan, value)) { _BorrRetSpan = value; OnPropertyChanged(__.BorrRetSpan); } }
        }

        private String _AirCoolRun;
        /// <summary></summary>
        [DisplayName("AirCoolRun")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirCoolRun", "", "varchar(5)")]
        public virtual String AirCoolRun
        {
            get { return _AirCoolRun; }
            set { if (OnPropertyChanging(__.AirCoolRun, value)) { _AirCoolRun = value; OnPropertyChanged(__.AirCoolRun); } }
        }

        private String _AirCoolStop;
        /// <summary></summary>
        [DisplayName("AirCoolStop")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirCoolStop", "", "varchar(5)")]
        public virtual String AirCoolStop
        {
            get { return _AirCoolStop; }
            set { if (OnPropertyChanging(__.AirCoolStop, value)) { _AirCoolStop = value; OnPropertyChanged(__.AirCoolStop); } }
        }

        private String _AirCoolTempSet;
        /// <summary></summary>
        [DisplayName("AirCoolTempSet")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirCoolTempSet", "", "varchar(5)")]
        public virtual String AirCoolTempSet
        {
            get { return _AirCoolTempSet; }
            set { if (OnPropertyChanging(__.AirCoolTempSet, value)) { _AirCoolTempSet = value; OnPropertyChanged(__.AirCoolTempSet); } }
        }

        private String _AirHotRun;
        /// <summary></summary>
        [DisplayName("AirHotRun")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirHotRun", "", "varchar(5)")]
        public virtual String AirHotRun
        {
            get { return _AirHotRun; }
            set { if (OnPropertyChanging(__.AirHotRun, value)) { _AirHotRun = value; OnPropertyChanged(__.AirHotRun); } }
        }

        private String _AirHotStop;
        /// <summary></summary>
        [DisplayName("AirHotStop")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirHotStop", "", "varchar(5)")]
        public virtual String AirHotStop
        {
            get { return _AirHotStop; }
            set { if (OnPropertyChanging(__.AirHotStop, value)) { _AirHotStop = value; OnPropertyChanged(__.AirHotStop); } }
        }

        private String _AirHotTempSet;
        /// <summary></summary>
        [DisplayName("AirHotTempSet")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("AirHotTempSet", "", "varchar(5)")]
        public virtual String AirHotTempSet
        {
            get { return _AirHotTempSet; }
            set { if (OnPropertyChanging(__.AirHotTempSet, value)) { _AirHotTempSet = value; OnPropertyChanged(__.AirHotTempSet); } }
        }

        private String _ServerAddr;
        /// <summary></summary>
        [DisplayName("ServerAddr")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("ServerAddr", "", "varchar(5)")]
        public virtual String ServerAddr
        {
            get { return _ServerAddr; }
            set { if (OnPropertyChanging(__.ServerAddr, value)) { _ServerAddr = value; OnPropertyChanged(__.ServerAddr); } }
        }

        private String _ServerUsing;
        /// <summary></summary>
        [DisplayName("ServerUsing")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("ServerUsing", "", "varchar(10)")]
        public virtual String ServerUsing
        {
            get { return _ServerUsing; }
            set { if (OnPropertyChanging(__.ServerUsing, value)) { _ServerUsing = value; OnPropertyChanged(__.ServerUsing); } }
        }

        private String _EnvirUsing;
        /// <summary></summary>
        [DisplayName("EnvirUsing")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("EnvirUsing", "", "varchar(10)")]
        public virtual String EnvirUsing
        {
            get { return _EnvirUsing; }
            set { if (OnPropertyChanging(__.EnvirUsing, value)) { _EnvirUsing = value; OnPropertyChanged(__.EnvirUsing); } }
        }

        private String _RfidBoxUsing;
        /// <summary></summary>
        [DisplayName("RfidBoxUsing")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("RfidBoxUsing", "", "varchar(10)")]
        public virtual String RfidBoxUsing
        {
            get { return _RfidBoxUsing; }
            set { if (OnPropertyChanging(__.RfidBoxUsing, value)) { _RfidBoxUsing = value; OnPropertyChanged(__.RfidBoxUsing); } }
        }

        private String _DoorType;
        /// <summary></summary>
        [DisplayName("DoorType")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("DoorType", "", "varchar(10)")]
        public virtual String DoorType
        {
            get { return _DoorType; }
            set { if (OnPropertyChanging(__.DoorType, value)) { _DoorType = value; OnPropertyChanged(__.DoorType); } }
        }

        private String _FingerDoorIp;
        /// <summary></summary>
        [DisplayName("FingerDoorIp")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("FingerDoorIp", "", "varchar(50)")]
        public virtual String FingerDoorIp
        {
            get { return _FingerDoorIp; }
            set { if (OnPropertyChanging(__.FingerDoorIp, value)) { _FingerDoorIp = value; OnPropertyChanged(__.FingerDoorIp); } }
        }

        private String _RfidBoxTime;
        /// <summary></summary>
        [DisplayName("RfidBoxTime")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("RfidBoxTime", "", "varchar(5)")]
        public virtual String RfidBoxTime
        {
            get { return _RfidBoxTime; }
            set { if (OnPropertyChanging(__.RfidBoxTime, value)) { _RfidBoxTime = value; OnPropertyChanged(__.RfidBoxTime); } }
        }

        private String _FingerPort;
        /// <summary></summary>
        [DisplayName("FingerPort")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("FingerPort", "", "varchar(5)")]
        public virtual String FingerPort
        {
            get { return _FingerPort; }
            set { if (OnPropertyChanging(__.FingerPort, value)) { _FingerPort = value; OnPropertyChanged(__.FingerPort); } }
        }

        private String _wgPort;
        /// <summary></summary>
        [DisplayName("wgPort")]
        [Description("")]
        [DataObjectField(false, false, true, 5)]
        [BindColumn("wgPort", "", "varchar(5)")]
        public virtual String wgPort
        {
            get { return _wgPort; }
            set { if (OnPropertyChanging(__.wgPort, value)) { _wgPort = value; OnPropertyChanged(__.wgPort); } }
        }

        private String _WgDoorName3;
        /// <summary></summary>
        [DisplayName("WgDoorName3")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("WgDoorName3", "", "varchar(20)")]
        public virtual String WgDoorName3
        {
            get { return _WgDoorName3; }
            set { if (OnPropertyChanging(__.WgDoorName3, value)) { _WgDoorName3 = value; OnPropertyChanged(__.WgDoorName3); } }
        }

        private String _WgDoorName4;
        /// <summary></summary>
        [DisplayName("WgDoorName4")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("WgDoorName4", "", "varchar(20)")]
        public virtual String WgDoorName4
        {
            get { return _WgDoorName4; }
            set { if (OnPropertyChanging(__.WgDoorName4, value)) { _WgDoorName4 = value; OnPropertyChanged(__.WgDoorName4); } }
        }

        private String _UsingFinger;
        /// <summary></summary>
        [DisplayName("UsingFinger")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("UsingFinger", "", "varchar(10)")]
        public virtual String UsingFinger
        {
            get { return _UsingFinger; }
            set { if (OnPropertyChanging(__.UsingFinger, value)) { _UsingFinger = value; OnPropertyChanged(__.UsingFinger); } }
        }

        private String _FingerDoorName;
        /// <summary></summary>
        [DisplayName("FingerDoorName")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("FingerDoorName", "", "varchar(20)")]
        public virtual String FingerDoorName
        {
            get { return _FingerDoorName; }
            set { if (OnPropertyChanging(__.FingerDoorName, value)) { _FingerDoorName = value; OnPropertyChanged(__.FingerDoorName); } }
        }

        private String _IcDoorOfRfid1;
        /// <summary></summary>
        [DisplayName("IcDoorOfRfid1")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("IcDoorOfRfid1", "", "varchar(10)")]
        public virtual String IcDoorOfRfid1
        {
            get { return _IcDoorOfRfid1; }
            set { if (OnPropertyChanging(__.IcDoorOfRfid1, value)) { _IcDoorOfRfid1 = value; OnPropertyChanged(__.IcDoorOfRfid1); } }
        }

        private String _IcDoorOfRfid2;
        /// <summary></summary>
        [DisplayName("IcDoorOfRfid2")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("IcDoorOfRfid2", "", "varchar(10)")]
        public virtual String IcDoorOfRfid2
        {
            get { return _IcDoorOfRfid2; }
            set { if (OnPropertyChanging(__.IcDoorOfRfid2, value)) { _IcDoorOfRfid2 = value; OnPropertyChanged(__.IcDoorOfRfid2); } }
        }

        private String _IcDoorOfRfid3;
        /// <summary></summary>
        [DisplayName("IcDoorOfRfid3")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("IcDoorOfRfid3", "", "varchar(10)")]
        public virtual String IcDoorOfRfid3
        {
            get { return _IcDoorOfRfid3; }
            set { if (OnPropertyChanging(__.IcDoorOfRfid3, value)) { _IcDoorOfRfid3 = value; OnPropertyChanged(__.IcDoorOfRfid3); } }
        }

        private String _IcDoorOfRfid4;
        /// <summary></summary>
        [DisplayName("IcDoorOfRfid4")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("IcDoorOfRfid4", "", "varchar(10)")]
        public virtual String IcDoorOfRfid4
        {
            get { return _IcDoorOfRfid4; }
            set { if (OnPropertyChanging(__.IcDoorOfRfid4, value)) { _IcDoorOfRfid4 = value; OnPropertyChanged(__.IcDoorOfRfid4); } }
        }

        private String _FingerDoorOfRfid;
        /// <summary></summary>
        [DisplayName("FingerDoorOfRfid")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("FingerDoorOfRfid", "", "varchar(10)")]
        public virtual String FingerDoorOfRfid
        {
            get { return _FingerDoorOfRfid; }
            set { if (OnPropertyChanging(__.FingerDoorOfRfid, value)) { _FingerDoorOfRfid = value; OnPropertyChanged(__.FingerDoorOfRfid); } }
        }

        private String _Rfid1No;
        /// <summary></summary>
        [DisplayName("Rfid1No")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("Rfid1No", "", "varchar(10)")]
        public virtual String Rfid1No
        {
            get { return _Rfid1No; }
            set { if (OnPropertyChanging(__.Rfid1No, value)) { _Rfid1No = value; OnPropertyChanged(__.Rfid1No); } }
        }

        private String _Rfid2No;
        /// <summary></summary>
        [DisplayName("Rfid2No")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("Rfid2No", "", "varchar(10)")]
        public virtual String Rfid2No
        {
            get { return _Rfid2No; }
            set { if (OnPropertyChanging(__.Rfid2No, value)) { _Rfid2No = value; OnPropertyChanged(__.Rfid2No); } }
        }

        private String _BorrOver;
        /// <summary></summary>
        [DisplayName("BorrOver")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("BorrOver", "", "varchar(10)")]
        public virtual String BorrOver
        {
            get { return _BorrOver; }
            set { if (OnPropertyChanging(__.BorrOver, value)) { _BorrOver = value; OnPropertyChanged(__.BorrOver); } }
        }

        private String _ErrInfo;
        /// <summary></summary>
        [DisplayName("ErrInfo")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("ErrInfo", "", "varchar(10)")]
        public virtual String ErrInfo
        {
            get { return _ErrInfo; }
            set { if (OnPropertyChanging(__.ErrInfo, value)) { _ErrInfo = value; OnPropertyChanged(__.ErrInfo); } }
        }

        private String _UsingFinger2;
        /// <summary></summary>
        [DisplayName("UsingFinger2")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("UsingFinger2", "", "varchar(10)")]
        public virtual String UsingFinger2
        {
            get { return _UsingFinger2; }
            set { if (OnPropertyChanging(__.UsingFinger2, value)) { _UsingFinger2 = value; OnPropertyChanged(__.UsingFinger2); } }
        }

        private String _FingerDoorIp2;
        /// <summary></summary>
        [DisplayName("FingerDoorIp2")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("FingerDoorIp2", "", "varchar(20)")]
        public virtual String FingerDoorIp2
        {
            get { return _FingerDoorIp2; }
            set { if (OnPropertyChanging(__.FingerDoorIp2, value)) { _FingerDoorIp2 = value; OnPropertyChanged(__.FingerDoorIp2); } }
        }

        private String _FingerPort2;
        /// <summary></summary>
        [DisplayName("FingerPort2")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("FingerPort2", "", "varchar(10)")]
        public virtual String FingerPort2
        {
            get { return _FingerPort2; }
            set { if (OnPropertyChanging(__.FingerPort2, value)) { _FingerPort2 = value; OnPropertyChanged(__.FingerPort2); } }
        }

        private String _FingerDoorName2;
        /// <summary></summary>
        [DisplayName("FingerDoorName2")]
        [Description("")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("FingerDoorName2", "", "varchar(20)")]
        public virtual String FingerDoorName2
        {
            get { return _FingerDoorName2; }
            set { if (OnPropertyChanging(__.FingerDoorName2, value)) { _FingerDoorName2 = value; OnPropertyChanged(__.FingerDoorName2); } }
        }

        private String _FingerDoorOfRfid2;
        /// <summary></summary>
        [DisplayName("FingerDoorOfRfid2")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn("FingerDoorOfRfid2", "", "varchar(10)")]
        public virtual String FingerDoorOfRfid2
        {
            get { return _FingerDoorOfRfid2; }
            set { if (OnPropertyChanging(__.FingerDoorOfRfid2, value)) { _FingerDoorOfRfid2 = value; OnPropertyChanged(__.FingerDoorOfRfid2); } }
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
                    case __.DoorUsing : return _DoorUsing;
                    case __.DoorIP : return _DoorIP;
                    case __.DoorSN : return _DoorSN;
                    case __.DoorCount : return _DoorCount;
                    case __.DoorName1 : return _DoorName1;
                    case __.DoorName2 : return _DoorName2;
                    case __.RfidUsing1 : return _RfidUsing1;
                    case __.RfidUsing2 : return _RfidUsing2;
                    case __.RfidIp1 : return _RfidIp1;
                    case __.RfidPort1 : return _RfidPort1;
                    case __.RfidIp2 : return _RfidIp2;
                    case __.RfidPort2 : return _RfidPort2;
                    case __.CameraIp : return _CameraIp;
                    case __.CameraPort : return _CameraPort;
                    case __.CameraUser : return _CameraUser;
                    case __.CameraPsw : return _CameraPsw;
                    case __.HotRun : return _HotRun;
                    case __.HotStop : return _HotStop;
                    case __.HumiRun : return _HumiRun;
                    case __.HumiStop : return _HumiStop;
                    case __.HandKeepTime : return _HandKeepTime;
                    case __.OverDayBorr : return _OverDayBorr;
                    case __.BorrRetSpan : return _BorrRetSpan;
                    case __.AirCoolRun : return _AirCoolRun;
                    case __.AirCoolStop : return _AirCoolStop;
                    case __.AirCoolTempSet : return _AirCoolTempSet;
                    case __.AirHotRun : return _AirHotRun;
                    case __.AirHotStop : return _AirHotStop;
                    case __.AirHotTempSet : return _AirHotTempSet;
                    case __.ServerAddr : return _ServerAddr;
                    case __.ServerUsing : return _ServerUsing;
                    case __.EnvirUsing : return _EnvirUsing;
                    case __.RfidBoxUsing : return _RfidBoxUsing;
                    case __.DoorType : return _DoorType;
                    case __.FingerDoorIp : return _FingerDoorIp;
                    case __.RfidBoxTime : return _RfidBoxTime;
                    case __.FingerPort : return _FingerPort;
                    case __.wgPort : return _wgPort;
                    case __.WgDoorName3 : return _WgDoorName3;
                    case __.WgDoorName4 : return _WgDoorName4;
                    case __.UsingFinger : return _UsingFinger;
                    case __.FingerDoorName : return _FingerDoorName;
                    case __.IcDoorOfRfid1 : return _IcDoorOfRfid1;
                    case __.IcDoorOfRfid2 : return _IcDoorOfRfid2;
                    case __.IcDoorOfRfid3 : return _IcDoorOfRfid3;
                    case __.IcDoorOfRfid4 : return _IcDoorOfRfid4;
                    case __.FingerDoorOfRfid : return _FingerDoorOfRfid;
                    case __.Rfid1No : return _Rfid1No;
                    case __.Rfid2No : return _Rfid2No;
                    case __.BorrOver : return _BorrOver;
                    case __.ErrInfo : return _ErrInfo;
                    case __.UsingFinger2 : return _UsingFinger2;
                    case __.FingerDoorIp2 : return _FingerDoorIp2;
                    case __.FingerPort2 : return _FingerPort2;
                    case __.FingerDoorName2 : return _FingerDoorName2;
                    case __.FingerDoorOfRfid2 : return _FingerDoorOfRfid2;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.DoorUsing : _DoorUsing = Convert.ToString(value); break;
                    case __.DoorIP : _DoorIP = Convert.ToString(value); break;
                    case __.DoorSN : _DoorSN = Convert.ToString(value); break;
                    case __.DoorCount : _DoorCount = Convert.ToString(value); break;
                    case __.DoorName1 : _DoorName1 = Convert.ToString(value); break;
                    case __.DoorName2 : _DoorName2 = Convert.ToString(value); break;
                    case __.RfidUsing1 : _RfidUsing1 = Convert.ToString(value); break;
                    case __.RfidUsing2 : _RfidUsing2 = Convert.ToString(value); break;
                    case __.RfidIp1 : _RfidIp1 = Convert.ToString(value); break;
                    case __.RfidPort1 : _RfidPort1 = Convert.ToString(value); break;
                    case __.RfidIp2 : _RfidIp2 = Convert.ToString(value); break;
                    case __.RfidPort2 : _RfidPort2 = Convert.ToString(value); break;
                    case __.CameraIp : _CameraIp = Convert.ToString(value); break;
                    case __.CameraPort : _CameraPort = Convert.ToString(value); break;
                    case __.CameraUser : _CameraUser = Convert.ToString(value); break;
                    case __.CameraPsw : _CameraPsw = Convert.ToString(value); break;
                    case __.HotRun : _HotRun = Convert.ToString(value); break;
                    case __.HotStop : _HotStop = Convert.ToString(value); break;
                    case __.HumiRun : _HumiRun = Convert.ToString(value); break;
                    case __.HumiStop : _HumiStop = Convert.ToString(value); break;
                    case __.HandKeepTime : _HandKeepTime = Convert.ToString(value); break;
                    case __.OverDayBorr : _OverDayBorr = Convert.ToString(value); break;
                    case __.BorrRetSpan : _BorrRetSpan = Convert.ToString(value); break;
                    case __.AirCoolRun : _AirCoolRun = Convert.ToString(value); break;
                    case __.AirCoolStop : _AirCoolStop = Convert.ToString(value); break;
                    case __.AirCoolTempSet : _AirCoolTempSet = Convert.ToString(value); break;
                    case __.AirHotRun : _AirHotRun = Convert.ToString(value); break;
                    case __.AirHotStop : _AirHotStop = Convert.ToString(value); break;
                    case __.AirHotTempSet : _AirHotTempSet = Convert.ToString(value); break;
                    case __.ServerAddr : _ServerAddr = Convert.ToString(value); break;
                    case __.ServerUsing : _ServerUsing = Convert.ToString(value); break;
                    case __.EnvirUsing : _EnvirUsing = Convert.ToString(value); break;
                    case __.RfidBoxUsing : _RfidBoxUsing = Convert.ToString(value); break;
                    case __.DoorType : _DoorType = Convert.ToString(value); break;
                    case __.FingerDoorIp : _FingerDoorIp = Convert.ToString(value); break;
                    case __.RfidBoxTime : _RfidBoxTime = Convert.ToString(value); break;
                    case __.FingerPort : _FingerPort = Convert.ToString(value); break;
                    case __.wgPort : _wgPort = Convert.ToString(value); break;
                    case __.WgDoorName3 : _WgDoorName3 = Convert.ToString(value); break;
                    case __.WgDoorName4 : _WgDoorName4 = Convert.ToString(value); break;
                    case __.UsingFinger : _UsingFinger = Convert.ToString(value); break;
                    case __.FingerDoorName : _FingerDoorName = Convert.ToString(value); break;
                    case __.IcDoorOfRfid1 : _IcDoorOfRfid1 = Convert.ToString(value); break;
                    case __.IcDoorOfRfid2 : _IcDoorOfRfid2 = Convert.ToString(value); break;
                    case __.IcDoorOfRfid3 : _IcDoorOfRfid3 = Convert.ToString(value); break;
                    case __.IcDoorOfRfid4 : _IcDoorOfRfid4 = Convert.ToString(value); break;
                    case __.FingerDoorOfRfid : _FingerDoorOfRfid = Convert.ToString(value); break;
                    case __.Rfid1No : _Rfid1No = Convert.ToString(value); break;
                    case __.Rfid2No : _Rfid2No = Convert.ToString(value); break;
                    case __.BorrOver : _BorrOver = Convert.ToString(value); break;
                    case __.ErrInfo : _ErrInfo = Convert.ToString(value); break;
                    case __.UsingFinger2 : _UsingFinger2 = Convert.ToString(value); break;
                    case __.FingerDoorIp2 : _FingerDoorIp2 = Convert.ToString(value); break;
                    case __.FingerPort2 : _FingerPort2 = Convert.ToString(value); break;
                    case __.FingerDoorName2 : _FingerDoorName2 = Convert.ToString(value); break;
                    case __.FingerDoorOfRfid2 : _FingerDoorOfRfid2 = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得TbSysdevice字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary></summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary></summary>
            public static readonly Field DoorUsing = FindByName(__.DoorUsing);

            ///<summary></summary>
            public static readonly Field DoorIP = FindByName(__.DoorIP);

            ///<summary></summary>
            public static readonly Field DoorSN = FindByName(__.DoorSN);

            ///<summary></summary>
            public static readonly Field DoorCount = FindByName(__.DoorCount);

            ///<summary></summary>
            public static readonly Field DoorName1 = FindByName(__.DoorName1);

            ///<summary></summary>
            public static readonly Field DoorName2 = FindByName(__.DoorName2);

            ///<summary></summary>
            public static readonly Field RfidUsing1 = FindByName(__.RfidUsing1);

            ///<summary></summary>
            public static readonly Field RfidUsing2 = FindByName(__.RfidUsing2);

            ///<summary></summary>
            public static readonly Field RfidIp1 = FindByName(__.RfidIp1);

            ///<summary></summary>
            public static readonly Field RfidPort1 = FindByName(__.RfidPort1);

            ///<summary></summary>
            public static readonly Field RfidIp2 = FindByName(__.RfidIp2);

            ///<summary></summary>
            public static readonly Field RfidPort2 = FindByName(__.RfidPort2);

            ///<summary></summary>
            public static readonly Field CameraIp = FindByName(__.CameraIp);

            ///<summary></summary>
            public static readonly Field CameraPort = FindByName(__.CameraPort);

            ///<summary></summary>
            public static readonly Field CameraUser = FindByName(__.CameraUser);

            ///<summary></summary>
            public static readonly Field CameraPsw = FindByName(__.CameraPsw);

            ///<summary></summary>
            public static readonly Field HotRun = FindByName(__.HotRun);

            ///<summary></summary>
            public static readonly Field HotStop = FindByName(__.HotStop);

            ///<summary></summary>
            public static readonly Field HumiRun = FindByName(__.HumiRun);

            ///<summary></summary>
            public static readonly Field HumiStop = FindByName(__.HumiStop);

            ///<summary></summary>
            public static readonly Field HandKeepTime = FindByName(__.HandKeepTime);

            ///<summary></summary>
            public static readonly Field OverDayBorr = FindByName(__.OverDayBorr);

            ///<summary></summary>
            public static readonly Field BorrRetSpan = FindByName(__.BorrRetSpan);

            ///<summary></summary>
            public static readonly Field AirCoolRun = FindByName(__.AirCoolRun);

            ///<summary></summary>
            public static readonly Field AirCoolStop = FindByName(__.AirCoolStop);

            ///<summary></summary>
            public static readonly Field AirCoolTempSet = FindByName(__.AirCoolTempSet);

            ///<summary></summary>
            public static readonly Field AirHotRun = FindByName(__.AirHotRun);

            ///<summary></summary>
            public static readonly Field AirHotStop = FindByName(__.AirHotStop);

            ///<summary></summary>
            public static readonly Field AirHotTempSet = FindByName(__.AirHotTempSet);

            ///<summary></summary>
            public static readonly Field ServerAddr = FindByName(__.ServerAddr);

            ///<summary></summary>
            public static readonly Field ServerUsing = FindByName(__.ServerUsing);

            ///<summary></summary>
            public static readonly Field EnvirUsing = FindByName(__.EnvirUsing);

            ///<summary></summary>
            public static readonly Field RfidBoxUsing = FindByName(__.RfidBoxUsing);

            ///<summary></summary>
            public static readonly Field DoorType = FindByName(__.DoorType);

            ///<summary></summary>
            public static readonly Field FingerDoorIp = FindByName(__.FingerDoorIp);

            ///<summary></summary>
            public static readonly Field RfidBoxTime = FindByName(__.RfidBoxTime);

            ///<summary></summary>
            public static readonly Field FingerPort = FindByName(__.FingerPort);

            ///<summary></summary>
            public static readonly Field wgPort = FindByName(__.wgPort);

            ///<summary></summary>
            public static readonly Field WgDoorName3 = FindByName(__.WgDoorName3);

            ///<summary></summary>
            public static readonly Field WgDoorName4 = FindByName(__.WgDoorName4);

            ///<summary></summary>
            public static readonly Field UsingFinger = FindByName(__.UsingFinger);

            ///<summary></summary>
            public static readonly Field FingerDoorName = FindByName(__.FingerDoorName);

            ///<summary></summary>
            public static readonly Field IcDoorOfRfid1 = FindByName(__.IcDoorOfRfid1);

            ///<summary></summary>
            public static readonly Field IcDoorOfRfid2 = FindByName(__.IcDoorOfRfid2);

            ///<summary></summary>
            public static readonly Field IcDoorOfRfid3 = FindByName(__.IcDoorOfRfid3);

            ///<summary></summary>
            public static readonly Field IcDoorOfRfid4 = FindByName(__.IcDoorOfRfid4);

            ///<summary></summary>
            public static readonly Field FingerDoorOfRfid = FindByName(__.FingerDoorOfRfid);

            ///<summary></summary>
            public static readonly Field Rfid1No = FindByName(__.Rfid1No);

            ///<summary></summary>
            public static readonly Field Rfid2No = FindByName(__.Rfid2No);

            ///<summary></summary>
            public static readonly Field BorrOver = FindByName(__.BorrOver);

            ///<summary></summary>
            public static readonly Field ErrInfo = FindByName(__.ErrInfo);

            ///<summary></summary>
            public static readonly Field UsingFinger2 = FindByName(__.UsingFinger2);

            ///<summary></summary>
            public static readonly Field FingerDoorIp2 = FindByName(__.FingerDoorIp2);

            ///<summary></summary>
            public static readonly Field FingerPort2 = FindByName(__.FingerPort2);

            ///<summary></summary>
            public static readonly Field FingerDoorName2 = FindByName(__.FingerDoorName2);

            ///<summary></summary>
            public static readonly Field FingerDoorOfRfid2 = FindByName(__.FingerDoorOfRfid2);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得TbSysdevice字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary></summary>
            public const String ID = "ID";

            ///<summary></summary>
            public const String DoorUsing = "DoorUsing";

            ///<summary></summary>
            public const String DoorIP = "DoorIP";

            ///<summary></summary>
            public const String DoorSN = "DoorSN";

            ///<summary></summary>
            public const String DoorCount = "DoorCount";

            ///<summary></summary>
            public const String DoorName1 = "DoorName1";

            ///<summary></summary>
            public const String DoorName2 = "DoorName2";

            ///<summary></summary>
            public const String RfidUsing1 = "RfidUsing1";

            ///<summary></summary>
            public const String RfidUsing2 = "RfidUsing2";

            ///<summary></summary>
            public const String RfidIp1 = "RfidIp1";

            ///<summary></summary>
            public const String RfidPort1 = "RfidPort1";

            ///<summary></summary>
            public const String RfidIp2 = "RfidIp2";

            ///<summary></summary>
            public const String RfidPort2 = "RfidPort2";

            ///<summary></summary>
            public const String CameraIp = "CameraIp";

            ///<summary></summary>
            public const String CameraPort = "CameraPort";

            ///<summary></summary>
            public const String CameraUser = "CameraUser";

            ///<summary></summary>
            public const String CameraPsw = "CameraPsw";

            ///<summary></summary>
            public const String HotRun = "HotRun";

            ///<summary></summary>
            public const String HotStop = "HotStop";

            ///<summary></summary>
            public const String HumiRun = "HumiRun";

            ///<summary></summary>
            public const String HumiStop = "HumiStop";

            ///<summary></summary>
            public const String HandKeepTime = "HandKeepTime";

            ///<summary></summary>
            public const String OverDayBorr = "OverDayBorr";

            ///<summary></summary>
            public const String BorrRetSpan = "BorrRetSpan";

            ///<summary></summary>
            public const String AirCoolRun = "AirCoolRun";

            ///<summary></summary>
            public const String AirCoolStop = "AirCoolStop";

            ///<summary></summary>
            public const String AirCoolTempSet = "AirCoolTempSet";

            ///<summary></summary>
            public const String AirHotRun = "AirHotRun";

            ///<summary></summary>
            public const String AirHotStop = "AirHotStop";

            ///<summary></summary>
            public const String AirHotTempSet = "AirHotTempSet";

            ///<summary></summary>
            public const String ServerAddr = "ServerAddr";

            ///<summary></summary>
            public const String ServerUsing = "ServerUsing";

            ///<summary></summary>
            public const String EnvirUsing = "EnvirUsing";

            ///<summary></summary>
            public const String RfidBoxUsing = "RfidBoxUsing";

            ///<summary></summary>
            public const String DoorType = "DoorType";

            ///<summary></summary>
            public const String FingerDoorIp = "FingerDoorIp";

            ///<summary></summary>
            public const String RfidBoxTime = "RfidBoxTime";

            ///<summary></summary>
            public const String FingerPort = "FingerPort";

            ///<summary></summary>
            public const String wgPort = "wgPort";

            ///<summary></summary>
            public const String WgDoorName3 = "WgDoorName3";

            ///<summary></summary>
            public const String WgDoorName4 = "WgDoorName4";

            ///<summary></summary>
            public const String UsingFinger = "UsingFinger";

            ///<summary></summary>
            public const String FingerDoorName = "FingerDoorName";

            ///<summary></summary>
            public const String IcDoorOfRfid1 = "IcDoorOfRfid1";

            ///<summary></summary>
            public const String IcDoorOfRfid2 = "IcDoorOfRfid2";

            ///<summary></summary>
            public const String IcDoorOfRfid3 = "IcDoorOfRfid3";

            ///<summary></summary>
            public const String IcDoorOfRfid4 = "IcDoorOfRfid4";

            ///<summary></summary>
            public const String FingerDoorOfRfid = "FingerDoorOfRfid";

            ///<summary></summary>
            public const String Rfid1No = "Rfid1No";

            ///<summary></summary>
            public const String Rfid2No = "Rfid2No";

            ///<summary></summary>
            public const String BorrOver = "BorrOver";

            ///<summary></summary>
            public const String ErrInfo = "ErrInfo";

            ///<summary></summary>
            public const String UsingFinger2 = "UsingFinger2";

            ///<summary></summary>
            public const String FingerDoorIp2 = "FingerDoorIp2";

            ///<summary></summary>
            public const String FingerPort2 = "FingerPort2";

            ///<summary></summary>
            public const String FingerDoorName2 = "FingerDoorName2";

            ///<summary></summary>
            public const String FingerDoorOfRfid2 = "FingerDoorOfRfid2";

        }
        #endregion
    }

    /// <summary>TbSysdevice接口</summary>
    /// <remarks></remarks>
    public partial interface ITbSysdevice
    {
        #region 属性
        /// <summary></summary>
        Int32 ID { get; set; }

        /// <summary></summary>
        String DoorUsing { get; set; }

        /// <summary></summary>
        String DoorIP { get; set; }

        /// <summary></summary>
        String DoorSN { get; set; }

        /// <summary></summary>
        String DoorCount { get; set; }

        /// <summary></summary>
        String DoorName1 { get; set; }

        /// <summary></summary>
        String DoorName2 { get; set; }

        /// <summary></summary>
        String RfidUsing1 { get; set; }

        /// <summary></summary>
        String RfidUsing2 { get; set; }

        /// <summary></summary>
        String RfidIp1 { get; set; }

        /// <summary></summary>
        String RfidPort1 { get; set; }

        /// <summary></summary>
        String RfidIp2 { get; set; }

        /// <summary></summary>
        String RfidPort2 { get; set; }

        /// <summary></summary>
        String CameraIp { get; set; }

        /// <summary></summary>
        String CameraPort { get; set; }

        /// <summary></summary>
        String CameraUser { get; set; }

        /// <summary></summary>
        String CameraPsw { get; set; }

        /// <summary></summary>
        String HotRun { get; set; }

        /// <summary></summary>
        String HotStop { get; set; }

        /// <summary></summary>
        String HumiRun { get; set; }

        /// <summary></summary>
        String HumiStop { get; set; }

        /// <summary></summary>
        String HandKeepTime { get; set; }

        /// <summary></summary>
        String OverDayBorr { get; set; }

        /// <summary></summary>
        String BorrRetSpan { get; set; }

        /// <summary></summary>
        String AirCoolRun { get; set; }

        /// <summary></summary>
        String AirCoolStop { get; set; }

        /// <summary></summary>
        String AirCoolTempSet { get; set; }

        /// <summary></summary>
        String AirHotRun { get; set; }

        /// <summary></summary>
        String AirHotStop { get; set; }

        /// <summary></summary>
        String AirHotTempSet { get; set; }

        /// <summary></summary>
        String ServerAddr { get; set; }

        /// <summary></summary>
        String ServerUsing { get; set; }

        /// <summary></summary>
        String EnvirUsing { get; set; }

        /// <summary></summary>
        String RfidBoxUsing { get; set; }

        /// <summary></summary>
        String DoorType { get; set; }

        /// <summary></summary>
        String FingerDoorIp { get; set; }

        /// <summary></summary>
        String RfidBoxTime { get; set; }

        /// <summary></summary>
        String FingerPort { get; set; }

        /// <summary></summary>
        String wgPort { get; set; }

        /// <summary></summary>
        String WgDoorName3 { get; set; }

        /// <summary></summary>
        String WgDoorName4 { get; set; }

        /// <summary></summary>
        String UsingFinger { get; set; }

        /// <summary></summary>
        String FingerDoorName { get; set; }

        /// <summary></summary>
        String IcDoorOfRfid1 { get; set; }

        /// <summary></summary>
        String IcDoorOfRfid2 { get; set; }

        /// <summary></summary>
        String IcDoorOfRfid3 { get; set; }

        /// <summary></summary>
        String IcDoorOfRfid4 { get; set; }

        /// <summary></summary>
        String FingerDoorOfRfid { get; set; }

        /// <summary></summary>
        String Rfid1No { get; set; }

        /// <summary></summary>
        String Rfid2No { get; set; }

        /// <summary></summary>
        String BorrOver { get; set; }

        /// <summary></summary>
        String ErrInfo { get; set; }

        /// <summary></summary>
        String UsingFinger2 { get; set; }

        /// <summary></summary>
        String FingerDoorIp2 { get; set; }

        /// <summary></summary>
        String FingerPort2 { get; set; }

        /// <summary></summary>
        String FingerDoorName2 { get; set; }

        /// <summary></summary>
        String FingerDoorOfRfid2 { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    #region

    #endregion

//try
//{

//}
//catch (Exception ex)
//{
//    if (frmMain.blDebug)
//        MessageUtil.ShowTips(ex.Message);
//} 

    class ToolInfo
    {
        private string strToolId;
        private string strToolType;
        private string strToolName;
        private string strRfidCode;
        private ToolsState toolState;
        private DateTime timeBorrRet;
        private string strPeopel;
        private string strLastActId = "";
        private string strParent;
        private string strStation;




        public ToolInfo(string strId,string strType,string strName,string strRfid,ToolsState tState,DateTime time,
                        string strPeo,string LastActId,string parent,string station)
        {
            strToolId = strId;
            strToolType = strType;
            strToolName = strName;
            strRfidCode = strRfid;
            toolState = tState;
            timeBorrRet = time;
            strPeopel = strPeo;
            strLastActId = LastActId;
            strParent = parent;
            strStation = station;
            //strNowActId = NowActId;
        }

        //public string StrNowActId
        //{
        //    get { return strNowActId; }
        //    set { strNowActId = value; }
        //}

        #region 属性

        /// <summary>
        /// 工具存放位置
        /// </summary>
        public string StrStation
        {
            get { return strStation; }
            set { strStation = value; }
        }

        public string StrParent
        {
            get { return strParent; }
            set { strParent = value; }
        }

        public string StrLastActId
        {
            get { return strLastActId; }
            set { strLastActId = value; }
        }

        public string StrPeopel
        {
            get { return strPeopel; }
            set { strPeopel = value; }
        }

        /// <summary>
        /// 工具借出归还时间
        /// </summary>
        public DateTime TimeBorrRet
        {
            get { return timeBorrRet; }
            set { timeBorrRet = value; }
        }

        public ToolsState ToolState
        {
            get { return toolState; }
            set { toolState = value; }
        }

        public string StrRfidCode
        {
            get { return strRfidCode; }
            set { strRfidCode = value; }
        }

        public string StrToolName
        {
            get { return strToolName; }
            set { strToolName = value; }
        }

        public string StrToolType
        {
            get { return strToolType; }
            set { strToolType = value; }
        }


        public string StrToolId
        {
            get { return strToolId; }
            set { strToolId = value; }
        }

        #endregion




    }


    #region  事件处理 及 参数

    #region   事件处理

    /// <summary>
    /// 验证通过 事件处理
    /// </summary>
    /// <param name="e"></param>
    public delegate void NewEventShowEventHandler(NewEventEventArgs e);


    #endregion

    #region  事件参数

    /// <summary>
    /// 新 事件参数
    /// </summary>
    public class NewEventEventArgs : EventArgs
    {
        public EventType eventType;
        public string strPoint;
        public string strContent;
        public string strPeople;
        public string strRemark;
        public DateTime timeAction;

        public NewEventEventArgs(EventType type, string point, string content, string people, string remark, DateTime time)
        {
            this.eventType = type;
            this.strPoint = point;
            this.strContent = content;
            this.strPeople = people;
            this.strRemark = remark;
            this.timeAction = time;
        }
    }

    #endregion

    #endregion

    #region  枚举

    #region

    #endregion

    #region 盘库语音

    /// <summary>
    /// 开启 关闭 盘库
    /// </summary>
    public enum OnOffMark : byte
    {
        开启 = 0x03,
        关闭 = 0x02
    }

    ///// <summary>
    ///// 语音内容
    ///// </summary>
    //public enum VoiceContent 
    //{
    //    您好,欢迎进入智能化工器具库房
    //}

    #endregion

    #region  IO板

    /// <summary>
    /// 继电器开关
    /// </summary>
    public enum OnOffRelay : byte
    {
        开启 = 0xff,
        关闭 = 0x00
    }

    /// <summary>
    /// 设备对应 继电器的哪一个
    /// </summary>
    public enum DeviceRelayNo : byte
    {
        烘干 = 0x00,
        除湿 = 0x01,
        新风 = 0x02,
        警灯 = 0x03,
        RFID = 0x04
    }

    #endregion

    #region 新风
    #endregion

    #region 空调

    /// <summary>
    /// 空调厂家类型
    /// </summary>
    public enum AirFactoryType
    {
        大金,
        其他
    }

    /// <summary>
    /// 空调运行模式  60H：送风、61H：制热、62H：制冷、63H：自动、67H：除湿
    /// </summary>
    public enum AirRunModel
    {
        送风,
        制热,
        制冷,
        除湿,
        自动
    }

    #endregion

    #region  大金空调

    /// <summary>
    /// 大金控制
    /// </summary>
    public enum DjControlType
    {
        开关 = 0x06,
        模式 = 0x08,
        温度 = 0x09
    }

    /// <summary>
    /// 大金控制开关
    /// </summary>
    public enum DjOnOff
    {
        开启 = 0x01,
        关闭 = 0x00
    }

    /// <summary>
    /// 大金控制模式
    /// </summary>
    public enum DjModel
    {
        制热 = 0x01,
        制冷 = 0x02
    }

    #endregion

    #region 非大金空调

    /// <summary>
    /// 其他空调启停
    /// </summary>
    public enum OtherAirOnOff : byte
    {
        开启 = 0xff,
        关闭 = 0x00
    }

    /// <summary>
    /// 其他空调模式类型
    /// </summary>
    public enum OtherAirModelType : byte
    {
        自动 = 0x00,
        制冷 = 0x01,
        除湿 = 0x02,
        送风 = 0x03,
        制热 = 0x04
    }

    /// <summary>
    /// 其他空调控制类型
    /// </summary>
    public enum OtherAirControlType : byte
    {
        厂家 = 0x02,
        开关 = 0x04,
        模式 = 0x05,
        温度 = 0x06
    }

    #endregion

    #region 变送器

    /// <summary>
    /// 变送器状态
    /// </summary>
    public enum ProbeState
    {
        正常,
        报警,
        不通讯,
        通信错误
    }

    #endregion

    #region  设备

    /// <summary>
    /// 重启RFID状态
    /// </summary>
    public enum StateRstRfid
    {
        重启,
        关闭,
        初值
    }

    /// <summary>
    /// 设备是否启用
    /// </summary>
    public enum DeviceUsing
    {
        未启用,
        启用
    }

    /// <summary>
    /// 设备运行状态
    /// </summary>
    public enum DeviceRunState
    {
        运行,
        停止
    }

    /// <summary>
    /// 设备运行模式 自动 手动 烟雾报警
    /// </summary>
    public enum DeviceRunModel
    {
        自动,
        手动,      
        烟雾报警,
        定时,
    }

    #endregion

    #region  通信

    /// <summary>
    ///  用于发送数据是 是否停止等待接收数据
    /// </summary>
    public enum IsWait
    {
        /// <summary>
        /// 只能等待
        /// </summary>
        OnlyWait,
        /// <summary>
        /// 可以停止
        /// </summary>
        CanStop
    }

    /// <summary>
    /// 通信 状态 类型
    /// </summary>
    public enum CommuniState : byte
    {
        正常 = 0x01,
        错误 = 0x02,
        无回复 = 0x03,
        已连接,
        已断开,
        未连接,
        初值
    }

    #endregion

    #region  语音

    /// <summary>
    /// 语音播放状态
    /// </summary>
    public enum VoiceState
    {
        播放,
        暂停,
        停止
    }

    /// <summary>
    /// 语音类容
    /// </summary>
    public enum VoiceContent
    {
        无语音播报,
        开门,
        工具借出,
        工具归还,
        工具外借超时
    }

    #endregion

    #region  系统

    /// <summary>
    /// 用户权限 显示
    /// </summary>
    public enum LoginBtnShow
    {
        系统登录,
        系统注销
    }

    /// <summary>
    /// 用户权限
    /// </summary>
    public enum UserPower
    {
        系统用户,
        普通用户,
        厂家,
        未注册
    }

    #endregion

    #region  门禁

    /// <summary>
    /// 微耕控制器 所控 门类型 大门 或工具柜门
    /// </summary>
    public enum WgDoorType
    {
        门禁,
        工具柜
    }

    /// <summary>
    /// 门禁用户表中的  班组和 人员的类型
    /// </summary>
    public enum GroupPeoType
    {
        班组,
        人员,
        未知
    }

    /// <summary>
    /// 门编号
    /// </summary>
    public enum DoorNo
    {
        门1,
        门2
    }

    /// <summary>
    /// 门数量
    /// </summary>
    public enum DoorCount
    {
        单门,
        双门,
        未设置
    }

    /// <summary>
    /// 门禁权限类型 
    /// </summary>
    public enum DoorPowerType
    {
        有权限,
        未授权,
        须更新
    }

    /// <summary>
    /// 门禁控制器类型 指纹 IC
    /// </summary>
    public enum DoorControlType
    {
        IC,
        指纹
    }

    /// <summary>
    /// 门的状态 
    /// </summary>
    public enum DoorsState
    {
        初值 = 0x00,
        开门 = 0x01,
        关门 = 0x02,
        不通信 = 0x03
    }

    /// <summary>
    /// wg开门类型 
    /// </summary>
    public enum WgOpenDoorType
    {
        刷卡开门,
        超级密码开门,
        //初值,
        //密码,
        //刷卡禁止通过
    }

    /// <summary>
    /// 开门类型 
    /// </summary>
    public enum OpenDoorType
    {
        初值,
        刷卡,
        密码,
        刷卡禁止通过,
        人脸,
        指纹
    }

    #endregion

    #region  工具

    /// <summary>
    /// 读到的EPC标签是否 已读取 即已处理
    /// </summary>
    public enum IsReadShow
    {
        已读,
        未读
    }

    /// <summary>
    /// Tools表中区域 类型 
    /// </summary>
    public enum ToolAreaType
    {
        区域,
        位置,
        工具柜,
        工具
    }

    /// <summary>
    /// 工具借还状态
    /// </summary>
    public enum ToolsState
    {
        在库 = 0x01,
        借出 = 0x02,
        外借超时,
        盘库错误
    }

    /// <summary>
    /// 工具借还
    /// </summary>
    public enum ToolsBorrRet
    {
        归还,
        借出,
        未知
    }

    #endregion

    #region  报警

    /// <summary>
    /// 报警类容
    /// </summary>
    public enum AlarmContent
    {
        报警,
        恢复正常,
        通信异常,
        通信恢复正常
    }

    /// <summary>
    /// 告警类型 
    /// </summary>
    public enum AlarmsType
    {
        烟雾,
        外借超时,
        标签欠压,
        半有源读写器通信,
        借还错误,
        工具放置位置错误,
        工具柜门禁,
        门禁,
        工具柜,
        RFID读写器通信错误
    }

    #endregion

    #region 查询记录

    /// <summary>
    /// 记录查询类型
    /// </summary>
    public enum QueryRecordType
    {
        全部
    }

    #endregion

    /// <summary>
    /// 异常事件 内容
    /// </summary>
    public enum ErrorContent
    {
        通信异常,
        定时器卡死,
        通信恢复正常,
        RFID读写器异常系统重启,
        初值
    }

    /// <summary>
    /// 事件内容
    /// </summary>
    public enum EventContent
    {
        初值,
        归还,
        开启,
        关闭,
        设置自动,
        设置制冷,
        设置除湿,
        设置送风,
        设置温度,
        设置制热
    }

    /// <summary>
    /// 事件类型 
    /// </summary>
    public enum EventType
    {
        初值,
        RFID读写器,
        烟雾,
        烘干,
        除湿,
        空调,
        新风,
        警灯,
        门禁,
        工具借还,
        工具柜,
        系统,
        标签欠压,
        借还错误,
        半有源读写器通信
    }

    /// <summary>
    /// 新事件是否读取
    /// </summary>
    public enum ServerIsRead
    {
        未读,
        已读
    }

    #region 其他

    /// <summary>
    /// 界面Tag类型 
    /// </summary>
    public enum TagType
    {
        添加,
        修改,
        初值
    }

    #endregion
    #region  RFID

    /// <summary>
    /// RFID天线
    /// </summary>
    public enum RfidAnt
    {
        天线1,
        天线2,
        天线3,
        天线4
    }

    #endregion
    #endregion
}

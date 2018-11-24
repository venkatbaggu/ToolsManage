using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

using ToolsManage.BaseClass.DoorClass;
using System.Threading.Tasks;

namespace ToolsManage.BaseClass
{
    public class clsZkDoor
    {
        public clsDoorInfo doorInfo = new clsDoorInfo();

        public zkemkeeper.CZKEMClass axCZKEM1;

        /// <summary>
        /// 机器号
        /// </summary>
        private int iMachineNumber = 1;

        #region  字段

        private string strIp;// 192.168.1.201 4370
        private int iPort = 0;
        bool blConnoct;
        private DateTime timeConnectLast=DateTime .Now .AddHours  (-2) ;
        private CommuniState stateOfNet = CommuniState.已连接;
        private string strNameOfZk;

        public string StrNameOfZk
        {
            get { return strNameOfZk; }
            set { strNameOfZk = value; }
        }

        public CommuniState StateOfNet
        {
            get { return stateOfNet; }
            set { stateOfNet = value; }
        }

        /// <summary>
        /// 上次 连接时间
        /// </summary>
        public DateTime TimeConnectLast
        {
            get { return timeConnectLast; }
            set { timeConnectLast = value; }
        }

        public bool BlConnoct
        {
            get { return blConnoct; }
            set { blConnoct = value; }
        }

        public string StrIp
        {
            get { return strIp; }
            set { strIp = value; }
        }

        public int IPort
        {
            get { return iPort; }
            set { iPort = value; }
        }

        #endregion

        #region  事件接口

        /// <summary>
        /// 验证通过事件
        /// </summary>
        public event VerifyOkEventHandler VerifyOkEvent;
        /// <summary>
        /// 开门事件
        /// </summary>
        public event OnDoorEventHandler OnDoorEvent;
        /// <summary>
        /// 报警事件
        /// </summary>
        public event OnAlarmEventHandler OnAlarmEvent;
        /// <summary>
        /// 断开连接 事件
        /// </summary>
        public event OnDisConnectEventHandler OnDisConnectEvent;

        #endregion

        public clsZkDoor()
        {
            if (axCZKEM1 == null)
                axCZKEM1 = new zkemkeeper.CZKEMClass();
        }

        #region 方法

        /// <summary>
        /// 清除 控制器类 及所控制的门
        /// </summary>
        public void Clear()
        {
            if (axCZKEM1 != null)
            {
                if (blConnoct)
                    DisConnect();
                axCZKEM1 = null;
            }
            if (doorInfo != null)
                doorInfo = null;
        }

        public bool DeleteUserInfo(string strId)
        {
            bool blRet = false;
            try
            {
                blRet = axCZKEM1.SSR_DeleteEnrollData(iMachineNumber, strId, 12);
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
            return blRet;
        }

        /// <summary>
        /// 添加或修改 用户信息
        /// </summary>
        /// <param name="strUserNo"></param>
        /// <param name="strName"></param>
        /// <param name="strPsw"></param>
        /// <param name="iPrivilege"></param>
        /// <returns></returns>
        public bool SetUserInfo(string strUserNo, string strName, string strPsw, int iPrivilege)
        {
            bool blRet = false;
            try
            {
                axCZKEM1.EnableDevice(iMachineNumber, false );
                blRet = axCZKEM1.SSR_SetUserInfo(iMachineNumber, strUserNo, strName, strPsw, iPrivilege, true);
                axCZKEM1.EnableDevice(iMachineNumber, true);
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
            return blRet;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            try
            {
                axCZKEM1.Disconnect();
                blConnoct = false;
                //当验证通过时触发该事件
                this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                this.axCZKEM1.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
                this.axCZKEM1.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
                this.axCZKEM1.OnDisConnected -= new zkemkeeper._IZKEMEvents_OnDisConnectedEventHandler(axCZKEM1_OnDisConnected);
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }

        /// <summary>
        /// 连接控制器
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ConnectZk()
        {
            bool blRet = false;
            await Task.Factory.StartNew(() => {
                try
                {
                    if (string.IsNullOrEmpty(strIp))
                    {
                        MessageUtil.ShowTips("指纹门禁 IP 为空");
                        return blRet;
                    }
                    if (iPort == 0)
                    {
                        MessageUtil.ShowTips("指纹门禁 端口号 未设置");
                        return blRet;
                    }
                    blRet = axCZKEM1.Connect_Net(strIp, iPort);
                    if (blRet == true)
                    {//连接成功
                        blConnoct = true;
                        int iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                        if (axCZKEM1.RegEvent(iMachineNumber, 65535))
                        {
                            blRet = true;
                            //当验证通过时触发该事件
                            this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                            this.axCZKEM1.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
                            this.axCZKEM1.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
                            this.axCZKEM1.OnDisConnected += new zkemkeeper._IZKEMEvents_OnDisConnectedEventHandler(axCZKEM1_OnDisConnected);
                        }
                        else
                        {
                            blRet = false;
                            //axCZKEM1.GetLastError(ref idwErrorCode);
                            //lbRTShow.Items.Add("连接设备失败，错误码： " + idwErrorCode.ToString());
                        }
                    }
                    else
                    {
                        blRet = false;
                        //axCZKEM1.GetLastError(ref idwErrorCode);
                        //lbRTShow.Items.Add("连接设备失败，错误码： " + idwErrorCode.ToString());
                    }
                }
                catch (Exception ex)
                {
                    if (frmMain.blDebug)
                        MessageUtil.ShowTips(ex.Message);
                }
                return blRet;
            });
            return blRet;
        }

        public bool GetDoorState(ref DoorsState  state)
        {
            bool blRet = false;
            try
            {
                int iState=0;
                if (axCZKEM1.GetDoorState(iMachineNumber, ref iState))
                {
                    blRet = true;
                    if (iState == 0)
                        state = DoorsState.开门;
                    else if (iState == 1)
                        state = DoorsState.关门;
                }
                else
                    blRet = false;
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
            return blRet;
        }

        /// <summary>
        /// 只连接 不绑定 事件
        /// </summary>
        public bool OnlyConnect()
        {
            bool blRet = false;
            try
            {
                if (string.IsNullOrEmpty(strIp))
                {
                    MessageUtil.ShowTips("指纹门禁IP为空");
                    return blRet;
                }
                if (iPort == 0)
                {
                    MessageUtil.ShowTips(strIp + "-指纹门禁端口号未设置");
                    return blRet;
                }
                blRet = axCZKEM1.Connect_Net(strIp, iPort);
                if (blRet == true)
                    blConnoct = true;
                else
                    blRet = false;
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
            return blRet;
        }

        /// <summary>
        /// 只断开连接  不注销事件
        /// </summary>
        public void OnlyDisConnect()
        {
            try
            {
                axCZKEM1.Disconnect();
                blConnoct = false;
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        #endregion

        #region  事件

        /// <summary>
        /// 当 断开连接时  时触发该事件
        /// </summary>
        private void axCZKEM1_OnDisConnected()
        {
            if (OnDisConnectEvent != null)
            {
                OnDisConnectEvent();
            }
        }

        /// <summary>
        /// 当 报警 时触发该事件
        /// </summary>
        /// <param name="iAlarmType"></param>
        /// <param name="iEnrollNumber"></param>
        /// <param name="iVerified"></param>
        private void axCZKEM1_OnAlarm(int iAlarmType, int iEnrollNumber, int iVerified)
        {
            if (OnAlarmEvent != null)
            {
                onAlarmType type = onAlarmType.初值;
                switch (iAlarmType)
                {
                    case (int)onAlarmType.拆机报警:
                        type = onAlarmType.拆机报警;
                        break;

                    case (int)onAlarmType.错按报警:
                        type = onAlarmType.错按报警;
                        break;

                    case (int)onAlarmType.反潜报警:
                        type = onAlarmType.反潜报警;
                        break;

                    case (int)onAlarmType.胁迫报警:
                        type = onAlarmType.胁迫报警;
                        break;

                    default:
                        break;
                }
                if (type != onAlarmType.初值)
                    OnAlarmEvent(new OnAlarmEventArgs(type, iEnrollNumber, iVerified));
            }
        }

        /// <summary>
        /// 当验证通过 时触发该事件
        /// </summary>
        private void axCZKEM1_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int iVerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            if (VerifyOkEvent != null)
            {
                VerifyMethod verifyMode = VerifyMethod.初值 ;
                switch (iVerifyMethod)
                {
                    case (int)VerifyMethod.指纹:
                        verifyMode = VerifyMethod.指纹;
                        break;

                    case (int)VerifyMethod.密码:
                        verifyMode = VerifyMethod.密码;
                        break;

                    case (int)VerifyMethod.刷卡:
                        verifyMode = VerifyMethod.刷卡;
                        break;

                    case (int)VerifyMethod.人脸:
                        verifyMode = VerifyMethod.人脸;
                        break;

                    case (int)VerifyMethod.PIN:
                        verifyMode = VerifyMethod.PIN;
                        break;

                    case (int)VerifyMethod.FP_OR_PW_OR_RF:
                        verifyMode = VerifyMethod.FP_OR_PW_OR_RF;
                        break;

                    case (int)VerifyMethod.FP_OR_PW:
                        verifyMode = VerifyMethod.FP_OR_PW;
                        break;

                    case (int)VerifyMethod.FP_OR_RF:
                        verifyMode = VerifyMethod.FP_OR_RF;
                        break;

                    case (int)VerifyMethod.PW_OR_RF:
                        verifyMode = VerifyMethod.PW_OR_RF;
                        break;

                    case (int)VerifyMethod.PIN_AND_FP:
                        verifyMode = VerifyMethod.PIN_AND_FP;
                        break;

                    case (int)VerifyMethod.FP_AND_PW:
                        verifyMode = VerifyMethod.FP_AND_PW;
                        break;

                    case (int)VerifyMethod.FP_AND_RF:
                        verifyMode = VerifyMethod.FP_AND_RF;
                        break;

                    case (int)VerifyMethod.PW_AND_RF:
                        verifyMode = VerifyMethod.PW_AND_RF;
                        break;

                    case (int)VerifyMethod.FP_AND_PW_AND_RF:
                        verifyMode = VerifyMethod.FP_AND_PW_AND_RF;
                        break;

                    case (int)VerifyMethod.PIN_AND_FP_AND_PW:
                        verifyMode = VerifyMethod.PIN_AND_FP_AND_PW;
                        break;

                    case (int)VerifyMethod.FP_AND_RF_OR_PIN:
                        verifyMode = VerifyMethod.FP_AND_RF_OR_PIN;
                        break;

                    default:
                        break;

                }
                if (verifyMode != VerifyMethod.初值)
                    VerifyOkEvent(new VerifyOkEventArgs(EnrollNumber, verifyMode,strIp));
            }
        }

        /// <summary>
        /// 当验开门 时触发该事件
        /// </summary>
        /// <param name="onDoorType"></param>
        private void axCZKEM1_OnDoor(int iOnDoorType)
        {
            if (OnDoorEvent != null)
            {
                OnDoorType onDoorType = OnDoorType.初值 ;
                switch (iOnDoorType)
                {
                    case (int)OnDoorType.出门按钮开门:
                        onDoorType = OnDoorType.出门按钮开门;
                        break;

                    case (int)OnDoorType.关门:
                        onDoorType = OnDoorType.关门;
                        break;

                    case (int)OnDoorType.开门:
                        onDoorType = OnDoorType.开门;
                        break;

                    case (int)OnDoorType.意外开门:
                        onDoorType = OnDoorType.意外开门;
                        break;

                    default:
                        break;
                }
                if (onDoorType != OnDoorType.初值)
                {
                    OnDoorEvent(new OnDoorEventArgs(onDoorType));
                }
            }
        }

        #endregion

    }

    #region  事件处理 及 参数

    #region   事件处理

    /// <summary>
    /// 验证通过 事件处理
    /// </summary>
    /// <param name="e"></param>
    public delegate void VerifyOkEventHandler(VerifyOkEventArgs e);
    /// <summary>
    /// 开门 事件处理
    /// </summary>
    /// <param name="e"></param>
    public delegate void OnDoorEventHandler(OnDoorEventArgs e);
    /// <summary>
    /// 报警 事件处理
    /// </summary>
    /// <param name="e"></param>
    public delegate void OnAlarmEventHandler(OnAlarmEventArgs e);
    /// <summary>
    /// 断开连接 事件处理
    /// </summary>
    public delegate void OnDisConnectEventHandler();

    #endregion

    #region  事件参数

    /// <summary>
    /// 报警 事件参数
    /// </summary>
    public class OnAlarmEventArgs : EventArgs
    {
        /// <summary>
        /// 报警类型
        /// </summary>
        public onAlarmType alarmType;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int iUserId;
        /// <summary>
        /// 是否验证
        /// </summary>
        public int iVerifyd;

        public OnAlarmEventArgs(onAlarmType Type, int iId, int iVeri)
        {
            this.alarmType = Type;
            this.iUserId = iId;
            this.iVerifyd = iVeri;
        }
    }

    /// <summary>
    /// 开门 事件参数
    /// </summary>
    public class OnDoorEventArgs : EventArgs
    {
        public OnDoorType onDoorType;

        public OnDoorEventArgs(OnDoorType type)
        {
            this.onDoorType = type;
        }
    }

    /// <summary>
    /// 验证通过 事件参数
    /// </summary>
    public class VerifyOkEventArgs : EventArgs
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string strUserId;
        /// <summary>
        /// 验证方式
        /// </summary>
        public VerifyMethod verifyMode;
        /// <summary>
        /// 设备 IP 地址
        /// </summary>
        public string strDeviIp;

        public VerifyOkEventArgs(string Id, VerifyMethod mode,string ip)
        {
            this.strUserId = Id;
            this.verifyMode = mode;
            this.strDeviIp = ip;
        }
    }

    #endregion

    #endregion

    #region  中控的 枚举

    public enum onAlarmType : int
    {
        初值,
        拆机报警 = 55,
        错按报警 = 58,
        胁迫报警 = 32,
        反潜报警 = 34
    }

    //4 表示门未关好或者门已打开,53 表示出门按钮,5 表示门已关闭,1 表示门被意外打开
    public enum OnDoorType : int
    {
        初值,
        开门 = 4,
        出门按钮开门 = 53,
        关门 = 5,
        意外开门 = 1
    }

    /// <summary>
    /// 验证方式
    /// </summary>
    public enum VerifyMethod : int
    {
        初值,
        指纹 = 1,
        密码 = 3,
        刷卡 = 4,
        人脸 = 15,


        PIN = 2,
        FP_OR_PW_OR_RF = 0,
        FP_OR_PW = 5,
        FP_OR_RF = 6,
        PW_OR_RF = 7,
        PIN_AND_FP = 8,
        FP_AND_PW = 9,
        FP_AND_RF = 10,
        PW_AND_RF = 11,
        FP_AND_PW_AND_RF = 12,
        PIN_AND_FP_AND_PW = 13,
        FP_AND_RF_OR_PIN = 14
    }

    #endregion


}

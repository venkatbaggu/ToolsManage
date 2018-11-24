using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Threading;

using ToolsManage.BaseClass.DoorClass;
using ToolsManage.BaseClass.SeralClass;
using ToolsManage.Common;
using Common.FileLog;

namespace ToolsManage.BaseClass.EnvirClass
{
    public class clsEnvirControl
    {
        public bool Is485XF = false;
        DataLogic datalogic = new DataLogic();
        clsCommon commonCls = new clsCommon();

        public clsSerialSensor serialSensor = new clsSerialSensor();//烟感 温湿度 
        public clsSerialIo serialIo = new clsSerialIo();//IO板
        public clsSerialAir serialAir = new clsSerialAir();//空调
        public clsSerialXF serialXF = null;   //新风
        

        public TimerHelper timerSensor = new TimerHelper(60000, false);   
        public TimerHelper timerIo = new TimerHelper(60000, false);
        public TimerHelper timerAir = new TimerHelper(60000,false);
        public TimerHelper timerXF = null;

        public static bool blAskAir = true;
        public static bool blAskIo = true;
        public static bool blAskSensor = true;
        public static bool blAskXF = true;
        public static List<RoomClass> listRoom = new List<RoomClass>();

        /// <summary>
        /// IO程序 首次读 设备状态是 不作为手动
        /// </summary>
        private bool blIoFrist = true;
        /// <summary>
        /// 空调程序 首次读 设备状态是 不作为手动
        /// </summary>
        private bool blAirFrist = true;
        /// <summary>
        /// true 时 第一圈 温湿度未采集完 不控制设备
        /// </summary>
        private bool blControl = true;
        /// <summary>
        /// 首次读 设备状态是 不作为手动
        /// </summary>
        private bool blXFFirst = true;


        private DateTime timeLastRecord = DateTime.Now.AddHours(-2);


        #region  事件接口

        /// <summary>
        /// 环境告警 事件
        /// </summary>
        public event NewEventShowEventHandler NewAlarmShowEvent;

        #endregion

        public clsEnvirControl()
        {
            Is485XF = Configurations.Is485XF;
            timerSensor.Execute += new TimerHelper.TimerExecution(EnvirSensor_Execute);
            timerIo.Execute += new TimerHelper.TimerExecution(EnvirIo_Execute);
            timerAir.Execute +=new TimerHelper.TimerExecution(timerAir_Execute);
            if(Is485XF)
            {
                serialXF= new clsSerialXF(); //新风
                timerXF = new TimerHelper(60 * 1000, false);
                timerXF.Execute+= new TimerHelper.TimerExecution(timerXF_Execute);
            }
        }

        #region 子函数

        public void Start()
        {
            blAskIo = true;
            blAskAir = true;
            blAskSensor = true;
            blAskXF = true;
            blIoFrist = true;
            blAirFrist = true;
            blControl = true;
            blXFFirst = true;
            timerSensor.Start();
            timerIo.Start();
            timerAir.Start();
            if (Is485XF)
            {
                timerXF.Start();
            }
        }

        public void Stop()
        {
            blAskIo = false;
            blAskAir = false;
            blAskSensor = false;
            blAskXF = false;
            blIoFrist = false;
            blAirFrist = false;
            blXFFirst = false;
            timerSensor.Stop();
            timerIo.Stop();
            timerAir.Stop();
            if(Is485XF)
            {
                timerXF.Stop();
            }
        }

        public void Clear()
        {
            if (timerSensor != null)
            {
                timerSensor.Execute -= new TimerHelper.TimerExecution(EnvirSensor_Execute);
                timerSensor.Stop();
                timerSensor = null; 
            }
            if (timerIo != null)
            {
                timerIo.Execute -= new TimerHelper.TimerExecution(EnvirIo_Execute);
                timerIo.Stop();
                timerIo = null; 
            }
            if (timerAir != null)
            {
                timerAir.Execute -= new TimerHelper.TimerExecution(timerAir_Execute);
                timerAir.Stop();
                timerAir = null; 
            }
            if(timerXF!=null)
            {
                timerXF.Execute -= new TimerHelper.TimerExecution(timerXF_Execute);
                timerAir.Stop();
                timerAir = null;
            }
            if (serialSensor != null || serialIo != null || serialAir != null||serialXF!=null)
            {
                blAskSensor = false;
                blAskIo = false;
                blAskAir = false;
                blAskXF = false;
                Thread.Sleep(200);

                if (serialSensor != null)
                {
                    if (serialSensor.serial.IsOpen)
                        serialSensor.serial.ClosePort();
                    serialSensor = null;
                }
                if (serialIo != null)
                {
                    if (serialIo.serial.IsOpen)
                        serialIo.serial.ClosePort();
                    serialIo = null;
                }
                if (serialAir != null)
                {
                    if (serialAir.serial.IsOpen)
                        serialAir.serial.ClosePort();
                    serialAir = null;
                }
                if(serialXF!=null)
                {
                    if (serialXF.serial.IsOpen)
                        serialXF.serial.ClosePort();
                    serialXF = null;
                }
            }
        }

        #endregion

        #region  新风轮询
        private void timerXF_Execute()
        {
            try
            {
                for (int iRoomIndex = 0; iRoomIndex < listRoom.Count; iRoomIndex++)
                {
                    if (blControl)
                        break;
                    if (blAskXF == false)
                        break;
                    if (listRoom[iRoomIndex] != null)
                    {
                        if (listRoom[iRoomIndex].roomFan != null)
                        {
                            int iAddr = listRoom[iRoomIndex].roomFan.IntAddr;
                            string strArea = listRoom[iRoomIndex].StrName;
                            #region 查询新风系统状态并同步
                            if (!blAskXF)
                                break;
                            byte[] btIoRet = serialXF.AskDeviceState(listRoom[iRoomIndex].roomFan.IntAddr);
                            if (btIoRet[0] == (byte)CommuniState.正常)
                            {
                                byte bDeviState = btIoRet[1];  //设备状态
                                if (bDeviState == 0x01)
                                {//新风运行
                                    if (listRoom[iRoomIndex].roomFan.State != DeviceRunState.运行)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomFan.State = DeviceRunState.运行;
                                        }
                                        if (!blXFFirst)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.手动;
                                                listRoom[iRoomIndex].roomFan.TimeHand = DateTime.Now;
                                            }
                                            serialXF.NewEventRecord(EventType.新风, strArea, DeviceRunModel.手动, EventContent.开启.ToString());
                                        }
                                    }
                                }
                                else
                                {//新风停止
                                    if (listRoom[iRoomIndex].roomFan.State != DeviceRunState.停止)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomFan.State = DeviceRunState.停止;
                                        }
                                        if (!blXFFirst)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.手动;
                                                listRoom[iRoomIndex].roomFan.TimeHand = DateTime.Now;
                                            }
                                            serialXF.NewEventRecord(EventType.新风, strArea, DeviceRunModel.手动, EventContent.关闭.ToString());
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region 新风 控制
                            if (!blAskXF)
                                break;
                            if (listRoom[iRoomIndex].roomFan.HandOrAuto == DeviceRunModel.自动 && blAskIo)
                            {
                                if (listRoom[iRoomIndex].roomFan.State == DeviceRunState.运行)
                                {
                                    bool blRet = serialXF.OperDevice(iAddr, OnOffRelay.关闭, strArea, DeviceRunModel.自动, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomFan.State = DeviceRunState.停止;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region 报警 关 设备，正常 恢复自动控制  
                            if (!blAskXF)
                                break;
                            if (listRoom[iRoomIndex].FireState == ProbeState.报警)
                            {
                                #region  关闭新风

                                if (!blAskIo)
                                    break;
                                if (listRoom[iRoomIndex].roomFan.HandOrAuto != DeviceRunModel.烟雾报警)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.烟雾报警;
                                    }
                                }
                                if (listRoom[iRoomIndex].roomFan.State == DeviceRunState.运行)
                                {
                                    bool blRet = serialXF.OperDevice(iAddr, OnOffRelay.关闭, strArea, DeviceRunModel.烟雾报警, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomFan.State = DeviceRunState.停止;
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (listRoom[iRoomIndex].FireState == ProbeState.正常)
                            {
                                //新风 恢复自动
                                if (listRoom[iRoomIndex].roomFan.HandOrAuto == DeviceRunModel.烟雾报警)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.自动;
                                    }
                                }
                            }
                            #endregion
                            #region 手动 恢复 自动控制
                            //新风
                            if (listRoom[iRoomIndex].roomFan.HandOrAuto == DeviceRunModel.手动)
                            {
                                TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].roomFan.TimeHand;
                                if (ts.TotalMinutes >= clsEnvirSet.iHandHoldTime)// TotalHours  TotalSeconds
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.自动;
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 空调轮询
        private void timerAir_Execute()
        {
            try
            {
                for (int iRoomIndex = 0; iRoomIndex < listRoom.Count; iRoomIndex++)
                {
                    if (blControl)
                        break;
                    if (blAskAir == false)
                        break;
                    if (listRoom[iRoomIndex] != null)
                    {
                        string strArea = listRoom[iRoomIndex].StrName;

                        #region  空调  同步 及 控制

                        if (listRoom[iRoomIndex].listAir != null)
                        {
                            bool blMoreAir = false;//是否 为 一个房间多台空调
                            if (listRoom[iRoomIndex].listAir.Count > 1)
                                blMoreAir = true;

                            for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listAir.Count; iIndex++)//房间内的 所有空调
                            {
                                if (blAskAir == false)
                                    break;

                                if (listRoom[iRoomIndex].listAir[iIndex] != null)
                                {
                                    #region  查询 空调状态 并同步

                                    if (listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.大金)
                                    {
                                        byte btAddr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                        DeviceRunState state = DeviceRunState.停止;
                                        AirRunModel model = AirRunModel.制冷;
                                        int iSetTemp = 26;
                                        //接受 格式错误 发送3次
                                        for (int iSend = 0; iSend < 3; iSend++)
                                        {
                                            if (blAskAir == false)
                                                break;
                                            CommuniState rece = serialAir.DjAirReadData(btAddr, ref state, ref model, ref iSetTemp);
                                            if (rece == CommuniState.无回复)//无回复 不需要 发三次
                                                break;
                                            if (rece == CommuniState.正常) 
                                            {

                                                #region   运行状态

                                                if (blAskAir == false)
                                                    break;
                                                if (state != listRoom[iRoomIndex].listAir[iIndex].State)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].State = state;
                                                    }
                                                    if (!blAirFrist )
                                                    {
                                                        lock (listRoom)
                                                        {
                                                            listRoom[iRoomIndex].listAir[iIndex].HandOrAuto = DeviceRunModel.手动;
                                                            listRoom[iRoomIndex].listAir[iIndex].TimeHand = DateTime.Now;
                                                        }
                                                        string strContent = listRoom[iRoomIndex].listAir[iIndex].State.ToString();
                                                        commonCls.NewEnvirEventRecord(EventType.空调, strArea, DeviceRunModel.手动, strContent);
                                                    }
                                                }

                                                #endregion

                                                #region   运行模式

                                                if (blAskAir == false)
                                                    break;
                                                if (model != listRoom[iRoomIndex].listAir[iIndex].Model)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].Model = model;
                                                    }
                                                    if (!blAirFrist)
                                                    {
                                                        lock (listRoom)
                                                        {
                                                            listRoom[iRoomIndex].listAir[iIndex].HandOrAuto = DeviceRunModel.手动;
                                                            listRoom[iRoomIndex].listAir[iIndex].TimeHand = DateTime.Now;
                                                        }
                                                        string strContent = listRoom[iRoomIndex].listAir[iIndex].Model.ToString();
                                                        commonCls.NewEnvirEventRecord(EventType.空调, strArea, DeviceRunModel.手动, strContent);
                                                    }
                                                }

                                                #endregion

                                                #region   设置温度

                                                if (blAskAir == false)
                                                    break;
                                                if (iSetTemp != listRoom[iRoomIndex].listAir[iIndex].IntTempSet)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].IntTempSet = iSetTemp;
                                                    }
                                                    if (!blAirFrist)
                                                    {
                                                        lock (listRoom)
                                                        {
                                                            listRoom[iRoomIndex].listAir[iIndex].HandOrAuto = DeviceRunModel.手动;
                                                            listRoom[iRoomIndex].listAir[iIndex].TimeHand = DateTime.Now;
                                                        }
                                                        string strContent = EventContent.设置温度.ToString() + iSetTemp.ToString();
                                                        commonCls.NewEnvirEventRecord(EventType.空调, strArea, DeviceRunModel.手动, strContent);
                                                    }
                                                }

                                                #endregion

                                                break;
                                            }
                                        }
                                    }

                                    #endregion

                                    #region 空调控制

                                    if (listRoom[iRoomIndex].listAir[iIndex].HandOrAuto == DeviceRunModel.自动)
                                    {
                                        #region  制冷 开启 设置模式 设置温度 ，关闭

                                        if (blAskAir == false)
                                            break;
                                        bool blOnCool = listRoom[iRoomIndex].ForWsdGetAirOnCool();
                                        if (blOnCool)
                                        {
                                            if (listRoom[iRoomIndex].listAir[iIndex].State == DeviceRunState.停止)
                                            {
                                                //开 空调
                                                if (blAskAir == false)
                                                    break;
                                                AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                                byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                                bool blRet = serialAir.AirControl(factory, addr, EventContent.开启, 26, strArea, DeviceRunModel.自动, blMoreAir,IsWait.CanStop );
                                                if (blRet)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].State = DeviceRunState.运行;
                                                    }
                                                }
                                            }
                                            if (listRoom[iRoomIndex].listAir[iIndex].Model != AirRunModel.制冷)
                                            {
                                                //设置 制冷
                                                if (blAskAir == false)
                                                    break;
                                                AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                                byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                                bool blRet = serialAir.AirControl(factory, addr, EventContent.设置制冷, 26, strArea, DeviceRunModel.自动, blMoreAir, IsWait.CanStop);
                                                if (blRet)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].Model = AirRunModel.制冷;
                                                    }
                                                }
                                            }
                                            if (listRoom[iRoomIndex].listAir[iIndex].IntTempSet != clsEnvirSet.intSetTempCool)
                                            {
                                                //设置 温度
                                                if (blAskAir == false)
                                                    break;
                                                AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                                byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                                byte btTemp = (byte)clsEnvirSet.intSetTempCool;
                                                bool blRet = serialAir.AirControl(factory, addr, EventContent.设置温度, btTemp, strArea, DeviceRunModel.自动, blMoreAir, IsWait.CanStop);
                                                if (blRet)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].IntTempSet = clsEnvirSet.intSetTempCool;
                                                    }
                                                }
                                            }
                                        }

                                        bool blOffCool = listRoom[iRoomIndex].ForWsdGetAirOffCool();
                                        if (blOffCool)
                                        {
                                            if (listRoom[iRoomIndex].listAir[iIndex].State == DeviceRunState.运行 && listRoom[iRoomIndex].listAir[iIndex].Model == AirRunModel.制冷)
                                            {
                                                //关 空调
                                                if (blAskAir == false)
                                                    break;
                                                AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                                byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                                bool blRet = serialAir.AirControl(factory, addr, EventContent.关闭, 26, strArea, DeviceRunModel.自动, blMoreAir, IsWait.CanStop);
                                                if (blRet)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].State = DeviceRunState.停止;
                                                    }
                                                }
                                            }
                                        }

                                        #endregion

                                        #region  制热 开启 设置模式 设置温度 ，关闭

                                        if (blAskAir == false)
                                            break;
                                        bool blOnHot = listRoom[iRoomIndex].ForWsdGetAirOnHot();
                                        if (blOnHot)
                                        {
                                            if (listRoom[iRoomIndex].listAir[iIndex].State == DeviceRunState.停止)
                                            {
                                                //开 空调
                                                if (blAskAir == false)
                                                    break;
                                                AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                                byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                                bool blRet = serialAir.AirControl(factory, addr, EventContent.开启, 26, strArea, DeviceRunModel.自动, blMoreAir, IsWait.CanStop);
                                                if (blRet)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].State = DeviceRunState.运行;
                                                    }
                                                }
                                            }

                                            if (listRoom[iRoomIndex].listAir[iIndex].Model != AirRunModel.制热)
                                            {
                                                //设置 制热
                                                if (blAskAir == false)
                                                    break;
                                                AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                                byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                                bool blRet = serialAir.AirControl(factory, addr, EventContent.设置制热, 26, strArea, DeviceRunModel.自动, blMoreAir, IsWait.CanStop);
                                                if (blRet)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].Model = AirRunModel.制热;
                                                    }
                                                }
                                            }
                                            if (listRoom[iRoomIndex].listAir[iIndex].IntTempSet != clsEnvirSet.intSetTempHot )
                                            {
                                                //设置 温度
                                                if (blAskAir == false)
                                                    break;
                                                AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                                byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                                byte btTemp = (byte)clsEnvirSet.intSetTempHot;
                                                bool blRet = serialAir.AirControl(factory, addr, EventContent.设置温度, btTemp, strArea, DeviceRunModel.自动, blMoreAir, IsWait.CanStop);
                                                if (blRet)
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].IntTempSet = clsEnvirSet.intSetTempHot;
                                                    }
                                                }
                                            }
                                        }

                                        bool blOffHot = listRoom[iRoomIndex].ForWsdGetAirOffHot();
                                        if (blOffHot)
                                        {
                                            if (listRoom[iRoomIndex].listAir[iIndex].State == DeviceRunState.运行 && listRoom[iRoomIndex].listAir[iIndex].Model == AirRunModel.制热)
                                            {
                                                //关 空调
                                                if (blAskAir == false)
                                                    break;
                                                AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                                byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                                bool blRet = serialAir.AirControl(factory, addr, EventContent.关闭, 26, strArea, DeviceRunModel.自动, blMoreAir, IsWait.CanStop);
                                                if (blRet) 
                                                {
                                                    lock (listRoom)
                                                    {
                                                        listRoom[iRoomIndex].listAir[iIndex].State = DeviceRunState.停止;
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }

                                    #endregion

                                    #region  手动 恢复自动控制

                                    if (listRoom[iRoomIndex].listAir[iIndex].HandOrAuto == DeviceRunModel.手动)
                                    {
                                        TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].listAir[iIndex].TimeHand;
                                        if (ts.TotalMinutes >= clsEnvirSet.iHandHoldTime)// TotalHours  TotalSeconds
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].listAir[iIndex].HandOrAuto = DeviceRunModel.自动;
                                            }
                                        }
                                    }

                                    #endregion

                                }
                            }
                        }




                        #endregion

                        #region  根据房间 烟感状态 关空调 或恢复自动控制

                        if (blAskAir == false)
                            break;
                        if (listRoom[iRoomIndex].FireState == ProbeState.报警)
                        {
                            #region  关 空调

                            if (listRoom[iRoomIndex].listAir != null)
                            {
                                bool blMoreAir = false;//是否 为 一个房间多台空调
                                if (listRoom[iRoomIndex].listAir.Count > 1)
                                    blMoreAir = true;
                                for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listAir.Count; iIndex++)//房间内的 所有空调
                                {
                                    if (blAskAir == false)
                                        break;
                                    if (listRoom[iRoomIndex].listAir[iIndex] != null)
                                    {
                                        if (listRoom[iRoomIndex].listAir[iIndex].State == DeviceRunState.运行)
                                        {
                                            //关 空调
                                            if (blAskAir == false)
                                                break;
                                            AirFactoryType factory = listRoom[iRoomIndex].listAir[iIndex].AirType;
                                            byte addr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
                                            bool blRet = serialAir.AirControl(factory, addr, EventContent.关闭, 26, strArea, DeviceRunModel.烟雾报警, blMoreAir, IsWait.CanStop);
                                            if (blRet) 
                                            {
                                                lock (listRoom)
                                                {
                                                    listRoom[iRoomIndex].listAir[iIndex].State = DeviceRunState.停止;
                                                }
                                            }
                                        }
                                        if (listRoom[iRoomIndex].listAir[iIndex].HandOrAuto != DeviceRunModel.烟雾报警)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].listAir[iIndex].HandOrAuto = DeviceRunModel.烟雾报警;
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        else if (listRoom[iRoomIndex].FireState == ProbeState.正常)
                        {
                            #region  恢复 自动控制

                            if (blAskAir == false)
                                break;
                            if (listRoom[iRoomIndex].listAir != null)
                            {
                                for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listAir.Count; iIndex++)//房间内的 所有空调
                                {
                                    if (listRoom[iRoomIndex].listAir[iIndex] != null)
                                    {
                                        if (listRoom[iRoomIndex].listAir[iIndex].HandOrAuto == DeviceRunModel.烟雾报警)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].listAir[iIndex].HandOrAuto = DeviceRunModel.自动;
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        #endregion
                    }
                }
                if (blAskAir && blControl == false)
                {
                    blAirFrist = false; 
                }
                Thread.Sleep(100);
                if (timerAir.State == TimerState.Running)
                    timerAir.Start(); 
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 
        }
        #endregion
        #region 传感器轮询
        private void EnvirSensor_Execute()
        {
            try
            {
                #region  判断是否存储数据记录 需要改 类似小区变

                bool blRecord = false;
                TimeSpan timespan = DateTime.Now - timeLastRecord;
                if (timespan.TotalHours > 2)
                {
                    timeLastRecord = DateTime.Now;
                    blRecord = true;
                }

                #endregion

                #region  温湿度 烟雾 

                for (int iRoomIndex = 0; iRoomIndex < listRoom.Count; iRoomIndex++)//所有房间
                {
                    if (!blAskSensor)
                        break;
                    if (listRoom[iRoomIndex] != null)
                    {
                        #region 温湿度值

                        if (listRoom[iRoomIndex].listWsd != null)
                        {
                            for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listWsd.Count; iIndex++)//房间内的 所有温湿度
                            {
                                if (!blAskSensor)
                                    break;
                                if (listRoom[iRoomIndex].listWsd[iIndex] != null)
                                {
                                    int iAddr = listRoom[iRoomIndex].listWsd[iIndex].IntAddr;
                                    int[] wsdValue = null;
                                    blRecord = serialSensor.AskWsd(iAddr,ref wsdValue);
                                    if (listRoom[iRoomIndex].listWsd[iIndex].IntTemp != wsdValue[0])
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].listWsd[iIndex].IntTemp = wsdValue[0];
                                        }
                                    }
                                    if (listRoom[iRoomIndex].listWsd[iIndex].IntHumi != wsdValue[1])
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].listWsd[iIndex].IntHumi = wsdValue[1];
                                        }
                                    }
                                }
                            }
                        }

                        #endregion

                        #region 烟雾

                        if (listRoom[iRoomIndex].listFire != null)
                        {
                            #region  查询烟雾状态

                            if (!blAskSensor)
                                break;
                            for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listFire.Count; iIndex++)
                            {
                                if (!blAskSensor)
                                    break;
                                string strAddr = listRoom[iRoomIndex].listFire[iIndex].StrAddr;
                                string strArea = listRoom[iRoomIndex].StrName;
                                ProbeState ReceFire = serialSensor.AskFire(strAddr);
                                if (ReceFire == ProbeState.报警)
                                {
                                    if (listRoom[iRoomIndex].listFire[iIndex].State == ProbeState.正常)
                                    {
                                        commonCls.NewEnvirAlarmRecord(AlarmsType.烟雾, strArea, strAddr, AlarmContent.报警.ToString());
                                        if (NewAlarmShowEvent != null)
                                        {
                                            NewAlarmShowEvent(new NewEventEventArgs(EventType.烟雾, strArea, AlarmContent.报警.ToString(), "", strAddr, DateTime.Now));
                                        }

                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].listFire[iIndex].State = ProbeState.报警;
                                        }
                                  
                                    }
                                }
                                else
                                {
                                    if (listRoom[iRoomIndex].listFire[iIndex].State == ProbeState.报警)
                                    {
                                        commonCls.NewEnvirAlarmRecord(AlarmsType.烟雾, strArea, strAddr, AlarmContent.恢复正常.ToString());
                                        if (NewAlarmShowEvent != null)
                                        {
                                            NewAlarmShowEvent(new NewEventEventArgs(EventType.烟雾, strArea, AlarmContent.恢复正常.ToString(), "", strAddr, DateTime.Now));
                                        }

                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].listFire[iIndex].State = ProbeState.正常;
                                        }
                                    }
                                }
                            }

                            #endregion

                            #region  判断 房间烟感是否报警

                            if (!blAskSensor)
                                break;
                            bool blRoomFire = false;
                            for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listFire.Count; iIndex++)
                            {
                                if (listRoom[iRoomIndex].listFire[iIndex].State == ProbeState.报警)
                                {
                                    blRoomFire = true;
                                    break;
                                }
                            }
                            if (blRoomFire)
                            {
                                if (listRoom[iRoomIndex].FireState == ProbeState.正常)
                                {
                                    lock (listRoom)
                                    {
                                        TXTWriteHelper.WriteException("烟感报警");
                                        listRoom[iRoomIndex].FireState = ProbeState.报警;
                                    }
                                }
                            }
                            else
                            {
                                if (listRoom[iRoomIndex].FireState == ProbeState.报警)
                                {
                                    lock (listRoom)
                                    {
                                        TXTWriteHelper.WriteException("烟感恢复正常");
                                        listRoom[iRoomIndex].FireState = ProbeState.正常;
                                    }
                                }
                            }

                            #endregion
                        }
                        #endregion
                    }
                }

                #endregion

                if (blAskSensor)
                    blControl = false;

                #region   数据记录

                if (blRecord)
                {
                    for (int iRoomIndex = 0; iRoomIndex < listRoom.Count; iRoomIndex++)
                    {
                        if (listRoom[iRoomIndex] != null)
                        {
                            string strArea = listRoom[iRoomIndex].StrName;
                            if (listRoom[iRoomIndex].listWsd != null)
                            {
                                for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listWsd.Count; iIndex++)
                                {
                                    string strAddr = listRoom[iRoomIndex].listWsd[iIndex].IntAddr.ToString();
                                    string strTemp = listRoom[iRoomIndex].listWsd[iIndex].IntTemp.ToString();
                                    string strHumi = listRoom[iRoomIndex].listWsd[iIndex].IntHumi.ToString();
                                    commonCls.NewEnvirDataRecord(strArea ,strAddr ,strTemp ,strHumi );
                                }
                            }
                        }
                    }
                }

                #endregion

                Thread.Sleep(100);
                if (timerSensor.State == TimerState.Running)
                    timerSensor.Start();
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }
        #endregion

        #region 继电器板轮询
        private void EnvirIo_Execute()
        {
            try
            {
                //TXTWriteHelper.WriteException("EnvirIo_Execute");
                for (int iRoomIndex = 0; iRoomIndex < listRoom.Count; iRoomIndex++)
                {
                    //TXTWriteHelper.WriteException("遍历"+iRoomIndex);
                    if (blControl)
                    {
                        //TXTWriteHelper.WriteException("遍历控制退出");
                        break;
                    }
                    
                    if (!blAskIo)
                    {
                        //TXTWriteHelper.WriteException("EnvirIo_Execute-退出1");
                        break;
                    }
                    //if(listRoom[iRoomIndex]==null)
                    //    TXTWriteHelper.WriteException("listRoom[iRoomIndex]为空");
                    if (listRoom[iRoomIndex] != null)
                    {
                        if (listRoom[iRoomIndex].roomRelay != null)
                        {
                            int iAddr = listRoom[iRoomIndex].roomRelay.IntAddr;
                            string strArea = listRoom[iRoomIndex].StrName;

                            #region  查询 继电器状态 并同步
                            //TXTWriteHelper.WriteException("查询 继电器状态 并同步");
                            if (!blAskIo)
                            {
                                //TXTWriteHelper.WriteException("EnvirIo_Execute-退出2");
                                break;
                            }
                            if (listRoom[iRoomIndex].roomRelay != null)
                            {
                                byte[] btIoRet = serialIo.AskDeviceState(listRoom[iRoomIndex].roomRelay.IntAddr);
                                if (btIoRet[0] == (byte)CommuniState.正常)
                                {
                                    byte bDeviState = btIoRet[1];

                                    #region  烘干

                                    if (!blAskIo)
                                    {
                                        //TXTWriteHelper.WriteException("EnvirIo_Execute-退出3");
                                        break;
                                    }
                                    if ((bDeviState & 0x01) == 0x01)
                                    {
                                        if (listRoom[iRoomIndex].roomHot.State != DeviceRunState.运行)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomHot.State = DeviceRunState.运行;
                                            }
                                            if (!blIoFrist)
                                            {
                                                lock (listRoom)
                                                {
                                                    listRoom[iRoomIndex].roomHot.HandOrAuto = DeviceRunModel.手动;
                                                    listRoom[iRoomIndex].roomHot.TimeHand = DateTime.Now;
                                                }
                                                serialIo.NewEventRecord(EventType.烘干, strArea, DeviceRunModel.手动, EventContent.开启.ToString());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (listRoom[iRoomIndex].roomHot.State != DeviceRunState.停止)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomHot.State = DeviceRunState.停止;
                                            }
                                            if (!blIoFrist)
                                            {
                                                lock (listRoom)
                                                {
                                                    listRoom[iRoomIndex].roomHot.HandOrAuto = DeviceRunModel.手动;
                                                    listRoom[iRoomIndex].roomHot.TimeHand = DateTime.Now;
                                                }
                                                serialIo.NewEventRecord(EventType.烘干, strArea, DeviceRunModel.手动, EventContent.关闭.ToString());
                                            }
                                        }
                                    }

                                    #endregion

                                    #region  除湿

                                    if (!blAskIo)
                                    {
                                        //TXTWriteHelper.WriteException("EnvirIo_Execute-退出4");
                                        break;
                                    }
                                    if ((bDeviState & 0x02) == 0x02)
                                    {
                                        if (listRoom[iRoomIndex].roomDehumi.State != DeviceRunState.运行)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomDehumi.State = DeviceRunState.运行;
                                            }
                                            if (!blIoFrist)
                                            {
                                                lock (listRoom)
                                                {
                                                    listRoom[iRoomIndex].roomDehumi.HandOrAuto = DeviceRunModel.手动;
                                                    listRoom[iRoomIndex].roomDehumi.TimeHand = DateTime.Now;
                                                }
                                                serialIo.NewEventRecord(EventType.除湿, strArea, DeviceRunModel.手动, EventContent.开启.ToString());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (listRoom[iRoomIndex].roomDehumi.State != DeviceRunState.停止)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomDehumi.State = DeviceRunState.停止;
                                            }
                                            if (!blIoFrist)
                                            {
                                                lock (listRoom)
                                                {
                                                    listRoom[iRoomIndex].roomDehumi.HandOrAuto = DeviceRunModel.手动;
                                                    listRoom[iRoomIndex].roomDehumi.TimeHand = DateTime.Now;
                                                }
                                                serialIo.NewEventRecord(EventType.除湿, strArea, DeviceRunModel.手动, EventContent.关闭.ToString());
                                            }
                                        }
                                    }

                                    #endregion

                                    #region  新风

                                    if (!blAskIo)
                                    {
                                        //TXTWriteHelper.WriteException("EnvirIo_Execute-退出5");
                                        break;
                                    }
                                    if ((bDeviState & 0x04) == 0x04)
                                    {
                                        if (listRoom[iRoomIndex].roomFan.State != DeviceRunState.运行)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomFan.State = DeviceRunState.运行;
                                            }
                                            if (!blIoFrist)
                                            {
                                                lock (listRoom)
                                                {
                                                    listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.手动;
                                                    listRoom[iRoomIndex].roomFan.TimeHand = DateTime.Now;
                                                }
                                                serialIo.NewEventRecord(EventType.新风, strArea, DeviceRunModel.手动, EventContent.开启.ToString());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (listRoom[iRoomIndex].roomFan.State != DeviceRunState.停止)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomFan.State = DeviceRunState.停止;
                                            }
                                            if (!blIoFrist)
                                            {
                                                lock (listRoom)
                                                {
                                                    listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.手动;
                                                    listRoom[iRoomIndex].roomFan.TimeHand = DateTime.Now;
                                                }
                                                serialIo.NewEventRecord(EventType.新风, strArea, DeviceRunModel.手动, EventContent.关闭.ToString());
                                            }
                                        }
                                    }

                                    #endregion

                                    #region 警灯

                                    if (!blAskIo)
                                    {
                                        //TXTWriteHelper.WriteException("EnvirIo_Execute-退出6");
                                        break;
                                    }
                                    if ((bDeviState & 0x08) == 0x08)
                                    {
                                        if (listRoom[iRoomIndex].StateOfAlarmLed != EventContent.开启)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].StateOfAlarmLed = EventContent.开启;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (listRoom[iRoomIndex].StateOfAlarmLed != EventContent.关闭)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].StateOfAlarmLed = EventContent.关闭;
                                            }
                                        }
                                    }

                                    #endregion

                                }
                            }

                            #endregion

                            #region 烘干 控制

                            if (!blAskIo)
                            {
                                //TXTWriteHelper.WriteException("EnvirIo_Execute-退出7");
                                break;
                            }
                            if (listRoom[iRoomIndex].roomHot.HandOrAuto == DeviceRunModel.自动 && blAskIo)
                            {
                                if (listRoom[iRoomIndex].roomHot.State == DeviceRunState.运行)
                                {
                                    bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.烘干, strArea, DeviceRunModel.自动, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomHot.State = DeviceRunState.停止;
                                        }
                                    }
                                }
                            }

                            #endregion

                            #region 除湿 控制

                            if (!blAskIo)
                            {
                                //TXTWriteHelper.WriteException("EnvirIo_Execute-退出8");
                                break;
                            }
                            if (listRoom[iRoomIndex].roomDehumi.HandOrAuto == DeviceRunModel.自动 && blAskIo)
                            {
                                bool biHumi = listRoom[iRoomIndex].ForWsdGetHumiOn();
                                if (biHumi)
                                {
                                    if (listRoom[iRoomIndex].roomDehumi.State == DeviceRunState.停止)
                                    {
                                        bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.开启, DeviceRelayNo.除湿, strArea, DeviceRunModel.自动, true, IsWait.CanStop);
                                        if (blRet)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomDehumi.State = DeviceRunState.运行;
                                            }
                                        }
                                    }
                                }
                                if (!blAskIo)
                                {
                                    //TXTWriteHelper.WriteException("EnvirIo_Execute-退出9");
                                    break;
                                }
                                biHumi = listRoom[iRoomIndex].ForWsdGetHumiOff();
                                if (biHumi)
                                {
                                    if (listRoom[iRoomIndex].roomDehumi.State == DeviceRunState.运行)
                                    {
                                        bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.除湿, strArea, DeviceRunModel.自动, true, IsWait.CanStop);
                                        if (blRet)
                                        {
                                            lock (listRoom)
                                            {
                                                listRoom[iRoomIndex].roomDehumi.State = DeviceRunState.停止;
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            #region 新风 控制

                            if (!blAskIo)
                            {
                                //TXTWriteHelper.WriteException("EnvirIo_Execute-退出10");
                                break;
                            }
                            if (listRoom[iRoomIndex].roomFan.HandOrAuto == DeviceRunModel.自动 && blAskIo)
                            {
                                if (listRoom[iRoomIndex].roomFan.State == DeviceRunState.运行)
                                {
                                    bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.新风, strArea, DeviceRunModel.自动, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomFan.State = DeviceRunState.停止;
                                        }
                                    }
                                }
                            }

                            #endregion

                            #region 报警 关 设备，正常 恢复自动控制  

                            if (!blAskIo)
                            {
                                //TXTWriteHelper.WriteException("EnvirIo_Execute-退出11");
                                break;
                            }
                            //TXTWriteHelper.WriteException("EnvirIo_Execute"+listRoom[iRoomIndex].FireState.ToString());
                            if (listRoom[iRoomIndex].FireState == ProbeState.报警)
                            {
                                //TXTWriteHelper.WriteException("烟感报警开警灯");
                                #region 开警灯

                                if (!blAskIo)
                                {
                                    //TXTWriteHelper.WriteException("EnvirIo_Execute-退出12");
                                    break;
                                }
                                if (listRoom[iRoomIndex].StateOfAlarmLed != EventContent.开启)
                                {
                                    //TXTWriteHelper.WriteException("开启警灯");
                                    bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.开启, DeviceRelayNo.警灯, strArea, DeviceRunModel.自动, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].StateOfAlarmLed = EventContent.开启;
                                        }
                                    }
                                }

                                #endregion

                                #region  关闭烘干

                                if (!blAskIo)
                                {
                                    //TXTWriteHelper.WriteException("EnvirIo_Execute-退出13");
                                    break;
                                }
                                if (listRoom[iRoomIndex].roomHot.HandOrAuto != DeviceRunModel.烟雾报警)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomHot.HandOrAuto = DeviceRunModel.烟雾报警;
                                    }
                                }
                                if (listRoom[iRoomIndex].roomHot.State == DeviceRunState.运行)
                                {
                                    bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.烘干, strArea, DeviceRunModel.烟雾报警, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomHot.State = DeviceRunState.停止;
                                        }
                                    }
                                }

                                #endregion

                                #region  关闭除湿

                                if (!blAskIo)
                                {
                                    //TXTWriteHelper.WriteException("EnvirIo_Execute-退出14");
                                    break;
                                }
                                if (listRoom[iRoomIndex].roomDehumi.HandOrAuto != DeviceRunModel.烟雾报警)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomDehumi.HandOrAuto = DeviceRunModel.烟雾报警;
                                    }
                                }
                                if (listRoom[iRoomIndex].roomDehumi.State == DeviceRunState.运行)
                                {
                                    bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.除湿, strArea, DeviceRunModel.烟雾报警, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomDehumi.State = DeviceRunState.停止;
                                        }
                                    }
                                }
                                #endregion

                                #region  关闭新风

                                if (!blAskIo)
                                {
                                    //TXTWriteHelper.WriteException("EnvirIo_Execute-退出15");
                                    break;
                                }
                                if (listRoom[iRoomIndex].roomFan.HandOrAuto != DeviceRunModel.烟雾报警)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.烟雾报警;
                                    }
                                }
                                if (listRoom[iRoomIndex].roomFan.State == DeviceRunState.运行)
                                {
                                    bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.新风, strArea, DeviceRunModel.烟雾报警, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].roomFan.State = DeviceRunState.停止;
                                        }
                                    }
                                }

                                #endregion
                            }
                            else if (listRoom[iRoomIndex].FireState == ProbeState.正常)
                            {
                                #region  正常 恢复自动控制 关警灯

                                //警灯
                                if (!blAskIo)
                                {
                                    //TXTWriteHelper.WriteException("EnvirIo_Execute-退出16");
                                    break;
                                }
                                if (listRoom[iRoomIndex].StateOfAlarmLed != EventContent.关闭)
                                {
                                    //TXTWriteHelper.WriteException("关闭警灯");
                                    bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.警灯, strArea, DeviceRunModel.自动, true, IsWait.CanStop);
                                    if (blRet)
                                    {
                                        lock (listRoom)
                                        {
                                            listRoom[iRoomIndex].StateOfAlarmLed = EventContent.关闭;
                                        }
                                    }
                                }

                                // 烘干 恢复自动
                                if (listRoom[iRoomIndex].roomHot.HandOrAuto == DeviceRunModel.烟雾报警)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomHot.HandOrAuto = DeviceRunModel.自动;
                                    }
                                }
                                // 除湿 恢复自动
                                if (listRoom[iRoomIndex].roomDehumi.HandOrAuto == DeviceRunModel.烟雾报警)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomDehumi.HandOrAuto = DeviceRunModel.自动;
                                    }
                                }
                                //新风 恢复自动
                                if (listRoom[iRoomIndex].roomFan.HandOrAuto == DeviceRunModel.烟雾报警)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.自动;
                                    }
                                }

                                #endregion
                            }

                            #endregion

                            #region 手动 恢复 自动控制

                            //烘干
                            if (listRoom[iRoomIndex].roomHot.HandOrAuto == DeviceRunModel.手动)
                            {
                                TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].roomHot.TimeHand;
                                if (ts.TotalMinutes >= clsEnvirSet.iHandHoldTime)//
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomHot.HandOrAuto = DeviceRunModel.自动;
                                    }
                                }
                            }
                            //除湿
                            if (listRoom[iRoomIndex].roomDehumi.HandOrAuto == DeviceRunModel.手动)
                            {
                                TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].roomDehumi.TimeHand;
                                if (ts.TotalMinutes >= clsEnvirSet.iHandHoldTime)// TotalHours  TotalSeconds
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomDehumi.HandOrAuto = DeviceRunModel.自动;
                                    }
                                }
                            }
                            //新风
                            if (listRoom[iRoomIndex].roomFan.HandOrAuto == DeviceRunModel.手动)
                            {
                                TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].roomFan.TimeHand;
                                if (ts.TotalMinutes >= clsEnvirSet.iHandHoldTime)// TotalHours  TotalSeconds
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.自动;
                                    }
                                }
                            }


                            //#region  恢复 自动控制

                            //if (blAskIo)
                            //{
                            //    //空调
                            //    if (listRoom[iRoomIndex].listAir != null)
                            //    {

                            //        for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listAir.Count; iIndex++)//房间内的 所有空调
                            //        {
                            //            if (blAskSensor == false)
                            //                break;
                            //            if (listRoom[iRoomIndex].listAir[iIndex] != null)
                            //            {

                            //            }
                            //        }
                            //    }


                            //}
                            //#endregion

                            #endregion
                        }
                    }
                }
                if (blAskIo && blControl==false )
                {
                    blIoFrist = false;
                }
                Thread.Sleep(100);
                if (timerIo.State == TimerState.Running)
                    timerIo.Start(); 
            }
            catch (Exception ex)
            {
                //TXTWriteHelper.WriteException(ex.ToString());
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }
        #endregion

        #region 计划任务执行
        public void ExecuteTask(int devicetype, OnOffRelay opertype)
        {
            try
            {
                blAskIo = false;
                for (int iRoomIndex = 0; iRoomIndex < listRoom.Count; iRoomIndex++)
                {
                    if (devicetype == 1)
                    {//新风系统
                        if (listRoom[iRoomIndex].roomRelay != null)
                        {
                            int iAddr = listRoom[iRoomIndex].roomRelay.IntAddr;
                            string strArea = listRoom[iRoomIndex].StrName;
                            if (listRoom[iRoomIndex].roomFan.State == DeviceRunState.停止 && opertype == OnOffRelay.开启)
                            {
                                bool blRet = false;
                                if (Is485XF == false)
                                {
                                    blRet = serialIo.OnOffDevice(iAddr, opertype, DeviceRelayNo.新风, strArea, DeviceRunModel.定时, true, IsWait.CanStop);
                                }
                                else
                                {
                                    blRet = serialXF.OperDevice(iAddr, opertype, strArea, DeviceRunModel.定时, true, IsWait.CanStop);
                                }
                                if (blRet)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomFan.State = DeviceRunState.运行;
                                    }
                                }
                            }
                            else if (listRoom[iRoomIndex].roomFan.State == DeviceRunState.运行 && opertype == OnOffRelay.关闭)
                            {
                                bool blRet = false;
                                if (Is485XF == false)
                                {
                                    blRet = serialIo.OnOffDevice(iAddr, opertype, DeviceRelayNo.新风, strArea, DeviceRunModel.定时, true, IsWait.CanStop);
                                }
                                else
                                {
                                    blRet = serialXF.OperDevice(iAddr, opertype, strArea, DeviceRunModel.定时, true, IsWait.CanStop);
                                }
                                if (blRet)
                                {
                                    lock (listRoom)
                                    {
                                        listRoom[iRoomIndex].roomFan.State = DeviceRunState.停止;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}





#region 恢复 自动控制

//#region  恢复 自动控制

//if (blAskIo)
//{
//    //空调
//    if (listRoom[iRoomIndex].listAir != null)
//    {

//        for (int iIndex = 0; iIndex < listRoom[iRoomIndex].listAir.Count; iIndex++)//房间内的 所有空调
//        {
//            if (blAskSensor == false)
//                break;
//            if (listRoom[iRoomIndex].listAir[iIndex] != null)
//            {
//                if (listRoom[iRoomIndex].listAir[iIndex].HandOrAuto == DeviceRunModel.手动)
//                {
//                    TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].listAir[iIndex].TimeHand;
//                    if (ts.TotalMinutes >= envirSet.IHandHoldTime)// TotalHours  TotalSeconds
//                    {
//                        lock (listRoom)
//                        {
//                            listRoom[iRoomIndex].listAir[iIndex].HandOrAuto = DeviceRunModel.自动;
//                        }
//                    }
//                }
//            }
//        }
//    }

//    //烘干
//    if (listRoom[iRoomIndex].roomHot.HandOrAuto == DeviceRunModel.手动)
//    {
//        TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].roomHot.TimeHand;
//        if (ts.TotalMinutes >= envirSet.IHandHoldTime)// TotalHours  TotalSeconds
//        {
//            lock (listRoom)
//            {
//                listRoom[iRoomIndex].roomHot.HandOrAuto = DeviceRunModel.自动;
//            }
//        }
//    }
//    //除湿
//    if (listRoom[iRoomIndex].roomDehumi.HandOrAuto == DeviceRunModel.手动)
//    {
//        TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].roomDehumi.TimeHand;
//        if (ts.TotalMinutes >= envirSet.IHandHoldTime)// TotalHours  TotalSeconds
//        {
//            lock (listRoom)
//            {
//                listRoom[iRoomIndex].roomDehumi.HandOrAuto = DeviceRunModel.自动;
//            }
//        }
//    }
//    //新风
//    if (listRoom[iRoomIndex].roomFan.HandOrAuto == DeviceRunModel.手动)
//    {
//        TimeSpan ts = DateTime.Now - listRoom[iRoomIndex].roomFan.TimeHand;
//        if (ts.TotalMinutes >= envirSet.IHandHoldTime)// TotalHours  TotalSeconds
//        {
//            lock (listRoom)
//            {
//                listRoom[iRoomIndex].roomFan.HandOrAuto = DeviceRunModel.自动;
//            }
//        }
//    }
//}
//#endregion

#endregion



///// <summary>
///// 关空调 及事件 存储
///// </summary>
///// <param name="iRoomIndex"></param>
///// <param name="iIndex"></param>
//private void OffAir(int iRoomIndex, int iIndex)
//{
//    bool blOff = false;
//    byte btAddr = (byte)listRoom[iRoomIndex].listAir[iIndex].IntAddr;
//    if (listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.大金)
//    {
//        blOff = serialSensor.DjAirControl(btAddr, DjControlType.开关, (byte)DjOnOff.关闭);
//    }
//    else if (listRoom[iRoomIndex].listAir[iIndex].AirType == AirFactoryType.其他)
//    {
//        blOff = serialSensor.OtherAirControl(btAddr, OtherAirControlType.开关, (byte)OtherAirOnOff.关闭);
//    }
//    if (blOff)
//    {
//        string strSql = "insert into tb_EnviriEvent (Type,Area,Reason,EventContent,Time)values " +
//       "('" + EventType.空调.ToString() + "','" + listRoom[iRoomIndex].StrName + "','" + DeviceRunModel.自动.ToString() + "'," +
//       "'" + EventContent.关闭.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//        datalogic.SqlComNonQuery(strSql);
//        lock (listRoom)
//        {
//            listRoom[iRoomIndex].listAir[iIndex].State = DeviceRunState.停止;
//        }
//        DataRow dr = MainControl.dtNewEvent.NewRow();
//        dr["Type"] = EventType.空调.ToString();
//        dr["Point"] = listRoom[iRoomIndex].StrName;
//        dr["Content"] = EventContent.关闭.ToString();
//        dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
//       MainControl. dtNewEvent.Rows.Add(dr);
//    }
//}
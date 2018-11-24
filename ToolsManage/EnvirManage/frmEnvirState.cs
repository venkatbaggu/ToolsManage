using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ToolsManage.BaseClass;
using ToolsManage.BaseClass.EnvirClass;
using ToolsManage.BaseClass.SeralClass;

using System.Threading;
using ToolsManage.Domain;

namespace ToolsManage.EnvirManage
{
    public partial class frmEnvirState : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        clsSerialSensor serialSensor = new clsSerialSensor();

        public clsSerialIo serialIo = new clsSerialIo();
        public clsSerialAir serialAir = new clsSerialAir();//空调

        private const int ciClick = 30;
        private int iIndex = 0;
        //private int iAirIndex = 0;

        /// <summary>
        /// 手动操作时 不定时刷新数据
        /// </summary>
        private int iAirClick = 0;
        private int iHotClick = 0;
        private int iDehumiClick = 0;
        private int iFanClick = 0;

        public frmEnvirState()
        {
            InitializeComponent();
        }

        private void frmEnvirState_Load(object sender, EventArgs e)
        {
            LoadRoomName();
        }

        private void LoadRoomName()
        {
            //string strSql = "select RoomName from tb_RoomInfo ";
            //DataTable dt = datalogic.GetDataTable(strSql);
            IList<TbRoominfo> list = TbRoominfo.FindAll("","ID ASC","",0,0);
            this.cbbRoomName.SelectedIndexChanged -= new System.EventHandler(this.cbbRoomName_SelectedIndexChanged);
            try
            {
                if(list != null && list.Count>0)
                {
                    cbbRoomName.DataSource = list;
                    cbbRoomName.DisplayMember = "RoomName";
                    cbbRoomName.ValueMember = "ID";                    
                }
                this.cbbRoomName.SelectedIndexChanged += new System.EventHandler(this.cbbRoomName_SelectedIndexChanged);
                if (cbbRoomName.Items.Count > 0)
                {
                    cbbRoomName.SelectedIndex = 0;
                    LoadRoomDeviceInfo(Convert.ToInt16(cbbRoomName.SelectedValue));
                }
                //foreach (DataRow dr in dt.Rows)
                //{
                //    string strName = dr["RoomName"].ToString();
                //    cbbRoomName.Items.Add(strName);
                //}
                //if (cbbRoomName.Items.Count > 0)
                //    cbbRoomName.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                if (frmMain.blDebug)
                {
                    MessageUtil.ShowTips(ex.Message);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region

            try
            {
                //digitalGauge5.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (iAirClick > 0)
                    iAirClick--;
                if (iHotClick > 0)
                    iHotClick--;
                if (iDehumiClick > 0)
                    iDehumiClick--;
                if (iFanClick > 0)
                    iFanClick--;

                if (clsEnvirControl.listRoom.Count > 0)
                {
                    if (tbFire.Text != clsEnvirControl.listRoom[iIndex].FireState.ToString())
                    {
                        tbFire.Text = clsEnvirControl.listRoom[iIndex].FireState.ToString();
                    }
                    if (clsEnvirControl.listRoom[iIndex].listWsd.Count == 2)
                    {
                        //温湿度1
                        if (gauLedTemp1.Text != clsEnvirControl.listRoom[iIndex].listWsd[0].IntTemp.ToString())
                        {
                            gauLedTemp1.Text = clsEnvirControl.listRoom[iIndex].listWsd[0].IntTemp.ToString();
                            pointerTemp1.Value = clsEnvirControl.listRoom[iIndex].listWsd[0].IntTemp;
                        }
                        if (gauLedHumi1.Text != clsEnvirControl.listRoom[iIndex].listWsd[0].IntHumi.ToString())
                        {
                            gauLedHumi1.Text = clsEnvirControl.listRoom[iIndex].listWsd[0].IntHumi.ToString();
                            pointerHumi1.Value = clsEnvirControl.listRoom[iIndex].listWsd[0].IntHumi;
                        }
                        //温湿度2
                        if (gauLedTemp2.Text != clsEnvirControl.listRoom[iIndex].listWsd[1].IntTemp.ToString())
                        {
                            gauLedTemp2.Text = clsEnvirControl.listRoom[iIndex].listWsd[1].IntTemp.ToString();
                            pointerTemp2.Value = clsEnvirControl.listRoom[iIndex].listWsd[1].IntTemp;
                        }
                        if (gauLedHumi2.Text != clsEnvirControl.listRoom[iIndex].listWsd[1].IntHumi.ToString())
                        {
                            gauLedHumi2.Text = clsEnvirControl.listRoom[iIndex].listWsd[1].IntHumi.ToString();
                            pointerHumi2.Value = clsEnvirControl.listRoom[iIndex].listWsd[1].IntHumi;
                        }
                    }
                    //空调
                    if (iAirClick <= 0)
                    {
                        if (clsEnvirControl.listRoom[iIndex].listAir != null)
                        {
                            if (clsEnvirControl.listRoom[iIndex].listAir[0] != null)
                            {
                                if (cbbAirOnOff.Text != clsEnvirControl.listRoom[iIndex].listAir[0].State.ToString())
                                {
                                    cbbAirOnOff.Text = clsEnvirControl.listRoom[iIndex].listAir[0].State.ToString();
                                    //if (cbbAirOnOff.Text == DeviceRunState.运行.ToString())
                                    //    stateAir.StateIndex = 3;
                                    //else
                                    //    stateAir.StateIndex = 1;
                                }
                                if (clsEnvirControl.listRoom[iIndex].listAir[0].State == DeviceRunState.运行 )
                                { 
                                    if (stateAir.StateIndex != 3)
                                        stateAir.StateIndex = 3;
                                }
                                if (clsEnvirControl.listRoom[iIndex].listAir[0].State == DeviceRunState.停止 )
                                {
                                    if (stateAir.StateIndex != 1)
                                        stateAir.StateIndex = 1;
                                }
                                if (cbbModel.Text != clsEnvirControl.listRoom[iIndex].listAir[0].Model.ToString())
                                {
                                    cbbModel.Text = clsEnvirControl.listRoom[iIndex].listAir[0].Model.ToString();
                                }
                                if (tbAirTemp.Text != clsEnvirControl.listRoom[iIndex].listAir[0].IntTempSet.ToString())
                                {
                                    tbAirTemp.Text = clsEnvirControl.listRoom[iIndex].listAir[0].IntTempSet.ToString();
                                }
                            }
                        }
                        //for (int iIndexAir = 0; iIndexAir < clsEnvirControl.listRoom[iIndex].listAir.Count; iIndexAir++)//房间内的 所有空调
                        //{
                           
                        //}

                    }
                    //烘干
                    if (iHotClick <= 0)
                    {
                        if (tbHot.Text != clsEnvirControl.listRoom[iIndex].roomHot.State.ToString())
                        {
                            tbHot.Text = clsEnvirControl.listRoom[iIndex].roomHot.State.ToString();
                            if (tbHot.Text == DeviceRunState.运行.ToString())
                            {
                                sbtnHot.Text = "关闭";
                                stateHot.StateIndex = 3; 
                            }
                            else
                            {
                                sbtnHot.Text = "开启";
                                stateHot.StateIndex = 1; 
                            }
                        }
                    }
                    //除湿
                    if (iDehumiClick <= 0)
                    {
                        if (tbDehumi.Text != clsEnvirControl.listRoom[iIndex].roomDehumi.State.ToString())
                        {
                            tbDehumi.Text = clsEnvirControl.listRoom[iIndex].roomDehumi.State.ToString();
                            if (tbDehumi.Text == DeviceRunState.运行.ToString())
                            {
                                sbtnDehumi.Text = "关闭";
                                stateHumi.StateIndex = 3; 
                            }
                            else
                            {
                                sbtnDehumi.Text = "开启";
                                stateHumi.StateIndex = 1; 
                            }
                        }
                    }
                    //新风
                    if (iFanClick <= 0)
                    {
                        if (tbFan.Text != clsEnvirControl.listRoom[iIndex].roomFan.State.ToString())
                        {
                            tbFan.Text = clsEnvirControl.listRoom[iIndex].roomFan.State.ToString();
                            if (tbFan.Text == DeviceRunState.运行.ToString())
                            {
                                sbtnFan.Text = "关闭";
                                stateFan.StateIndex = 3; 
                            }
                            else
                            {
                                sbtnFan.Text = "开启";
                                stateFan.StateIndex = 1; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                {
                    MessageUtil.ShowTips(ex.Message);
                }
            }

            #endregion
        }

        private void LoadRoomDeviceInfo(int roomid)
        {
            TbRoominfo roominfo = TbRoominfo.Find("ID=" + roomid);
            if (roominfo != null)
            {//查找到对应的房间
                //温湿度
                if (roominfo.WSDCount > 0)
                {
                    gcWSD1.Visible = roominfo.WSDCount > 0 ? true : false;
                    gcWSD2.Visible = roominfo.WSDCount >= 2 ? true : false;
                }
                else
                {
                    gcWSD1.Visible = gcWSD2.Visible = false;
                }
                //烟感
                if (roominfo.YGCount > 0)
                {
                    pnlYG.Visible = true;
                }
                else
                {
                    pnlYG.Visible = false;
                }
                //控制设备
                gbCSJ.Visible = roominfo.IsExistCsj == DeviceUsing.启用.ToString() ? true : false;
                gbJRQ.Visible = roominfo.IsExistJrq == DeviceUsing.启用.ToString() ? true : false;
                gbXF.Visible = roominfo.IsExistXf == DeviceUsing.启用.ToString() ? true : false;
                //空调
                if (roominfo.KTCount > 0)
                {
                    gbKT.Visible = true;
                }
                else
                {
                    gbKT.Visible = false;
                }
            }
        }
      
        private void cbbRoomName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strRoomName = cbbRoomName.Text;
            int roomid = Convert.ToInt16(cbbRoomName.SelectedValue);
            LoadRoomDeviceInfo(roomid);

            for (int iRoomIndex = 0; iRoomIndex < clsEnvirControl.listRoom.Count; iRoomIndex++)
            {
                if (clsEnvirControl.listRoom[iRoomIndex].StrName == strRoomName)
                {
                    iIndex = iRoomIndex;
                    break;
                }
            }
        }

        private void tbFire_TextChanged(object sender, EventArgs e)
        {
            if (tbFire.Text == ProbeState.报警.ToString())
                tbFire.ForeColor = Color.Red;
            else
                tbFire.ForeColor = Color.Black;
        }

        private void tbHot_TextChanged(object sender, EventArgs e)
        {
            if (tbHot.Text == DeviceRunState .运行 .ToString ())
                tbHot.ForeColor = Color.Red;
            else
                tbHot.ForeColor = Color.Black;
        }

        private void tbDehumi_TextChanged(object sender, EventArgs e)
        {
            if (tbDehumi.Text == DeviceRunState.运行.ToString())
                tbDehumi.ForeColor = Color.Red;
            else
                tbDehumi.ForeColor = Color.Black;
        }

        private void tbFan_TextChanged(object sender, EventArgs e)
        {
            if (tbFan.Text == DeviceRunState.运行.ToString())
                tbFan.ForeColor = Color.Red;
            else
                tbFan.ForeColor = Color.Black;
        }

        private void cbbAirOnOff_TextChanged(object sender, EventArgs e)
        {
            if (cbbAirOnOff.Text == DeviceRunState.运行.ToString())
                cbbAirOnOff.ForeColor = Color.Red;
            else
                cbbAirOnOff.ForeColor = Color.Black;
        }

        private void cbbAirOnOff_Click(object sender, EventArgs e)
        {
            iAirClick = 30;
        }

        private void cbbModel_Click(object sender, EventArgs e)
        {
            iAirClick = 30;
        }

        private void tbAirTemp_Click(object sender, EventArgs e)
        {
            iAirClick = 30;
        }

        private void sbtnAirSet_Click(object sender, EventArgs e)
        {
            string strOnOff = cbbAirOnOff.Text;
            string strMode = cbbModel.Text;
            string strTemp = tbAirTemp.Text;
            if (string.IsNullOrEmpty(strOnOff))
            {
                MessageUtil.ShowError("空调启停不可为空！");
                return;
            }
            if (string.IsNullOrEmpty(strMode))
            {
                MessageUtil.ShowError("空调模式不可为空！");
                return;
            }
            if (string.IsNullOrEmpty(strTemp))
            {
                MessageUtil.ShowError("空调设置温度不可为空！");
                return;
            }

            sbtnAirSet.Enabled = false;
            clsEnvirControl.blAskAir = false;
            iAirClick = 30;
            Thread.Sleep(600);

            #region  空调控制

            DeviceRunState state = DeviceRunState.停止;
            EventContent OnOff = EventContent.关闭  ;
            if (cbbAirOnOff.Text == DeviceRunState.停止.ToString())
            {
                OnOff = EventContent.关闭;
                state = DeviceRunState.停止;
            }
            else if (cbbAirOnOff.Text == DeviceRunState.运行.ToString())
            {
                OnOff = EventContent.开启;
                state = DeviceRunState.运行;
            }

            AirRunModel airRunModel = AirRunModel.制冷;
            EventContent model = EventContent.设置制冷 ;
            if (cbbModel.Text == AirRunModel.制冷.ToString())
            {
                airRunModel = AirRunModel.制冷;
                model = EventContent.设置制冷;
            }
            else if (cbbModel.Text == AirRunModel.制热.ToString())
            {
                airRunModel = AirRunModel.制热;
                model = EventContent.设置制热;
            }
            byte bTemp = Convert.ToByte(tbAirTemp.Text);

            string strArea = clsEnvirControl.listRoom[iIndex].StrName;
            bool blMoreAir = false;//是否 为 一个房间多台空调
            if (clsEnvirControl.listRoom[iIndex].listAir.Count > 1)
                blMoreAir = true;
            for (int iIndexAir = 0; iIndexAir <clsEnvirControl. listRoom[iIndex].listAir.Count; iIndexAir++)//房间内的 所有空调
            {
                #region  启停

                if (clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].State != state)
                {
                    AirFactoryType factory = clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].AirType;
                    byte addr = (byte)clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].IntAddr;
                    bool blRet = serialAir.AirControl(factory, addr, OnOff, 26, strArea, DeviceRunModel.手动, blMoreAir, IsWait.OnlyWait);
                    if (blRet)
                    {
                        lock (clsEnvirControl.listRoom)
                        {
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].State = state;
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].HandOrAuto = DeviceRunModel.手动;
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].TimeHand = DateTime.Now;
                        }
                    }
                    else
                    {
                        MessageUtil.ShowTips(addr.ToString() + "#空调启停设置失败！");
                        iAirClick = 0;
                        clsEnvirControl.blAskAir = true;
                        sbtnAirSet.Enabled = true;
                        return;
                    }
                }

                #endregion

                #region  模式

                if (clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].Model != airRunModel)
                {
                    AirFactoryType factory = clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].AirType;
                    byte addr = (byte)clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].IntAddr;
                    bool blRet = serialAir.AirControl(factory, addr, model, 26, strArea, DeviceRunModel.自动, blMoreAir, IsWait.OnlyWait);
                    if (blRet)
                    {
                        lock (clsEnvirControl.listRoom)
                        {
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].Model = airRunModel;
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].HandOrAuto = DeviceRunModel.手动;
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].TimeHand = DateTime.Now;
                        }
                    }
                    else
                    {
                        MessageUtil.ShowTips(addr.ToString() + "# 空调模式设置失败！");
                        iAirClick = 0;
                        clsEnvirControl.blAskAir = true;
                        sbtnAirSet.Enabled = true;
                        return;
                    }
                }

                #endregion

                #region  温度

                if (clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].IntTempSet != bTemp)
                {
                    AirFactoryType factory = clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].AirType;
                    byte addr = (byte)clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].IntAddr;
                    bool blRet = serialAir.AirControl(factory, addr, EventContent.设置温度, bTemp, strArea, DeviceRunModel.自动, blMoreAir, IsWait.OnlyWait );
                    if (blRet)
                    {
                        lock (clsEnvirControl.listRoom)
                        {
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].IntTempSet = clsEnvirSet.intSetTempCool;
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].HandOrAuto = DeviceRunModel.手动;
                            clsEnvirControl.listRoom[iIndex].listAir[iIndexAir].TimeHand = DateTime.Now;
                        }
                    }
                    else
                    {
                        MessageUtil.ShowTips(addr.ToString() + "# 空调温度设置失败！");
                        iAirClick = 0;
                        clsEnvirControl.blAskAir = true;
                        sbtnAirSet.Enabled = true;
                        return;
                    }
                }


                #endregion

            }
            #endregion
            iAirClick = 0;
            clsEnvirControl.blAskAir = true;
            sbtnAirSet.Enabled = true;
        }

        private void sbtnHot_Click(object sender, EventArgs e)
        {
            sbtnHot.Enabled = false;
            clsEnvirControl.blAskIo   = false;
            iHotClick = 30;
            Thread.Sleep(600);
            if (sbtnHot.Text == "开启")
            {
                int iAddr = clsEnvirControl.listRoom[iIndex].roomRelay.IntAddr;
                string strArea = clsEnvirControl.listRoom[iIndex].StrName;
                bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.开启 , DeviceRelayNo.烘干 , strArea, DeviceRunModel.手动, true,IsWait.OnlyWait );
                if (blRet)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iIndex].roomHot.State = DeviceRunState.运行;
                        clsEnvirControl.listRoom[iIndex].roomHot.HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iIndex].roomHot.TimeHand = DateTime.Now;
                    }
                }
            }
            else
            {
                int iAddr = clsEnvirControl.listRoom[iIndex].roomRelay.IntAddr;
                string strArea = clsEnvirControl.listRoom[iIndex].StrName;
                bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.烘干, strArea, DeviceRunModel.手动, true, IsWait.OnlyWait);
                if (blRet)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iIndex].roomHot.State = DeviceRunState.停止;
                        clsEnvirControl.listRoom[iIndex].roomHot.HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iIndex].roomHot.TimeHand = DateTime.Now;
                    }
                }
            }
            iHotClick = 0;
            clsEnvirControl.blAskIo = true;
            sbtnHot.Enabled = true; 
        }

        private void sbtnDehumi_Click(object sender, EventArgs e)
        {
            sbtnDehumi.Enabled = false;
            clsEnvirControl.blAskIo = false;
            iDehumiClick = 30;
            Thread.Sleep(600);
            if (sbtnDehumi.Text == "开启")
            {
                int iAddr = clsEnvirControl.listRoom[iIndex].roomRelay.IntAddr;
                string strArea = clsEnvirControl.listRoom[iIndex].StrName;
                bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.开启, DeviceRelayNo.除湿, strArea, DeviceRunModel.手动, true, IsWait.OnlyWait);
                if (blRet)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iIndex].roomDehumi.State = DeviceRunState.运行;
                        clsEnvirControl.listRoom[iIndex].roomDehumi.HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iIndex].roomDehumi.TimeHand = DateTime.Now;
                    }
                }
            }
            else
            {
                int iAddr = clsEnvirControl.listRoom[iIndex].roomRelay.IntAddr;
                string strArea = clsEnvirControl.listRoom[iIndex].StrName;
                bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.除湿, strArea, DeviceRunModel.手动, true, IsWait.OnlyWait);
                if (blRet)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iIndex].roomDehumi.State = DeviceRunState.停止;
                        clsEnvirControl.listRoom[iIndex].roomDehumi.HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iIndex].roomDehumi.TimeHand = DateTime.Now;
                    }
                }
            }
            iDehumiClick = 0;
            clsEnvirControl.blAskIo = true;
            sbtnDehumi.Enabled = true; 
        }

        private void sbtnFan_Click(object sender, EventArgs e)
        {
            sbtnFan.Enabled = false;
            clsEnvirControl.blAskIo = false;
            iFanClick = 30;
            Thread.Sleep(600);
            if (sbtnFan.Text == "开启")
            {
                int iAddr = clsEnvirControl.listRoom[iIndex].roomRelay.IntAddr;
                string strArea = clsEnvirControl.listRoom[iIndex].StrName;
                bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.开启, DeviceRelayNo.新风, strArea, DeviceRunModel.手动, true, IsWait.OnlyWait);
                if (blRet)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iIndex].roomFan.State = DeviceRunState.运行;
                        clsEnvirControl.listRoom[iIndex].roomFan.HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iIndex].roomFan.TimeHand = DateTime.Now;
                    }
                }
            }
            else
            {
                int iAddr = clsEnvirControl.listRoom[iIndex].roomRelay.IntAddr;
                string strArea = clsEnvirControl.listRoom[iIndex].StrName;
                bool blRet = serialIo.OnOffDevice(iAddr, OnOffRelay.关闭, DeviceRelayNo.新风, strArea, DeviceRunModel.手动, true, IsWait.OnlyWait);
                if (blRet)
                {
                    lock (clsEnvirControl.listRoom)
                    {
                        clsEnvirControl.listRoom[iIndex].roomFan.State = DeviceRunState.停止;
                        clsEnvirControl.listRoom[iIndex].roomFan.HandOrAuto = DeviceRunModel.手动;
                        clsEnvirControl.listRoom[iIndex].roomFan.TimeHand = DateTime.Now;
                    }
                }
            }
            iFanClick = 0;
            clsEnvirControl.blAskIo = true;
            sbtnFan.Enabled = true; 
        }

        private void tbAirTemp_MouseClick(object sender, MouseEventArgs e)
        {
            iAirClick = 30;
        }
    }
}
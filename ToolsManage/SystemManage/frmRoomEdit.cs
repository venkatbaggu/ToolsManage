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
using ToolsManage.BaseClass.SeralClass;
using ToolsManage.Domain;

namespace ToolsManage.SystemManage
{
    public partial class frmRoomEdit : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        clsSerialAir serial = new clsSerialAir();//空调
        private TbRoominfo roomInfo = null;

        public string strID = "";
        public string strName = "";
        public string strWsd1 = "";
        public string strWsd2 = "";
        public string strFire1 = "";
        public string strFire2 = "";
        public string strRelay = "";
        public string strAir = "";
        public string strFactory = "";
        public string strAir2 = "";
        public string strFactory2 = "";
        private bool m_IsExistCsj = false;
        private bool m_IsExistJrq = false;
        private bool m_IsExistXf = false;

        public bool IsExistCsj
        {
            get
            {
                return m_IsExistCsj;
            }

            set
            {
                m_IsExistCsj = value;
            }
        }

        public bool IsExistJrq
        {
            get
            {
                return m_IsExistJrq;
            }

            set
            {
                m_IsExistJrq = value;
            }
        }

        public bool IsExistXf
        {
            get
            {
                return m_IsExistXf;
            }

            set
            {
                m_IsExistXf = value;
            }
        }

        public frmRoomEdit(TbRoominfo _roomInfo)
        {
            InitializeComponent();
            roomInfo = _roomInfo;
        }

        private void InitDeviceCount()
        {
            for(int i=0;i<3;i++)
            {
                cmbYG.Items.Add(i);
                cmbWSD.Items.Add(i);
                cmbKT.Items.Add(i);
            }
            cmbYG.SelectedIndex = 0;
            cmbWSD.SelectedIndex = 0;
            cmbKT.SelectedIndex = 0;
        }

        private void frmRoomEdit_Load(object sender, EventArgs e)
        {
            InitDeviceCount();
            if (this.Tag.ToString() == "添加")
            {
                if (roomInfo == null)
                    roomInfo = new TbRoominfo();
                this.Text = "添加房间设备信息";
            }
            else if (this.Tag.ToString() == "修改")
            {
                this.Text = "修改房间设备信息";
                LoadRoomInfo();
                //tbName.Text = strName;
                //tbWsd2.Text = strWsd2;
                //tbFire2.Text = strFire2;
                //tbWsd1.Text = strWsd1;
                //tbFire1.Text = strFire1;
                //tbRelayAddr.Text = strRelay;
                //tbAirAddr.Text = strAir;
                //cbbAirFactory.Text = strFactory;
                //tbAirAddr2.Text = strAir2;
                //cbbAirFactory2.Text = strFactory2;
                //chbCSJ.Checked = IsExistCsj;
                //chbJRQ.Checked = IsExistJrq;
                //chbXF.Checked = IsExistXf;
            } 
        }

        private void LoadRoomInfo()
        {
            tbName.Text = roomInfo.RoomName;
            cmbYG.SelectedIndex = roomInfo.YGCount;
            cmbWSD.SelectedIndex = roomInfo.WSDCount;
            cmbKT.SelectedIndex = roomInfo.KTCount;
            tbFire1.Text = roomInfo.FireAddr1;
            tbFire2.Text = roomInfo.FireAddr2;
            tbWsd1.Text = roomInfo.WsdAddr1;
            tbWsd2.Text = roomInfo.WsdAddr2;
            tbRelayAddr.Text = roomInfo.RelayAddr;
            tbAirAddr.Text = roomInfo.AirAddr;
            cbbAirFactory.Text = roomInfo.AirFactory;
            tbAirAddr2.Text = roomInfo.AirAddr2;            
            cbbAirFactory2.Text = roomInfo.AirFactory2;
            chbJDQ.Checked = roomInfo.IsExistRealy == DeviceUsing.启用.ToString() ? true : false;
            chbCSJ.Checked= roomInfo.IsExistCsj == DeviceUsing.启用.ToString() ? true : false;
            chbJRQ.Checked = roomInfo.IsExistJrq == DeviceUsing.启用.ToString() ? true : false;
            chbXF.Checked = roomInfo.IsExistXf == DeviceUsing.启用.ToString() ? true : false;
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            #region  输入 判断
            if (string.IsNullOrEmpty(tbName.Text.Trim()))
            {
                MessageUtil.ShowTips("房间名称不能为空！");
                return;
            }
            if (cmbWSD.Text == "1" || cmbWSD.Text == "2")
            {
                if (string.IsNullOrEmpty(tbWsd1.Text.Trim()))
                {
                    MessageUtil.ShowTips("请输入1#温湿度地址！");
                    return;
                }
            }
            if (cmbWSD.Text == "2")
            {
                if (string.IsNullOrEmpty(tbWsd2.Text.Trim()))
                {
                    MessageUtil.ShowTips("请输入2#温湿度地址！");
                    return;
                }
            }
            if (cmbYG.Text == "1" || cmbYG.Text == "2")
            {
                if (string.IsNullOrEmpty(tbFire1.Text.Trim()))
                {
                    MessageUtil.ShowTips("请输入1#烟感地址！");
                    return;
                }
            }
            if (cmbYG.Text == "2")
            {
                if (string.IsNullOrEmpty(tbFire2.Text.Trim()))
                {
                    MessageUtil.ShowTips("请输入2#烟感地址！");
                    return;
                }
            }
            if (chbJDQ.Checked)
            {
                if (string.IsNullOrEmpty(tbRelayAddr.Text.Trim()))
                {
                    MessageUtil.ShowTips("请输入控制板地址！");
                    return;
                }
            }
            if (cmbKT.Text == "1" || cmbKT.Text == "2")
            {
                if (string.IsNullOrEmpty(tbAirAddr.Text.Trim()))
                {
                    MessageUtil.ShowTips("请输入1#空调控制器地址！");
                    return;
                }
            }
            if (cmbKT.Text == "1" || cmbKT.Text == "2")
            {
                if (string.IsNullOrEmpty(cbbAirFactory.Text))
                {
                    MessageUtil.ShowTips("请选择1#空调厂家！");
                    return;
                }
            }
            if (cmbKT.Text == "2")
            {
                if (string.IsNullOrEmpty(tbAirAddr2.Text.Trim()))
                {
                    MessageUtil.ShowTips("请输入2#空调控制器地址！");
                    return;
                }
            }
            if (cmbKT.Text == "2")
            {
                if (string.IsNullOrEmpty(cbbAirFactory2.Text))
                {
                    MessageUtil.ShowTips("请选择2#空调厂家！");
                    return;
                }
            }

            if (strFire1.Length < 2)
            {
                strFire1 = "0" + strFire1;
            }
            if (strFire2.Length < 2)
            {
                strFire2 = "0" + strFire2;
            }

            roomInfo.RoomName = tbName.Text.Trim();
            roomInfo.WsdAddr1= tbWsd1.Text.Trim();
            roomInfo.FireAddr1= tbFire1.Text.Trim();
            roomInfo.WsdAddr2= tbWsd2.Text.Trim();
            roomInfo.FireAddr2= tbFire2.Text.Trim();
            roomInfo.RelayAddr= tbRelayAddr.Text.Trim();
            roomInfo.AirAddr = tbAirAddr.Text.Trim();
            roomInfo.AirFactory= cbbAirFactory.Text;
            roomInfo.AirAddr2= tbAirAddr2.Text.Trim();
            roomInfo.AirFactory2= cbbAirFactory2.Text;
            roomInfo.IsExistCsj = chbCSJ.Checked ? DeviceUsing.启用.ToString() : DeviceUsing.未启用.ToString();
            roomInfo.IsExistJrq= chbJRQ.Checked ? DeviceUsing.启用.ToString() : DeviceUsing.未启用.ToString();
            roomInfo.IsExistXf = chbXF.Checked ? DeviceUsing.启用.ToString() : DeviceUsing.未启用.ToString();
            roomInfo.IsExistRealy = chbJDQ.Checked ? DeviceUsing.启用.ToString() : DeviceUsing.未启用.ToString();
            roomInfo.YGCount = Convert.ToInt16(cmbYG.Text);
            roomInfo.WSDCount = Convert.ToInt16(cmbWSD.Text);
            roomInfo.KTCount = Convert.ToInt16(cmbKT.Text);

            #endregion
            if (this.Text == "添加房间设备信息")//tb_RoomInfo ID,RoomName,IsSingle,WsdAddr1,WsdAddr2,FireAddr1,FireAddr2,RelayAddr
            {
                //string strSql = "insert into tb_RoomInfo (RoomName,WsdAddr1,WsdAddr2,FireAddr1,FireAddr2,RelayAddr,AirAddr,AirFactory"+
                //    ",AirAddr2,AirFactory2)" +
                //"values ('" + strName + "','" + strWsd1 + "','" + strWsd2 + "','" + strFire1 + "','" + strFire2 + "','" + strRelay + "'"+
                //  ",'" + strAir + "','" + strFactory + "','" + strAir2 + "','" + strFactory2 + "')";
                //datalogic.SqlComNonQuery(strSql);
                int count= TbRoominfo.Insert(roomInfo);
                if (count > 0)
                {
                    MessageUtil.ShowTips("添加房间设备信息成功！");
                }
                else
                {
                    MessageUtil.ShowWarning("添加房间设备信息失败！");
                }
            }
            else if (this.Text == "修改房间设备信息")
            {
                //string strSql = "update tb_RoomInfo set RoomName='" + strName + "',WsdAddr1='" + strWsd1 + "',AirAddr='" + strAir + "'," +
                //                "WsdAddr2='" + strWsd2 + "' ,FireAddr1='" + strFire1 + "',FireAddr2='" + strFire2 + "',RelayAddr='" + strRelay + "' " +
                //                ",AirFactory='" + strFactory + "',AirAddr2='" + strAir2 + "',AirFactory2='" + strFactory2 + "' "+
                //                "where ID='" + strID + "' ";
                //datalogic.SqlComNonQuery(strSql);
                int count = TbRoominfo.Update(roomInfo);
                if (count > 0)
                {
                    MessageUtil.ShowTips("修改房间设备信息成功！");
                }
                else
                {
                    MessageUtil.ShowWarning("区域设备没有数据更新！");
                }
            }
            this.Close();
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbAirFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAirFactory.Text == "大金")
            {
                lblCode.Visible = false;
                tbAirCode.Visible = false;
                sbtnAirSet.Visible = false;
            }
            else
            {
                lblCode.Visible = true ;
                tbAirCode.Visible = true;
                sbtnAirSet.Visible = true;
            }
        }

        private void sbtnAirSet_Click(object sender, EventArgs e)
        {
            string strAirAddr = tbAirAddr.Text;
            string strAirCode = tbAirCode.Text;
            string strFac = cbbAirFactory.Text;

            if (string.IsNullOrEmpty(strAirAddr))
            {
                MessageUtil.ShowTips("空调控制器地址不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(strFac))
            {
                MessageUtil.ShowTips("空调厂家不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(strAirCode))
            {
                MessageUtil.ShowTips("空调库码不能为空！");
                return;
            }

            sbtnAirSet.Enabled = false;

            try
            {
                byte bFactory = GetAirFactory(strFac);
                byte bAddr = Convert.ToByte(strAirAddr);
                int iCode = Convert.ToInt32(strAirCode);
                bool blRet = serial.OtherAirSetType(bAddr, bFactory, iCode,IsWait .OnlyWait );
                if (blRet)
                {
                    MessageUtil.ShowTips("设置成功！");
                }
                else
                {
                    MessageUtil.ShowTips("设置失败！");
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            } 

            sbtnAirSet.Enabled = true;
        }

        private byte GetAirFactory(string strFactory)
        {
            byte bFactory = 0;
            switch (strFactory)
            {
                case "格力":
                    bFactory = 1;
                    break;

                case "海尔":
                    bFactory = 2;
                    break;

                case "美的":
                    bFactory = 3;
                    break;

                case "长虹":
                    bFactory = 4;
                    break;

                case "志高":
                    bFactory = 5;
                    break;

                case "华宝":
                    bFactory = 6;
                    break;

                case "科龙":
                    bFactory = 7;
                    break;

                case "TCL":
                    bFactory = 8;
                    break;

                case "格兰仕":
                    bFactory = 9;
                    break;

                case "华凌":
                    bFactory = 10;
                    break;

                case "春兰":
                    bFactory = 11;
                    break;

                case "新科":
                    bFactory = 12;
                    break;

                case "新飞":
                    bFactory = 13;
                    break;

                case "海信":
                    bFactory = 14;
                    break;

                default:
                    break;
            }
            return bFactory;
        }

        private void cbbAirFactory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAirFactory2.Text == "大金")
            {
                lblCode2.Visible = false;
                tbAirCode2.Visible = false;
                sbtnAirSet2.Visible = false;
            }
            else
            {
                lblCode2.Visible = true;
                tbAirCode2.Visible = true;
                sbtnAirSet2.Visible = true;
            }
        }

        private void sbtnAirSet2_Click(object sender, EventArgs e)
        {
            string strAirAddr = tbAirAddr2.Text;
            string strAirCode = tbAirCode2.Text;
            string strFac = cbbAirFactory2.Text;

            if (string.IsNullOrEmpty(strAirAddr))
            {
                MessageUtil.ShowTips("空调控制器地址不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(strFac))
            {
                MessageUtil.ShowTips("空调厂家不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(strAirCode))
            {
                MessageUtil.ShowTips("空调库码不能为空！");
                return;
            }

            sbtnAirSet.Enabled = false;

            try
            {
                byte bFactory = GetAirFactory(strFac);
                byte bAddr = Convert.ToByte(strAirAddr);
                int iCode = Convert.ToInt32(strAirCode);
                bool blRet = serial.OtherAirSetType(bAddr, bFactory, iCode, IsWait.OnlyWait);
                if (blRet)
                {
                    MessageUtil.ShowTips("设置成功！");
                }
                else
                {
                    MessageUtil.ShowTips("设置失败！");
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }

            sbtnAirSet.Enabled = true;
        }

        private void cmbYG_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbYG.SelectedIndex)
            {
                case 0:
                    gbYG.Enabled = false;
                    break;
                case 1:
                    gbYG.Enabled = true;
                    pnlYG1.Enabled = true;
                    pnlYG2.Enabled = false;
                    break;
                case 2:
                    gbYG.Enabled = true;
                    pnlYG1.Enabled = true;
                    pnlYG2.Enabled = true;
                    break;
            }
        }

        private void cmbWSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbWSD.SelectedIndex)
            {
                case 0:
                    gbWSD.Enabled = false;
                    break;
                case 1:
                    gbWSD.Enabled = true;
                    pnlWSD1.Enabled = true;
                    pnlWSD1.Enabled = false;
                    break;
                case 2:
                    gbWSD.Enabled = true;
                    pnlWSD1.Enabled = true;
                    pnlWSD1.Enabled = true;
                    break;
            }
        }

        private void cmbKT_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbKT.SelectedIndex)
            {
                case 0:
                    gbKT.Enabled = false;
                    break;
                case 1:
                    gbKT.Enabled = true;
                    gbKT1.Enabled = true;
                    gbKT2.Enabled = false;
                    break;
                case 2:
                    gbKT.Enabled = true;
                    gbKT1.Enabled = true;
                    gbKT2.Enabled = true;
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolsManage.Domain;
using ToolsManage.UCControlNew;

namespace ToolsManage.SystemManage
{
    public partial class frmDevicePlan : Form
    {
        List<UCTimeInterval> listUCTI = new List<UCTimeInterval>();
        IList<TbDeviceplan> listDevicePlan = null;
        public frmDevicePlan()
        {
            InitializeComponent();
        }

        private void GetTimeGroup()
        {
            foreach(Control ctrl in this.gbXF.Controls)
            {
                if(ctrl is UCTimeInterval)
                {
                    UCTimeInterval uctime = ctrl as UCTimeInterval;
                    if (uctime != null)
                        this.listUCTI.Add(uctime);
                }
            }
        }

        private void GetDeviceType()
        {
            listDevicePlan = TbDeviceplan.FindAll("IsValid=1", "ID ASC", "", 0, 0);
            if(listDevicePlan != null && listDevicePlan.Count>0)
            {
                listDevicePlan = listDevicePlan.FindAll(p => p.DeviceName == "新风");
                if(listDevicePlan != null && listDevicePlan.Count>0)
                {
                    foreach (TbDeviceplan tdp in listDevicePlan)
                    {
                        Control[] ctrls= this.gbXF.Controls.Find("ucTimeInterval" + tdp.PlanNo, true);
                        if(ctrls!=null && ctrls.Length>0)
                        {
                            UCTimeInterval ucTime = ctrls[0] as UCTimeInterval;
                            if (ucTime != null)
                            {
                                ucTime.SetEnabled = tdp.IsValid;
                                ucTime.SetStartTime(tdp.StartTime);
                                ucTime.SetStopTime(tdp.StopTime);
                            }
                        }
                    }
                }
            }
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {            
            foreach(UCTimeInterval uctime in listUCTI)
            {
                string starttime = uctime.GetStartTime();
                string stoptime = uctime.GetStopTime();
                TbDeviceplan tbdp = this.listDevicePlan.Find(p => p.PlanNo == uctime.GroupIndex);
                tbdp.IsValid = uctime.SetEnabled;
                if(starttime!="" && stoptime!="")
                {
                    tbdp.StartTime = starttime;
                    tbdp.StopTime = stoptime;
                }
                if (tbdp == null)
                {//插入
                    tbdp.Insert();
                }
                else
                {//修改
                    tbdp.Update();
                }
            }
            MessageBox.Show("编辑时间段成功！");
        }

        private void frmDevicePlan_Load(object sender, EventArgs e)
        {
            GetTimeGroup();
            GetDeviceType();
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

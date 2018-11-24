using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WG3000_COMM.Core;

namespace ToolsManage.SystemManage
{
    public partial class frmDoorSearch : DevExpress.XtraEditors.XtraForm
    {
        public static bool blSelect = false;
        public static string strSn = "";
        public static string strIp = "";
        //public static string strPort = "";

        public frmDoorSearch()
        {
            InitializeComponent();
        }

        private void frmDoorSearch_Load(object sender, EventArgs e)
        {
            blSelect = false;
            this.sbtnAlter.Enabled = false;
            this.sbtnSearch.PerformClick();
        }

        private void sbtnSearch_Click(object sender, EventArgs e)
        {
            lblCount.Text = "0";
            this.dgvFoundControllers.Rows.Clear();
            this.sbtnAlter.Enabled = false;

            System.Collections.ArrayList arrControllers = new System.Collections.ArrayList();

            using (wgMjController control = new wgMjController())
            {
                control.SearchControlers(ref arrControllers);
            }
            if (arrControllers != null)
            {
                if (arrControllers.Count <= 0)
                {
                    MessageBox.Show("Not Found");
                    this.sbtnAlter.Enabled = true;
                    return;
                }
                this.dgvFoundControllers.Rows.Clear();
                wgMjControllerConfigure conf;
                for (int i = 0; i < arrControllers.Count; i++)
                {
                    conf = (wgMjControllerConfigure)arrControllers[i];
                    string[] subItems = new string[] {
                         (this.dgvFoundControllers.Rows.Count+1).ToString().PadLeft(4,'0'),  //
                        conf.controllerSN.ToString(),                   //SN
                        conf.ip.ToString(),         //IP
                        conf.mask.ToString(),       //"MASK",
                        conf.gateway.ToString(),    //"Gateway",
                        conf.port.ToString(),       //"PORT" 
                        conf.MACAddr,               //MAC
                        conf.pcIPAddr               //Note [pcIPAddr]
                    };
                    this.dgvFoundControllers.Rows.Add(subItems);
                }
            }
            this.sbtnSearch.Enabled = true;
            if (this.dgvFoundControllers.Rows.Count > 0)
            {
                this.sbtnAlter.Enabled = true;
            }
            lblCount.Text = this.dgvFoundControllers.Rows.Count.ToString(); 
        }

        private void sbtnAlter_Click(object sender, EventArgs e)
        {
            if (this.dgvFoundControllers.SelectedRows.Count <= 0)
            {
                return;
            }
            using (frmDoorConfig frm = new frmDoorConfig())
            {
                DataGridViewRow dgvdr = this.dgvFoundControllers.SelectedRows[0];

                frm.strSN = dgvdr.Cells["f_ControllerSN"].Value.ToString();
                frm.strMac = dgvdr.Cells["f_MACAddr"].Value.ToString();
                frm.strIP = dgvdr.Cells["f_IP"].Value.ToString();
                frm.strMask = dgvdr.Cells["f_Mask"].Value.ToString();
                frm.strGateway = dgvdr.Cells["f_Gateway"].Value.ToString();
                frm.strTCPPort = dgvdr.Cells["f_PORT"].Value.ToString();
                string pcIPAddr = "";
                if (dgvdr.Cells["f_PCIPAddr"].Value != null)
                {
                    pcIPAddr = dgvdr.Cells["f_PCIPAddr"].Value.ToString();
                }

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    string strSN = frm.strSN;
                    string strMac = frm.strMac;
                    string strIP = frm.strIP;
                    string strMask = frm.strMask;
                    string strGateway = frm.strGateway;
                    string strTCPPort = frm.strTCPPort;
                    string strOperate = frm.Text;
                    this.Refresh();

                    Cursor.Current = Cursors.WaitCursor;
                    using (wgMjController control = new wgMjController())
                    {
                        control.NetIPConfigure(strSN, strMac, strIP, strMask, strGateway, strTCPPort, pcIPAddr);
                    }
                }
            }
        }

        private void dgvFoundControllers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.sbtnAlter.Enabled)
            {
                this.sbtnAlter.PerformClick();
            }
        }

        private void sbtnSelect_Click(object sender, EventArgs e)
        {
            if (this.dgvFoundControllers.SelectedRows.Count <= 0)
            {
                return;
            }
            blSelect = true;
            DataGridViewRow dgvdr = this.dgvFoundControllers.SelectedRows[0];
            strSn = dgvdr.Cells["f_ControllerSN"].Value.ToString();
            strIp = dgvdr.Cells["f_IP"].Value.ToString();
            //strPort = dgvdr.Cells["f_PORT"].Value.ToString();
            this.Close();
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
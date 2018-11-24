using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToolsManage.UCControlNew
{
    public partial class UCTimeInterval : UserControl
    {
        public UCTimeInterval()
        {
            InitializeComponent();
        }

        public int GroupIndex
        {
            get; set;
        }

        public string GroupName
        {
            get { return this.chbGroup.Text; }
            set { this.chbGroup.Text = value; }
        }

        public bool SetEnabled
        {
            get { return this.pnlTime.Enabled; }
            set
            {
                this.chbGroup.Checked = value;
                this.pnlTime.Enabled = value;
            }
        }

        public void SetStartTime(string starttime)
        {
            if (!String.IsNullOrEmpty(starttime))
            {
                string[] time = starttime.Split(':');
                if (time != null && time.Length >= 2)
                {
                    nudStartHour.Value = Convert.ToInt16(time[0]);
                    nudStartMin.Value = Convert.ToInt16(time[1]);
                }
            }
        }

        public void SetStopTime(string stoptime)
        {
            if (!String.IsNullOrEmpty(stoptime))
            {
                string[] time = stoptime.Split(':');
                if (time != null && time.Length >= 2)
                {
                    nudStopHour.Value = Convert.ToInt16(time[0]);
                    nudStopMin.Value = Convert.ToInt16(time[1]);
                }
            }
        }

        public string GetStartTime()
        {
            string timestart = "";
            if(nudStartHour.Value>=nudStopHour.Value)
            {
                MessageBox.Show("启动时间不能大于停止时间！");
                return "";
            }
            if (chbGroup.Checked)
            {
                timestart = nudStartHour.Value.ToString() + ":";
                timestart += nudStartMin.Value.ToString();
            }
            return timestart;
        }

        public string GetStopTime()
        {
            string timestop = "";
            if (chbGroup.Checked)
            {
                timestop = nudStopHour.Value.ToString() + ":";
                timestop += nudStopMin.Value.ToString();
            }
            return timestop;
        }

        private void chbGroup_CheckedChanged(object sender, EventArgs e)
        {
            pnlTime.Enabled = chbGroup.Checked;
        }
    }
}

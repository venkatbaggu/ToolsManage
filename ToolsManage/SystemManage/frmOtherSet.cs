using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ToolsManage.BaseClass.OtherClass;
using ToolsManage.BaseClass;

namespace ToolsManage.SystemManage
{
    public partial class frmOtherSet : DevExpress.XtraEditors.XtraForm
    {
        AppConfig config = new AppConfig();

        public frmOtherSet()
        {
            InitializeComponent();
        }

        private void frmOtherSet_Load(object sender, EventArgs e)
        {
         
            tbGruopName.Text = config.AppConfigGet("VoiceGroupName");
            tbVolume.Text = config.AppConfigGet("Volume");
            tbVideoIp.Text = config.AppConfigGet("VideoIP");
            string strXml = config.AppConfigGet("MarkUsing");
            if (strXml == DeviceUsing.启用.ToString())
                cboxMark.Checked = true; 
            else
                cboxMark.Checked = false ;
               
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbGruopName.Text))
            {
                MessageUtil.ShowTips("请设置班组名称");
                return;
            }
            if (string.IsNullOrEmpty(tbVolume.Text))  
            {
                MessageUtil.ShowTips("请设置 语音音量");
                return;
            }
            if (string.IsNullOrEmpty(tbVideoIp.Text)) 
            {
                MessageUtil.ShowTips("请设置 硬盘录像机IP");
                return;
            }
      
            config.AppConfigSet("VoiceGroupName", tbGruopName.Text);
            config.AppConfigSet("Volume", tbVolume.Text);
            config.AppConfigSet("VideoIP", tbVideoIp.Text);
            if (cboxMark.Checked)
                config.AppConfigSet("MarkUsing", DeviceUsing.启用.ToString());
            else
                config.AppConfigSet("MarkUsing", DeviceUsing.未启用 .ToString());

            MessageUtil.ShowTips("设置成功");
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
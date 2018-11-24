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

namespace ToolsManage.SystemManage
{
    public partial class frmDataClear : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        public frmDataClear()
        {
            InitializeComponent();
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            if (MessageUtil.ShowYesNoAndTips("所选信息表清除不可恢复，确定清除？") == DialogResult.Yes)
            {
                if (cboxAlarm.Checked)
                    datalogic.SqlComNonQuery("delete from tb_AlarmEvent");
                if (cboxBorrow.Checked)
                    datalogic.SqlComNonQuery("delete from tb_RecordBorrow");
                if (cboxBoxOpen.Checked)
                    datalogic.SqlComNonQuery("delete from tb_RecordBoxDoor");
                if (cboxBoxPower.Checked)
                    datalogic.SqlComNonQuery("delete from tb_RecordBoxPower");
                if (cboxDoolPower.Checked)
                    datalogic.SqlComNonQuery("delete from tb_RecordPower");
                if (cboxDoorInOut.Checked)
                    datalogic.SqlComNonQuery("delete from tb_RecordDoorInOut");
                if (cboxEnvirData.Checked)
                    datalogic.SqlComNonQuery("delete from tb_EnviriData");
                if (cboxEnvirEvent.Checked)
                    datalogic.SqlComNonQuery("delete from tb_EnviriEvent");
                if (cboxInRoom.Checked) 
                    datalogic.SqlComNonQuery("delete from tb_RecordInRoom");
                if (cboxScrap.Checked)
                    datalogic.SqlComNonQuery("delete from tb_RecordScrap");
                if (cboxTest.Checked) 
                    datalogic.SqlComNonQuery("delete from tb_RecordTest");
         
                MessageUtil.ShowTips("数据清理成功!");
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxAll.Checked)
            {
                cboxAlarm.Checked = true;
                cboxBorrow.Checked = true;
                cboxBoxOpen.Checked = true;
                cboxBoxPower.Checked = true;
                cboxDoolPower.Checked = true;
                cboxDoorInOut.Checked = true;
                cboxEnvirData.Checked = true;
                cboxEnvirEvent.Checked = true;
                cboxInRoom.Checked = true;
                cboxScrap.Checked = true;
                cboxTest.Checked = true;
            }
            else
            {
                cboxAlarm.Checked = false ;
                cboxBorrow.Checked = false;
                cboxBoxOpen.Checked = false;
                cboxBoxPower.Checked = false;
                cboxDoolPower.Checked = false;
                cboxDoorInOut.Checked = false;
                cboxEnvirData.Checked = false;
                cboxEnvirEvent.Checked = false;
                cboxInRoom.Checked = false;
                cboxScrap.Checked = false;
                cboxTest.Checked = false;
            }
        }
    }
}
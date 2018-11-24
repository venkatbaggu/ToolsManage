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

namespace ToolsManage.EnvirManage
{
    public partial class frmEnvirSet : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        private bool blHas = false;
        private string strId = "";

        public frmEnvirSet()
        {
            InitializeComponent();
        }

        private void frmEnvirSet_Load(object sender, EventArgs e)
        {
            string strSql = "select ID,HotRun,HotStop,HumiRun,HumiStop,HandKeepTime,AirCoolRun,AirCoolStop,"+
                "AirCoolTempSet,AirHotRun,AirHotStop,AirHotTempSet from tb_SysDevice ";
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                blHas = true;
                strId = dt.Rows[0]["ID"].ToString();
                //tbHotRun.Text = dt.Rows[0]["HotRun"].ToString();
                //tbHotStop.Text = dt.Rows[0]["HotStop"].ToString();
                tbHumiRun.Text = dt.Rows[0]["HumiRun"].ToString();
                tbHumiStop.Text = dt.Rows[0]["HumiStop"].ToString();
                tbDisAuto.Text = dt.Rows[0]["HandKeepTime"].ToString();
                tbCoolRun.Text = dt.Rows[0]["AirCoolRun"].ToString();
                tbCoolStop.Text = dt.Rows[0]["AirCoolStop"].ToString();
                tbCoolTempSet.Text = dt.Rows[0]["AirCoolTempSet"].ToString();
                tbAirHotRun.Text = dt.Rows[0]["AirHotRun"].ToString();
                tbAirHotStop.Text = dt.Rows[0]["AirHotStop"].ToString(); 
                tbAirHotTempSet.Text = dt.Rows[0]["AirHotTempSet"].ToString(); 
            }
            else
                blHas = false;
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            #region  输入 判断

            //string strHotRun = tbAirHotRun.Text.Trim();
            //string strHotStop = tbHotStop.Text.Trim();
            string strHumiRun = tbHumiRun.Text.Trim();
            string strHumiStop = tbHumiStop.Text.Trim();
            string strHandTime = tbDisAuto.Text.Trim();

            string strAirCoolRun = tbCoolRun.Text.Trim();
            string strAirCoolStop = tbCoolStop.Text.Trim();
            string strAirCoolTempSet = tbCoolTempSet.Text.Trim();
            string strAirHotRun = tbAirHotRun.Text.Trim();
            string strAirHotStop = tbAirHotStop.Text.Trim(); 
            string strAirHotTempSet = tbAirHotTempSet.Text.Trim();

            if (string.IsNullOrEmpty(strAirHotTempSet))
            {
                MessageUtil.ShowTips("空调制热温度设置值不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strAirHotStop))
            {
                MessageUtil.ShowTips("空调制热停止值不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strAirHotRun))
            {
                MessageUtil.ShowTips("空调制热启动值不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strAirCoolTempSet))
            {
                MessageUtil.ShowTips("空调制冷温度设置值不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strAirCoolStop))
            {
                MessageUtil.ShowTips("空调制冷停止值不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strAirCoolRun))
            {
                MessageUtil.ShowTips("空调制冷启动值不可为空");
                return;
            }

            //if (string.IsNullOrEmpty(strHotRun))
            //{
            //    MessageUtil.ShowTips("加热启动阀值不可为空！");
            //    return;
            //}
            //if (string.IsNullOrEmpty(strHotStop ))
            //{
            //    MessageUtil.ShowTips("加热停止阀值不可为空！");
            //    return;
            //}
            if (string.IsNullOrEmpty(strHumiRun ))
            {
                MessageUtil.ShowTips("除湿启动阀值不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strHumiStop ))
            {
                MessageUtil.ShowTips("除湿停止阀值不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strHandTime))
            {
                MessageUtil.ShowTips("非自动控制保持时间不可为空！");
                return;
            }
            #endregion

            if (blHas)
            {// HotRun,HotStop,HumiRun,HumiStop HandKeepTime,AirCoolRun,AirCoolStop,AirCoolTempSet,AirHotRun,AirHotStop,AirHotTempSet    
                string strSql = "update tb_SysDevice set HotRun='',HotStop='',HumiRun='" + strHumiRun  + "'," +
                     "HumiStop='" + strHumiStop + "',HandKeepTime='" + strHandTime + "' ,AirCoolRun='" + strAirCoolRun + "'" +
                     ",AirCoolStop='" + strAirCoolStop + "',AirCoolTempSet='" + strAirCoolTempSet + "',AirHotRun='" + strAirHotRun + "'" +
                     ",AirHotStop='" + strAirHotStop + "',AirHotTempSet='" + strAirHotTempSet + "' where ID='" + strId + "' ";
                datalogic.SqlComNonQuery(strSql);
            }
            else
            {     
                string strSql = "insert into tb_SysDevice (HotRun,HotStop,HumiRun,HumiStop,HandKeepTime,AirCoolRun,AirCoolStop,AirCoolTempSet,"+
                    "AirHotRun,AirHotStop,AirHotTempSet)" +
                       "values ('','','" + strHumiRun + "','" + strHumiStop + "','" + strHandTime + "'"+
                       ",'" + strAirCoolRun + "','" + strAirCoolStop + "','" + strAirCoolTempSet + "','" + strAirHotRun + "',"+
                       "'" + strAirHotStop + "','" + strAirHotTempSet + "')";
                datalogic.SqlComNonQuery(strSql);
            }
            MessageUtil.ShowTips("设置成功");
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
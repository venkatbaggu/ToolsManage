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
    public partial class frmSystemSet : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        private string strId = "";

        public frmSystemSet()
        {
            InitializeComponent();
        }

        private void frmSystemSet_Load(object sender, EventArgs e)
        {
            string strSql = "select ID,OverDayBorr,BorrRetSpan from tb_SysDevice ";
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                strId = dt.Rows[0]["ID"].ToString();
                tbOverBorr.Text = dt.Rows[0]["OverDayBorr"].ToString();
                tbBorrRetSpan.Text = dt.Rows[0]["BorrRetSpan"].ToString();




            }
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            #region  输入判断

            string strOverBorr = tbOverBorr.Text.Trim();
            string strBorrRetSpan = tbBorrRetSpan.Text.Trim();
            if (string.IsNullOrEmpty(strOverBorr))
            {
                MessageUtil.ShowTips("正常外借天数不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strBorrRetSpan))
            {
                MessageUtil.ShowTips("借还时间间隔不可为空");
                return;
            }

            #endregion

            string strSql = "update tb_SysDevice set OverDayBorr='" + strOverBorr + "',BorrRetSpan='" + strBorrRetSpan + "' where ID='" + strId + "' ";
            datalogic.SqlComNonQuery(strSql);
            MessageUtil.ShowTips("设置成功");
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
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
    public partial class frmAlterPsd : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        public frmAlterPsd()
        {
            InitializeComponent();
        }

        private void frmAlterPsd_Load(object sender, EventArgs e)
        {
            tbUser.Text = frmMain.strUserName;
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            #region  输入 判断

            string strPsd = tbPsd.Text.Trim();
            string strPsdNew = tbPsdNew.Text.Trim();
            if (string.IsNullOrEmpty(strPsd))
            {
                MessageUtil.ShowTips("原用户名密码不可为空");
                return;
            }
            if (string.IsNullOrEmpty(strPsdNew))
            {
                MessageUtil.ShowTips("新用户名密码不可为空");
                return;
            }
            #endregion

            string strName = tbUser.Text;
            if (string.IsNullOrEmpty(strName))
                return;
            // ID,UsersName,UsersPassWord,UsersPower,UsersRemark
            string strSql = "select UsersPower from tb_SystemUser where UsersName='" + strName + "' and UsersPassWord='" + strPsd + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                strSql = "update tb_SystemUser set UsersPassWord='" + strPsdNew + "' where ID='" + frmMain .strUserID + "' ";
                datalogic.SqlComNonQuery(strSql);
                MessageUtil.ShowTips("密码修改成功");
            }
            else
            {
                MessageUtil.ShowTips("原用户名密码错误");
            }
        }

        private void sbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
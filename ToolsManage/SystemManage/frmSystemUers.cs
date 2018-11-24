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
    public partial class frmSystemUers : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        public frmSystemUers()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
        }

        private void frmSystemUers_Load(object sender, EventArgs e)
        {
            TableShowInit();

            string strSql = "select ID,UsersName,UsersPassWord,UsersPower,UsersRemark from tb_SystemUser ";
            gridControl1.DataSource = datalogic.GetDataTable(strSql);
        }

        private void TableShowInit()//tb_SystemUser ID,UsersName,UsersPassWord,UsersPower,UsersRemark
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "UsersName";
            Col1.Caption = "用户名";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "UsersPassWord";
            Col1.Caption = "密码";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "UsersPower";
            Col1.Caption = "用户权限";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "UsersRemark";
            Col1.Caption = "备注";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

        }

        private void sbtnNew_Click(object sender, EventArgs e)
        {
            string strSql = "select ID,UsersName,UsersPassWord,UsersPower,UsersRemark from tb_SystemUser ";
            gridControl1.DataSource = datalogic.GetDataTable(strSql);
        }

        private void sbtnAddUser_Click(object sender, EventArgs e)
        {
            frmUsersEdit frm = new frmUsersEdit();
            frm.Tag = TagType.添加.ToString();
            frm.ShowDialog(this );
            frm.Dispose();
            string strSql = "select ID,UsersName,UsersPassWord,UsersPower,UsersRemark from tb_SystemUser ";
            gridControl1.DataSource = datalogic.GetDataTable(strSql);
        }

        private void sbtnAlter_Click(object sender, EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                frmUsersEdit frm = new frmUsersEdit();
                frm.Tag = TagType.修改.ToString();
                frm.strID = gridView1.GetRowCellDisplayText(rows[0], "ID").ToString();
                frm.strName = gridView1.GetRowCellDisplayText(rows[0], "UsersName").ToString();
                frm.strPsd = gridView1.GetRowCellDisplayText(rows[0], "UsersPassWord").ToString();
                frm.strPower = gridView1.GetRowCellDisplayText(rows[0], "UsersPower").ToString();
                frm.strRemark = gridView1.GetRowCellDisplayText(rows[0], "UsersRemark").ToString();
                frm.ShowDialog(this);
                frm.Dispose();
                string strSql = "select ID,UsersName,UsersPassWord,UsersPower,UsersRemark from tb_SystemUser ";
                gridControl1.DataSource = datalogic.GetDataTable(strSql);
            }
            else
            {
                MessageUtil.ShowTips("无选中的用户");
            }
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                if (MessageUtil.ShowYesNoAndTips("确定删除表中所选用户？") == DialogResult.Yes)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        string strID = gridView1.GetRowCellDisplayText(rows[i], "ID").ToString();
                        string strsql = "delete from tb_SystemUser where ID='" + strID + "'";
                        datalogic.SqlComNonQuery(strsql);
                    }
                    string strSql = "select ID,UsersName,UsersPassWord,UsersPower,UsersRemark from tb_SystemUser ";
                    gridControl1.DataSource = datalogic.GetDataTable(strSql);
                }
            }
            else
            {
                MessageUtil.ShowTips("无选中的用户");
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
        }











    }
}
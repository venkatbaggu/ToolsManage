using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Diagnostics;

using ToolsManage.BaseClass;

namespace ToolsManage.DoorManage
{
    public partial class frmRecordPower : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        DataTable dtRecord = new DataTable();

        /// <summary>
        /// 分页显示的表
        /// </summary>
        DataTable dtPage = new DataTable();
        private int iAllCount = 0;//所有行
        private int iPageSizes = 45;//一页显示的行数
        private int iShowStart = 0;// 显示拷贝 起始 条数
        private int iShowEnd = 0;// 显示拷贝 结束 条数

        public frmRecordPower()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
        }

        private void frmRecordPower_Load(object sender, EventArgs e)
        {
            LoadGroup();
            SetQueryTime();
            TableShowInit();
            ConditionQuery();
        }




        #region  子程序

        private void SetQueryTime()
        {
            DateTime time = DateTime.Now.AddMonths(-1);
            dtpStart.Value = new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
            dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }

        private void TableShowInit()//tb_RecordPower ID, PowerType,GroupName,UserName,OperateTime,People
        {
            gridView1.Columns.Clear();
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "PowerType";
            Col1.Caption = "授权类型";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "GroupName";
            Col1.Caption = "班组/部门";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "UserName";
            Col1.Caption = "姓名";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "OperateTime";
            Col1.Caption = "授权时间";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "People";
            Col1.Caption = "授权人";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

        }

        private void ConditionQuery()
        {
            try
            {
                PagetableInit();
                TableShowInit();

                #region 查询条件
                string strCondition = "";
                if (cbbType.Text != "全部")
                {
                    if (cbbType.Text == "添加权限")
                    {
                        strCondition = "PowerType='添加权限' ";
                    }
                    else if (cbbType.Text == "删除权限")
                    {
                        strCondition = "PowerType='删除权限' ";
                    }
                }
                if (cbbGroup.Text != "全部")
                {
                    if (!string.IsNullOrEmpty(strCondition))
                    {
                        strCondition += " and ";
                    }
                    if (cbbPeople.Text == "全部")
                    {
                        string str = "GroupName='" + cbbGroup.Text + "' ";
                        strCondition += str;
                    }
                    else
                    {
                        string str = "UserName='" + cbbPeople.Text + "' ";
                        strCondition += str;
                    }
                }
                else if (cbbGroup.Text == "全部")
                {
                    if (cbbPeople.Text != "全部")
                    {
                        if (!string.IsNullOrEmpty(strCondition))
                        {
                            strCondition += " and ";
                        }
                        string str = "GroupName='" + cbbGroup.Text + "' ";
                        strCondition += str;
                    }
                }

                #endregion

                string strStart = dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string strEnd = dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //tb_RecordPower  PowerType,GroupName,UserName,OperateTime,People
                string strSql = "select top 1000 ID,PowerType,GroupName,UserName,OperateTime,People from tb_RecordPower " +
                "where OperateTime BETWEEN '" + strStart + "' and '" + strEnd + "' ";// 
                if (!string.IsNullOrEmpty(strCondition))
                {
                    strSql += " and ";
                    strSql += strCondition;
                }
                strSql += " Order By OperateTime Desc";
                dtRecord = datalogic.GetDataTable(strSql);

                #region  计算分页

                dtPage.Rows.Clear();
                sbtnPageDown.Enabled = false;
                sbtnPageUp.Enabled = false;
                if (dtRecord.Rows.Count > 0)
                {
                    iAllCount = dtRecord.Rows.Count;
                    if (iAllCount > iPageSizes)
                    {
                        iShowStart = 0;
                        iShowEnd = iPageSizes;
                        if (iAllCount > iShowEnd)
                        {
                            sbtnPageDown.Enabled = true;
                        }
                        for (int i = iShowStart; i < iShowEnd; i++)
                        {
                            DataRow dr = dtPage.NewRow();// ID, ,,,,
                            dr[0] = dtRecord.Rows[i]["ID"].ToString();
                            dr[1] = dtRecord.Rows[i]["PowerType"].ToString();
                            dr[2] = dtRecord.Rows[i]["GroupName"].ToString();
                            dr[3] = dtRecord.Rows[i]["UserName"].ToString();
                            dr[4] = dtRecord.Rows[i]["OperateTime"].ToString();
                            dr[5] = dtRecord.Rows[i]["People"].ToString();
                            dtPage.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < iAllCount; i++)
                        {
                            DataRow dr = dtPage.NewRow();// ID  OpenType GroupName UserName OpenTime CloseTime DurationTime
                            dr[0] = dtRecord.Rows[i]["ID"].ToString();
                            dr[1] = dtRecord.Rows[i]["PowerType"].ToString();
                            dr[2] = dtRecord.Rows[i]["GroupName"].ToString();
                            dr[3] = dtRecord.Rows[i]["UserName"].ToString();
                            dr[4] = dtRecord.Rows[i]["OperateTime"].ToString();
                            dr[5] = dtRecord.Rows[i]["People"].ToString();
                            dtPage.Rows.Add(dr);
                        }
                    }
                    gridControl1.DataSource = dtPage;
                }

                #endregion
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        private void PagetableInit()//  ID, PowerType,GroupName,UserName,OperateTime,People
        {
            dtPage.Columns.Clear();

            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PowerType";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "GroupName";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserName";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "OperateTime";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "People";
            dtPage.Columns.Add(column);
        }


        private void LoadGroup()
        {
            string strSql = "select GroupName from tb_DoorUser where IsGroup='" + GroupPeoType.班组.ToString() + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                cbbGroup.Items.Add(dr["GroupName"].ToString());
            }
            cbbGroup.Items.Add("全部");
            cbbGroup.Text = "全部";
        }


        #endregion





        private void cbbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbPeople.Items.Clear();
            if (cbbGroup.Text == "全部")
            {
                string strSql = "select UserName from tb_DoorUser where IsGroup='" + GroupPeoType.人员.ToString() + "'";
                DataTable dt = datalogic.GetDataTable(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    cbbPeople.Items.Add(dr["UserName"].ToString());
                }
            }
            else
            {
                string strSql = "select UserName from tb_DoorUser where GroupName='" + cbbGroup.Text.Trim() + "' and IsGroup='" + GroupPeoType.人员.ToString() + "' ";
                DataTable dt = datalogic.GetDataTable(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    cbbPeople.Items.Add(dr["UserName"].ToString());
                }
            }
            cbbPeople.Items.Add("全部");
            cbbPeople.Text = "全部";
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

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            sbtnQuery.Enabled = false;
            ConditionQuery();
            sbtnQuery.Enabled = true;
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            bool blJudge = frmMain.PowerJudge(UserPower.系统用户);
            if (!blJudge)
                return;

            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                if (MessageUtil.ShowYesNoAndTips("确定删除选中记录？") == DialogResult.Yes)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        string strID = gridView1.GetRowCellDisplayText(rows[i], "ID").ToString();
                        string strsql = "delete from tb_RecordPower where ID='" + strID + "'";
                        datalogic.SqlComNonQuery(strsql);
                    }
                    ConditionQuery();
                }
            }
            else
            {
                MessageUtil.ShowTips("请选择需删除的记录");
            }
        }

        private void sbtnOut_Click(object sender, EventArgs e)
        {
            try
            {
                string savePath = FileDialogHelper.SaveExcel();
                if (!string.IsNullOrEmpty(savePath))
                {
                    string outError = "";
                    AsposeExcelTools.DataTableToExcel(dtPage, savePath, out outError);

                    if (!string.IsNullOrEmpty(outError))
                    {
                        MessageBox.Show(outError);
                    }
                    else
                    {
                        Process.Start(savePath);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageUtil.ShowTips(ee.ToString());
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtnPageUp_Click(object sender, EventArgs e)
        {
            try
            {
                sbtnPageDown.Enabled = false;
                sbtnPageUp.Enabled = false;
                if (iShowEnd >= iPageSizes)
                {
                    int iTemp = iShowEnd % iPageSizes;
                    if (iTemp == 0)
                    {
                        iShowEnd -= iPageSizes;
                    }
                    else
                    {
                        iShowEnd -= iTemp;
                    }
                }
                if (iShowStart >= iPageSizes)
                {
                    iShowStart -= iPageSizes;
                }
                if (iShowEnd > iAllCount)
                {
                    iShowEnd = iAllCount;
                }

                dtPage.Rows.Clear();
                for (int i = iShowStart; i < iShowEnd; i++)
                {
                    DataRow dr = dtPage.NewRow();//tb_RecordEvent ID,Area,Type,Addr,EventContent,Time
                    dr[0] = dtRecord.Rows[i]["ID"].ToString();
                    dr[1] = dtRecord.Rows[i]["PowerType"].ToString();
                    dr[2] = dtRecord.Rows[i]["GroupName"].ToString();
                    dr[3] = dtRecord.Rows[i]["UserName"].ToString();
                    dr[4] = dtRecord.Rows[i]["OperateTime"].ToString();
                    dr[5] = dtRecord.Rows[i]["People"].ToString();
                    dtPage.Rows.Add(dr);
                }
                gridControl1.DataSource = dtPage;
                if (iShowStart >= iPageSizes)
                {
                    sbtnPageUp.Enabled = true;
                }
                if (iAllCount > iShowEnd)
                {
                    sbtnPageDown.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        private void sbtnPageDown_Click(object sender, EventArgs e)
        {
            try
            {
                sbtnPageDown.Enabled = false;
                sbtnPageUp.Enabled = false;
                iShowEnd += iPageSizes;
                iShowStart += iPageSizes;
                if (iShowEnd > iAllCount)
                {
                    iShowEnd = iAllCount;
                }

                dtPage.Rows.Clear();
                for (int i = iShowStart; i < iShowEnd; i++)
                {
                    DataRow dr = dtPage.NewRow();//tb_RecordEvent ID,Area,Type,Addr,EventContent,Time
                    dr[0] = dtRecord.Rows[i]["ID"].ToString();
                    dr[1] = dtRecord.Rows[i]["PowerType"].ToString();
                    dr[2] = dtRecord.Rows[i]["GroupName"].ToString();
                    dr[3] = dtRecord.Rows[i]["UserName"].ToString();
                    dr[4] = dtRecord.Rows[i]["OperateTime"].ToString();
                    dr[5] = dtRecord.Rows[i]["People"].ToString();
                    dtPage.Rows.Add(dr);
                }
                gridControl1.DataSource = dtPage;
                if (iShowEnd > iPageSizes)
                {
                    sbtnPageUp.Enabled = true;
                }
                if (iAllCount > iShowEnd)
                {
                    sbtnPageDown.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }







    }
}
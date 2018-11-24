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

namespace ToolsManage.ToolsManage
{
    public partial class frmRecordScrap : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        DataTable dtRecord = new DataTable();

        DataTable dtPage = new DataTable();
        private int iAllCount = 0;//所有行
        private int iPageSizes = 45;//一页显示的行数
        private int iShowStart = 0;// 显示拷贝 起始 条数
        private int iShowEnd = 0;// 显示拷贝 结束 条数

        public frmRecordScrap()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
        }

        private void frmRecordScrap_Load(object sender, EventArgs e)
        {
            SetQueryTime();
            TableShowInit();
            LoadToolType();
            LoadUser();
            ConditionQuery();
        }

        #region  子程序

        private void LoadToolType()
        {
            string strSql = "select ToolType from tb_TypeAndName where tvParent='0'";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                cbbToolType.Items.Add(dr["ToolType"].ToString());
            }
            cbbToolType.Items.Add("全部");
            cbbToolType.Text = "全部";
        }

        private void LoadUser()   //tb_SystemUser ID,UsersName,UsersPassWord,UsersPower,UsersRemark
        {
            cbbPeopleIn.Items.Clear();
            cbbPeopleIn.Items.Add("全部");
            string strSql = "select UsersName from tb_SystemUser";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                string str = datarow["UsersName"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    cbbPeopleIn.Items.Add(str);
                }
            }
            if (cbbPeopleIn.Items.Count > 0)
                cbbPeopleIn.SelectedIndex = 0;
        }

        private void SetQueryTime()
        {
            DateTime time = DateTime.Now.AddMonths(-1);
            dtpStart.Value = new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
            dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }

        private void TableShowInit()// ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,ScrapInfo,ScrapPeople,ScrapTime
        {
            gridView1.Columns.Clear();
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolID";
            Col1.Caption = "工具ID";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolType";
            Col1.Caption = "工具种类";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolName";
            Col1.Caption = "工具名称";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "RFIDCoding";
            Col1.Caption = "RFID编码";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "StoragePlace";
            Col1.Caption = "存放位置";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ScrapInfo";
            Col1.Caption = "报废原因";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ScrapPeople";
            Col1.Caption = "报废人";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ScrapTime";
            Col1.Caption = "报废时间";
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
                if (cbbToolType.Text != "全部")
                {
                    strCondition = "ToolType='" + cbbToolType.Text + "' ";
                }
                if (cbbToolName.Text != "全部")
                {
                    if (!string.IsNullOrEmpty(strCondition))
                    {
                        strCondition += " and ";
                    }
                    string str = "ToolName='" + cbbToolName.Text + "' ";
                    strCondition += str;
                }
                if (cbbPeopleIn.Text != "全部")
                {
                    if (!string.IsNullOrEmpty(strCondition))
                    {
                        strCondition += " and ";
                    }
                    string str = "ScrapPeople='" + cbbPeopleIn.Text + "' ";
                    strCondition += str;
                }
                #endregion

                string strStart = dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string strEnd = dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //tb_RecordScrap ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,ScrapInfo,ScrapPeople,ScrapTime
                string strSql = "select top 1000 ID,ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,ScrapInfo,ScrapPeople,ScrapTime from tb_RecordScrap " +
                "where ScrapTime BETWEEN '" + strStart + "' and '" + strEnd + "' ";// 
                if (!string.IsNullOrEmpty(strCondition))
                {
                    strSql += " and ";
                    strSql += strCondition;
                }
                strSql += " Order By ScrapTime Desc";
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
                            DataRow dr = dtPage.NewRow();//  // ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,ScrapInfo,ScrapPeople,ScrapTime   
                            dr[0] = dtRecord.Rows[i]["ID"].ToString();
                            dr[1] = dtRecord.Rows[i]["ToolType"].ToString();
                            dr[2] = dtRecord.Rows[i]["ToolName"].ToString();
                            dr[3] = dtRecord.Rows[i]["ToolID"].ToString();
                            dr[4] = dtRecord.Rows[i]["RFIDCoding"].ToString();
                            dr[5] = dtRecord.Rows[i]["StoragePlace"].ToString();
                            dr[6] = dtRecord.Rows[i]["ScrapInfo"].ToString();
                            dr[7] = dtRecord.Rows[i]["ScrapPeople"].ToString();
                            dr[8] = dtRecord.Rows[i]["ScrapTime"].ToString();
                            dtPage.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < iAllCount; i++)
                        {
                            DataRow dr = dtPage.NewRow();// ID,ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,InPeople,InTime    
                            dr[0] = dtRecord.Rows[i]["ID"].ToString();
                            dr[1] = dtRecord.Rows[i]["ToolType"].ToString();
                            dr[2] = dtRecord.Rows[i]["ToolName"].ToString();
                            dr[3] = dtRecord.Rows[i]["ToolID"].ToString();
                            dr[4] = dtRecord.Rows[i]["RFIDCoding"].ToString();
                            dr[5] = dtRecord.Rows[i]["StoragePlace"].ToString();
                            dr[6] = dtRecord.Rows[i]["ScrapInfo"].ToString();
                            dr[7] = dtRecord.Rows[i]["ScrapPeople"].ToString();
                            dr[8] = dtRecord.Rows[i]["ScrapTime"].ToString();
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

        private void PagetableInit()
        {
            dtPage.Columns.Clear();

            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolID";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolType";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "RFIDCoding";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "StoragePlace";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ScrapInfo";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ScrapPeople";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ScrapTime";
            dtPage.Columns.Add(column);
        }


        #endregion


        private void cbbToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbToolName.Items.Clear();
            if (cbbToolType.Text == "全部")
            {
                string strSql = "select ToolName from tb_TypeAndName ";
                DataTable dt = datalogic.GetDataTable(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    string str = dr["ToolName"].ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        cbbToolName.Items.Add(dr["ToolName"].ToString());
                    }
                }
            }
            else
            {
                string strSql = "select tvChildId from tb_TypeAndName where ToolType='" + cbbToolType.Text.Trim() + "' ";
                object ob = datalogic.SqlComScalar(strSql);
                if (ob != null)
                {
                    strSql = "select ToolName from tb_TypeAndName where tvParent='" + ob.ToString() + "'";
                    DataTable dt = datalogic.GetDataTable(strSql);
                    foreach (DataRow dr in dt.Rows)
                    {
                        cbbToolName.Items.Add(dr["ToolName"].ToString());
                    }
                }
            }
            cbbToolName.Items.Add("全部");
            cbbToolName.Text = "全部";
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
                        string strsql = "delete from tb_RecordScrap where ID='" + strID + "'";
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
                    dr[1] = dtRecord.Rows[i]["ToolType"].ToString();
                    dr[2] = dtRecord.Rows[i]["ToolName"].ToString();
                    dr[3] = dtRecord.Rows[i]["ToolID"].ToString();
                    dr[4] = dtRecord.Rows[i]["RFIDCoding"].ToString();
                    dr[5] = dtRecord.Rows[i]["StoragePlace"].ToString();
                    dr[6] = dtRecord.Rows[i]["ScrapInfo"].ToString();
                    dr[7] = dtRecord.Rows[i]["ScrapPeople"].ToString();
                    dr[8] = dtRecord.Rows[i]["ScrapTime"].ToString();
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
                    dr[1] = dtRecord.Rows[i]["ToolType"].ToString();
                    dr[2] = dtRecord.Rows[i]["ToolName"].ToString();
                    dr[3] = dtRecord.Rows[i]["ToolID"].ToString();
                    dr[4] = dtRecord.Rows[i]["RFIDCoding"].ToString();
                    dr[5] = dtRecord.Rows[i]["StoragePlace"].ToString();
                    dr[6] = dtRecord.Rows[i]["ScrapInfo"].ToString();
                    dr[7] = dtRecord.Rows[i]["ScrapPeople"].ToString();
                    dr[8] = dtRecord.Rows[i]["ScrapTime"].ToString();
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
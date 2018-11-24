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

using System.Diagnostics;

namespace ToolsManage.ToolsManage
{
    public partial class frmRecordBr : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        DataTable dtRecord = new DataTable();

        DataTable dtPage = new DataTable();
        private int iAllCount = 0;//所有行
        private int iPageSizes = 45;//一页显示的行数
        private int iShowStart = 0;// 显示拷贝 起始 条数
        private int iShowEnd = 0;// 显示拷贝 结束 条数

        public frmRecordBr()
        {
            InitializeComponent();
            this.gridView1.IndicatorWidth = 40;
        }

        private void frmRecordBr_Load(object sender, EventArgs e)
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

        /// <summary>
        /// 绑定 人员
        /// </summary>
        private void LoadUser()   //tb_DoorUser  tvParent tvChildId IsGroup  GroupName UserName IcNo CardNo
        {
            cbbPeopleBorrow.Items.Clear();
            cbbPeopleBorrow.Items.Add("全部");
            string strSql = "select UserName from tb_DoorUser where IsGroup='人员' ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                string str = datarow["UserName"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    cbbPeopleBorrow.Items.Add(str);
                }
            }
            if (cbbPeopleBorrow.Items.Count > 0)
                cbbPeopleBorrow.SelectedIndex = 0;
        }

        private void JudgeNoRetrun()
        {
            for (int i = 0; i < dtRecord.Rows.Count; i++)
            {
                string str = dtRecord.Rows[i]["ReturnTime"].ToString();
                if (string.IsNullOrEmpty(str))
                {
                    dtRecord.Rows[i]["IsReturn"] = "未归还";
                    string strTimeB = dtRecord.Rows[i]["BorrowTime"].ToString();

                    #region 计算 借用时长
                    string strBorrowDuration = "";
                    if (!string.IsNullOrEmpty(strTimeB))//入库后第一次读到
                    {
                        try
                        {
                            DateTime Lastdatetime = Convert.ToDateTime(strTimeB);
                            TimeSpan timeSpan = DateTime.Now - Lastdatetime;// TotalSeconds 
                            if (timeSpan.TotalDays > 1)
                            {
                                double d = timeSpan.TotalDays;
                                int iDuration = (int)d;
                                iDuration++;
                                strBorrowDuration = iDuration.ToString() + "天";
                            }
                            else if (timeSpan.TotalHours > 1)
                            {
                                double d = timeSpan.TotalHours;
                                int iDuration = (int)d;
                                iDuration++;
                                strBorrowDuration = iDuration.ToString() + "小时";
                            }
                            else if (timeSpan.TotalMinutes > 1)
                            {
                                double d = timeSpan.TotalMinutes;
                                int iDuration = (int)d;
                                iDuration++;
                                strBorrowDuration = iDuration.ToString() + "分";
                            }
                            else if (timeSpan.TotalSeconds > 1)
                            {
                                double d = timeSpan.TotalSeconds;
                                int iDuration = (int)d;
                                iDuration++;
                                strBorrowDuration = iDuration.ToString() + "秒";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageUtil.ShowTips(ex.Message);
                        }
                    }
                    #endregion

                    dtRecord.Rows[i]["BorrowDuration"] = strBorrowDuration;
                }
                else
                    dtRecord.Rows[i]["IsReturn"] = "已归还";
            }
        }

        private void PagetableInit()//  dtToolsBR ID ToolType ToolName ToolID PeopleBorrow  BorrowTime IsReturn  PeopleReturn ReturnTime BorrowDuration
        {
            dtPage.Columns.Clear();

            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ID";
            column.Caption = "ID";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolID";
            column.Caption = "工具ID";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolType";
            column.Caption = "工具种类";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            column.Caption = "工具名称";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PeopleBorrow";
            column.Caption = "借用人";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "BorrowTime";
            column.Caption = "借用时间";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IsReturn";
            column.Caption = "是否归还";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PeopleReturn";
            column.Caption = "归还人";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ReturnTime";
            column.Caption = "归还时间";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "BorrowDuration";
            column.Caption = "借用时长";
            dtPage.Columns.Add(column);
        }

        private void TableShowInit()
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
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ToolName";
            Col1.Caption = "工具名称";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "PeopleBorrow";
            Col1.Caption = "借用人";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "BorrowTime";
            Col1.Caption = "借用时间";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsReturn";
            Col1.Caption = "是否归还";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "PeopleReturn";
            Col1.Caption = "归还人";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// ReturnTime  IsReturn
            Col1.FieldName = "ReturnTime";
            Col1.Caption = "归还时间";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "BorrowDuration";
            Col1.Caption = "借用时长";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);
        }

        private void SetQueryTime()
        {
            DateTime time = DateTime.Now.AddMonths(-1);
            dtpStart.Value = new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
            dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
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
                if (cbbStatusBr.Text != "全部")
                {
                    string str = "";
                    if (cbbStatusBr.Text == "已归还")
                    {
                        str = "ReturnTime<>'' ";
                    }
                    else if (cbbStatusBr.Text == "未归还")
                    {
                        str = "ReturnTime='' ";
                    }
                    if (!string.IsNullOrEmpty(strCondition))
                    {
                        strCondition += " and ";
                    }
                    strCondition += str;
                }
                if (cbbPeopleBorrow.Text != "全部")
                {
                    if (!string.IsNullOrEmpty(strCondition))
                    {
                        strCondition += " and ";
                    }
                    string str = "PeopleBorrow='" + cbbPeopleBorrow.Text + "' ";
                    strCondition += str;
                }

                #endregion

                string strStart = dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string strEnd = dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");

                string strSql = "select top 1000 ID,ToolType,ToolName,ToolID,PeopleBorrow,BorrowTime,IsReturn,PeopleReturn,ReturnTime,BorrowDuration from tb_RecordBorrow " +
               "where BorrowTime BETWEEN '" + strStart + "' and '" + strEnd + "' ";// 
                if (!string.IsNullOrEmpty(strCondition))
                {
                    strSql += " and ";
                    strSql += strCondition;
                }
                strSql += " Order By BorrowTime Desc";
                dtRecord = datalogic.GetDataTable(strSql);
                JudgeNoRetrun();


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
                            DataRow dr = dtPage.NewRow();// ID           
                            dr[0] = dtRecord.Rows[i]["ID"].ToString();
                            dr[1] = dtRecord.Rows[i]["ToolType"].ToString();
                            dr[2] = dtRecord.Rows[i]["ToolName"].ToString();
                            dr[3] = dtRecord.Rows[i]["ToolID"].ToString();
                            dr[4] = dtRecord.Rows[i]["PeopleBorrow"].ToString();
                            dr[5] = dtRecord.Rows[i]["BorrowTime"].ToString();
                            dr[6] = dtRecord.Rows[i]["IsReturn"].ToString();
                            dr[7] = dtRecord.Rows[i]["PeopleReturn"].ToString();
                            dr[8] = dtRecord.Rows[i]["ReturnTime"].ToString();
                            dr[9] = dtRecord.Rows[i]["BorrowDuration"].ToString();
                            dtPage.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < iAllCount; i++)
                        {
                            DataRow dr = dtPage.NewRow();// ID ToolType ToolName ToolID PeopleBorrow  BorrowTime IsReturn  PeopleReturn ReturnTime BorrowDuration
                            dr[0] = dtRecord.Rows[i]["ID"].ToString();
                            dr[1] = dtRecord.Rows[i]["ToolType"].ToString();
                            dr[2] = dtRecord.Rows[i]["ToolName"].ToString();
                            dr[3] = dtRecord.Rows[i]["ToolID"].ToString();
                            dr[4] = dtRecord.Rows[i]["PeopleBorrow"].ToString();
                            dr[5] = dtRecord.Rows[i]["BorrowTime"].ToString();
                            dr[6] = dtRecord.Rows[i]["IsReturn"].ToString();
                            dr[7] = dtRecord.Rows[i]["PeopleReturn"].ToString();
                            dr[8] = dtRecord.Rows[i]["ReturnTime"].ToString();
                            dr[9] = dtRecord.Rows[i]["BorrowDuration"].ToString();
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

        private void sbtnQuery_Click(object sender, EventArgs e)    //tb_RecordBorrow  ID ToolType ToolName ToolID PeopleBorrow BorrowTime PeopleReturn ReturnTime BorrowDuration 
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
                        string strsql = "delete from tb_RecordBorrow where ID='" + strID + "'";
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

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0) return;
            DataRow dr = this.gridView1.GetDataRow(hand);
            if (dr == null) return;
            string str = dr["IsReturn"].ToString();
            if (str =="未归还")// // ReturnTime  IsReturn
            {
                e.Appearance.ForeColor = Color.Orange   ;// 改变行字体颜色
                e.Appearance.BackColor = Color.Orange;// 改变行背景颜色
                e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            }

            //str = dr["BorrowDuration"].ToString();
            //str 

            //if (dr["PeopleBR"].ToString() == "")
            //{
            //    e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
            //    e.Appearance.BackColor = Color.Red;// 改变行背景颜色
            //    e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色
            //}
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
                    dr[4] = dtRecord.Rows[i]["PeopleBorrow"].ToString();
                    dr[5] = dtRecord.Rows[i]["BorrowTime"].ToString();
                    dr[6] = dtRecord.Rows[i]["IsReturn"].ToString();
                    dr[7] = dtRecord.Rows[i]["PeopleReturn"].ToString();
                    dr[8] = dtRecord.Rows[i]["ReturnTime"].ToString();
                    dr[9] = dtRecord.Rows[i]["BorrowDuration"].ToString();
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
                    dr[4] = dtRecord.Rows[i]["PeopleBorrow"].ToString();
                    dr[5] = dtRecord.Rows[i]["BorrowTime"].ToString();
                    dr[6] = dtRecord.Rows[i]["IsReturn"].ToString();
                    dr[7] = dtRecord.Rows[i]["PeopleReturn"].ToString();
                    dr[8] = dtRecord.Rows[i]["ReturnTime"].ToString();
                    dr[9] = dtRecord.Rows[i]["BorrowDuration"].ToString();
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
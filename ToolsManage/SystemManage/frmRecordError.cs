﻿using System;
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
    public partial class frmRecordError : DevExpress.XtraEditors.XtraForm
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

        public frmRecordError()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
        }

        private void frmRecordAlarm_Load(object sender, EventArgs e)
        {
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

        private void TableShowInit()//tb_RecordErr ID Type Point EventContent Remark Time
        {
            gridView1.Columns.Clear();
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Type";
            Col1.Caption = "类型";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Point";
            Col1.Caption = "异常点";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "EventContent";
            Col1.Caption = "内容";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Remark";
            Col1.Caption = "备注";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Time";
            Col1.Caption = "时间";
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
                    strCondition = "Type='" + cbbType.Text + "' ";
                }

                #endregion

                string strStart = dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string strEnd = dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");

                string strSql = "select top 1000 ID,Type,Point,EventContent,Remark,Time from tb_RecordErr " +
                "where Time BETWEEN '" + strStart + "' and '" + strEnd + "' ";
                if (!string.IsNullOrEmpty(strCondition))
                {
                    strSql += " and ";
                    strSql += strCondition;
                }
                strSql += " Order By Time Desc";
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
                            DataRow dr = dtPage.NewRow();// ID     
                            dr[0] = dtRecord.Rows[i]["ID"].ToString();
                            dr[1] = dtRecord.Rows[i]["Type"].ToString();
                            dr[2] = dtRecord.Rows[i]["Point"].ToString();
                            dr[3] = dtRecord.Rows[i]["EventContent"].ToString();
                            dr[4] = dtRecord.Rows[i]["Remark"].ToString();
                            dr[5] = dtRecord.Rows[i]["Time"].ToString();
                            dtPage.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < iAllCount; i++)
                        {
                            DataRow dr = dtPage.NewRow();// ID  OpenType GroupName UserName OpenTime CloseTime DurationTime
                            dr[0] = dtRecord.Rows[i]["ID"].ToString();
                            dr[1] = dtRecord.Rows[i]["Type"].ToString();
                            dr[2] = dtRecord.Rows[i]["Point"].ToString();
                            dr[3] = dtRecord.Rows[i]["EventContent"].ToString();
                            dr[4] = dtRecord.Rows[i]["Remark"].ToString();
                            dr[5] = dtRecord.Rows[i]["Time"].ToString();
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
            column.ColumnName = "Type";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Point";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "EventContent";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Remark";
            dtPage.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Time";
            dtPage.Columns.Add(column);
        }

        #endregion


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
                        string strsql = "delete from tb_RecordErr where ID='" + strID + "'";
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
                    dr[1] = dtRecord.Rows[i]["Type"].ToString();
                    dr[2] = dtRecord.Rows[i]["Point"].ToString();
                    dr[3] = dtRecord.Rows[i]["EventContent"].ToString();
                    dr[4] = dtRecord.Rows[i]["Remark"].ToString();
                    dr[5] = dtRecord.Rows[i]["Time"].ToString();
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
                    dr[1] = dtRecord.Rows[i]["Type"].ToString();
                    dr[2] = dtRecord.Rows[i]["Point"].ToString();
                    dr[3] = dtRecord.Rows[i]["EventContent"].ToString();
                    dr[4] = dtRecord.Rows[i]["Remark"].ToString();
                    dr[5] = dtRecord.Rows[i]["Time"].ToString();
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
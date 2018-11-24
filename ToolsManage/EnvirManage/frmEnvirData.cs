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

namespace ToolsManage.EnvirManage
{
    public partial class frmEnvirData : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        DataTable dtOut = new DataTable();

        public frmEnvirData()
        {
            InitializeComponent();
            this.gridView1.IndicatorWidth = 40;
        }

        private void frmEnvirData_Load(object sender, EventArgs e)
        {
            LoadRoomName();
            SetQueryTime();
            BrTableShowInit();

            string strStart = dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strEnd = dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
            // tb_EnviriData  ID,Area,Addr,Temp,Humi,date
            string strSql = "select top 1000 ID,Area,Addr,Temp,Humi,date from tb_EnviriData " +
                 "where date BETWEEN '" + strStart + "' and '" + strEnd + "' Order By date Desc";
            dtOut = datalogic.GetDataTable(strSql);
            gridControl1.DataSource = dtOut;
        }

        private void LoadRoomName()   
        {
            cbbArea.Items.Clear();

            string strSql = "select RoomName from tb_RoomInfo ";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow datarow in dt.Rows)
            {
                string str = datarow["RoomName"].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    cbbArea.Items.Add(str);
                }
            }
            cbbArea.Items.Add("全部");
            cbbArea.Text = "全部";
        }

        private void SetQueryTime()
        {
            DateTime time = DateTime.Now.AddMonths(-1);
            dtpStart.Value = new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
            dtpEnd.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }

        private void BrTableShowInit()
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Area";
            Col1.Caption = "区域";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Addr";
            Col1.Caption = "变送器地址";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Temp";
            Col1.Caption = "温度 ℃";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "Humi";
            Col1.Caption = "湿度 RH%";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "date";
            Col1.Caption = "时间";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            sbtnQuery.Enabled = false;
            ConditionQuery();
            sbtnQuery.Enabled = true;
        }

        private void ConditionQuery()
        {
            #region 查询条件
            string strCondition = "";

            if (cbbArea.Text != "全部")
                strCondition = "Area='" + cbbArea.Text + "' ";
            #endregion

            string strStart = dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strEnd = dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
            //tb_RecordInRoom ToolType,ToolName,ToolID,RFIDCoding,StoragePlace,InPeople,InTime
            string strSql = "select top 1000 ID,Area,Addr,Temp,Humi,date from tb_EnviriData " +
            "where date BETWEEN '" + strStart + "' and '" + strEnd + "' ";// 
            if (!string.IsNullOrEmpty(strCondition))
            {
                strSql += " and ";
                strSql += strCondition;
            }
            strSql += " Order By date Desc";
            dtOut = datalogic.GetDataTable(strSql);
            gridControl1.DataSource = dtOut;
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
                        string strsql = "delete from tb_EnviriData where ID='" + strID + "'";
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
                    AsposeExcelTools.DataTableToExcel(dtOut, savePath, out outError);

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
                if (frmMain.blDebug)
                {
                    MessageUtil.ShowTips(ee.Message);
                }
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
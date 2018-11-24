using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Threading;

using ToolsManage.BaseClass;
using ToolsManage.BaseClass.DoorClass;

using WG3000_COMM.Core;

namespace ToolsManage.ToolsManage
{
    public partial class frmBoxPowerManage : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        DataTable dtUser = new DataTable();
        DataTable dtBox = new DataTable();
        DataTable dtUserSelect = new DataTable();
        DataTable dtBoxSelect = new DataTable();

        clsWgInfo wgControl = new clsWgInfo();
        List<clsWgInfo> listWg = new List<clsWgInfo>();

        //wgMjController control = new wgMjController();

        public frmBoxPowerManage()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
            this.gridView2.IndicatorWidth = 40;
            this.gridView3.IndicatorWidth = 40;
            this.gridView4.IndicatorWidth = 40;
        }

        private void frmBoxPowerAdd_Load(object sender, EventArgs e)
        {
            //control.PORT = 60000;
            LoadBox();
            tbInitUser();
            tbInitBox();
            tbInitUserSelect();
            tbInitBoxSelect();

            ShowInitUser();
            ShowInitUserSelect();
            ShowInitBox();
            ShowInitBoxSelect();

            LoadGroup();
            LoadArea();
        }


        #region  子程序

        public void LoadBox()
        {
            try
            {
                #region  工具柜门禁

                string strSql = "select tvChildId,AreaName,PlaceName,DoorIp,DoorSn,BoxHasRfid,BoxRfidMain,wgPort from tb_Tools where  " +
                                "IsArea='" + ToolAreaType.工具柜.ToString() + "' and HasDoor='" + DeviceUsing.启用.ToString() + "'  ";
                DataTable dt = datalogic.GetDataTable(strSql);

                foreach (DataRow datarow in dt.Rows)
                {
                    string strChildId = datarow["tvChildId"].ToString();
                    string strArea = datarow["AreaName"].ToString();
                    string strName = datarow["PlaceName"].ToString();
                    string strIp = datarow["DoorIp"].ToString();
                    string strPort = datarow["wgPort"].ToString();
                    string strSn = datarow["DoorSn"].ToString();
                    string strHasRfid = datarow["BoxHasRfid"].ToString();
                    string strRfidMain = datarow["BoxRfidMain"].ToString();
                    int iSn = 0;
                    int iPort = 60000;
                    if (!string.IsNullOrEmpty(strSn))
                    {
                        iSn = Convert.ToInt32(strSn);
                    }
                    else
                    {
                        MessageUtil.ShowTips(strName + " 工具柜门禁SN为空");
                    }
                    if (!string.IsNullOrEmpty(strPort))
                    {
                        iPort = Convert.ToInt32(strPort);
                    }
                    else
                    {
                        MessageUtil.ShowTips(strName + " 工具柜门禁 端口号 为空");
                    }

                    clsWgInfo wgInfo = new clsWgInfo(strIp, iPort, iSn);
                    //wgInfo.StrNameOfWg = strName;
                    //wgInfo.DoorOrBoxDoor = WgDoorType.工具柜;
                    wgInfo.StrChildId = strChildId;
                    clsDoorInfo door = new clsDoorInfo(strName, 1);
                    wgInfo.listDoor.Add(door);
                    listWg.Add(wgInfo);

                    //boxDoor.StrChildId = strChildId;
                    //boxDoor.StrHasRfid = strHasRfid;
                    //boxDoor.StrRfidMain = strRfidMain;
                }
                #endregion


            }
            catch (Exception ex)
            {
                if (frmMain.blDebug)
                    MessageUtil.ShowTips(ex.Message);
            }
        }

        /// <summary>
        /// 工具柜 区域
        /// </summary>
        private void LoadArea()
        {
            string strSql = "select AreaName from tb_Tools where IsArea='" + ToolAreaType.区域.ToString() + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            foreach (DataRow dr in dt.Rows)
            {
                cbbArea.Items.Add(dr["AreaName"].ToString()); 
            }
            cbbArea.Items.Add("全部");
            cbbArea.Text = "全部";
        }

        /// <summary>
        /// 用户班组
        /// </summary>
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

        private void tbInitUser()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");// ID,UserName,IcNo,CardNo
            column.ColumnName = "tvChildId";
            dtUser.Columns.Add(column); 

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserName";
            dtUser.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IcNo";//用户名
            dtUser.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CardNo";//卡编号
            dtUser.Columns.Add(column);

        }

        private void tbInitBox()
        {
            DataColumn column; 

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");// ID,tvParent,tvChildId,PlaceName
            column.ColumnName = "ID";
            dtBox.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "tvParent";
            dtBox.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "tvChildId";//用户名
            dtBox.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PlaceName";//卡编号
            dtBox.Columns.Add(column);
        }

        private void tbInitUserSelect()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");// ID,UserName,IcNo,CardNo
            column.ColumnName = "tvChildId";
            dtUserSelect.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserName";
            dtUserSelect.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IcNo";//用户名
            dtUserSelect.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CardNo";//卡编号
            dtUserSelect.Columns.Add(column);

        }

        private void tbInitBoxSelect()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");// ID,tvParent,tvChildId,PlaceName
            column.ColumnName = "ID";
            dtBoxSelect.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "tvParent";
            dtBoxSelect.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "tvChildId";//用户名
            dtBoxSelect.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PlaceName";//卡编号
            dtBoxSelect.Columns.Add(column);

        }

        /// <summary>
        /// 用户表
        /// </summary>
        private void ShowInitUser()
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// UserName,IcNo,CardNo
            Col1.FieldName = "tvChildId";
            Col1.Caption = "tvChildId";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "UserName";
            Col1.Caption = "姓名";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IcNo";
            Col1.Caption = "卡号";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "CardNo";
            Col1.Caption = "编号";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);
        }

        /// <summary>
        /// 用户表 选择后
        /// </summary>
        private void ShowInitUserSelect()
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// UserName,IcNo,CardNo
            Col1.FieldName = "tvChildId";
            Col1.Caption = "tvChildId";
            Col1.Visible = false;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "UserName";
            Col1.Caption = "姓名";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IcNo";
            Col1.Caption = "卡号";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "CardNo";
            Col1.Caption = "编号";
            Col1.Visible = true;
            gridView3.Columns.Add(Col1);
        }

        /// <summary>
        /// 工具柜表
        /// </summary>
        private void ShowInitBox()
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// ID,tvParent,tvChildId,PlaceName
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView2.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "tvParent";
            Col1.Caption = "tvParent";
            Col1.Visible = false;
            gridView2.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "tvChildId";
            Col1.Caption = "tvChildId";
            Col1.Visible = false;
            gridView2.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "PlaceName";
            Col1.Caption = "名称";
            Col1.Visible = true;
            gridView2.Columns.Add(Col1);
        }

        /// <summary>
        /// 工具柜表
        /// </summary>
        private void ShowInitBoxSelect()
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();// ID,tvParent,tvChildId,PlaceName
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView4.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "tvParent";
            Col1.Caption = "tvParent";
            Col1.Visible = false;
            gridView4.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "tvChildId";
            Col1.Caption = "tvChildId";
            Col1.Visible = false;
            gridView4.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "PlaceName";
            Col1.Caption = "名称";
            Col1.Visible = true;
            gridView4.Columns.Add(Col1);
        }

        #endregion

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

        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        private void gridView4_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        private void cbbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtUser.Rows.Clear();
            if (cbbGroup.Text == "全部")
            {
                string strSql = "select tvChildId,UserName,IcNo,CardNo from tb_DoorUser where IsGroup='" + GroupPeoType.人员.ToString() + "'";
                DataTable dt = datalogic.GetDataTable(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow row = dtUser.NewRow();
                    row["tvChildId"] = dr["tvChildId"].ToString();
                    row["UserName"] = dr["UserName"].ToString();
                    row["IcNo"] = dr["IcNo"].ToString();
                    row["CardNo"] = dr["CardNo"].ToString();
                    dtUser.Rows.Add(row);
                }
                this.gridControl1.DataSource = dtUser;
            }
            else
            {
                string strSql = "select tvChildId,UserName,IcNo,CardNo from tb_DoorUser where GroupName='" + cbbGroup.Text.Trim() + "' and IsGroup='" + GroupPeoType.人员.ToString() + "' ";
                DataTable dt = datalogic.GetDataTable(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow row = dtUser.NewRow();
                    row["tvChildId"] = dr["tvChildId"].ToString();
                    row["UserName"] = dr["UserName"].ToString();
                    row["IcNo"] = dr["IcNo"].ToString();
                    row["CardNo"] = dr["CardNo"].ToString();
                    dtUser.Rows.Add(row);
                }
                this.gridControl1.DataSource = dtUser;
            }
        }

        private void cbbArea_SelectedIndexChanged(object sender, EventArgs e)// ID,tvParent,tvChildId,PlaceName
        {
            dtBox.Rows.Clear(); 
            if (cbbArea.Text == "全部")
            {
                string strSql = "select ID,tvParent,tvChildId,PlaceName from tb_Tools where IsArea='" + ToolAreaType.工具柜.ToString() + "'";
                DataTable dt = datalogic.GetDataTable(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow row = dtBox.NewRow();
                    row["ID"] = dr["ID"].ToString();
                    row["tvParent"] = dr["tvParent"].ToString();
                    row["tvChildId"] = dr["tvChildId"].ToString();
                    row["PlaceName"] = dr["PlaceName"].ToString();
                    dtBox.Rows.Add(row);
                }
                this.gridControl2.DataSource = dtBox;
            }
            else
            {
                string strSql = "select ID,tvParent,tvChildId,PlaceName from tb_Tools where AreaName='" + cbbArea.Text.Trim() + "' and IsArea='" + ToolAreaType.工具柜.ToString() + "' ";
                DataTable dt = datalogic.GetDataTable(strSql);
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow row = dtBox.NewRow();
                    row["ID"] = dr["ID"].ToString();
                    row["tvParent"] = dr["tvParent"].ToString();
                    row["tvChildId"] = dr["tvChildId"].ToString();
                    row["PlaceName"] = dr["PlaceName"].ToString();
                    dtBox.Rows.Add(row);
                }
                this.gridControl2.DataSource = dtBox;
            }
        }

        private void btnUserAddAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in dtUser.Rows)
            {
                DataRow row = dtUserSelect.NewRow();
                row["tvChildId"] = dr["tvChildId"].ToString();
                row["UserName"] = dr["UserName"].ToString();
                row["IcNo"] = dr["IcNo"].ToString();
                row["CardNo"] = dr["CardNo"].ToString();// ID,UserName,IcNo,CardNo
                dtUserSelect.Rows.Add(row);
            }
            this.gridControl3.DataSource = dtUserSelect;
            dtUser.Rows.Clear();
            this.gridControl1.DataSource = dtUser;
        }

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string strID = gridView1.GetRowCellDisplayText(rows[i], "tvChildId").ToString();
                    string strName = gridView1.GetRowCellDisplayText(rows[i], "UserName").ToString();
                    string strIcNo = gridView1.GetRowCellDisplayText(rows[i], "IcNo").ToString();
                    string strCardNo = gridView1.GetRowCellDisplayText(rows[i], "CardNo").ToString();
                    DataRow row = dtUserSelect.NewRow();
                    row["tvChildId"] = strID;
                    row["UserName"] = strName;
                    row["IcNo"] = strIcNo;
                    row["CardNo"] = strCardNo;
                    dtUserSelect.Rows.Add(row);
                }
                this.gridControl3.DataSource = dtUserSelect;
                for (int i = 0; i < rows.Length; i++)
                {
                    string strID = gridView1.GetRowCellDisplayText(rows[i], "tvChildId").ToString();

                    foreach (DataRow dr in dtUser.Rows)
                    {
                        if (dr["tvChildId"].ToString() == strID)
                        {
                            dtUser.Rows.Remove(dr );
                            break;
                        }
                    }

                    //for (int iRow = 0; iRow < dtUser.Rows.Count; iRow++)
                    //{
                    //    if (dtUser.Rows[iRow]["ID"].ToString() == strID)
                    //    {
                    //        dtUser.Rows.RemoveAt(iRow );
                    //        break;
                    //    }
                    //}
                }
                this.gridControl1.DataSource = dtUser;

            }

        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            int[] rows = gridView3.GetSelectedRows(); //选中多行    dtUserSelect
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string strID = gridView3.GetRowCellDisplayText(rows[i], "tvChildId").ToString();
                    string strName = gridView3.GetRowCellDisplayText(rows[i], "UserName").ToString();
                    string strIcNo = gridView3.GetRowCellDisplayText(rows[i], "IcNo").ToString();
                    string strCardNo = gridView3.GetRowCellDisplayText(rows[i], "CardNo").ToString();
                    DataRow row = dtUser.NewRow();
                    row["tvChildId"] = strID;
                    row["UserName"] = strName;
                    row["IcNo"] = strIcNo;
                    row["CardNo"] = strCardNo;
                    dtUser.Rows.Add(row);
                }
                this.gridControl1.DataSource = dtUser;
                for (int i = 0; i < rows.Length; i++)
                {
                    string strID = gridView3.GetRowCellDisplayText(rows[i], "tvChildId").ToString();

                    foreach (DataRow dr in dtUserSelect.Rows)
                    {
                        if (dr["tvChildId"].ToString() == strID)
                        {
                            dtUserSelect.Rows.Remove(dr);
                            break;
                        }
                    }
                }
                this.gridControl3.DataSource = dtUserSelect;
            }
        }

        private void btnUserDeleteAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in dtUserSelect.Rows)
            {
                DataRow row = dtUser.NewRow();
                row["tvChildId"] = dr["tvChildId"].ToString();
                row["UserName"] = dr["UserName"].ToString();
                row["IcNo"] = dr["IcNo"].ToString();
                row["CardNo"] = dr["CardNo"].ToString();
                dtUser.Rows.Add(row);
            }
            this.gridControl1.DataSource = dtUser;
            dtUserSelect.Rows.Clear();
            this.gridControl3.DataSource = dtUserSelect;
        }

        private void btnBoxAddAll_Click(object sender, EventArgs e) //ID,tvParent,tvChildId,PlaceName
        {
            foreach (DataRow dr in dtBox.Rows) 
            {
                DataRow row = dtBoxSelect.NewRow(); 
                row["ID"] = dr["ID"].ToString();
                row["tvParent"] = dr["tvParent"].ToString();
                row["tvChildId"] = dr["tvChildId"].ToString();
                row["PlaceName"] = dr["PlaceName"].ToString();
                dtBoxSelect.Rows.Add(row);
            }
            this.gridControl4.DataSource = dtBoxSelect;
            dtBox.Rows.Clear();
            this.gridControl2.DataSource = dtBox;
        }

        private void btnBoxAdd_Click(object sender, EventArgs e)//ID,tvParent,tvChildId,PlaceName
        {
            int[] rows = gridView2.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string strID = gridView2.GetRowCellDisplayText(rows[i], "ID").ToString();
                    string strParent = gridView2.GetRowCellDisplayText(rows[i], "tvParent").ToString();
                    string strChildId = gridView2.GetRowCellDisplayText(rows[i], "tvChildId").ToString();
                    string strName = gridView2.GetRowCellDisplayText(rows[i], "PlaceName").ToString();
                    DataRow row = dtBoxSelect.NewRow(); 
                    row["ID"] = strID;
                    row["tvParent"] = strParent;
                    row["tvChildId"] = strChildId;
                    row["PlaceName"] = strName;
                    dtBoxSelect.Rows.Add(row);
                }
                this.gridControl4.DataSource = dtBoxSelect;
                for (int i = 0; i < rows.Length; i++)
                {
                    string strID = gridView2.GetRowCellDisplayText(rows[i], "ID").ToString();
                    foreach (DataRow dr in dtBox.Rows) 
                    {
                        if (dr["ID"].ToString() == strID)
                        {
                            dtBox.Rows.Remove(dr);
                            break;
                        }
                    }
                }
                this.gridControl2.DataSource = dtBox;
            }
        }

        private void btnBoxDelete_Click(object sender, EventArgs e)//ID,tvParent,tvChildId,PlaceName
        {
            int[] rows = gridView4.GetSelectedRows(); //选中多行    dtUserSelect
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string strID = gridView4.GetRowCellDisplayText(rows[i], "ID").ToString();
                    string strParent = gridView4.GetRowCellDisplayText(rows[i], "tvParent").ToString();
                    string strChildId = gridView4.GetRowCellDisplayText(rows[i], "tvChildId").ToString();
                    string strName = gridView4.GetRowCellDisplayText(rows[i], "PlaceName").ToString();
                    DataRow row = dtBox.NewRow(); 
                    row["ID"] = strID;
                    row["tvParent"] = strParent;
                    row["tvChildId"] = strChildId;
                    row["PlaceName"] = strName;
                    dtBox.Rows.Add(row);
                }
                this.gridControl2.DataSource = dtBox;
                for (int i = 0; i < rows.Length; i++)
                {
                    string strID = gridView4.GetRowCellDisplayText(rows[i], "ID").ToString();

                    foreach (DataRow dr in dtBoxSelect.Rows) 
                    {
                        if (dr["ID"].ToString() == strID)
                        {
                            dtBoxSelect.Rows.Remove(dr);
                            break;
                        }
                    }
                }
                this.gridControl4.DataSource = dtBoxSelect;
            }
        }

        private void btnBoxDeleteAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in dtBoxSelect.Rows)
            {
                DataRow row = dtBox.NewRow();  
                row["ID"] = dr["ID"].ToString();
                row["tvParent"] = dr["tvParent"].ToString();
                row["tvChildId"] = dr["tvChildId"].ToString();
                row["PlaceName"] = dr["PlaceName"].ToString();
                dtBox.Rows.Add(row);
            }
            this.gridControl2.DataSource = dtBox;
            dtBoxSelect.Rows.Clear();
            this.gridControl4.DataSource = dtBoxSelect;
        }

        private void sbtnDeleteExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtnAdd_Click(object sender, EventArgs e)
        {
            sbtnAdd.Enabled = false;
            sbtnDelete.Enabled = false;
            sbtnDeleteExit.Enabled = false;
            lblInfo.Visible = true ;

            if (MessageUtil.ShowYesNoAndTips("确定上传？") == DialogResult.Yes)
            {
                //所有选中的 工具柜
                bool blBox = true;
                foreach (DataRow dr in dtBoxSelect.Rows)
                {
                    string strChildId = dr["tvChildId"].ToString();
                    string strParentId = dr["tvParent"].ToString();
                    string strBoxName = dr["PlaceName"].ToString();
                    int iCount = listWg .Count;
                    if (iCount > 0)
                    {
                        for (int iIndex = 0; iIndex < iCount; iIndex++)
                        {
                            if (listWg[iIndex].StrChildId == strChildId)
                            {

                                wgControl.IntSn = listWg[iIndex].IntSn;
                                wgControl.StrIp  = listWg[iIndex].StrIp;
                                wgControl.IntPort  = listWg[iIndex].IntPort;
                                MjRegisterCard mjrc = new MjRegisterCard();
                                bool blUser = true;
                                //所有选中的人
                                foreach (DataRow row in dtUserSelect.Rows)
                                {
                                    string strIcNo = row["IcNo"].ToString();
                                    string strUserId = row["tvChildId"].ToString();
                                    string strName = row["UserName"].ToString();
                                    string strCardNo = row["CardNo"].ToString();

                                    if (wgControl.AddCardPower(strIcNo))
                                    {
                                        //ID,BoxParentId,BoxChildId,UserId,BoxName,UserName,CardNo,IcNo tb_BoxIcPower
                                        // ID,UserName,IcNo,CardNo
                                        string strSql = "select ID from tb_BoxIcPower where BoxChildId='" + strChildId + "' and IcNo='" + strIcNo + "'";
                                        DataTable dt = datalogic.GetDataTable(strSql);
                                        if (dt.Rows.Count == 0)
                                        {
                                            strSql = "insert into tb_BoxIcPower (BoxParentId,BoxChildId,UserId,BoxName,UserName,CardNo,IcNo)" +
                                                     "values ('" + strParentId + "','" + strChildId + "','" + strUserId + "','" + strBoxName + "','" + strName + "','" + strCardNo + "','" + strIcNo + "')";
                                            datalogic.SqlComNonQuery(strSql);
                                        }
                                    }
                                    else
                                    {
                                        blUser = false;
                                        break;
                                    }
                                    Thread.Sleep(20);
                                }
                                //人员中有授权错误
                                if (blUser == false)
                                    blBox = false;
                                break;
                            }
                        }
                    }
                    // 工具柜 有授权错误
                    if (blBox == false)
                        break;
                }
                if (blBox)
                {
                    MessageUtil.ShowTips("添加权限成功");
                }
                else
                {
                    MessageUtil.ShowTips("添加权限失败，请检查设备或网络连接");
                }
            }

           
            sbtnAdd.Enabled = true;
            sbtnDelete.Enabled = true;
            sbtnDeleteExit.Enabled = true;
            lblInfo.Visible = false;
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            sbtnAdd.Enabled = false;
            sbtnDelete.Enabled = false;
            sbtnDeleteExit.Enabled = false;
            lblInfo.Visible = true ;

            if (MessageUtil.ShowYesNoAndTips("确定上传？") == DialogResult.Yes)
            {
                //所有选中的 工具柜
                bool blBox = true;
                foreach (DataRow dr in dtBoxSelect.Rows)
                {
                    string strChildId = dr["tvChildId"].ToString();
                    string strParentId = dr["tvParent"].ToString();
                    string strBoxName = dr["PlaceName"].ToString();
                    int iCount = listWg.Count;
                    if (iCount > 0)
                    {
                        for (int iIndex = 0; iIndex < iCount; iIndex++)
                        {
                            if (listWg[iIndex].StrChildId == strChildId)
                            {
                                wgControl.IntSn = listWg[iIndex].IntSn;
                                wgControl.StrIp = listWg[iIndex].StrIp;
                                wgControl.IntPort = listWg[iIndex].IntPort;
                                //MjRegisterCard mjrc = new MjRegisterCard();
                                bool blUser = true;
                                //所有选中的人
                                foreach (DataRow row in dtUserSelect.Rows)
                                {
                                    string strIcNo = row["IcNo"].ToString();
                                    //string strUserId = row["tvChildId"].ToString();
                                    //string strName = row["UserName"].ToString();
                                    //string strCardNo = row["CardNo"].ToString();
                                    if (wgControl.DeleteCardPower(strIcNo))
                                    {
                                        string strSql = "delete from tb_BoxIcPower where BoxChildId='" + strChildId + "' and IcNo='" + strIcNo + "'";
                                        datalogic.SqlComNonQuery(strSql);
                                    }
                                    else
                                    {
                                        blUser = false;
                                        break;
                                    }
                                    Thread.Sleep(20);
                                }
                                //人员中有授权错误
                                if (blUser == false)
                                    blBox = false;
                                break;
                            }
                        }
                    }
                    // 工具柜 有授权错误
                    if (blBox == false)
                        break;
                }
                if (blBox)
                {
                    MessageUtil.ShowTips("删除权限成功");
                }
                else
                {
                    MessageUtil.ShowTips("删除权限失败，请检查设备或网络连接");
                }
            }


            sbtnAdd.Enabled = true;
            sbtnDelete.Enabled = true;
            sbtnDeleteExit.Enabled = true;
            lblInfo.Visible = false;
        }
    }
}
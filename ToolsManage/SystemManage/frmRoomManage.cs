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
using ToolsManage.Domain;

namespace ToolsManage.SystemManage
{
    public partial class frmRoomManage : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();
        IList<TbRoominfo> listRoomInfo = null;

        public frmRoomManage()
        {
            InitializeComponent();
            this.gridView1.IndicatorWidth = 40;
        }

        private void frmRoomManage_Load(object sender, EventArgs e)
        {
            TableShowInit();
            LoadAndUpShow();
        }

        private void LoadAndUpShow()
        {
            //string strSql = "select ID,RoomName,IsSingle,WsdAddr1,WsdAddr2,FireAddr1,FireAddr2,RelayAddr,AirAddr,AirFactory,AirAddr2,AirFactory2,IsExistCsj,IsExistJrq,IsExistXf from tb_RoomInfo ";
            //gridControl1.DataSource = datalogic.GetDataTable(strSql);
            listRoomInfo = TbRoominfo.FindAll("","Id ASC","",0,0);
            gridControl1.DataSource = listRoomInfo;
        }

        private void TableShowInit()//tb_RoomInfo ID,RoomName,IsSingle,WsdAddr1,WsdAddr2,FireAddr1,FireAddr2,RelayAddr
        {
            DevExpress.XtraGrid.Columns.GridColumn Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "ID";
            Col1.Caption = "ID";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "RoomName";
            Col1.Caption = "房间名称";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsSingle";
            Col1.Caption = "传感器为单个";
            Col1.Visible = false;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "WsdAddr1";
            Col1.Caption = "1#温湿度地址";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "WsdAddr2";
            Col1.Caption = "2#温湿度地址";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "FireAddr1";
            Col1.Caption = "1#烟感地址";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "FireAddr2";
            Col1.Caption = "2#烟感地址";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "RelayAddr";
            Col1.Caption = "控制板地址";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "AirAddr";
            Col1.Caption = "1#空调控制器地址";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "AirFactory";
            Col1.Caption = "1#空调厂家";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "AirAddr2";
            Col1.Caption = "2#空调控制器地址";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "AirFactory2";
            Col1.Caption = "2#空调厂家";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsExistCsj";
            Col1.Caption = "除湿机";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsExistJrq";
            Col1.Caption = "加热器";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);

            Col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            Col1.FieldName = "IsExistXf";
            Col1.Caption = "新风";
            Col1.Visible = true;
            gridView1.Columns.Add(Col1);
        }

        private void sbtnNew_Click(object sender, EventArgs e)
        {
            LoadAndUpShow();
        }

        private void sbtnAddUser_Click(object sender, EventArgs e)
        {
            frmRoomEdit frm = new frmRoomEdit(null);
            frm.Tag = "添加";
            frm.ShowDialog(this);
            frm.Dispose();
            LoadAndUpShow();
        }

        private void sbtnAlter_Click(object sender, EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                int ID = Convert.ToInt16(gridView1.GetRowCellDisplayText(rows[0], "ID"));
                TbRoominfo roomInfo = listRoomInfo.Find(p => p.ID == ID);
                if (roomInfo != null)
                {
                    frmRoomEdit frm = new frmRoomEdit(roomInfo);
                    frm.Tag = "修改";
                    frm.ShowDialog(this);
                    frm.Dispose();
                    LoadAndUpShow();
                }
                //frm.Tag = "修改";
                //frm.strID = gridView1.GetRowCellDisplayText(rows[0], "ID").ToString();
                //frm.strName = gridView1.GetRowCellDisplayText(rows[0], "RoomName").ToString();
                //frm.strWsd1 = gridView1.GetRowCellDisplayText(rows[0], "WsdAddr1").ToString();
                //frm.strFire1 = gridView1.GetRowCellDisplayText(rows[0], "FireAddr1").ToString();
                //frm.strRelay = gridView1.GetRowCellDisplayText(rows[0], "RelayAddr").ToString();
                //frm.strWsd2 = gridView1.GetRowCellDisplayText(rows[0], "WsdAddr2").ToString();
                //frm.strFire2 = gridView1.GetRowCellDisplayText(rows[0], "FireAddr2").ToString();
                //frm.strAir = gridView1.GetRowCellDisplayText(rows[0], "AirAddr").ToString();
                //frm.strFactory = gridView1.GetRowCellDisplayText(rows[0], "AirFactory").ToString();
                //frm.strAir2 = gridView1.GetRowCellDisplayText(rows[0], "AirAddr2").ToString();
                //frm.strFactory2 = gridView1.GetRowCellDisplayText(rows[0], "AirFactory2").ToString();
                //frm.IsExistCsj = gridView1.GetRowCellDisplayText(rows[0], "IsExistCsj").ToString() == DeviceUsing.启用.ToString() ? true : false;
                //frm.IsExistJrq = gridView1.GetRowCellDisplayText(rows[0], "IsExistJrq").ToString() == DeviceUsing.启用.ToString() ? true : false;
                //frm.IsExistXf = gridView1.GetRowCellDisplayText(rows[0], "IsExistXf").ToString() == DeviceUsing.启用.ToString() ? true : false;
                //frm.ShowDialog(this);
                //frm.Dispose();
                //LoadAndUpShow();
            }
            else
            {
                MessageUtil.ShowTips("无选中的房间");
            }
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows(); //选中多行   
            if (rows.Length > 0)
            {
                if (MessageUtil.ShowYesNoAndTips("确定删除表中所选房间？") == DialogResult.Yes)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        string strID = gridView1.GetRowCellDisplayText(rows[i], "ID").ToString();
                        string strsql = "delete from tb_RoomInfo where ID='" + strID + "'";
                        datalogic.SqlComNonQuery(strsql);
                    }
                    LoadAndUpShow();
                }
            }
            else
            {
                MessageUtil.ShowTips("无选中的房间");
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
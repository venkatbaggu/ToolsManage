using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ReaderB;//RFID

using ToolsManage.BaseClass;

namespace ToolsManage.ToolsManage
{
    public partial class frmBorrow : DevExpress.XtraEditors.XtraForm
    {
        DataLogic datalogic = new DataLogic();

        DataTable dataTable = new DataTable();

        #region RFID

        int Port = 1;   //串口号
        byte ComAdr = 0xff; //当前操作的ComAdr
        byte Baud = 5; //波特率 5=57600
        int fCmdRet;  // 连接RFID返回值
        private int frmcomportindex;//输入变量，返回与读写器连接端口对应的句柄，应用程序通过该句柄可以操作连接在相应端口的读写器。如果打开不成功，返回的句柄值为-1。
        private bool fIsInventoryScan;
        private string fInventory_EPC_List; //存贮询查列表（如果读取的数据没有变化，则不进行刷新）
        //private bool fAppClosed; //在测试模式下响应关闭应用程序

        #endregion

        public frmBorrow()
        {
            InitializeComponent();

            this.gridView1.IndicatorWidth = 40;
        }

        private void frmBorrow_Load(object sender, EventArgs e)
        {
            TableInit();

            #region  连接RFID 读写器

            try
            {
                StaticClassReaderB.CloseComPort();//先关闭RFID读写器
                fCmdRet = StaticClassReaderB.OpenComPort(Port, ref ComAdr, Baud, ref frmcomportindex);
                if (fCmdRet == 0)
                {
                    timer1.Enabled = true;
                }
                else
                {
                    timer1.Enabled = false;
                    MessageUtil.ShowError("RFID读写器连接错误，请检查RFID读写器");
                }
            }
            catch
            {
                timer1.Enabled = false;
                MessageUtil.ShowError("RFID读写器连接错误，请检查RFID读写器");
            }

            #endregion

        }

        private void TableInit()//ToolType,ToolName,ToolID,RFIDCoding,StoragePlace
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolID";
            column.Caption = "工具ID";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "RfidCoding";
            column.Caption = "RFID编码";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolType";
            column.Caption = "工具种类";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ToolName";
            column.Caption = "工具名称";
            dataTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "StoragePlace";
            column.Caption = "存放位置";
            dataTable.Columns.Add(column);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
            {
                return;
            }
            Inventory();
        }

        byte MaskMem = 0;
        byte[] MaskAdr = new byte[2];
        byte MaskLen = 0;
        byte[] MaskData = new byte[100];
        byte MaskFlag = 0;
        byte AdrTID = 0;
        byte LenTID = 0;
        byte TIDFlag = 0;
        byte Ant = 0;
        int Totallen = 0;

        private void Inventory()
        {
            try
            {
                int CardNum = 0;//输出变量，电子标签的张数
                int Totallen = 0;//EPC 的字节数
                int EPClen, m;
                byte[] EPC = new byte[5000];//指向输出数组变量 是读到的电子标签的EPC数据，是一张标签的EPC长度+一张标签的EPC号，依此累加
                int CardIndex;
                string temps;
                string sEPC = null;
                fIsInventoryScan = true;
                byte LenTID = 0;
                byte TIDFlag = 0;
                byte AdrTID = 0;
                bool isonlistview;
                ListViewItem aListItem = new ListViewItem();
                fCmdRet = StaticClassReaderB.Inventory_G2(ref ReaderAddr, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, bEpcArray, ref Ant, ref Totallen, ref CardCount, ReaderHandle); 
                    
                if ((fCmdRet == 1) || (fCmdRet == 2) || (fCmdRet == 3) || (fCmdRet == 4) || (fCmdRet == 0xFB))//代表已查找结束，
                {
                    byte[] daw = new byte[Totallen];
                    Array.Copy(EPC, daw, Totallen);//将EPC中的数据拷贝到daw
                    temps = ByteArrayToHexString(daw);//转换成字符串
                    fInventory_EPC_List = temps;            //存贮记录
                    m = 0;
                    if (CardNum == 0)
                    {
                        fIsInventoryScan = false;
                        return;
                    }
                    for (CardIndex = 0; CardIndex < CardNum; CardIndex++)
                    {
                        EPClen = daw[m];//epc的长度
                        sEPC = temps.Substring(m * 2 + 2, EPClen * 2);//得到一个EPC数据
                        m = m + EPClen + 1;//下一个EPC标签的 标签长度指示位置
                        if (sEPC.Length != EPClen * 2)
                        {
                            return;
                        }
                    }
                }
                isonlistview = false;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    if (datarow["RfidCoding"].ToString() == sEPC)
                    {
                        isonlistview = true;
                        break;
                    }
                }

                if (!isonlistview)
                {
                    tbRFID.Text = sEPC;
                }
                fIsInventoryScan = false;
            }
            catch
            { 
            
            }
        }

        private string ByteArrayToHexString(byte[] data)//十六进制转字符串
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();

        }

        private void tbRFID_TextChanged(object sender, EventArgs e)
        {
            if (tbRFID.Text.Length == 8)
            {
                string strSql = "select ToolType,ToolName,ToolID,RFIDCoding,StoragePlace from tb_Tools where RFIDCoding='" + tbRFID.Text + "' and IsInStore='在库'";
                DataTable dt = datalogic.GetDataTable(strSql);
                if (dt.Rows.Count == 1)
                {
                    dt.Rows[0]["ToolID"].ToString();
                    DataRow row = dataTable.NewRow();
                    row["ToolID"] = dt.Rows[0]["ToolID"].ToString();
                    row["RFIDCoding"] = dt.Rows[0]["RFIDCoding"].ToString();
                    row["ToolType"] = dt.Rows[0]["ToolType"].ToString();
                    row["ToolName"] = dt.Rows[0]["ToolName"].ToString();
                    row["StoragePlace"] = dt.Rows[0]["StoragePlace"].ToString();
                    dataTable.Rows.Add(row);
                    gridControl1.DataSource = dataTable;
                }
            }
        }

        private void sbtnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                StaticClassReaderB.CloseComPort();
            }
            catch
            {
                MessageUtil.ShowError("RFID读写器错误，请检查RFID读写器");
            }

            this.Close();
        }

        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            int focusedRow = this.gridView1.FocusedRowHandle;
            dataTable.Rows.RemoveAt(focusedRow);
            this.gridControl1.DataSource = dataTable;
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

        private void sbtnNew_Click(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = dataTable;
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            foreach (DataRow datarow in dataTable.Rows)
            {
                string str = datarow["RfidCoding"].ToString();
                string strSql = strSql = "update tb_Tools set IsInStore='借出' where RFIDCoding='" + str + "' ";
                datalogic.SqlComNonQuery(strSql);
            }
            dataTable.Rows.Clear();
            gridControl1.DataSource = dataTable;
            MessageUtil.ShowTips("以上工具成功借出");
        }

        private void sbtnHandBorrow_Click(object sender, EventArgs e)
        {

        }





    }
}
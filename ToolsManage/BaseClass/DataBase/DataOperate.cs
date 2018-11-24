using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using System.IO;//图片
using System.Drawing;

using Excel = Microsoft.Office.Interop.Excel;

namespace ToolsManage.BaseClass
{
    class DataOperate
    {
        DataLogic datalogic = new DataLogic();

        /// <summary>
        /// 获取ChildId
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string GetChildId(string tableName, string field)
        {
            string strRet = "";
            int iNo = 0;
            string strSql = "select top 1 " + field + " from " + tableName + " order by ID desc ";// select top 1 * from table order by id desc
            object ob = datalogic.SqlComScalar(strSql);
            if (ob != null)
            {
                iNo = Convert.ToInt32(ob.ToString());
            }
            iNo++;
            strRet = iNo.ToString();
            return strRet;
        }

        public bool blCheckHas(string strSql)
        {
            bool blRet = false;
            DataTable dt = datalogic.GetDataTable(strSql);
            int i = dt.Rows.Count;
            if (i > 0)
                blRet = true;
            else
                blRet = false;
            return blRet;
        }

        /// <summary>
        /// 查找表中某列是否有相同的数据
        /// </summary>
        /// <param name="strTableName">数据表名</param>
        /// <param name="strWhere">查询条件字符串</param>
        /// <returns></returns>
        public int boolCheckSame(string strTableName, string strWhere)
        {
            int iReturn = 0;
            string strSql = "Select * From" + " " + strTableName + " " + strWhere;
            DataTable dt = datalogic.GetDataTable(strSql);
            iReturn = dt.Rows.Count;
            return iReturn;
        }

        #region
        /// <summary>
        /// ComboBox控件绑定到数据源
        /// </summary>
        /// <param name="cbx">ComboBox控件</param>
        /// <param name="strValueColumn">代码列的名称</param>
        /// <param name="strDisplayColumn">名称列的名称</param>
        /// <param name="strSql">查询代码表的SQL语句</param>
        /// <param name="strTableName">代码表名称</param>
        public void ComboBoxBind(ComboBox cbx, string strValueColumn, string strDisplayColumn, string strSql)
        {
            try
            {
                cbx.BeginUpdate();
                cbx.DataSource = datalogic.GetDataTable(strSql);
                cbx.DisplayMember = strDisplayColumn;
                cbx.ValueMember = strValueColumn;
                cbx.EndUpdate();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示");
                throw e;
            }
        }
        #endregion

        /// <summary>
        /// 生成各种数据表的代码字段的值
        /// </summary>
        /// <param name="strTableName">数据表名</param>
        /// <param name="strCodeColumn">要选择的列</param>
        /// <param name="strHeader">各种代码头</param>  
        /// <param name="intLength">代码可变动部分的长度</param>
        /// <returns></returns>
        public string BuildCode(string strTableName, string strCodeColumn, string strHeader, int intLength)
        {
            string strSql = "Select " + strCodeColumn + " From " + strTableName + " ";//获取主键列的最大值

            DataTable dt = datalogic.GetDataTable(strSql);
            string strMaxCode = dt.Rows.Count.ToString();
            dt.Dispose();
            if (strMaxCode == "0")
            {
                strMaxCode = strHeader + FormatString(intLength);
            }
            string strMaxSeqNum = strMaxCode.Substring(strHeader.Length);
            return strHeader + (Convert.ToInt32(strMaxSeqNum) + 1).ToString(FormatString(intLength));
        }

        /// <summary>
        /// 格式化具有流水性质的编号
        /// </summary>
        /// <param name="intLength"></param>
        /// <returns></returns>
        public string FormatString(int intLength)
        {
            string strFormat = String.Empty;
            for (int i = 0; i < intLength; i++)
            {
                strFormat = strFormat + "0";
            }
            return strFormat;
        }


        #region 将DataGridView控件中数据导出到Excel
        /// <summary>
        /// 将DataGridView控件中数据导出到Excel
        /// </summary>
        /// <param name="gridView">DataGridView对象</param>
        /// <param name="isShowExcle">是否显示Excel界面</param>
        /// <returns></returns>
        public bool ExportDataGridview(DataGridView gridView, bool isShowExcle)
        {
            try
            {
                if (gridView.Rows.Count == 0)
                    return false;
                //建立Excel对象
                Excel.Application excel = new Excel.Application();
                excel.Application.Workbooks.Add(true);
                excel.Visible = isShowExcle;
                //生成字段名称
                for (int i = 0; i < gridView.ColumnCount; i++)
                {
                    excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
                }
                //填充数据
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    for (int j = 0; j < gridView.ColumnCount; j++)
                    {
                        if (gridView[j, i].ValueType == typeof(string))
                        {
                            excel.Cells[i + 2, j + 1] = "'" + gridView[j, i].Value.ToString();
                        }
                        else
                        {
                            excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
                        }
                    }
                }
                return true;
            }
            catch
            {
                return true;
            }
        }
        #endregion

    }
}

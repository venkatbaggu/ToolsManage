using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ToolsManage.BaseClass
{
    class StationServer
    {
        #region  建立数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
        public SqlConnection GetCon()
        {
            SqlConnection sqlCon = null;
            //string sqlcon = "Data Source=192.168.1.197;Database=db_ToolsManageServer;User id=sa;PWD=123321";
            //string sqlcon = "Data Source=(local);Database=ToolsManage;User id=sa;PWD=123321";
            string sqlcon = "Data Source=(local);Database=ToolsManage;User id=sa;PWD=123456";
            try
            {
                sqlCon = new SqlConnection(sqlcon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                throw ex;
            }
            return sqlCon;
        }
        #endregion

        #region
        /// <summary>
        /// 重新封装ExecuteScalar方法，得到结果集中的第一行的第一列
        /// </summary>
        /// <param name="strSql">Transact-SQL语句</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object SqlComScalar(string strSql)
        {
            object obj = null;
            SqlCommand sqlcom = null;
            SqlConnection sqlcon = this.GetCon();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                sqlcom = new SqlCommand(strSql, sqlcon);
                obj = sqlcom.ExecuteScalar();//执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                throw ex;
            }
            finally
            {
                sqlcon.Close();
                sqlcon.Dispose();
                sqlcom.Dispose();
            }
            return obj;
        }
        #endregion

        #region  通过Transact-SQL语句提交数据
        /// <summary>
        /// 通过Transact-SQL语句提交数据
        /// </summary>
        /// <param name="strSql">Transact-SQL语句</param>
        /// <returns>受影响的行数</returns>
        public int SqlComNonQuery(string strSql)
        {
            int intReturnValue;
            SqlCommand sqlcom = null;
            SqlConnection sqlcon = this.GetCon();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                sqlcom = new SqlCommand(strSql, sqlcon);
                intReturnValue = sqlcom.ExecuteNonQuery();//对连接执行 Transact-SQL 语句并返回受影响的行数。
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                throw ex;
            }
            finally
            {
                sqlcon.Close();//连接关闭，但不释放掉该对象所占的内存单元
                sqlcon.Dispose();
                sqlcom.Dispose();
            }
            return intReturnValue;
        }
        #endregion


        #region
        /// <summary>
        /// 多条Transact-SQL语句提交数据
        /// </summary>
        /// <param name="strSqls">使用List泛型封装多条SQL语句</param>
        /// <returns>bool值(提交是否成功)</returns>
        public bool SqlComNonQuerys(List<string> strSqls)
        {
            bool booIsSucceed;
            SqlCommand sqlcom = null;
            SqlTransaction sqlTran = null;
            SqlConnection sqlcon = this.GetCon();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                sqlTran = sqlcon.BeginTransaction();
                sqlcom = new SqlCommand();
                sqlcom.Connection = sqlcon;
                sqlcom.Transaction = sqlTran;
                foreach (string item in strSqls)
                {

                    sqlcom.CommandType = CommandType.Text;
                    sqlcom.CommandText = item;
                    sqlcom.ExecuteNonQuery();
                }

                sqlTran.Commit();
                booIsSucceed = true;  //表示提交数据库成功
            }
            catch (Exception ex)
            {
                sqlTran.Rollback();     //回滚
                booIsSucceed = false;  //表示提交数据库失败！
                MessageBox.Show(ex.Message, "提示");
                throw ex;
            }
            finally
            {
                sqlcon.Close();
                strSqls.Clear();
                sqlcon.Dispose();
                sqlcom.Dispose();
            }
            return booIsSucceed;
        }
        #endregion

        #region
        /// <summary>
        /// 通过Transact-SQL语句得到SqlDataReader实例
        /// </summary>
        /// <param name="strSql">Transact-SQL语句</param>
        /// <returns>SqlDataReader实例的引用</returns>
        public SqlDataReader GetDataReader(string strSql)
        {
            SqlDataReader sdr;
            SqlCommand sqlcom = null;
            SqlConnection sqlcon = this.GetCon();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                sqlcom = new SqlCommand(strSql, sqlcon);
                sdr = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                throw ex;
            }
            //sdr对象和m_Conn对象暂时不能关闭和释放掉，否则在调用时无法使用
            //待使用完毕sdr，再关闭sdr对象（同时会自动关闭关联的m_Conn对象）
            //m_Conn的关闭是指关闭连接通道，但连接对象依然存在
            //m_Conn的释放掉是指销毁连接对象
            return sdr;
        }
        #endregion

        #region
        /// <summary>
        /// 通过Transact-SQL语句得到DataSet实例
        /// </summary>
        /// <param name="strSql">Transact-SQL语句</param>
        /// <param name="strTable">相关的数据表</param>
        /// <returns>DataSet实例的引用</returns>
        public DataSet GetDataSet(string strSql, string strTable)
        {
            DataSet ds = null;
            SqlConnection sqlcon = this.GetCon();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(strSql, sqlcon);
                ds = new DataSet();     //dt = new DataTable(strTableName);
                sda.Fill(ds, strTable);
            }
            catch (Exception e)
            {
                throw e;
            }
            return ds;
        }
        #endregion


        #region 通过Transact-SQL语句，得到DataTable实例
        /// <summary>
        /// 通过Transact-SQL语句，得到DataTable实例
        /// </summary>
        /// <param name="strSqlCode">Transact-SQL语句</param>
        /// <param name="strTableName">数据表的名称</param>
        /// <returns>DataTable实例的引用</returns>
        public DataTable GetDataTable(string strSql)
        {
            DataTable dt = null;
            SqlDataAdapter sda = null;
            SqlConnection sqlcon = this.GetCon();
            try
            {
                sda = new SqlDataAdapter(strSql, sqlcon);
                dt = new DataTable();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                throw ex;
            }
            return dt; //dt.Rows.Count可能等于零
        }
        #endregion

    }
}

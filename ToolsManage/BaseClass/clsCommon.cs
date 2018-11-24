using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace ToolsManage.BaseClass
{
    /// <summary>
    /// 通用类
    /// </summary>
    public class clsCommon
    {
        private DataLogic datalogic = new DataLogic();

        #region  静态

        /// <summary>
        /// 计算时长
        /// </summary>
        /// <param name="message"></param>
        public static string CalculateTime(TimeSpan timeSpan)
        {
            string strRet = ""; ;
            if (timeSpan.TotalDays > 1)
            {
                double d = timeSpan.TotalDays;
                int iDuration = (int)d;
                iDuration++;
                strRet = iDuration.ToString() + "天";
            }
            else if (timeSpan.TotalHours > 1)
            {
                double d = timeSpan.TotalHours;
                int iDuration = (int)d;
                iDuration++;
                strRet = iDuration.ToString() + "小时";
            }
            else if (timeSpan.TotalMinutes > 1)
            {
                double d = timeSpan.TotalMinutes;
                int iDuration = (int)d;
                iDuration++;
                strRet = iDuration.ToString() + "分";
            }
            else if (timeSpan.TotalSeconds > 1)
            {
                double d = timeSpan.TotalSeconds;
                int iDuration = (int)d;
                iDuration++;
                strRet = iDuration.ToString() + "秒";
            }
            return strRet;
        }

        public static string FormatString(int intLength)
        {
            string strFormat = String.Empty;
            for (int i = 0; i < intLength; i++)
            {
                strFormat = strFormat + "0";
            }
            return strFormat;
        }

        // <summary>
        // 更新 事件记录 显示 主界面
        // </summary>
        //public static void NewEventShow(EventType eventType, string strPoint, string strContent, string strPeople, string strRemark)
        //{
        //    DataRow dr = MainControl.dtNewEvent.NewRow();
        //    dr["Type"] = eventType;
        //    dr["Point"] = strPoint;
        //    dr["Content"] = strContent;
        //    dr["People"] = strPeople;
        //    dr["Remark"] = strRemark;
        //    dr["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    MainControl.dtNewEvent.Rows.Add(dr);
        //}

        #endregion

        #region

        /// <summary>
        /// 获取 中控开门人 及 班组
        /// </summary>
        public void GetZkUserGroup(string strUserId, ref string strGroup, ref string strPeople)
        {
            string strSql = "select GroupName,UserName from tb_DoorUser where UserId='" + strUserId + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt.Rows.Count > 0)// DtDoorRun     GroupName UserName OnOffDoor ActionTime
            {
                strGroup = dt.Rows[0]["GroupName"].ToString();
                strPeople = dt.Rows[0]["UserName"].ToString();
            }
            else
            {
                strGroup = "";
                strPeople = "";
            }
        }

        /// <summary>
        /// 获取  微耕 开门人 及 班组
        /// </summary>
        public void GetWgUserGroup(string strCardNo, ref string strGroup, ref string strPeople)
        {
            string strSql = "select GroupName,UserName from tb_DoorUser where IcNo='" + strCardNo + "'";
            //string strSql = "select GroupName,UserName from tb_DoorUser where UserId='" + str + "'";
            DataTable dt = datalogic.GetDataTable(strSql);
            if (dt.Rows.Count > 0)// DtDoorRun     GroupName UserName OnOffDoor ActionTime
            {
                strGroup = dt.Rows[0]["GroupName"].ToString();
                strPeople = dt.Rows[0]["UserName"].ToString();
            }
            else
            {
                strGroup = "";
                strPeople = "";
            }
        }


        /// <summary>
        /// 环境事件记录
        /// </summary>
        public void NewEnvirEventRecord(EventType type, string area, DeviceRunModel reason, string content)
        {
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "insert into tb_EnviriEvent (Type,Area,Reason,EventContent,Time)values " +
                            "('" + type.ToString() + "','" + area + "','" + reason.ToString() + "'," +
                            "'" + content + "','" + strTime + "')";
            datalogic.SqlComNonQuery(strSql);
        }

        /// <summary>
        /// 环境告警记录
        /// </summary>
        public void NewEnvirAlarmRecord(AlarmsType type, string area, string strAddr, string content)
        {
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "insert into tb_EnviriEvent (Type,Area,Addr,EventContent,Time)values ('" + type.ToString() + "','" + area + "','" + strAddr + "'," +
                            "'" + content + "','" + strTime + "')";
            datalogic.SqlComNonQuery(strSql);
        }

        /// <summary>
        /// 环境 数据记录 记录
        /// </summary>
        public void NewEnvirDataRecord(string area,string addr,string temp,string humi)
        {
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "insert into tb_EnviriData (Area,Addr,Temp,Humi,date)values ('" + area + "','" + addr + "'," +
                                    "'" + temp + "','" + humi + "','" + strTime + "')";
            datalogic.SqlComNonQuery(strSql);
        }

        /// <summary>
        /// 工具柜 开关门 时间
        /// </summary>
        public void BoxOnOffRecord(string strArea, string strName, DoorsState doorState, string strOpenType, string strGroupName,string strOpenDoorPeople)
        {
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string str = "insert into tb_RecordBoxDoor (BoxArea,BoxName,OpenOrClose,Time,OpenType,GroupName,UserName) values ('" + strArea + "'," +
                    "'" + strName + "','" + doorState.ToString() + "','" + strTime + "','" + strOpenType + "','" + strGroupName + "','" + strOpenDoorPeople + "')";
            datalogic.SqlComNonQuery(str);
        }

        /// <summary>
        /// 门禁进出记录
        /// </summary>
        public void NewDoorInOut(DoorsState inOrOut,string strOpenType, string strPoint, string strGroupName, string strOpenDoorPeople,string strSpan)
        {
            string strSql = "";
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (inOrOut == DoorsState.开门)
            {
                strSql = "insert into tb_RecordDoorInOut (OpenType,Point,GroupName,UserName,OpenTime,CloseTime,DurationTime)" +
                  "values ('" + strOpenType + "','" + strPoint + "','" + strGroupName + "','" + strOpenDoorPeople + "','" + strTime + "','','')";
            }
            else if (inOrOut == DoorsState.关门 )
            {
                strSql = "update tb_RecordDoorInOut set CloseTime='" + strTime + "',DurationTime='" + strSpan + "' where " +
                    "CloseTime='' and DurationTime='' and Point='" + strPoint + "'";
                datalogic.SqlComNonQuery(strSql);
            }
            if (!string.IsNullOrEmpty(strSql))
                datalogic.SqlComNonQuery(strSql);
        }

        /// <summary>
        /// 工具借还记录
        /// </summary>
        public void NewToolBorrowRet(ToolsState state, string strTime, string strEpc, string strToolType, string strToolName, string strToolID,string strUser)
        {
            if (state == ToolsState.借出)
            {
                string strSql = strSql = "update tb_Tools set IsInStore='" + state.ToString() + "',BorrowReturnTime='" + strTime + "' where RFIDCoding='" + strEpc + "' ";
                datalogic.SqlComNonQuery(strSql);

                //tb_RecordBorrow  ID ToolType ToolName ToolID RFIDCoding PeopleBorrow BorrowTime PeopleReturn ReturnTime BorrowDuration     
                strSql = "insert into tb_RecordBorrow (ToolType,ToolName,ToolID,RFIDCoding,PeopleBorrow,BorrowTime,PeopleReturn,ReturnTime,BorrowDuration)" +
                "values ('" + strToolType + "','" + strToolName + "','" + strToolID + "','" + strEpc + "','"+strUser+"','" + strTime + "'," +
                 "'','','')";
                datalogic.SqlComNonQuery(strSql);
            }
            else if (state == ToolsState.在库)
            {
                string strSql = strSql = "update tb_Tools set IsInStore='" + state + "',BorrowReturnTime='" + strTime + "' where RFIDCoding='" + strEpc + "' ";
                datalogic.SqlComNonQuery(strSql);
                strSql= "update tb_RecordBorrow set PeopleReturn='"+strUser+"',ReturnTime='" + strTime + "'," +
                 "BorrowDuration='' where RFIDCoding='" + strEpc + "' and ReturnTime='' ";
                datalogic.SqlComNonQuery(strSql);
            }


        }

        /// <summary>
        /// 异常记录
        /// </summary>
        public void NewErrRecord(string strType, string strPoint, string strContent, string strRemark)
        {
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "insert into tb_RecordErr (Type,Point,EventContent,Remark,Time)" +
                            "values ('" + strType + "','" + strPoint + "','" + strContent + "','" + strRemark + "','" + strTime + "')";
            datalogic.SqlComNonQuery(strSql);

            //if (strContent == ErrorContent.RFID读写器异常系统重启.ToString())
            //{ 
            
            //}
        }

        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WG3000_COMM.Core;

using System.Collections;
using System.Windows.Forms;
using System.Threading;
using System.Data;



namespace ToolsManage.BaseClass
{
//    class wgControl
//    {
//        DataLogic datalogic = new DataLogic();

//        public string strIp = "";
//        public int iSn = 0;

//        public wgWatchingService watching; //实时监控服务
//        private wgMjController wgMjControllerWatching = null; //2011-11-15_23:50:42 要监控的控制器

//        public static int receivedPktCount = 0;
//        public static Queue QueRecText = new Queue();

//        public static string strCardNo = "";

//        //选定要监控的控制表
//        Dictionary<int, wgMjController> selectedControllers = new Dictionary<int, wgMjController>();

//        public wgControl(string ip,int sn)
//        {
//            strIp = ip;
//            iSn = sn;
//        }

//        #region  子程序


//        public DoorsState DoorOpenOrClose(int doorNo)
//        {
//            DoorsState state = DoorsState.关门;
//            if (wgMjControllerWatching != null)
//            {
//                if (wgMjControllerWatching.GetMjControllerRunInformationIP() > 0) //取控制器信息
//                {
//                    bool bl = wgMjControllerWatching.RunInfo.IsOpen(doorNo);
//                    if (bl)
//                        state = DoorsState.关门;
//                    else
//                        state = DoorsState.开门;
//                }
//            }
//            return state;
//        }

//        public bool OpenDoor()
//        {
//            if (wgMjControllerWatching == null)
//            {
//                wgMjControllerWatching = new wgMjController();
//                wgMjControllerWatching.ControllerSN = iSn;
//                wgMjControllerWatching.PORT = 60000;
//                wgMjControllerWatching.IP = strIp;
//            }
//            bool blRet = false;
//            if (wgMjControllerWatching.RemoteOpenDoorIP(1) > 0)
//            {
//                blRet = true;
//            }
//            else
//            {
//                blRet = false;
//            }
//            return blRet;
//        }

//        public void StartWatch()
//        {
//            if (wgMjControllerWatching == null)
//            {
//                wgMjControllerWatching = new wgMjController();
//                wgMjControllerWatching.ControllerSN = iSn;
//                wgMjControllerWatching.PORT = 60000;
//                wgMjControllerWatching.IP = strIp;
//            }
//            if (watching == null)
//            {
//                watching = new wgWatchingService();  //加载监视服务
//                watching.EventHandler += new OnEventHandler(evtNewInfoCallBack); //事件处理
//            }
//            if (selectedControllers.Count == 0)
//            {
//                selectedControllers.Add(wgMjControllerWatching.ControllerSN, wgMjControllerWatching);
//            }
//            if (selectedControllers.Count > 0)
//            {
//                System.Diagnostics.Debug.WriteLine("selectedControllers.Count=" + selectedControllers.Count.ToString());
//                watching.WatchingController = selectedControllers;
//            }
//        }

//        public string DoorTimeInfo(string runInfo, string start, string end)
//        {
//            int iStart = runInfo.IndexOf(start);
//            int iEnd = runInfo.IndexOf(end);
//            int iLenth = iEnd - iStart - start.Length;
//            string strRet = runInfo.Substring(start.Length + iStart, iLenth);
//            return strRet;
//        }

//        public string DoorTimeDate(string runInfo, string start)
//        {
//            int iStart = runInfo.IndexOf(start);
//            int iLenth = runInfo.Length - iStart - start.Length - 2;
//            string strRet = runInfo.Substring(start.Length + iStart, iLenth);
//            return strRet;
//        }

//        private void evtNewInfoCallBack(string text)
//        {
//            System.Diagnostics.Debug.WriteLine("Got text through callback! {0}", text);
//            receivedPktCount++;
//            lock (QueRecText.SyncRoot)
//            {
//                QueRecText.Enqueue(text);  //加入到文件中
//            }
//        }

//        public void updateControllerStatus()
//        {
//            if (watching != null)
//            {
//                wgMjControllerRunInformation conRunInfo = null;
//                int commStatus;
//                commStatus = watching.CheckControllerCommStatus(iSn, ref conRunInfo);

//                if (commStatus == -1)
//                {
//                    ////实时监控 超过3秒后 仍未通信上, 则提示未连接
//                    if (frmMain.blDebug)
//                        MessageUtil.ShowTips("门禁控制器通信失败");
//                }
//                else if (commStatus == 1)
//                {
//                    //校时
//                    DateTime time = conRunInfo.dtNow;
//                    TimeSpan ts = DateTime.Now - time;//记录历史温湿度数据间隔
//                    if (ts.TotalSeconds >= 59 || ts.TotalSeconds <= -59)
//                    {
//                        if (wgMjControllerWatching.AdjustTimeIP(DateTime.Now) > 0)
//                        {
//                            if (frmMain.blDebug)
//                                MessageUtil.ShowTips("门禁控制器校时成功");
//                        }
//                        else
//                        {
//                            if (frmMain.blDebug)
//                                MessageUtil.ShowTips("门禁控制器校时失败");
//                        }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// 正在处理的门禁运行信息
//        /// </summary>
//        public static int dealingTxt = 0;
//        public static int infoRowsCount = 0;
//        public void txtInfoHaveNewInfoEntry()
//        {
//            if (dealingTxt > 0)
//            {
//                return;
//            }
//            if (watching.WatchingController == null) //2010-8-1 08:27:15 没有选中监控的就退出
//            {
//                return;
//            }
//            System.Threading.Interlocked.Exchange(ref dealingTxt, 1);
//            int dealt = 0;
//            object obj;
//            long startTicks = DateTime.Now.Ticks; // Date.Now.Ticks  'us
//            long updateSpan = 2000 * 1000 * 10;
//            long endTicks = startTicks + updateSpan; // '100毫微秒的倍数  '一个Ticks＝10亿分之一秒，一毫秒＝10000Ticks
//            while (wgControl.QueRecText.Count > 0)
//            {
//                lock (wgControl.QueRecText.SyncRoot)
//                {
//                    obj = wgControl.QueRecText.Dequeue();//取出
//                }
//                txtInfoUpdateEntry(obj);
//                infoRowsCount++;
//                dealt++;
//                if (DateTime.Now.Ticks > endTicks)
//                {
//                    endTicks = DateTime.Now.Ticks + updateSpan;
//                    if (watching.WatchingController == null)
//                    {
//                        break;
//                    }
//                }
//            }
//            Application.DoEvents();//显示
//            System.Threading.Interlocked.Exchange(ref dealingTxt, 0);
//        }

//        private void txtInfoUpdateEntry(object info)
//        {
//            wgMjControllerSwipeRecord mjrec = new wgMjControllerSwipeRecord(info as string);
//            if (mjrec.ControllerSN > 0)
//            {
//                //如果不处于监控的控制器 则不作数据处理
//                try
//                {
//                    if (!watching.WatchingController.ContainsKey((int)mjrec.ControllerSN))
//                    {
//                        return; //不属于监控的控制器发出的信息 则返回
//                    }
//                }
//                catch (Exception)
//                {
//                    return;
//                }
//                string str = mjrec.ToDisplaySimpleInfo(true);

//                string strStart = "Swipe Status: \t";
//                string strEnd = "\r\nRead Date:";
//                string strStatus = DoorTimeInfo(str, strStart, strEnd);

//                if (strStatus == OpenDoorType.刷卡开门.ToString())
//                {
//                    strStart = "CardID: \t";
//                    strEnd = "\r\nDoorNO: \t";
//                    strCardNo = DoorTimeInfo(str, strStart, strEnd);
//                    if (strCardNo.Length == 6)
//                    {
//                        strCardNo = "00" + strCardNo;
//                    }
//                    if (strCardNo.Length == 7)
//                    {
//                        strCardNo = "0" + strCardNo;
//                    }
//                }
//                else
//                {
//                    strCardNo = "";
//                }
  


//                //strStart = "DoorNO: \t";
//                //strEnd = "\r\n \t[";
//                //string strDoorNo =DoorTimeInfo(str, strStart, strEnd);

//                //strStart = "\r\n \t[";
//                //strEnd = "]\r\nReaderNO:";
//                //string InOut = DoorTimeInfo(str, strStart, strEnd);

//                //strStart = "ReaderNO: \t";
//                //strEnd = "\r\nSwipe Status:";
//                //string strReadNo = DoorTimeInfo(str, strStart, strEnd);

//                //strStart = "Read Date: \t";
//                //string strDate = DoorTimeDate(str, strStart);

//            }
//        }

//        #endregion









//        //public static int dealingTxt = 0;
//        //public static int infoRowsCount = 0;
//        //private void txtInfoHaveNewInfoEntry()
//        //{
//        //    if (dealingTxt > 0)
//        //    {
//        //        return;
//        //    }
//        //    if (watching.WatchingController == null) //2010-8-1 08:27:15 没有选中监控的就退出
//        //    {
//        //        return;
//        //    }
//        //    System.Threading.Interlocked.Exchange(ref dealingTxt, 1);
//        //    int dealt = 0;
//        //    object obj;
//        //    long startTicks = DateTime.Now.Ticks; // Date.Now.Ticks  'us
//        //    long updateSpan = 2000 * 1000 * 10;
//        //    long endTicks = startTicks + updateSpan; // '100毫微秒的倍数  '一个Ticks＝10亿分之一秒，一毫秒＝10000Ticks
//        //    while (QueRecText.Count > 0)
//        //    {
//        //        lock (QueRecText.SyncRoot)
//        //        {
//        //            obj = QueRecText.Dequeue();//取出
//        //        }
//        //        txtInfoUpdateEntry(obj);
//        //        infoRowsCount++;
//        //        dealt++;
//        //        if (DateTime.Now.Ticks > endTicks)
//        //        {
//        //            endTicks = DateTime.Now.Ticks + updateSpan;
//        //            if (watching.WatchingController == null)
//        //            {
//        //                break;
//        //            }
//        //        }
//        //    }
//        //    Application.DoEvents();//显示
//        //    System.Threading.Interlocked.Exchange(ref dealingTxt, 0);
//        //}

//        //private void txtInfoUpdateEntry(object info)
//        //{
//        //    wgMjControllerSwipeRecord mjrec = new wgMjControllerSwipeRecord(info as string);
//        //    if (mjrec.ControllerSN > 0)
//        //    {
//        //        //如果不处于监控的控制器 则不作数据处理
//        //        try
//        //        {
//        //            if (!watching.WatchingController.ContainsKey((int)mjrec.ControllerSN))
//        //            {
//        //                return; //不属于监控的控制器发出的信息 则返回
//        //            }
//        //        }
//        //        catch (Exception)
//        //        {
//        //            return;
//        //        }
//        //        string str = mjrec.ToDisplaySimpleInfo(true);

//        //        string strStart = "CardID: \t";
//        //        string strEnd = "\r\nDoorNO: \t";
//        //        string strId = DoorTimeInfo(str, strStart, strEnd);

//        //        strStart = "DoorNO: \t";
//        //        strEnd = "\r\n \t[";
//        //        string strDoorNo = DoorTimeInfo(str, strStart, strEnd);

//        //        strStart = "\r\n \t[";
//        //        strEnd = "]\r\nReaderNO:";
//        //        string InOut = DoorTimeInfo(str, strStart, strEnd);

//        //        strStart = "ReaderNO: \t";
//        //        strEnd = "\r\nSwipe Status:";
//        //        string strReadNo = DoorTimeInfo(str, strStart, strEnd);

//        //        strStart = "Swipe Status: \t";
//        //        strEnd = "\r\nRead Date:";
//        //        string strStatus = DoorTimeInfo(str, strStart, strEnd);

//        //        strStart = "Read Date: \t";
//        //        //strEnd = "";
//        //        string strDate = DoorTimeDate(str, strStart);

//        //        //this.textBox1.AppendText(str);
//        //        //this.textBox1.AppendText(strId + "\r\n");
//        //        //this.textBox1.AppendText(strDoorNo + "\r\n");
//        //        //this.textBox1.AppendText(InOut + "\r\n");
//        //        //this.textBox1.AppendText(strReadNo + "\r\n");
//        //        //this.textBox1.AppendText(strStatus + "\r\n");
//        //        //this.textBox1.AppendText(strDate + "\r\n");

//        //        //this.textBox1.AppendText(mjrec.ToDisplaySimpleInfo(true));
//        //    }
//        //}





//        //public void SetConfig(int sn, string ip, int port)
//        //{
//        //    wgMjController.ControllerSN = sn;
//        //    wgMjController.IP = ip;
//        //    wgMjController.PORT = port;
//        //    iSn = sn;
//        //}

//        #region

//        /*
         
//         */

//        /*
         
// */

//        /*
//                 private void txtInfoHaveNewInfoEntry()
//        {
//            if (dealingTxt > 0)
//            {
//                return;
//            }
//            if (watching.WatchingController == null) //2010-8-1 08:27:15 没有选中监控的就退出
//            {
//                return;
//            }
//            System.Threading.Interlocked.Exchange(ref dealingTxt, 1);
//            int dealt = 0;
//            object obj;
//            long startTicks = DateTime.Now.Ticks; // Date.Now.Ticks  'us
//            long updateSpan = 2000 * 1000 * 10;
//            long endTicks = startTicks + updateSpan; // '100毫微秒的倍数  '一个Ticks＝10亿分之一秒，一毫秒＝10000Ticks
//            while (QueRecText.Count > 0)
//            {
//                lock (QueRecText.SyncRoot)
//                {
//                    obj = QueRecText.Dequeue();//取出
//                }
//                txtInfoUpdateEntry(obj);
//                infoRowsCount++;
//                dealt++;
//                if (DateTime.Now.Ticks > endTicks)
//                {
//                    endTicks = DateTime.Now.Ticks + updateSpan;
//                    if (watching.WatchingController == null)
//                    {
//                        break;
//                    }
//                }
//            }
//            Application.DoEvents();//显示
//            System.Threading.Interlocked.Exchange(ref dealingTxt, 0);
//        }
//*/

//        /*
//               private void txtInfoUpdateEntry(object info)
//        {
//            wgMjControllerSwipeRecord mjrec = new wgMjControllerSwipeRecord(info as string);
//            if (mjrec.ControllerSN > 0)
//            {
//                //如果不处于监控的控制器 则不作数据处理
//                try
//                {
//                    if (!watching.WatchingController.ContainsKey((int)mjrec.ControllerSN))
//                    {
//                        return; //不属于监控的控制器发出的信息 则返回
//                    }
//                }
//                catch (Exception)
//                {
//                    return;
//                }
//                string str = mjrec.ToDisplaySimpleInfo(true);

//                string strStart = "CardID: \t";
//                string strEnd = "\r\nDoorNO: \t";
//                string strId = DoorRunInfo(str, strStart, strEnd);

//                strStart = "DoorNO: \t";
//                strEnd = "\r\n \t[";
//                string strDoorNo = DoorRunInfo(str, strStart, strEnd);

//                strStart = "\r\n \t[";
//                strEnd = "]\r\nReaderNO:";
//                string InOut = DoorRunInfo(str, strStart, strEnd);

//                strStart = "ReaderNO: \t";
//                strEnd = "\r\nSwipe Status:";
//                string strReadNo = DoorRunInfo(str, strStart, strEnd);

//                strStart = "Swipe Status: \t";
//                strEnd = "\r\nRead Date:";
//                string strStatus = DoorRunInfo(str, strStart, strEnd);

//                strStart = "Read Date: \t";
//                //strEnd = "";
//                string strDate = DoorRunInfoDate(str, strStart);

//                //this.textBox1.AppendText(str);
//                //this.textBox1.AppendText(strId + "\r\n");
//                //this.textBox1.AppendText(strDoorNo + "\r\n");
//                //this.textBox1.AppendText(InOut + "\r\n");
//                //this.textBox1.AppendText(strReadNo + "\r\n");
//                //this.textBox1.AppendText(strStatus + "\r\n");
//                //this.textBox1.AppendText(strDate + "\r\n");

//                //this.textBox1.AppendText(mjrec.ToDisplaySimpleInfo(true));
//            }
//        }
//*/

//        /*
         
//                 void updateControllerStatus()
//        {
//            if (watching != null)
//            {
//                wgMjControllerRunInformation conRunInfo = null;
//                int commStatus;
//                commStatus = watching.CheckControllerCommStatus(iSn, ref conRunInfo);
//                if (commStatus == -1)
//                {
//                    ////实时监控 超过3秒后 仍未通信上, 则提示未连接
//                    if (frmMain.blDebug)
//                        MessageUtil.ShowTips("门禁控制器通信失败");
//                }
//                else if (commStatus == 1)
//                {
//                    //校时
//                    DateTime time = conRunInfo.dtNow;
//                    TimeSpan ts = DateTime.Now - time;//记录历史温湿度数据间隔
//                    if (ts.TotalSeconds >= 59 || ts.TotalSeconds <= -59)
//                    {
//                        if (wgMjController1.AdjustTimeIP(DateTime.Now) > 0)
//                        {
//                            if (frmMain.blDebug)
//                                MessageUtil.ShowTips("门禁控制器校时成功");
//                        }
//                        else
//                        {
//                            if (frmMain.blDebug)
//                                MessageUtil.ShowTips("门禁控制器校时失败");
//                        }
//                    }
//                }
//            }
//        }
         
//         */

//        #endregion







//    }
}

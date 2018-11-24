using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WG3000_COMM.Core;

using System.Collections;
using System.Windows.Forms;

namespace ToolsManage.BaseClass
{
    //class clsWgControl
    //{
    //    public string strIp = "";
    //    public int iSn = 0;

    //    private wgMjController wgController = null; //2011-11-15_23:50:42 要监控的控制器

    //    public wgWatchingService watching; //实时监控服务
    //    Dictionary<int, wgMjController> selectedControllers = new Dictionary<int, wgMjController>();

    //    public static Queue QueRecText = new Queue();

    //    public static int receivedPktCount = 0;
    //    public static int dealingTxt = 0;
    //    public static int infoRowsCount = 0;
    //    public static string strCardNo = "";


    //    public clsWgControl(string ip, int sn)
    //    {
    //        strIp = ip;
    //        iSn = sn;
    //    }

    //    #region  子程序

    //    /// <summary>
    //    /// 获取门的状态 门号
    //    /// </summary>
    //    /// <param name="doorNo"></param>
    //    /// <returns></returns>
    //    public DoorsState GetDoorState(int doorNo)
    //    {
    //        DoorsState state = DoorsState.关门;
    //        if (wgController != null)
    //        {
    //            if (wgController.GetMjControllerRunInformationIP() > 0) //取控制器信息
    //            {
    //                bool bl = wgController.RunInfo.IsOpen(doorNo);
    //                if (bl)
    //                    state = DoorsState.关门;
    //                else
    //                    state = DoorsState.开门;
    //            }
    //        }
    //        return state;
    //    }

    //    /// <summary>
    //    /// 开门 门号
    //    /// </summary>
    //    /// <param name="iDoorNo"></param>
    //    /// <returns></returns>
    //    public bool OpenDoor(int iDoorNo)
    //    {
    //        if (wgController == null)
    //        {
    //            wgController = new wgMjController();
    //            wgController.ControllerSN = iSn;
    //            wgController.PORT = 60000;
    //            wgController.IP = strIp;
    //        }
    //        bool blRet = false;
    //        if (wgController.RemoteOpenDoorIP(1) > 0)
    //        {
    //            blRet = true;
    //        }
    //        else
    //        {
    //            blRet = false;
    //        }
    //        return blRet;
    //    }

    //    public void StartWatch()
    //    {
    //        if (wgController == null)
    //        {
    //            wgController = new wgMjController();
    //            wgController.ControllerSN = iSn;
    //            wgController.PORT = 60000;
    //            wgController.IP = strIp;
    //        }

    //        if (watching == null)
    //        {
    //            watching = new wgWatchingService();  //加载监视服务
    //            watching.EventHandler += new OnEventHandler(evtNewInfoCallBack); //事件处理
    //        }
    //        if (selectedControllers.Count == 0)
    //        {
    //            selectedControllers.Add(wgController.ControllerSN, wgController);
    //        }
    //        if (selectedControllers.Count > 0)
    //        {
    //            System.Diagnostics.Debug.WriteLine("selectedControllers.Count=" + selectedControllers.Count.ToString());
    //            watching.WatchingController = selectedControllers;
    //        }

    //    }

    //    private void evtNewInfoCallBack(string text)
    //    {
    //        System.Diagnostics.Debug.WriteLine("Got text through callback! {0}", text);
    //        receivedPktCount++;
    //        lock (QueRecText.SyncRoot)
    //        {
    //            QueRecText.Enqueue(text);  //加入到文件中
    //        }
    //    }

    //    /// <summary>
    //    /// 更新 门禁控制器状态
    //    /// </summary>
    //    public void updateControllerStatus()
    //    {
    //        if (watching != null)
    //        {
    //            wgMjControllerRunInformation conRunInfo = null;
    //            int commStatus;
    //            commStatus = watching.CheckControllerCommStatus(iSn, ref conRunInfo);

    //            if (commStatus == -1)
    //            {
    //                ////实时监控 超过3秒后 仍未通信上, 则提示未连接
    //                if (frmMain.blDebug)
    //                    MessageUtil.ShowTips("门禁控制器通信失败");
    //            }
    //            else if (commStatus == 1)
    //            {
    //                //校时
    //                DateTime time = conRunInfo.dtNow;
    //                TimeSpan ts = DateTime.Now - time;//记录历史温湿度数据间隔
    //                if (ts.TotalSeconds >= 59 || ts.TotalSeconds <= -59)
    //                {
    //                    wgController.AdjustTimeIP(DateTime.Now);
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 有新的运行信息
    //    /// </summary>
    //    public void txtInfoHaveNewInfoEntry()
    //    {
    //        if (dealingTxt > 0)
    //        {
    //            return;
    //        }
    //        if (watching.WatchingController == null) //2010-8-1 08:27:15 没有选中监控的就退出
    //        {
    //            return;
    //        }
    //        System.Threading.Interlocked.Exchange(ref dealingTxt, 1);
    //        int dealt = 0;
    //        object obj;
    //        long startTicks = DateTime.Now.Ticks; // Date.Now.Ticks  'us
    //        long updateSpan = 2000 * 1000 * 10;
    //        long endTicks = startTicks + updateSpan; // '100毫微秒的倍数  '一个Ticks＝10亿分之一秒，一毫秒＝10000Ticks
    //        while (QueRecText.Count > 0)
    //        {
    //            lock (QueRecText.SyncRoot)
    //            {
    //                obj = QueRecText.Dequeue();//取出
    //            }
    //            txtInfoUpdateEntry(obj);
    //            infoRowsCount++;
    //            dealt++;
    //            if (DateTime.Now.Ticks > endTicks)
    //            {
    //                endTicks = DateTime.Now.Ticks + updateSpan;
    //                if (watching.WatchingController == null)
    //                {
    //                    break;
    //                }
    //            }
    //        }
    //        Application.DoEvents();//显示
    //        System.Threading.Interlocked.Exchange(ref dealingTxt, 0);
    //    }

    //    private void txtInfoUpdateEntry(object info)
    //    {
    //        wgMjControllerSwipeRecord mjrec = new wgMjControllerSwipeRecord(info as string);
    //        if (mjrec.ControllerSN > 0)
    //        {
    //            //如果不处于监控的控制器 则不作数据处理
    //            try
    //            {
    //                if (!watching.WatchingController.ContainsKey((int)mjrec.ControllerSN))
    //                {
    //                    return; //不属于监控的控制器发出的信息 则返回
    //                }
    //            }
    //            catch (Exception)
    //            {
    //                return;
    //            }
    //            string str = mjrec.ToDisplaySimpleInfo(true);

    //            string strStart = "Swipe Status: \t";
    //            string strEnd = "\r\nRead Date:";
    //            string strStatus = DoorTimeInfo(str, strStart, strEnd);

    //            if (strStatus == OpenDoorType.刷卡开门.ToString())
    //            {
    //                strStart = "CardID: \t";
    //                strEnd = "\r\nDoorNO: \t";
    //                strCardNo = DoorTimeInfo(str, strStart, strEnd);
    //                if (strCardNo.Length == 6)
    //                {
    //                    strCardNo = "00" + strCardNo;
    //                }
    //                if (strCardNo.Length == 7)
    //                {
    //                    strCardNo = "0" + strCardNo;
    //                }
    //            }
    //            else
    //            {
    //                strCardNo = "";
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 提取运行信息
    //    /// </summary>
    //    /// <param name="runInfo"></param>
    //    /// <param name="start"></param>
    //    /// <param name="end"></param>
    //    /// <returns></returns>
    //    public string DoorTimeInfo(string runInfo, string start, string end)
    //    {
    //        int iStart = runInfo.IndexOf(start);
    //        int iEnd = runInfo.IndexOf(end);
    //        int iLenth = iEnd - iStart - start.Length;
    //        string strRet = runInfo.Substring(start.Length + iStart, iLenth);
    //        return strRet;
    //    }

    //    #endregion


    //}
}

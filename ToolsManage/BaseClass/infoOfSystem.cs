using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsManage.BaseClass
{
    /// <summary>
    /// 系统设置 信息
    /// </summary>
    class infoOfSystem
    {
        /// <summary>
        /// 本机地址
        /// </summary>
        public static string strServerAddr = "1";
        /// <summary>
        /// 借还间隔 比如20S
        /// </summary>
        public static int iBorrRetSpan = 30;
        /// <summary>
        /// 外借超时 时间
        /// </summary>
        public static int iOverDayBorr = 30;

        /// <summary>
        /// 外借超时提醒 是否启用
        /// </summary>
        public static DeviceUsing usingBorrOver = DeviceUsing.未启用;
        /// <summary>
        /// 是否显示异常信息
        /// </summary>
        public static DeviceUsing usingOfErr = DeviceUsing.未启用;

        /// <summary>
        /// 是否启用超高频RFID读写器
        /// </summary>
        public static DeviceUsing usingRfid = DeviceUsing.未启用;


    }
}

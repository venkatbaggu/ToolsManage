using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace ToolsManage.VideoManage
{
    public class PageValidate
    {
        private static Regex RegNumber = new Regex("^[0-9]+$");
        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            if (inputData != "")
            {
                Match m = RegNumber.Match(inputData);
                if (m.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return true;
        }
    }
}

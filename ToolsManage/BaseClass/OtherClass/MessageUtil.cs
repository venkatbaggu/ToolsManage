﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace ToolsManage.BaseClass
{
    /// <summary>
    /// MessageUtil 的摘要说明。
    /// </summary>
    public class MessageUtil
    {
        /// <summary>
        /// 显示一般的提示信息
        /// </summary>
        /// <param name="message">提示信息</param>
        public static DialogResult ShowTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="message">警告信息</param>
        public static DialogResult ShowWarning(string message)
        {
            return MessageBox.Show(message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowError(string message)
        {
            return MessageBox.Show(message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示询问用户信息，并显示错误标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            return MessageBox.Show(message, "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示询问用户信息，并显示警告标志
        /// </summary>
        /// <param name="message">警告信息</param>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return MessageBox.Show(message, "警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示一个YesNo选择对话框
        /// </summary>
        /// <param name="prompt">对话框的选择内容提示信息</param>
        /// <returns>如果选择Yes则返回true，否则返回false</returns>
        public static bool ConfirmYesNo(string prompt)
        {
            return MessageBox.Show(prompt, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// 显示一个YesNoCancel选择对话框
        /// </summary>
        /// <param name="prompt">对话框的选择内容提示信息</param>
        /// <returns>返回选择结果的的DialogResult值</returns>
        public static DialogResult ConfirmYesNoCancel(string prompt)
        {
            return MessageBox.Show(prompt, "确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// 询问一个输入字符串
        /// </summary>
        /// <param name="prompt">提示信息</param>
        /// <returns>询问到的字符串</returns>
        //public static string QueryInputStr(string prompt)
        //{
        //    QueryInputDialog dlg = new QueryInputDialog();
        //    dlg.Text = prompt;
        //    dlg.lblPrompt.Text = prompt.EndsWith(":") || prompt.EndsWith("：") ? prompt : prompt + ":";
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        return dlg.tbValue.Text;
        //    }
        //    return "";
        //}

        /// <summary>
        /// 询问一个输入字符串
        /// </summary>
        /// <param name="prompt">提示信息</param>
        /// <param name="initValue">初始值</param>
        /// <returns>询问到的字符串</returns>
        //public static string QueryInputStr(string prompt, string initValue)
        //{
        //    QueryInputDialog dlg = new QueryInputDialog();
        //    dlg.Text = prompt;
        //    dlg.lblPrompt.Text = prompt.EndsWith(":") || prompt.EndsWith("：") ? prompt : prompt + ":";
        //    dlg.tbValue.Text = initValue;
        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        return dlg.tbValue.Text;
        //    }
        //    return initValue;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Common
{
    public class MsgBox
    {
        public static void ShowInfo(string strContent)
        {
            //frmMsgBox frm = new frmMsgBox(MsgType.INFO, strContent);
            //frm.ShowDialog();
            MessageBox.Show(strContent, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowWarn(string strContent)
        {
            //frmMsgBox frm = new frmMsgBox(MsgType.WARN,strContent);
            //frm.ShowDialog();
            MessageBox.Show(strContent, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        public static void ShowError(string strContent)
        {
            //frmMsgBox frm = new frmMsgBox(MsgType.ERROR, strContent);
            //frm.ShowDialog();
            MessageBox.Show(strContent, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

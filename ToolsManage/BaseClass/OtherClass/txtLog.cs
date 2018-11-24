using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace ToolsManage.BaseClass
{
    public class txtLog
    {

        public static void TxtWriteStr(string strFileName, string strType, string strContent, bool blNewLine)
        {
            if (frmMain.blDebug)
            {
                string strFile = IsTodayFile();
                strFile += strFileName;
                string str = strType;
                str += ":  ";
                str += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str += "  ";
                str += strContent;
                str += "\r\n";
                if (blNewLine)
                    str += "\r\n";
                WriteFile(strFile, true, str);
            }
        }

        public static void TxtWriteByte(string strFileName, string strType, byte[] bContent, bool blNewLine)
        {
            if (frmMain.blDebug)
            {
                string strContent = SerialPortUtil.ByteToStrHex(bContent);
                string strFile = IsTodayFile();
                strFile += strFileName;
                string str = strType;
                str += ":  ";
                str += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                str += "  ";
                str += strContent;
                str += "\r\n";
                if (blNewLine)
                    str += "\r\n";
                WriteFile(strFile, true, str);
            }
        }


        /// <summary>
        /// 写txt文件  文件路径 true 内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isAppend"></param>
        /// <param name="fileContent"></param>
        public static void WriteFile(string filePath, bool isAppend, string fileContent)
        {
            StreamWriter TxtWriter = new StreamWriter(filePath, isAppend, System.Text.Encoding.Default);
            TxtWriter.Write(fileContent);
            TxtWriter.Close();
        }

        private void TxtWrite(string strToWrite, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.WriteLine(strToWrite);
            sw.Close();
        }

        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 年月日这个文件夹是否存在 
        /// </summary>
        /// <returns></returns>
        public static string IsTodayFile()
        {
            string strFile = DateTime.Now.ToString("yyyy-MM-dd");
            string filePath = @"D:\ToolsManageTxt\";//@"D:\HikVideo\";
            filePath = filePath + strFile + "\\";
            if (!txtLog.IsExistFile(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return filePath;
        }
    }

}

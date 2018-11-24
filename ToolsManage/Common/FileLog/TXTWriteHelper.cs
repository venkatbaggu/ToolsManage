using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common.FileLog

{
    public struct RecSendInfo
    {
        public string SimNo;
        public DateTime time;
        public byte[] data;
        public byte mark;
    }
    public class TXTWriteHelper
    {
        public static System.Timers.Timer timeDataDeal = new System.Timers.Timer(10000);
        static TXTWriteHelper()
        {
            timeDataDeal.Elapsed += new System.Timers.ElapsedEventHandler(timeRecSendDataDeal_Elapsed);
            timeDataDeal.Enabled = true;
            timeDataDeal.AutoReset = true;
        }

        static void timeRecSendDataDeal_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (lockWriteTxt)
            {
                if (listRecSendInfo != null && listRecSendInfo.Count > 0)
                {
                    if (timeDataDeal.Interval != 5)
                        timeDataDeal.Interval = 5;
                    RecSendInfo recsend = listRecSendInfo[0];
                    WriteTxt(recsend.SimNo, recsend.data, recsend.mark, recsend.time);
                    listRecSendInfo.RemoveAt(0);
                }
                else
                {
                    if (timeDataDeal.Interval != 10000)
                        timeDataDeal.Interval = 10000;
                }
            }
        }
        private static string BytetoHex(byte[] _data)
        {
            string str = "";
            for (int i = 0; i < _data.Length; i++)
            {
                str += _data[i].ToString("X2") + " ";
            }
            return str;
        }
        static object lockAdd = new object();
        static List<RecSendInfo> listRecSendInfo = new List<RecSendInfo>();
        public static void AddList(string simno,byte[] _data,byte mark)
        {
            if (timeDataDeal.Interval != 5)
                timeDataDeal.Interval = 5;
            lock (listRecSendInfo)
            {
                RecSendInfo recsend = new RecSendInfo();
                recsend.SimNo = simno;
                recsend.time = DateTime.Now;
                recsend.data = _data;
                recsend.mark = mark;
                listRecSendInfo.Add(recsend);
            }
        }



        static object lockWriteTxt = new object();
        public static void WriteTxt(string simno,byte[] _data, int mark,DateTime time)
        {
            //mark:0发送,1接收
            try
            {
                lock (lockWriteTxt)
                {
                    string str = BytetoHex(_data);
                    string dt = time.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    if (mark == 0)
                    {
                        str = simno + " " + dt + " [发送] " + str + "\r\n";
                    }
                    else if (mark == 1)
                    {
                        str = simno + " " + dt + " [接收] " + str + "\r\n";
                    }
                    string filepath = "";
                    filepath = System.AppDomain.CurrentDomain.BaseDirectory + "CommandStrGprs\\";
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    filepath += "{0}.txt";
                    filepath = string.Format(filepath, DateTime.Now.ToString("yyyy-MM-dd"));
                    File.AppendAllText(filepath, str);
                    File.AppendAllText(filepath, "\r\n");
                }
            }
            catch
            { }
        }

        static object lockWriteTxtSecond = new object();
        public static void WriteTxtSecond(byte[] _data, int mark, DateTime time)
        {
            //mark:0发送,1接收
            try
            {
                lock (lockWriteTxtSecond)
                {
                    string str = BytetoHex(_data);
                    string dt = time.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    if (mark == 0)
                    {
                        str =  dt + " [发送] " + str + "\r\n";
                    }
                    else if (mark == 1)
                    {
                        str = dt + " [接收] " + str + "\r\n";
                    }
                    string filepath = "";
                    filepath = System.AppDomain.CurrentDomain.BaseDirectory + "CommandStrGprs\\";
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    filepath += "{0}.txt";
                    filepath = string.Format(filepath, DateTime.Now.ToString("yyyy-MM-dd"));
                    File.AppendAllText(filepath, str);
                    File.AppendAllText(filepath, "\r\n");
                }
            }
            catch
            { }
        }
        static object lockWriteTxtThird = new object();
        public static void WriteTxtThird(string content, int mark, DateTime time)
        {
            lock (lockWriteTxtThird)
            {
                try
                {
                    string str = "";
                    string dt = time.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    if (mark == 0)
                    {
                        str = dt + " [发送] " + content; //+ "\r\n";
                    }
                    else if (mark == 1)
                    {
                        str = dt + " [接收] " + content; //+ "\r\n";
                    }
                    string filepath = "";
                    filepath = System.AppDomain.CurrentDomain.BaseDirectory + "CommandStrGprs\\";
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    filepath += "{0}.txt";
                    filepath = string.Format(filepath, DateTime.Now.ToString("yyyy-MM-dd"));
                    File.AppendAllText(filepath, str);
                    File.AppendAllText(filepath, "\r\n");
                }
                catch (Exception ex)
                { }
            }
        }

        public static void WriteException(string content)
        {
            try
            {
                string filepath = "";
                filepath = System.AppDomain.CurrentDomain.BaseDirectory + "ExceptionContent\\";
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath += "{0}.txt";
                filepath = string.Format(filepath, DateTime.Now.ToString("yyyy-MM-dd"));
                string dt = DateTime.Now.ToString();
                content = dt + "   " + content + "\r\n";
                File.AppendAllText(filepath, content);
                File.AppendAllText(filepath, "\r\n");
            }
            catch
            { }
        }
    }
}

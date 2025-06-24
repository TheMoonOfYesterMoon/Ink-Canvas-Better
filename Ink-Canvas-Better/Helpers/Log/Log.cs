using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink_Canvas_Better.Helpers.Log
{
    class Log
    {
        public static string LogFile = "Logs/Log.txt";

        public static void NewLog(string str)
        {
            WriteLog(str, LogType.Info);
        }

        public static void WriteLogToFile(string str)
        {
            WriteLog(str, LogType.Info);
        }

        public static void WriteLogToFile(string str, LogType logType = LogType.Info)
        {
            WriteLog(str, logType);
        }

        private static void WriteLog(string str, LogType logType)
        {
            string strLogType = "Info";
            switch (logType)
            {
                case LogType.Event:
                    strLogType = "Event";
                    break;
                case LogType.Trace:
                    strLogType = "Trace";
                    break;
                case LogType.Error:
                    strLogType = "Error";
                    break;
            }
            try
            {
                var file = App.RootPath + LogFile;
                if (!Directory.Exists(App.RootPath))
                {
                    Directory.CreateDirectory(App.RootPath);
                }
                StreamWriter sw = new StreamWriter(file, true);
                sw.WriteLine(string.Format("{0} [{1}] {2}", DateTime.Now.ToString("O"), strLogType, str));
                sw.Close();
            }
            catch { }
        }

        public enum LogType
        {
            Info,
            Trace,
            Error,
            Event
        }
    }
}

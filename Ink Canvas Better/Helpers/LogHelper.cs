using System.IO;
using Ink_Canvas_Better.Enums;

namespace Ink_Canvas_Better.Helpers
{
    internal class LogHelper
    {
        public static string LogFilePath = Path.Combine("Logs", "Log.txt");

        public static void NewLog(string str)
        {
            WriteLog(str, GeneralEnums.LogType.Info);
        }

        public static void WriteLogToFile(string str)
        {
            WriteLog(str, GeneralEnums.LogType.Info);
        }

        public static void WriteLogToFile(string str, GeneralEnums.LogType logType = GeneralEnums.LogType.Info)
        {
            WriteLog(str, logType);
        }

        private static void WriteLog(string str, GeneralEnums.LogType logType)
        {
            string strLogType = "Info";
            switch (logType)
            {
                case GeneralEnums.LogType.Event:
                    strLogType = "Event";
                    break;
                case GeneralEnums.LogType.Trace:
                    strLogType = "Trace";
                    break;
                case GeneralEnums.LogType.Error:
                    strLogType = "Error";
                    break;
            }
            try
            {
                var file = App.RootPath + LogFilePath;
                if (!Directory.Exists(App.RootPath))
                {
                    Directory.CreateDirectory(App.RootPath);
                }
                StreamWriter sw = new StreamWriter(file, true);
                sw.WriteLine(string.Format("{0} [{1}] {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), strLogType, str));
                sw.Close();
            }
            catch { }
        }
    }
}

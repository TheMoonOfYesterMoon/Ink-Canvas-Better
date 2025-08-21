using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace Ink_Canvas_Better
{
    public partial class App : Application
    {

        public static string[] StartArgs = null;
        public static string RootPath = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas\\";
        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Helpers.Log.NewLog(e.Exception.ToString());
            // TODO: show in the messagebox
            // Ink_Canvas.MainWindow.ShowNewMessage($"抱歉，出现预料之外的异常，可能导致 Ink Canvas 画板运行不稳定。\n建议保存墨迹后重启应用。\n报错信息：\n{e.ToString()}", true);
            e.Handled = true;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            RootPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            if (!Directory.Exists($"{RootPath}Logs"))
            {
                Directory.CreateDirectory($"{RootPath}Logs");
            }

            Helpers.Log.NewLog(string.Format("Ink Canvas Starting (Version: {0})", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            

            Mutex _ = new Mutex(true, "Ink_Canvas_Better", out bool ret);

            if (!ret && !e.Args.Contains("-m")) // -m multiple
            {
                Helpers.Log.NewLog("Detected existing instance");
                MessageBox.Show("已有一个程序实例正在运行");
                Helpers.Log.NewLog("Ink Canvas automatically closed");
                Environment.Exit(0);
            }

            StartArgs = e.Args;
        }
    }
}

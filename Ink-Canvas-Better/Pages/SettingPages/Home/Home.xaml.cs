using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Resources;
using Ink_Canvas_Better.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Pages.SettingPages
{
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

            Loaded += Home_Loaded;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.isCloseFromButton = true;
            Application.Current.Shutdown();
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location, "-m");
            RuntimeData.isCloseFromButton = true;
            Application.Current.Shutdown();
        }

        private void ButtonLog_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Helpers.Log.LogFilePath))
            {
                Process.Start(Helpers.Log.LogFilePath);
            }
        }

        private void ButtonResetSettings_Click(object sender, RoutedEventArgs e)
        {
            Setting.ResetSettings();
        }

        private void SettingsCard_Github_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/BaiYang2238/Ink-Canvas-Better") { UseShellExecute = true });
        }

        private void SettingsCard_Author_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/BaiYang2238") { UseShellExecute = true });
        }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            GroupBox_About_1.Header = $"Ink Canvas Better v{Version}";
            String[] Version1 = Version.Split('.');
            if (int.TryParse(Version1[3], out int i))
            {
                if (i > 0)
                {
                    GroupBox_About_1.Header = Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - beta";
                }
            }
        }

        private void SettingsCard_Language_Click(object sender, RoutedEventArgs e)
        {
            Language languageWindow = new Language();
            languageWindow.ShowDialog();
        }
    }
}

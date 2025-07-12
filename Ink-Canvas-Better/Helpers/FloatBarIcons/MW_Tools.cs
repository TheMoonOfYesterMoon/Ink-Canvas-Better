using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        public void ToolsIcon_Click(object sender, RoutedEventArgs e)
        {
            // Temp
            if (settingWindow == null || !IsWindowValid(settingWindow))
            {
                settingWindow = new SettingWindow();
                settingWindow.Closed += (s, args) => settingWindow = null;
                settingWindow.Show();
            }
            else
            {
                settingWindow.Activate();

                if (settingWindow.WindowState == WindowState.Minimized)
                {
                    settingWindow.WindowState = WindowState.Normal;
                }

                settingWindow.Topmost = true;
                settingWindow.Topmost = false;
            }
        }

        // Temp
        private bool IsWindowValid(Window window)
        {
            return window != null && PresentationSource.FromVisual(window) != null;
        }
    }
}

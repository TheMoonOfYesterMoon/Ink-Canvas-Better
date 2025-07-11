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
            Windows.Setting settingWindow = new Windows.Setting();
            settingWindow.Show();
        }
    }
}

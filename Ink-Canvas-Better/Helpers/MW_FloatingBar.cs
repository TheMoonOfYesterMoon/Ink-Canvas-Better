using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        bool isFloatingBatFolded = false;
        public void FoldFloatingBar()
        {
            Border_DrawingTools.Visibility = Visibility.Collapsed;
            Border_Others.Visibility = Visibility.Collapsed;
            Border_TouchSetting.Visibility = Visibility.Collapsed;
            Border_ExitPPT.Visibility = Visibility.Collapsed;
            isFloatingBatFolded = true;
        }
        public void UnFoldFloatingBar()
        {
            Border_DrawingTools.Visibility = Visibility.Visible;
            Border_Others.Visibility = Visibility.Visible;
            Border_TouchSetting.Visibility = Visibility.Visible;
            Border_ExitPPT.Visibility = Visibility.Visible;
            isFloatingBatFolded = false;
        }
        public void SwitchFloatingBatFoldingState()
        {
            if (isFloatingBatFolded)
            {
                UnFoldFloatingBar();
            }
            else
            {
                FoldFloatingBar();
            }
        }
    }
}

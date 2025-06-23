using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better
{ 
    public partial class MainWindow
    {
        public void PenIcon_Click(Object sender, RoutedEventArgs e)
        {
            // TODO
            // 临时代码
            if (MainWindow_Grid.Background != Brushes.Transparent)
            {
                MainWindow_Grid.Background = Brushes.Transparent;
            }
            else
            {
                MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
            }

            if (CurrentMode != Mode.Pen)
            {
                CurrentMode = Mode.Pen;
                // TODO
            }
            else
            {
                // TODO
            }
        }
    }
}

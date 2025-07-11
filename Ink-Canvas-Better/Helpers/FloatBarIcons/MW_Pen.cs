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
            if (CurrentMode != Mode.Pen)
            {
                MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                CurrentMode = Mode.Pen;
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
            else
            {
                // TODO
            }
        }
    }
}

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
        public void CursorIcon_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMode != Mode.None)
            {
                CursorIcon_ChangeToCursor();
                SwitchButtonStatus(CurrentMode.ToString());
            }
        }

        public void CursorIcon_ChangeToCursor()
        {
            MainWindow_Grid.Background = Brushes.Transparent;
            CurrentMode = Mode.None;
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
        }
    }
}

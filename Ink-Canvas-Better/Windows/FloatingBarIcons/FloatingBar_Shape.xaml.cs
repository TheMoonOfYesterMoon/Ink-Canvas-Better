using Ink_Canvas_Better.Resources;
using iNKORE.UI.WPF.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;

namespace Ink_Canvas_Better.Windows.FloatingBarIcons
{
    public partial class FloatingBar_Shape : UserControl
    {
        public FloatingBar_Shape()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Shape.IsOpen = false;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Shape.StaysOpen = !RuntimeData.mainWindow.Popup_Shape.StaysOpen;
            if (RuntimeData.mainWindow.Popup_Shape.StaysOpen)
            {
                PinButton.FindVisualChild<iNKORE.UI.WPF.Modern.Controls.FontIcon>().Glyph = "\ue77a";
            }
            else
            {
                PinButton.FindVisualChild<iNKORE.UI.WPF.Modern.Controls.FontIcon>().Glyph = "\ue718";
            }
        }

    }
}

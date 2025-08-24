using Ink_Canvas_Better.Helpers.Others;
using Ink_Canvas_Better.Resources;
using iNKORE.UI.WPF.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
            ((Popup)this.GetFirstLogicalTreeParent(typeof(Popup))).IsOpen = false;
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

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RuntimeData.CurrentDrawStep = 0;
            RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Shape;
            RuntimeData.CurrentShape = ((Image)sender).Name;
        }
    }
}

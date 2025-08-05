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
    public partial class FloatingBar_Eraser : UserControl
    {
        private bool _isSquare = false;

        public FloatingBar_Eraser()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Eraser.IsOpen = false;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Eraser.StaysOpen = !RuntimeData.mainWindow.Popup_Eraser.StaysOpen;
            if (RuntimeData.mainWindow.Popup_Eraser.StaysOpen)
            {
                PinButton.FindVisualChild<iNKORE.UI.WPF.Modern.Controls.FontIcon>().Glyph = "\ue77a";
            }
            else
            {
                PinButton.FindVisualChild<iNKORE.UI.WPF.Modern.Controls.FontIcon>().Glyph = "\ue718";
            }
        }

        private void Slider_StrokeThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ChangeEraserShape();
        }

        private void ToggleButton_EraseByStroke_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_EraseByStroke.IsChecked = true;
            ToggleButton_EraseByPoint.IsChecked = false;
            RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        private void ToggleButton_EraseByPoint_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_EraseByStroke.IsChecked = false;
            ToggleButton_EraseByPoint.IsChecked = true;
            RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void ToggleButton_Square_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Square.IsChecked = true;
            ToggleButton_Circular.IsChecked = false;
            _isSquare = true;
            ChangeEraserShape();
        }

        private void ToggleButton_Circular_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Square.IsChecked= false;
            ToggleButton_Circular.IsChecked = true;
            _isSquare = false;
            ChangeEraserShape();
        }

        private void ChangeEraserShape()
        {
            if (RuntimeData.mainWindow != null && RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Highlighter)
            {
                RuntimeData.mainWindow.MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Highlighter;
                RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                RuntimeData.mainWindow.inkCanvas.DefaultDrawingAttributes = RuntimeData.CurrentDrawingAttributes_Highlighter;
            }
            var value = Slider_StrokeThickness.Value;
            if (RuntimeData.mainWindow?.inkCanvas != null)
            {
                // it`s necessary
                RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                if (_isSquare)
                {
                    RuntimeData.mainWindow.inkCanvas.EraserShape = new RectangleStylusShape(value * 0.8, value);
                }
                else
                {
                    RuntimeData.mainWindow.inkCanvas.EraserShape = new EllipseStylusShape(value, value);
                }
            }
        }
    }
}

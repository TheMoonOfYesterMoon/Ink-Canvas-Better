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
            ChangeToEraserMode();
            ChangeEraserShape();
        }

        private void ToggleButton_EraseByStroke_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Circular.IsEnabled = false;
            ToggleButton_Square.IsEnabled = false;
            ToggleButton_EraseByStroke.IsChecked = true;
            ToggleButton_EraseByPoint.IsChecked = false;
            RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Eraser;
            RuntimeData.CurrentEraserMode = RuntimeData.EraserMode.Stroke;
        }

        private void ToggleButton_EraseByPoint_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Circular.IsEnabled = true;
            ToggleButton_Square.IsEnabled = true;
            ToggleButton_EraseByStroke.IsChecked = false;
            ToggleButton_EraseByPoint.IsChecked = true;
            RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Eraser;
            RuntimeData.CurrentEraserMode = RuntimeData.EraserMode.Point;
        }

        private void ToggleButton_Square_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Square.IsChecked = true;
            ToggleButton_Circular.IsChecked = false;
            Preview_Square.Visibility = Visibility.Visible;
            Preview_Circular.Visibility = Visibility.Collapsed;
            _isSquare = true;
            ChangeToEraserMode();
            ChangeEraserShape();
        }

        private void ToggleButton_Circular_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton_Square.IsChecked= false;
            ToggleButton_Circular.IsChecked = true;
            Preview_Square.Visibility = Visibility.Collapsed;
            Preview_Circular.Visibility = Visibility.Visible;
            _isSquare = false;
            ChangeToEraserMode();
            ChangeEraserShape();
        }

        private void ChangeEraserShape()
        {
            var value = Slider_StrokeThickness.Value;
            if (RuntimeData.mainWindow != null)
            {
                if (_isSquare)
                {
                    RuntimeData.CurrentEraserShape = new RectangleStylusShape(value, value);
                }
                else
                {
                    RuntimeData.CurrentEraserShape = new EllipseStylusShape(value, value);
                }
                // it's necessary
                RuntimeData.mainWindow.inkCanvas.EraserShape = RuntimeData.CurrentEraserShape;
                RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
        }

        private void ChangeToEraserMode()
        {
            if (RuntimeData.mainWindow != null && (RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Eraser))
            {
                RuntimeData.mainWindow.MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Eraser;
                if (RuntimeData.CurrentEraserMode == RuntimeData.EraserMode.Stroke)
                {
                    RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                }
                else
                {
                    RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                }
            }
        }
    }
}

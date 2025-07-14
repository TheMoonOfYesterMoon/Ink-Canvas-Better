using Ink_Canvas_Better.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better.Windows.FloatingBarIcons
{
    /// <summary>
    /// FloatingBar_Pen.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingBar_Pen : UserControl
    {
        public FloatingBar_Pen()
        {
            InitializeComponent();

            AddHandler(ICB_PresetColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
        }
        private void OnColorSelected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is ICB_PresetColor selector)
            {
                ColorPreview.Fill = new SolidColorBrush(selector.Color);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement pinning functionality
        }

        private void Slider_StrokeThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.DrawingAttributes.Width = e.NewValue;
            MainWindow.DrawingAttributes.Height = e.NewValue;
        }

        public void DrawingColorChanged()
        {
            ColorPreview.Fill = new SolidColorBrush(MainWindow.DrawingAttributes.Color);
        }
    }
}

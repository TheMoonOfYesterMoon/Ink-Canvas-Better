using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Ink_Canvas_Better;

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

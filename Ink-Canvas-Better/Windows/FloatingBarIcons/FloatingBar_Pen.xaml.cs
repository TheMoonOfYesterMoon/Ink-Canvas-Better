using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Resources;
using System;
using System.Diagnostics;
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

            RuntimeData.floatingBar_Pen = this;
            AddHandler(ICB_PresetColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
            AddHandler(ICB_CustomColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
        }
        private void OnColorSelected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is ICB_PresetColor selector)
            {
                ColorPreview.Fill = new SolidColorBrush(selector.Color);
            } else if (e.OriginalSource is ICB_CustomColor Selector)
            {
                ColorPreview.Fill = new SolidColorBrush(Selector.Color);
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
            RuntimeData.DrawingAttributes.Width = e.NewValue;
            RuntimeData.DrawingAttributes.Height = e.NewValue;
        }

        public void DrawingColorChanged()
        {
            ColorPreview.Fill = new SolidColorBrush(RuntimeData.DrawingAttributes.Color);
        }

        public void ToggleButton_inkStyle_Unchecked(object sender, RoutedEventArgs e)
        {
            RuntimeData.settingData.Runtime.InkStyle = InkStyle.Default;
            inkstyleTextBlock.Text = Properties.Resources.Off;
            ToggleButton_inkStyle.IsChecked = false;
            Setting.SaveSettings();
        }

        public void ToggleButton_inkStyle_Checked(object sender, RoutedEventArgs e)
        {
            RuntimeData.settingData.Runtime.InkStyle = InkStyle.Simulative;
            inkstyleTextBlock.Text = Properties.Resources.On;
            ToggleButton_inkStyle.IsChecked = true;
            Setting.SaveSettings();
        }
    }
}

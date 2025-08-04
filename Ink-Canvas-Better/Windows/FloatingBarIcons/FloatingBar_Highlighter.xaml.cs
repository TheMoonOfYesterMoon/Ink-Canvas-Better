using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Resources;
using iNKORE.UI.WPF.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Windows.Media;

namespace Ink_Canvas_Better.Windows.FloatingBarIcons
{
    /// <summary>
    /// FloatingBar_Highlighter.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingBar_Highlighter : UserControl
    {
        public FloatingBar_Highlighter()
        {
            InitializeComponent();

            RuntimeData.floatingBar_Highlighter = this;
            AddHandler(ICB_PresetColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
            AddHandler(ICB_CustomColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
        }

        private void OnColorSelected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is ICB_PresetColor presetSelector)
            {
                ColorPreview.Fill = new SolidColorBrush(presetSelector.Color);
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color = Color.FromArgb(
                    (byte)(Slider_Opacity.Value / 100d * 255d),
                    presetSelector.Color.R,
                    presetSelector.Color.G,
                    presetSelector.Color.B);
                AllColorUnselected();
                presetSelector.IsSelected = true;
            } else if (e.OriginalSource is ICB_CustomColor customSelector)
            {
                ColorPreview.Fill = new SolidColorBrush(customSelector.Color);
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color = Color.FromArgb(
                    (byte)(Slider_Opacity.Value / 100d * 255d),
                    customSelector.Color.R,
                    customSelector.Color.G,
                    customSelector.Color.B);
                AllColorUnselected();
                customSelector.IsSelected = true;
            }
        }

        private void AllColorUnselected()
        {
            Color0.IsSelected = false;
            Color1.IsSelected = false;
            Color2.IsSelected = false;
            Color3.IsSelected = false;
            Color4.IsSelected = false;
            Color5.IsSelected = false;
            Color6.IsSelected = false;
            Color7.IsSelected = false;
            Color8.IsSelected = false;
            Color9.IsSelected = false;
            Color10.IsSelected = false;
            Color11.IsSelected = false;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Pen.IsOpen = false;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Pen.StaysOpen = !RuntimeData.mainWindow.Popup_Pen.StaysOpen;
            if (RuntimeData.mainWindow.Popup_Pen.StaysOpen)
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
            //RuntimeData.CurrentDrawingAttributes_Highlighter.Width = e.NewValue;
            RuntimeData.CurrentDrawingAttributes_Highlighter.Height = e.NewValue;
        }

        private void Slider_Opacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RuntimeData.CurrentDrawingAttributes_Highlighter.Color = Color.FromArgb(
                (byte)(e.NewValue/100d*255d),
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color.R,
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color.G,
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color.B);
        }

        public void DrawingColorChanged()
        {
            ColorPreview.Fill = new SolidColorBrush(RuntimeData.CurrentDrawingAttributes_Highlighter.Color);
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

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
    public partial class FloatingBar_Highlighter : UserControl
    {
        public FloatingBar_Highlighter()
        {
            InitializeComponent();

            RuntimeData.floatingBar_Highlighter = this;
            AddHandler(ICB_CustomColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
        }

        private void OnColorSelected(object sender, RoutedEventArgs e)
        {
            SwitchEdittingMode();
            var customSelector = (ICB_CustomColor)sender;
            ColorPreview.Fill = new SolidColorBrush(customSelector.Color);
            RuntimeData.CurrentDrawingAttributes_Highlighter.Color = Color.FromArgb(
                (byte)(Slider_Alpha.Value / 100d * 255d),
                customSelector.Color.R,
                customSelector.Color.G,
                customSelector.Color.B);
            AllColorUnselected();
            customSelector.IsSelected = true;
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
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Highlighter.IsOpen = false;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Highlighter.StaysOpen = !RuntimeData.mainWindow.Popup_Highlighter.StaysOpen;
            if (RuntimeData.mainWindow.Popup_Highlighter.StaysOpen)
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
            SwitchEdittingMode();
            //RuntimeData.CurrentDrawingAttributes_Highlighter.Width = e.NewValue;
            RuntimeData.CurrentDrawingAttributes_Highlighter.Height = e.NewValue;
        }

        private void Slider_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SwitchEdittingMode();
            RuntimeData.CurrentDrawingAttributes_Highlighter.Color = Color.FromArgb(
                (byte)(e.NewValue/100d*255d),
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color.R,
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color.G,
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color.B);
        }

        public void ToggleButton_inkStyle_Unchecked(object sender, RoutedEventArgs e)
        {
            SwitchEdittingMode();
            RuntimeData.settingData.Runtime.InkStyle = InkStyle.Default;
            //inkstyleTextBlock.Text = Application.Current.Resources["Off"].ToString();
            ToggleButton_inkStyle.IsChecked = false;
            Setting.SaveSettings();
        }

        public void ToggleButton_inkStyle_Checked(object sender, RoutedEventArgs e)
        {
            SwitchEdittingMode();
            RuntimeData.settingData.Runtime.InkStyle = InkStyle.Simulative;
            //inkstyleTextBlock.Text = Application.Current.Resources["On"].ToString();
            ToggleButton_inkStyle.IsChecked = true;
            Setting.SaveSettings();
        }

        private void SwitchEdittingMode()
        {
            if (RuntimeData.mainWindow!= null && RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Highlighter)
            {
                RuntimeData.mainWindow.MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Highlighter;
                RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                RuntimeData.mainWindow.inkCanvas.DefaultDrawingAttributes = RuntimeData.CurrentDrawingAttributes_Highlighter;
            }
        }

        private void ToggleButton_CustomColor_Checked(object sender, RoutedEventArgs e)
        {
            Color0.IsCustomizingColor = true;
            Color1.IsCustomizingColor = true;
            Color2.IsCustomizingColor = true;
            Color3.IsCustomizingColor = true;
            Color4.IsCustomizingColor = true;
            Color5.IsCustomizingColor = true;
            Color6.IsCustomizingColor = true;
            Color7.IsCustomizingColor = true;
            Color8.IsCustomizingColor = true;
            Color9.IsCustomizingColor = true;
            Color10.IsCustomizingColor = true;

        }

        private void ToggleButton_CustomColor_Unchecked(object sender, RoutedEventArgs e)
        {
            Color2.IsCustomizingColor = false;
            Color1.IsCustomizingColor = false;
            Color0.IsCustomizingColor = false;
            Color3.IsCustomizingColor = false;
            Color4.IsCustomizingColor = false;
            Color5.IsCustomizingColor = false;
            Color6.IsCustomizingColor = false;
            Color7.IsCustomizingColor = false;
            Color8.IsCustomizingColor = false;
            Color9.IsCustomizingColor = false;
            Color10.IsCustomizingColor = false;

        }
    }
}

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
    public partial class FloatingBar_Pen : UserControl
    {
        public FloatingBar_Pen()
        {
            InitializeComponent();

            RuntimeData.floatingBar_Pen = this;
            AddHandler(ICB_CustomColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
        }
        
        private void OnColorSelected(object sender, RoutedEventArgs e)
        {
            SwitchEdittingMode();
            if (e.OriginalSource is ICB_CustomColor customSelector)
            {
                ColorPreview.Fill = new SolidColorBrush(customSelector.Color);
                RuntimeData.CurrentDrawingAttributes_Pen.Color = customSelector.Color;
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
            SwitchEdittingMode();
            RuntimeData.CurrentDrawingAttributes_Pen.Width = e.NewValue;
            RuntimeData.CurrentDrawingAttributes_Pen.Height = e.NewValue;
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
            if (RuntimeData.mainWindow != null && RuntimeData.CurrentDrawingMode != RuntimeData.DrawingMode.Pen)
            {
                RuntimeData.mainWindow.MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Pen;
                RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                RuntimeData.mainWindow.inkCanvas.DefaultDrawingAttributes = RuntimeData.CurrentDrawingAttributes_Pen;
            }
        }

        private void ToggleButton_CustomColor_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleButton_CustomColor_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}

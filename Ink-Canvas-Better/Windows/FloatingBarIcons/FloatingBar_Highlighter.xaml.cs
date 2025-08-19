using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Helpers.Others;
using Ink_Canvas_Better.Resources;
using iNKORE.UI.WPF.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Ink_Canvas_Better.Windows.FloatingBarIcons
{
    public partial class FloatingBar_Highlighter : UserControl
    {
        private readonly Collection<UIElement> CustomColorCollection = new Collection<UIElement>();

        public FloatingBar_Highlighter()
        {
            InitializeComponent();

            AddAllCustomColorToCollection();
            RuntimeData.floatingBar_Highlighter = this;
            AddHandler(ICB_CustomColor.ColorSelectedEvent, new RoutedEventHandler(OnColorSelected));
        }

        #region Properties

        #region StaysOpen

        readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register(
                "StaysOpen",
                typeof(bool),
                typeof(FloatingBar_Highlighter),
                new PropertyMetadata(StaysOpen_OnValueChanged)
            );

        public bool StaysOpen
        {
            get { return (bool)GetValue(StaysOpenProperty); }
            set { SetValue(StaysOpenProperty, value); }
        }

        public static void StaysOpen_OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FloatingBar_Highlighter;
            control.FindAscendant<Popup>().StaysOpen = (bool)e.NewValue;
            if ((bool)e.NewValue)
            {
                control.PinButton.FindDescendant<iNKORE.UI.WPF.Modern.Controls.FontIcon>().Glyph = "\ue77a";
            }
            else
            {
                control.PinButton.FindDescendant<iNKORE.UI.WPF.Modern.Controls.FontIcon>().Glyph = "\ue718";
            }
        }

        #endregion

        #endregion

        private void OnColorSelected(object sender, RoutedEventArgs e)
        {
            SwitchEdittingMode();
            if (e.OriginalSource is ICB_CustomColor customSelector)
            {
                ColorPreview.Fill = new SolidColorBrush(customSelector.Color);
                RuntimeData.CurrentDrawingAttributes_Highlighter.Color = customSelector.Color;
                foreach (ICB_CustomColor item in CustomColorCollection.Cast<ICB_CustomColor>())
                {
                    item.IsSelected = false;
                }
                customSelector.IsSelected = true;
            }
        }

        private void AddAllCustomColorToCollection()
        {
            CustomColorCollection.Add(Color0);
            CustomColorCollection.Add(Color1);
            CustomColorCollection.Add(Color2);
            CustomColorCollection.Add(Color3);
            CustomColorCollection.Add(Color4);
            CustomColorCollection.Add(Color5);
            CustomColorCollection.Add(Color6);
            CustomColorCollection.Add(Color7);
            CustomColorCollection.Add(Color8);
            CustomColorCollection.Add(Color9);
            CustomColorCollection.Add(Color10);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ((Popup)this.GetFirstLogicalTreeParent(typeof(Popup))).IsOpen = false;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            StaysOpen = !StaysOpen;
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
                RuntimeData.mainWindow.inkCanvas.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                RuntimeData.CurrentDrawingMode = RuntimeData.DrawingMode.Highlighter;
                RuntimeData.mainWindow.inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                RuntimeData.mainWindow.inkCanvas.DefaultDrawingAttributes = RuntimeData.CurrentDrawingAttributes_Highlighter;
            }
        }

        private void ToggleButton_CustomColor_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ICB_CustomColor item in CustomColorCollection.Cast<ICB_CustomColor>())
            {
                item.IsCustomizingColor = true;
            }
        }

        private void ToggleButton_CustomColor_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (ICB_CustomColor item in CustomColorCollection.Cast<ICB_CustomColor>())
            {
                item.IsCustomizingColor = false;
            }
        }
    }
}

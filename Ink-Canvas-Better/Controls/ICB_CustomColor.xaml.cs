using Ink_Canvas_Better.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ColorConverter = Ink_Canvas_Better.Helpers.Others.ColorConverter;

namespace Ink_Canvas_Better.Controls
{
    public partial class ICB_CustomColor : UserControl
    {
        bool _IsFirstSetColor = true;
        bool _temp = false;

        public ICB_CustomColor()
        {
            InitializeComponent();
        }

        #region Properties

        #region Color

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color",
                typeof(Color),
                typeof(ICB_CustomColor),
                new PropertyMetadata(Color_OnValueChanged)
            );

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void Color_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_CustomColor)dependencyObject;
            control.InnerBorder.Background = new SolidColorBrush((Color)eventArgs.NewValue);
            if (control._IsFirstSetColor)
            {
                control._IsFirstSetColor = false;
                control.ColorPicker.SelectedColor = (Color)eventArgs.NewValue;
                ColorConverter.ColorToRgb((Color)eventArgs.NewValue, out byte r, out byte g, out byte b);
                control.R.Value = r;
                control.G.Value = g;
                control.B.Value = b;
                ColorConverter.ColorToHsl((Color)eventArgs.NewValue, out double h, out double s, out double l);
                control.H.Value = h;
                control.S.Value = s;
                control.L.Value = l;
                control.Hex.Text = ColorConverter.ColorToHex((Color)eventArgs.NewValue);
            }
        }

        #endregion

        #region IsSelected

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                "IsSelected",
                typeof(bool),
                typeof(ICB_CustomColor),
                new PropertyMetadata(IsSelected_OnValueChanged)
            );

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        private static void IsSelected_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_CustomColor)dependencyObject;
            if ((bool)eventArgs.NewValue)
            {
                control.Viewbox1.Visibility = Visibility.Visible;
            }
            else
            {
                control.Viewbox1.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #endregion

        #region Events

        public static readonly RoutedEvent ColorSelectedEvent =
            EventManager.RegisterRoutedEvent(
                "ColorSelected",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ICB_CustomColor));

        public event RoutedEventHandler ColorSelected
        {
            add => AddHandler(ColorSelectedEvent, value);
            remove => RemoveHandler(ColorSelectedEvent, value);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var args = new RoutedEventArgs(ColorSelectedEvent, this);
            RaiseEvent(args);
            e.Handled = true;
        }

        private void Custom_Click(object sender, RoutedEventArgs e)
        {
            Popup_ColorPicker.IsOpen = !Popup_ColorPicker.IsOpen;
        }

        private void SquarePicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            Color = ColorPicker.SelectedColor;
            Button_Click(sender, e);
            _temp = true;

            ColorConverter.ColorToRgb(Color, out byte r, out byte g, out byte b);
            R.Value = r;
            G.Value = g;
            B.Value = b;
            ColorConverter.ColorToHsl(Color, out double h, out double s, out double l);
            H.Value = h;
            S.Value = s;
            L.Value = l;
            Hex.Text = ColorConverter.ColorToHex(Color);

            _temp = false;
        }

        private void NumberBox_ValueChanged(iNKORE.UI.WPF.Modern.Controls.NumberBox sender, iNKORE.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            if (!_temp)
            {
                switch ((String)sender.Tag)
                {
                    case "RGB":
                    {
                        Color = ColorConverter.RgbToColor((byte)R.Value, (byte)G.Value, (byte)B.Value);

                        ColorPicker.SelectedColor = Color;
                        ColorConverter.ColorToHsl(Color, out double h, out double s, out double l);
                        H.Value = h;
                        S.Value = s;
                        L.Value = l;
                        Hex.Text = ColorConverter.ColorToHex(Color);
                        break;
                    }
                    case "HSL":
                    {
                        Color = ColorConverter.HslToColor(H.Value, S.Value, L.Value);

                        ColorPicker.SelectedColor = Color;
                        ColorConverter.ColorToRgb(Color, out byte r, out byte g, out byte b);
                        R.Value = r;
                        G.Value = g;
                        B.Value = b;
                        Hex.Text = ColorConverter.ColorToHex(Color);
                        break;
                    }
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(((TextBox)sender).Text, @"^#?([0-9a-fA-F]{6})$"))
            {
                Hex.Text = "000000";
                throw new FormatException("Invalid Hex Color Format. Expected format: #RRGGBB or RRGGBB.");
            }
            else
            {
                Color = ColorConverter.HexToColor(Hex.Text);

                ColorPicker.SelectedColor = Color;
                ColorConverter.ColorToRgb(Color, out byte r, out byte g, out byte b);
                R.Value = r;
                G.Value = g;
                B.Value = b;
                ColorConverter.ColorToHsl(Color, out double h, out double s, out double l);
                H.Value = h;
                S.Value = s;
                L.Value = l;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Popup_ColorPicker.IsOpen = false;
        }
    }
}

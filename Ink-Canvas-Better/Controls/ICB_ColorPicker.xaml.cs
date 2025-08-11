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
    /// <summary>
    /// ICB_ColorPicker.xaml 的交互逻辑
    /// </summary>
    public partial class ICB_ColorPicker : UserControl
    {
        private bool _temp = true;
        public ICB_ColorPicker()
        {
            InitializeComponent();
        }

        #region Events

        public static readonly RoutedEvent ColorPicker_ColorSelectedEvent =
            EventManager.RegisterRoutedEvent(
                "ColorSelected",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ICB_ColorPicker));

        public event RoutedEventHandler ColrPicker_ColorSelected { add => AddHandler(ColorPicker_ColorSelectedEvent, value); remove => RemoveHandler(ColorSelectedEvent, value); }

        #endregion

        private void SquarePicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            if (_temp)
            {
                _temp = false;
                Color c = ColorPicker.SelectedColor;
                R.Value = c.R;
                G.Value = c.G;
                B.Value = c.B;
                ColorConverter.ColorToHsl(c, out double h, out double s, out double l);
                H.Value = h;
                S.Value = s;
                L.Value = l;
                Hex.Text = ColorConverter.ColorToHex(c);
                var args = new RoutedEventArgs(ColorPicker_ColorSelectedEvent, this);
                RaiseEvent(args);
                e.Handled = true;
                _temp = true;
            }
        }

        private void NumberBox_ValueChanged(iNKORE.UI.WPF.Modern.Controls.NumberBox sender, iNKORE.UI.WPF.Modern.Controls.NumberBoxValueChangedEventArgs args)
        {
            if (_temp)
            {
                _temp = false;
                switch (sender.Tag)
                {
                    case "RGB":
                        Color c0 = Color.FromRgb((byte)R.Value, (byte)G.Value, (byte)B.Value);
                        ColorPicker.SelectedColor = c0;
                        ColorConverter.ColorToHsl(c0, out double h, out double s, out double l);
                        H.Value = h;
                        S.Value = s;
                        L.Value = l;
                        Hex.Text = ColorConverter.ColorToHex(c0);
                        break;
                    case "HSL":
                        Color c1 = ColorConverter.HslToColor(H.Value, S.Value, L.Value);
                        ColorPicker.SelectedColor = c1;
                        R.Value = c1.R;
                        G.Value = c1.G;
                        B.Value = c1.B;
                        Hex.Text = ColorConverter.ColorToHex(c1);
                        break;
                }
                var args1 = new RoutedEventArgs(ColorPicker_ColorSelectedEvent, this);
                RaiseEvent(args1);
                e.Handled = true;
                _temp = true;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_temp)
            {
                _temp = false;
                if (!Regex.IsMatch(((TextBox)sender).Text, @"^#?([0-9a-fA-F]{6})$"))
                {
                    Hex.Text = "000000";
                    throw new FormatException("Invalid Hex Color Format. Expected format: #RRGGBB or RRGGBB.");
                }
                else
                {
                    Color c = ColorConverter.HexToColor(Hex.Text);
                    ColorPicker.SelectedColor = c;
                    R.Value = c.R;
                    G.Value = c.G;
                    B.Value = c.B;
                    ColorConverter.ColorToHsl(c, out double h, out double s, out double l);
                    H.Value = h;
                    S.Value = s;
                    L.Value = l;
                }
                var args = new RoutedEventArgs(ColorPicker_ColorSelectedEvent, this);
                RaiseEvent(args);
                e.Handled = true;
                _temp = true;
            }
        }
    }
}

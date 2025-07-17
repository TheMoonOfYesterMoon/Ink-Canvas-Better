using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Ink_Canvas_Better.Helpers.Others
{
    static class ColorConverter
    {
        public static Color RgbToColor(byte r, byte g, byte b)
        {
            return Color.FromRgb(r, g, b);
        }

        public static void ColorToRgb(Color color, out byte r, out byte g, out byte b)
        {
            r = color.R;
            g = color.G;
            b = color.B;
        }

        public static Color HexToColor(string hex)
        {
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            byte r = Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = Convert.ToByte(hex.Substring(4, 2), 16);

            return Color.FromRgb(r, g, b);
        }

        public static string ColorToHex(Color color)
        {
            return $"{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static Color HslToColor(double h, double s, double l)
        {
            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
            double m = l - c / 2;

            double r1 = 0, g1 = 0, b1 = 0;

            if (h >= 0 && h < 60)
            {
                r1 = c; g1 = x; b1 = 0;
            }
            else if (h >= 60 && h < 120)
            {
                r1 = x; g1 = c; b1 = 0;
            }
            else if (h >= 120 && h < 180)
            {
                r1 = 0; g1 = c; b1 = x;
            }
            else if (h >= 180 && h < 240)
            {
                r1 = 0; g1 = x; b1 = c;
            }
            else if (h >= 240 && h < 300)
            {
                r1 = x; g1 = 0; b1 = c;
            }
            else if (h >= 300 && h <= 360)
            {
                r1 = c; g1 = 0; b1 = x;
            }

            byte r = (byte)Math.Round((r1 + m) * 255);
            byte g = (byte)Math.Round((g1 + m) * 255);
            byte b = (byte)Math.Round((b1 + m) * 255);

            return Color.FromRgb(r, g, b);
        }

        public static void ColorToHsl(Color color, out double h, out double s, out double l)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double delta = max - min;

            l = (max + min) / 2;

            if (delta < 0.00001)
            {
                s = 0;
                h = 0;
                return;
            }

            s = l > 0.5 ? delta / (2 - max - min) : delta / (max + min);

            if (Math.Abs(max - r) < 0.00001)
            {
                h = (g - b) / delta + (g < b ? 6 : 0);
            }
            else if (Math.Abs(max - g) < 0.00001)
            {
                h = (b - r) / delta + 2;
            }
            else
            {
                h = (r - g) / delta + 4;
            }

            h *= 60;
            if (h < 0) h += 360;
        }
    }
}

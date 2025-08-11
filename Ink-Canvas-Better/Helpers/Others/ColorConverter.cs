using System;
using System.Windows.Media;

namespace Ink_Canvas_Better.Helpers.Others
{
    static class ColorConverter
    {
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

        private static double HueToRgb(double p, double q, double t)
        {
            if (t < 0)
            {
                t += 1;
            }
            else if (t > 1)
            {
                t -= 1;
            };
            if (t < 1 / 6)
            {
                return p + (q - p) * 6 * t;
            }
            else if (t < 1 / 2)
            {
                return q;
            }
            else if (t < 2 / 3)
            {
                return p + (q - p) * (2 / 3 - t) * 6;
            }
            else
            {
                return p;
            }
        }

        public static Color HslToColor(double h, double s, double l)
        {
            double r, g, b;
            if (s == 0)
            {
                r = g = b = 1;
            }
            else 
            {
                var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                var p = 2 * l - q;
                r = HueToRgb(p, q, h + 1 / 3);
                g = HueToRgb(p, q, h);
                b = HueToRgb(p, q, h - 1 / 3);
            }
            return Color.FromRgb((byte)Math.Round(r*255d), (byte)Math.Round(g * 255d), (byte)Math.Round(b * 255d));
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

            if (delta == 0.00001)
            {
                s = 0;
                h = 0;
                return;
            }
            else
            {
                s = l > 0.5 ? delta / (2 - max - min) : delta / (max + min);
                if (max == r)
                {
                    h = (g - b) / delta + (g < b ? 6 : 0);
                }
                else if (max == g)
                {
                    h = (b - r) / delta + 2;
                }
                else
                {
                    h= (r - g) / delta + 4;
                }
                h /= 6;
            }
            //TODO
            h = Math.Floor(h * 100);
            s = Math.Round(s * 100);
            l = Math.Round(l * 100);
        }
    }
}

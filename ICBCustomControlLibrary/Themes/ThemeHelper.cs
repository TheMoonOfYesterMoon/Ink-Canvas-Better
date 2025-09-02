using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Diagnostics;

namespace ICBCustomControlLibrary.Themes
{
    public static class ThemeHelper
    {
        public static ResourceDictionary Dictionary { get; set; }
        static ThemeHelper()
        {
            Dictionary = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/ICBCustomControlLibrary;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
            };
        }

        public static FontFamily GetFont(string key)
        {
            if (Dictionary.Contains(key) && Dictionary[key] is FontFamily fontFamily)
            {
                Debug.WriteLine($"FontFamily found for key '{key}': {fontFamily.Source}");
                return fontFamily;
            }
            return new FontFamily(key);
        }

        public static FontFamily SegoeFluentIcons => GetFont("SegoeFluentIcons");
    }
}

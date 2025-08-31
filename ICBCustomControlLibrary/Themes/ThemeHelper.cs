using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace ICBCustomControlLibrary.Themes
{
    static class ThemeHelper
    {
        public static ResourceDictionary ThemeResources { get; set; } =
            new ResourceDictionary { Source = new Uri("/ICBCustomControlLibrary;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute) };
    }
}

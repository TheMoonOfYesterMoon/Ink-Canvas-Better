using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using iNKORE.UI.WPF.Modern;

namespace Ink_Canvas_Better.Helpers
{
    public class ThemeAndLangHelper
    {
        // TODO: The Code below are copy from abandoned project, a check needed
        public static void SwitchLanguage(string languageCode)
        {
            string path = $"Resources/Language/{languageCode}.xaml";
            ResourceDictionary newDict;
            try
            {
                newDict = new ResourceDictionary { Source = new Uri(path, UriKind.Relative) };
            }
            catch (Exception)
            {
                // TODO: LogHelper.WriteLogToFile("");
                newDict = new ResourceDictionary { Source = new Uri("Resources/Language/en.xaml", UriKind.Relative) };
            }

            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source?.OriginalString.Contains("Languages/") == true);

            if (oldDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            }

            Application.Current.Resources.MergedDictionaries.Add(newDict);
        }

        public static void ApplySystemTheme(bool? b)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var oldTheme = new ResourceDictionary();
                var newTheme = new ResourceDictionary();
                if (b == null | b == true)
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                    newTheme.Source = new Uri("Resources/Styles/Light.xaml", UriKind.Relative);
                    oldTheme.Source = new Uri("Resources/Styles/Dark.xaml", UriKind.Relative);
                }
                else
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                    oldTheme.Source = new Uri("Resources/Styles/Light.xaml", UriKind.Relative);
                    newTheme.Source = new Uri("Resources/Styles/Dark.xaml", UriKind.Relative);
                }
                Application.Current.Resources.MergedDictionaries.Remove(oldTheme);
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            });
        }
    }
}

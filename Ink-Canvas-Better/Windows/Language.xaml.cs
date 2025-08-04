using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Windows
{
    /// <summary>
    /// Language.xaml 的交互逻辑
    /// </summary>
    public partial class Language : Window
    {
        public Language()
        {
            InitializeComponent();
            LanguageListBox.ItemsSource = new List<String>(SupportedLanguage.Keys);
        }

        private readonly Dictionary<String, String> SupportedLanguage = new Dictionary<String, String>()
        {
            { "English", "en" },
            { "简体中文", "zh-CN" },
            { "繁体中文", "zh-TW" },
        };

        private void LanguageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = LanguageListBox.SelectedIndex != -1 ? OK.IsEnabled = true : OK.IsEnabled = false;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            SupportedLanguage.TryGetValue((String)LanguageListBox.SelectedItem, out String value);
            RuntimeData.settingData.Others.Language = value;
            Setting.SaveSettings();
            SwitchLanguage(value);

            this.Close();
        }

        private void SwitchLanguage(string languageCode)
        {
            string path = $"Resources/Language/{languageCode}.xaml";
            ResourceDictionary newDict;
            try
            {
                newDict = new ResourceDictionary { Source = new Uri(path, UriKind.Relative) };
            }
            catch (Exception)
            {
                //Log.WriteLogToFile("");
                newDict = new ResourceDictionary { Source = new Uri ("Resources/Language/en.xaml", UriKind.Relative) };
            }

            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source?.OriginalString.Contains("Languages/") == true);

            if (oldDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            }

            Application.Current.Resources.MergedDictionaries.Add(newDict);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Model;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Windows
{
    public partial class Language : Window
    {
        SettingsService settingsService = App.GetService<SettingsService>();

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
            SupportedLanguage.TryGetValue((String)LanguageListBox.SelectedItem, out string? value);
            if (value == null) value = "en";
            settingsService.Settings.Others.Language = value;
            settingsService.SaveData();
            ThemeAndLangHelper.SwitchLanguage(value);

            this.Close();
        }
    }
}

﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using iNKORE.UI.WPF.Modern;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using File = System.IO.File;

namespace Ink_Canvas_Better.Helpers
{
    internal class SettingsHelper
    {

        public static void LoadSettings(bool isStartup = false)
        {
            try
            {
                if (File.Exists(App.RootPath + RuntimeData.settingsFileName))
                {
                    try
                    {
                        string text = File.ReadAllText(App.RootPath + RuntimeData.settingsFileName);
                        RuntimeData.SettingModel = JsonConvert.DeserializeObject<SettingModel>(text);
                    }
                    catch { }
                }
                else
                {
                    ResetSettings();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile(ex.ToString(), LogHelper.LogType.Error);
            }

            #region OnStartup
            if (isStartup)
            {
                // There hasn`t a need to check it.
                // Pherhaps it will be used in the future.
            }
            try
            {
                if (RuntimeData.SettingModel.StartupAndUpdate.IsAutoStartup)
                {
                    if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Ink Canvas Better.lnk"))
                    {
                        StartAutomaticallyCreate("Ink Canvas Better");
                    }
                }
                else
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Ink Canvas Better.lnk"))
                    {
                        StartAutomaticallyDel("Ink Canvas Better");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile(ex.ToString(), LogHelper.LogType.Error);
            }
            #endregion
        }

        public static void SaveSettings()
        {
            string text = JsonConvert.SerializeObject(RuntimeData.SettingModel, Formatting.Indented);
            try
            {
                File.WriteAllText(App.RootPath + RuntimeData.settingsFileName, text);
            }
            catch { }
        }

        public static void ResetSettings()
        {
            RuntimeData.SettingModel = new SettingModel();
            SaveSettings();
        }

        public static bool StartAutomaticallyCreate(string exeName)
        {
            try
            {
                string f = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + exeName + ".lnk";
                if (File.Exists(f))
                {
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(f);
                    shortcut.TargetPath = Environment.ProcessPath;
                    shortcut.WorkingDirectory = Environment.CurrentDirectory;
                    shortcut.WindowStyle = 1;
                    shortcut.Description = exeName + "_Ink";
                    shortcut.Save();
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }

        public static bool StartAutomaticallyDel(string exeName)
        {
            try
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + exeName + ".lnk");
                return true;
            }
            catch (Exception e) { LogHelper.WriteLogToFile(e.ToString(), LogHelper.LogType.Error); }
            return false;
        }

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

        public static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.General)
            {
                ApplySystemTheme(RuntimeData.SettingModel.Appearance.Theme);
            }
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

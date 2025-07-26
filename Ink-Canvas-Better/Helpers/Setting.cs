using Ink_Canvas_Better.Resources;
using IWshRuntimeLibrary;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = System.IO.File;

namespace Ink_Canvas_Better.Helpers
{
    internal class Setting
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
                        RuntimeData.settingData = JsonConvert.DeserializeObject<SettingData>(text);
                    }
                    catch { }
                }
                else
                {
                    //BtnResetToSuggestion_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.ToString(), Log.LogType.Error);
            }

            if (isStartup)
            {
                RuntimeData.mainWindow.CursorIcon_Click(null, null);
            }
            try
            {
                if (RuntimeData.settingData.StartupAndUpdate.IsAutoStartup)
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
                Log.WriteLogToFile(ex.ToString(), Log.LogType.Error);
            }

        }

        public static void SaveSettings()
        {
            string text = JsonConvert.SerializeObject(RuntimeData.settingData, Formatting.Indented);
            try
            {
                File.WriteAllText(App.RootPath + RuntimeData.settingsFileName, text);
            }
            catch { }
        }

        public static bool StartAutomaticallyCreate(string exeName)
        {
            try
            {
                String f = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + exeName + ".lnk";
                if (File.Exists(f))
                {
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(f);
                    shortcut.TargetPath = Process.GetCurrentProcess().MainModule.FileName;
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
            catch (Exception e) { Log.WriteLogToFile(e.ToString(), Log.LogType.Error); }
            return false;
        }
    }
}

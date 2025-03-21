using Ink_Canvas.Properties;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Ink_Canvas.Helpers;
using Ink_Canvas.Windows;

namespace Ink_Canvas.Windows.Controls
{
    /// <summary>
    /// ShortcutSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ShortcutSetting : Grid
    {
        int index;
        public ShortcutSetting(int index, bool enable, String Name, String URL)
        {
            this.index = index;
            /*
            this.Toggle_shortcut_0.IsOn = enable;
            this.NameTextbox_Shortcut.Text = Name;
            this.URLTextbox_Shortcut.Text= URL;
            */
            InitializeComponent();
        }

        /// <summary>
        /// "删除"按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shortcut_Button_Delete(object sender, RoutedEventArgs e)
        {
            MainWindow.Settings.Shortcut.ShortcutEnable.RemoveAt(index);
            MainWindow.Settings.Shortcut.ShortcutName.RemoveAt(index);
            MainWindow.Settings.Shortcut.ShortcutUrls.RemoveAt(index);

            MainWindow.SaveSettingsToFile();
        }

        /// <summary>
        /// 启用或关闭快捷方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitchShortcutEnable_Toggled(object sender, RoutedEventArgs e)
        {
            if (!MainWindow.isLoaded) return;
            MainWindow.Settings.Shortcut.ShortcutEnable[index] = this.Toggle_shortcut_0.IsOn;
            MainWindow.SaveSettingsToFile();
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameTextbox_SourceUpdated_0(object sender, DataTransferEventArgs e)
        {
            if (!MainWindow.isLoaded) return;
            MainWindow.Settings.Shortcut.ShortcutName[index] = this.NameTextbox_Shortcut.Text;
            MainWindow.SaveSettingsToFile();
        }

        /// <summary>
        /// URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URLTextbox_SourceUpdated_0(object sender, DataTransferEventArgs e)
        {
            if (!MainWindow.isLoaded) return;
            MainWindow.Settings.Shortcut.ShortcutUrls[index] = this.URLTextbox_Shortcut.Text;
            MainWindow.SaveSettingsToFile();
        }
    }
}

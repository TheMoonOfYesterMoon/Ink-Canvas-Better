using Ink_Canvas_Better.Resources;
using Ink_Canvas_Better.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Pages.SettingPages
{
    /// <summary>
    /// Appearance.xaml 的交互逻辑
    /// </summary>
    public partial class Appearance : Page
    {
        public Appearance()
        {
            InitializeComponent();
            this.Loaded += Appearance_Loaded;
        }

        private void Appearance_Loaded(object sender, RoutedEventArgs e)
        {
            switch (RuntimeData.settingData.Appearance.Theme)
            {
                case null:
                    Theme_ComboBox.SelectedIndex = 0;
                    break;
                case true:
                    Theme_ComboBox.SelectedIndex = 1;
                    break;
                case false:
                    Theme_ComboBox.SelectedIndex = 2;
                    break;
            }
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Theme_ComboBox.SelectedIndex)
            {
                case 0:
                    RuntimeData.settingData.Appearance.Theme = null;
                    Setting.ApplySystemTheme(null);
                    break;
                case 1:
                    RuntimeData.settingData.Appearance.Theme = true;
                    Setting.ApplySystemTheme(true);
                    break;
                case 2:
                    RuntimeData.settingData.Appearance.Theme = false;
                    Setting.ApplySystemTheme(false);
                    break;
            }
            Setting.SaveSettings();
        }
    }
}

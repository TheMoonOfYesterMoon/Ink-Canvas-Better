using Ink_Canvas.Helpers;
using Ink_Canvas.Windows;
using iNKORE.UI.WPF.Controls;
using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using File = System.IO.File;
using MessageBox = System.Windows.MessageBox;

namespace Ink_Canvas
{
    public partial class MainWindow : Window
    {

        #region Window Initialization

        Magnify MagnifyWindow;
        public MainWindow()
        {
            /*
                处于画板模式内：Topmost == false / currentMode != 0
                处于 PPT 放映内：BtnPPTSlideShowEnd.Visibility
            */
            InitializeComponent();

            HideAllSetting();

            BlackboardLeftSide.Visibility = Visibility.Collapsed;
            BlackboardCenterSide.Visibility = Visibility.Collapsed;
            BlackboardRightSide.Visibility = Visibility.Collapsed;

            BorderTools.Visibility = Visibility.Collapsed;
            BorderSettings.Visibility = Visibility.Collapsed;

            BtnPPTSlideShowEnd.Visibility = Visibility.Collapsed;
            PPTNavigationBottomLeft.Visibility = Visibility.Collapsed;
            PPTNavigationBottomRight.Visibility = Visibility.Collapsed;
            PPTNavigationSidesLeft.Visibility = Visibility.Collapsed;
            PPTNavigationSidesRight.Visibility = Visibility.Collapsed;

            BorderSettings.Margin = new Thickness(0, 150, 0, 150);

            TwoFingerGestureBorder.Visibility = Visibility.Collapsed;
            BoardTwoFingerGestureBorder.Visibility = Visibility.Collapsed;
            BorderDrawShape.Visibility = Visibility.Collapsed;
            BoardBorderDrawShape.Visibility = Visibility.Collapsed;

            GridInkCanvasSelectionCover.Visibility = Visibility.Collapsed;

            ViewboxFloatingBar.Margin = new Thickness((SystemParameters.WorkArea.Width - 284) / 2, SystemParameters.WorkArea.Height - 60, -2000, -200);
            ViewboxFloatingBarMarginAnimation(100);

            try
            {
                if (File.Exists("Log.txt"))
                {
                    FileInfo fileInfo = new FileInfo("Log.txt");
                    long fileSizeInKB = fileInfo.Length / 1024;
                    if (fileSizeInKB > 512)
                    {
                        try
                        {
                            File.Delete("Log.txt");
                            LogHelper.WriteLogToFile("The Log.txt file has been successfully deleted. Original file size: " + fileSizeInKB + " KB", LogHelper.LogType.Info);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLogToFile(ex + " | Can not delete the Log.txt file. File size: " + fileSizeInKB + " KB", LogHelper.LogType.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile(ex.ToString(), LogHelper.LogType.Error);
            }

            InitTimers();
            timeMachine.OnRedoStateChanged += TimeMachine_OnRedoStateChanged;
            timeMachine.OnUndoStateChanged += TimeMachine_OnUndoStateChanged;
            inkCanvas.Strokes.StrokesChanged += StrokesOnStrokesChanged;

            Microsoft.Win32.SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            try
            {
                if (File.Exists("SpecialVersion.ini")) SpecialVersionResetToSuggestion_Click();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile(ex.ToString(), LogHelper.LogType.Error);
            }

            CheckColorTheme(true);
        }

        #endregion

        #region Ink Canvas Functions

        DrawingAttributes drawingAttributes;
        private void loadPenCanvas()
        {
            try
            {
                drawingAttributes = inkCanvas.DefaultDrawingAttributes;
                drawingAttributes.Color = Colors.Red;

                drawingAttributes.Height = 2.5;
                drawingAttributes.Width = 2.5;

                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                inkCanvas.Gesture += InkCanvas_Gesture;
            }
            catch { }
        }

        private void InkCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            ReadOnlyCollection<GestureRecognitionResult> gestures = e.GetGestureRecognitionResults();
            try
            {
                foreach (GestureRecognitionResult gest in gestures)
                {
                    if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible)
                    {
                        if (gest.ApplicationGesture == ApplicationGesture.Left)
                        {
                            BtnPPTSlidesDown_Click(null, null);
                        }
                        if (gest.ApplicationGesture == ApplicationGesture.Right)
                        {
                            BtnPPTSlidesUp_Click(null, null);
                        }
                    }
                }
            }
            catch { }
        }

        private void inkCanvas_EditingModeChanged(object sender, RoutedEventArgs e)
        {
            var inkCanvas1 = sender as InkCanvas;
            if (inkCanvas1 == null) return;
            if (Settings.Canvas.IsShowCursor)
            {
                if (inkCanvas1.EditingMode == InkCanvasEditingMode.Ink || drawingShapeMode != 0)
                {
                    inkCanvas1.ForceCursor = true;
                }
                else
                {
                    inkCanvas1.ForceCursor = false;
                }
            }
            else
            {
                inkCanvas1.ForceCursor = false;
            }
            if (inkCanvas1.EditingMode == InkCanvasEditingMode.Ink) forcePointEraser = !forcePointEraser;
        }

        #endregion Ink Canvas Functions

        #region Definations and Loading

        public static Settings Settings = new Settings();
        public static string settingsFileName = "Settings.json";
        bool isLoaded = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadPenCanvas();
            //加载设置
            LoadSettings(true);
            if (Environment.Is64BitProcess)
            {
                GroupBoxInkRecognition.Visibility = Visibility.Collapsed;
            }

            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            SystemEvents_UserPreferenceChanged(null, null);

            // 显示“测试版”字样
            String[] Version = Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
            AppVersionTextBlock.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (int.TryParse(Version[3],out int i))
            {
                if (i > 0)
                {
                    AppVersionTextBlock.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - 测试版";
                }
            }

            LogHelper.WriteLogToFile("Ink Canvas Loaded", LogHelper.LogType.Event);
            isLoaded = true;

            RegisterGlobalHotkeys();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LogHelper.WriteLogToFile("Ink Canvas closing", LogHelper.LogType.Event);
            if (!CloseIsFromButton && Settings.Advanced.IsSecondConfimeWhenShutdownApp)
            {
                e.Cancel = true;
                if (MessageBox.Show("是否继续关闭 Ink Canvas 画板，这将丢失当前未保存的工作。", "Ink Canvas 画板", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    if (MessageBox.Show("真的狠心关闭 Ink Canvas 画板吗？", "Ink Canvas 画板", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.OK)
                    {
                        if (MessageBox.Show("是否取消关闭 Ink Canvas 画板？", "Ink Canvas 画板", MessageBoxButton.OKCancel, MessageBoxImage.Error) != MessageBoxResult.OK)
                        {
                            e.Cancel = false;
                        }
                    }
                }
            }
            if (e.Cancel)
            {
                LogHelper.WriteLogToFile("Ink Canvas closing cancelled", LogHelper.LogType.Event);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LogHelper.WriteLogToFile("Ink Canvas closed", LogHelper.LogType.Event);
        }

        #endregion Definations and Loading

        #region Setting

        private void HideAllSetting()
        {
            Setting_General1.Visibility = Visibility.Collapsed;
            Setting_General2.Visibility = Visibility.Collapsed;
            Setting_StartAndUpgrade.Visibility = Visibility.Collapsed;
            GroupBoxAppearanceNewUI.Visibility = Visibility.Collapsed;
            Setting_CanvasAndGesture1.Visibility = Visibility.Collapsed;
            GroupBoxInkRecognition.Visibility = Visibility.Collapsed;
            Setting_CanvasAndGesture2.Visibility = Visibility.Collapsed;
            Setting_PPT.Visibility = Visibility.Collapsed;
            Setting_Advance.Visibility = Visibility.Collapsed;
            Setting_Others.Visibility = Visibility.Collapsed;
            Setting_Shortcut.Visibility = Visibility.Collapsed;
        }

        private void BtnSettingGeneral_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_General1.Visibility = Visibility.Visible;
            Setting_General2.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 基本";
        }

        private void BtnSettingStartAndUpgrade_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_StartAndUpgrade.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 启动与更新";
        }

        private void BtnSettingAppearance_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            GroupBoxAppearanceNewUI.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 外观";
        }

        private void BtnSettingCanvasAndGesture_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_CanvasAndGesture1.Visibility = Visibility.Visible;
            GroupBoxInkRecognition.Visibility = Visibility.Visible;
            Setting_CanvasAndGesture2.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 画板与手势";
        }

        private void BtnSettingPPT_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_PPT.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - PPT";
        }

        private void BtnSettingAdvance_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_Advance.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 高级";
        }

        private void BtnSettingShortcut_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_Shortcut.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 快捷启动";
        }
        private void BtnSettingOthers_Click(object sender, RoutedEventArgs e)
        {
            HideAllSetting();
            Setting_Others.Visibility = Visibility.Visible;
            Setting_CurrentOption.Text = "详细设置 - 其它";
        }

        #endregion

        #region Shortcut
        private void BtnNewShortcut_Click(object sender, RoutedEventArgs e)
        {
            // 这里新建一个Shortcut类
        }

        #endregion
        private void WindowDragMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) this.DragMove();
        }

    }

    public partial class Shortcut
    {
        #region defination
        /// <summary>
        /// 总外框
        /// </summary>
        iNKORE.UI.WPF.Controls.SimpleStackPanel simpleStackPanel_1;
        /// <summary>
        /// 按钮 -> 删除
        /// </summary>
        Button btnRemoveShortcut;
        /// <summary>
        /// 删除按钮的图标
        /// </summary>
        iNKORE.UI.WPF.Modern.Controls.FontIcon btnRemoveShortcutIcon;
        /// <summary>
        /// 开关外框
        /// </summary>
        iNKORE.UI.WPF.Controls.SimpleStackPanel simpleStackPanel_2;
        /// <summary>
        /// 开关
        /// </summary>
        iNKORE.UI.WPF.Modern.Controls.ToggleSwitch toggleSwitch;
        /// <summary>
        /// 显示的名称
        /// </summary>
        TextBox NameTextBox;
        /// <summary>
        /// URL
        /// </summary>
        TextBox URLTextBox;
        #endregion

        public Shortcut()
        {
            // 基本设置
            // 总外框
            simpleStackPanel_1 = new SimpleStackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // 按钮删除
            btnRemoveShortcut = new Button
            {
                BorderThickness = new Thickness(0, 0, 0, 0),
                Height = 28,
                Margin = new Thickness(0, 0, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Padding = new Thickness(0, 0, 0, 0),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00000000")),
            };

            // 删除按钮的图标
            btnRemoveShortcutIcon.SetResourceReference(Control.ForegroundProperty, "SettingsPageForeground");
            btnRemoveShortcutIcon = new FontIcon
            {
                Width = 18,
                Height = 18,
                FontFamily = (FontFamily)Application.Current.Resources["FluentIconFontFamily"], // 静态资源获取
                FontSize = 18,
                Glyph = "\uf16d"
            };

            // 开关的外框
            simpleStackPanel_2 = new SimpleStackPanel
            {
                Width = 50
            };

            // 开关
            toggleSwitch = new ToggleSwitch
            {
                Name = "Toggle_shortcut_0",
                IsOn = false,
                OffContent = "关",
                OnContent = "开",
            };
            toggleSwitch.Toggled += ToggleSwitch_Toggled();

            // 显示的名称
            NameTextBox = new TextBox
            {
                Margin = new Thickness(10, 0, 10, 10),
                Text = "显示的名称"
            };
            NameTextBox.SourceUpdated += NameTextBox_SourceUpdated();

            // URL
            URLTextBox = new TextBox
            {
                Margin = new Thickness(10, 0, 10, 10),
                Text = "修改为文件、程序或网页的URL",
                MaxWidth = 100
            };
            URLTextBox.SourceUpdated += URLTextBox_SourceUpdated();

            // 塞在一起
            btnRemoveShortcut.Content = btnRemoveShortcutIcon;
            simpleStackPanel_2.Children.Add(toggleSwitch);
            simpleStackPanel_1.Children.Add(btnRemoveShortcut);
            simpleStackPanel_1.Children.Add(simpleStackPanel_2);
            simpleStackPanel_1.Children.Add(NameTextBox);
            simpleStackPanel_1.Children.Add(URLTextBox);


        }

        /// <summary>
        /// 开关
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private RoutedEventHandler ToggleSwitch_Toggled()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 填写显示名称的文本框
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private EventHandler<DataTransferEventArgs> NameTextBox_SourceUpdated()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 填写URL的文本框
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private EventHandler<DataTransferEventArgs> URLTextBox_SourceUpdated()
        {
            throw new NotImplementedException();
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ink_Canvas_Better.Enums;

namespace Ink_Canvas_Better.Model
{
    internal class SettingsModel
    {
        public class SettingModel
        {
            [JsonProperty("startupAndUpdate")]
            public StartupAndUpdate StartupAndUpdate { get; set; } = new StartupAndUpdate();

            [JsonProperty("appearance")]
            public Appearance Appearance { get; set; } = new Appearance();

            [JsonProperty("ppt")]
            public PPT PPT { get; set; } = new PPT();

            [JsonProperty("experimentalFeatures")]
            public ExperimentalFeatures ExperimentalFeatures { get; set; } = new ExperimentalFeatures();

            [JsonProperty("others")]
            public Others Others { get; set; } = new Others();

            [JsonProperty("runtime")]
            public Runtime Runtime { get; set; } = new Runtime();
        }

        /// <summary>
        /// Startup and update
        /// </summary>
        public class StartupAndUpdate : INotifyPropertyChanged
        {
            #region IsAutoUpdate
            [JsonProperty("isAutoUpdate")]
            private bool _IsAutoUpdate = true;
            public bool IsAutoUpdate
            {
                get { return _IsAutoUpdate; }
                set
                {
                    if (IsAutoUpdate != value)
                    {
                        _IsAutoUpdate = value;
                        OnPropertyChanged(nameof(IsAutoUpdate));
                    }
                }
            }
            #endregion
            #region IsAutoUpdateWithProxy
            [JsonProperty("isAutoUpdateWithProxy")]
            private bool _IsAutoUpdateWithProxy = false;
            public bool IsAutoUpdateWithProxy
            {
                get { return _IsAutoUpdateWithProxy; }
                set
                {
                    if (IsAutoUpdateWithProxy != value)
                    {
                        _IsAutoUpdateWithProxy = value;
                        OnPropertyChanged(nameof(IsAutoUpdateWithProxy));
                    }
                }
            }
            #endregion
            #region AutoUpdateProxy
            [JsonProperty("autoUpdateProxy")]
            private string _AutoUpdateProxy = "https://mirror.ghproxy.com/";
            public string AutoUpdateProxy
            {
                get { return _AutoUpdateProxy; }
                set
                {
                    if (AutoUpdateProxy != value)
                    {
                        _AutoUpdateProxy = value;
                        OnPropertyChanged(nameof(AutoUpdateProxy));
                    }
                }
            }
            #endregion
            #region IsAutoUpdateSilently
            [JsonProperty("isAutoUpdateSilently")]
            private bool _IsAutoUpdateSilently = false;
            public bool IsAutoUpdateSilently
            {
                get { return _IsAutoUpdateSilently; }
                set
                {
                    if (IsAutoUpdateSilently != value)
                    {
                        _IsAutoUpdateSilently = value;
                        OnPropertyChanged(nameof(IsAutoUpdateSilently));
                    }
                }
            }
            #endregion
            #region AutoUpdateSilentlyStartTime
            [JsonProperty("autoUpdateSilentlyStartTime")]
            private string _AutoUpdateSilentlyStartTime = "00:00";
            public string AutoUpdateSilentlyStartTime
            {
                get { return _AutoUpdateSilentlyStartTime; }
                set
                {
                    if (AutoUpdateSilentlyStartTime != value)
                    {
                        _AutoUpdateSilentlyStartTime = value;
                        OnPropertyChanged(nameof(AutoUpdateSilentlyStartTime));
                    }
                }
            }
            #endregion
            #region AutoUpdateSilentlyEndTime
            [JsonProperty("autoUpdateSilentlyEndTime")]
            private string _AutoUpdateSilentlyEndTime = "00:00";
            public string AutoUpdateSilentlyEndTime
            {
                get { return _AutoUpdateSilentlyEndTime; }
                set
                {
                    if (AutoUpdateSilentlyEndTime != value)
                    {
                        _AutoUpdateSilentlyEndTime = value;
                        OnPropertyChanged(nameof(AutoUpdateSilentlyEndTime));
                    }
                }
            }
            #endregion
            #region IsFoldAtStartup
            [JsonProperty("isFoldAtStartup")]
            private bool _IsFoldAtStartup = false;
            public bool IsFoldAtStartup
            {
                get { return _IsFoldAtStartup; }
                set
                {
                    if (IsFoldAtStartup != value)
                    {
                        _IsFoldAtStartup = value;
                        OnPropertyChanged(nameof(IsFoldAtStartup));
                    }
                }
            }
            #endregion
            #region IsAutoStartup
            [JsonProperty("isAutoStartup")]
            private bool _IsAutoStartup = true;
            public bool IsAutoStartup
            {
                get { return _IsAutoStartup; }
                set
                {
                    if (IsAutoStartup != value)
                    {
                        _IsAutoStartup = value;
                        OnPropertyChanged(nameof(IsAutoStartup));
                    }
                }
            }
            #endregion

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class Appearance : INotifyPropertyChanged
        {
            #region IsShowFloatingBarText
            [JsonProperty("isShowFloatingBarText")]
            private bool _IsShowFloatingBarText = false;
            public bool IsShowFloatingBarText
            {
                get { return _IsShowFloatingBarText; }
                set
                {
                    if (IsShowFloatingBarText != value)
                    {
                        _IsShowFloatingBarText = value;
                        OnPropertyChanged(nameof(IsShowFloatingBarText));
                    }
                }
            }
            #endregion
            #region FloatingBarScaling
            [JsonProperty("floatingBarScaling")]
            private float _FloatingBarScaling = 1.0f;
            public float FloatingBarScaling
            {
                get { return _FloatingBarScaling; }
                set
                {
                    if (FloatingBarScaling != value)
                    {
                        _FloatingBarScaling = value;
                        OnPropertyChanged(nameof(FloatingBarScaling));
                    }
                }
            }
            #endregion
            #region IsShowModeFingerToggleSwitch
            [JsonProperty("isShowModeFingerToggleSwitch")]
            private bool _IsShowModeFingerToggleSwitch = true;
            public bool IsShowModeFingerToggleSwitch
            {
                get { return _IsShowModeFingerToggleSwitch; }
                set
                {
                    if (IsShowModeFingerToggleSwitch != value)
                    {
                        _IsShowModeFingerToggleSwitch = value;
                        OnPropertyChanged(nameof(IsShowModeFingerToggleSwitch));
                    }
                }
            }
            #endregion
            #region Theme
            [JsonProperty("theme")]
            private bool? _Theme = null;
            public bool? Theme
            {
                get { return _Theme; }
                set
                {
                    if (Theme != value)
                    {
                        _Theme = value;
                        OnPropertyChanged(nameof(Theme));
                    }
                }
            }
            #endregion

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// PPT
        /// </summary>
        public class PPT : INotifyPropertyChanged
        {

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Experimental features
        /// </summary>
        public class ExperimentalFeatures : INotifyPropertyChanged
        {

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class Others : INotifyPropertyChanged
        {
            #region IsShowWelcomeWindow
            [JsonProperty("isShowWelcomeWindow")]
            private bool _IsShowWelcomeWindow = true;
            public bool IsShowWelcomeWindow
            {
                get { return _IsShowWelcomeWindow; }
                set
                {
                    if (IsShowWelcomeWindow != value)
                    {
                        _IsShowWelcomeWindow = value;
                        OnPropertyChanged(nameof(IsShowWelcomeWindow));
                    }
                }
            }
            #endregion
            #region IsShowLanguageWindow
            [JsonProperty("isShowLanguageWindow")]
            private bool _IsShowLanguageWindow = true;
            public bool IsShowLanguageWindow
            {
                get { return _IsShowLanguageWindow; }
                set
                {
                    if (IsShowLanguageWindow != value)
                    {
                        _IsShowLanguageWindow = value;
                        OnPropertyChanged(nameof(IsShowLanguageWindow));
                    }
                }
            }
            #endregion
            #region Language
            [JsonProperty("language")]
            private string _Language = "en";
            public string Language
            {
                get { return _Language; }
                set
                {
                    if (Language != value)
                    {
                        _Language = value;
                        OnPropertyChanged(nameof(Language));
                    }
                }
            }
            #endregion

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class Runtime : INotifyPropertyChanged
        {
            #region InkStyle
            [JsonProperty("inkStyle")]
            private InkCanvasEnums.InkStyle _InkStyle = InkCanvasEnums.InkStyle.Default;
            public InkCanvasEnums.InkStyle InkStyle
            {
                get { return _InkStyle; }
                set
                {
                    if (InkStyle != value)
                    {
                        _InkStyle = value;
                        OnPropertyChanged(nameof(InkStyle));
                    }
                }
            }
            #endregion
            #region BluntnessFactor
            [JsonProperty("bluntnessFactor")]
            private float _BluntnessFactor = 49f / 50f;// influent writing style
            public float BluntnessFactor
            {
                get { return _BluntnessFactor; }
                set
                {
                    if (BluntnessFactor != value)
                    {
                        _BluntnessFactor = value;
                        OnPropertyChanged(nameof(BluntnessFactor));
                    }
                }
            }
            #endregion

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

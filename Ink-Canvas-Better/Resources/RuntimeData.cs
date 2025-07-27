using Ink_Canvas_Better.Windows;
using Ink_Canvas_Better.Windows.FloatingBarIcons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;

namespace Ink_Canvas_Better.Resources
{
    static class RuntimeData
    {
        public static bool isCloseFromButton = false;
        public static Mode currentMode = Mode.None;
        public static SettingWindow settingWindow;
        public static MainWindow mainWindow;
        public static FloatingBar_Pen floatingBar_Pen;
        public static SettingData settingData = new SettingData();
        public static String settingsFileName = "settings.json";

        public static DrawingAttributes DrawingAttributes { get; set; } = new DrawingAttributes();

        public enum Mode
        {
            None,
            Pen,
            Highlighter,
            Eraser
        }

    }
}

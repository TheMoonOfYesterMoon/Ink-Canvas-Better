using Ink_Canvas_Better.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;
using static Ink_Canvas_Better.MainWindow;

namespace Ink_Canvas_Better.Resources
{
    static class RuntimeData
    {
        public static bool CloseIsFromButton = false;
        public static Mode CurrentMode = Mode.None;
        public static SettingWindow settingWindow;
        
        /// <summary>
        /// influent writing style
        /// </summary>
        public static float bluntnessFactor = (49f/50f);

        public static InkStyle CurrentInkStyle = InkStyle.Default;

        public static DrawingAttributes DrawingAttributes { get; set; } = new DrawingAttributes();

        public enum Mode
        {
            None,
            Pen,
            Highlighter,
            Eraser
        }

        public enum InkStyle
        {
            Default,
            Simulative
        }

    }
}

using Ink_Canvas_Better.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using static Ink_Canvas_Better.MainWindow;

namespace Ink_Canvas_Better.Resources
{
    static class RuntimeData
    {
        public static bool CloseIsFromButton = false;
        static readonly Mode CurrentMode = Mode.None;
        static readonly SettingWindow settingWindow;

        public static DrawingAttributes DrawingAttributes { get; set; } = new DrawingAttributes();

        public enum Mode
        {
            None,
            Pen,
            Highlighter,
            Eraser
        }

        public enum NibSimulationMode
        {
            
        }

    }
}

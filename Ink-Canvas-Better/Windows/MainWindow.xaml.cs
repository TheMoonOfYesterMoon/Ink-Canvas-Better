using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Windows;
using Ink_Canvas_Better.Windows.FloatingBarIcons;

namespace Ink_Canvas_Better
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Mode CurrentMode = Mode.None;
        private SettingWindow settingWindow;

        public enum Mode
        {
            None,
            Pen,
            Highlighter,
            Eraser
        }

        public static DrawingAttributes DrawingAttributes { get; set; } = new DrawingAttributes();

        #region Initialize

        readonly Magnifier MagnifierWindow = new Magnifier();

        /// <summary>
        /// Initialize MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            inkCanvas.DefaultDrawingAttributes = DrawingAttributes;

            DrawingAttributes.FitToCurve = true;
        }

        #endregion
    }
}

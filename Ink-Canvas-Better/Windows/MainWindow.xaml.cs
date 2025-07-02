using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ink_Canvas_Better.Windows;
using Ink_Canvas_Better.Helpers;

namespace Ink_Canvas_Better
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Mode CurrentMode = Mode.None;

        public enum Mode
        {
            None,
            Pen,
            Highlighter,
            Eraser
        }

        #region init

        readonly Magnifier MagnifierWindow = new Magnifier();

        /// <summary>
        /// initialize MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            CursorIcon_ChangeToCursor();
        }

        #endregion
    }
}

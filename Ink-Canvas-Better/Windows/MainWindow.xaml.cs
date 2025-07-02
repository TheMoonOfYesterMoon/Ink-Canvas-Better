using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Ink_Canvas_Better
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Mode CurrentMode = Mode.None;
        DrawingAttributes drawingAttributes;

        public enum Mode
        {
            None,
            Pen,
            Highlighter,
            Eraser
        }

        #region Initialize

        readonly Magnifier MagnifierWindow = new Magnifier();

        /// <summary>
        /// Initialize MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            drawingAttributes = new DrawingAttributes();
            inkCanvas.DefaultDrawingAttributes = drawingAttributes;

            drawingAttributes.FitToCurve = true;

        }

        #endregion
    }
}

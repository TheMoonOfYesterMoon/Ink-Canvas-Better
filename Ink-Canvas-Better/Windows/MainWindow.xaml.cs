using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Helpers.Others;
using Ink_Canvas_Better.Resources;
using Ink_Canvas_Better.Windows;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Ink_Canvas_Better
{
    public partial class MainWindow : Window
    {
        #region Initialize

        readonly Magnifier MagnifierWindow = new Magnifier();

        /// <summary>
        /// Initialize MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Setting.LoadSettings(isStartup : true);
            RuntimeData.mainWindow = this;

            MainInkCanvas.AddHandler(MouseDownEvent, new MouseButtonEventHandler(MainInkCanvas_MouseDown), true);
            MainInkCanvas.AddHandler(MouseDownEvent, new MouseButtonEventHandler(MainInkCanvas_MouseMove), true);
            MainInkCanvas.AddHandler(MouseDownEvent, new MouseButtonEventHandler(MainInkCanvas_MouseUp), true);
            this.SourceInitialized += Win32Helper.MainWindow_SourceInitialized;
            this.Loaded += DockWindowToBottom;

            CursorIcon_Click(null, null);
        }

        #endregion

        private bool _isHideDrawingTools = false;
        public bool IsHideDrawingTools
        {
            get { return _isHideDrawingTools; }
            set
            {
                _isHideDrawingTools = value;
                if (_isHideDrawingTools) HideDrawingTools(); else ShowDrawingTools();
            }
        }

        private void MainInkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainInkCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void MainInkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

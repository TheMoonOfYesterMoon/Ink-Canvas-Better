using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Resources;
using Ink_Canvas_Better.Windows;
using Ink_Canvas_Better.Windows.FloatingBarIcons;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace Ink_Canvas_Better
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
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

            inkCanvas.DefaultDrawingAttributes = RuntimeData.DrawingAttributes;
            RuntimeData.DrawingAttributes.FitToCurve = true;

            this.Loaded += DockWindowToBottom;
        }

        #endregion

    }
}

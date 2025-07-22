using Ink_Canvas_Better.Resources;
using Ink_Canvas_Better.Windows;
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

            inkCanvas.DefaultDrawingAttributes = RuntimeData.DrawingAttributes;
            RuntimeData.DrawingAttributes.FitToCurve = true;

            this.Loaded += DockWindowToBottom;
        }

        #endregion

        public void ICB_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

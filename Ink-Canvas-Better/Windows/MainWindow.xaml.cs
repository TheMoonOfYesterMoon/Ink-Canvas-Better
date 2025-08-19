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
            
            this.SourceInitialized += Win32Helper.MainWindow_SourceInitialized;
            this.Loaded += DockWindowToBottom;
        }

        #endregion

        private void inkCanvas_TouchDown(object sender, TouchEventArgs e)
        {

        }

        private void inkCanvas_TouchMove(object sender, TouchEventArgs e)
        {

        }

        private void inkCanvas_TouchUp(object sender, TouchEventArgs e)
        {

        }
    }
}

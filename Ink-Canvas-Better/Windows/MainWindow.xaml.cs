using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ink_Canvas_Better
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Mode CurrentMode = Mode.Cursor;

        enum Mode
        {
            Cursor,
            Pen,
            Highlighter,
            Eraser
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}

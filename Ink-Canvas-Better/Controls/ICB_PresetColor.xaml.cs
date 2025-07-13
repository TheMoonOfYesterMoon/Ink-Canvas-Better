using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// ICB_PresetColor.xaml 的交互逻辑
    /// </summary>
    public partial class ICB_PresetColor : UserControl
    {
        public ICB_PresetColor()
        {
            InitializeComponent();
        }

        #region Properties

        #region Color

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color", 
                typeof(Color), 
                typeof(ICB_PresetColor), 
                new PropertyMetadata(Color_OnValueChanged)
            );

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void Color_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_PresetColor)dependencyObject;
            control.InnerButton.Background = new SolidColorBrush((Color)eventArgs.NewValue);
        }

        #endregion

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.DrawingAttributes.Color = ((SolidColorBrush)InnerButton.Background).Color;
        }
    }
}

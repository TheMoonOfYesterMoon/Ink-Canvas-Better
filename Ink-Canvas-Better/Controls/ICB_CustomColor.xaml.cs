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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// ICB_CustomColor.xaml 的交互逻辑
    /// </summary>
    public partial class ICB_CustomColor : UserControl
    {
        public ICB_CustomColor()
        {
            InitializeComponent();
        }

        #region Properties

        #region Color

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                "Color",
                typeof(Color),
                typeof(ICB_CustomColor),
                new PropertyMetadata(Color_OnValueChanged)
            );

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void Color_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_CustomColor)dependencyObject;
            control.InnerBorder.Background = new SolidColorBrush((Color)eventArgs.NewValue);
        }

        #endregion

        #endregion

        #region Events

        public static readonly RoutedEvent ColorSelectedEvent =
            EventManager.RegisterRoutedEvent(
                "ColorSelected",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ICB_CustomColor));

        public event RoutedEventHandler ColorSelected
        {
            add => AddHandler(ColorSelectedEvent, value);
            remove => RemoveHandler(ColorSelectedEvent, value);
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.DrawingAttributes.Color = ((SolidColorBrush)InnerBorder.Background).Color;

            var args = new RoutedEventArgs(ColorSelectedEvent, this);
            RaiseEvent(args);
            e.Handled = true;
        }
    }
}

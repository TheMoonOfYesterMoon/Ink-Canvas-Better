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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// SubPanel.xaml 的交互逻辑
    /// </summary>
    [ContentProperty("Children")]
    public partial class ICB_SubPanel : UserControl
    {
        public ICB_SubPanel()
        {
            InitializeComponent();
        }

        #region Properties

        #region Property_Title

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(ICB_SubPanel),
                new PropertyMetadata("Title")
            );

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        #endregion

        #region Property_IsShowPinButton

        /// <summary>
        /// When set to true, the subpanel will display a pin button.
        /// </summary>
        public static readonly DependencyProperty IsShowPinButtonProperty =
            DependencyProperty.Register(
                "IsShowPinButton",
                typeof(bool),
                typeof(ICB_SubPanel),
                new PropertyMetadata(false, IsShowPinButton_OnValueChanged)
            );

        public bool IsShowPinButton
        {
            get => (bool)GetValue(IsShowPinButtonProperty);
            set => SetValue(IsShowPinButtonProperty, value);
        }

        private static void IsShowPinButton_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (ICB_SubPanel)dependencyObject;
            if ((bool)eventArgs.NewValue)
            {
                control.PinButton.Visibility = Visibility.Visible;
            }
            else
            {
                control.PinButton.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        #endregion

        private readonly StackPanel contentPanel = new StackPanel();

        public UIElementCollection Children => contentPanel.Children;


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement pinning functionality
        }
    }
}


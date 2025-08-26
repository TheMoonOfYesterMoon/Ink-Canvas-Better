using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICBCustomControlLibrary.Controls.Panel
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ICBCustomControlLibrary.Controls.Panel"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ICBCustomControlLibrary.Controls.Panel;assembly=ICBCustomControlLibrary.Controls.Panel"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:Subpanel/>
    ///
    /// </summary>
    [ContentProperty("Content")]
    [TemplatePart(Name = "PART_PinButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class Subpanel : ContentControl
    {
        static Subpanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Subpanel), new FrameworkPropertyMetadata(typeof(Subpanel)));
        }

        #region Properties

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Subpanel), new PropertyMetadata("Title"));

        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Subpanel), new PropertyMetadata(false));

        public bool IsOpen { get => (bool)GetValue(IsOpenProperty); set => SetValue(IsOpenProperty, value); }

        public static readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register("StaysOpen", typeof(bool), typeof(Subpanel), new PropertyMetadata(false));

        public bool StaysOpen { get => (bool)GetValue(StaysOpenProperty); set => SetValue(StaysOpenProperty, value); }

        public static readonly DependencyProperty PlacementTargetProperty =
            DependencyProperty.Register("PlacementTarget", typeof(UIElement), typeof(Subpanel), new PropertyMetadata(null));

        public UIElement PlacementTarget { get => (UIElement)GetValue(PlacementTargetProperty); set => SetValue(PlacementTargetProperty, value); }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_PinButton") is Button pinButton)
            {
                pinButton.Click += PinButton_Click;
            }
            if (GetTemplateChild("PART_CloseButton") is Button closeButton)
            {
                closeButton.Click += CloseButton_Click;
            }
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            StaysOpen = !StaysOpen;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }
    }

}

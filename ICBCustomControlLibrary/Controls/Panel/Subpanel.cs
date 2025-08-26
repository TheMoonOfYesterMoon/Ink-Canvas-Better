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
            if (GetTemplateChild("PART_PinTextblock") is TextBlock textBlock)
            {
                textBlock.Text = StaysOpen ? "\ue77a" : "\ue840";
            }

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }
    }

}

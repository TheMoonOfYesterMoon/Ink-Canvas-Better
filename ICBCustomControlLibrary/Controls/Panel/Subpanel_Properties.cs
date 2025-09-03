using ICBCustomControlLibrary.Helpers;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ICBCustomControlLibrary.Controls.Panel
{
    public partial class Subpanel
    {
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == StaysOpenProperty)
            {
                _pinButton.Content = (bool)e.NewValue ? "\ue77a" : "\ue718";
            }
        }

        #region Title

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Subpanel), new PropertyMetadata("Title", OnTitleChanged));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                var control = d as Subpanel;
                control._titleTextBlock.Text = e.NewValue as string;
            }
        }

        #endregion

        #region IsShowHeader

        public static readonly DependencyProperty IsShowHeaderProperty =
            DependencyProperty.Register("ShowHeader", typeof(bool), typeof(Subpanel), new PropertyMetadata(true, OnIsShowHeaderChanged));

        public bool IsShowHeader
        {
            get { return (bool)GetValue(IsShowHeaderProperty); }
            set { SetValue(IsShowHeaderProperty, value); }
        }

        private static void OnIsShowHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                var control = d as Subpanel;
                control._titleBarGrid.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion

        #region HeaderBackground

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(Subpanel), new PropertyMetadata(ThemeHelper.DefaultBackgroundColor, OnHeaderBackgroundChanged));

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        private static void OnHeaderBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                var control = d as Subpanel;
                control._titleBarGrid.Background = e.NewValue as Brush;
            }
        }

        #endregion

    }
}

using ICBCustomControlLibrary.Helpers;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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
            DependencyProperty.Register("ShowHeader", typeof(bool), typeof(Subpanel), new PropertyMetadata(true));

        public bool IsShowHeader
        {
            get { return (bool)GetValue(IsShowHeaderProperty); }
            set { SetValue(IsShowHeaderProperty, value); }
        }

        #endregion

        private Binding childBinding;
        private Binding isShowHeaderBinding;
    }
}

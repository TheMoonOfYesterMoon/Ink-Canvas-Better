using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ICBCustomControlLibrary.Controls.Panel
{
    public partial class Subpanel
    {
        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            e.Handled = false;
        }

        protected virtual void OnPinButtonClicked(Object s, RoutedEventArgs args)
        {
            StaysOpen = !StaysOpen;
            args.Handled = true;
        }

        protected virtual void OnCloseButtonClicked(Object s, RoutedEventArgs args)
        {
            this.IsOpen = false;
            args.Handled = true;
        }

        public void Show()
        {
            this.IsOpen = true;
        }

        public void Hide()
        {
            this.IsOpen = false;
        }
    }
}

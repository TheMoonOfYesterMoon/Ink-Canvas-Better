using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        const double FOLD_TOLERANCE = 8;
        bool _isMouseDown = false;
        Point _mouseDownPosition;
        Point _mouseUpPosition;
        Point _mouseDownControlPosition;

        #region Drag & Fold

        private void SmileIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = sender as Control;
            _isMouseDown = true;
            _mouseDownPosition = e.GetPosition(MainInkCanvas);
            if (!(floatingBar.RenderTransform is TranslateTransform transform))
            {
                transform = new TranslateTransform();
                floatingBar.RenderTransform = transform;
            }
            _mouseDownControlPosition = new Point(transform.X, transform.Y);
            c.MouseMove += SmileIcon_MouseMove;
            c.CaptureMouse();
        }

        private void SmileIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                var c = floatingBar;
                var pos = e.GetPosition(this);
                var dp = pos - _mouseDownPosition;
                var transform = c.RenderTransform as TranslateTransform;
                transform.X = _mouseDownControlPosition.X + dp.X;
                transform.Y = _mouseDownControlPosition.Y + dp.Y;
            }
        }

        private void SmileIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var c = sender as Control;
            _isMouseDown = false;
            _mouseUpPosition = e.GetPosition(MainInkCanvas);

            // fold the floating bar if the mouse movement is within the tolerance
            double deltaX = _mouseUpPosition.X - _mouseDownPosition.X;
            double deltaY = _mouseUpPosition.Y - _mouseDownPosition.Y;
            if (Math.Abs(deltaX) <= FOLD_TOLERANCE
                & Math.Abs(deltaY) <= FOLD_TOLERANCE)
            {
                SwitchFloatingBatFoldingState();
            }

            c.MouseMove -= SmileIcon_MouseMove;
            c.ReleaseMouseCapture();
        }

        #endregion

    }
}

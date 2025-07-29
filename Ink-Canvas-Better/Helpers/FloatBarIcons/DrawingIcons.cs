using Ink_Canvas_Better.Resources;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        #region Cursor
        public void CursorIcon_Click(object sender, RoutedEventArgs e)
        {
            if (RuntimeData.currentDrawingMode != RuntimeData.DrawingMode.None)
            {
                CursorIcon_ChangeToCursor();
            }
        }

        public void CursorIcon_ChangeToCursor()
        {
            MainWindow_Grid.Background = Brushes.Transparent;
            RuntimeData.currentDrawingMode = RuntimeData.DrawingMode.None;
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
        }

        #endregion

        #region Pen
        public void PenIcon_Click(Object sender, RoutedEventArgs e)
        {
            if (RuntimeData.currentDrawingMode != RuntimeData.DrawingMode.Pen)
            {
                MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                RuntimeData.currentDrawingMode = RuntimeData.DrawingMode.Pen;
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
            else
            {
                // TODO
            }
        }
        #endregion

        #region Highlighter
        public void HighlighterIcon_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        #endregion

        #region Eraser
        public void EraserIcon_Click(object sender, RoutedEventArgs e)
        {
            if (RuntimeData.currentDrawingMode != RuntimeData.DrawingMode.Eraser)
            {
                MainWindow_Grid.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                RuntimeData.currentDrawingMode = RuntimeData.DrawingMode.Eraser;
                // TODO
                inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            else
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            }
        }
        #endregion

        #region Pick
        public void PickIcon_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Clear
        public void ClearIcon_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }
        #endregion

    }
}

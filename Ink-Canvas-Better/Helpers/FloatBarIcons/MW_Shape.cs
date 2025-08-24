using Ink_Canvas_Better.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;

namespace Ink_Canvas_Better
{
    public partial class MainWindow
    {
        public void ShapeIcon_Click(object sender, RoutedEventArgs e)
        {
            RuntimeData.mainWindow.Popup_Shape.IsOpen = true;
        }

        public double GetDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        private StrokeCollection GenerateStrokeCollection_DashedLine(Point st, Point ed)
        {
            double step = MainInkCanvas.DefaultDrawingAttributes.Width * 4;
            Stroke stroke;
            StrokeCollection strokeCollection = new StrokeCollection();
            double d = GetDistance(st, ed);
            double sinTheta = (ed.Y - st.Y) / d;
            double cosTheta = (ed.X - st.X) / d;
            for (double i = 0.0; i < d; i += step)
            {
                List<Point> pointList = new List<Point>{
                    new Point(st.X + i * cosTheta, st.Y + i * sinTheta),
                    new Point(st.X + Math.Min(i + step / 2, d) * cosTheta, st.Y + Math.Min(i + step / 2, d) * sinTheta)
                };
                stroke = new Stroke(new StylusPointCollection(pointList))
                {
                    DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone()
                };
                strokeCollection.Add(stroke);
            }
            return strokeCollection;
        }

    }
}

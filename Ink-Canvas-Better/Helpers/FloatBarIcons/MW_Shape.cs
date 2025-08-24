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

        private void MainInkCanvas_DrawShape(Point endPoint)
        {
            switch (RuntimeData.CurrentShape)
            {
                // 2D shape
                case "Shape_Line": UpdateStrokes(GenerateStrokeCollection_Line(iniPoint, endPoint)); break;
                case "Shape_DashedLine": UpdateStrokes(GenerateStrokeCollection_DashedLine(iniPoint, endPoint)); break;
                case "Shape_DotLine": UpdateStrokes(GenerateStrokeCollection_DotLine(iniPoint, endPoint)); break;
                case "Shape_ArrowLine":
                case "Shape_ParallelLine":
                case "Shape_Coordinate1":
                case "Shape_Coordinate2":
                case "Shape_Rectangle":
                case "Shape_Circle":
                case "Shape_DashedCircle":
                case "Shape_Ellipse":
                case "Shape_Hyperbola":
                case "Shape_Parabola":
                // 3D shape
                case "Shape_Cylinder":
                case "Shape_Cone":
                case "Shape_Cuboid":
                case "Shape_Tetrahedron":
                default:
                    throw new NotImplementedException($"Unsupported shape {RuntimeData.CurrentShape}");
            }
        }

        #region 2D shape generators

        // 2D shape -- Line
        private StrokeCollection GenerateStrokeCollection_Line(Point st, Point ed)
        {
            List<Point> pointList = new List<Point>{
                new Point(st.X, st.Y),
                new Point(ed.X, ed.Y)
            };
            Stroke stroke = new Stroke(new StylusPointCollection(pointList))
            {
                DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone()
            };
            StrokeCollection strokeCollection = new StrokeCollection
            {
                stroke
            };
            return strokeCollection;
        }

        // 2D shape -- DashedLine
        private StrokeCollection GenerateStrokeCollection_DashedLine(Point st, Point ed)
        {
            double step = MainInkCanvas.DefaultDrawingAttributes.Width * 4;
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
                Stroke stroke = new Stroke(new StylusPointCollection(pointList))
                {
                    DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone()
                };
                strokeCollection.Add(stroke);
            }
            return strokeCollection;
        }

        // 2D shape -- DotLine
        private StrokeCollection GenerateStrokeCollection_DotLine(Point st, Point ed)
        {
            double step = MainInkCanvas.DefaultDrawingAttributes.Width * 2;
            StrokeCollection strokeCollection = new StrokeCollection();
            double d = GetDistance(st, ed);
            double sinTheta = (ed.Y - st.Y) / d;
            double cosTheta = (ed.X - st.X) / d;
            for (double i = 0.0; i < d; i += step)
            {
                List<Point> pointList = new List<Point>{
                    new Point(st.X + i * cosTheta, st.Y + i * sinTheta)
                };
                Stroke stroke = new Stroke(new StylusPointCollection(pointList))
                {
                    DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone()
                };
                strokeCollection.Add(stroke.Clone());
            }
            return strokeCollection;
        }

        #endregion

        #region 3D shape generators

        #endregion

        private void UpdateStrokes(StrokeCollection strokeCollection)
        {
            try { MainInkCanvas.Strokes.Remove(lastTempStrokeCollection); } catch { }
            lastTempStrokeCollection = strokeCollection;
            MainInkCanvas.Strokes.Add(lastTempStrokeCollection);
        }

        public double GetDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

    }
}

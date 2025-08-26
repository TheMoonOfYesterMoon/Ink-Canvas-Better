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
using System.Windows.Media.Media3D;

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
                case "Shape_ArrowLine": UpdateStrokes(GenerateStrokeCollection_ArrowLine(iniPoint, endPoint)); break;
                case "Shape_ParallelLine": UpdateStrokes(GenerateStrokeCollection_ParallelLine(iniPoint, endPoint)); break;
                case "Shape_Coordinate2D": UpdateStrokes(GenerateStrokeCollection_Coordinate2D(iniPoint, endPoint)); break;
                case "Shape_Rectangle": UpdateStrokes(GenerateStrokeCollection_Rectangle(iniPoint, endPoint)); break;
                case "Shape_Circle":
                case "Shape_DashedCircle":
                case "Shape_Ellipse":
                case "Shape_Hyperbola":
                case "Shape_Parabola":
                // 3D shape
                case "Shape_Coordinate3D":
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
            List<Point> pointList = new List<Point> {
                new Point(st.X, st.Y),
                new Point(ed.X, ed.Y)
            };
            Stroke stroke = new Stroke(new StylusPointCollection(pointList))
            {
                DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone()
            };
            StrokeCollection strokeCollection = new StrokeCollection { stroke.Clone() };
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
                List<Point> pointList = new List<Point> {
                    new Point(st.X + i * cosTheta, st.Y + i * sinTheta),
                    new Point(st.X + Math.Min(i + step / 2, d) * cosTheta, st.Y + Math.Min(i + step / 2, d) * sinTheta)
                };
                Stroke stroke = new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes);
                strokeCollection.Add(stroke.Clone());
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
                List<Point> pointList = new List<Point> {
                    new Point(st.X + i * cosTheta, st.Y + i * sinTheta)
                };
                Stroke stroke = new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes);
                strokeCollection.Add(stroke.Clone());
            }
            return strokeCollection;
        }

        // 2D shape -- ArrowLine
        private StrokeCollection GenerateStrokeCollection_ArrowLine(Point st, Point ed)
        {
            StrokeCollection strokeCollection = new StrokeCollection();
            double d = GetDistance(st, ed);
            if (d == 0) return strokeCollection; // the strokeCollection is empty
            double w = Math.Log(MainInkCanvas.DefaultDrawingAttributes.Width + Math.E) * 30, h = Math.Log(MainInkCanvas.DefaultDrawingAttributes.Width + Math.E) * 10;
            double theta = Math.Atan2(st.Y - ed.Y, st.X - ed.X);
            double sint = Math.Sin(theta);
            double cost = Math.Cos(theta);

            List<Point> pointList = new List<Point> {
                new Point(st.X, st.Y),
                new Point(ed.X, ed.Y),
                new Point(ed.X + (w * cost - h * sint), ed.Y + (w * sint + h * cost)),
                new Point(ed.X, ed.Y),
                new Point(ed.X + (w * cost + h * sint), ed.Y - (h * cost - w * sint))
            };
            Stroke stroke = new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes);
            strokeCollection.Add(stroke.Clone());
            return strokeCollection;
        }

        // 2D shape -- ParallelLine
        private StrokeCollection GenerateStrokeCollection_ParallelLine(Point st, Point ed)
        {
            StrokeCollection strokeCollection = new StrokeCollection();
            double d = GetDistance(st, ed);
            if (d == 0) return strokeCollection; // the strokeCollection is empty

            double sinTheta = (st.Y - ed.Y) / d;
            double cosTheta = (ed.X - st.X) / d;
            // double tanTheta = Math.Abs(sinTheta / cosTheta);
            double x = Math.Log(MainInkCanvas.DefaultDrawingAttributes.Width + Math.E) * 20;
            // TODO: Angle Constraint
            //if (Math.Abs(tanTheta) < 1.0 / 12)
            //{
            //    sinTheta = 0;
            //    cosTheta = 1;
            //    ed.Y = st.Y;
            //}
            //if (tanTheta < 0.63 && tanTheta > 0.52) //30
            //{
            //    sinTheta = sinTheta / Math.Abs(sinTheta) * 0.5;
            //    cosTheta = cosTheta / Math.Abs(cosTheta) * 0.866;
            //    ed.Y = st.Y - d * sinTheta;
            //    ed.X = st.X + d * cosTheta;
            //}
            //if (tanTheta < 1.08 && tanTheta > 0.92) //45
            //{
            //    sinTheta = sinTheta / Math.Abs(sinTheta) * 0.707;
            //    cosTheta = cosTheta / Math.Abs(cosTheta) * 0.707;
            //    ed.Y = st.Y - d * sinTheta;
            //    ed.X = st.X + d * cosTheta;
            //}
            //if (tanTheta < 1.95 && tanTheta > 1.63) //60
            //{
            //    sinTheta = sinTheta / Math.Abs(sinTheta) * 0.866;
            //    cosTheta = cosTheta / Math.Abs(cosTheta) * 0.5;
            //    ed.Y = st.Y - d * sinTheta;
            //    ed.X = st.X + d * cosTheta;
            //}
            //if (Math.Abs(cosTheta / sinTheta) < 1.0 / 12)
            //{
            //    ed.X = st.X;
            //    sinTheta = 1;
            //    cosTheta = 0;
            //}
            strokeCollection.Add(GenerateStrokeCollection_Line(new Point(st.X - 3 * x * sinTheta, st.Y - 3 * x * cosTheta), new Point(ed.X - 3 * x * sinTheta, ed.Y - 3 * x * cosTheta)));
            strokeCollection.Add(GenerateStrokeCollection_Line(new Point(st.X - x * sinTheta, st.Y - x * cosTheta), new Point(ed.X - x * sinTheta, ed.Y - x * cosTheta)));
            strokeCollection.Add(GenerateStrokeCollection_Line(new Point(st.X + x * sinTheta, st.Y + x * cosTheta), new Point(ed.X + x * sinTheta, ed.Y + x * cosTheta)));
            strokeCollection.Add(GenerateStrokeCollection_Line(new Point(st.X + 3 * x * sinTheta, st.Y + 3 * x * cosTheta), new Point(ed.X + 3 * x * sinTheta, ed.Y + 3 * x * cosTheta)));
            
            return strokeCollection;
        }

        // 2D shape -- Coordinate (2D)
        private StrokeCollection GenerateStrokeCollection_Coordinate2D(Point st, Point ed)
        {
            StrokeCollection strokeCollection = new StrokeCollection();
            double d = GetDistance(st, ed);
            if (d == 0) return strokeCollection; // the strokeCollection is empty
            strokeCollection.Add(GenerateStrokeCollection_ArrowLine(new Point(2 * st.X - (ed.X - 20), st.Y), new Point(ed.X, st.Y)));
            strokeCollection.Add(GenerateStrokeCollection_ArrowLine(new Point(st.X, 2 * st.Y - (ed.Y + 20)), new Point(st.X, ed.Y)));
            return strokeCollection;
        }

        // 2D shape -- Rectangle
        private StrokeCollection GenerateStrokeCollection_Rectangle(Point st, Point ed)
        {
            List<Point> pointList = new List<Point>{
                new Point(st.X, st.Y),
                new Point(st.X, ed.Y),
                new Point(ed.X, ed.Y),
                new Point(ed.X, st.Y),
                new Point(st.X, st.Y)};
            Stroke stroke = new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes);
            StrokeCollection strokeCollection = new StrokeCollection { stroke.Clone() };
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

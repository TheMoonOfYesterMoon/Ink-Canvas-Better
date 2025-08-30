using Ink_Canvas_Better.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                case "Shape_Circle": UpdateStrokes(GenerateStrokeCollection_Circle(iniPoint, endPoint)); break;
                case "Shape_DashedCircle": UpdateStrokes(GenerateStrokeCollection_DashedCircle(iniPoint, endPoint)); break;
                case "Shape_Ellipse": UpdateStrokes(GenerateStrokeCollection_Ellipse(iniPoint, endPoint)); break;
                case "Shape_Hyperbola": UpdateStrokes(GenerateStrokeCollection_Hyperbola(iniPoint, endPoint)); break;
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
            double w = RuntimeData.GetShapePara_0() * 30, h = RuntimeData.GetShapePara_0() * 10;
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
            double x = RuntimeData.GetShapePara_0() * 20;
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

        // 2D shape -- Circle
        private StrokeCollection GenerateStrokeCollection_Circle(Point st, Point ed)
        {
            double d = GetDistance(st, ed);
            List<Point> pointList = new List<Point>();
            for (double r = 0; r <= 2 * Math.PI; r += 0.01)
            {
                pointList.Add(new Point(st.X + d * Math.Cos(r), st.Y + d * Math.Sin(r)));
            }
            Stroke stroke = new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes);
            StrokeCollection strokeCollection = new StrokeCollection { stroke.Clone() };
            return strokeCollection;
        }

        // 2D shape -- DashedCircle
        private StrokeCollection GenerateStrokeCollection_DashedCircle(Point st, Point ed, bool isDrawTop = true, bool isDrawBottom = true)
        {
            double a = ed.X - st.X;
            double b = ed.Y - st.Y;
            double step = 0.05;
            List<Point> pointList = new List<Point>();
            StrokeCollection strokeCollection = new StrokeCollection();
            if (isDrawBottom)
            {
                for (double i = 0.0; i < 1.0; i += step * 1.66)
                {
                    pointList.Clear();
                    for (double r = Math.PI * i; r <= Math.PI * (i + step); r += 0.01)
                    {
                        pointList.Add(new Point(st.X + a * Math.Cos(r), st.Y + b * Math.Sin(r)));
                    }
                    Stroke stroke = new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes);
                    strokeCollection.Add(stroke.Clone());
                }
            }
            if (isDrawTop)
            {
                for (double i = 1.0; i < 2.0; i += step * 1.66)
                {
                    pointList.Clear();
                    for (double r = Math.PI * i; r <= Math.PI * (i + step); r += 0.01)
                    {
                        pointList.Add(new Point(st.X + a * Math.Cos(r), st.Y + b * Math.Sin(r)));
                    }
                    Stroke stroke = new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes);
                    strokeCollection.Add(stroke.Clone());
                }
            }
            return strokeCollection;
        }

        // 2D shape -- Ellipse
        private StrokeCollection GenerateStrokeCollection_Ellipse(Point st, Point ed, bool isDrawTop = true, bool isDrawBottom = true)
        {
            double a = ed.X - st.X;
            double b = ed.Y - st.Y;
            List<Point> pointList = new List<Point>();
            if (isDrawTop && isDrawBottom)
            {
                for (double r = 0; r <= 2 * Math.PI; r += 0.01)
                {
                    pointList.Add(new Point(st.X + a * Math.Cos(r), st.Y + b * Math.Sin(r)));
                }
            }
            else if (isDrawTop || isDrawBottom)
            {
                for (double r = 0; r <= Math.PI; r += 0.01)
                {
                    double x = isDrawTop ? r += Math.PI : r;
                    pointList.Add(new Point(st.X + a * Math.Cos(x), st.Y + b * Math.Sin(x)));
                }
            }
            Stroke stroke = new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes);
            StrokeCollection strokeCollection = new StrokeCollection { stroke.Clone() };
            return strokeCollection;
        }

        // 2D shape -- Hyperbola
        private StrokeCollection GenerateStrokeCollection_Hyperbola(Point st, Point ed)
        {
            // x^2/a^2 - y^2/b^2 = 1
            StrokeCollection strokeCollection = new StrokeCollection();
            List<Point> pointList;
            Stroke stroke;
            StylusPointCollection stylusPointCollection;
            double a, b, c;
            if (Math.Abs(st.X - ed.X) < 0.01 || Math.Abs(st.Y - ed.Y) < 0.01) return strokeCollection;
            if (RuntimeData.CurrentDrawStep == 0)
            {
                // Asymptote first
                double k = Math.Abs((ed.Y - st.Y) / (ed.X - st.X));
                strokeCollection.Add(GenerateStrokeCollection_DashedLine(new Point(2 * st.X - ed.X, 2 * st.Y - ed.Y), ed));
                strokeCollection.Add(GenerateStrokeCollection_DashedLine(new Point(2 * st.X - ed.X, ed.Y), new Point(ed.X, 2 * st.Y - ed.Y)));
                RuntimeData.Shape_Para_1 = k;
                multiStepShapeSpecialStrokeCollection = strokeCollection;
            }
            else
            {
                // Hyperbola next
                var pointList2 = new List<Point>();
                var pointList3 = new List<Point>();
                var pointList4 = new List<Point>();
                st = lastInitPoint;
                double k = RuntimeData.Shape_Para_1;
                bool isHyperbolaFocalPointOnXAxis = Math.Abs((ed.Y - st.Y) / (ed.X - st.X)) < k;
                if (isHyperbolaFocalPointOnXAxis)
                {
                    // The focus is on the x-axis
                    a = Math.Sqrt(Math.Abs((ed.X - st.X) * (ed.X - st.X) - (ed.Y - st.Y) * (ed.Y - st.Y) / (k * k)));
                    b = a * k;
                    pointList = new List<Point>();
                    for (double i = a; i <= Math.Abs(ed.X - st.X); i += 0.5)
                    {
                        double rY = Math.Sqrt(Math.Abs(k * k * i * i - b * b));
                        pointList.Add(new Point(st.X + i, st.Y - rY));
                        pointList2.Add(new Point(st.X + i, st.Y + rY));
                        pointList3.Add(new Point(st.X - i, st.Y - rY));
                        pointList4.Add(new Point(st.X - i, st.Y + rY));
                    }
                }
                else
                {
                    // The focus is on the y-axis
                    a = Math.Sqrt(Math.Abs((ed.Y - st.Y) * (ed.Y - st.Y) - (ed.X - st.X) * (ed.X - st.X) * (k * k)));
                    b = a / k;
                    pointList = new List<Point>();
                    for (double i = a; i <= Math.Abs(ed.Y - st.Y); i += 0.5)
                    {
                        double rX = Math.Sqrt(Math.Abs(i * i / k / k - b * b));
                        pointList.Add(new Point(st.X - rX, st.Y + i));
                        pointList2.Add(new Point(st.X + rX, st.Y + i));
                        pointList3.Add(new Point(st.X - rX, st.Y - i));
                        pointList4.Add(new Point(st.X + rX, st.Y - i));
                    }
                }
                try
                {
                    strokeCollection.Add(new Stroke(new StylusPointCollection(pointList), MainInkCanvas.DefaultDrawingAttributes).Clone());
                    strokeCollection.Add(new Stroke(new StylusPointCollection(pointList2), MainInkCanvas.DefaultDrawingAttributes).Clone());
                    strokeCollection.Add(new Stroke(new StylusPointCollection(pointList3), MainInkCanvas.DefaultDrawingAttributes).Clone());
                    strokeCollection.Add(new Stroke(new StylusPointCollection(pointList4), MainInkCanvas.DefaultDrawingAttributes).Clone());

                    c = Math.Sqrt(a * a + b * b);

                    StylusPoint stylusPoint = isHyperbolaFocalPointOnXAxis ? new StylusPoint(st.X + c, st.Y, (float)1.0) : new StylusPoint(st.X, st.Y + c, (float)1.0);
                    stylusPointCollection = new StylusPointCollection { stylusPoint };
                    stroke = new Stroke(stylusPointCollection, MainInkCanvas.DefaultDrawingAttributes);
                    strokeCollection.Add(stroke.Clone());

                    stylusPoint = isHyperbolaFocalPointOnXAxis ? new StylusPoint(st.X - c, st.Y, (float)1.0) : new StylusPoint(st.X, st.Y - c, (float)1.0);
                    stylusPointCollection = new StylusPointCollection { stylusPoint };
                    stroke = new Stroke(stylusPointCollection, MainInkCanvas.DefaultDrawingAttributes);
                    strokeCollection.Add(stroke.Clone());
                }
                catch
                {
                    return strokeCollection;
                }
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

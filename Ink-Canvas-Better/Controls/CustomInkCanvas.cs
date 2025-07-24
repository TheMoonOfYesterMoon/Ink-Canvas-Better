using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ink_Canvas_Better.Resources;

namespace Ink_Canvas_Better.Controls
{
    class CustomInkCanvas : InkCanvas
    {
        private CustomDynamicRenderer _customRenderer = new CustomDynamicRenderer();

        public CustomInkCanvas() : base()
        {
            DefaultDrawingAttributes = new DrawingAttributes
            {
                Color = Colors.Black,
                FitToCurve = true,
                Height = 20,
                Width = 20,
                StylusTip = StylusTip.Ellipse
            };

            DynamicRenderer = _customRenderer;
        }

        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            this.Strokes.Remove(e.Stroke);

            this.Strokes.Add(new CustomStroke(e.Stroke.StylusPoints, RuntimeData.DrawingAttributes));
        }
    }

    class CustomStroke : Stroke
    {
        private DrawingAttributes _drawingAttributes;
        private Brush _brush;
        private Pen _pen;

        public CustomStroke(
            StylusPointCollection stylusPoints,
            DrawingAttributes drawingAttributes)
            : base(stylusPoints)
        {
            _drawingAttributes = drawingAttributes;

            _brush = new SolidColorBrush(_drawingAttributes.Color);
            _brush.Freeze();

            _pen = new Pen(_brush, _drawingAttributes.Width)
            {
                StartLineCap = PenLineCap.Round,
                EndLineCap = PenLineCap.Round,
                LineJoin = PenLineJoin.Round
            };
            _pen.Freeze();
        }

        private readonly double width = 16;

        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            if (this.StylusPoints?.Count < 1)
                return;

            var p1 = new Point(double.NegativeInfinity, double.NegativeInfinity);
            var w1 = this.width + 20;


            for (int i = 0; i < StylusPoints.Count; i++)
            {
                var p2 = (Point)this.StylusPoints[i];

                var vector = p1 - p2;

                var dx = (p2.X - p1.X) / vector.Length;
                var dy = (p2.Y - p1.Y) / vector.Length;

                var w2 = this.width;
                if (w1 - vector.Length > this.width)
                    w2 = w1 - vector.Length;

                for (int j = 0; j < vector.Length; j++)
                {
                    var x = p2.X;
                    var y = p2.Y;

                    if (!double.IsInfinity(p1.X) && !double.IsInfinity(p1.Y))
                    {
                        x = p1.X + dx;
                        y = p1.Y + dy;
                    }

                    drawingContext.DrawEllipse(new SolidColorBrush(this.DrawingAttributes.Color), null, new Point(x, y), w2, w2);

                    p1 = new Point(x, y);
                    if (double.IsInfinity(vector.Length)) break;
                }
            }
        }
    }

    class CustomDynamicRenderer : DynamicRenderer
    {
        private readonly double width = 16;

        protected override void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
        {
            var p1 = new Point(double.NegativeInfinity, double.NegativeInfinity);
            var w1 = this.width + 20;

            for (int i = 0; i < stylusPoints.Count; i++)
            {
                Point p2 = (Point)stylusPoints[i];

                var vector = p1 - p2;

                var dx = (p2.X - p1.X) / vector.Length;
                var dy = (p2.Y - p1.Y) / vector.Length;

                var w2 = this.width;
                if (w1 - vector.Length > this.width)
                    w2 = w1 - vector.Length;

                for (int j = 0; j < vector.Length; j++)
                {
                    var x = p2.X;
                    var y = p2.Y;

                    if (!double.IsInfinity(p1.X) && !double.IsInfinity(p1.Y))
                    {
                        x = p1.X + dx;
                        y = p1.Y + dy;
                    }

                    drawingContext.DrawEllipse(new SolidColorBrush(this.DrawingAttributes.Color), null, new Point(x, y), w2, w2);

                    p1 = new Point(x, y);

                    if (double.IsInfinity(vector.Length))
                        break;

                }
            }
        }
    }
}
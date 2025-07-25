using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using Ink_Canvas_Better.Resources;

namespace Ink_Canvas_Better.Controls
{
    class CustomInkCanvas : InkCanvas
    {
        private readonly CustomDynamicRenderer _customRenderer = new CustomDynamicRenderer();

        public CustomInkCanvas() : base()
        {
            DefaultDrawingAttributes = new DrawingAttributes
            {
                Color = Colors.Red,
                FitToCurve = true,
                Height = 3,
                Width = 3,
                StylusTip = StylusTip.Ellipse
            };
        }

        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            this.Strokes.Remove(e.Stroke);
            this.Strokes.Add(new CustomStroke(e.Stroke.StylusPoints, RuntimeData.DrawingAttributes));
            DynamicRenderer = _customRenderer;
        }
    }

    class CustomStroke : Stroke
    {
        public CustomStroke(StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes) : base(stylusPoints)
        {
            this.DrawingAttributes = drawingAttributes.Clone();
        }

        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            switch (RuntimeData.CurrentInkStyle)
            {
                case RuntimeData.InkStyle.Default:
                    base.DrawCore(drawingContext, drawingAttributes);
                    break;
                case RuntimeData.InkStyle.Simulative:
                    DrawSimulativeStroke(drawingContext);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(RuntimeData.CurrentInkStyle), "Unsupported ink style.");
            }
        }

        private void DrawSimulativeStroke(DrawingContext drawingContext)
        {
            if (this.StylusPoints?.Count < 1)
                return;

            var bluntnessFactor = RuntimeData.bluntnessFactor;
            var prevPoint = new Point(double.NegativeInfinity, double.NegativeInfinity);
            var w1 = this.DrawingAttributes.Width;

            for (int i = 0; i < StylusPoints.Count; i++)
            {
                var newPoint = (Point)this.StylusPoints[i];

                var vector = prevPoint - newPoint;

                var dx = (newPoint.X - prevPoint.X) / vector.Length;
                var dy = (newPoint.Y - prevPoint.Y) / vector.Length;

                var w2 = DrawingAttributes.Width;
                if (vector.Length > 1)
                {
                    w2 = w1 * Math.Pow(bluntnessFactor, vector.Length);
                }

                for (int j = 0; j < vector.Length; j++)
                {
                    var p3 = new Point(newPoint.X, newPoint.Y);

                    if (!double.IsInfinity(prevPoint.X) && !double.IsInfinity(prevPoint.Y))
                    {
                        p3.X = prevPoint.X + dx;
                        p3.Y = prevPoint.Y + dy;
                    }

                    drawingContext.DrawEllipse(new SolidColorBrush(this.DrawingAttributes.Color), null, p3, w2 / 2, w2 / 2);

                    prevPoint = p3;
                    if (double.IsInfinity(vector.Length)) break;
                }
            }
        }
    }

    class CustomDynamicRenderer : DynamicRenderer
    {
        protected override void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
        {
            switch (RuntimeData.CurrentInkStyle)
            {
                case RuntimeData.InkStyle.Default:
                    base.OnDraw(drawingContext, stylusPoints, geometry, fillBrush);
                    break;
                case RuntimeData.InkStyle.Simulative:
                    DrawSimulativeStroke(drawingContext, stylusPoints);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(RuntimeData.CurrentInkStyle), "Unsupported ink style.");
            }
        }

        private void DrawSimulativeStroke(DrawingContext drawingContext, StylusPointCollection stylusPoints)
        {
            var bluntnessFactor = RuntimeData.bluntnessFactor;
            var prevPoint = new Point(double.NegativeInfinity, double.NegativeInfinity);
            var w1 = DrawingAttributes.Width;

            for (int i = 0; i < stylusPoints.Count; i++)
            {
                Point newPoint = (Point)stylusPoints[i];

                var vector = prevPoint - newPoint;

                var dx = (newPoint.X - prevPoint.X) / vector.Length;
                var dy = (newPoint.Y - prevPoint.Y) / vector.Length;

                var w2 = DrawingAttributes.Width;
                if (vector.Length > 1)
                {
                    w2 = w1 * Math.Pow(bluntnessFactor, vector.Length);
                }

                for (int j = 0; j < vector.Length; j++)
                {
                    var p3 = new Point(newPoint.X, newPoint.Y);

                    if (!double.IsInfinity(prevPoint.X) && !double.IsInfinity(prevPoint.Y))
                    {
                        p3.X = prevPoint.X + dx;
                        p3.Y = prevPoint.Y + dy;
                    }

                    drawingContext.DrawEllipse(new SolidColorBrush(this.DrawingAttributes.Color), null, p3, w2 / 2, w2 / 2);

                    prevPoint = p3;
                    if (double.IsInfinity(vector.Length)) break;
                }
            }
        }
    }
}
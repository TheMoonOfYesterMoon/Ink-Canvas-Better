using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ink_Canvas_Better.Controls
{
    class CustomInkCanvas : System.Windows.Controls.InkCanvas
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

            SyncRendererWithAttributes();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == DefaultDrawingAttributesProperty)
            {
                SyncRendererWithAttributes();
            }
        }

        private void SyncRendererWithAttributes()
        {
            _customRenderer.SetAttributes(DefaultDrawingAttributes.Clone());
        }

        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            this.Strokes.Remove(e.Stroke);

            CustomStroke customStroke = new CustomStroke(
                e.Stroke.StylusPoints,
                _customRenderer.GetDynamicWidths(),
                DefaultDrawingAttributes.Clone());

            this.Strokes.Add(customStroke);

            InkCanvasStrokeCollectedEventArgs args =
                new InkCanvasStrokeCollectedEventArgs(customStroke);
            base.OnStrokeCollected(args);
        }
    }

    class CustomStroke : Stroke
    {
        private DrawingAttributes _drawingAttributes;
        private List<double> _dynamicWidths;
        private StreamGeometry _geometry;
        private bool _isGeometryDirty = true;
        private Brush _brush;
        private Pen _pen;

        public CustomStroke(
            StylusPointCollection stylusPoints,
            List<double> dynamicWidths,
            DrawingAttributes drawingAttributes)
            : base(stylusPoints)
        {
            _drawingAttributes = drawingAttributes;
            _dynamicWidths = dynamicWidths;

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

        private void BuildGeometry()
        {
            if (StylusPoints.Count == 0) return;

            _geometry = new StreamGeometry();
            using (var context = _geometry.Open())
            {
                context.BeginFigure((Point)StylusPoints[0], false, false);

                for (int i = 1; i < StylusPoints.Count; i++)
                {
                    context.LineTo((Point)StylusPoints[i], true, false);
                }
            }

            _geometry.Freeze();
            _isGeometryDirty = false;
        }

        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            if (StylusPoints.Count == 0) return;

            if (_isGeometryDirty || _geometry == null)
            {
                BuildGeometry();
            }

            if (StylusPoints.Count == 1)
            {
                double width = _dynamicWidths.Count > 0 ? _dynamicWidths[0] : _drawingAttributes.Width;
                drawingContext.DrawEllipse(_brush, null, (Point)StylusPoints[0], width / 2, width / 2);
                return;
            }

            for (int i = 1; i < StylusPoints.Count; i++)
            {
                Point startPoint = (Point)StylusPoints[i - 1];
                Point endPoint = (Point)StylusPoints[i];

                double startWidth = _dynamicWidths[i - 1];
                double endWidth = _dynamicWidths[i];
                double avgWidth = (startWidth + endWidth) / 2;

                Pen dynamicPen = _pen.Clone();
                dynamicPen.Thickness = avgWidth;
                dynamicPen.Freeze();

                drawingContext.DrawLine(dynamicPen, startPoint, endPoint);
            }
        }
    }

    class CustomDynamicRenderer : DynamicRenderer
    {
        private Point _previousPoint;
        private StreamGeometry _currentGeometry;
        private StreamGeometryContext _geometryContext;
        private DrawingAttributes _currentAttributes = new DrawingAttributes();
        private List<double> _dynamicWidths = new List<double>();
        private Brush _brush;
        private Pen _pen;

        public void SetAttributes(DrawingAttributes attributes)
        {
            _currentAttributes = attributes;
            InvalidateCachedResources();
        }

        public List<double> GetDynamicWidths()
        {
            return new List<double>(_dynamicWidths);
        }

        private void InvalidateCachedResources()
        {
            _brush = null;
            _pen = null;
        }

        private Brush GetBrush()
        {
            if (_brush == null)
            {
                _brush = new SolidColorBrush(_currentAttributes.Color);
                _brush.Freeze();
            }
            return _brush;
        }

        private Pen GetPen(double width)
        {
            if (_pen == null || Math.Abs(_pen.Thickness - width) > 0.1)
            {
                _pen = new Pen(GetBrush(), width)
                {
                    StartLineCap = PenLineCap.Round,
                    EndLineCap = PenLineCap.Round,
                    LineJoin = PenLineJoin.Round
                };
                _pen.Freeze();
            }
            return _pen;
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            _dynamicWidths.Clear();

            _currentGeometry = new StreamGeometry();
            _geometryContext = _currentGeometry.Open();

            var stylusPoints = rawStylusInput.GetStylusPoints();
            _previousPoint = (Point)stylusPoints[0];

            _geometryContext.BeginFigure(_previousPoint, false, false);

            _dynamicWidths.Add(_currentAttributes.Width);

            base.OnStylusDown(rawStylusInput);
        }

        protected override void OnStylusUp(RawStylusInput rawStylusInput)
        {
            if (_geometryContext != null)
            {
                _geometryContext.Close();
                _geometryContext = null;
                _currentGeometry.Freeze();
            }

            base.OnStylusUp(rawStylusInput);
        }

        protected override void OnDraw(DrawingContext drawingContext,
                                      StylusPointCollection stylusPoints,
                                      Geometry geometry, Brush fillBrush)
        {
            if (stylusPoints.Count == 0 || _geometryContext == null)
                return;

            for (int i = 0; i < stylusPoints.Count; i++)
            {
                Point currentPoint = (Point)stylusPoints[i];

                double width = _currentAttributes.Width;
                double distance = (currentPoint - _previousPoint).Length;
                double widthFactor = Math.Min(1.0, distance / 15);
                width = Math.Max(1.0, _currentAttributes.Width * (1.0 - widthFactor * 0.7));

                _geometryContext.LineTo(currentPoint, true, false);

                _dynamicWidths.Add(width);

                if (i > 0 || _dynamicWidths.Count > 1)
                {
                    double startWidth = _dynamicWidths[_dynamicWidths.Count - 2];
                    double endWidth = width;
                    double avgWidth = (startWidth + endWidth) / 2;

                    drawingContext.DrawLine(GetPen(avgWidth), _previousPoint, currentPoint);

                }

                _previousPoint = currentPoint;
            }
        }
    }
}
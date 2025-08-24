using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Helpers.Others;
using Ink_Canvas_Better.Resources;
using Ink_Canvas_Better.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace Ink_Canvas_Better
{
    public partial class MainWindow : Window
    {
        Point iniPoint;
        Stroke lastTempStroke = null;

        #region Initialize

        readonly Magnifier MagnifierWindow = new Magnifier();

        /// <summary>
        /// Initialize MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Setting.LoadSettings(isStartup : true);
            RuntimeData.mainWindow = this;

            this.SourceInitialized += Win32Helper.MainWindow_SourceInitialized;
            this.Loaded += DockWindowToBottom;

            CursorIcon_Click(null, null);
        }

        #endregion

        private bool _mainInkCanvas_Hitable = true;
        public bool MainInkCanvas_Hitable
        {
            get { return _mainInkCanvas_Hitable; }
            set
            {
                _mainInkCanvas_Hitable= value;
                if (_mainInkCanvas_Hitable) MainInkCanvas.Background = (Brush)new BrushConverter().ConvertFrom("#01FFFFFF");
                else MainInkCanvas.Background = Brushes.Transparent;
            }
        }

        private bool _isHideDrawingTools = false;
        public bool IsHideDrawingTools
        {
            get { return _isHideDrawingTools; }
            set
            {
                _isHideDrawingTools = value;
                if (_isHideDrawingTools) HideDrawingTools(); else ShowDrawingTools();
            }
        }

        #region MainInkCanvas

        private void MainInkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
            iniPoint = e.GetPosition(MainInkCanvas);
        }

        private void MainInkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Shape && _isMouseDown)
            {
                MainInkCanvas_DrawShape(e.GetPosition(MainInkCanvas));
            }
        }

        private void MainInkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!RuntimeData.IsShapeModePersistent && RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Shape)
            {
                RuntimeData.CurrentDrawingMode = RuntimeData.LastDrawingMode;
            }
            _isMouseDown = false;
            lastTempStroke = null;
        }

        private void MainInkCanvas_TouchDown(object sender, TouchEventArgs e)
        {
            iniPoint = e.GetTouchPoint(MainInkCanvas).Position;
        }

        private void MainInkCanvas_TouchMove(object sender, TouchEventArgs e)
        {
            if (RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Shape)
            {
                MainInkCanvas_DrawShape(e.GetTouchPoint(MainInkCanvas).Position);
            }
        }

        private void MainInkCanvas_TouchUp(object sender, TouchEventArgs e)
        {
            if (!RuntimeData.IsShapeModePersistent && RuntimeData.CurrentDrawingMode == RuntimeData.DrawingMode.Shape)
            {
                RuntimeData.CurrentDrawingMode = RuntimeData.LastDrawingMode;
            }
            _isMouseDown = false;
            lastTempStroke = null;
        }

        private void MainInkCanvas_DrawShape(Point endPoint)
        {
            List<Point> pointList;
            StylusPointCollection pointCollection;
            Stroke stroke;
            StrokeCollection strokeCollection = new StrokeCollection();
            Point newIniPoint = iniPoint;
            switch (RuntimeData.CurrentShape)
            {
                // 2D shape
                case "Shape_Line":
                    pointList = new List<Point>{
                        new Point(iniPoint.X, iniPoint.Y),
                        new Point(endPoint.X, endPoint.Y)
                    };
                    pointCollection = new StylusPointCollection(pointList);
                    stroke = new Stroke(pointCollection) { DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone() };
                    try
                    {
                        MainInkCanvas.Strokes.Remove(lastTempStroke);
                    }
                    catch { }
                    lastTempStroke = stroke;
                    MainInkCanvas.Strokes.Add(stroke);
                    break;
                case "Shape_DashedLine":
                    break;
                case "Shape_DotLine":
                    break;
                case "Shape_ArrowLine":
                    break;
                case "Shape_ParallelLine":
                    break;
                case "Shape_Coordinate1":
                    break;
                case "Shape_Coordinate2":
                    break;
                case "Shape_Rectangle":
                    break;
                case "Shape_Circle":
                    break;
                case "Shape_DashedCircle":
                    break;
                case "Shape_Ellipse":
                    break;
                case "Shape_Hyperbola":
                    break;
                case "Shape_Parabola":
                    break;
                // 3D shape
                case "Shape_Cylinder":
                    break;
                case "Shape_Cone":
                    break;
                case "Shape_Cuboid":
                    break;
                case "Shape_Tetrahedron":
                    break;
                default:
                    throw new NotImplementedException($"Unsupported shape {RuntimeData.CurrentShape}");
            }
        }

        #endregion
    }
}

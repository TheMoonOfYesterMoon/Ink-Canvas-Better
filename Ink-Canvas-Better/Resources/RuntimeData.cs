using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Windows;
using Ink_Canvas_Better.Windows.FloatingBarIcons;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;

namespace Ink_Canvas_Better.Resources
{
    static class RuntimeData
    {
        public static bool isCloseFromButton = false;
        public static SettingWindow settingWindow;
        public static MainWindow mainWindow;
        public static FloatingBar_Pen floatingBar_Pen;
        public static FloatingBar_Highlighter floatingBar_Highlighter;
        public static ICB_ColorPicker colorPicker;
        public static String settingsFileName = "settings.json";
        public static SettingData settingData = new SettingData();
        public static Metadata currentMetadata = new Metadata();

        public static StylusShape CurrentEraserShape { get; set; } = new EllipseStylusShape(30, 30);

        public static DrawingAttributes CurrentDrawingAttributes_Pen { get; set; } = new DrawingAttributes()
        {
            Color = Color.FromRgb(255,0,0)
        };
        public static DrawingAttributes CurrentDrawingAttributes_Highlighter { get; set; } = new DrawingAttributes {
            Color = Color.FromRgb(255,255,0),
            StylusTip = StylusTip.Rectangle,
            Width = 1
        };

        private static DrawingMode _currentDrawingMode = DrawingMode.None;
        public static DrawingMode CurrentDrawingMode
        {
            get { return _currentDrawingMode; }
            set
            {
                _currentDrawingMode = value;
                mainWindow.CursorIcon.IsStatusEnable = false;
                mainWindow.PenIcon.IsStatusEnable = false;
                mainWindow.HighlighterIcon.IsStatusEnable = false;
                mainWindow.EraserIcon.IsStatusEnable = false;
                mainWindow.PickIcon.IsStatusEnable = false;
                switch (_currentDrawingMode)
                {
                    case DrawingMode.None:
                        break;
                    case DrawingMode.Cursor:
                        mainWindow.CursorIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Pen:
                        mainWindow.PenIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Highlighter:
                        mainWindow.HighlighterIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Eraser:
                        mainWindow.EraserIcon.IsStatusEnable = true;
                        break;
                    case DrawingMode.Pick:
                        mainWindow.PickIcon.IsStatusEnable = true;
                        break;
                    default:
                        _currentDrawingMode = DrawingMode.None;
                        throw new NotImplementedException();
                }
            }
        }

        private static EraserMode _currentEraserMode = EraserMode.Point;
        public static EraserMode CurrentEraserMode
        {
            get { return _currentEraserMode; }
            set
            {
                _currentEraserMode = value;
            }
        }

        public static void ApplyMetadata()
        {
            // TODO
        }

        public enum DrawingMode
        {
            None,
            Cursor,
            Pen,
            Highlighter,
            Eraser,
            Pick // Select
        }

        public enum EraserMode
        {
            Stroke,
            Point
        }

    }

    public class Metadata
    {
        [JsonProperty("metadataVer")]
        public String MetadataVer { get; set; } = "1.0";

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("icbMode")]
        public ICBMode CurrentICBMode { get; set; } = ICBMode.None;



        [JsonProperty("metadata_PPT")]
        public Metadata_PPT ExInfo_PPT { get; set; } = new Metadata_PPT();

        [JsonProperty("metadata_whiteBoard")]
        public Metadata_whiteBoard ExInfo_whiteBoard { get; set; } = new Metadata_whiteBoard();
        
        public enum ICBMode
        {
            None,
            PPT,
            WhiteBoard
        }
    }

    public class Metadata_PPT
    {
        [JsonProperty("slideCount")]
        public int CurrentSlideIndex { get; set; } = 0;
    }

    public class Metadata_whiteBoard
    {
        [JsonProperty("slideCount")]
        public int CurrentSlideIndex { get; set; } = 0;
    }
}

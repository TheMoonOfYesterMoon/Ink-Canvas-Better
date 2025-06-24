using iNKORE.UI.WPF.Controls;
using iNKORE.UI.WPF.Helpers;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Application = System.Windows.Application;
using Brush = System.Windows.Media.Brush;
using FontFamily = System.Windows.Media.FontFamily;
using Image = System.Windows.Controls.Image;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// ICB_Button
    /// </summary>
    public partial class ICB_Button : UserControl
    {
        private FontIcon fontIcon;
        private Image img;
        private readonly int SQUEEZE = 5;
        private bool _IsSqueezeHorizontally = false;
        private bool _IsShowText = true;
        private int _CornerRadius;
        private bool _IsStatusEnable = false;

        private readonly Brush BORDER_BRUSH_DEFAULT = (Brush)Application.Current.Resources["floatingBarBackground"];
        private readonly Brush BORDER_BRUSH_ENABLE = (Brush)Application.Current.Resources["ICB_ButtonStateEnable"];

        #region 构造方法

        public ICB_Button() {
            InitializeComponent();
            TextBox_1.Text = "example";
        }

        public ICB_Button(String text, FontIcon fontIcon)
        {
            InitializeComponent();
            PartOfInitialize(text, fontIcon);
        }

        public ICB_Button(String text, SimpleStackPanel simpleStackPanel)
        {
            InitializeComponent();
            PartOfInitialize(text, simpleStackPanel);
        }

        /// <summary>
        /// 使用此方法以简化部分构造
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <param name="a"></param>
        /// <param name="isSqueezeHorizontally"></param>
        private void PartOfInitialize<T>(String text, T a) where T : System.Windows.UIElement
        {
            TextBox_1.Text = text;
            SimpleStackPanel_1.Children.Add(a);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 文本
        /// </summary>
        public String Text
        {
            get => TextBox_1.Text;
            set => TextBox_1.Text = value;
        }

        /// <summary>
        /// 是否扁化
        /// </summary>
        public bool IsSqueezeHorizontally
        {
            get { return _IsSqueezeHorizontally; }
            set
            {
                _IsSqueezeHorizontally = value;
                if (_IsSqueezeHorizontally)
                {
                    Width -= SQUEEZE;
                    SimpleStackPanel_1.Width -= SQUEEZE;
                    TextBox_1.Width -= SQUEEZE;
                    InnerButton.Width -= SQUEEZE;
                    Border.Width = Width - 5;
                }

            }
        }

        /// <summary>
        /// 是否显示文字
        /// </summary>
        public bool IsShowText
        {
            get { return _IsShowText; }
            set
            {
                _IsShowText = value;
                ShowTextCheck();
            }
        }

        /// <summary>
        /// 圆角
        /// </summary>
        public int CornerRadius
        {
            get { return _CornerRadius; }
            set
            {
                _CornerRadius = value;
                CornerRadius cornerRadius = new CornerRadius(_CornerRadius);
                Border.CornerRadius = cornerRadius;
            }
        }

        public bool IsStatusEnable
        {
            get { return _IsStatusEnable; }
            set {
                _IsStatusEnable = value;
                if ( _IsStatusEnable )
                {
                    Console.WriteLine("enabled");
                    Border.Background = BORDER_BRUSH_ENABLE;
                }
                else
                {
                    Border.Background = BORDER_BRUSH_DEFAULT;
                }
            }
        }

        /// <summary>
        /// 文字图标
        /// </summary>
        public String FontIcon
        {
            set {
                fontIcon = new FontIcon(value, (FontFamily)System.Windows.Application.Current.Resources["FluentIconFontFamily"]);
                SetIcon(fontIcon);
            }
        }

        /// <summary>
        /// 图像图标
        /// </summary>
        public String Img
        {
            set
            {
                img = new Image();
                BitmapImage _ = new BitmapImage();
                _.BeginInit();
                _.UriSource = new Uri(value);
                _.EndInit();
                img.Source = _;
                SetIcon(img);
            }
        }

        public void SetIcon(UIElement element)
        {
            SimpleStackPanel_1.Children.Clear();
            SimpleStackPanel_1.Children.Add(element);
            ShowTextCheck();
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        public event RoutedEventHandler OnClick
        {
            add => InnerButton.Click += value;
            remove => InnerButton.Click -= value;
        }

        public new void Background(Brush background)
        {
            InnerButton.Background = background;
        }

        #endregion

        /// <summary>
        /// 检查是否显示文本，对控件大小进行调整
        /// </summary>
        public void ShowTextCheck()
        {
            if (_IsShowText)
            {
                TextBox_1.Visibility = Visibility.Visible;
                SimpleStackPanel_1.Height = 28;
                if (SimpleStackPanel_1.Children[0] is FontIcon)
                {
                    fontIcon.Height = SimpleStackPanel_1.Height;
                    fontIcon.FontSize = fontIcon.Height - 10;
                }
            }
            else
            {
                TextBox_1.Visibility = Visibility.Collapsed;
                SimpleStackPanel_1.Height = 43;
                if (SimpleStackPanel_1.Children[0] is FontIcon)
                {
                    fontIcon.Height = SimpleStackPanel_1.Height;
                    fontIcon.FontSize = fontIcon.Height - 10;
                }
            }
        }

    }
}

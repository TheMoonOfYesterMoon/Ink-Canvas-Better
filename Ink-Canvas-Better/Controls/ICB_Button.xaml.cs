using iNKORE.UI.WPF.Controls;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Ink_Canvas_Better.Controls
{
    /// <summary>
    /// ICB_Button
    /// </summary>
    public partial class ICB_Button : UserControl
    {
        private FontIcon fontIcon;
        private readonly int _Squeeze = 5;
        private bool _IsSqueezeHorizontally = false;
        private bool _IsShowText = true;
        private int _CornerRadius;

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
                    Width -= _Squeeze;
                    SimpleStackPanel_1.Width -= _Squeeze;
                    TextBox_1.Width -= _Squeeze;
                    InnerButton.Width -= _Squeeze;
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

        /// <summary>
        /// 文字图标
        /// </summary>
        public String FontIcon
        {
            set {
                SetFontIcon(value);
            }
        }

        public void SetFontIcon(String Glyph)
        {
            SimpleStackPanel_1.Children.Clear();
            fontIcon = new FontIcon(Glyph, (FontFamily)System.Windows.Application.Current.Resources["FluentIconFontFamily"]);
            SimpleStackPanel_1.Children.Add(fontIcon);
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
                fontIcon.Height = SimpleStackPanel_1.Height;
                if (SimpleStackPanel_1.Children[0] is FontIcon)
                {
                    fontIcon.FontSize = fontIcon.Height - 10;
                }
            }
            else
            {
                TextBox_1.Visibility = Visibility.Collapsed;
                SimpleStackPanel_1.Height = 43;
                fontIcon.Height = SimpleStackPanel_1.Height;
                if (SimpleStackPanel_1.Children[0] is FontIcon)
                {
                    fontIcon.FontSize = fontIcon.Height - 10;
                }
            }
        }

    }
}

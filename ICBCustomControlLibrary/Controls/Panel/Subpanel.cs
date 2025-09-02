using ICBCustomControlLibrary.Themes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

[assembly: XmlnsDefinition("ICBCustomControlLibrary", "ICBCustomControlLibrary.Controls.Panel")]
namespace ICBCustomControlLibrary.Controls.Panel
{
    [ContentProperty("Child")]
    [TemplatePart(Name = "PART_PinButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class Subpanel : Popup
    {
        static Subpanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Subpanel), new FrameworkPropertyMetadata(typeof(Subpanel)));
        }

        #region Properties

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Subpanel), new PropertyMetadata("Title"));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ShowHeaderProperty =
            DependencyProperty.Register("ShowHeader", typeof(bool), typeof(Subpanel),
                new PropertyMetadata(true));

        public bool ShowHeader
        {
            get { return (bool)GetValue(ShowHeaderProperty); }
            set { SetValue(ShowHeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(Subpanel), new PropertyMetadata(Brushes.LightGray));

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        #endregion

        #region Controls

        // title
        private readonly TextBlock _titleTextBlock = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10, 0, 0, 0),
            FontWeight = FontWeights.Bold
        };

        // close button
        private readonly Button _closeButton = new Button
        {
            Background = Brushes.Transparent,
            FontFamily = ThemeHelper.SegoeFluentIcons,
            Content = "\ue8bb",
            Width = 30,
            BorderThickness = new Thickness(0)
        };
        protected virtual void OnCloseButtonClicked(Object s, RoutedEventArgs args)
        {
            this.IsOpen = false;
        }

        // pin button
        private readonly Button _pinButton = new Button
        {
            Background = Brushes.Transparent,
            FontFamily = ThemeHelper.SegoeFluentIcons,
            Content = "\ue718",
            Width = 30,
            BorderThickness = new Thickness(0)
        };
        protected virtual void OnPinButtonClicked(Object s, RoutedEventArgs args)
        {
            StaysOpen = !StaysOpen;
            _pinButton.Content = StaysOpen ? "\ue77a" : "\ue718";
        }

        // title bar
        private readonly Grid _headerGrid = new Grid
        {
            Height = 30,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        private readonly Border _mainBorder = new Border
        {
            Background = (Brush)ThemeHelper.Dictionary["DefaultBackgroundColor"],
            BorderBrush = (Brush)ThemeHelper.Dictionary["BorderBrush"],
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(4)
        };

        #endregion

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _titleTextBlock.Text = Title;

            _headerGrid.Background = HeaderBackground;
            _headerGrid.Children.Add(_titleTextBlock);
            Grid.SetColumn(_titleTextBlock, 0);

            _pinButton.Click += OnPinButtonClicked;
            _headerGrid.Children.Add(_pinButton);
            Grid.SetColumn(_pinButton, 1);

            _closeButton.Click += OnCloseButtonClicked;
            _headerGrid.Children.Add(_closeButton);
            Grid.SetColumn(_closeButton, 2);

            var contentPresenter = new ContentPresenter();
            var contentBinding = new Binding("Child") { Source = this.MemberwiseClone() };
            contentPresenter.SetBinding(ContentPresenter.ContentProperty, contentBinding);

            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            mainGrid.Children.Add(_headerGrid);
            Grid.SetRow(_headerGrid, 0);

            mainGrid.Children.Add(contentPresenter);
            Grid.SetRow(contentPresenter, 1);

            var visibilityBinding = new Binding("ShowHeader")
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            };
            _headerGrid.SetBinding(VisibilityProperty, visibilityBinding);

            _mainBorder.Child = mainGrid;
            this.Child = _mainBorder;
        }

        public void Show()
        {
            this.IsOpen = true;
        }

        public void Hide()
        {
            this.IsOpen = false;
        }
    }

}

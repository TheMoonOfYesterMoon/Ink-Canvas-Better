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

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var mainBorder = new Border
            {
                Background = (Brush)ThemeHelper.Dictionary["DefaultBackgroundColor"],
                BorderBrush = (Brush)ThemeHelper.Dictionary["BorderBrush"],
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(4)
            };

            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            // title bar
            var headerGrid = new Grid
            {
                Background = HeaderBackground,
                Height = 30
            };
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            // title
            var titleTextBlock = new TextBlock
            {
                Text = Title,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0),
                FontWeight = FontWeights.Bold
            };

            // pin button
            var pinButton = new Button
            {
                FontFamily = ThemeHelper.SegoeFluentIcons,
                Content = "\ue718",
                Width = 30,
            };
            pinButton.Click += (s, args) =>
            {
                StaysOpen = !StaysOpen;
                if (StaysOpen)
                {
                    pinButton.Content = "\ue77a";
                }
                else
                {
                    pinButton.Content = "\ue718";
                }
            };

            // close button
            var closeButton = new Button
            {
                FontFamily = ThemeHelper.SegoeFluentIcons,
                Content = "\ue8bb",
                Width = 30,
            };
            closeButton.Click += (s, args) => { this.IsOpen = false; };

            headerGrid.Children.Add(titleTextBlock);
            Grid.SetColumn(titleTextBlock, 0);

            headerGrid.Children.Add(pinButton);
            Grid.SetColumn(pinButton, 1);

            headerGrid.Children.Add(closeButton);
            Grid.SetColumn(closeButton, 2);

            var contentPresenter = new ContentPresenter();

            var contentBinding = new Binding("Child") { Source = this.MemberwiseClone() };
            contentPresenter.SetBinding(ContentPresenter.ContentProperty, contentBinding);

            mainGrid.Children.Add(headerGrid);
            Grid.SetRow(headerGrid, 0);

            mainGrid.Children.Add(contentPresenter);
            Grid.SetRow(contentPresenter, 1);

            var visibilityBinding = new Binding("ShowHeader")
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            };
            headerGrid.SetBinding(VisibilityProperty, visibilityBinding);

            mainBorder.Child = mainGrid;
            this.Child = mainBorder;
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

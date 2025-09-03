using System;
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
    public partial class Subpanel : Popup
    {
        static Subpanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Subpanel), new FrameworkPropertyMetadata(typeof(Subpanel)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _titleTextBlock.Text = Title;

            _titleBarGrid.Background = HeaderBackground;
            _titleBarGrid.Children.Add(_titleTextBlock);
            Grid.SetColumn(_titleTextBlock, 0);

            _pinButton.Content = StaysOpen ? "\ue77a" : "\ue718";
            _pinButton.Click += OnPinButtonClicked;
            _titleBarGrid.Children.Add(_pinButton);
            Grid.SetColumn(_pinButton, 1);

            _closeButton.Click += OnCloseButtonClicked;
            _titleBarGrid.Children.Add(_closeButton);
            Grid.SetColumn(_closeButton, 2);

            var contentBinding = new Binding("Child") { Source = this.MemberwiseClone() };
            _contentPresenter.SetBinding(ContentPresenter.ContentProperty, contentBinding);

            _mainGrid.Children.Add(_titleBarGrid);
            Grid.SetRow(_titleBarGrid, 0);

            _mainGrid.Children.Add(_contentPresenter);
            Grid.SetRow(_contentPresenter, 1);

            var visibilityBinding = new Binding("ShowHeader")
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            };
            _titleBarGrid.SetBinding(VisibilityProperty, visibilityBinding);

            _mainBorder.Child = _mainGrid;
            this.Child = _mainBorder;
        }
    }
}

using ICBCustomControlLibrary.Helpers;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ICBCustomControlLibrary.Controls.Panel
{
    public partial class Subpanel
    {
        #region TitleBar

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

        // pin button
        private readonly Button _pinButton = new Button
        {
            Background = Brushes.Transparent,
            FontFamily = ThemeHelper.SegoeFluentIcons,
            Content = "\ue718",
            Width = 30,
            BorderThickness = new Thickness(0)
        };

        // title bar
        private readonly Grid _titleBarGrid = new Grid
        {
            Height = 30,
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        #endregion

        private readonly Border _mainBorder = new Border
        {
            MinHeight = 30,
            Background = ThemeHelper.DefaultBackgroundColor,
            BorderBrush = ThemeHelper.DefaultBorderColor,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(4)
        };

        private readonly Grid _mainGrid = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            }
        };

        private readonly ContentPresenter _contentPresenter = new ContentPresenter
        {
            Margin = new Thickness(5)
        };
    }
}

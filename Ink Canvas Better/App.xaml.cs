using System.Configuration;
using System.Data;
using System.Windows;

namespace Ink_Canvas_Better
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[] StartArgs = null;
        public static readonly string RootPath = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas Better\\";

    }

}

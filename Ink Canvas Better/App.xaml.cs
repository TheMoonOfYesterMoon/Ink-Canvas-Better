using System.Configuration;
using System.Data;
using System.Windows;
using Ink_Canvas_Better.Model;
using Ink_Canvas_Better.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ink_Canvas_Better
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[] StartArgs = [];
        public static readonly string RootPath = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas Better\\";
        public const string SettingsFileName = "settings.json";

        public static T GetService<T>() => IAppHost.GetService<T>();

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            IAppHost.Host = Host
                .CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<SettingsService>();
                    // views
                    services.AddSingleton<MainWindow>();
                }).Build();

        }
    }
}

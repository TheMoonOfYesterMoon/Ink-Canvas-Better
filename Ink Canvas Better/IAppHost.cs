using Microsoft.Extensions.Hosting;

namespace Ink_Canvas_Better;

public interface IAppHost
{
    public static IHost? Host;

    public static T GetService<T>()
    {
        var s = Host?.Services.GetService(typeof(T));
        if (s != null)
        {
            return (T)s;
        }
        throw new ArgumentException($"Service {typeof(T)} is null!");
    }

    public static T? TryGetService<T>()
    {
        return (T?)Host?.Services.GetService(typeof(T));
    }
}

using Megumi.Core.Logging;

namespace Megumi.Core.Extensions;

public static class CoreServiceExtensions
{
    public static void RegisterLogging(this IServiceCollection collection)
    {
        collection.AddLogging(x =>
        {
            x.ClearProviders();
#if DEBUG
            x.SetMinimumLevel(LogLevel.Trace);
            x.AddZLoggerConsole(o =>
            {
                o.ConfigureEnableAnsiEscapeCode = true;
                o.UseFormatter(() => new LogConsoleFormatter());
            });
#else
            x.SetMinimumLevel(LogLevel.Information);
#endif
            x.AddZLoggerFile((o, _) =>
            {
                o.FileShared = true;
                o.UseFormatter(() => new LogFileFormatter());
                return "./log.txt";
            });
        });
    }
}

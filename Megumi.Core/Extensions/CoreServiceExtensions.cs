using AleRoe.LiteDB.Extensions.DependencyInjection;
using Megumi.Core.Logging;
using Megumi.Core.ViewModels;

namespace Megumi.Core.Extensions;

public static class CoreServiceExtensions
{
    /// <summary>
    /// Register ZLogger logging.
    /// </summary>
    /// <param name="collection"></param>
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

    /// <summary>
    /// Register core services.
    /// </summary>
    /// <param name="collection"></param>
    public static void RegisterCoreServices(this IServiceCollection collection)
    {
        collection.AddLiteDatabase(o =>
        {
            o.ConnectionString.Filename = "./data.db";
            // Need shared because access will happen on different threads
            o.ConnectionString.Connection = ConnectionType.Shared;
        });
        collection.AddSingleton<IDatabaseService, DatabaseService>();
    }

    /// <summary>
    /// Add View Models to service collection.
    /// </summary>
    /// <param name="collection"></param>
    public static void AddViewModels(this IServiceCollection collection)
    {
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<ExplorerViewModel>();
    }

    /// <summary>
    /// Add <see cref="ProviderService"/> and load file providers.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="providerDirectory">Location of file provider dlls.</param>
    public static void AddFileService(this IServiceCollection collection, string providerDirectory)
    {
        collection.AddSingleton(provider =>
        {
            var loggerFactory = provider.GetService<ILoggerFactory>();
            var providers = FileProviderLoader.LoadProviders(providerDirectory, loggerFactory!);
            return new ProviderService(providers);
        });
    }
}

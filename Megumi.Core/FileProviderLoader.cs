using System.Reflection;
using Megumi.Core.Interfaces;

namespace Megumi.Core;

/// <summary>
/// Loader for file providers using reflection.
/// </summary>
public static class FileProviderLoader
{
    /// <summary>
    /// Load file providers from directory.
    /// </summary>
    /// <param name="providerDirectory">Directory to load providers from.</param>
    /// <param name="loggerFactory">Logger factory for providing loggers to providers.</param>
    /// <returns>Dictionary with provider prefix keys, and instance values.</returns>
    public static Dictionary<string, IFileProvider> LoadProviders(string providerDirectory, ILoggerFactory loggerFactory)
    {
        var providers = new Dictionary<string, IFileProvider>();
        var log = loggerFactory.CreateLogger(typeof(FileProviderLoader));

        if (!Directory.Exists(providerDirectory))
        {
            log.ZLogError($"Provider directory {providerDirectory} does not exist. No providers loaded.");
            return providers;
        }

        foreach (var dllFile in Directory.GetFiles(providerDirectory, "*.dll"))
        {
            var assembly = Assembly.LoadFrom(dllFile);
            log.ZLogInformation($"Loading provider from {dllFile}");
            var fileProviderTypes = assembly.GetTypes().Where(t => typeof(IFileProvider).IsAssignableFrom(t) && t is { IsAbstract: false, IsClass: true });

            foreach (var type in fileProviderTypes)
            {
                // TODO: It would be preferred to create ILogger<T> and pass that instead of the ILoggerFactory and relying
                // on the provider to create it's own logger. Also is a connection to the database needed for network accounts, etc.?
                //var loggerType = typeof(ILogger<>).MakeGenericType(type);
                //var logger = loggerFactory.CreateLogger(type);
                
                // Find constructor that accepts ILoggerFactory
                var constructor = type.GetConstructors().FirstOrDefault(c =>
                {
                    var parameters = c.GetParameters();
                    return parameters.Length == 1 && parameters[0].ParameterType == typeof(ILoggerFactory);
                });
                
                if (constructor == null)
                {
                    log.ZLogWarning($"No suitable constructor found for {type.FullName}");
                    continue;
                }
                
                var instance = (IFileProvider)constructor.Invoke([loggerFactory]);
                if (!providers.TryAdd(instance.Prefix, instance))
                {
                    log.ZLogWarning($"Provider for {instance.Prefix} is already loaded.");
                }
            }
        }
        
        return providers;
    }
}

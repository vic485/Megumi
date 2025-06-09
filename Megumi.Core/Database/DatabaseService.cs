namespace Megumi.Core.Database;

/// <summary>
/// LiteDB database handler.
/// </summary>
/// <param name="logger"></param>
public class DatabaseService(ILogger<DatabaseService> logger, LiteDatabase database) : IDatabaseService, IDisposable
{
    public Configuration Config { get; private set; }

    /// <summary>
    /// Connect to database.
    /// </summary>
    public void Initialize()
    {
        logger.ZLogInformation($"Loading configuration from database");
        var col = database.GetCollection<Configuration>("AppConfig");
        Config = col.Query().FirstOrDefault() ?? new Configuration();
    }

    public void SaveConfig()
    {
        var col = database.GetCollection<Configuration>("AppConfig");
        col?.Upsert(Config);
    }

    public void Dispose()
    {
        database.Dispose();
        GC.SuppressFinalize(this);
    }
}

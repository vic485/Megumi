namespace Megumi.Core.Database;

public interface IDatabaseService
{
    Configuration Config { get; }
    void Initialize();

    void SaveConfig();
}

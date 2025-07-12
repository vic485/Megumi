using Megumi.Core.Interfaces;

namespace Megumi.Core;

/// <summary>
/// Service for connecting actions to a file/directory to the appropriate provider(s).
/// </summary>
public class ProviderService
{
    private Dictionary<string, IFileProvider> _providers;

    public ProviderService(Dictionary<string, IFileProvider> providers)
    {
        _providers = providers;
    }

    private IFileProvider ResolveProvider(string path)
    {
        throw new NotImplementedException();
    }
}

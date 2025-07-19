using System.Text.RegularExpressions;
using Megumi.Core.Interfaces;
using Megumi.Core.Models;

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

    public async Task<DirectoryObject> GetDirectory(string path)
    {
        var provider = ResolveProvider(path);
        return await provider.GetDirectoryAsync(path);
    }

    private IFileProvider ResolveProvider(string path)
    {
        var regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9+.-]*://");
        var match = regex.Match(path);
        if (match.Success && _providers.TryGetValue(match.Value, out var provider))
            return provider;

        throw new NotSupportedException(match.Success
            ? $"No provider for {match.Value}"
            : "Unknown filesystem scheme.");
    }
}

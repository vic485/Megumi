using Megumi.Core.Interfaces;
using Megumi.Core.Models;
using Microsoft.Extensions.Logging;
using ZLogger;

namespace Megumi.Provider.FileSystem;

/// <summary>
/// Provider for local filesystem access.
/// </summary>
public class FileSystemProvider : IFileProvider
{
    public string Prefix => "file://";

    private readonly ILogger<FileSystemProvider> _logger;

    public FileSystemProvider(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<FileSystemProvider>();
        _logger.ZLogTrace($"Constructor called.");
    }

    public async Task<DirectoryObject> GetDirectoryAsync(string path)
    {
        _logger.ZLogTrace($"Loading directory: {path}");
        // Remove prefix to work with system.
        var fsPath = path[Prefix.Length..];
        return await Task.Run(() =>
        {
            var directory = new DirectoryObject(fsPath);
            var subDirs = Directory.GetDirectories(fsPath).Select(subDir => new DirectoryObject(subDir)).ToList();

            directory.Directories = subDirs;
            var files = Directory.GetFiles(fsPath).Select(file => new FileObject(file)).ToList();

            directory.Files = files;
            return directory;
        });
    }
}

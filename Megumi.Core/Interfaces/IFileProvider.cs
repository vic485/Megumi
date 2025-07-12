using Megumi.Core.Models;

namespace Megumi.Core.Interfaces;

/// <summary>
/// Base interface for file providers
/// </summary>
public interface IFileProvider
{
    /// <summary>
    /// Path prefix to specify protocol handling.
    /// </summary>
    string Prefix { get; }

    /// <summary>
    /// Load a directory.
    /// </summary>
    /// <param name="path">Full path to directory, including provider prefix.</param>
    /// <returns><see cref="DirectoryObject"/> of the loaded directory.</returns>
    Task<DirectoryObject> GetDirectoryAsync(string path);
}

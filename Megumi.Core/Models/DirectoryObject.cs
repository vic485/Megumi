using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Megumi.Core.Models;

public class DirectoryObject : FileSystemObject
{
    public DirectoryObject(string path)
    {
        Path = path;
        Name = System.IO.Path.GetFileName(path);
        Icon = new Bitmap(AssetLoader.Open(new Uri("avares://Megumi/Assets/FileTypes/directory.png")));
        var info = new DirectoryInfo(path);
        Size = 0;
        SizeString = ByteSize(Size);
        Type = FileSystemObjectType.Directory;
        Created = info.CreationTime;
        Modified = info.LastWriteTime;
        Accessed = info.LastAccessTime;
        var attributes = File.GetAttributes(path);
        IsHidden = attributes.HasFlag(FileAttributes.Hidden) || Name.StartsWith('.');
        IsSystem = attributes.HasFlag(FileAttributes.System);
        IsSymbolic = attributes.HasFlag(FileAttributes.ReparsePoint);
    }
}

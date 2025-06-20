using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Megumi.Core.Models;

public class FileObject : FileSystemObject
{
    public FileObject(string path)
    {
        Path = path;
        Name = System.IO.Path.GetFileName(path);
        Icon = new Bitmap(AssetLoader.Open(new Uri("avares://Megumi/Assets/FileTypes/file.png")));
        var info = new FileInfo(path);
        Size = info.Length;
        SizeString = ByteSize(Size);
        Type = FileSystemObjectType.File;
        Created = info.CreationTime;
        Modified = info.LastWriteTime;
        Accessed = info.LastAccessTime;
        var attributes = File.GetAttributes(path);
        IsHidden = attributes.HasFlag(FileAttributes.Hidden) || Name.StartsWith('.');
        IsSystem = attributes.HasFlag(FileAttributes.System);
        IsSymbolic = attributes.HasFlag(FileAttributes.ReparsePoint);
    }
}

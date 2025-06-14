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
    }
}

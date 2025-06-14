using Avalonia.Media.Imaging;
using ReactiveUI;

namespace Megumi.Core.Models;

public class FileSystemObject : ReactiveObject
{
    public string Name { get; set; }
    
    public string Path { get; set; }
    
    public Bitmap Icon { get; set; }
}

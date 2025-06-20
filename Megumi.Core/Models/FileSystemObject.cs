using Avalonia.Media.Imaging;
using ReactiveUI;

namespace Megumi.Core.Models;

public abstract class FileSystemObject : ReactiveObject
{
    public string Name { get; set; }

    public string Path { get; set; }

    public Bitmap Icon { get; set; }

    public long? Size { get; set; }

    public string SizeString { get; set; }
    
    public FileSystemObjectType Type { get; protected set; }

    public DateTime? Created { get; set; }

    public DateTime? Modified { get; set; }

    public DateTime? Accessed { get; set; }

    public bool IsHidden { get; set; }

    public bool IsSystem { get; set; }

    public bool IsSymbolic { get; set; }
    
    private static readonly IReadOnlyList<string> ByteUnits = ["B", "KiB", "MiB", "GiB", "TiB", "PiB"];
    
    /// <summary>
    /// Convert byte size to the largest unit representation.
    /// </summary>
    /// <param name="size">Size in Bytes.</param>
    /// <returns>Size with byte unit.</returns>
    /// <example>1.0 KiB</example>
    /// <remarks>From https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net/4967106#4967106</remarks>
    protected static string ByteSize(long? size)
    {
        const string formatTemplate = "{0}{1:0.#} {2}";

        //if (!(size > 0))
        if (size is null or 0)
            return string.Format(formatTemplate, null, 0, ByteUnits[0]);

        var absSize = Math.Abs((double)size);
        var fpPower = Math.Log(absSize, 1024);
        var intPower = (int)fpPower;
        var iUnit = intPower >= ByteUnits.Count ? ByteUnits.Count - 1 : intPower;
        var normSize = absSize / Math.Pow(1024, iUnit);

        return string.Format(formatTemplate, size < 0 ? "-" : null, normSize, ByteUnits[iUnit]);
    }
}

public enum FileSystemObjectType : byte
{
    File,
    Directory
}

using System.Buffers;
using Utf8StringInterpolation;

namespace Megumi.Core.Logging;

/// <summary>
/// Formatter for logging to a file.
/// </summary>
public class LogFileFormatter : IZLoggerFormatter
{
    /// <summary>
    /// Entry formatter.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="entry"></param>
    public void FormatLogEntry(IBufferWriter<byte> writer, IZLoggerEntry entry)
    {
        using var utf8Writer = new Utf8StringWriter<IBufferWriter<byte>>(writer);

        utf8Writer.Append($"[{entry.LogInfo.Timestamp.Local:HH:mm:ss}] ");
        utf8Writer.Append($"{entry.LogInfo.LogLevel} ({entry.LogInfo.Category}) ");
        utf8Writer.AppendLine(entry.ToString());

        if (entry.LogInfo.Exception is not { } ex)
            return;

        utf8Writer.AppendLine(ex.ToString());
    }

    public bool WithLineBreak => true;
}

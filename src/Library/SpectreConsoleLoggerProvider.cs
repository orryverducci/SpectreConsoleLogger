using System.Collections.Concurrent;

namespace SpectreConsoleLogger;

/// <summary>
/// Provides loggers that output to the console using the <see cref="N:Spectre.Console"/> library.
/// </summary>
public class SpectreConsoleLoggerProvider : ILoggerProvider
{
    /// <summary>
    /// Determines the logging style that should be used by the loggers.
    /// </summary>
    public Style Style { get; set; } = Style.Standard;

    /// <summary>
    /// The directory of loggers for each category.
    /// </summary>
    private readonly ConcurrentDictionary<string, SpectreConsoleLogger> _loggers = new();

    /// <inheritdoc/>
    public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, new SpectreConsoleLogger(categoryName, Style));

    /// <summary>
    /// Releases all resources used by the <see cref="SpectreConsoleLoggerProvider"/> object.
    /// </summary>
    public void Dispose() => _loggers.Clear();
}

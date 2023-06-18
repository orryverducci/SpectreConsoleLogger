namespace SpectreConsoleLogger;

public static class LoggingBuilderExtensions
{
    /// <summary>
    /// Adds a console logger using <see cref="N:Spectre.Console"/> to the factory.
    /// </summary>
    /// <param name="loggingBuilder">The logging builder.</param>
    /// <param name="style">The logging style that should be used.</param>
    /// <returns>The logging builder with <see cref="SpectreConsoleLoggerProvider"/> added to the factory.</returns>
    public static ILoggingBuilder AddSpectreConsole(this ILoggingBuilder loggingBuilder, Style style = Style.Standard)
        => loggingBuilder.AddProvider(new SpectreConsoleLoggerProvider()
        {
            Style = style
        });
}

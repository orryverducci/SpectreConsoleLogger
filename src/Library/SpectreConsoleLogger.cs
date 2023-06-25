namespace SpectreConsoleLogger;

/// <summary>
/// A logger that outputs to the console using the <see cref="N:Spectre.Console"/> library.
/// </summary>
public class SpectreConsoleLogger : ILogger
{
    /// <summary>
    /// The name of the category the logger is for.
    /// </summary>
    private readonly string _categoryName;

    /// <summary>
    /// The logging style that should bse used by the logger.
    /// </summary>
    private readonly Style _style;

    /// <summary>
    /// Initialises a new instance of the <see cref="SpectreConsoleLogger"/> class.
    /// </summary>
    /// <param name="categoryName">The name of the category the logger is for.</param>
    /// <param name="style">The logging style that should be used by the logger.</param>
    public SpectreConsoleLogger(string categoryName, Style style)
    {
        _categoryName = categoryName;
        _style = style;
    }

    /// <inheritdoc/>
    public IDisposable BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        // Check if enabled
        if (!IsEnabled(logLevel))
        {
            return;
        }

        string levelText = string.Empty;
        string levelColour = string.Empty;
        string levelBackground = string.Empty;

        // Set the log level text and colour
        switch (logLevel)
        {
            case LogLevel.Trace:
                levelText = "Trace";
                levelColour = "tan";
                break;
            case LogLevel.Debug:
                levelText = "Debug";
                levelColour = "chartreuse3";
                break;
            case LogLevel.Information:
                levelText = "Info";
                levelColour = "dodgerblue1";
                break;
            case LogLevel.Warning:
                levelText = "Warn";
                levelColour = "darkorange";
                break;
            case LogLevel.Error:
                levelText = "Error";
                levelColour = "red3_1";
                break;
            case LogLevel.Critical:
                levelText = "Fatal";
                levelColour = "white";
                levelBackground = " on red3_1";
                break;
        }

        // Create the table to hold the time, level and message
        Table table = new();
        table.Border(TableBorder.None)
                .HideHeaders()
                .AddColumn("Time")
                .AddColumn("Level")
                .AddColumn("Message");

        // Add the rows to the table containing the information
        if (_style == Style.Extended)
        {
            table.AddRow($"[grey]{DateTime.Now.ToString("HH:mm:ss")}[/]",
                $"[[[bold {levelColour}{levelBackground}]{levelText.PadRight(5)}[/]]]",
                $"[bold]{_categoryName.EscapeMarkup()}:[/]");

            table.AddRow($"[grey]{DateTime.Now.ToString("zzz")}[/]", string.Empty, formatter(state, exception).EscapeMarkup());
        }
        else
        {
            table.AddRow($"[grey]{DateTime.Now.ToString("HH:mm:ss")}[/]",
                $"[[[bold {levelColour}{levelBackground}]{levelText.PadRight(5)}[/]]]",
                formatter(state, exception).EscapeMarkup());
        }

        if (exception != null)
        {
            ExceptionFormats exceptionFormat = ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes | ExceptionFormats.ShortenMethods;
            table.AddRow(new Text(string.Empty), new Text(string.Empty), exception.GetRenderable(exceptionFormat));
        }

        // Render the outer table to the console
        AnsiConsole.Write(table);
    }
}

using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace SpectreConsoleLogger.DemoApp;

internal class DemoService : IHostedService
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// The timer which logs a test message every 2 seconds.
    /// </summary>
    private System.Timers.Timer _timer;

    /// <summary>
    /// Initialises a new instance of the <see cref="DemoService"/> class.
    /// </summary>
    /// <param name="logger">The logger provided by the host.</param>
    public DemoService(ILogger<DemoService> logger) => _logger = logger;

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new System.Timers.Timer(2000);
        _timer.Elapsed += LogMessage;
        _timer.AutoReset = true;
        _timer.Enabled = true;

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Enabled = false;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Logs a test message whenever the timer elapses.
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="e">The event arguments.</param>
    private void LogMessage(object? sender, ElapsedEventArgs e) => _logger.LogInformation("This is a test log message");
}

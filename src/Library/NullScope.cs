namespace SpectreConsoleLogger;

/// <summary>
/// Provides a dummy scope to use with a logger that doesn't support scopes.
/// </summary>
internal sealed class NullScope : IDisposable
{
    internal static NullScope Instance { get; } = new NullScope();

    private NullScope()
    {
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }
}

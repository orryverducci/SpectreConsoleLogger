using Microsoft.Extensions.DependencyInjection;

namespace SpectreConsoleLogger.DemoApp;

internal static class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Logging.ClearProviders().AddSpectreConsole(Style.Extended).SetMinimumLevel(LogLevel.Trace);

        builder.Services.AddHostedService<DemoService>();

        IHost app = builder.Build();

        app.Run();
    }
}

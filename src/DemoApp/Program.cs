using Microsoft.Extensions.DependencyInjection;

namespace SpectreConsoleLogger.DemoApp;

internal class Program
{
    public static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostContext, builder) =>
            {
                builder.ClearProviders().AddSpectreConsole(Style.Extended);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<DemoService>();
            })
            .Build();

        host.Run();
    }
}

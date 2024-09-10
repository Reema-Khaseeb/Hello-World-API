using Serilog;

namespace HelloWorldAPI;

public static class CustomLoggerConfigurationExtensions
{
    public static void ConfigureLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}

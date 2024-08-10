using Serilog;

namespace Web.API.Extensions;

public static class LoggerExtensions
{
    public static void ApplyLoggerConfig(this WebApplicationBuilder builder)
    {

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("seriLogs/catalog.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        builder.Logging.AddSerilog();

    }
}

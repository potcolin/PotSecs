using Serilog;

namespace PotSecs.Core.Ref
{
    public static class SerilogConfig
    {
        public static ILogger CreateLogger(string? logDirectory = null)
        {
            if (string.IsNullOrWhiteSpace(logDirectory))
            {
                logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
            }

            Directory.CreateDirectory(logDirectory);

            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File(
                    Path.Combine(logDirectory, "app-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    shared: true)
                .CreateLogger();
        }
    }
}

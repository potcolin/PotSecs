using Serilog;

namespace PotSecs.Core.Ref
{
    /// <summary>
    /// Serilog 日志的统一配置入口：同时输出到 Console、Debug 以及滚动 .log 文本文件。
    /// </summary>
    public static class SerilogConfig
    {
        /// <summary>
        /// 创建写往 Console / Debug / 滚动 .log 文件的日志实例。
        /// </summary>
        /// <param name="logDirectory">日志文件目录，默认取应用基目录下的 logs 子目录。</param>
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

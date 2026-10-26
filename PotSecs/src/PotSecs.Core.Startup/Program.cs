using PotSecs.Core.Ref;
using Serilog;

namespace PotSecs.Core.Startup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = SerilogConfig.CreateLogger();
            try
            {
                Log.Information("应用程序启动");
                Log.Debug("这是一条 Debug 日志，用于验证 Debug 输出");
                Console.WriteLine("Hello, World!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

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
                Log.Information("start application.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

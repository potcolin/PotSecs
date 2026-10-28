using Microsoft.Extensions.Options;
using Secs4Net;
using Secs4Net.Sml;
using Serilog;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Secs4Net.Item;

namespace PotSecs.Device.Stocker.wpf
{
    internal sealed class ConsoleSecsLogger : ISecsGemLogger
    {
        public void MessageIn(SecsMessage msg, int id)
            => Log.Information($"<-- [0x{id:X8}] {msg.ToSml()}");

        public void MessageOut(SecsMessage msg, int id)
            => Log.Information($"--> [0x{id:X8}] {msg.ToSml()}");

        public void Debug(string msg)
            => Log.Information($"[DEBUG] {msg}");

        public void Info(string msg)
            => Log.Information($"[INFO] {msg}");

        public void Warning(string msg)
            => Log.Information($"[WARN] {msg}");

        public void Error(string msg, SecsMessage? message, Exception? ex)
        {
            Log.Information($"[ERROR] {msg}");

            if (message is not null)
                Log.Information(message.ToSml());

            if (ex is not null)
                Log.Information(ex, "exception happened.");
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


            var options = Options.Create(new SecsGemOptions
            {
                IsActive = false,       // 被动模式/passive：等待 EAP 连接
                IpAddress = "0.0.0.0",  // 监听本机所有 IPv4 网卡
                Port = 5000,            // 必须与 EAP 配置一致
                DeviceId = 0,
                T3 = 45000
            });

            var logger = new ConsoleSecsLogger();

            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            await using var connection = new HsmsConnection(options, logger);
            using var secsGem = new SecsGem(options, connection, logger);

            connection.ConnectionChanged += delegate
            {
                Log.Information($"HSMS state ：{connection.State}");
            };

            connection.Start(cts.Token);

            Log.Information("waiting for EAP connection. press Ctrl+C to exit.");

            try
            {
                await foreach (var wrapper in
                    secsGem.GetPrimaryMessageAsync(cts.Token))
                {
                    using var primary = wrapper.PrimaryMessage;

                    Log.Information($"receive EAP message：S{primary.S}F{primary.F}");

                    // 输出消息的基本信息；需要 SML 格式时可使用
                    // Secs4Net.Sml 扩展 ToSml()。

                    if (primary.S == 1 && primary.F == 1)
                    {
                        using var reply = new SecsMessage(1, 2)
                        {
                            SecsItem = L(
                                A("StockerSimulator"),
                                A("1.0.0"))
                        };

                        await wrapper.TryReplyAsync(reply, cts.Token);
                        Log.Information("replay S1F2");
                    }
                    else
                    {
                        Log.Information($"Business processing for S{primary.S}F{primary.F} is not implemented yet.");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Log.Information("Listening stopped.");
            }
        }
    }
}
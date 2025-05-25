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
using CommonSDK.AI.ChatClient;
using CommonSDK.AI.Ollama;
using ICSharpCode.AvalonEdit;

namespace CommonSDK.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly OllamaService service = new("D:\\RiderProject\\CommonSDK\\CommonSDK.Application\\OllamaConfiguration.json");

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                // use on service
                //await service.RunAsync();

                //AI.ChatClient.ChatResponse response = await service.ChatAsync("Recommend a few must-read books for programmers.");

                //ChatTextBlock.Text += response.Data.Message.Content;

                //await service.ChatStreamAsync("Recommend a few must-read books for programmers.", (message) =>
                //{
                //    ChatTextBlock.Text += message;
                //});

                // use on client
                IChatClient client = new OllamaChatClient("http://localhost:8000", "llama3.2");
                CancellationTokenSource tokenSource = new();
                await foreach (var item in client.ChatStreamAsync("Recommend a few must-read books for programmers.", tokenSource.Token))
                {
                    if (item.Code == ChatResultCode.Success)
                    {
                        textEditor.Text += item.Data.Message.Content;
                        //ChatTextBlock.Text += item.Data.Message.Content;
                    }
                }
            });
        }

        private async void MainWindow_Closed(object? sender, EventArgs e)
        {
            await service.DisposeAsync();
        }
    }
}
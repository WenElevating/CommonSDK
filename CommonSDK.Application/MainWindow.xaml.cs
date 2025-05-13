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
using CommonSDK.AI.Ollama;

namespace CommonSDK.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly OllamaService service = new("http://localhost:8000", "llama3.2");

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
                await service.RunAsync();

                await service.ChatStreamAsync("Recommend a few must-read books for programmers.", (text) =>
                {
                    ChatTextBlock.Text += text;
                });
            });
        }

        private async void MainWindow_Closed(object? sender, EventArgs e)
        {
            await service.DisposeAsync();
        }
    }
}
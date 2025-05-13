## Goal: Write a universal development toolkit
#### Includes the following directory:
- CommonSDK
  - AI
	- Ollama
		- IChatService<T>
		- OllamaService
  - EventBus
	- Interface
	- Model
	- Service
  - README.md

1. The AI module only supports Ollama and provides the IChatService<T> interface and the OllamaService class. The IChatService<T> interface defines the basic operations of the chat service, including methods for sending messages, receiving messages, and retrieving chat history. The OllamaService class implements the IChatService<T> interface and provides the specific interaction logic with the Ollama chat model.

2. The event bus provides a communication method between modules, offering the BaseEventSource<T> class and the EventBusService class. The BaseEventSource<T> class defines the basic operations of the event source, including methods for registering, unregistering, and triggering events. The EventBusService class implements the BaseEventSource<T> interface and provides the specific interaction logic with the event bus.

3. The remaining modules are still under development, please do not use.

#### The following is an example of usage: (based on the WPF framework)
``` c#
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
                    ChatTextBlock.Text = (ChatTextBlock.Text += text);
                });
            });
        }

        private async void MainWindow_Closed(object? sender, EventArgs e)
        {
            await service.DisposeAsync();
        }
    }
```
##### Provide a running screenshot:
<img width="591" alt="Run Sample" src="https://github.com/user-attachments/assets/eb099a24-e941-40e8-8512-1eaac24fdd81" />

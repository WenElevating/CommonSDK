## Goal: Write a universal development toolkit
#### Includes the following directory:
- CommonSDK
  - AI
    - Enum
        - AIPlatform
    - Interface
        - IChatService<T>
        - ICommandService
	- Ollama
		- OllamaService
        - OllamaCommandService
  - EventBus
	- Interface
	- Model
	- Service
  - README.md

1. The AI module only supports Ollama and provides the IChatService<T> interface and the OllamaService class. The IChatService<T> interface defines the basic operations of the chat service, including methods for sending messages, receiving messages, and retrieving chat history. The OllamaService class implements the IChatService<T> interface and provides the specific interaction logic with the Ollama chat model.

2. The event bus provides a communication method between modules, offering the BaseEventSource<T> class and the EventBusService class. The BaseEventSource<T> class defines the basic operations of the event source, including methods for registering, unregistering, and triggering events. The EventBusService class implements the BaseEventSource<T> interface and provides the specific interaction logic with the event bus.

3. Microsoft.Extensions.AI and Microsoft.Extensions.AI.Ollama isn't use, I have now rewritten the request logic to refer to the API documentation provided by Ollama. Currently only ChatAsync calls are supported, and the Stream implementation is under way. ollama chat API interface, providing large model question-answering capabilities. This interface is time-consuming, with an average time of 20 to 30ms

4. The remaining modules are still under development, please do not use.

5. Only the call to load a local JSON configuration file is supported now. Here is an example:
```json
{
    "ModelId": "llama3.2",
    "Endpoint": "http://localhost:8000",
    "ExectuePath":  "D://example"
}
```

#### The following is an example of usage: (based on the WPF framework)
``` c#
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
                await service.RunAsync();

                //await service.ChatStreamAsync("Recommend a few must-read books for programmers.", (text) =>
                //{
                //    ChatTextBlock.Text += text;
                //});

                AI.ChatClient.ChatResponse response = await service.ChatAsync("Recommend a few must-read books for programmers.");
                ChatTextBlock.Text += response.Data.Message.Content;
            });
            
        }

        private async void MainWindow_Closed(object? sender, EventArgs e)
        {
            await service.DisposeAsync();
        }
    }
```
##### Provide a running screenshot:
![Run Sample](.\sample.png)  
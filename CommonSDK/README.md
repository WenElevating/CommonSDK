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
    - ChatClient
        - IChatClient
        - OllamaChatClient
  - EventBus
	- Interface
	- Model
	- Service
  - README.md

1. The AI module only supports Ollama and provides the IChatService<T> interface and the OllamaService class. The IChatService<T> interface defines the basic operations of the chat service, including methods for sending messages, receiving messages, and retrieving chat history. The OllamaService class implements the IChatService<T> interface and provides the specific interaction logic with the Ollama chat model.

2. The event bus provides a communication method between modules, offering the BaseEventSource<T> class and the EventBusService class. The BaseEventSource<T> class defines the basic operations of the event source, including methods for registering, unregistering, and triggering events. The EventBusService class implements the BaseEventSource<T> interface and provides the specific interaction logic with the event bus.

3. Has stopped using Microsoft.Extensions.AI and Microsoft.Extensions.AI.Ollama, now rewrite the logical interface request, to provide the reference Ollama API documentation. ChatStreamAsync、ChatAsync、GetLocalModelsAsync、GetModelInfoAsync calls are currently supported, and implementation of streams is in progress. ollama chat API interface, providing large model question-answering capabilities. It takes a long time to request the interface without streaming, and the average time is 20 ~ 30ms

4. It is now possible to request a question and answer as an OllamaService or OllamaChatClient, and OllamaService supports running the model through code

5. The full source code for this project is available at https://github.com/WenElevating/CommonSDK

6. Support to load a local JSON configuration file is supported now. Here is an example:
```json
{
    "ModelId": "llama3.2",
    "Endpoint": "http://localhost:8000",
    "ExectuePath":  "D://example"
}
```

#### The following is an example usage of the ollama service: (based on the WPF framework)
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
                        ChatTextBlock.Text += item.Data.Message.Content;
                    }
                }
            });
        }

        private async void MainWindow_Closed(object? sender, EventArgs e)
        {
            await service.DisposeAsync();
        }
    }
```
##### Provide a running screenshot:
![Run TextBlock Sample](https://github.com/WenElevating/ImageBed/blob/main/AI/sample.png)  
![Run Markdown Sample](https://github.com/WenElevating/ImageBed/blob/main/AI/sample-markdown-editor.png)
## 目标: 编写一个通用的开发工具包
#### 包含以下模块:
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

1. AI模块目前只支持Ollama，并提供IChatService接口和OllamaService类。IChatService接口定义了聊天服务的基本操作，包括发送消息、接收消息和检索聊天记录的方法。OllamaService类实现了IChatService接口，并提供了与Ollama聊天模型的特定交互逻辑。

2. 事件总线提供模块之间的通信方法，提供BaseEventSource类和EventBusService类。BaseEventSource类定义事件源的基本操作，包括注册、取消注册和触发事件的方法。EventBusService类实现了BaseEventSource接口，并提供了与事件总线的特定交互逻辑。

3. 已停止使用Microsoft.Extensions.AI和Microsoft.Extensions.AI开发工具包。现在通过Ollama API文档重写接口。目前支持全量和流式回复调用。如果使用全量聊天接口需要很长时间才能回复，平均耗时为20 ~ 30ms。

4. 现在可以通过OllamaService或OllamaChatClient请求聊天，OllamaService支持通过代码启动模型

5. 现在支持加载本地JSON配置文件。请看示例：
```json
{
    "ModelId": "llama3.2",
    "Endpoint": "http://localhost:8000",
    "ExectuePath":  "D://example"
}
```

#### 下面是Ollama接口的使用示例：（基于WPF框架）
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
##### 提供运行效果截图:
![Run TextBlock Sample](https://github.com/WenElevating/ImageBed/blob/main/AI/sample.png)  
![Run Markdown Sample](https://github.com/WenElevating/ImageBed/blob/main/AI/sample-markdown-editor.png)


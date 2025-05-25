## 目标: 编写一个通用的开发工具包
#### 包含以下目标:
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

1. AI模块仅支持Ollama平台，并提供IChatService<T>接口和OllamaService类。IChatService<T>接口定义了聊天服务的基本操作，包括发送消息、接收消息和检索聊天记录的方法。OllamaService类实现了IChatService<T>接口，并提供与Ollama聊天模型的具体交互逻辑。

2. 事件总线提供了一种模块之间的通信方法，提供了BaseEventSource<T>类和EventBusService类。BaseEventSource<T>类定义了事件源的基本操作，包括注册、注销和触发事件的方法。EventBusService类实现了BaseEventSource<T>接口，并提供了与事件总线的特定交互逻辑。

3. 剩余的模块仍在开发中，请不要使用。

#### 使用示例如下: (基于WPF框架)
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
##### 提供运行截图:
<img width="591" alt="Run Sample" src="https://github.com/user-attachments/assets/eb099a24-e941-40e8-8512-1eaac24fdd81" />

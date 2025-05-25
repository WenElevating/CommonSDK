using CommonSDK.AI.ChatClient;
using CommonSDK.AI.Configuration;
using CommonSDK.AI.Enum;
using CommonSDK.AI.Factory;
using CommonSDK.AI.Interface;
using CommonSDK.Service;
using CommonSDK.Util;

namespace CommonSDK.AI.Ollama;

public class OllamaService : IChatService<ChatResponse>
{
    private readonly string _endpoint = "http://localhost:11434";

    private readonly string _modelId = "llama3.2";

    private readonly string _ollamaName = "ollama";

    private readonly string ollamaExectueFileName = "ollama.exe";

    private readonly string executeablePath = "";

    private readonly OllamaConfigurationManager configurationManager;

    private readonly IChatClient client;

    private readonly ITerminalService terminalService;

    private readonly ICommandService commandService;

    public OllamaService(string path)
    {
        configurationManager = new OllamaConfigurationManager(path);
        _endpoint = configurationManager.Configuration.Endpoint;
        _modelId = configurationManager.Configuration.ModelId;
        terminalService = TerminalServiceFactory.Create();
        commandService = CommandServiceFactory.Create(AIPlatform.Ollama);
        executeablePath = SystemUtil.GetFileIfExist(SystemUtil.GetApplicationInfo(_ollamaName).InstallLocation, ollamaExectueFileName);
        client = new OllamaChatClient(_endpoint, _modelId);
    }

    /// <summary>
    /// It will run model service
    /// </summary>
    /// <returns></returns>
    public async Task<bool> RunAsync()
    {   
        string command = commandService.GetRunModelCommand(executeablePath, _modelId);

        return await terminalService.ExecuteCommandAsync(command);
    }

    /// <summary>
    /// It will stop model service
    /// </summary>
    /// <returns></returns>
    public async Task<bool> StopAsync()
    {
        string command = commandService.GetStopModelCommand(executeablePath, _modelId);

        return await terminalService.ExecuteCommandAsync(command);
    }

    /// <summary>
    /// Send chat message with async
    /// </summary>
    /// <param name="message">chat message</param>
    /// <returns>ChatResponse, it contain Modelid、ConversationId、Text</returns>
    public async Task<ChatResponse> ChatAsync(string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

        return await client.ChatAsync(message);
    }

    public async Task ChatStreamAsync(string message, Action<string> streamCallback)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

        CancellationTokenSource tokenSource = new();
        await foreach (var item in client.ChatStreamAsync(message, tokenSource.Token))
        {
            streamCallback?.Invoke(item.Data.Message.Content);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await StopAsync();
    }
}
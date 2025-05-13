using CommonSDK.AI.Enum;
using CommonSDK.AI.Factory;
using CommonSDK.AI.Interface;
using CommonSDK.Service;
using CommonSDK.Util;
using Microsoft.Extensions.AI;

namespace CommonSDK.AI.Ollama;

public class OllamaService : IChatService<ChatResponse>
{
    private readonly string endpoint = "http://localhost:11434";

    private readonly string modelId = "llama3.2";

    private readonly string ollamaName = "ollama";

    private readonly string ollamaExectueFileName = "ollama.exe";

    private readonly string executeablePath = "";

    private readonly IChatClient client;

    private readonly ITerminalService terminalService;

    private readonly ICommandService commandService;

    public OllamaService(string endpoint, string modelId)
    {
        this.endpoint = endpoint;
        this.modelId = modelId;
        terminalService = TerminalServiceFactory.Create();
        commandService = CommandServiceFactory.Create(AIPlatform.Ollama);
        executeablePath = SystemUtil.GetFileIfExist(SystemUtil.GetApplicationInfo(ollamaName).InstallLocation, ollamaExectueFileName);
        client = new OllamaChatClient(endpoint, modelId: modelId);
    }

    /// <summary>
    /// It will run model service
    /// </summary>
    /// <returns></returns>
    public async Task<bool> RunAsync()
    {   
        string command = commandService.GetRunModelCommand(executeablePath, modelId);

        return await terminalService.ExecuteCommandAsync(command);
    }

    /// <summary>
    /// It will stop model service
    /// </summary>
    /// <returns></returns>
    public async Task<bool> StopAsync()
    {
        string command = commandService.GetStopModelCommand(executeablePath, modelId);

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

        return await client.GetResponseAsync(message);
    }

    public async Task ChatStreamAsync(string message, Action<string> streamCallback)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

        await foreach (var update in client.GetStreamingResponseAsync(message))
        {
            streamCallback?.Invoke(update.Text);
            Console.Write(update);
        }
        Console.WriteLine();
    }

    public async ValueTask DisposeAsync()
    {
        await StopAsync();
    }
}
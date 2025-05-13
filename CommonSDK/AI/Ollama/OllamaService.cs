using Microsoft.Extensions.AI;

namespace CommonSDK.AI.Ollama;

public class OllamaHelper
{
    private static readonly string endpoint = "http://192.168.4.94:8000";

    private static readonly string modelId = "llama3.2";
    
    /// <summary>
    /// 发送聊天消息
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static async Task<ChatResponse> Chat(string message)
    {
        IChatClient client = new OllamaChatClient(endpoint, modelId: modelId);

        return await client.GetResponseAsync(message);
    }

    public static async Task ChatStream()
    {
        
    }
}
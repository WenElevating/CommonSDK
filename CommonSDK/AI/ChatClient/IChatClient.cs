using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CommonSDK.AI.ChatClient
{
    /// <summary>
    /// Returns the chat data output by the model
    /// </summary>
    public class InternalChatResponse
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("message")]
        public ChatMessage Message { get; set; }

        [JsonProperty("done_reason")]
        public string DoneReason { get; set; }

        [JsonProperty("done")]
        public bool Done { get; set; }

        [JsonProperty("total_duration")]
        public long TotalDuration { get; set; }

        [JsonProperty("load_duration")]
        public long LoadDuration { get; set; }

        [JsonProperty("prompt_eval_count")]
        public long PromptEvalCount { get; set; }

        [JsonProperty("prompt_eval_duration")]
        public long PromptEvalDuration { get; set; }

        [JsonProperty("eval_count")]
        public long EvalCount { get; set; }

        [JsonProperty("eval_duration")]
        public long EvalDuration { get; set; }
    }

    /// <summary>
    /// Chat message data structure
    /// </summary>
    public class ChatMessage
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }

    public enum ChatResultCode
    {
        Failed = -1,
        Success = 0,
        Timeout = 1
    }

    public struct ChatResponse
    {
        [JsonProperty("data")]
        public InternalChatResponse Data { get; set; }

        [JsonProperty("code")]
        public ChatResultCode Code { get; set; }
    }
    public interface IChatClient
    {
        public Task<ChatResponse> ChatAsync(string message);
    }
}

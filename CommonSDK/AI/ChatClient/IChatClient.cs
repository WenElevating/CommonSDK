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
        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("data")]
        public InternalChatResponse Data { get; set; }

        [JsonProperty("code")]
        public ChatResultCode Code { get; set; }
    }

    public interface IChatClient
    {
        /// <summary>
        /// chat API interface, providing large model question-answering capabilities.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<ChatResponse> ChatAsync(string message);

        /// <summary>
        /// chat API interface, providing large model question-answering capabilities with cancellation support.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<ChatResponse> ChatAsync(string message, CancellationToken token);

        /// <summary>
        /// Stream chat API interface, providing large model question-answering capabilities with real-time message streaming.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="onReceivedMessage"></param>
        /// <returns></returns>
        public Task<ChatResponse> ChatStreamAsync(string message, Action<string> onReceivedMessage);

        /// <summary>
        /// Stream chat API interface, providing large model question-answering capabilities with real-time message streaming and cancellation support.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="onReceivedMessage"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<ChatResponse> ChatStreamAsync(string message, Action<string> onReceivedMessage, CancellationToken token = default);

        /// <summary>
        /// Stream chat API interface, providing large model question-answering capabilities with real-time message streaming without callback.It base on yield.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public IAsyncEnumerable<ChatResponse> ChatStreamAsync(string message, CancellationToken token = default);

    }
}

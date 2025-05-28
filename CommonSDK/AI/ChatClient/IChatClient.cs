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

    public class LocalModelData
    {
        [JsonProperty("models")]
        List<LocalModel> Models { get; set; }
    }

    public class LocalModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("modified_at")]
        public string ModifiedAt { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("digest")]
        public string Digest { get; set; }

        [JsonProperty("details")]
        public LocalModelDetail Details { get; set; }
    }

    public class LocalModelDetail
    {
        [JsonProperty("parent_model")]
        public string ParentModel { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("family")]
        public string Family { get; set; }

        [JsonProperty("families")]
        public List<string> Families { get; set; }

        [JsonProperty("parameter_size")]
        public string ParameterSize { get; set; }

        [JsonProperty("quantization_level")]
        public string QuantizationLevel { get; set; }
    }

    public class ModelInfoResponse
    {
        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("modelfile")]
        public string Modelfile { get; set; }

        [JsonProperty("parameters")]
        public string Parameters { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("details")]
        public LocalModelDetail Details { get; set; }

        [JsonProperty("model_info")]
        public ModelInfo ModelInfo { get; set; }

        [JsonProperty("tensors")]
        public List<Tensor> Tensors { get; set; }

        [JsonProperty("capabilities")]
        public List<string> Capabilities { get; set; }

        [JsonProperty("modified_at")]
        public string ModifiedAt { get; set; }
    }

    public class ModelInfo
    {
        [JsonProperty("general.architecture")]
        public string GeneralArchitecture { get; set; }

        [JsonProperty("general.basename")]
        public string GeneralBasename { get; set; }

        [JsonProperty("general.file_type")]
        public int GeneralFileType { get; set; }

        [JsonProperty("general.finetune")]
        public string GeneralFinetune { get; set; }

        [JsonProperty("general.languages")]
        public object GeneralLanguages { get; set; }

        [JsonProperty("general.parameter_count")]
        public long GeneralParameterCount { get; set; }

        [JsonProperty("general.quantization_version")]
        public int GeneralQuantizationVersion { get; set; }

        [JsonProperty("general.size_label")]
        public string GeneralSizeLabel { get; set; }

        [JsonProperty("general.tags")]
        public object GeneralTags { get; set; }

        [JsonProperty("general.type")]
        public string GeneralType { get; set; }

        [JsonProperty("llama.attention.head_count")]
        public int LlamaAttentionHeadCount { get; set; }

        [JsonProperty("llama.attention.head_count_kv")]
        public int LlamaAttentionHeadCountKv { get; set; }

        [JsonProperty("llama.attention.key_length")]
        public int LlamaAttentionKeyLength { get; set; }

        [JsonProperty("llama.attention.layer_norm_rms_epsilon")]
        public double LlamaAttentionLayerNormRmsEpsilon { get; set; }

        [JsonProperty("llama.attention.value_length")]
        public int LlamaAttentionValueLength { get; set; }

        [JsonProperty("llama.block_count")]
        public int LlamaBlockCount { get; set; }

        [JsonProperty("llama.context_length")]
        public int LlamaContextLength { get; set; }

        [JsonProperty("llama.embedding_length")]
        public int LlamaEmbeddingLength { get; set; }

        [JsonProperty("llama.feed_forward_length")]
        public int LlamaFeedForwardLength { get; set; }

        [JsonProperty("llama.rope.dimension_count")]
        public int LlamaRopeDimensionCount { get; set; }

        [JsonProperty("llama.rope.freq_base")]
        public int LlamaRopeFreqBase { get; set; }

        [JsonProperty("llama.vocab_size")]
        public int LlamaVocabSize { get; set; }

        [JsonProperty("tokenizer.ggml.bos_token_id")]
        public int TokenizerGgmlBosTokenId { get; set; }

        [JsonProperty("tokenizer.ggml.eos_token_id")]
        public int TokenizerGgmlEosTokenId { get; set; }

        [JsonProperty("tokenizer.ggml.merges")]
        public object TokenizerGgmlMerges { get; set; }

        [JsonProperty("tokenizer.ggml.model")]
        public string TokenizerGgmlModel { get; set; }

        [JsonProperty("tokenizer.ggml.pre")]
        public string TokenizerGgmlPre { get; set; }

        [JsonProperty("tokenizer.ggml.token_type")]
        public object TokenizerGgmlTokenType { get; set; }

        [JsonProperty("tokenizer.ggml.tokens")]
        public object TokenizerGgmlTokens { get; set; }
    }

    public class Tensor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("shape")]
        public List<long> Shape { get; set; }
    }

    public class ModelInfoRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }
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

        /// <summary>
        /// Stream chat API interface, providing history chat message question-answering capabilities with real-time message streaming without callback.It base on yield.
        /// For example: "messages": [ { "role": "user", "content": "why is the sky blue?"  }, { "role": "assistant",  "content": "due to rayleigh scattering." },  { "role": "user",  "content": "how is that different than mie scattering?"} ]
        /// </summary>
        /// <param name="messageList">provide history message list</param>
        /// <param name="onReceivedMessage">on received message callback</param>
        /// <param name="token">cancel token</param>
        /// <returns></returns>
        public IAsyncEnumerable<ChatResponse> ChatStreamWithHistoryAsync(List<string> messageList, [AllowNull] Action<string> onReceivedMessage, CancellationToken token = default);

        /// <summary>
        /// Get local Models
        /// </summary>
        /// <returns></returns>
        public Task<LocalModelData> GetLocalModelsAsync(CancellationToken token = default);

        /// <summary>
        /// Get model detail info
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<ModelInfoResponse> GetModelInfoAsync(string mode, CancellationToken token = default);
    }
}

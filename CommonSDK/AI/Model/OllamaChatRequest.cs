using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CommonSDK.AI.Model
{
    internal struct OllamaChatRequest
    {
        /// <summary>
        ///  (required) the model name
        /// </summary>
        [JsonProperty("model")]
        public required string Model { get; set; }

        /// <summary>
        /// the messages of the chat, this can be used to keep a chat memory
        /// </summary>
        [JsonProperty("messages")]
        public List<OllamaChatRequestMessage> Messages { get; set; }

        /// <summary>
        /// the format to return a response in. Format can be json or a JSON schema.（It is not suitable for the time being, please do not fill it！）
        /// </summary>
        [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
        public object? Format { get; set; }

        /// <summary>
        ///  additional model parameters listed in the documentation for the Modelfile such as temperature（It is not suitable for the time being, please do not fill it！）
        /// </summary>
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public object? Options { get; set; }

        /// <summary>
        /// if false the response will be returned as a single response object, rather than a stream of objects
        /// </summary>
        [JsonProperty("stream")]
        public bool Stream { get; set; }

        /// <summary>
        /// controls how long the model will stay loaded into memory following the request (default: 5m)
        /// </summary>
        [JsonProperty("keep_alive")]
        public int KeepAlive { get; set; }

        public static OllamaChatRequest Create(string model, OllamaChatRequestMessage messages, bool stream = false, int keepAlive = 5)
        {
            return new OllamaChatRequest
            {
                Model = model,
                Messages = [messages],
                Stream = stream,
                KeepAlive = keepAlive
            };
        }
    }

    internal struct OllamaChatRequestMessage
    {
        /// <summary>
        /// the role of the message, either system, user, assistant, or tool
        /// </summary>
        [JsonProperty("role")]
        public required string Role { get; set; }

        /// <summary>
        ///  the content of the message
        /// </summary>
        [JsonProperty("content")]
        public required string Content { get; set; }

        /// <summary>
        ///  (optional): a list of images to include in the message (for multimodal models such as llava)
        /// </summary>
        [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
        public List<string>? Images { get; set; }

        /// <summary>
        ///  (optional): a list of tools in JSON that the model wants to use
        /// </summary>
        [JsonProperty("tool_calls", NullValueHandling = NullValueHandling.Ignore)]
        public object? ToolCalls { get; set; }

        public static OllamaChatRequestMessage Create(string role, string content, List<string>? images = null, object? toolCalls = null)
        {
            return new OllamaChatRequestMessage
            {
                Role = role,
                Content = content,
                Images = images,
                ToolCalls = toolCalls
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.ChatClient;
using Newtonsoft.Json;

namespace CommonSDK.AI.Model
{
    internal struct OllamaChatResponse
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("message")]
        public ChatMessage Message { get; set; }
    }
}

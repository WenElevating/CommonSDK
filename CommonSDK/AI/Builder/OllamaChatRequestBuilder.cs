using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Common;
using CommonSDK.AI.Model;
using Newtonsoft.Json;

namespace CommonSDK.AI.Builder
{
    internal class OllamaChatRequestBuilder
    {

        internal static string BuildSingleMessage(string model, string message)
        {
            // 封装message
            OllamaChatRequest chatRequest = OllamaChatRequest.Create(model, OllamaChatRequestMessage.Create(OllamaRoleString.User, message));

            // 转为Json
            return JsonConvert.SerializeObject(chatRequest);
        }
    }
}

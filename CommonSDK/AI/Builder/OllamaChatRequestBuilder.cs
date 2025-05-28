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

        internal static string BuildStreamMessage(string model, string message, CancellationToken token = default)
        {
            // 封装message
            OllamaChatRequest chatRequest = OllamaChatRequest.Create(model, OllamaChatRequestMessage.Create(OllamaRoleString.User, message));

            chatRequest.Stream = true;

            // 检查是否取消
            token.ThrowIfCancellationRequested();

            // 转为Json
            return JsonConvert.SerializeObject(chatRequest);
        }

        internal static string BuildStreamMessageList(string model, List<string> messages, CancellationToken token = default)
        {
            List<OllamaChatRequestMessage> messageList = [];
            token.ThrowIfCancellationRequested();

            foreach (var item in messages)
            {
                messageList.Add(OllamaChatRequestMessage.Create(OllamaRoleString.User, item));
            }

            // 封装message
            OllamaChatRequest chatRequest = OllamaChatRequest.Create(model, messageList);

            chatRequest.Stream = true;

            // 检查是否取消
            token.ThrowIfCancellationRequested();

            // 转为Json
            return JsonConvert.SerializeObject(chatRequest);
        }

        internal static string BuildNoStreamMessage(string model, string message)
        {
            // 封装message
            OllamaChatRequest chatRequest = OllamaChatRequest.Create(model, OllamaChatRequestMessage.Create(OllamaRoleString.User, message));

            // 关闭流式响应
            chatRequest.Stream = false;

            // 转为Json
            return JsonConvert.SerializeObject(chatRequest);
        }
    }
}

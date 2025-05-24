using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.AI.Builder;
using CommonSDK.AI.Common;
using CommonSDK.AI.Model;
using CommonSDK.Http;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CommonSDK.AI.ChatClient
{
    /// <summary>
    /// This client is written based on the API documentation provided by ollama
    /// Docs: https://github.com/ollama/ollama/blob/main/docs/api.md
    /// </summary>
    public class OllamaChatClient : IChatClient
    {
        private readonly string url;

        private readonly string model;

        private readonly IHttpClient client;

        public OllamaChatClient(string url, string model)
        {
            this.url = url;
            this.model = model;
            client = new CustomHttpClient();
        }

        /// <summary>
        /// ollama chat API interface, providing large model question-answering capabilities. This interface is time-consuming, with an average time of 20 to 30ms
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<ChatResponse> ChatAsync(string message)
        {
            Stopwatch dog = new ();
            dog.Start();

            ArgumentNullException.ThrowIfNullOrEmpty(message);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

            ChatResponse chatResponse;

            try
            {
                // 调用http请求接口
                string chatUrl = url + OllamaApiString.ChatApi;

                // 转为Json
                string jsonRequest = OllamaChatRequestBuilder.BuildSingleMessage(model, message);

                // 封装请求上下文
                ReqeustContextWithHeader data = new(chatUrl, jsonRequest);

                // 返回结果
                HttpResponseMessage response = await client.PostJsonAsync(data);

                // 解析得到response
                chatResponse = await ParseResponseByMessage(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

            dog.Stop();
            Debug.WriteLine($"Total elapsed time: {dog.ElapsedMilliseconds} ms");

            return chatResponse;
        }

        private static async Task<ChatResponse> ParseResponseByMessage(HttpResponseMessage response)
        {
            // 获取流数据
            using Stream stream = await response.Content.ReadAsStreamAsync() ?? throw new InvalidOperationException("Response stream is null.");

            // 读取流数据
            using StreamReader reader = new(stream, Encoding.UTF8);

            // 构建响应
            ChatResponse chatResponse = new()
            {
                Code = response.StatusCode == System.Net.HttpStatusCode.OK ? ChatResultCode.Success : ChatResultCode.Failed,
                Data = new InternalChatResponse()
            };

            InternalChatResponse internalChatResponse = new();

            StringBuilder builder = new();
            string? currentResult = string.Empty;
            while ((currentResult = await reader.ReadLineAsync()) != null)
            {
                internalChatResponse = JsonConvert.DeserializeObject<InternalChatResponse>(currentResult) ?? throw new NotSupportedException("The response data can't support!");
                builder.Append(internalChatResponse?.Message.Content);
            }

            if (internalChatResponse != null)
            {
                internalChatResponse.Message.Content = builder.ToString();
                chatResponse.Data = internalChatResponse;
            }

            return chatResponse;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
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
        /// ollama chat API interface, providing large model question-answering capabilities.
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
                string jsonRequest = OllamaChatRequestBuilder.BuildNoStreamMessage(model, message);

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

        public async Task<ChatResponse> ChatAsync(string message, CancellationToken token)
        {
            Stopwatch dog = new();
            dog.Start();

            ArgumentNullException.ThrowIfNullOrEmpty(message);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

            ChatResponse chatResponse;

            try
            {
                // 调用http请求接口
                string chatUrl = url + OllamaApiString.ChatApi;

                // 转为Json
                string jsonRequest = OllamaChatRequestBuilder.BuildNoStreamMessage(model, message);

                // 封装请求上下文
                ReqeustContextWithHeader data = new(chatUrl, jsonRequest);

                // 检查token是否被取消
                token.ThrowIfCancellationRequested();

                // 返回结果
                HttpResponseMessage response = await client.PostJsonAsync(data, token);

                // 解析得到response
                chatResponse = await ParseResponseByMessage(response, token);
            }
            catch (OperationCanceledException opex)
            {
                Debug.WriteLine($"任务已被取消, ex: {opex}", token);
                throw;
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

        private static async Task<ChatResponse> ParseResponseByMessage(HttpResponseMessage response, CancellationToken token = default)
        {
            // 获取流数据
            using Stream stream = await response.Content.ReadAsStreamAsync(token) ?? throw new InvalidOperationException("Response stream is null.");

            // 读取流数据
            using StreamReader reader = new(stream, Encoding.UTF8);

            // 构建响应
            ChatResponse chatResponse = new()
            {
                Id = Guid.NewGuid().ToString(),
                Code = response.StatusCode == System.Net.HttpStatusCode.OK ? ChatResultCode.Success : ChatResultCode.Failed,
                Data = new InternalChatResponse()
            };

            InternalChatResponse internalChatResponse = new();

            StringBuilder builder = new();
            string? currentResult = string.Empty;
            while ((currentResult = await reader.ReadLineAsync(token)) != null)
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

        /// <summary>
        /// ollama chat API interface, providing large model question-answering capabilities with streaming response.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="onReceivedMessage"></param>
        /// <returns></returns>
        public async Task<ChatResponse> ChatStreamAsync(string message, Action<string> onReceivedMessage)
        {
            Stopwatch dog = new();
            dog.Start();

            ArgumentNullException.ThrowIfNullOrEmpty(message);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

            ChatResponse chatResponse;

            try
            {
                // 调用http请求接口
                string chatUrl = url + OllamaApiString.ChatApi;

                // 转为Json
                string jsonRequest = OllamaChatRequestBuilder.BuildStreamMessage(model, message);

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



        public async Task<ChatResponse> ChatStreamAsync(string message, Action<string> onReceivedMessage, CancellationToken token = default)
        {
            Stopwatch dog = new();
            dog.Start();

            ArgumentNullException.ThrowIfNullOrEmpty(message);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

            ChatResponse chatResponse;

            try
            {
                // 调用http请求接口
                string chatUrl = url + OllamaApiString.ChatApi;

                // 转为Json
                string jsonRequest = OllamaChatRequestBuilder.BuildStreamMessage(model, message, token);

                // 封装请求上下文
                ReqeustContextWithHeader data = new(chatUrl, jsonRequest);

                // 返回结果
                HttpResponseMessage response = await client.PostJsonAsync(data, token);

                // 解析得到response
                chatResponse = await ParseResponseByMessage(response, token);
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

        public async IAsyncEnumerable<ChatResponse> ChatStreamAsync(string message, [EnumeratorCancellation] CancellationToken token = default)
        {
            Stopwatch dog = new();
            dog.Start();

            ArgumentNullException.ThrowIfNullOrEmpty(message);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(message);

            // 调用http请求接口
            string chatUrl = url + OllamaApiString.ChatApi;

            // 转为Json
            string jsonRequest = OllamaChatRequestBuilder.BuildStreamMessage(model, message, token);

            // 封装请求上下文
            ReqeustContextWithHeader data = new(chatUrl, jsonRequest);

            // 返回结果
            HttpResponseMessage response = await client.PostNDJsonAsync(data, token);

            // 获取流数据
            using Stream stream = await response.Content.ReadAsStreamAsync(token) ?? throw new InvalidOperationException("Response stream is null.");

            // 读取流数据
            using StreamReader reader = new(stream, Encoding.UTF8);

            string? currentResult = string.Empty;
            while ((currentResult = await reader.ReadLineAsync(token)) != null)
            {
                InternalChatResponse? internalChatResponse =
                        JsonConvert.DeserializeObject<InternalChatResponse>(currentResult);

                if (internalChatResponse == null)
                    throw new NotSupportedException("The response data can't support!");

                yield return new ChatResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = ChatResultCode.Success,
                    Data = new InternalChatResponse
                    {
                        Message = new ChatMessage
                        {
                            Content = internalChatResponse.Message.Content
                        }
                    }
                };
            }

            dog.Stop();
            Debug.WriteLine($"Total elapsed time: {dog.ElapsedMilliseconds} ms");
        }

        public async IAsyncEnumerable<ChatResponse> ChatStreamWithHistoryAsync(List<string> messageList, [AllowNull] Action<string> onReceivedMessage, 
            [EnumeratorCancellation] CancellationToken token = default)
        {
            Stopwatch dog = new();
            dog.Start();

            ArgumentNullException.ThrowIfNull(messageList);

            if (messageList.Count == 0)
            {
                throw new ArgumentException("It don't have valid message!");
            }

            // 调用http请求接口
            string chatUrl = url + OllamaApiString.ChatApi;

            // 转为Json
            string jsonRequest = OllamaChatRequestBuilder.BuildStreamMessageList(model, messageList, token);

            // 封装请求上下文
            ReqeustContextWithHeader data = new(chatUrl, jsonRequest);

            // 返回结果
            HttpResponseMessage response = await client.PostNDJsonAsync(data, token);

            // 获取流数据
            using Stream stream = await response.Content.ReadAsStreamAsync(token) ?? throw new InvalidOperationException("Response stream is null.");

            // 读取流数据
            using StreamReader reader = new(stream, Encoding.UTF8);

            string? currentResult = string.Empty;
            while ((currentResult = await reader.ReadLineAsync(token)) != null)
            {
                InternalChatResponse? internalChatResponse =
                        JsonConvert.DeserializeObject<InternalChatResponse>(currentResult);

                if (internalChatResponse == null)
                    throw new NotSupportedException("The response data can't support!");

                yield return new ChatResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = ChatResultCode.Success,
                    Data = new InternalChatResponse
                    {
                        Message = new ChatMessage
                        {
                            Content = internalChatResponse.Message.Content
                        }
                    }
                };
            }

            dog.Stop();
            Debug.WriteLine($"Total elapsed time: {dog.ElapsedMilliseconds} ms");
        }

        public async Task<LocalModelData> GetLocalModelsAsync(CancellationToken token = default)
        {
            Stopwatch dog = new();
            dog.Start();

            token.ThrowIfCancellationRequested();

            LocalModelData localModels;
            RequestContext requestContext = new()
            {
                 url = url + OllamaApiString.LocalModelsApi
            };
            HttpResponseMessage response = await client.GetAsync(requestContext);

            string data = await response.Content.ReadAsStringAsync(token);

            localModels = JsonConvert.DeserializeObject<LocalModelData>(data) ?? throw new ArgumentException("Get local model is null!");

            dog.Stop();
            Debug.WriteLine($"Total elapsed time: {dog.ElapsedMilliseconds} ms");

            return localModels;
        }

        public async Task<ModelInfoResponse> GetModelInfoAsync(string mode, CancellationToken token = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(mode);
            ArgumentException.ThrowIfNullOrEmpty(mode);

            Stopwatch dog = new();
            dog.Start();

            token.ThrowIfCancellationRequested();
            ModelInfoResponse modelInfo;
            try
            {
                string api = url + OllamaApiString.GetModeInfoApi;
                string data = JsonConvert.SerializeObject(new ModelInfoRequest()
                {
                    Model = mode
                });
                ReqeustContextWithHeader requestContext = new(api, data);
                HttpResponseMessage response = await client.PostJsonAsync(requestContext, token);

                string result = await response.Content.ReadAsStringAsync(token);

                modelInfo = JsonConvert.DeserializeObject<ModelInfoResponse>(result) ?? throw new ArgumentException("Get local model is null!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                dog.Stop();
                Debug.WriteLine($"Total elapsed time: {dog.ElapsedMilliseconds} ms");
            }

            throw new Exception("Get model info failed!");
        }
    }
}

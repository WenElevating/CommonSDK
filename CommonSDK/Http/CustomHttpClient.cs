using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CommonSDK.Http
{
    internal class CustomHttpClient : IHttpClient
    {
        public async Task<HttpResponseMessage> GetAsync(RequestContext context)
        {
            using HttpClient client = new();
            return await client.GetAsync(context.url);
        }

        public async Task<HttpResponseMessage> PostJsonAsync(ReqeustContextWithHeader context)
        {
            using HttpClient client = new();
            using HttpRequestMessage message = new();
            message.RequestUri = new Uri(context.url);
            message.Method = HttpMethod.Post;
            message.Content = new StringContent(context.data.ToString() ?? throw new ArgumentException("context data is null!"), Encoding.UTF8, "application/json");
            foreach (var header in context.headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }
            return await client.SendAsync(message);
        }

        public async Task<HttpResponseMessage> PostJsonAsync(ReqeustContextWithHeader context, CancellationToken token)
        {
            using HttpClient client = new();
            using HttpRequestMessage message = new();
            message.RequestUri = new Uri(context.url);
            message.Method = HttpMethod.Post;
            message.Content = new StringContent(context.data.ToString() ?? throw new ArgumentException("context data is null!"), Encoding.UTF8, "application/json");
            foreach (var header in context.headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            // Check if the token is cancelled before sending the request
            token.ThrowIfCancellationRequested();
            
            return await client.SendAsync(message, token);
        }

        public async Task<HttpResponseMessage> PostNDJsonAsync(ReqeustContextWithHeader context, CancellationToken token)
        {
            using HttpClient client = new();
            using HttpRequestMessage message = new();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
            message.RequestUri = new Uri(context.url);
            message.Method = HttpMethod.Post;
            message.Content = new StringContent(context.data.ToString() ?? throw new ArgumentException("context data is null!"), Encoding.UTF8, "application/json");
            foreach (var header in context.headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            // Check if the token is cancelled before sending the request
            token.ThrowIfCancellationRequested();

            return await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, token);
        }


        public async Task<Stream> PostStreamAsync(ReqeustContextWithHeader context)
        {
            using HttpClient client = new();
            using HttpRequestMessage message = new();
            message.RequestUri = new Uri(context.url);
            message.Method = HttpMethod.Post;
            message.Content = new StringContent(context.data.ToString() ?? throw new ArgumentException("context data is null!"), Encoding.UTF8, "application/json");
            foreach (var header in context.headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }
            HttpResponseMessage response = await client.SendAsync(message);
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<Stream> PostStreamAsync(ReqeustContextWithHeader context, CancellationToken token)
        {
            using HttpClient client = new();
            using HttpRequestMessage message = new();
            message.RequestUri = new Uri(context.url);
            message.Method = HttpMethod.Post;
            message.Content = new StringContent(context.data.ToString() ?? throw new ArgumentException("context data is null!"), Encoding.UTF8, "application/json");
            foreach (var header in context.headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            // Check if the token is cancelled before sending the request
            token.ThrowIfCancellationRequested();
            HttpResponseMessage response = await client.SendAsync(message, token);
            return await response.Content.ReadAsStreamAsync(token);
        }
    }
}

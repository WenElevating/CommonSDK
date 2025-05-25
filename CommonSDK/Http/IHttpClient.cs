using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSDK.Http
{
    /// <summary>
    /// provide a common request context
    /// </summary>
    internal struct RequestContext
    {
        public string url;

        public object data;
    }

    internal struct ReqeustContextWithHeader
    {
        public string url;

        public string data;

        public List<KeyValuePair<string, string>> headers;

        public ReqeustContextWithHeader(string url, string data)
        {
            this.url = url;
            this.data = data;
            headers = [];
        }
    }

    internal interface IHttpClient
    {
        /// <summary>
        /// Get request by context
        /// </summary>
        /// <param name="context">request context</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> GetAsync(RequestContext context);


        /// <summary>
        /// Post request by context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PostJsonAsync(ReqeustContextWithHeader context);

        /// <summary>
        /// Post request by context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PostJsonAsync(ReqeustContextWithHeader context, CancellationToken token);

        /// <summary>
        /// Post request by context, and return NDJson response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PostNDJsonAsync(ReqeustContextWithHeader context, CancellationToken token);

        /// <summary>
        /// Post request by context, and return stream
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<Stream> PostStreamAsync(ReqeustContextWithHeader context);

        /// <summary>
        /// Post request by context, and return stream with cancellation token
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<Stream> PostStreamAsync(ReqeustContextWithHeader context, CancellationToken token);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Utils
{
    internal class Web
    {
        private static readonly HttpClient client = new HttpClient();
        private static async Task<HttpResponseMessage> SendRawRequest(
            string url,
            HttpMethod method,
            string? body = null,
            Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(method, url);
            if (body != null)
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            request.Headers.Add("", header.Value);
            return await client.SendAsync(request);
        }
        internal static async Task<HttpResponseMessage> Get(string url, Dictionary<string, string>? headers = null)
            => await SendRawRequest(url, HttpMethod.Get, null, headers);
        internal static async Task<HttpResponseMessage> Post(string url, string? body = null, Dictionary<string, string>? headers = null)
            => await SendRawRequest(url, HttpMethod.Post, body, headers);
    }
}

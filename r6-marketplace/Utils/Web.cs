﻿using System.Text;

namespace r6_marketplace.Utils
{
    internal class Web
    {
        private const string APP_ID = "80a4a0e8-8797-440f-8f4c-eaba87d0fdda";
        private const string SESSION_ID = "1e08944e-f5da-4ebf-afb3-664091601c4b";
        private readonly HttpClient client = new HttpClient();
        private string? token;
        internal bool isAuthenticated => token != null;
        internal Web(string? token = null)
        {
            if (token != null) this.token = token;
        }

        internal void SetToken(string token) => this.token = token;

        internal void EnsureAuthenticated()
        {
            if (!isAuthenticated)
            {
                throw new InvalidOperationException("Client is not authenticated. " +
                    "You must call AuthenticateAsync before using any other methods.");
            }
        }

        private static async Task<HttpResponseMessage> SendRawRequest(
                 Uri uri,
                 HttpMethod method,
                 string? body = null,
                 Dictionary<string, string>? headers = null,
                 Web? web = null)
        {
            var request = new HttpRequestMessage(method, uri);
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
            HttpClient client = web?.client ?? new HttpClient();
            return await client.SendAsync(request);
        }
        private static Dictionary<string, string> PrepareHeaders(
        Dictionary<string, string>? headers,
        string? token = null,
        bool useDefaultHeaders = false,
        Data.Local? local = null)
        {
            headers ??= new Dictionary<string, string>();

            if (local != null)
            {
                headers["Ubi-Localecode"] = Data.FormatLanguage(local.Value);
            }

            if (token != null)
                headers["Authorization"] = token;

            if (useDefaultHeaders)
            {
                headers["ubi-appid"] = APP_ID;
                headers["Ubi-SessionId"] = SESSION_ID;
            }

            return headers;
        }
        internal async Task<HttpResponseMessage> GetAsync(
        Uri uri,
        Dictionary<string, string>? headers = null,
        bool sendAuthToken = true,
        bool useDefaultHeaders = true,
        Data.Local local = Data.Local.en)
        {
            var finalHeaders = PrepareHeaders(headers, sendAuthToken ? token : null, useDefaultHeaders, local);
            return await SendRawRequest(uri, HttpMethod.Get, null, finalHeaders, this);
        }
        internal async Task<HttpResponseMessage> PostAsync(
    Uri uri,
    string? body = null,
    Dictionary<string, string>? headers = null,
    bool sendAuthToken = true,
    bool useDefaultHeaders = true,
    Data.Local local = Data.Local.en)
        {
            var finalHeaders = PrepareHeaders(headers, sendAuthToken ? token : null, useDefaultHeaders, local);
            return await SendRawRequest(uri, HttpMethod.Post, body, finalHeaders, this);
        }

        public static async Task<HttpResponseMessage> GetAsync(
            Uri uri,
            Dictionary<string, string>? headers = null,
            bool useDefaultHeaders = false)
            => await SendRawRequest(uri, HttpMethod.Get, null, PrepareHeaders(headers, useDefaultHeaders: useDefaultHeaders));

        public static async Task<HttpResponseMessage> PostAsync(
            Uri uri,
            string? body = null,
            Dictionary<string, string>? headers = null,
            bool useDefaultHeaders = false)
            => await SendRawRequest(uri, HttpMethod.Post, body, PrepareHeaders(headers, useDefaultHeaders:useDefaultHeaders));
    }
}

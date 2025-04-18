using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item.Error;
using r6_marketplace.Utils.Exceptions;

namespace r6_marketplace.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<string> ReadAsStringAsyncSafe(this HttpContent content)
        {
            return await content.ReadAsStringAsync().ConfigureAwait(false);
        }
        public static async Task<T?> DeserializeAsyncSafe<T>(this HttpResponseMessage response,
            bool checkFor404 = true, bool checkForStatusCode = true)
        {
            if (checkForStatusCode && !response.IsSuccessStatusCode)
                throw new UnsuccessfulStatusCodeException(response.StatusCode.ToString());
            var json = await response.Content.ReadAsStringAsyncSafe();
            var error = System.Text.Json.JsonSerializer.Deserialize<List<ApiError>?>(json);
            if (checkFor404 && IsNotFoundError(error))
                return default;
            if (IsInvalidTokenError(error))
                throw new InvalidTokenException("The authentication ticket is invalid. Please re-authenticate.");

            return JsonSerializer.Deserialize<T>(json);
        }
        private static bool IsNotFoundError(List<ApiError>? errors)
            => errors?.Any(e => e.errors?.Any(err => err.message.Contains("404")) == true) == true;
        private static bool IsInvalidTokenError(List<ApiError>? errors)
            => errors?.Any(e => e.errors?.Any(err => err.message.Contains("The authentication ticket is invalid")) == true) == true;
    }
}

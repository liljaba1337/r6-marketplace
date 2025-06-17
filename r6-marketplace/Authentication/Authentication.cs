using System.Net;
using System.Text;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;

namespace r6_marketplace.Authentication
{
    internal static class Authentication
    {
        internal static async Task<Classes.AuthenticationResponse>Authenticate(string email, string password, Web web)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new Utils.Exceptions.InvalidCredentialsException("Either your login or password is empty.");
            }

            var auth = $"{email}:{password}";
            var bytes = Encoding.UTF8.GetBytes(auth);
            string credentialsb64 = Convert.ToBase64String(bytes);

            var data = $"{{\"rememberMe\": false}}";
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Basic {credentialsb64}" }
            };
            var response = await web.Post(
                Data.authUri,
                data,
                headers,
                true
            );

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new Utils.Exceptions.InvalidCredentialsException("Either your login or password is incorrect.");
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Utils.Exceptions.HttpRequestException($"Unexpected HTTP status code: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsyncSafe();
            r6_marketplace.Classes.AuthenticationResponse? parsedresponse =
                System.Text.Json.JsonSerializer.Deserialize<r6_marketplace.Classes.AuthenticationResponse>(content);
            if (parsedresponse == null)
            {
                throw new Utils.Exceptions.JsonDeserializationException("An unknown error occurred. Couldn't deserialize the response.");
            }
            if(string.IsNullOrEmpty(parsedresponse.ticket))
            {
                throw new Utils.Exceptions.JsonDeserializationException("An unknown error occurred. Token is null or empty.");
            }
            return parsedresponse;
        }
    }
}

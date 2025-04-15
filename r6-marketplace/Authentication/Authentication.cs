using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Authentication
{
    internal static class Authentication
    {
        internal static async Task<r6_marketplace.Classes.AuthenticationResponse> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new Utils.Exceptions.InvalidCredentialsException("Either your login or password is empty.");
            }

            var auth = $"{email}:{password}";
            var bytes = Encoding.UTF8.GetBytes(auth);
            string _credentialsb64 = Convert.ToBase64String(bytes);

            var data = $"{{\"rememberMe\": false}}"; // for testing purposes. will change to true later
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Basic {_credentialsb64}" }
            };
            var response = await Utils.Web.PostAsync(
                new Uri("https://public-ubiservices.ubi.com/v3/profiles/sessions"),
                data,
                headers,
                true
            );
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                throw new Utils.Exceptions.InvalidCredentialsException("Either your login or password is incorrect.");
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Utils.Exceptions.InvalidCredentialsException($"An unknown error occurred. HTTP code: {response.StatusCode}");
            }
            var content = await response.Content.ReadAsStringAsync();
            r6_marketplace.Classes.AuthenticationResponse? _response =
                System.Text.Json.JsonSerializer.Deserialize<r6_marketplace.Classes.AuthenticationResponse>(content);
            if (_response == null)
            {
                throw new Utils.Exceptions.InvalidCredentialsException("An unknown error occurred. Couldn't deserialize the response.");
            }
            if(string.IsNullOrEmpty(_response.ticket))
            {
                throw new Utils.Exceptions.InvalidCredentialsException("An unknown error occurred. Token is null or empty.");
            }
            return _response;
        }
    }
}

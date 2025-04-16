using r6_marketplace.Utils;
using r6_marketplace.Endpoints;

namespace r6_marketplace
{
    public class R6MarketplaceClient
    {
        public ItemInfoEndpoints ItemInfoEndpoints { get; }

        private readonly Web web;
        /// <summary>
        /// Returns true if the token is not null and the client is ready to process requests.
        /// Does NOT guarantee that the token is valid if you passed it manually.
        /// </summary>
        public bool isAuthenticated => web.isAuthenticated;
        /// <summary>
        /// Create a new instance of the R6MarketplaceClient.
        /// </summary>
        /// <param name="token">Your ubisoft account token (in case you already have it)</param>
        /// <param name="httpClient">An http client if you wish to use a custom one</param>
        public R6MarketplaceClient(string? token = null, HttpClient? httpClient = null)
        {
            web = new Web(httpClient ?? new HttpClient(), token);

            ItemInfoEndpoints = new ItemInfoEndpoints(web);
        }

        /// <summary>
        /// Authenticate with email and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Your access token. You may use it to reauthenticate without using your email and password again.</returns>
        public async Task<string> Authenticate(string email, string password)
        {
            r6_marketplace.Classes.AuthenticationResponse response =
                await Authentication.Authentication.Authenticate(email, password);
            string token = "Ubi_v1 t=" + response.ticket;
            web.SetToken(token);
            return token;
        }
        /// <summary>
        /// Update the token manually.
        /// </summary>
        /// <param name="token"></param>
        public void SetToken(string token) => web.SetToken(token);
        /// <summary>
        /// Change the http client.
        /// </summary>
        /// <param name="client"></param>
        public void SetHttpClient(HttpClient client) => web.SetHttpClient(client);
    }
}

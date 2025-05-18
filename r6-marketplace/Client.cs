using r6_marketplace.Utils;
using r6_marketplace.Endpoints;
using r6_marketplace.Authentication;

namespace r6_marketplace
{
    public sealed class R6MarketplaceClient
    {
        public readonly ItemInfoEndpoints ItemInfoEndpoints;
        public readonly SearchEndpoints SearchEndpoints;
        public readonly AccountEndpoints AccountEndpoints;
        public readonly TransactionsEndpoints TransactionsEndpoints;
        private readonly TokenRefresher TokenRefresher;
        private readonly Web web;

        [Obsolete("This event is not ready yet and should not be used.", false)]
        public event TokenRefreshedEventHandler? TokenRefreshed
        {
            add => TokenRefresher.TokenRefreshed += value;
            remove => TokenRefresher.TokenRefreshed -= value;
        }

        /// <summary>
        /// The expiration time of the current token.  
        /// When this time is reached, the token is automatically refreshed,  
        /// and the <see cref="TokenRefreshed"/> event is triggered.
        /// </summary>
        public DateTime? TokenExpiration => TokenRefresher.Expiration;

        /// <summary>
        /// Returns true if the token is not null and the client is ready to process requests.
        /// Please note: this does NOT guarantee that the token is valid if you passed it manually.
        /// </summary>
        public bool isAuthenticated => web.isAuthenticated;

        /// <summary>
        /// Create a new instance of the R6MarketplaceClient.
        /// </summary>
        /// <param name="token">Your Ubisoft account token beginning with "Ubi_v1 t=".
        /// Mostly used for testing purposes, so as not to reauthenticate many times.</param>
        /// <param name="httpClient">An <see cref="HttpClient"/> instance if you wish to use a custom one</param>
        public R6MarketplaceClient(HttpClient? httpClient = null, string? token = null)
        {
            web = new Web(httpClient ?? new HttpClient(), token);

            ItemInfoEndpoints = new ItemInfoEndpoints(web);
            TokenRefresher = new TokenRefresher(web);
            TransactionsEndpoints = new TransactionsEndpoints(web);
            AccountEndpoints = new AccountEndpoints(web, TransactionsEndpoints);
            SearchEndpoints = new SearchEndpoints(web, TransactionsEndpoints);
        }

        /// <summary>
        /// Authenticate with email and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Your access token.
        /// It's used internally, but is returned in case you want to save it somewhere.</returns>
        public async Task<string> Authenticate(string email, string password)
        {
            r6_marketplace.Classes.AuthenticationResponse response =
                await Authentication.Authentication.Authenticate(email, password);
            string token = "Ubi_v1 t=" + response.ticket;
            web.token = token;
            return token;
        }

        /// <summary>
        /// Change the http client.
        /// </summary>
        /// <param name="client"></param>
        public void SetHttpClient(HttpClient client) => web.SetHttpClient(client);
    }
}

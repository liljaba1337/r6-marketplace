using r6_marketplace.Utils;
using r6_marketplace.Endpoints;
using r6_marketplace.Authentication;
using r6_marketplace.Events;

namespace r6_marketplace
{
    public sealed class R6MarketplaceClient
    {
        public readonly ItemInfoEndpoints ItemInfoEndpoints;
        public readonly SearchEndpoints SearchEndpoints;
        public readonly AccountEndpoints AccountEndpoints;
        public readonly TransactionsEndpoints TransactionsEndpoints;
        public readonly AccountEvents AccountEvents;
        private readonly TokenRefresher TokenRefresher;
        private readonly Web web;

        #region token refresher
        /// <summary>
        /// Triggered when the token is automatically refreshed.
        /// </summary>
        public event TokenRefreshedEventHandler? OnTokenRefreshed
        {
            add => TokenRefresher.TokenRefreshed += value;
            remove => TokenRefresher.TokenRefreshed -= value;
        }

        /// <summary>
        /// Enable or disable automatic token refreshing.
        /// </summary>
        /// <remarks>
        /// Please note that the token will be refreshed immediately in order to capture the expiration time.
        /// Also, if you call this method before subscribing to <see cref="OnTokenRefreshed"/>,
        /// the event won't be triggered for the first time.
        /// </remarks>
        public void SetupTokenRefreshing(bool enabled = true)
        {
            TokenRefresher.SetupRefreshing(enabled);
        }

        /// <summary>
        /// The expiration time of the current token.
        /// </summary>
        public DateTime? TokenExpiration => TokenRefresher.Expiration;
        #endregion

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
            AccountEvents = new AccountEvents(AccountEndpoints);
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

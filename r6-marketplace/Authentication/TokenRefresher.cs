//#pragma warning disable CS8604

using r6_marketplace.Utils;
using static r6_marketplace.R6MarketplaceClient;

namespace r6_marketplace.Authentication
{
    public delegate void TokenRefreshedEventHandler(string oldToken, string newToken, DateTime expirationDate);
    internal class TokenRefresher
    {
        private readonly Web web;
        private Timer? refreshTimer;
        internal event TokenRefreshedEventHandler? TokenRefreshed;
        internal TokenRefresher(Web web)
        {
            this.web = web;
        }
        internal DateTime Expiration { get; private set; }
        internal void SetupRefreshing()
        {
            refreshTimer?.Dispose();
            refreshTimer = new Timer(_ => _ = RefreshToken(), null,
                Expiration - DateTime.UtcNow - TimeSpan.FromMinutes(10), Timeout.InfiniteTimeSpan);
        }
        internal async Task<string> RefreshToken()
        {
            var response = await web.Put(Data.authUri, "{\"rememberMe\": false}");
            Console.WriteLine(response.StatusCode);

            Expiration = DateTime.MinValue;
            TokenRefreshed?.Invoke(web.token, string.Empty, Expiration);
            web.token = string.Empty;
            SetupRefreshing();
            return string.Empty;
        }
    }
}

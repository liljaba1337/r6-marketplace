using r6_marketplace.Extensions;
using r6_marketplace.Utils;

namespace r6_marketplace.Authentication
{
    public delegate void TokenRefreshedEventHandler(string oldToken, string newToken, DateTime expirationDate);
    internal class TokenRefresher
    {
        private bool enabled = false;
        private const string body = "{\"rememberMe\":false}";
        private readonly Web web;
        private Timer? refreshTimer;
        internal event TokenRefreshedEventHandler? TokenRefreshed;
        internal static readonly SemaphoreSlim tokenRefreshLock = new(1, 1);
        internal TokenRefresher(Web web)
        {
            this.web = web;
        }
        internal DateTime Expiration { get; private set; }
        internal void SetupRefreshing(bool enabled)
        {
            this.enabled = enabled;
            refreshTimer?.Dispose();
            if (!enabled) return;
            if (Expiration == default) // First start
            {
                RefreshToken().GetAwaiter().GetResult();
                return;
            }
            refreshTimer = new Timer(_ => _ = RefreshToken(), null,
                Expiration - DateTime.UtcNow - TimeSpan.FromMinutes(10), Timeout.InfiniteTimeSpan);
        }
        internal async Task RefreshToken()
        {
            await tokenRefreshLock.WaitAsync();
            try
            {
                if (web.token == null)
                    return;

                var data = await SendRefreshRequest();
                if (data == null || string.IsNullOrEmpty(data.ticket) || data.expiration == default)
                    return;

                Expiration = data.expiration;
                TokenRefreshed?.Invoke(web.token, data.ticket, Expiration);
                web.token = "Ubi_v1 t=" + data.ticket;
                SetupRefreshing(enabled);
            }
            finally
            {
                tokenRefreshLock.Release();
            }
        }
        private async Task<Classes.AuthenticationResponse?> SendRefreshRequest()
        {
            var response = await web.Post(Data.authUri, body, forceTokenAccess:true);
            if (response.IsSuccessStatusCode)
                return await response.DeserializeAsyncSafe<Classes.AuthenticationResponse>(false, false);
            return null;
        }
    }
}

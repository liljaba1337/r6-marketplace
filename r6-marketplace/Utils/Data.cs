#pragma warning disable CS8981

namespace r6_marketplace.Utils
{
    public class Data
    {
        /// <summary>
        /// Langugage codes for the API. Uncommon ones like jp or ko are not yet supported (by this package, not API).
        /// </summary>
        public enum Local
        {
            de,
            en,
            es,
            fr,
            it,
            pl,
            ru,
            tr
        }
        public enum OrderType
        {
            Buy,
            Sell
        }
        public enum OrderState
        {
            /// <summary>
            /// Purchased / sold successfully.
            /// </summary>
            Succeeded,
            /// <summary>
            /// Most likely cancelled.
            /// </summary>
            Failed
        }
        public static readonly Uri dataUri = new Uri("https://public-ubiservices.ubi.com/v1/profiles/me/uplay/graphql");
        public static readonly Uri authUri = new Uri("https://public-ubiservices.ubi.com/v3/profiles/sessions");
    }
}

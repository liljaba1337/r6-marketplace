using r6_marketplace.Classes.Item;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;

namespace r6_marketplace.Endpoints
{
    public class SearchEndpoints : EndpointsBase
    {
        internal SearchEndpoints(Web web) : base(web) { }
        public enum SortBy
        {
            PurchaseAvailaible,
            SaleAvailaible,
            LastSalePrice,
            ItemName
        }
        
        public enum SortDirection
        {
            /// <summary>
            /// Ascending / A-Z
            /// </summary>
            ASC,
            /// <summary>
            /// Descending / Z-A
            /// </summary>
            DESC
        }

        /// <summary>
        /// Returns a list of all the search tags you can use in <see cref="SearchItem"/>.
        /// </summary>
        /// <returns>An instance of <see cref="Classes.Tags.Tags"/>.</returns>
        public async Task<Classes.Tags.Tags> GetSearchTags()
        {
            web.EnsureAuthenticated();
            var response = await web.Post(Data.dataUri, new RequestBodies.GetSearchTags.Root().AsJson());

            var json = await response.DeserializeAsyncSafe<List<Classes.Tags.RawData.Root>>(false);
            var tagGroups = json?[0].data.game.marketplace.tagGroups;
            var tagGroupDict = tagGroups?.ToDictionary(x => x.displayName, x => x.values);
            var tags = new Classes.Tags.Tags()
            {
                Rarity = tagGroupDict?.GetValueOrDefault("Rarity"),
                Esports_Team = tagGroupDict?.GetValueOrDefault("Esports Team"),
                Season = tagGroupDict?.GetValueOrDefault("Season"),
                Operator = tagGroupDict?.GetValueOrDefault("Operator"),
                Weapon = tagGroupDict?.GetValueOrDefault("Weapon"),
                Event = tagGroupDict?.GetValueOrDefault("Event"),
                Type = json?[0].data.game.marketplace.types.Select(x => x.value).ToList()
            };

            return tags;
        }

        /// <summary>
        /// Search for items in the marketplace. Filtering is extremely unintuitive at the moment,
        /// so you may want to check the descriptions of all the parameters to understand everything.
        /// I will definitely improve this in the future.
        /// </summary>
        /// <param name="name">The name of the item to search for.</param>
        /// <param name="types">A list of item TYPES (e.g. WeaponSkin or DroneSkin). Available types can be retrieved using <see cref="GetSearchTags"/> from the <see cref="r6_marketplace.Classes.Tags.Tags.Type"/> property.</param>
        /// <param name="tags">A list of item TAGS. This is basically everything else from the response of <see cref="GetSearchTags"/> EXCEPT types.</param>
        /// <param name="sortBy">The method of sorting.</param>
        /// <param name="sortDirection">The direction of sorting.</param>
        /// <param name="limit">The maximum number of items to return. Must be between 0 and 500.</param>
        /// <param name="offset">The number of items to skip before returning results. Must be non-negative.</param>
        /// <returns>A read-only list of matching <see cref="PurchasableItem"/> objects.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="limit"/> or <paramref name="offset"/> is negative.
        /// </exception>
        public async Task<IReadOnlyList<PurchasableItem>> SearchItem(
            string? name = default, List<string>? types = default, List<string>? tags = default,
            SortBy sortBy = SortBy.PurchaseAvailaible, SortDirection sortDirection = SortDirection.DESC,
            int limit = 40, int offset = 0
            )
        {
            if (limit < 0 || offset < 0)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit and offset cannot be negative.");
            if (limit > 500)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit cannot be greater than 500.");

            web.EnsureAuthenticated();

            var body = new RequestBodies.SearchItems.Root(
                name ?? "", limit, offset, types ?? new List<string>(), tags ?? new List<string>(), sortBy, sortDirection);
            var response = await web.Post(Data.dataUri, body.AsJson());
            var json = await response.DeserializeAsyncSafe<List<Classes.SearchResponse.RawData.Root>>(false);
            if(json == null || json[0].data.game.marketableItems.nodes.Count == 0)
                return new List<PurchasableItem>();

            return json[0].data.game.marketableItems.nodes.Select(x => new PurchasableItem
            {
                ID = x.item.itemId,
                Name = x.item.name,
                AssetUrl = new Classes.ImageUri(x.item.assetUrl),
                Type = x.item.type,
                Tags = x.item.tags,
                LastSoldAtPrice = x.marketData.lastSoldAt[0].price,
                LastSoldAtTime = x.marketData.lastSoldAt[0].performedAt,
                BuyOrdersStats = x.marketData.buyStats != null ? new OrdersStats()
                {
                    lowestPrice = x.marketData.buyStats[0].lowestPrice,
                    highestPrice = x.marketData.buyStats[0].highestPrice,
                    activeCount = x.marketData.buyStats[0].activeCount
                } : null,
                SellOrdersStats = x.marketData.sellStats != null ? new OrdersStats()
                {
                    lowestPrice = x.marketData.sellStats[0].lowestPrice,
                    highestPrice = x.marketData.sellStats[0].highestPrice,
                    activeCount = x.marketData.sellStats[0].activeCount
                } : null
            }).ToList();
        }
    }
}

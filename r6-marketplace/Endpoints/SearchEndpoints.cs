using System.Diagnostics;
using r6_marketplace.Classes.Item;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;

namespace r6_marketplace.Endpoints
{
    public class SearchEndpoints : EndpointsBase
    {
        private readonly TransactionsEndpoints transactionsEndpoints;
        internal SearchEndpoints(Web web, TransactionsEndpoints transactionsEndpoints) : base(web)
        { this.transactionsEndpoints = transactionsEndpoints; }
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
        [Obsolete("This method isn't needed anymore. Still works, but may be removed in the future.")]
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
        /// Search for items in the marketplace with custom filters.
        /// </summary>
        /// <param name="name">The name of the item to search for.</param>
        /// <param name="filters">A collection of <see cref="SearchTags"/>.</param>
        /// <param name="sortBy">The method of sorting.</param>
        /// <param name="sortDirection">The direction of sorting.</param>
        /// <param name="limit">The maximum number of items to return. Must be between 0 and 500.</param>
        /// <param name="offset">The number of items to skip before returning results. Must be non-negative.</param>
        /// <param name="local">The locale to use for the search results. Defaults to English (en).</param>
        /// <returns>A read-only list of matching <see cref="PurchasableItem"/> objects.</returns>
        public async Task<IReadOnlyList<PurchasableItem>> SearchItem(
            string name = "",
            IEnumerable<Enum>? filters = null,
            SortBy sortBy = SortBy.PurchaseAvailaible,
            SortDirection sortDirection = SortDirection.DESC,
            int limit = 40,
            int offset = 0,
            Data.Local local = Data.Local.en)
        {
            var (types, tags) = SearchTags.PrepareTags(filters);
            return await _SearchItem(name, types, tags, sortBy, sortDirection, limit, offset, local);
        }



        /// <summary>
        /// Search for items in the marketplace with custom filters and without type safety.
        /// </summary>
        /// <remarks>
        /// Warning:<br></br> If you use tags/types from <see cref="SearchTags"/>, you must use <see cref="SearchTags.GetAPIName(string)"/> on each tag
        /// </remarks>
        /// <param name="name">The name of the item to search for.</param>
        /// <param name="types">A collection of item types (<see cref="SearchTags.Type"/>)</param>
        /// <param name="tags">A collection of item tags (<see cref="SearchTags"/> EXCEPT <see cref="SearchTags.Type"/>)</param>
        /// <param name="sortBy">The method of sorting.</param>
        /// <param name="sortDirection">The direction of sorting.</param>
        /// <param name="limit">The maximum number of items to return. Must be between 0 and 500.</param>
        /// <param name="offset">The number of items to skip before returning results. Must be non-negative.</param>
        /// <param name="local">The locale to use for the search results. Defaults to English (en).</param>
        /// <returns>A read-only list of matching <see cref="PurchasableItem"/> objects.</returns>
        public async Task<IReadOnlyList<PurchasableItem>> SearchItemUnrestricted(
            string name,
            List<string> types,
            List<string> tags,
            SortBy sortBy = SortBy.PurchaseAvailaible,
            SortDirection sortDirection = SortDirection.DESC,
            int limit = 40,
            int offset = 0,
            Data.Local local = Data.Local.en)
            => await _SearchItem(name, types, tags, sortBy, sortDirection, limit, offset, local);

        private async Task<IReadOnlyList<PurchasableItem>> _SearchItem(
            string name,
            List<string> types,
            List<string> tags,
            SortBy sortBy,
            SortDirection sortDirection,
            int limit,
            int offset,
            Data.Local local)
        {
            if (limit < 0 || offset < 0)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit and offset cannot be negative.");
            if (limit > 500)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit cannot be greater than 500.");

            web.EnsureAuthenticated();

            var body = new RequestBodies.SearchItems.Root(
                name, limit, offset, types, tags, sortBy, sortDirection);

            var response = await web.Post(Data.dataUri, body.AsJson());

            var json = await response.DeserializeAsyncSafe<List<Classes.SearchResponse.RawData.Root>>(false);
            try
            {
                if (json == null || json[0].data.game.marketableItems.nodes.Count == 0)
                    return new List<PurchasableItem>();
            }
            catch
            {
                return new List<PurchasableItem>();
            }

                return json[0].data.game.marketableItems.nodes.Select(x => new PurchasableItem(transactionsEndpoints)
            {
                ID = x.item.itemId,
                Name = x.item.name,
                AssetUrl = new Classes.ImageUri(x.item.assetUrl),
                Type = x.item.type,
                Tags = x.item.tags,
                LastSoldAtPrice = x.marketData.lastSoldAt?[0].price ?? -1,
                LastSoldAtTime = x.marketData.lastSoldAt?[0].performedAt,
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

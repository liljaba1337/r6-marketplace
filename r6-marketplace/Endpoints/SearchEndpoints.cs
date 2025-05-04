using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;
using r6_marketplace.Utils.Exceptions;

namespace r6_marketplace.Endpoints
{
    public class SearchEndpoints
    {
        private Web web;
        internal SearchEndpoints(Web web)
        {
            this.web = web;
        }
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

        public async Task<List<Item>> SearchItem(string? name = default, List<string>? types = default, List<string>? tags = default,
            SortBy sortBy = SortBy.PurchaseAvailaible, SortDirection sortDirection = SortDirection.DESC,
            int limit = 40, int offset = 0)
        {
            if (limit < 0 || offset < 0)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit and offset cannot be negative.");
            web.EnsureAuthenticated();

            var body = new RequestBodies.SearchItems.Root(
                name ?? "", limit, offset, types ?? new List<string>(), tags ?? new List<string>(), sortBy, sortDirection);
            var response = await web.Post(Data.dataUri, body.AsJson());
            var json = await response.DeserializeAsyncSafe<List<Classes.SearchResponse.RawData.Root>>(false);
            if(json == null || json[0].data.game.marketableItems.nodes.Count == 0)
                return new List<Item>();

            return json[0].data.game.marketableItems.nodes.Select(x => new Item
            {
                ID = x.item.itemId,
                Name = x.item.name,
                AssetUrl = x.item.assetUrl,
                Type = x.item.type,
                Tags = x.item.tags,
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

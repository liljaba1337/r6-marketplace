using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;
using static r6_marketplace.Extensions.ItemsExtensions;
using static r6_marketplace.Endpoints.SearchEndpoints;

namespace r6_marketplace.Endpoints
{
    public static class AccountEndpointsExtensions
    {
        /// <summary>
        /// Get the total value of the inventory based on the current lowest price, last sale price, and current lowest buy order price.
        /// </summary>
        /// <returns>An <see cref="InventoryValue"/> instance containing all the calculated values.</returns>
        public static InventoryValue GetTotalValue(this IReadOnlyList<SellableItem> items)
            => items._GetInventoryValue();
    }
    public class AccountEndpoints : EndpointsBase
    {
        private readonly TransactionsEndpoints transactionsEndpoints;
        internal AccountEndpoints(Web web, TransactionsEndpoints transactionsEndpoints) : base(web)
        { this.transactionsEndpoints = transactionsEndpoints; }
        /// <summary>
        /// Get the current balance of the account.
        /// </summary>
        /// <returns>Your balance, or -1 if an error occured.</returns>
        public async Task<int> GetBalance()
        {
            web.EnsureAuthenticated();
            var response = await web.Post(Data.dataUri, new RequestBodies.AccountData.GetBalance.Root().AsJson());
            var rawBalance = await response.DeserializeAsyncSafe<List<Classes.BalanceResponse.Root>>();
            return rawBalance?[0]?.data?.game?.viewer?.meta?.secondaryStoreItem?.meta?.quantity ?? -1;
        }

        /// <summary>
        /// Gets the items from your inventory with optional filtering.
        /// </summary>
        /// <param name="name">The name of the item to search for.</param>
        /// <param name="filters">A collection of <see cref="SearchTags"/>.</param>
        /// <param name="sortBy">The method of sorting.</param>
        /// <param name="sortDirection">The direction of sorting.</param>
        /// <param name="limit">The maximum number of items to return. Must be between 0 and 500.</param>
        /// <param name="offset">The number of items to skip before returning results. Must be non-negative.</param>
        /// <param name="local">The locale to use for the search results. Defaults to English (en).</param>
        /// An empty list can be returned if you don't have access to the marketplace, but I'm not sure about that.</returns>
        public async Task<IReadOnlyList<SellableItem>> GetInventory(
            string name = "",
            IEnumerable<Enum>? filters = null,
            SortBy sortBy = SortBy.PurchaseAvailaible,
            SortDirection sortDirection = SortDirection.DESC,
            int limit = 40,
            int offset = 0,
            Data.Local local = Data.Local.en)
        {
            var (types, tags) = SearchTags.PrepareTags(filters);
            return await _GetInventory(name, types, tags, sortBy, sortDirection, limit, offset, local);
        }

        /// <summary>
        /// Gets the items from your inventory with optional filtering and without type safety.
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
        /// <returns>A read-only list of matching <see cref="SellableItem"/> objects.</returns>
        public async Task<IReadOnlyList<SellableItem>> GetInventoryUnrestricted(
            string name,
            List<string> types,
            List<string> tags,
            SortBy sortBy = SortBy.PurchaseAvailaible,
            SortDirection sortDirection = SortDirection.DESC,
            int limit = 40,
            int offset = 0,
            Data.Local local = Data.Local.en)
            => await _GetInventory(name, types, tags, sortBy, sortDirection, limit, offset, local);

        private async Task<IReadOnlyList<SellableItem>> _GetInventory(
            string name,
            List<string> types,
            List<string> tags,
            SortBy sortBy,
            SortDirection sortDirection,
            int limit,
            int offset,
            Data.Local local
            )
        {
            web.EnsureAuthenticated();
            if (limit < 0 || offset < 0)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit and offset cannot be negative.");
            if (limit > 500)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit cannot be greater than 500.");

            var response = await web.Post(Data.dataUri, new RequestBodies.AccountData.GetInventory.Root(
                name, limit, offset, types, tags, sortBy, sortDirection).AsJson());

            var json = await response.DeserializeAsyncSafe<List<Classes.InventoryResponse.RawData.Root>>(false);
            if (json == null || json[0].data.game.viewer.meta.marketableItems.nodes.Count == 0)
                return new List<SellableItem>();
            return json[0].data.game.viewer.meta.marketableItems.nodes.Select(x => new SellableItem(transactionsEndpoints)
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

        /// <summary>
        /// Gets the total value of the inventory based on the current lowest price, last sale price, and current lowest buy order price.
        /// </summary>
        /// <returns>An <see cref="InventoryValue"/> instance containing all the calculated values.</returns>
        public async Task<InventoryValue> GetInventoryValue()
        {
            var inventory = await GetInventory(limit: 400, sortBy:SortBy.LastSalePrice, sortDirection:SortDirection.DESC);
            return inventory._GetInventoryValue();
        }
    }
}

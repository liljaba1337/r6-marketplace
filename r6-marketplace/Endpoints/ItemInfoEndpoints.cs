﻿using System.Text.Json;
using r6_marketplace.Classes.Item;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;
using r6_marketplace.Utils.Exceptions;

namespace r6_marketplace.Endpoints
{
    public class ItemInfoEndpoints : EndpointsBase
    {
        internal ItemInfoEndpoints(Web web) : base(web) { }

        /// <summary>
        /// Get item info by its ID.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="lang"></param>
        /// <returns><see cref="r6_marketplace.Classes.Item.Item"/> if the item was found, or <c>null</c> if not.</returns>
        /// <exception cref="UnsuccessfulStatusCodeException"></exception>
        /// <exception cref="InvalidTokenException"></exception>
        public async Task<Item?> GetItem(string itemId, Data.Local lang = Data.Local.en)
        {
            web.EnsureAuthenticated();
            var response = await web.Post(Data.dataUri, 
                JsonSerializer.Serialize(new RequestBodies.GetItemDetails.Root(itemId).AsList()),
                local:lang);
            
            var rawitem = await response.DeserializeAsyncSafe<List<Classes.Item.RawData.Root>>();
            if (rawitem is not { Count: > 0 })
                return null;

            var sellStats = rawitem[0].data.game.marketableItem.marketData.sellStats;
            var buyStats = rawitem[0].data.game.marketableItem.marketData.buyStats;
            return new Item
            {
                ID = rawitem[0].data.game.marketableItem.item.itemId,
                Name = rawitem[0].data.game.marketableItem.item.name,
                AssetUrl = new Classes.ImageUri(rawitem[0].data.game.marketableItem.item.assetUrl),
                Type = rawitem[0].data.game.marketableItem.item.type,
                Tags = rawitem[0].data.game.marketableItem.item.tags,
                BuyOrdersStats = buyStats != null ? new OrdersStats()
                {
                    lowestPrice = buyStats[0].lowestPrice,
                    highestPrice = buyStats[0].highestPrice,
                    activeCount = buyStats[0].activeCount
                } : null,
                SellOrdersStats = sellStats != null ? new OrdersStats()
                {
                    lowestPrice = sellStats[0].lowestPrice,
                    highestPrice = sellStats[0].highestPrice,
                    activeCount = sellStats[0].activeCount
                } : null
            };
        }

        /// <summary>
        /// Get item price history (past 30 days) by its ID.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns><see cref="ItemPriceHistory"/> if the item was found, or <c>null</c> if not.</returns>
        /// <exception cref="UnsuccessfulStatusCodeException"></exception>
        /// <exception cref="InvalidTokenException"></exception>
        public async Task<ItemPriceHistory?> GetItemPriceHistory(string itemId)
        {
            web.EnsureAuthenticated();
            var response = await web.Post(Data.dataUri, new RequestBodies.GetItemPriceHistory.Root(itemId).AsJson());

            var rawitem = await response.DeserializeAsyncSafe<List<Classes.Item.PriceHistoryRaw.Root>>();

            if (rawitem is not { Count: > 0 })
                return null;

            return new ItemPriceHistory(rawitem[0].data?.game?.marketableItem?.priceHistory?
                .Select(p => new ItemPriceHistoryEntry
                {
                    Date = DateTime.TryParse(p.date, out var parsedDate) ? parsedDate : DateTime.MinValue,
                    LowestPrice = p.lowestPrice,
                    AveragePrice = p.averagePrice,
                    HighestPrice = p.highestPrice,
                    ItemsCount = p.itemsCount
                }).ToList() ?? new List<ItemPriceHistoryEntry>());
        }
    }
}

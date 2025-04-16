using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;
using r6_marketplace.Classes.Item.Error;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;
using r6_marketplace.Utils.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace r6_marketplace.Endpoints
{
    public class ItemInfoEndpoints
    {
        private Web web;
        internal ItemInfoEndpoints(Web web)
        {
            this.web = web;
        }

        /// <summary>
        /// Get item info by its ID (i.e. ccb2fda5-c0a9-7c0a-8f8f-37cb00249659)
        /// </summary>
        /// <param name="itemId">The item ID to search for.</param>
        /// <param name="lang"></param>
        /// <returns><see cref="r6_marketplace.Classes.Item.Item"/> if the item was found, or <c>null</c> if not.</returns>
        /// <exception cref="UnsuccessfulStatusCodeException"></exception>
        public async Task<Item?> GetItem(string itemId, Data.Local lang = Data.Local.en)
        {
            web.EnsureAuthenticated();
            var response = await web.Post(Data.dataUri, 
                RequestBodies.GetItemData.Replace("{ITEMID}", itemId), local:lang);
            if (!response.IsSuccessStatusCode)
            {
                throw new UnsuccessfulStatusCodeException(response.StatusCode.ToString());
            }

            var json = await response.Content.ReadAsStringAsyncSafe();

            var error = System.Text.Json.JsonSerializer.Deserialize
                <List<Classes.Item.Error.ApiError>>(json);
            if (IsNotFoundError(error))
                return null;
            if (IsInvalidTokenError(error))
                throw new InvalidTokenException("The authentication ticket is invalid. Please re-authenticate.");

            var rawitem = System.Text.Json.JsonSerializer.Deserialize
                <List<Classes.Item.RawData.Root>>(json);

            if (rawitem is not { Count: > 0 })
                return null;

            var sellStats = rawitem[0].data.game.marketableItem.marketData.sellStats;
            var buyStats = rawitem[0].data.game.marketableItem.marketData.buyStats;
            return new Item
            {
                Name = rawitem[0].data.game.marketableItem.item.name,
                assetUrl = rawitem[0].data.game.marketableItem.item.assetUrl,
                BuyOrdersStats = buyStats != null ? new BuyStats()
                {
                    lowestPrice = buyStats[0].lowestPrice,
                    highestPrice = buyStats[0].highestPrice,
                    activeCount = buyStats[0].activeCount
                } : null,
                SellOrdersStats = sellStats != null ? new SellStats()
                {
                    lowestPrice = sellStats[0].lowestPrice,
                    highestPrice = sellStats[0].highestPrice,
                    activeCount = sellStats[0].activeCount
                } : null
            };
        }
        private static bool IsNotFoundError(List<ApiError>? errors)
        {
            return errors?.Any(e => e.errors?.Any(err => err.message.Contains("404")) == true) == true;
        }
        private static bool IsInvalidTokenError(List<ApiError>? errors)
        {
            return errors?.Any(e => e.errors?.Any(err => err.message.Contains("The authentication ticket is invalid")) == true) == true;
        }
    }
}

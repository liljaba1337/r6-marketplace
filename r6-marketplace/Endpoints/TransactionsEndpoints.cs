using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;

namespace r6_marketplace.Endpoints
{
    public class TransactionsEndpoints : EndpointsBase
    {
        internal TransactionsEndpoints(Web web) : base(web) { }

        /// <summary>
        /// Get active buy/sell orders.
        /// </summary>
        /// <param name="local">Language to retrieve items' metadata in.</param>
        /// <param name="limit">I don't really understand why it's here, as you can't have more than 10 active orders at a time anyway.
        /// But Ubisoft API requires this parameter, so I thought it might be useful to include it here. Must be non-negative.</param>
        /// <param name="offset">The number of orders to skip before returning results. Must be non-negative.</param>
        /// <returns>A read-only list of <see cref="Classes.GetActiveOrders.Simplified.Order"/> instances (may be empty if there are no orders)
        /// or null if an error occured</returns>
        public async Task<IReadOnlyList<Classes.GetActiveOrders.Simplified.Order>?>
            GetActiveOrders(Data.Local local = Data.Local.en, int limit = 40, int offset = 0)
        {
            web.EnsureAuthenticated();

            if (limit < 0 || offset < 0)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit and offset cannot be negative.");

            var response = await web.Post(Data.dataUri,
                JsonSerializer.Serialize(new RequestBodies.AccountOrders.Active.Root(limit, offset).AsList()),
                local: local);

            var rawitem = await response.DeserializeAsyncSafe<List<Classes.GetActiveOrders.Raw.Root>>(false);
            if (rawitem is not { Count: > 0 })
                return null;

            return rawitem[0].data.game.viewer.meta.trades.nodes.Select(x => new Classes.GetActiveOrders.Simplified.Order()
            {
                ID = x.tradeId,
                OrderType = Classes.GetActiveOrders.Simplified.Types.ConvertOrderType(x.category),
                CreatedAt = x.createdAt,
                ExpiresAt = x.expiresAt,
                LastModifiedAt = x.lastModifiedAt,
                Item = new Classes.Item.Item()
                {
                    ID = x.tradeItems[0].item.itemId,
                    Name = x.tradeItems[0].item.name,
                    AssetUrl = x.tradeItems[0].item.assetUrl,
                    Type = x.tradeItems[0].item.type,
                    Tags = x.tradeItems[0].item.tags
                },
                Price = x.paymentOptions.Count > 0 ? x.paymentOptions[0].price : x.paymentProposal.price,
                Fee = x.paymentOptions.Count > 0 ? x.paymentOptions[0].transactionFee : 0,
                NetAmount = x.category == "Sell" ? x.paymentOptions[0].price - x.paymentOptions[0].transactionFee : 0,
            }).ToList();
        }
    }
}

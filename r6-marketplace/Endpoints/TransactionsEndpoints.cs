using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;
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
        /// <returns>A read-only list of <see cref="Classes.Orders.Order"/> instances (empty if there are no orders).</returns>
        public async Task<IReadOnlyList<Classes.Orders.Order>?>
            GetActiveOrders(int limit = 40, int offset = 0, Data.Local local = Data.Local.en)
        {
            web.EnsureAuthenticated();

            if (limit < 0 || offset < 0)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit and offset cannot be negative.");

            var response = await web.Post(Data.dataUri,
                JsonSerializer.Serialize(new RequestBodies.AccountOrders.Active.Root(limit, offset).AsList()),
                local: local);

            var rawitem = await response.DeserializeAsyncSafe<List<Classes.Orders.Raw.Root>>(false);
            if (rawitem is not { Count: > 0 })
                return new List<Classes.Orders.Order>();

            return rawitem[0].data.game.viewer.meta.trades.nodes.Select(x => new Classes.Orders.Order(this)
            {
                ID = x.tradeId,
                OrderType = Classes.Orders.Types.ConvertOrderType(x.category),
                CreatedAt = x.createdAt,
                ExpiresAt = x.expiresAt,
                LastModifiedAt = x.lastModifiedAt,
                Item = new Item()
                {
                    ID = x.tradeItems[0].item.itemId,
                    Name = x.tradeItems[0].item.name,
                    AssetUrl = new Classes.ImageUri(x.tradeItems[0].item.assetUrl),
                    Type = x.tradeItems[0].item.type,
                    Tags = x.tradeItems[0].item.tags
                },
                Price = x.paymentOptions.Count > 0 ? x.paymentOptions[0].price : x.paymentProposal.price,
                Fee = x.paymentOptions.Count > 0 ? x.category == "Sell" ? x.paymentOptions[0].transactionFee : 0 : 0,
                NetAmount = x.paymentOptions.Count > 0 ? x.category == "Sell" ?
                    x.paymentOptions[0].price - x.paymentOptions[0].transactionFee : 0 : 0,
            }).ToList();
        }

        /// <summary>
        /// Get a history of buy/sell orders.
        /// </summary>
        /// <param name="local">Language to retrieve items' metadata in.</param>
        /// <param name="limit">The number of orders to return. Must be non-negative.</param>
        /// <param name="offset">The number of orders to skip before returning results. Must be non-negative.</param>
        /// <returns>A read-only list of <see cref="Classes.Orders.HistoryOrder"/> instances (empty if there are no orders).</returns>
        public async Task<IReadOnlyList<Classes.Orders.HistoryOrder>>
            GetOrdersHistory(int limit = 40, int offset = 0, Data.Local local = Data.Local.en)
        {
            web.EnsureAuthenticated();

            if (limit < 0 || offset < 0)
                throw new ArgumentOutOfRangeException(nameof(limit), "Limit and offset cannot be negative.");

            var response = await web.Post(Data.dataUri,
                JsonSerializer.Serialize(new RequestBodies.AccountOrders.History.Root(limit, offset).AsList()),
                local: local);
            var rawitem = await response.DeserializeAsyncSafe<List<Classes.Orders.History.Raw.Root>>(false);
            if (rawitem is not { Count: > 0 })
                return new List<Classes.Orders.HistoryOrder>();

            return rawitem[0].data.game.viewer.meta.trades.nodes.Select(x => new Classes.Orders.HistoryOrder(this)
            {
                ID = x.tradeId,
                OrderType = Classes.Orders.Types.ConvertOrderType(x.category),
                CreatedAt = x.createdAt,
                State = Classes.Orders.Types.ConvertOrderState(x.state),
                LastModifiedAt = x.lastModifiedAt,
                Item = new Item()
                {
                    ID = x.tradeItems[0].item.itemId,
                    Name = x.tradeItems[0].item.name,
                    AssetUrl = new Classes.ImageUri(x.tradeItems[0].item.assetUrl),
                    Type = x.tradeItems[0].item.type,
                    Tags = x.tradeItems[0].item.tags
                },
                Price = x.paymentOptions.Count > 0 ? x.paymentOptions[0].price : 0,
                Fee = x.paymentOptions.Count > 0 ? x.category == "Sell" ? x.paymentOptions[0].transactionFee : 0 : 0,
                NetAmount = x.paymentOptions.Count > 0 ? x.category == "Sell" ?
                    x.paymentOptions[0].price - x.paymentOptions[0].transactionFee : 0 : 0,
            }).ToList();
        }

        /// <summary>
        /// Create a sell order for an item.
        /// </summary>
        /// <param name="itemid">The ID of the item.</param>
        /// <param name="price">The price you want to sell this item for. Must be between 10 and 1000000.</param>
        /// <returns>An instance of <see cref="Classes.Orders.Order"/> if the order was places successfully.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<Classes.Orders.Order> CreateSellOrder(string itemid, int price, Data.Local local = Data.Local.en)
        {
            web.EnsureAuthenticated();
            if (price < 10 || price > 1000000)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be between 10 and 1000000.");
            var response = await web.Post(Data.dataUri, new RequestBodies.AccountOrders.Sell.Root(itemid, price).AsJson(), local:local);

            var rawitem = await response.DeserializeAsyncSafe<List<Classes.CreateSellOrder.Root>>(false);
            var x = rawitem?[0]?.data?.createSellOrder?.trade;

            if (x == null)
                throw new Exception("An error occurred. The item doesn't exist or you don't have access to the marketplace.");

            return new Classes.Orders.Order(this)
            {
                ID = x.tradeId,
                OrderType = Classes.Orders.Types.ConvertOrderType(x.category),
                CreatedAt = x.createdAt,
                ExpiresAt = x.expiresAt,
                LastModifiedAt = x.lastModifiedAt,
                Item = new Item()
                {
                    ID = x.tradeItems[0].item.itemId,
                    Name = x.tradeItems[0].item.name,
                    AssetUrl = new Classes.ImageUri(x.tradeItems[0].item.assetUrl),
                    Type = x.tradeItems[0].item.type,
                    Tags = x.tradeItems[0].item.tags
                },
                Price = x.paymentOptions.Count > 0 ? x.paymentOptions[0].price : 0,
                Fee = x.paymentOptions.Count > 0 ? x.paymentOptions[0].transactionFee : 0,
                NetAmount = x.paymentOptions.Count > 0 ? x.paymentOptions[0].price - x.paymentOptions[0].transactionFee : 0,
            };
        }
        /// <summary>
        /// Create a buy order for an item.
        /// </summary>
        /// <param name="itemid">The ID of the item.</param>
        /// <param name="price">The price you want to buy this item for. Must be between 10 and 1000000.</param>
        /// <returns>An instance of <see cref="Classes.Orders.Order"/> if the order was places successfully.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<Classes.Orders.Order> CreateBuyOrder(string itemid, int price, Data.Local local = Data.Local.en)
        {
            web.EnsureAuthenticated();
            if (price < 10 || price > 1000000)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be between 10 and 1000000.");
            var response = await web.Post(Data.dataUri, new RequestBodies.AccountOrders.Buy.Root(itemid, price).AsJson(), local:local);

            var rawitem = await response.DeserializeAsyncSafe<List<Classes.CreateBuyOrder.Root>>(false);
            var x = rawitem?[0]?.data?.createBuyOrder?.trade;

            if (x == null)
                throw new Exception("An error occurred. The item doesn't exist, you don't have access to the marketplace " +
                    "or you have exceeded your balance.");

            return new Classes.Orders.Order(this)
            {
                ID = x.tradeId,
                OrderType = Classes.Orders.Types.ConvertOrderType(x.category),
                CreatedAt = x.createdAt,
                ExpiresAt = x.expiresAt,
                LastModifiedAt = x.lastModifiedAt,
                Item = new Item()
                {
                    ID = x.tradeItems[0].item.itemId,
                    Name = x.tradeItems[0].item.name,
                    AssetUrl = new Classes.ImageUri(x.tradeItems[0].item.assetUrl),
                    Type = x.tradeItems[0].item.type,
                    Tags = x.tradeItems[0].item.tags
                },
                Price = x.paymentProposal.price,
                Fee = 0,
                NetAmount = 0,
            };
        }
        /// <summary>
        /// Cancel an order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>For some reason, API always returns an error even if the order was cancelled successfully.
        /// So right now there is no way to tell if an order was cancelled or not, except retrieving active orders again.</returns>
        public async Task CancelOrder(string orderId)
        {
            web.EnsureAuthenticated();
            await web.Post(Data.dataUri, new RequestBodies.AccountOrders.Cancel.Root(orderId).AsJson());
        }
    }
}

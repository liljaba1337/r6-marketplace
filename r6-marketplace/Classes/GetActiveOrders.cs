using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;
using r6_marketplace.Endpoints;
using r6_marketplace.Utils;

namespace r6_marketplace.Classes.Orders.Raw
{
    internal class Buy
    {
        public string id { get; set; }
        public int resolvedTransactionCount { get; set; }
        public int resolvedTransactionPeriodInMinutes { get; set; }
        public int activeTransactionCount { get; set; }
        public string __typename { get; set; }
    }

    internal class Data
    {
        public Game game { get; set; }
    }

    internal class Game
    {
        public string id { get; set; }
        public Viewer viewer { get; set; }
        public string __typename { get; set; }
    }

    internal class Item
    {
        public string id { get; set; }
        public string assetUrl { get; set; }
        public string itemId { get; set; }
        public string name { get; set; }
        public List<string> tags { get; set; }
        public string type { get; set; }
        public string __typename { get; set; }
        public Viewer viewer { get; set; }
    }

    internal class Meta
    {
        public string id { get; set; }
        public Trades trades { get; set; }
        public string __typename { get; set; }
        public bool isOwned { get; set; }
        public int quantity { get; set; }
        public TradesLimitations tradesLimitations { get; set; }
    }

    internal class Node
    {
        public string id { get; set; }
        public string tradeId { get; set; }
        public string state { get; set; }
        public string category { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime expiresAt { get; set; }
        public DateTime lastModifiedAt { get; set; }
        public List<object> failures { get; set; }
        public List<TradeItem> tradeItems { get; set; }
        public object payment { get; set; }
        public List<PaymentOption> paymentOptions { get; set; }
        public PaymentProposal paymentProposal { get; set; }
        public Viewer viewer { get; set; }
        public string __typename { get; set; }
    }

    internal class PaymentOption
    {
        public string id { get; set; }
        public Item item { get; set; }
        public int price { get; set; }
        public int transactionFee { get; set; }
        public string __typename { get; set; }
    }

    internal class PaymentProposal
    {
        public string id { get; set; }
        public Item item { get; set; }
        public int price { get; set; }
        public string __typename { get; set; }
    }

    internal class ResaleLock
    {
        public string id { get; set; }
        public string itemId { get; set; }
        public DateTime expiresAt { get; set; }
        public string __typename { get; set; }
    }

    internal class Root
    {
        public Data data { get; set; }
    }

    internal class Sell
    {
        public string id { get; set; }
        public int resolvedTransactionCount { get; set; }
        public int resolvedTransactionPeriodInMinutes { get; set; }
        public int activeTransactionCount { get; set; }
        public List<ResaleLock> resaleLocks { get; set; }
        public string __typename { get; set; }
    }

    internal class TradeItem
    {
        public string id { get; set; }
        public Item item { get; set; }
        public string __typename { get; set; }
    }

    internal class Trades
    {
        public List<Node> nodes { get; set; }
        public string __typename { get; set; }
    }

    internal class TradesLimitations
    {
        public string id { get; set; }
        public Buy buy { get; set; }
        public Sell sell { get; set; }
        public string __typename { get; set; }
    }

    internal class Viewer
    {
        public Meta meta { get; set; }
        public string __typename { get; set; }
    }
}

namespace r6_marketplace.Classes.Orders
{
    internal static class Types
    {
        internal static Data.OrderType ConvertOrderType(string _ordertype)
        {
            return _ordertype switch
            {
                "Buy" => Data.OrderType.Buy,
                "Sell" => Data.OrderType.Sell,
                _ => throw new ArgumentOutOfRangeException(nameof(_ordertype), _ordertype, null)
            };
        }
    }
    // Might be a good idea to split this into BuyOrder and SellOrder later.
    public class Order
    {
        private readonly TransactionsEndpoints transactionsEndpoints;
        internal Order(TransactionsEndpoints transactionsEndpoints) { this.transactionsEndpoints = transactionsEndpoints; }
        public string ID { get; internal set; }
        public Data.OrderType OrderType { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime ExpiresAt { get; internal set; }
        public DateTime LastModifiedAt { get; internal set; }
        public Item.Item Item { get; internal set; }
        public int Price { get; internal set; }
        /// <summary>
        /// For purchase orders this is always 0.
        /// </summary>
        public int Fee { get; internal set; }
        /// <summary>
        /// The amount you will receive after selling this item. For purchase orders this is always 0.
        /// </summary>
        public int NetAmount { get; internal set; }
        /// <summary>
        /// Cancel this order.
        /// </summary>
        /// <param name="orderId">The ID of an order to cancel.</param>
        /// <returns>For some reason, API always returns an error even if the order was cancelled successfully.
        /// So right now there is no way to tell if an order was cancelled or not, except retrieving active orders again.</returns>
        public async Task Cancel()
            => await transactionsEndpoints.CancelOrder(ID);
    }
}
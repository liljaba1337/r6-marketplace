#pragma warning disable CS8618

using System.Collections;

namespace r6_marketplace.Classes.Item.RawData
{
    internal class BuyStat
    {
        public string id { get; set; }
        public string paymentItemId { get; set; }
        public int lowestPrice { get; set; }
        public int highestPrice { get; set; }
        public int activeCount { get; set; }
        public string __typename { get; set; }
    }

    internal class Data
    {
        public Game game { get; set; }
    }

    internal class Game
    {
        public string id { get; set; }
        public MarketableItem marketableItem { get; set; }
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

    internal class LastSoldAt
    {
        public string id { get; set; }
        public string paymentItemId { get; set; }
        public int price { get; set; }
        public DateTime performedAt { get; set; }
        public string __typename { get; set; }
    }

    internal class MarketableItem
    {
        public string id { get; set; }
        public Item item { get; set; }
        public MarketData marketData { get; set; }
        public List<PaymentLimitation> paymentLimitations { get; set; }
        public string __typename { get; set; }
    }

    internal class MarketData
    {
        public string id { get; set; }
        public List<SellStat> sellStats { get; set; }
        public List<BuyStat> buyStats { get; set; }
        public List<LastSoldAt> lastSoldAt { get; set; }
        public string __typename { get; set; }
    }

    internal class Meta
    {
        public string id { get; set; }
        public bool isOwned { get; set; }
        public int quantity { get; set; }
        public string __typename { get; set; }
        public Trades trades { get; set; }
    }

    internal class PaymentLimitation
    {
        public string id { get; set; }
        public string paymentItemId { get; set; }
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
        public string __typename { get; set; }
    }

    internal class Root
    {
        public Data data { get; set; }
    }

    internal class SellStat
    {
        public string id { get; set; }
        public string paymentItemId { get; set; }
        public int lowestPrice { get; set; }
        public int highestPrice { get; set; }
        public int activeCount { get; set; }
        public string __typename { get; set; }
    }

    internal class Trades
    {
        public List<object> nodes { get; set; }
        public string __typename { get; set; }
    }

    internal class Viewer
    {
        public Meta meta { get; set; }
        public string __typename { get; set; }
    }
}

// simplified
namespace r6_marketplace.Classes.Item
{
    public class Item
    {
        public string Name { get; internal set; }
        public string assetUrl { get; internal set; }
        public OrdersStats? SellOrdersStats { get; internal set; }
        public OrdersStats? BuyOrdersStats { get; internal set; }
    }
    public class OrdersStats
    {
        public int lowestPrice { get; internal set; }
        public int highestPrice { get; internal set; }
        public int activeCount { get; internal set; }
    }
}

namespace r6_marketplace.Classes.ItemPriceHistory.RawData
{
    internal class Data
    {
        public Game game { get; set; }
    }

    internal class Game
    {
        public string id { get; set; }
        public MarketableItem marketableItem { get; set; }
        public string __typename { get; set; }
    }

    internal class MarketableItem
    {
        public string id { get; set; }
        public List<PriceHistory> priceHistory { get; set; }
        public string __typename { get; set; }
    }

    internal class PriceHistory
    {
        public string id { get; set; }
        public string date { get; set; }
        public int lowestPrice { get; set; }
        public int averagePrice { get; set; }
        public int highestPrice { get; set; }
        public int itemsCount { get; set; }
        public string __typename { get; set; }
    }

    internal class Root
    {
        public Data data { get; set; }
    }
}

namespace r6_marketplace.Classes.ItemPriceHistory
{
    /// <summary>
    /// Represents the price history of an item over the past 30 days.
    /// </summary>
    public class ItemPriceHistory : IEnumerable<ItemPriceHistoryEntry>
    {
        /// <summary>
        /// The highest price at which the item was sold in the past 30 days.
        /// </summary>
        public int AllTimeHigh { get; }
        /// <summary>
        /// The highest daily average price of the item in the past 30 days.
        /// </summary>
        public int AllTimeAverageHigh { get; }
        /// <summary>
        /// The lowest daily average price of the item in the past 30 days.
        /// </summary>
        public int AllTimeAverageLow { get; }
        public ItemPriceHistory(IEnumerable<ItemPriceHistoryEntry> history)
        {
            _history = new List<ItemPriceHistoryEntry>(history);
            AllTimeHigh = _history.Count > 0 ? _history.Select(x => x.highestPrice).Max() : 0;
            AllTimeAverageHigh = _history.Count > 0 ? _history.Select(x => x.averagePrice).Max() : 0;
            AllTimeAverageLow = _history.Count > 0 ? _history.Select(x => x.averagePrice).Min() : 0;
        }
        private readonly List<ItemPriceHistoryEntry> _history = new();
        public IReadOnlyList<ItemPriceHistoryEntry> History => _history;
        public ItemPriceHistoryEntry this[int index] => _history[index];
        public int Count => _history.Count;
        public IEnumerator<ItemPriceHistoryEntry> GetEnumerator() => _history.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
    /// <summary>
    /// Represents a single entry (day) in the item price history.
    /// </summary>
    public class ItemPriceHistoryEntry
    {
        public DateTime date { get; internal set; }
        public int lowestPrice { get; internal set; }
        public int averagePrice { get; internal set; }
        public int highestPrice { get; internal set; }
        public int itemsCount { get; internal set; }
    }
}

namespace r6_marketplace.Classes.Item.Error
{
    internal class ApiError
    {
        public List<ErrorDetail> errors { get; set; }
    }

    internal class ErrorDetail
    {
        public string message { get; set; }
    }
}
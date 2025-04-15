#pragma warning disable CS8618

using System.Collections;

namespace r6_marketplace.Classes.Item.RawData
{
    public class BuyStat
    {
        public string id { get; set; }
        public string paymentItemId { get; set; }
        public int lowestPrice { get; set; }
        public int highestPrice { get; set; }
        public int activeCount { get; set; }
        public string __typename { get; set; }
    }

    public class Data
    {
        public Game game { get; set; }
    }

    public class Game
    {
        public string id { get; set; }
        public MarketableItem marketableItem { get; set; }
        public Viewer viewer { get; set; }
        public string __typename { get; set; }
    }

    public class Item
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

    public class LastSoldAt
    {
        public string id { get; set; }
        public string paymentItemId { get; set; }
        public int price { get; set; }
        public DateTime performedAt { get; set; }
        public string __typename { get; set; }
    }

    public class MarketableItem
    {
        public string id { get; set; }
        public Item item { get; set; }
        public MarketData marketData { get; set; }
        public List<PaymentLimitation> paymentLimitations { get; set; }
        public string __typename { get; set; }
    }

    public class MarketData
    {
        public string id { get; set; }
        public List<SellStat> sellStats { get; set; }
        public List<BuyStat> buyStats { get; set; }
        public List<LastSoldAt> lastSoldAt { get; set; }
        public string __typename { get; set; }
    }

    public class Meta
    {
        public string id { get; set; }
        public bool isOwned { get; set; }
        public int quantity { get; set; }
        public string __typename { get; set; }
        public Trades trades { get; set; }
    }

    public class PaymentLimitation
    {
        public string id { get; set; }
        public string paymentItemId { get; set; }
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
        public string __typename { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }

    public class SellStat
    {
        public string id { get; set; }
        public string paymentItemId { get; set; }
        public int lowestPrice { get; set; }
        public int highestPrice { get; set; }
        public int activeCount { get; set; }
        public string __typename { get; set; }
    }

    public class Trades
    {
        public List<object> nodes { get; set; }
        public string __typename { get; set; }
    }

    public class Viewer
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
        public string Name { get; set; }
        public SellStats? SellOrdersStats { get; set; }
        public BuyStats? BuyOrdersStats { get; set; }
    }
    public class SellStats
    {
        public int lowestPrice { get; set; }
        public int highestPrice { get; set; }
        public int activeCount { get; set; }
    }
    public class BuyStats
    {
        public int lowestPrice { get; set; }
        public int highestPrice { get; set; }
        public int activeCount { get; set; }
    }
    public class ItemPriceHistory : IEnumerable<ItemPriceHistoryEntry>
    {
        public int AllTimeHigh { get; }
        public int AllTimeAverageHigh { get; }
        public ItemPriceHistory(IEnumerable<ItemPriceHistoryEntry> history)
        {
            _history = new List<ItemPriceHistoryEntry>(history);
            AllTimeHigh = _history.Count > 0 ? _history.Select(x => x.highestPrice).Max() : 0;
            AllTimeAverageHigh = _history.Count > 0 ? _history.Select(x => x.averagePrice).Max() : 0;
        }
        private readonly List<ItemPriceHistoryEntry> _history = new();
        public IReadOnlyList<ItemPriceHistoryEntry> History => _history;
        public ItemPriceHistoryEntry this[int index] => _history[index];
        public int Count => _history.Count;
        public IEnumerator<ItemPriceHistoryEntry> GetEnumerator() => _history.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
    public class ItemPriceHistoryEntry
    {
        public DateTime date { get; set; }
        public int lowestPrice { get; set; }
        public int averagePrice { get; set; }
        public int highestPrice { get; set; }
        public int itemsCount { get; set; }
    }
}

namespace r6_marketplace.Classes.Item.Error
{
    public class ApiError
    {
        public List<ErrorDetail> errors { get; set; }
    }

    public class ErrorDetail
    {
        public string message { get; set; }
    }
}
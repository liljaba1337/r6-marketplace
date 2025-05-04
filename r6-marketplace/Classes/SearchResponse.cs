#pragma warning disable CS8618

namespace r6_marketplace.Classes.SearchResponse.RawData
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
        public MarketableItems marketableItems { get; set; }
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

    internal class MarketableItems
    {
        public List<Node> nodes { get; set; }
        public int totalCount { get; set; }
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
        public object activeTrade { get; set; }
    }

    internal class Node
    {
        public string id { get; set; }
        public Item item { get; set; }
        public MarketData marketData { get; set; }
        public Viewer viewer { get; set; }
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

    internal class Viewer
    {
        public Meta meta { get; set; }
        public string __typename { get; set; }
    }
}

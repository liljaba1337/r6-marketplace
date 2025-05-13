using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
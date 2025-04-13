#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.using System;

using System.Collections;

namespace r6_marketplace.Classes.Item
{
    public class Item
    {
        public ItemMetadata Metadata { get; }
        public ItemPriceHistory PriceHistory { get; }
        public ItemMarketData MarketData { get; }
        Item(ItemMetadata itemMetadata, ItemPriceHistory itemPriceHistory, ItemMarketData itemMarketData)
        {
            Metadata = itemMetadata;
            PriceHistory = itemPriceHistory;
            MarketData = itemMarketData;
        }
    }
    public class ItemMetadata
    {
        public string id { get; set; }
        public string assetUrl { get; set; }
        public string itemId { get; set; }
        public string name { get; set; }
        public string[] tags { get; set; }
    }
    public class ItemMarketData
    {
        public string id { get; set; }
        public Sellstats[] sellStats { get; set; }
        public object buyStats { get; set; } // I haven't seen this one in the API yet, so I don't know what it looks like
        public Lastsoldat[] lastSoldAt { get; set; }

        public class Sellstats
        {
            public string id { get; set; }
            public string paymentItemId { get; set; }
            public int lowestPrice { get; set; }
            public int highestPrice { get; set; }
            public int activeCount { get; set; }
        }

        public class Lastsoldat
        {
            public string id { get; set; }
            public string paymentItemId { get; set; }
            public int price { get; set; }
            public DateTime performedAt { get; set; }
        }

    }
    public class ItemPriceHistory : IEnumerable<ItemPriceHistoryEntry>
    {
        /// <summary>
        /// The highest price of the item in the graph.
        /// </summary>
        public int AllTimeHigh { get; }
        /// <summary>
        /// The highest average price of the item in the graph.
        /// </summary>
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
        public DateTime Date { get; set; }
        public int lowestPrice { get; set; }
        public int averagePrice { get; set; }
        public int highestPrice { get; set; }
        public int itemsCount { get; set; }
    }
}

namespace r6_marketplace.Classes.Item.PriceHistoryRaw
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

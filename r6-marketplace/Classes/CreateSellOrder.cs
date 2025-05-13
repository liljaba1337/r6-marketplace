using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Classes.CreateSellOrder
{
    public class Buy
    {
        public string id { get; set; }
        public int resolvedTransactionCount { get; set; }
        public int resolvedTransactionPeriodInMinutes { get; set; }
        public int activeTransactionCount { get; set; }
        public string __typename { get; set; }
    }

    public class CreateSellOrder
    {
        public Trade trade { get; set; }
        public string __typename { get; set; }
    }

    public class Data
    {
        public CreateSellOrder createSellOrder { get; set; }
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

    public class Meta
    {
        public string id { get; set; }
        public bool isOwned { get; set; }
        public int quantity { get; set; }
        public string __typename { get; set; }
        public TradesLimitations tradesLimitations { get; set; }
    }

    public class PaymentOption
    {
        public string id { get; set; }
        public Item item { get; set; }
        public int price { get; set; }
        public int transactionFee { get; set; }
        public string __typename { get; set; }
    }

    public class ResaleLock
    {
        public string id { get; set; }
        public string itemId { get; set; }
        public DateTime expiresAt { get; set; }
        public string __typename { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }

    public class Sell
    {
        public string id { get; set; }
        public int resolvedTransactionCount { get; set; }
        public int resolvedTransactionPeriodInMinutes { get; set; }
        public int activeTransactionCount { get; set; }
        public List<ResaleLock> resaleLocks { get; set; }
        public string __typename { get; set; }
    }

    public class Trade
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
        public object paymentProposal { get; set; }
        public Viewer viewer { get; set; }
        public string __typename { get; set; }
    }

    public class TradeItem
    {
        public string id { get; set; }
        public Item item { get; set; }
        public string __typename { get; set; }
    }

    public class TradesLimitations
    {
        public string id { get; set; }
        public Buy buy { get; set; }
        public Sell sell { get; set; }
        public string __typename { get; set; }
    }

    public class Viewer
    {
        public Meta meta { get; set; }
        public string __typename { get; set; }
    }


}

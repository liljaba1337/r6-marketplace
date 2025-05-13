using r6_marketplace.RequestBodies.Shared;

namespace r6_marketplace.RequestBodies.AccountOrders.Active
{
    internal class Root : RequestRoot<Variables>
    {
        public Root(int limit, int offset) : base(new Variables(limit, offset)) { }
        public override string operationName => "GetTransactionsPending";
        public override string query => RequestQueries.GetActiveOrdersData;
    }
    internal class Variables : BaseVariables
    {
        public Variables(int limit, int offset)
        {
            this.limit = limit;
            this.offset = offset;
        }
        public int limit { get; }
        public int offset { get; }
    }
}

namespace r6_marketplace.RequestBodies.AccountOrders.Sell
{
    internal class Root : RequestRoot<Variables>
    {
        public Root(string itemid, int price) : base(new Variables(itemid, price)) { }
        public override string operationName => "CreateSellOrderMutation";
        public override string query => RequestQueries.CreateSellOrderData;
    }
    internal class Variables : BaseVariables
    {
        public Variables(string itemid, int price)
        {
            tradeItems.Add(new tradeItem(itemid));
            paymentOptions.Add(new paymentOption(price));
        }
        public List<tradeItem> tradeItems { get; } = new List<tradeItem>();
        public List<paymentOption> paymentOptions { get; } = new List<paymentOption>();
    }
    internal class tradeItem
    {
        public tradeItem(string itemId)
        {
            this.itemId = itemId;
        }
        public string itemId { get; set; }
        public int quantity { get; } = 1;
    }
    internal class paymentOption
    {
        public paymentOption(int price)
        {
            this.price = price;
        }
        public int price { get; set; }
        public string paymentItemId { get; } = "9ef71262-515b-46e8-b9a8-b6b6ad456c67";
    }
}
namespace r6_marketplace.RequestBodies.AccountOrders.Cancel
{
    internal class Root : RequestRoot<Variables>
    {
        public Root(string id) : base(new Variables(id)) { }
        public override string operationName => "CancelOrderMutation";
        public override string query => RequestQueries.CancelOrderData;
    }
    internal class Variables : BaseVariables
    {
        public Variables(string id)
        {
            this.tradeId = id;
        }
        public string tradeId { get; }
    }
}
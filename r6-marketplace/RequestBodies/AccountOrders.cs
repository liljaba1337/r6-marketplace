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

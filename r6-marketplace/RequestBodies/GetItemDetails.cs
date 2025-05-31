using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.RequestBodies.Shared;

namespace r6_marketplace.RequestBodies.GetItemDetails
{
    internal class Root : RequestRoot<Variables>
    {
        public Root(string itemId) : base(new Variables(itemId)) { }

        public override string operationName => "GetItemDetails";
        public override string query => RequestQueries.Get("GetItem");
    }

    internal class Variables : BaseVariables
    {
        public Variables(string itemId)
        {
            this.itemId = itemId;
        }

        public string itemId { get; }
        public string tradeId { get; } = "";
        public bool fetchTrade { get; } = false;
    }
}
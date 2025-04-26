using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Utils.RequestBody.Shared;

namespace r6_marketplace.Utils.RequestBody.GetItemPriceHistory
{
    internal class Root : RequestRoot<Variables>
    {
        public Root(string itemId) : base(new Variables(itemId)) { }

        public override string operationName => "GetItemPriceHistory";
        public override string query => RequestQueries.GetItemPriceHistoryData;
    }

    internal class Variables : BaseVariables
    {
        public Variables(string itemId)
        {
            this.itemId = itemId;
        }

        public string itemId { get; }
        public string paymentItemId { get; } = "9ef71262-515b-46e8-b9a8-b6b6ad456c67";
    }
}
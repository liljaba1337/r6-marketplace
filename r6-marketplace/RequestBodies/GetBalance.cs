using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.RequestBodies.Shared;

namespace r6_marketplace.RequestBodies.GetBalance
{
    internal class Root : RequestRoot<Variables>
    {
        public Root() : base(new Variables()) { }
        public override string operationName => "GetBalance";
        public override string query => RequestQueries.GetBalanceData;
    }
    internal class Variables : BaseVariables
    {
        public Variables() { }

        public string itemId { get; } = "9ef71262-515b-46e8-b9a8-b6b6ad456c67";
    }
}

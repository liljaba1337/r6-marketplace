using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.RequestBodies.Shared;

namespace r6_marketplace.RequestBodies.GetSearchTags
{
    internal class Root : RequestRoot<Variables>
    {
        public Root() : base(new Variables()) { }
        public override string operationName => "GetMarketplaceGameConfig";
        public override string query => RequestQueries.GetSearchTagsData;
    }
    internal class Variables : BaseVariables
    {
        public Variables(){}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Utils;

namespace r6_marketplace.Endpoints
{
    public class TransactionsEndpoints : EndpointsBase
    {
        internal TransactionsEndpoints(Web web) : base(web) { }
        public async Task<object> GetActiveOrders(Data.Local local = Data.Local.en)
        {
            web.EnsureAuthenticated();

            return null;
        }
    }
}

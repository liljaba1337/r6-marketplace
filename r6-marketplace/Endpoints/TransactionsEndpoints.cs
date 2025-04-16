using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Utils;

namespace r6_marketplace.Endpoints
{
    public class TransactionsEndpoints
    {
        private Web web;
        internal TransactionsEndpoints(Web web)
        {
            this.web = web;
        }
        public async Task<object> OrdersHistory(int limit = 40, int offset = 0, Data.Local local = Data.Local.en)
        {
            web.EnsureAuthenticated();

            return null;
        }
    }
}

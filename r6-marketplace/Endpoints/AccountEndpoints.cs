using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;

namespace r6_marketplace.Endpoints
{
    public class AccountEndpoints : EndpointsBase
    {
        internal AccountEndpoints(Web web) : base(web) { }
        /// <summary>
        /// Get the current balance of the account.
        /// </summary>
        /// <returns>Your balance, or -1 if an error occured.</returns>
        public async Task<int> GetBalance()
        {
            web.EnsureAuthenticated();
            var response = await web.Post(Data.dataUri, new RequestBodies.GetBalance.Root().AsJson());
            var rawBalance = await response.DeserializeAsyncSafe<List<Classes.BalanceResponse.Root>>();
            return rawBalance?[0]?.data?.game?.viewer?.meta?.quantity ?? -1;
        }
    }
}

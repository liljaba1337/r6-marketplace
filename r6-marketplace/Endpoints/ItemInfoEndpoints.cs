using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;
using r6_marketplace.Utils;

namespace r6_marketplace.Endpoints
{
    public class ItemInfoEndpoints
    {
        private Web web;
        internal ItemInfoEndpoints(Web web)
        {
            this.web = web;
        }
        public async Task<Item?> GetItemAsync(string itemId, Data.Local lang = Data.Local.en)
        {
            var response = await web.PostAsync(Data.dataUri, RequestBodies.GetItemDataWithPriceHistory, local:lang);
            Console.WriteLine(response.StatusCode);
            return null;
        }
    }
}

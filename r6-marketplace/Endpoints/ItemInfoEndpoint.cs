using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;

namespace r6_marketplace.Endpoints
{
    internal class ItemInfoEndpoint
    {
        public static async Task<Item> GetItemAsync(string itemId)
        {
            var url = $"https://public-ubiservices.ubi.com/v1/profiles/me/uplay/graphql";
            var response = await Utils.Web.Post(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var item = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(json);
                return item;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
    }
}

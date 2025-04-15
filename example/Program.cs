using r6_marketplace;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // If you already have your token
            r6_marketplace.Client client = new r6_marketplace.Client(token:"token");
            
            // If not
            client = new r6_marketplace.Client();
            await client.AuthenticateAsync("email", "password");

            // Get an item by its ID
            var item = await client.ItemInfoEndpoints.GetItemAsync("dcde67d3-2bdd-4598-61ac-a0c7d849f2b6",
                r6_marketplace.Utils.Data.Local.en);

            if (item == null)
                Console.WriteLine("Not found");
            else
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.SellOrdersStats?.lowestPrice);
                Console.WriteLine(item.SellOrdersStats?.highestPrice);
                Console.WriteLine(item.SellOrdersStats?.activeCount);

                // Picture of it
                Console.WriteLine(item.assetUrl);
            }
        }
    }
}
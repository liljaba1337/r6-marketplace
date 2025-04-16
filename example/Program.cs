using r6_marketplace;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // If you already have your token
            r6_marketplace.R6MarketplaceClient client = new r6_marketplace.R6MarketplaceClient(token:"token");
            
            // If not
            client = new r6_marketplace.R6MarketplaceClient();
            await client.Authenticate("email", "password");

            // Get an item by its ID
            var item = await client.ItemInfoEndpoints.GetItem("dcde67d3-2bdd-4598-61ac-a0c7d849f2b6",
                r6_marketplace.Utils.Data.Local.en);

            // Get an item's price history by its ID
            var pricehistory = await client.ItemInfoEndpoints.GetItemPriceHistory("dcde67d3-2bdd-4598-61ac-a0c7d849f2b6");

            if (item == null || pricehistory == null)
                Console.WriteLine("Not found");
            else
            {
                // Main data
                Console.WriteLine(item.Name);
                Console.WriteLine(item.SellOrdersStats?.lowestPrice);
                Console.WriteLine(item.SellOrdersStats?.highestPrice);
                Console.WriteLine(item.SellOrdersStats?.activeCount);

                // Picture of it
                Console.WriteLine(item.assetUrl);

                // Price history
                Console.WriteLine($"30 days high: {pricehistory.AllTimeHigh}");
                Console.WriteLine($"30 days highest daily average: {pricehistory.AllTimeAverageHigh}");
                foreach (var itemPrice in pricehistory)
                {
                    Console.WriteLine($"Average price: {itemPrice.averagePrice} - Date: {itemPrice.date}");
                }
            }
        }
    }
}
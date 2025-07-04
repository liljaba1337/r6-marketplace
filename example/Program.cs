﻿using r6_marketplace;
using r6_marketplace.Classes.Orders;
using r6_marketplace.Endpoints;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a client instance and authenticate
            r6_marketplace.R6MarketplaceClient client = new r6_marketplace.R6MarketplaceClient();
            await client.Authenticate("email", "password");

            // Get an item by its ID
            var item = await client.ItemInfoEndpoints.GetItem("dcde67d3-2bdd-4598-61ac-a0c7d849f2b6",
                r6_marketplace.Utils.Data.Local.en);

            // Get an item's price history by its ID
            var pricehistory = await client.ItemInfoEndpoints.GetItemPriceHistory("dcde67d3-2bdd-4598-61ac-a0c7d849f2b6");

            // Print item details. This is applicable for almost any return type.
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
                Console.WriteLine(item.AssetUrl);

                // Price history
                Console.WriteLine($"30 days high: {pricehistory.AllTimeHigh}");
                Console.WriteLine($"30 days highest daily average: {pricehistory.AllTimeAverageHigh}");
                foreach (var itemPrice in pricehistory)
                {
                    Console.WriteLine($"Average price: {itemPrice.AveragePrice} - Date: {itemPrice.Date}");
                }
            }

            // Get search tags (temporary)
            var tags = await client.SearchEndpoints.GetSearchTags();

            // Get your balance
            int balance = await client.AccountEndpoints.GetBalance();

            // Get your inventory
            var inventory = await client.AccountEndpoints.GetInventory(
                limit: 500
            );

            // Get your open orders
            IReadOnlyList<Order> orders = await client.TransactionsEndpoints.GetActiveOrders();
        }
    }
}
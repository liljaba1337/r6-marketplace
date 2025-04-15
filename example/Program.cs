using r6_marketplace;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // not finished yet, so I'm using my own account for testing
            string[] creds = File.ReadAllLines("creds.txt");
            r6_marketplace.Client client = new r6_marketplace.Client(token:creds[2]);
            //await client.AuthenticateAsync(creds[0], creds[1]);
            var item = await client.ItemInfoEndpoints.GetItemAsync("dcde67d3-2bdd-4598-61ac-a0c7d849f2b6",
                r6_marketplace.Utils.Data.Local.en);
            if (item == null) Console.WriteLine("Not found");
            else
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.SellOrdersStats?.lowestPrice);
            }
        }
    }
}
using r6_marketplace;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // not finished yet, so I'm using my own account for testing
            string[] creds = File.ReadAllLines("creds.txt");
            r6_marketplace.Client client = new r6_marketplace.Client();
            await client.AuthenticateAsync(creds[0], creds[1]);
            await client.ItemInfoEndpoints.GetItemAsync("itemId", r6_marketplace.Utils.Data.Local.en);
        }
    }
}
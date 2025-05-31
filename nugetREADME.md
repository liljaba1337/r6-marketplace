# [R6 Marketplace API Wrapper](https://github.com/liljaba1337/r6-marketplace)

A .NET wrapper for the Rainbow Six Siege Marketplace API.  

---

## Installation

### With NuGet:
```
dotnet add package r6-marketplace
```

### Or Visual Studio NuGet Package Manager:

```bash
Install-Package r6-marketplace
```

---

## Getting Started

Initialize the client with your authentication credentials and start interacting with the API. For detailed usage instructions, see the [Documentation](https://github.com/liljaba1337/r6-marketplace/wiki).

### Example

<!-- START_SECTION:EXAMPLE -->

```c#
using r6_marketplace;
using r6_marketplace.Endpoints;

namespace Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            r6_marketplace.R6MarketplaceClient client = new r6_marketplace.R6MarketplaceClient();
            await client.Authenticate("email", "password");

            int balance = await client.AccountEndpoints.GetBalance();

            var inventory = await client.AccountEndpoints.GetInventory(
                limit: 500
            );

            var totalValue = inventory.GetInventoryValue();
            Console.WriteLine($"Total value: {totalValue.TotalValue}");
            Console.WriteLine($"Total value without fees: {totalValue.TotalValueWithoutFee}");
        }
    }
}
```

<!-- END_SECTION:EXAMPLE -->

---

## Contributing

**Contributions are welcome!**
If you find bugs or want to suggest improvements, feel free to [open an issue](https://github.com/liljaba1337/r6-marketplace/issues) or [create a pull request](https://github.com/liljaba1337/r6-marketplace/pulls).

---

## License & Disclaimer

This project is licensed under the [Apache 2.0 License](https://github.com/liljaba1337/r6-marketplace/blob/master/LICENSE.txt).

"Ubisoft" and related marks are trademarks or registered trademarks of Ubisoft Entertainment. This project is not affiliated with, endorsed, or sponsored by Ubisoft Entertainment.
# R6 Marketplace API Wrapper

A .NET wrapper for the Rainbow Six Siege Marketplace API.  

![Dotnet Framework](https://img.shields.io/badge/Framework-.NET%208.0-purple.svg)
![GitHub License](https://img.shields.io/github/license/liljaba1337/r6-marketplace)
![NuGet Version](https://img.shields.io/nuget/v/r6-marketplace)
![NuGet Downloads](https://img.shields.io/nuget/dt/r6-marketplace)

---

## Features

Pretty much all the requests have already been implemented.
<details>
<summary>Click here to view the list of all the completed features</summary>

- [x] Authentication flow
- [x] Retrieve item data by ID
- [x] Retrieve item sale history
- [x] Search items by name or filters
- [x] Retrieve account details (balance/inventory)
- [x] Retrieve orders (open/history)
- [x] Manage sale orders
- [x] Manage buy orders
- [x] Updates events handling
- [x] Token refresher

</details>

### Planned / Completed improvements (what I'm focusing on right now)
- [ ] Order / Item refactoring
- [x] Better filtering logic
- [ ] Better filenaming
- [ ] Advanced error handling
- [ ] Optimized requests

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

## Usage

### [Documentation](https://github.com/liljaba1337/r6-marketplace/wiki)

### [Example](https://github.com/liljaba1337/r6-marketplace/tree/master/example) (I update it rarely so it may be outdated)

---

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
If you find bugs or want to suggest improvements, feel free to [open an issue](https://github.com/liljaba1337/r6-marketplace/issues) or [create a pull request](https://github.com/liljaba1337/r6-marketplace/pulls)! I'm completely open to all contributions, so don’t hesitate to reach out with anything you find!

---

## License & Disclaimer

This project is licensed under the [Apache 2.0 License](https://github.com/liljaba1337/r6-marketplace/blob/master/LICENSE.txt).

"Ubisoft" and related marks are trademarks or registered trademarks of Ubisoft Entertainment. This project is not affiliated with, endorsed, or sponsored by Ubisoft Entertainment.

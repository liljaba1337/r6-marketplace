# R6 Marketplace API Wrapper

A .NET wrapper for the Rainbow Six Siege Marketplace API.  

> **Note:** This project is under active development and is not yet feature-complete.

---

## Features (Planned / In Progress)

- [x] Authentication flow
- [x] Retrieve item data by ID
- [x] Retrieve item sale history
- [x] Search items by name or filters
- [x] Retrieve account details (balance/inventory)
- [ ] Retrieve orders (open/history)
- [ ] Manage sale orders
- [ ] Manage buy orders
- [ ] Updates events handling

---

## Installation

NuGet package is coming soon.  
For now, you can clone the repo and reference it directly in your project.

```bash
git clone https://github.com/liljaba1337/r6-marketplace
```
Then add the `.csproj` to your solution or reference the compiled `.dll` manually.

---

## Usage (Example)

Documentation is not yet ready, so take a look at [the example code](https://github.com/liljaba1337/r6-marketplace/blob/master/example/Program.cs), containing all the finished features.

---

## Q&A

**Q: Do I need to provide my account credentials?**

**A: Yes. Ubisoft requires authorization for all requests.**
However, advanced users can use their existing token instead of providing credentials. If you don’t need selling or buying functions, I recommend creating an alternate account.


**Q: Do I need to have marketplace access on my account?**

**A: Not for all methods.**
Only the selling and buying (and maybe inventory) methods require marketplace access.

---

## Contributing

**Contributions are welcome!**
If you find bugs or want to suggest improvements, feel free to [open an issue](https://github.com/liljaba1337/r6-marketplace/issues) or [create a pull request](https://github.com/liljaba1337/r6-marketplace/pulls)! I'm completely open to all contributions, so don’t hesitate to reach out with anything you find!

---

## License & Disclaimer

This project is licensed under the [Apache 2.0 License](https://github.com/liljaba1337/r6-marketplace/blob/master/LICENSE.txt).

"Ubisoft" and related marks are trademarks or registered trademarks of Ubisoft Entertainment. This project is not affiliated with, endorsed, or sponsored by Ubisoft Entertainment.

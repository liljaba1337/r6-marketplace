using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Classes.Tags.RawData
{
    internal class Assets
    {
        public string backgroundImage { get; set; }
        public HomeAssets homeAssets { get; set; }
        public string __typename { get; set; }
    }

    internal class Buy
    {
        public List<object> filterSelection { get; set; }
        public object sortSelection { get; set; }
        public string __typename { get; set; }
    }

    internal class Contact
    {
        public string bugUrl { get; set; }
        public string bugLabel { get; set; }
        public string __typename { get; set; }
    }

    internal class Currency
    {
        public string currencyId { get; set; }
        public string currencyUrl { get; set; }
        public string __typename { get; set; }
    }

    internal class Data
    {
        public Game game { get; set; }
    }

    internal class Filter
    {
        public string categoryId { get; set; }
        public string type { get; set; }
        public string displayName { get; set; }
        public string sortType { get; set; }
        public string sortDirection { get; set; }
        public List<Value> values { get; set; }
        public string __typename { get; set; }
    }

    internal class Game
    {
        public string id { get; set; }
        public Marketplace marketplace { get; set; }
        public string __typename { get; set; }
    }

    internal class HomeAssets
    {
        public string buyTab { get; set; }
        public string sellTab { get; set; }
        public string transactionsTab { get; set; }
        public string __typename { get; set; }
    }

    internal class HomeType
    {
        public string value { get; set; }
        public string displayName { get; set; }
        public string iconUrl { get; set; }
        public string __typename { get; set; }
    }

    internal class Marketplace
    {
        public Assets assets { get; set; }
        public Privilege privilege { get; set; }
        public Currency currency { get; set; }
        public Contact contact { get; set; }
        public List<Tag> tags { get; set; }
        public List<TagGroup> tagGroups { get; set; }
        public List<Type> types { get; set; }
        public List<HomeType> homeTypes { get; set; }
        public List<Filter> filters { get; set; }
        public Recommendations recommendations { get; set; }
        public TabConfig tabConfig { get; set; }
        public object userResearch { get; set; }
        public string __typename { get; set; }
    }

    internal class Privilege
    {
        public string tradePrivilegeId { get; set; }
        public string __typename { get; set; }
    }

    internal class Recommendations
    {
        public UserItemRecommendations userItemRecommendations { get; set; }
        public string __typename { get; set; }
    }

    internal class Root
    {
        public Data data { get; set; }
    }

    internal class Sell
    {
        public List<object> filterSelection { get; set; }
        public object sortSelection { get; set; }
        public string __typename { get; set; }
    }

    internal class TabConfig
    {
        public Buy buy { get; set; }
        public Sell sell { get; set; }
        public string __typename { get; set; }
    }

    internal class Tag
    {
        public string color { get; set; }
        public string value { get; set; }
        public string iconUrl { get; set; }
        public string displayName { get; set; }
        public string __typename { get; set; }
    }

    internal class TagGroup
    {
        public string type { get; set; }
        public List<string> values { get; set; }
        public string displayName { get; set; }
        public string __typename { get; set; }
    }

    internal class Type
    {
        public string value { get; set; }
        public string iconUrl { get; set; }
        public string displayName { get; set; }
        public string __typename { get; set; }
    }

    internal class UserItemRecommendations
    {
        public string projectId { get; set; }
        public string categoryId { get; set; }
        public string __typename { get; set; }
    }

    internal class Value
    {
        public string value { get; set; }
        public string displayName { get; set; }
        public string __typename { get; set; }
    }
}
namespace r6_marketplace.Classes.Tags
{
    public class Tags
    {
        public IReadOnlyList<string>? Rarity { get; internal set; }
        public IReadOnlyList<string>? Season { get; internal set; }
        public IReadOnlyList<string>? Operator { get; internal set; }
        public IReadOnlyList<string>? Weapon { get; internal set; }
        public IReadOnlyList<string>? Event { get; internal set; }
        public IReadOnlyList<string>? Esports_Team { get; internal set; }
        public IReadOnlyList<string>? Other { get; internal set; }
        public IReadOnlyList<string>? Type { get; internal set; }
    }
}
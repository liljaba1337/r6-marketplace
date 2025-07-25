using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Endpoints;
using r6_marketplace.Extensions;
using r6_marketplace.Utils;

namespace r6_marketplace.Classes.Item
{
    /// <summary>
    /// Basic representation of an item on the marketplace.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Returns the name of the item.
        /// </summary>
        public override string ToString() => Name;
        /// <summary>
        /// Unique identifier of the item.
        /// </summary>
        public string ID { get; internal set; }
        /// <summary>
        /// Name of the item.
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Asset URL of a picture if this item.
        /// </summary>
        public ImageUri AssetUrl { get; internal set; }
        public bool IsOwned { get; internal set; }
        /// <summary>
        /// Weapon name, operator name, or other target of the item.
        /// </summary>
        /// <remarks>
        /// This is extracted from a list of item tags, so it may not always be accurate.
        /// </remarks>
        public string Target { get ; internal set; }
        /// <summary>
        /// Rarity of the item (if exists)
        /// </summary>
        /// <remarks>
        /// This is extracted from a list of item tags, so it may not always be accurate.
        /// </remarks>
        public SearchTags.Rarity Rarity { get; internal set; }
        public string Type { get; internal set; }
        /// <summary>
        /// Weapon name is usually at index 0, release year/season at index 3, and rarity at index 5.
        /// I'm not sure about the consistency of this yet, so I don't extract them myself.
        /// I will probably change it in the future.
        /// </summary>
        public List<string> Tags { get; internal set; }
        /// <summary>
        /// The price of the last sale. May be -1 if the item hasn't been sold in the past 30 days.
        /// </summary>
        public int LastSoldAtPrice { get; internal set; }
        /// <summary>
        /// The time of the last sale. May be null if the item hasn't been sold in the past 30 days.
        /// </summary>
        public DateTime? LastSoldAtTime { get; internal set; }
        /// <summary>
        /// May be null if there are no sell orders for this item.
        /// </summary>
        public OrdersStats? SellOrdersStats { get; internal set; }
        /// <summary>
        /// May be null if there are no buy orders for this item.
        /// </summary>
        public OrdersStats? BuyOrdersStats { get; internal set; }
        /// <summary>
        /// Get a link to the item on the Ubisoft marketplace.
        /// </summary>
        public string GetMarketplaceURL(Utils.Data.Local lang = Utils.Data.Local.en)
        {
            return $"https://www.ubisoft.com/{lang.Format()}/game/rainbow-six/siege/marketplace" +
                $"?route=buy%2Fitem-details&itemId={ID}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Item other && ID == other.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
    public class OrdersStats
    {
        public int lowestPrice { get; internal set; }
        public int highestPrice { get; internal set; }
        public int activeCount { get; internal set; }
    }
}
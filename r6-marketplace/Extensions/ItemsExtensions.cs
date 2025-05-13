using r6_marketplace.Classes.Item;

namespace r6_marketplace.Extensions
{
    public static class ItemsExtensions
    {
        private const double FeeMultiplier = 0.9;
        internal static InventoryValue _GetInventoryValue(this IReadOnlyList<SellableItem> items)
        {
            int total = 0, totalWithoutFee = 0;
            int lastSale = 0, lastSaleWithoutFee = 0;
            int autobuy = 0, autobuyWithoutFee = 0;

            foreach (var item in items)
            {
                int lowestPrice = item.SellOrdersStats != null ? item.SellOrdersStats.lowestPrice : 0;
                int lastSoldPrice = item.LastSoldAtPrice;
                int highestBuy = item.BuyOrdersStats != null ? item.BuyOrdersStats.highestPrice : 0;

                total += lowestPrice;
                totalWithoutFee += (int)(lowestPrice * FeeMultiplier);

                lastSale += lastSoldPrice;
                lastSaleWithoutFee += (int)(lastSoldPrice * FeeMultiplier);

                autobuy += highestBuy;
                autobuyWithoutFee += (int)(highestBuy * FeeMultiplier);
            }

            return new InventoryValue(total, totalWithoutFee, lastSale, lastSaleWithoutFee, autobuy, autobuyWithoutFee);
        }

        public readonly struct InventoryValue
        {
            /// <summary>
            /// The total value of the inventory based on the current lowest price.
            /// </summary>
            public int TotalValue { get; }
            /// <summary>
            /// The total value of the inventory based on the current lowest price minus 10% fees.
            /// </summary>
            public int TotalValueWithoutFee { get; }
            /// <summary>
            /// The total value of the inventory based on the last sale price.
            /// </summary>
            public int TotalValueLastSale { get; }
            /// <summary>
            /// The total value of the inventory based on the last sale price minus 10% fees.
            /// </summary>
            public int TotalValueLastSaleWithoutFee { get; }
            /// <summary>
            /// The total value of the inventory based on the current lowest buy order price.
            /// </summary>
            public int TotalValueAutobuy { get; }
            /// <summary>
            /// The total value of the inventory based on the current lowest buy order price minus 10% fees.
            /// </summary>
            public int TotalValueAutobuyWithoutFee { get; }

            internal InventoryValue(int totalValue, int totalValueWithoutFee, int totalValueLastSale, int totalValueLastSaleWithoutFee,
                int totalValueAutobuy, int totalValueAutobuyWithoutFee)
            {
                TotalValue = totalValue;
                TotalValueWithoutFee = totalValueWithoutFee;
                TotalValueLastSale = totalValueLastSale;
                TotalValueLastSaleWithoutFee = totalValueLastSaleWithoutFee;
                TotalValueAutobuy = totalValueAutobuy;
                TotalValueAutobuyWithoutFee = totalValueAutobuyWithoutFee;
            }
            /// <returns><see cref="TotalValue"/></returns>
            public override readonly string ToString() => TotalValue.ToString();
        }
    }
}

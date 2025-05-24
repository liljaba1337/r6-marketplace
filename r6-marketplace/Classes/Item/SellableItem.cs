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
    /// Represents an <see cref="Item"/> that can be sold.
    /// </summary>
    public class SellableItem : Item
    {
        private readonly TransactionsEndpoints transactionsEndpoints;
        internal SellableItem(TransactionsEndpoints transactionsEndpoints) { this.transactionsEndpoints = transactionsEndpoints; }

        /// <summary>
        /// Sell this item.
        /// </summary>
        /// <param name="price">The price to sell this item for. Must be between 10 and 1,000,000.</param>
        /// <returns>An instance of <see cref="Orders.Order"/> if the order was places successfully.</returns>
        public async Task<Orders.Order> Sell(int price)
            => await transactionsEndpoints.CreateSellOrder(ID, price);

        /// <summary>
        /// Instantly sell this item for the highest buy order price.
        /// </summary>
        /// <param name="offset">Amount to add to or subtract from the base price.</param>
        /// <returns>An instance of <see cref="Orders.Order"/> if the order was places successfully.</returns>
        public async Task<Orders.Order> InstantSell(int offset = 0)
        {
            if (BuyOrdersStats?.highestPrice == null)
                throw new InvalidOperationException("There are no buy orders for this item. Cannot instant sell.");
            return await transactionsEndpoints.CreateSellOrder(ID, BuyOrdersStats.highestPrice + offset);
        }

        /// <summary>
        /// Sell the item for 1 credit lower price than the current lowest sell order (undercut the market).
        /// </summary>
        /// <param name="offset">Amount to add to or subtract from the base price.</param>
        /// <returns>An instance of <see cref="Orders.Order"/> if the order was places successfully.</returns>
        public async Task<Orders.Order> BestPriceSell(int offset = 0)
        {
            if (SellOrdersStats?.lowestPrice == null)
                throw new InvalidOperationException("There are no sell orders for this item. Cannot best price sell.");
            return await transactionsEndpoints.CreateSellOrder(ID, SellOrdersStats.lowestPrice - 1 + offset);
        }
    }
}
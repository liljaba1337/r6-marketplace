using r6_marketplace.Endpoints;

namespace r6_marketplace.Classes.Item
{
    /// <summary>
    /// Represents an <see cref="Item"/> that can be purchased.
    /// </summary>
    public class PurchasableItem : Item
    {
        private readonly TransactionsEndpoints transactionsEndpoints;
        internal PurchasableItem(TransactionsEndpoints transactionsEndpoints) { this.transactionsEndpoints = transactionsEndpoints; }

        /// <summary>
        /// Create a buy order for this item.
        /// </summary>
        /// <param name="price">The price to buy this item for. Must be between 10 and 1,000,000.</param>
        /// <returns>An instance of <see cref="Orders.Order"/> if the order was places successfully.</returns>
        public async Task<Orders.Order> Buy(int price)
            => await transactionsEndpoints.CreateBuyOrder(ID, price);

        /// <summary>
        /// Instantly buy this item for the lowest sell order price.
        /// </summary>
        /// <returns>An instance of <see cref="Orders.Order"/> if the order was places successfully.</returns>
        public async Task<Orders.Order> InstantBuy()
            => await transactionsEndpoints.CreateSellOrder(ID, SellOrdersStats.lowestPrice);
    }
}

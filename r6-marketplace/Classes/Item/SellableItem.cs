using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Classes.Item
{
    /// <summary>
    /// Represents an <see cref="Item"/> that can be sold.
    /// </summary>
    public class SellableItem : Item
    {
        internal SellableItem() { }
        public Task<object> CreateSellOrder(int price)
        {
            return null;
        }
    }
}
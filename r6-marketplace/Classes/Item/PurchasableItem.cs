using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Classes.Item
{
    /// <summary>
    /// Represents an <see cref="Item"/> that can be purchased.
    /// </summary>
    public class PurchasableItem : Item
    {
        internal PurchasableItem() { }
        public Task<object> CreateBuyOrder(int price)
        {
            return null;
        }
    }
}

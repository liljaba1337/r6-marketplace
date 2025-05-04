using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static r6_marketplace.Endpoints.SearchEndpoints;

namespace r6_marketplace.Extensions
{
    internal static class FiltersExtensions
    {
        internal static string Format(this SortBy sortBy)
        {
            return sortBy switch
            {
                SortBy.PurchaseAvailaible => "ACTIVE_COUNT",
                SortBy.SaleAvailaible => "ACTIVE_COUNT",
                SortBy.LastSalePrice => "LAST_TRANSACTION_PRICE",
                SortBy.ItemName => "DISPLAY_NAME",
                _ => throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, null)
            };
        }
        internal static string? GetOrderType(this SortBy sortBy)
        {
            return sortBy switch
            {
                SortBy.PurchaseAvailaible => "Buy",
                SortBy.SaleAvailaible => "Sell",
                _ => null
            };
        }
    }
}

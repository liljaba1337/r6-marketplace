using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Classes.Item
{
    /// <summary>
    /// Represents the price history of an item over the past 30 days.
    /// </summary>
    public class ItemPriceHistory : IEnumerable<ItemPriceHistoryEntry>
    {
        /// <summary>
        /// The highest price at which the item was sold in the past 30 days.
        /// </summary>
        public int AllTimeHigh { get; }
        /// <summary>
        /// The highest daily average price of the item in the past 30 days.
        /// </summary>
        public int AllTimeAverageHigh { get; }
        /// <summary>
        /// The lowest daily average price of the item in the past 30 days.
        /// </summary>
        public int AllTimeAverageLow { get; }
        /// <summary>
        /// The average price of the item in the past 30 days.
        /// </summary>
        public double Average { get; }
        internal ItemPriceHistory(IEnumerable<ItemPriceHistoryEntry> history)
        {
            _history = new List<ItemPriceHistoryEntry>(history);
            AllTimeHigh = _history.Count > 0 ? _history.Select(x => x.HighestPrice).Max() : 0;
            AllTimeAverageHigh = _history.Count > 0 ? _history.Select(x => x.AveragePrice).Max() : 0;
            AllTimeAverageLow = _history.Count > 0 ? _history.Select(x => x.AveragePrice).Min() : 0;
            Average = _history.Count > 0 ? _history.Select(x => x.AveragePrice).Average() : 0;
        }
        private readonly List<ItemPriceHistoryEntry> _history = new();
        public IReadOnlyList<ItemPriceHistoryEntry> History => _history;
        public ItemPriceHistoryEntry this[int index] => _history[index];
        public int Count => _history.Count;
        public IEnumerator<ItemPriceHistoryEntry> GetEnumerator() => _history.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
    /// <summary>
    /// Represents a single entry (day) in the item price history.
    /// </summary>
    public readonly struct ItemPriceHistoryEntry
    {
        public DateTime Date { get; init; }
        public int LowestPrice { get; init; }
        public int AveragePrice { get; init; }
        public int HighestPrice { get; init; }
        public int ItemsCount { get; init; }
    }
}

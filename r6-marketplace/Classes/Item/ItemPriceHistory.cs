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

        #region LINQ Extensions
        /// <summary>
        /// Returns a new <see cref="ItemPriceHistory"/> containing the first specified number of elements from the
        /// current history.
        /// </summary>
        /// <param name="count">The number of elements to include in the new <see cref="ItemPriceHistory"/>. Must be non-negative.</param>
        /// <returns>A new <see cref="ItemPriceHistory"/> containing up to <paramref name="count"/> elements from the start of
        /// the current history. If <paramref name="count"/> exceeds the number of elements in the current history, all
        /// elements are included.</returns>
        public ItemPriceHistory Take(int count) =>
            new ItemPriceHistory(_history.Take(count));

        /// <summary>
        /// Skips the specified number of entries in the price history and returns a new <see cref="ItemPriceHistory"/>
        /// instance.
        /// </summary>
        /// <param name="count">The number of entries to skip. Must be non-negative.</param>
        /// <returns>A new <see cref="ItemPriceHistory"/> instance containing the remaining entries after skipping the specified
        /// number. If <paramref name="count"/> is greater than the total number of entries, an empty <see
        /// cref="ItemPriceHistory"/> is returned.</returns>
        public ItemPriceHistory Skip(int count) =>
            new ItemPriceHistory(_history.Skip(count));

        /// <summary>
        /// Filters the price history entries based on the specified predicate.
        /// </summary>
        /// <param name="predicate">A function to test each entry for a condition. The function should return <see langword="true"/> for entries
        /// that should be included in the filtered result, and <see langword="false"/> otherwise.</param>
        /// <returns>A new <see cref="ItemPriceHistory"/> instance containing the entries that satisfy the specified predicate.</returns>
        public ItemPriceHistory Where(Func<ItemPriceHistoryEntry, bool> predicate) =>
            new ItemPriceHistory(_history.Where(predicate));

        /// <summary>
        /// Returns a new <see cref="ItemPriceHistory"/> instance with its entries ordered according to the specified
        /// key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the <paramref name="keySelector"/> function.</typeparam>
        /// <param name="keySelector">A function to extract a key from each <see cref="ItemPriceHistoryEntry"/> for ordering.</param>
        /// <returns>A new <see cref="ItemPriceHistory"/> instance containing the entries sorted by the specified key.</returns>
        public ItemPriceHistory OrderBy<TKey>(Func<ItemPriceHistoryEntry, TKey> keySelector) =>
            new ItemPriceHistory(_history.OrderBy(keySelector));

        /// <summary>
        /// Reverses the order of the price history entries.
        /// </summary>
        /// <returns>A new <see cref="ItemPriceHistory"/> instance with the entries in reverse order.</returns>
        public ItemPriceHistory Reverse() =>
            new ItemPriceHistory(_history.AsEnumerable().Reverse());
        #endregion

    }
    /// <summary>
    /// Represents a single entry (day) in the item price history.
    /// </summary>
    public readonly struct ItemPriceHistoryEntry
    {
        /// <summary>
        /// The date of this day.
        /// </summary>
        public DateTime Date { get; init; }
        /// <summary>
        /// The lowest price at which the item was sold on this day.
        /// </summary>
        public int LowestPrice { get; init; }
        /// <summary>
        /// The average price at which the item was sold on this day.
        /// </summary>
        public int AveragePrice { get; init; }
        /// <summary>
        /// The highest price at which the item was sold on this day.
        /// </summary>
        public int HighestPrice { get; init; }
        /// <summary>
        /// The number of items sold on this day.
        /// </summary>
        public int ItemsCount { get; init; }
    }
}

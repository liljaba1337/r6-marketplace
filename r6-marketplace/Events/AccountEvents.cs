using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Classes.Item;
using r6_marketplace.Endpoints;

namespace r6_marketplace.Events
{
    public class AccountEvents
    {
        private readonly object inventoryLock = new();
        private List<SellableItem>? inventory;
        private Timer? timer;
        private bool enabled = false;
        /// <summary>
        /// Triggered when a new item is added to the inventory.
        /// </summary>
        /// <remarks>
        /// The way this item was obtained doesn't matter. Whether that was a packet, a reward or a marketplace purchase.
        /// <para></para>
        /// <c>Important:</c> items that are not availaible on the marketplace don't trigger this event.
        /// </remarks>
        /// <returns>
        /// <see cref="SellableItem"/> that appeared in the inventory.
        /// <para></para>
        /// <c>Note:</c> selling methods will throw an exception if the item was aquired through the marketplace
        /// because of the 14-day ban.
        /// </returns>
        public event Action<SellableItem>? OnInventoryItemAdded;
        /// <summary>
        /// Triggered when an item is removed from the inventory.
        /// </summary>
        /// <remarks>
        /// I don't really know any way for an item to be removed from your inventory other than being sold on the marketplace.
        /// </remarks>
        /// <returns>
        /// <see cref="Item"/> that was removed from the inventory.
        /// </returns>
        public event Action<Item>? OnInventoryItemRemoved;
        private AccountEndpoints accountEndpoints;
        internal AccountEvents(AccountEndpoints accountEndpoints) : base()
        {
            this.accountEndpoints = accountEndpoints;
        }
        /// <summary>
        /// Sets up or updates the polling for the inventory change events.
        /// </summary>
        /// <param name="enabled">Enable (<c>true</c>) or disable (<c>false</c>) the polling.</param>
        /// <param name="frequency">Polling interval in seconds.
        /// Low values (&lt; 3) may cause overlapping calls under certain conditions, so be careful.</param>
        public void SetupInventoryPolling(bool enabled = true, int frequency = 60)
        {
            if (frequency < 1) throw new ArgumentOutOfRangeException(nameof(frequency), "Frequency must be greater than 0.");
            this.enabled = enabled;
            timer = new(async _ => await PollInventory(), null, TimeSpan.Zero, TimeSpan.FromSeconds(frequency));
        }
        private async Task PollInventory()
        {
            if (enabled)
            {
                IReadOnlyList<SellableItem> newinventory = await accountEndpoints.GetInventory(
                    limit:500,
                    sortBy:SearchEndpoints.SortBy.LastSalePrice,
                    sortDirection:SearchEndpoints.SortDirection.DESC);

                List<SellableItem>? added = null;
                List<SellableItem>? removed = null;

                // I don't think someone would have that many items and that slow pc, but just in case
                lock (inventoryLock)
                {
                    if (inventory == null)
                    {
                        inventory = new(newinventory);
                        return;
                    }

                    if (OnInventoryItemAdded != null) added = newinventory.Except(inventory).ToList();
                    if (OnInventoryItemRemoved != null) removed = inventory.Except(newinventory).ToList();

                    inventory = new(newinventory);
                }

                if (added != null)
                {
                    foreach (var item in added)
                    {
                        OnInventoryItemAdded?.Invoke(item);
                    }
                }
                if (removed != null)
                {
                    foreach (var item in removed)
                    {
                        OnInventoryItemRemoved?.Invoke(item);
                    }
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace CircuitBreakerSpike.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        public InventoryRepository()
        {
            ThrowExceptions = false;
        }

        public IEnumerable<InventoryItem> FindInventoryItems()
        {
            if (ThrowExceptions)
            {
                throw new ApplicationException("Inventory system is not responding.");
            }
            return new List<InventoryItem>
            {
                new InventoryItem(),
                new InventoryItem(),
                new InventoryItem(),
                new InventoryItem(),
                new InventoryItem(),
                new InventoryItem(),
                new InventoryItem(),
                new InventoryItem(),
                new InventoryItem()
            };
        }

        public bool ThrowExceptions { get; set; }
    }
}
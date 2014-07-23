using System.Collections.Generic;

namespace CircuitBreakerSpike.Repositories
{
    public interface IInventoryRepository
    {
        IEnumerable<InventoryItem> FindInventoryItems();
        bool ThrowExceptions { get; set; }
    }
}
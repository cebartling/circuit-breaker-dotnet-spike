using System;
using System.Collections.Generic;

namespace CircuitBreakerSpike.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private IRepositoryExceptionThrowingState _repositoryExceptionThrowingState;

        public InventoryRepository(IRepositoryExceptionThrowingState repositoryExceptionThrowingState)
        {
            _repositoryExceptionThrowingState = repositoryExceptionThrowingState;
        }

        public IEnumerable<InventoryItem> FindInventoryItems()
        {
            if (_repositoryExceptionThrowingState.ThrowExceptions)
            {
                throw new Exception("Inventory system is not responding.");
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

        
    }
}
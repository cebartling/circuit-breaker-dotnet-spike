using System;
using System.Collections.Generic;
using System.Threading;

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
                int millisecondsTimeout = _repositoryExceptionThrowingState.SecondsToWaitBeforeThrowingException * 1000;
                Thread.Sleep(millisecondsTimeout);
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
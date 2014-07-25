using System;
using System.Collections.Generic;

namespace CircuitBreakerSpike.Repositories
{
    public class OrderManagementRepository : IOrderManagementRepository
    {
        private IRepositoryExceptionThrowingState _repositoryExceptionThrowingState;

        public OrderManagementRepository(IRepositoryExceptionThrowingState repositoryExceptionThrowingState)
        {
            _repositoryExceptionThrowingState = repositoryExceptionThrowingState;
        }

        public IEnumerable<Order> FindOrders()
        {
            if (_repositoryExceptionThrowingState.ThrowExceptions)
            {
                throw new Exception("Order management system is not responding.");
            }
            return new List<Order>
            {
                new Order(),
                new Order(),
                new Order(),
                new Order(),
                new Order(),
                new Order(),
                new Order(),
                new Order(),
                new Order()
            };
        }

    }
}
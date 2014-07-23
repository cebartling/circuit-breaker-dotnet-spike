using System;
using System.Collections.Generic;

namespace CircuitBreakerSpike.Repositories
{
    public class OrderManagementRepository : IOrderManagementRepository
    {
        public OrderManagementRepository()
        {
            ThrowExceptions = false;
        }

        public IEnumerable<Order> FindOrders()
        {
            if (ThrowExceptions)
            {
                throw new ApplicationException("Order management system is not responding.");
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

        public bool ThrowExceptions { get; set; }
    }
}
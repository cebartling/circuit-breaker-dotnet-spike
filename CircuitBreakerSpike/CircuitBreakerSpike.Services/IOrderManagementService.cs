using System.Collections.Generic;
using CircuitBreakerSpike.Repositories;

namespace CircuitBreakerSpike.Services
{
    public interface IOrderManagementService
    {
        IEnumerable<Order> GetOrders();
    }
}
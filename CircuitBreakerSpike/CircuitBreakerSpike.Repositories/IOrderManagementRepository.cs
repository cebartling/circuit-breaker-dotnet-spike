using System.Collections.Generic;

namespace CircuitBreakerSpike.Repositories
{
    public interface IOrderManagementRepository
    {
        IEnumerable<Order> FindOrders();
        bool ThrowExceptions { get; set; }
    }
}
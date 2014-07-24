using System.Collections.Generic;
using CircuitBreakerSpike.Repositories;

namespace CircuitBreakerSpike.Services
{
    public class OrderManagementService : IOrderManagementService
    {
        private IOrderManagementRepository _orderManagementRepository;
        private IInventoryRepository _inventoryRepository;

        public OrderManagementService(IOrderManagementRepository orderManagementRepository, 
            IInventoryRepository inventoryRepository)
        {
            _orderManagementRepository = orderManagementRepository;
            _inventoryRepository = inventoryRepository;
        }


        public IEnumerable<Order> GetOrders()
        {
            return null;
        }
    }
}
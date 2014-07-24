using System;
using System.Collections.Generic;
using CircuitBreakerSpike.Core;
using CircuitBreakerSpike.Repositories;

namespace CircuitBreakerSpike.Services
{
    public class OrderManagementService : IOrderManagementService
    {
        private readonly IOrderManagementRepository _orderManagementRepository;
        private IInventoryRepository _inventoryRepository;
        private readonly CircuitBreaker _orderManagementRepositoryCircuitBreaker;

        public OrderManagementService(IOrderManagementRepository orderManagementRepository, 
            IInventoryRepository inventoryRepository)
        {
            _orderManagementRepository = orderManagementRepository;
            _inventoryRepository = inventoryRepository;
            var openToHalfOpenWaitTime = new TimeSpan(0,0,0,20);
            _orderManagementRepositoryCircuitBreaker = new CircuitBreaker(openToHalfOpenWaitTime);
        }


        public IEnumerable<Order> GetOrders()
        {
            IEnumerable<Order> orders = new List<Order>();
            Action action = () => orders = _orderManagementRepository.FindOrders();
            _orderManagementRepositoryCircuitBreaker.ExecuteAction(action);
            return orders;
        }
    }
}
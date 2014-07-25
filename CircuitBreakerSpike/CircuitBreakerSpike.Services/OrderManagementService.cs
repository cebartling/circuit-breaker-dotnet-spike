using System;
using System.Collections.Generic;
using CircuitBreakerSpike.Core;
using CircuitBreakerSpike.Repositories;

namespace CircuitBreakerSpike.Services
{
    public class OrderManagementService : IOrderManagementService
    {
        private readonly IOrderManagementRepository _orderManagementRepository;
        private readonly ICircuitBreaker _orderManagementRepositoryCircuitBreaker;

        public OrderManagementService(IOrderManagementRepository orderManagementRepository,
            ICircuitBreaker orderManagementRepositoryCircuitBreaker)
        {
            _orderManagementRepository = orderManagementRepository;
            _orderManagementRepositoryCircuitBreaker = orderManagementRepositoryCircuitBreaker;
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
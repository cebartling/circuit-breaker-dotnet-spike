using System.Collections.Generic;
using CircuitBreakerSpike.Core;

namespace CircuitBreakerSpike.Services
{
    public class CircuitBreakerService : ICircuitBreakerService
    {
        public const string ORDER_MANAGEMENT_CIRCUIT_BREAKER = "ORDER_MANAGEMENT_CIRCUIT_BREAKER";
        public const string INVENTORY_MANAGEMENT_CIRCUIT_BREAKER = "INVENTORY_MANAGEMENT_CIRCUIT_BREAKER";

        private readonly IDictionary<string, ICircuitBreaker> _map = new Dictionary<string, ICircuitBreaker>();

        public CircuitBreakerService(ICircuitBreaker orderManagementCircuitBreaker,
            ICircuitBreaker inventoryManagementCircuitBreakerService)
        {
            _map[ORDER_MANAGEMENT_CIRCUIT_BREAKER] = orderManagementCircuitBreaker;
            _map[INVENTORY_MANAGEMENT_CIRCUIT_BREAKER] = inventoryManagementCircuitBreakerService;
        }

        public void Enable(string circuitBreakerName)
        {
            if (_map.ContainsKey(circuitBreakerName))
            {
                _map[circuitBreakerName].Enabled = true;
            }
        }

        public void Disable(string circuitBreakerName)
        {
            if (_map.ContainsKey(circuitBreakerName))
            {
                _map[circuitBreakerName].Enabled = false;
            }
        }
    }
}
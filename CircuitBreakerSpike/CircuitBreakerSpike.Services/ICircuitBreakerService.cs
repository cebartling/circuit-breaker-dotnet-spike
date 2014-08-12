namespace CircuitBreakerSpike.Services
{
    public interface ICircuitBreakerService
    {
        /// <summary>
        /// Enable the circuit breaker functionality.
        /// </summary>
        /// <param name="circuitBreakerName"></param>
        void Enable(string circuitBreakerName);

        /// <summary>
        /// Disable the circuit breaker functionality.
        /// </summary>
        /// <param name="circuitBreakerName"></param>
        void Disable(string circuitBreakerName);
    }
}
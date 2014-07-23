namespace CircuitBreakerSpike.Core
{
    public class CircuitBreakerStateStoreFactory
    {
        /// <summary>
        ///     Create a new instance of ICircuitBreakerStateStore implementation.
        /// </summary>
        /// <returns>A new instance of the ICircuitBreakerStateStore implementation</returns>
        public static ICircuitBreakerStateStore GetCircuitBreakerStateStore()
        {
            return new CircuitBreakerStateStore();
        }
    }
}
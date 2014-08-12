using System;

namespace CircuitBreakerSpike.Core
{
    public interface ICircuitBreaker
    {
        TimeSpan OpenToHalfOpenWaitTime { get; set; }
        bool IsClosed { get; }
        bool IsOpen { get; }
        bool IsHalfOpen { get; }

        /// <summary>
        /// Enable/disable the circuit breaker (for demonstrative purposes only).
        /// </summary>
        bool Enabled { get; set; }


        /// <summary>
        ///     Execute an Action with a circuit breaker wrapped around it.
        /// </summary>
        /// <param name="action">A System.Action delegate.</param>
        void ExecuteAction(Action action);
    }
}
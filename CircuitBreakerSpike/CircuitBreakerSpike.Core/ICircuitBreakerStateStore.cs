using System;

namespace CircuitBreakerSpike.Core
{
    public interface ICircuitBreakerStateStore
    {
        CircuitBreakerStateEnum State { get; }

        Exception LastException { get; }

        DateTime LastStateChangedDateUtc { get; }
        bool IsClosed { get; }

        void Trip(Exception ex);

        void Reset();

        void HalfOpen();
    }
}
using System;

namespace CircuitBreakerSpike.Core
{
    public class CircuitBreakerStateStore : ICircuitBreakerStateStore
    {
        public CircuitBreakerStateEnum State { get; private set; }
        public Exception LastException { get; private set; }
        public DateTime LastStateChangedDateUtc { get; private set; }

        public CircuitBreakerStateStore()
        {
            State = CircuitBreakerStateEnum.Closed;
        }

        public bool IsClosed
        {
            get { return State == CircuitBreakerStateEnum.Closed; }
            private set { }
        }

        public void Trip(Exception ex)
        {
            State = CircuitBreakerStateEnum.Open;
            LastException = ex;
            LastStateChangedDateUtc = DateTime.UtcNow;
        }

        public void Reset()
        {
            State = CircuitBreakerStateEnum.Closed;
            LastStateChangedDateUtc = DateTime.UtcNow;
        }

        public void HalfOpen()
        {
            State = CircuitBreakerStateEnum.HalfOpen;
            LastStateChangedDateUtc = DateTime.UtcNow;
        }
    }
}
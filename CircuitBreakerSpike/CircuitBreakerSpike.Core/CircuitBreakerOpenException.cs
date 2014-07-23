using System;

namespace CircuitBreakerSpike.Core
{
    public class CircuitBreakerOpenException : Exception
    {
        public CircuitBreakerOpenException(Exception lastException)
        {
            LastException = lastException;
        }

        public Exception LastException { get; set; }
    }
}
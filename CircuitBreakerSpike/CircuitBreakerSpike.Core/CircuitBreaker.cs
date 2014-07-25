using System;
using System.Threading;

namespace CircuitBreakerSpike.Core
{
    /// <summary>
    ///     Circuit breaker pattern implementation.
    /// </summary>
    public class CircuitBreaker : ICircuitBreaker
    {
        private readonly object _halfOpenSyncObject = new object();

        private readonly ICircuitBreakerStateStore _stateStore =
            CircuitBreakerStateStoreFactory.GetCircuitBreakerStateStore();

        public TimeSpan OpenToHalfOpenWaitTime { get; set; }

        public CircuitBreaker()
        {
            OpenToHalfOpenWaitTime = new TimeSpan(0,0,0,20);
        }

        public CircuitBreaker(TimeSpan openToHalfOpenWaitTime)
        {
            OpenToHalfOpenWaitTime = openToHalfOpenWaitTime;
        }

        public bool IsClosed
        {
            get { return _stateStore.IsClosed; }
        }

        public bool IsOpen
        {
            get { return !IsClosed; }
        }

        public bool IsHalfOpen
        {
            get { return CircuitBreakerStateEnum.HalfOpen == _stateStore.State; }
        }

        /// <summary>
        ///     Execute an Action with a circuit breaker wrapped around it.
        /// </summary>
        /// <param name="action">A System.Action delegate.</param>
        public void ExecuteAction(Action action)
        {
            if (IsOpen)
            {
                TryActionWhenCircuitIsOpen(action);
            }
            DoActionWhenCircuitIsClosed(action);
        }

        private void TryActionWhenCircuitIsOpen(Action action)
        {
            // The circuit breaker is Open. Check if the Open timeout has expired.
            // If it has, set the state to HalfOpen. Another approach may be to simply 
            // check for the HalfOpen state that had be set by some other operation.
            if (_stateStore.LastStateChangedDateUtc + OpenToHalfOpenWaitTime < DateTime.UtcNow)
            {
                // The Open timeout has expired. Allow one operation to execute. Note that, in
                // this example, the circuit breaker is simply set to HalfOpen after being 
                // in the Open state for some period of time. An alternative would be to set 
                // this using some other approach such as a timer, test method, manually, and 
                // so on, and simply check the state here to determine how to handle execution
                // of the action. 
                // Limit the number of threads to be executed when the breaker is HalfOpen.
                // An alternative would be to use a more complex approach to determine which
                // threads or how many are allowed to execute, or to execute a simple test 
                // method instead.
                bool lockTaken = false;
                try
                {
                    Monitor.TryEnter(_halfOpenSyncObject, ref lockTaken);
                    if (lockTaken)
                    {
                        // Set the circuit breaker state to HalfOpen.
                        _stateStore.HalfOpen();

                        // Attempt the operation.
                        action();

                        // If this action succeeds, reset the state and allow other operations.
                        // In reality, instead of immediately returning to the Open state, a counter
                        // here would record the number of successful operations and return the
                        // circuit breaker to the Open state only after a specified number succeed.
                        _stateStore.Reset();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // If there is still an exception, trip the breaker again immediately.
                    _stateStore.Trip(ex);

                    // Throw the exception so that the caller knows which exception occurred.
                    throw;
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(_halfOpenSyncObject);
                    }
                }
            }
            // The Open timeout has not yet expired. Throw a CircuitBreakerOpen exception to
            // inform the caller that the caller that the call was not actually attempted, 
            // and return the most recent exception received.
            throw new CircuitBreakerOpenException(_stateStore.LastException);
        }

        private void DoActionWhenCircuitIsClosed(Action action)
        {
            // The circuit breaker is Closed, execute the action.
            try
            {
                action();
            }
            catch (Exception ex)
            {
                // If an exception still occurs here, simply 
                // re-trip the breaker immediately.
                TrackException(ex);

                // Throw the exception so that the caller can tell
                // the type of exception that was thrown.
                throw;
            }
        }

        private void TrackException(Exception ex)
        {
            // For simplicity in this example, open the circuit breaker on the first exception.
            // In reality this would be more complex. A certain type of exception, such as one
            // that indicates a service is offline, might trip the circuit breaker immediately. 
            // Alternatively it may count exceptions locally or across multiple instances and
            // use this value over time, or the exception/success ratio based on the exception
            // types, to open the circuit breaker.
            _stateStore.Trip(ex);
        }
    }
}
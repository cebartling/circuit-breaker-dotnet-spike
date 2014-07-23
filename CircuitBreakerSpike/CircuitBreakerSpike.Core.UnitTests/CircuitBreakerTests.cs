using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitBreakerSpike.Core.UnitTests
{
    [TestClass]
    public class CircuitBreakerTests
    {
        private CircuitBreaker _circuitBreaker;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _circuitBreaker = new CircuitBreaker();
        }

        private void DoSuccessfulAction()
        {
            Console.WriteLine("Executing action with successful result");
        }

        private void DoUnsuccessfulAction()
        {
            throw new ApplicationException("Just a test");
        }

        private void DoUnsuccessfulActionAndEatException()
        {
            try
            {
                _circuitBreaker.ExecuteAction(DoUnsuccessfulAction);
            }
            catch (Exception e)
            {
            }
        }

        private void DoSuccessfulActionAndEatException()
        {
            try
            {
                _circuitBreaker.ExecuteAction(DoSuccessfulAction);
            }
            catch (CircuitBreakerOpenException e)
            {
            }
        }

        #region ExecuteAction() tests

        [TestMethod]
        public void ExecuteAction_CircuitBreakInitializesToClosedState()
        {
            Assert.IsTrue(_circuitBreaker.IsClosed);
        }

        [TestMethod]
        public void ExecuteAction_CircuitBreakRemainsClosed()
        {
            Action action = DoSuccessfulAction;
            _circuitBreaker.ExecuteAction(action);

            Assert.IsTrue(_circuitBreaker.IsClosed);
        }

        [TestMethod, ExpectedException(typeof (ApplicationException))]
        public void ExecuteAction_CircuitBreakTransitionsToOpenState()
        {
            Action action = DoUnsuccessfulAction;
            try
            {
                _circuitBreaker.ExecuteAction(action);
            }
            catch (ApplicationException e)
            {
                Assert.IsTrue(_circuitBreaker.IsOpen);
                throw;
            }
        }

        [TestMethod, ExpectedException(typeof (CircuitBreakerOpenException))]
        public void ExecuteAction_ThrowsCircuitBreakerOpenException()
        {
            DoUnsuccessfulActionAndEatException();
            DoUnsuccessfulActionAndEatException();
            _circuitBreaker.ExecuteAction(DoSuccessfulAction);
        }

        [TestMethod]
        public void ExecuteAction_CircuitBreakTransitionsToOpenState_ThenHalfOpenState_ThenClosedState()
        {
            // To facilitate unit testing, set up circuit breaker with an extremely 
            // small wait period for transitioning to half-open state.
            _circuitBreaker = new CircuitBreaker(new TimeSpan(0,0,0,0,20));
            DoUnsuccessfulActionAndEatException();
            DoUnsuccessfulActionAndEatException();
            Thread.Sleep(200);
            DoSuccessfulActionAndEatException();

            Assert.IsTrue(_circuitBreaker.IsClosed);
            Assert.IsFalse(_circuitBreaker.IsOpen);
        }

        #endregion
    }
}
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitBreakerSpike.Core.UnitTests
{
    [TestClass]
    public class CircuitBreakerStateStoreTests
    {
        private ICircuitBreakerStateStore _store;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _store = new CircuitBreakerStateStore();
        }

        #region IsClosed() tests

        [TestMethod]
        public void IsClosed_DefaultStateAfterInstantiation()
        {
            Assert.IsTrue(_store.IsClosed);
        }

        [TestMethod]
        public void IsClosed_AfterTripping()
        {
            _store.Trip(new Exception("Just testing"));

            Assert.IsFalse(_store.IsClosed);
        }

        #endregion

        #region Trip() tests

        [TestMethod]
        public void Trip_SetStateToOpen()
        {
            _store.Trip(new Exception("just a test"));

            Assert.IsTrue(CircuitBreakerStateEnum.Open == _store.State);
        }

        [TestMethod]
        public void Trip_CachesLastException()
        {
            var expectedException = new Exception("A testing exception");

            _store.Trip(expectedException);

            Assert.AreEqual(expectedException, _store.LastException);
        }

        [TestMethod]
        public void Trip_SetsLastStateChangeTimestamp()
        {
            DateTime lastChangedTimestamp = _store.LastStateChangedDateUtc;

            _store.Trip(new Exception("A testing exception"));

            Assert.IsNotNull(_store.LastStateChangedDateUtc);
            Assert.AreNotEqual(lastChangedTimestamp, _store.LastStateChangedDateUtc);
        }

        #endregion

        #region Reset() tests

        [TestMethod]
        public void Reset_AfterTripping_StateSetToClosed()
        {
            _store.Trip(new Exception("Just testing"));

            _store.Reset();

            Assert.AreEqual(CircuitBreakerStateEnum.Closed, _store.State);
        }

        [TestMethod]
        public void Reset_SetsLastStateChangedDateUtc()
        {
            _store.Trip(new Exception("Just testing"));

            _store.Reset();
        
            Assert.AreEqual(DateTime.UtcNow.Ticks, _store.LastStateChangedDateUtc.Ticks);
        }

        #endregion

        #region HalfOpen() tests

        [TestMethod]
        public void HalfOpen_SetsStateToHalfOpen()
        {
            _store.Trip(new Exception("just testin"));

            _store.HalfOpen();

            Assert.AreEqual(CircuitBreakerStateEnum.HalfOpen, _store.State);
        }

        [TestMethod]
        public void HalfOpen_SetsLastStateChangedDateUtc()
        {
            _store.Trip(new Exception("just testin"));
            Thread.Sleep(300);

            _store.HalfOpen();

            Assert.AreEqual(DateTime.UtcNow.Ticks, _store.LastStateChangedDateUtc.Ticks);
        }

        #endregion
    }
}
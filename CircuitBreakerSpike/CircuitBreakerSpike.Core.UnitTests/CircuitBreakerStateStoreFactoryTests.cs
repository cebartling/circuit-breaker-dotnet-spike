using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitBreakerSpike.Core.UnitTests
{
    [TestClass]
    public class CircuitBreakerStateStoreFactoryTests
    {
        [TestMethod]
        public void GetCircuitBreakerStateStore_ReturnsICircuitBreakerStateStoreImplementation()
        {
            var circuitBreakerStateStore = CircuitBreakerStateStoreFactory.GetCircuitBreakerStateStore();

            Assert.IsNotNull(circuitBreakerStateStore);
        }
    }
}
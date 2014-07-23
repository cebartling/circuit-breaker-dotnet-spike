using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitBreakerSpike.Repositories.UnitTests
{
    [TestClass]
    public class InventoryRepositoryTests
    {
        private IInventoryRepository _repository;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _repository = new InventoryRepository();
        }

        [TestMethod]
        public void FindOrders_ReturnsEnumerationOfInventoryItems()
        {
            var orders = _repository.FindInventoryItems();

            Assert.AreEqual(9, orders.Count());
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void FindOrders_ThrowsApplicationException()
        {
            _repository.ThrowExceptions = true;
            _repository.FindInventoryItems();
        }

    }
}
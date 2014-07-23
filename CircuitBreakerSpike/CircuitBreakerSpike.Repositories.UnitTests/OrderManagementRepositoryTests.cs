using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitBreakerSpike.Repositories.UnitTests
{
    [TestClass]
    public class OrderManagementRepositoryTests
    {
        private IOrderManagementRepository _repository;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _repository = new OrderManagementRepository();
        }

        [TestMethod]
        public void FindOrders_ReturnsEnumerationOfOrders()
        {
            var orders = _repository.FindOrders();

            Assert.AreEqual(9, orders.Count());
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void FindOrders_ThrowsApplicationException()
        {
            _repository.ThrowExceptions = true;
            _repository.FindOrders();
        }


    }
}
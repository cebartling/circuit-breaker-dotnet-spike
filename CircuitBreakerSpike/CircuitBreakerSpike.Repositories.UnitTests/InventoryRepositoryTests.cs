using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CircuitBreakerSpike.Repositories.UnitTests
{
    [TestClass]
    public class InventoryRepositoryTests
    {
        private Mock<IRepositoryExceptionThrowingState> _repositoryExceptionThrowingStateMock; 
        private IInventoryRepository _repository;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _repositoryExceptionThrowingStateMock = new Mock<IRepositoryExceptionThrowingState>();
            _repository = new InventoryRepository(_repositoryExceptionThrowingStateMock.Object);
        }

        [TestMethod]
        public void FindOrders_ReturnsEnumerationOfInventoryItems()
        {
            _repositoryExceptionThrowingStateMock.SetupGet(x => x.ThrowExceptions).Returns(false);

            var orders = _repository.FindInventoryItems();

            Assert.AreEqual(9, orders.Count());
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void FindOrders_ThrowsException()
        {
            _repositoryExceptionThrowingStateMock.SetupGet(x => x.ThrowExceptions).Returns(true);

            _repository.FindInventoryItems();
        }

    }
}
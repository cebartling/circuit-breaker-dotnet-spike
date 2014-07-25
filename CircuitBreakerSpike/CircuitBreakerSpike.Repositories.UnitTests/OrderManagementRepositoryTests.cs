using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CircuitBreakerSpike.Repositories.UnitTests
{
    [TestClass]
    public class OrderManagementRepositoryTests
    {
        private Mock<IRepositoryExceptionThrowingState> _repositoryExceptionThrowingStateMock; 
        private IOrderManagementRepository _repository;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _repositoryExceptionThrowingStateMock = new Mock<IRepositoryExceptionThrowingState>();
            _repository = new OrderManagementRepository(_repositoryExceptionThrowingStateMock.Object);
        }

        [TestMethod]
        public void FindOrders_ReturnsEnumerationOfOrders()
        {
            _repositoryExceptionThrowingStateMock.SetupGet(x => x.ThrowExceptions).Returns(false);

            var orders = _repository.FindOrders();

            Assert.AreEqual(9, orders.Count());
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void FindOrders_ThrowsException()
        {
            _repositoryExceptionThrowingStateMock.SetupGet(x => x.ThrowExceptions).Returns(true);

            _repository.FindOrders();
        }
    }
}
using System;
using System.Collections.Generic;
using CircuitBreakerSpike.Core;
using CircuitBreakerSpike.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CircuitBreakerSpike.Services.UnitTests
{
    [TestClass]
    public class OrderManagementServiceTests
    {
        private Mock<ICircuitBreaker> _circuitBreakerMock;
        private Mock<IOrderManagementRepository> _orderManagementRepositoryMock;
        private IOrderManagementService _service;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _circuitBreakerMock = new Mock<ICircuitBreaker>();
            _orderManagementRepositoryMock = new Mock<IOrderManagementRepository>();
            _service = new OrderManagementService(_orderManagementRepositoryMock.Object, 
                _circuitBreakerMock.Object);
        }

        [TestMethod]
        public void GetOrders_VerifyCircuitBreakerCollaboration()
        {
            _circuitBreakerMock.Setup(x => x.ExecuteAction(It.IsAny<Action>()));
            IEnumerable<Order> expectedOrders = new List<Order>
            {
                new Order(),
                new Order(),
                new Order()
            };

            var orders = _service.GetOrders();

           _circuitBreakerMock.Verify(x => x.ExecuteAction(It.IsAny<Action>()));

        }
    }
}
using System.Collections.Generic;
using CircuitBreakerSpike.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CircuitBreakerSpike.Services.UnitTests
{
    [TestClass]
    public class OrderManagementServiceTests
    {
        private Mock<IInventoryRepository> _inventoryRepositoryMock;
        private Mock<IOrderManagementRepository> _orderManagementRepositoryMock;
        private IOrderManagementService _service;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _inventoryRepositoryMock = new Mock<IInventoryRepository>();
            _orderManagementRepositoryMock = new Mock<IOrderManagementRepository>();
            _service = new OrderManagementService(_orderManagementRepositoryMock.Object, 
                _inventoryRepositoryMock.Object);
        }

        [TestMethod]
        public void GetOrders_VerifyOrderManagementRepositoryCollaboration()
        {
            IEnumerable<Order> expectedOrders = new List<Order>
            {
                new Order(),
                new Order(),
                new Order()
            };
            _orderManagementRepositoryMock.Setup(x => x.FindOrders()).Returns(expectedOrders);

            var orders = _service.GetOrders();

            _orderManagementRepositoryMock.Verify(x => x.FindOrders());
        }
    }
}
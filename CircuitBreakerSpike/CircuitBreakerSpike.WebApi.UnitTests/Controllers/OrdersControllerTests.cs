using System.Collections.Generic;
using CircuitBreakerSpike.Repositories;
using CircuitBreakerSpike.Services;
using CircuitBreakerSpike.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CircuitBreakerSpike.WebApi.UnitTests.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {
        private Mock<IOrderManagementService> _orderManagementServiceMock;
        private OrdersController _controller;

        [TestInitialize]
        public void InitializeBeforeEachTest()
        {
            _orderManagementServiceMock = new Mock<IOrderManagementService>();
            _controller = new OrdersController(_orderManagementServiceMock.Object);
        }

        [TestMethod]
        public void Get_VerifiesOrderManagementServiceCollaboration()
        {
            IEnumerable<Order> expectedOrders = new List<Order>();
            _orderManagementServiceMock.Setup(x => x.GetOrders()).Returns(expectedOrders);

            IEnumerable<Order> actualOrders = _controller.Get();

            _orderManagementServiceMock.Verify(x => x.GetOrders());
        }
    }
}
using Moq;
using NUnit.Framework;
using Restaurant.Application.Handlers.Orders;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Tests.Application.Handlers.Orders
{
    internal class CreateOrderHandlerShould
    {
        private Mock<IOrderService> _orderService;
        private CreateOrderHandler _createOrderHandler;

        [SetUp]
        public void SetUp()
        {
            _orderService = new Mock<IOrderService>();
            _createOrderHandler = new CreateOrderHandler(_orderService.Object);
        }

        [Test]
        public async Task ReturnFalse_When_NotCreated()
        {
            _orderService
                .Setup(o => o.Add(It.IsAny<List<int>>()))
                .ReturnsAsync(default(Order));

            Assert.IsFalse(await _createOrderHandler.Create(new List<int>()));
        }

        [Test]
        public async Task ReturnTrue_When_Created()
        {
            _orderService
                .Setup(o => o.Add(It.IsAny<List<int>>()))
                .ReturnsAsync(new Order());

            Assert.IsTrue(await _createOrderHandler.Create(new List<int>()));
        }
    }
}

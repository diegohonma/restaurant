using Moq;
using NUnit.Framework;
using Restaurant.Application.Handlers.Orders;
using Restaurant.Application.Requests;
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
        public async Task ReturnNull_When_NotCreated()
        {
            _orderService
                .Setup(o => o.Add(It.IsAny<List<Product>>()))
                .ReturnsAsync(default(Order));

            Assert.IsNull(await _createOrderHandler.Create(new CreateOrderRequest(new List<CreateOrderProductsRequest>())));
        }

        [Test]
        public async Task ReturnOrder_When_Created()
        {
            _orderService
                .Setup(o => o.Add(It.IsAny<List<Product>>()))
                .ReturnsAsync(new Order());

            Assert.IsNotNull(await _createOrderHandler.Create(new CreateOrderRequest(new List<CreateOrderProductsRequest>())));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Restaurant.Api.Controllers;
using Restaurant.Application.Handlers.Orders;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.Tests.Presentation.Controllers
{
    public class OrdersControllerShould
    {
        private OrdersController _ordersController;
        private Mock<ICreateOrderHandler> _createOrderHandler;

        [OneTimeSetUp]
        public void SetUp()
        {
            _createOrderHandler = new Mock<ICreateOrderHandler>();
            _ordersController = new OrdersController(_createOrderHandler.Object);
        }

        [Test]
        public async Task Return_NoContent_When_Created()
        {
            _createOrderHandler
                .Setup(g => g.Create(It.IsAny<List<int>>()))
                .ReturnsAsync(true);

            var response = await _ordersController.Create(new List<int>());

            Assert.Multiple(() =>
            {
                var objectResult = (NoContentResult)response;
                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.NoContent, objectResult.StatusCode);
            });
        }

        [Test]
        public async Task Return_Ok_When_ProductsFound()
        {
            _createOrderHandler
                .Setup(g => g.Create(It.IsAny<List<int>>()))
                .ReturnsAsync(false);

            var response = await _ordersController.Create(new List<int>());

            Assert.Multiple(() =>
            {
                var objectResult = (BadRequestResult)response;
                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
            });
        }
    }
}

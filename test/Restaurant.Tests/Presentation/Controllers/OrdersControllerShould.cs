using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Restaurant.Api.Controllers;
using Restaurant.Application.Handlers.Orders;
using Restaurant.Application.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.Tests.Presentation.Controllers
{
    public class OrdersControllerShould
    {
        private OrdersController _ordersController;
        private Mock<ICreateOrderHandler> _createOrderHandler;
        private Mock<IGetOrderHandler> _getOrderHandler;

        [OneTimeSetUp]
        public void SetUp()
        {
            _createOrderHandler = new Mock<ICreateOrderHandler>();
            _getOrderHandler = new Mock<IGetOrderHandler>();
            _ordersController = new OrdersController(_createOrderHandler.Object, _getOrderHandler.Object);
        }

        [Test]
        public async Task Return_NoContent_When_Created()
        {
            _createOrderHandler
                .Setup(c => c.Create(It.IsAny<List<int>>()))
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
                .Setup(c => c.Create(It.IsAny<List<int>>()))
                .ReturnsAsync(false);

            var response = await _ordersController.Create(new List<int>());

            Assert.Multiple(() =>
            {
                var objectResult = (BadRequestResult)response;
                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
            });
        }

        [Test]
        public void Return_NotFound_When_OrderNotFound()
        {
            _getOrderHandler
                .Setup(g => g.GetOrderById(It.IsAny<string>()))
                .Returns(new GetOrdersResponse(new List<GetOrdersViewModel>()));

            var response = _ordersController.GetById(string.Empty);

            Assert.Multiple(() =>
            {
                var result = (NotFoundResult)response;
                Assert.IsNotNull(result);
                Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
            });
        }

        [Test]
        public void Return_Ok_When_ProductFound()
        {
            var expectedOrder = new GetOrdersViewModel(Guid.NewGuid().ToString(), "status");

            _getOrderHandler
                .Setup(g => g.GetOrderById(It.IsAny<string>()))
                .Returns(new GetOrdersResponse(new List<GetOrdersViewModel> { expectedOrder }));

            var response = _ordersController.GetById(string.Empty);

            Assert.Multiple(() =>
            {
                var objectResult = (OkObjectResult)response;
                var content = objectResult.Value as GetOrdersViewModel;

                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
                Assert.IsNotNull(content);
                Assert.AreEqual(expectedOrder.OrderId, content.OrderId);
                Assert.AreEqual(expectedOrder.OrderStatus, content.OrderStatus);
            });
        }
    }
}

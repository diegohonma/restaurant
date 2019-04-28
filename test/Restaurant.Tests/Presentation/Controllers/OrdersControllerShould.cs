using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Restaurant.Api.Controllers;
using Restaurant.Application.Handlers.Orders;
using Restaurant.Application.Requests;
using Restaurant.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var expectedResponse = new Response<GetOrdersResponse>(
                new GetOrdersResponse("orderId", "orderStatus"),
                string.Empty);

            _createOrderHandler
                .Setup(c => c.Create(It.IsAny<CreateOrderRequest>()))
                .ReturnsAsync(expectedResponse);

            var response = await _ordersController.Create(new CreateOrderRequest(new List<CreateOrderProductsRequest>()));

            Assert.Multiple(() =>
            {
                var objectResult = (OkObjectResult)response;
                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);

                var content = objectResult.Value as GetOrdersResponse;
                Assert.IsNotNull(content);
                Assert.AreEqual(expectedResponse.Value.OrderId, content.OrderId);
                Assert.AreEqual(expectedResponse.Value.OrderStatus, content.OrderStatus);
            });
        }

        [Test]
        public async Task Return_BadRequest_When_NotCreated()
        {
            const string expectedError = "error";

            _createOrderHandler
                .Setup(c => c.Create(It.IsAny<CreateOrderRequest>()))
                .ReturnsAsync(new Response<GetOrdersResponse>(
                    default(GetOrdersResponse), expectedError));

            var response = await _ordersController.Create(new CreateOrderRequest(new List<CreateOrderProductsRequest>()));

            Assert.Multiple(() =>
            {
                var objectResult = (BadRequestObjectResult)response;
                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);

                var content = objectResult.Value as Response<GetOrdersResponse>;
                Assert.IsNotNull(content);
                Assert.AreEqual(expectedError, content.Error);
            });
        }

        [Test]
        public void Return_NotFound_When_OrderNotFound()
        {
            _getOrderHandler
                .Setup(g => g.GetOrderById(It.IsAny<string>()))
                .Returns(default(GetOrdersResponse));

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
            var expectedOrder = new GetOrdersResponse(Guid.NewGuid().ToString(), "status");

            _getOrderHandler
                .Setup(g => g.GetOrderById(It.IsAny<string>()))
                .Returns(expectedOrder);

            var response = _ordersController.GetById(string.Empty);

            Assert.Multiple(() =>
            {
                var objectResult = (OkObjectResult)response;
                var content = objectResult.Value as GetOrdersResponse;

                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
                Assert.IsNotNull(content);
                Assert.AreEqual(expectedOrder.OrderId, content.OrderId);
                Assert.AreEqual(expectedOrder.OrderStatus, content.OrderStatus);
            });
        }

        [Test]
        public void Return_Ok_When_ProductsFound()
        {
            var expectedOrders = new List<GetOrdersResponse>
            {
                new GetOrdersResponse("1", "status")
            };

            _getOrderHandler
                .Setup(g => g.GetAll())
                .Returns(expectedOrders);

            var response = _ordersController.GetAll();

            Assert.Multiple(() =>
            {
                var objectResult = (OkObjectResult)response;
                var content = objectResult.Value as List<GetOrdersResponse>;

                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
                Assert.IsNotNull(content);

                var order = content.First();
                Assert.IsNotNull(order);
                Assert.AreEqual(expectedOrders.First().OrderId, order.OrderId);
                Assert.AreEqual(expectedOrders.First().OrderStatus, order.OrderStatus);
            });
        }


        [Test]
        public void Return_NoContent_When_ProductsNotFound()
        {
            _getOrderHandler
                .Setup(g => g.GetAll())
                .Returns(new List<GetOrdersResponse>());

            var response = _ordersController.GetAll();

            Assert.Multiple(() =>
            {
                var objectResult = (NoContentResult)response;
                Assert.IsNotNull(objectResult);
                Assert.AreEqual((int)HttpStatusCode.NoContent, objectResult.StatusCode);
            });
        }
    }
}

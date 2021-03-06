﻿using Moq;
using NUnit.Framework;
using Restaurant.Application.Handlers.Orders;
using Restaurant.CrossCutting;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Tests.Application.Handlers.Orders
{
    public class GetOrderHandlerShould
    {
        private Mock<IOrderRepository> _orderRepository;
        private GetOrderHandler _getOrderHandler;

        [OneTimeSetUp]
        public void SetUp()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _getOrderHandler = new GetOrderHandler(_orderRepository.Object);
        }

        [Test]
        public void Return_Null_When_NotFound()
        {
            _orderRepository
                .Setup(o => o.GetById(It.IsAny<Guid>()))
                .Returns(default(Order));

            Assert.IsNull(_getOrderHandler.GetOrderById(Guid.NewGuid().ToString()));
        }

        [Test]
        public void Return_Order_When_Found()
        {
            var expectedOrder = new Order();

            _orderRepository
                .Setup(o => o.GetById(It.IsAny<Guid>()))
                .Returns(expectedOrder);

            var foundOrder = _getOrderHandler.GetOrderById(expectedOrder.OrderId.ToString());

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedOrder.OrderId.ToString(), foundOrder.OrderId);
                Assert.AreEqual(expectedOrder.OrderStatus.GetDescription(), foundOrder.OrderStatus);
            });
        }

        [Test]
        public void Return_AllOrders_When_Found()
        {
            var expectedOrder = new List<Order> { new Order() };

            _orderRepository
                .Setup(o => o.GetAll())
                .Returns(expectedOrder);

            var foundOrders = _getOrderHandler.GetAll();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedOrder.Count, foundOrders.Count);

                var order = foundOrders.First();
                Assert.IsNotNull(order);
                Assert.AreEqual(expectedOrder.First().OrderId.ToString(), order.OrderId);
                Assert.AreEqual(expectedOrder.First().OrderStatus.GetDescription(), order.OrderStatus);
            });
        }
    }
}

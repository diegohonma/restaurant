using NUnit.Framework;
using Restaurant.Data.Repositories;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Tests.Data.Repositories
{
    public class OrderRepositoryShould
    {
        private OrderRepository _orderRepository;
        private List<Order> _orders;

        [OneTimeSetUp]
        public void SetUp()
        {
            _orders = new List<Order>();
            _orderRepository = new OrderRepository(_orders);
        }

        [Test]
        public void AddNewOrder()
        {
            var order = new Order();
            _orderRepository.Add(order);
        }

        [Test]
        public void ReturnOrder_When_Found()
        {
            var order = _orders.First();
            var foundOrder = _orderRepository.GetById(order.OrderId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(order.OrderId, foundOrder.OrderId);
                Assert.AreEqual(order.OrderStatus, foundOrder.OrderStatus);
                Assert.AreEqual(order.Products.Count, foundOrder.Products.Count);
            });
        }

        [Test]
        public void ReturnNull_When_NotFound()
        {
            Assert.IsNull(_orderRepository.GetById(Guid.NewGuid()));
        }
    }
}

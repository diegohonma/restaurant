using Moq;
using NUnit.Framework;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Interfaces.Repositories;
using Restaurant.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Tests.Domain
{
    public class OrderServiceShould
    {
        private Mock<IProductsRepository> _productsRepository;
        private Mock<IOrderRepository> _orderRepository;

        private OrderService _orderService;

        [SetUp]
        public void SetUp()
        {
            _productsRepository = new Mock<IProductsRepository>();
            _orderRepository = new Mock<IOrderRepository>();

            _orderService = new OrderService(
                _productsRepository.Object, _orderRepository.Object);
        }

        [Test]
        public async Task ReturnNull_When_ProductIdNotFound()
        {
            _productsRepository
                .Setup(p => p.GetById(It.Is<int>(id => id != 3)))
                .ReturnsAsync(new Product(2, "", "", ProductType.Burger));

            Assert.IsNull(await _orderService.Add(new List<int> { 1, 2, 3 }));
        }

        [Test]
        public async Task ReturnOrder_When_Success()
        {
            _productsRepository
                .Setup(p => p.GetById(It.IsAny<int>()))
                .ReturnsAsync(new Product(2, "", "", ProductType.Burger));

            _orderRepository
                .Setup(o => o.Add(It.IsAny<Order>()));

            Assert.IsNotNull(await _orderService.Add(new List<int> { 1, 2, 3 }));
        }
    }
}

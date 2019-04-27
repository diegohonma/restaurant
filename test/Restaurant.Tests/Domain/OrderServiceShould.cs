using Moq;
using NUnit.Framework;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Interfaces.Repositories;
using Restaurant.Domain.Services;
using System.Collections.Generic;
using System.Linq;
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
                .Setup(p => p.GetById(It.Is<int>(i => i == 1)))
                .ReturnsAsync(new Product(1, "", "", ProductType.Burger));

            _productsRepository
                .Setup(p => p.GetById(It.Is<int>(i => i == 2)))
                .ReturnsAsync(new Product(2, "", "", ProductType.Others));

            _orderRepository
                .Setup(o => o.Add(It.IsAny<Order>()));

            Assert.IsNotNull(await _orderService.Add(new List<int> { 1, 2 }));
        }

        [Test]
        public async Task AddFreeDrink_When_TwoOrMoreBurgers()
        {
            _productsRepository
                .Setup(p => p.GetById(It.IsAny<int>()))
                .ReturnsAsync(new Product(2, "", "", ProductType.Burger));

            _productsRepository
                .Setup(p => p.GetByType(It.IsAny<ProductType>()))
                .ReturnsAsync(new List<Product> { new Product(2, "", "", ProductType.DrinkFree) });

            _orderRepository
                .Setup(o => o.Add(It.IsAny<Order>()));

            var order = await _orderService.Add(new List<int> { 1, 2 });

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(order);
                Assert.IsTrue(order.Products.Count(p => p.Type == ProductType.DrinkFree) == 1);
            });
        }
    }
}

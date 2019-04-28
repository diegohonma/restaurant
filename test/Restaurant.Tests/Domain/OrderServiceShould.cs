using Moq;
using NUnit.Framework;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Interfaces.Repositories;
using Restaurant.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
                _productsRepository.Object, _orderRepository.Object, 4);
        }

        [Test]
        public async Task ReturnNull_When_ProductIdNotFound()
        {
            _productsRepository
                .Setup(p => p.GetById(It.Is<int>(id => id != 3)))
                .ReturnsAsync(new Product(2, "", "", ProductType.Burger));

            _orderRepository
                .Setup(o => o.GetByStatus(It.IsAny<OrderStatus>()))
                .Returns(new List<Order>());

            Assert.IsNull(await _orderService.Add(new List<Product>
            {
                new Product(1, string.Empty, string.Empty, ProductType.None),
                new Product(2, string.Empty, string.Empty, ProductType.None),
                new Product(3, string.Empty, string.Empty, ProductType.None)
            }));
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

            _orderRepository
                .Setup(o => o.GetByStatus(It.IsAny<OrderStatus>()))
                .Returns(new List<Order>());

            Assert.IsNotNull(await _orderService.Add(new List<Product>
            {
                new Product(1, string.Empty, string.Empty, ProductType.None),
                new Product(2, string.Empty, string.Empty, ProductType.None)
            }));
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

            _orderRepository
                .Setup(o => o.GetByStatus(It.IsAny<OrderStatus>()))
                .Returns(new List<Order>());

            var order = await _orderService.Add(new List<Product>
            {
                new Product(1, string.Empty, string.Empty, ProductType.None),
                new Product(2, string.Empty, string.Empty, ProductType.None)
            });

            Assert.Multiple(() =>
            {
                Assert.IsNotNull(order);
                Assert.IsTrue(order.Products.Count(p => p.Type == ProductType.DrinkFree) == 1);
            });
        }

        [Test]
        public async Task NotAllowOrder_When_KitchenHasMaximumOrders()
        {
            _productsRepository
                .Setup(p => p.GetById(It.IsAny<int>()))
                .ReturnsAsync(new Product(2, "", "", ProductType.Burger));

            _orderRepository
                .Setup(o => o.GetByStatus(It.IsAny<OrderStatus>()))
                .Returns(new List<Order>
                {
                    new Order(), new Order(), new Order(), new Order()
                });

            var order = await _orderService.Add(new List<Product>
            {
                new Product(1, string.Empty, string.Empty, ProductType.None),
                new Product(2, string.Empty, string.Empty, ProductType.None)
            });

            Assert.IsNull(order);
        }

        [Test]
        public void FinishOrders_When_CookTimePassed()
        {
            const int cookTime = 3;

            var order = new Order();
            order.AddNewProduct(1, "description", cookTime.ToString(), ProductType.Burger);

            _orderRepository
                .Setup(o => o.GetByStatus(It.Is<OrderStatus>(os => os == OrderStatus.Started)))
                .Returns(new List<Order>
                {
                    order
                });

            Assert.Multiple(() =>
            {
                Assert.IsTrue(order.OrderStatus == OrderStatus.Started);
                Thread.Sleep((cookTime + 1) * 1000);
                _orderService.FinishOrders();

                Assert.IsTrue(order.OrderStatus == OrderStatus.Finished);
            });
            
        }
    }
}

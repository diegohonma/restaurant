using Moq;
using NUnit.Framework;
using Restaurant.Application.Handlers.Products;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Tests.Application.Handlers.Products
{
    internal class GetProductsHandlerShould
    {
        private Mock<IProductsRepository> _productsRepository;
        private GetProductsHandler _getProductsHandler;

        [OneTimeSetUp]
        public void SetUp()
        {
            _productsRepository = new Mock<IProductsRepository>();
            _getProductsHandler = new GetProductsHandler(_productsRepository.Object);
        }

        [Test]
        public async Task Return_AllProducts()
        {
            var expectedProducts = new List<Product>()
            {
                new Product(1, "prod1", 10, ProductType.Burger),
                new Product(2, "prod2", 20, ProductType.Drink)
            };

            _productsRepository
                .Setup(p => p.GetAll())
                .ReturnsAsync(expectedProducts);

            var products = await _getProductsHandler.GetAll();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(products.Count, products.Count);

                foreach (var product in products)
                {
                    var expectedProduct = products.FirstOrDefault(p => p.Id == product.Id);

                    Assert.IsNotNull(expectedProduct);
                    Assert.AreEqual(expectedProduct.Description, product.Description);
                    Assert.AreEqual(expectedProduct.CookTime, product.CookTime);

                }
            });
        }
    }
}

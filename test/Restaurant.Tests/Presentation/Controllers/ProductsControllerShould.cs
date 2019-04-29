using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Restaurant.Api.Controllers;
using Restaurant.Application.Handlers.Products;
using Restaurant.Application.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.Tests.Presentation.Controllers
{
    public class ProductsControllerShould
    {
        private ProductsController _productsController;
        private Mock<IGetProductsHandler> _getProductsHandler;

        [OneTimeSetUp]
        public void SetUp()
        {
            _getProductsHandler = new Mock<IGetProductsHandler>();
            _productsController = new ProductsController(_getProductsHandler.Object);
        }

        [Test]
        public async Task Return_NoContent_When_NoProductsFound()
        {
            _getProductsHandler
                .Setup(g => g.GetAll())
                .ReturnsAsync(new List<GetProductsResponse>());

            var response = await _productsController.GetAll();

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
            var expectedProduct = new GetProductsResponse(1, "product1", 10);

            _getProductsHandler
                .Setup(g => g.GetAll())
                .ReturnsAsync(new List<GetProductsResponse> { expectedProduct });

            var response = await _productsController.GetAll();

            Assert.Multiple(() =>
            {
                var result = (OkObjectResult)response;
                var content = result.Value as List<GetProductsResponse>;

                Assert.IsNotNull(content);
                Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);

                var responseProduct = content.FirstOrDefault();
                Assert.IsNotNull(responseProduct);
                Assert.AreEqual(expectedProduct.Id, responseProduct.Id);
                Assert.AreEqual(expectedProduct.Description, responseProduct.Description);
                Assert.AreEqual(expectedProduct.CookTime, responseProduct.CookTime);
            });
        }
    }
}

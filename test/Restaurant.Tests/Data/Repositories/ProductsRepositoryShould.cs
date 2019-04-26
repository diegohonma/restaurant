using NUnit.Framework;
using Restaurant.Data.Repositories;
using System.IO;
using System.Threading.Tasks;

namespace Restaurant.Tests.Repositories
{
    public class ProductsRepositoryShould
    {
        [Test]
        public async Task Return_AllProducts()
        {
           var productsRepository = new ProductsRepository(
                Path.Combine(TestContext.CurrentContext.TestDirectory, "products.json"));

            var products = await productsRepository.GetAll();

            Assert.IsNotEmpty(products);
        }

        [Test]
        public async Task Return_NoProducts_When_EmptyJson()
        {
            var productsRepository = new ProductsRepository(
                Path.Combine(TestContext.CurrentContext.TestDirectory, "Data/empty_product.json"));

            var products = await productsRepository.GetAll();

            Assert.IsEmpty(products);
        }
    }
}

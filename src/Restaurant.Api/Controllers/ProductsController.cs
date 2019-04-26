using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Handlers.Products;
using System.Threading.Tasks;

namespace Restaurant.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGetProductsHandler _getProductsHandler;

        public ProductsController(IGetProductsHandler getProductsHandler)
        {
            _getProductsHandler = getProductsHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _getProductsHandler.GetAll();
            return products.Products.Count == 0 ? NoContent() : (IActionResult)Ok(products.Products);
        }
    }
}

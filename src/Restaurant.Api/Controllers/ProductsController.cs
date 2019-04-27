using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Handlers.Products;
using Restaurant.Application.Responses;
using System.Collections.Generic;
using System.Net;
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
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(List<GetProductsResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _getProductsHandler.GetAll();
            return products.Count == 0 ? NoContent() : (IActionResult)Ok(products);
        }
    }
}

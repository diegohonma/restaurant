using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Handlers.Orders;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrderHandler _createOrderHandler;

        public OrdersController(ICreateOrderHandler createOrderHandler)
        {
            _createOrderHandler = createOrderHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(List<int> productsId)
        {
            var created = await _createOrderHandler.Create(productsId);
            return created ? NoContent() : (IActionResult)BadRequest();
        }
    }
}

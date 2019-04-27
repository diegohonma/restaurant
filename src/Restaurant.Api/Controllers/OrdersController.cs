using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Handlers.Orders;
using Restaurant.Application.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrderHandler _createOrderHandler;
        private readonly IGetOrderHandler _getOrderHandler;

        public OrdersController(ICreateOrderHandler createOrderHandler, IGetOrderHandler getOrderHandler)
        {
            _createOrderHandler = createOrderHandler;
            _getOrderHandler = getOrderHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(List<int> productsId)
        {
            var created = await _createOrderHandler.Create(productsId);
            return created ? NoContent() : (IActionResult)BadRequest();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetOrdersViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        public IActionResult GetById(string id)
        {
            var found = _getOrderHandler.GetOrderById(id);
            return found.Orders.Count == 0 ? NotFound() : (IActionResult)Ok(found.Orders.First());
        }
    }
}

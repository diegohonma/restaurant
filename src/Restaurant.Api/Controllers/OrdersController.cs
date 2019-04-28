using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Handlers.Orders;
using Restaurant.Application.Requests;
using Restaurant.Application.Responses;
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
        private readonly IGetOrderHandler _getOrderHandler;

        public OrdersController(ICreateOrderHandler createOrderHandler, IGetOrderHandler getOrderHandler)
        {
            _createOrderHandler = createOrderHandler;
            _getOrderHandler = getOrderHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetOrdersResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<GetOrdersResponse>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateOrderRequest orderRequest)
        {
            var newOrder = await _createOrderHandler.Create(orderRequest);
            return newOrder.Value == null ? BadRequest(newOrder) : (IActionResult)Ok(newOrder.Value);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetOrdersResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        public IActionResult GetById(string id)
        {
            var found = _getOrderHandler.GetOrderById(id);
            return found == null ? NotFound() : (IActionResult)Ok(found);
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<GetOrdersResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        public IActionResult GetAll()
        {
            var orders = _getOrderHandler.GetAll();
            return orders.Count > 0 ? Ok(orders) : (IActionResult)NoContent();
        }
    }
}

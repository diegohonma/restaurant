using Restaurant.Application.Responses;
using Restaurant.CrossCutting;
using Restaurant.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Application.Handlers.Orders
{
    public class CreateOrderHandler : ICreateOrderHandler
    {
        private readonly IOrderService _orderService;

        public CreateOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrdersResponse> Create(List<int> productsId)
        {
            var order = await _orderService.Add(productsId);

            return order == null
                ? default(GetOrdersResponse)
                : new GetOrdersResponse(order.OrderId.ToString(), order.OrderStatus.GetDescription());
        }
    }
}

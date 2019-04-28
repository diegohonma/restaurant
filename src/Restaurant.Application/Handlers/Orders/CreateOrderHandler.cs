using Restaurant.Application.Requests;
using Restaurant.Application.Responses;
using Restaurant.CrossCutting;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Interfaces.Services;
using System.Linq;
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

        public async Task<GetOrdersResponse> Create(CreateOrderRequest order)
        {
            var newOrder = await _orderService.Add(
                order.Products.Select(p => new Product(p.Id, string.Empty, string.Empty, ProductType.None)).ToList());

            return newOrder == null
                ? default(GetOrdersResponse)
                : new GetOrdersResponse(newOrder.OrderId.ToString(), newOrder.OrderStatus.GetDescription());
        }
    }
}

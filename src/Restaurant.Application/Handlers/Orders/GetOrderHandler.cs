using Restaurant.Application.Responses;
using Restaurant.CrossCutting;
using Restaurant.Domain.Interfaces.Repositories;
using System;

namespace Restaurant.Application.Handlers.Orders
{
    public class GetOrderHandler : IGetOrderHandler
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public GetOrdersResponse GetOrderById(string id)
        {
            var order = _orderRepository.GetById(Guid.Parse(id));

            return order != null
                ? new GetOrdersResponse(order.OrderId.ToString(), order.OrderStatus.GetDescription())
                : default(GetOrdersResponse);

        }
    }
}

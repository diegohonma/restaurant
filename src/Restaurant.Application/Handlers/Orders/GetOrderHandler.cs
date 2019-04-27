using Restaurant.Application.Responses;
using Restaurant.CrossCutting;
using Restaurant.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

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

            return new GetOrdersResponse(order != null
                ? new List<GetOrdersViewModel> { new GetOrdersViewModel(order.OrderId.ToString(), order.OrderStatus.GetDescription()) }
                : new List<GetOrdersViewModel>());

        }
    }
}

using Restaurant.Application.Responses;
using Restaurant.CrossCutting;
using Restaurant.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<GetOrdersResponse> GetAll()
        {
            var orders = _orderRepository.GetAll();
            return orders.Select(o => 
                    new GetOrdersResponse(o.OrderId.ToString(), o.OrderStatus.GetDescription())).ToList();
        }
    }
}

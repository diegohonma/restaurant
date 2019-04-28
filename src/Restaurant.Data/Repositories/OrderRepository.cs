using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _listOrder;

        public OrderRepository(List<Order> listOrder)
        {
            _listOrder = listOrder;
        }

        public void Add(Order order)
            => _listOrder.Add(order);

        public Order GetById(Guid id)
            => _listOrder.FirstOrDefault(o => o.OrderId == id);

        public List<Order> GetByStatus(OrderStatus orderStatus)
            => _listOrder.Where(o => o.OrderStatus == orderStatus).ToList();

        public List<Order> GetAll()
            => _listOrder;
    }
}

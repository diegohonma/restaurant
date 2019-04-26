using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace Restaurant.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _listOrder;

        public OrderRepository(List<Order> listOrder)
        {
            _listOrder = listOrder;
        }

        public void Add(Order order) => _listOrder.Add(order);
    }
}

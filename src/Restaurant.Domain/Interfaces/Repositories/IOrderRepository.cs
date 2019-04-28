using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);

        Order GetById(Guid id);

        List<Order> GetByStatus(OrderStatus orderStatus);
    }
}

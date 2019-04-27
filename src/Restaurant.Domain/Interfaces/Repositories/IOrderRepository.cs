using Restaurant.Domain.Entities;
using System;

namespace Restaurant.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);

        Order GetById(Guid id);
    }
}

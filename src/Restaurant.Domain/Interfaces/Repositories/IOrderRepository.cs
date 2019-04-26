using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
    }
}

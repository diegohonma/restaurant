using Restaurant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Order> Add(List<int> productsId);
    }
}

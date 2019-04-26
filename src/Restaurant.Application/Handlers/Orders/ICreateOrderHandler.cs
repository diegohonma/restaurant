using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Application.Handlers.Orders
{
    public interface ICreateOrderHandler
    {
        Task<bool> Create(List<int> productsId);
    }
}

using Restaurant.Application.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Application.Handlers.Orders
{
    public interface ICreateOrderHandler
    {
        Task<GetOrdersResponse> Create(List<int> productsId);
    }
}

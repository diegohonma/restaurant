using Restaurant.Application.Requests;
using Restaurant.Application.Responses;
using System.Threading.Tasks;

namespace Restaurant.Application.Handlers.Orders
{
    public interface ICreateOrderHandler
    {
        Task<Response<GetOrdersResponse>> Create(CreateOrderRequest order);
    }
}

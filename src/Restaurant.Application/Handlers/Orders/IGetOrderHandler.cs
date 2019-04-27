using Restaurant.Application.Responses;

namespace Restaurant.Application.Handlers.Orders
{
    public interface IGetOrderHandler
    {
        GetOrdersResponse GetOrderById(string id);
    }
}

using Restaurant.Application.Responses;
using System.Collections.Generic;

namespace Restaurant.Application.Handlers.Orders
{
    public interface IGetOrderHandler
    {
        GetOrdersResponse GetOrderById(string id);

        List<GetOrdersResponse> GetAll();
    }
}

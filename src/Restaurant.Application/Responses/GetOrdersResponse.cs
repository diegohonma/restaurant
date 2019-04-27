using System.Collections.Generic;

namespace Restaurant.Application.Responses
{
    public class GetOrdersResponse
    {
        public List<GetOrdersViewModel> Orders { get; }

        public GetOrdersResponse(List<GetOrdersViewModel> orders)
        {
            Orders = orders;
        }
    }

    public class GetOrdersViewModel
    {
        public string OrderId { get; }

        public string OrderStatus { get;}

        public GetOrdersViewModel(string orderId, string orderStatus)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
        }
    }
}

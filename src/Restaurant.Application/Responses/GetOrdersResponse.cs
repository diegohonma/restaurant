namespace Restaurant.Application.Responses
{
    public class GetOrdersResponse
    {
        public string OrderId { get; }

        public string OrderStatus { get; }

        public GetOrdersResponse(string orderId, string orderStatus)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
        }
    }
}

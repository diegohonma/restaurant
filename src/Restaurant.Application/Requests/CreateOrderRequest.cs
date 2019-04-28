using System.Collections.Generic;

namespace Restaurant.Application.Requests
{
    public class CreateOrderRequest
    {
        public readonly List<CreateOrderProductsRequest> Products;

        public CreateOrderRequest(List<CreateOrderProductsRequest> products)
        {
            Products = products;
        }
    }

    public class CreateOrderProductsRequest
    {
        public readonly int Id;

        public CreateOrderProductsRequest(int id)
        {
            Id = id;
        }
    }
}

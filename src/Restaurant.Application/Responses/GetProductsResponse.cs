using System.Collections.Generic;

namespace Restaurant.Application.Responses
{
    public class GetProductsResponse
    {
        public List<GetProductsViewModel> Products { get; }

        public GetProductsResponse(List<GetProductsViewModel> products)
        {
            Products = products;
        }
    }

    public class GetProductsViewModel
    {
        public int Id { get; }

        public string Description { get; }

        public string CookTime { get; }

        public GetProductsViewModel(int id, string description, string cookTime)
        {
            Id = id;
            Description = description;
            CookTime = cookTime;
        }
    }
}

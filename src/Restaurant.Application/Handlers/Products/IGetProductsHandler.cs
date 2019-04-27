using Restaurant.Application.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Application.Handlers.Products
{
    public interface IGetProductsHandler
    {
        Task<List<GetProductsResponse>> GetAll();
    }
}

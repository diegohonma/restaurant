using Restaurant.Application.Responses;
using System.Threading.Tasks;

namespace Restaurant.Application.Handlers.Products
{
    public interface IGetProductsHandler
    {
        Task<GetProductsResponse> GetAll();
    }
}

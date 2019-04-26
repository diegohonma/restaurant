using Restaurant.Application.Responses;
using Restaurant.Domain.Interfaces.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Application.Handlers.Products
{
    public class GetProductsHandler : IGetProductsHandler
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductsHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<GetProductsResponse> GetAll()
        {
            var products = await _productsRepository.GetAll();

            return new GetProductsResponse(
                products.Select(p => new GetProductsViewModel(p.Id, p.Description, p.CookTime)).ToList());
        }
    }
}

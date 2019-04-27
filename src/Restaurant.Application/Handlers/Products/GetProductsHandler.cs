using Restaurant.Application.Responses;
using Restaurant.Domain.Interfaces.Repositories;
using System.Collections.Generic;
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

        public async Task<List<GetProductsResponse>> GetAll()
        {
            var products = await _productsRepository.GetAll();
            return products.Select(p => new GetProductsResponse(p.Id, p.Description, p.CookTime)).ToList();
        }
    }
}

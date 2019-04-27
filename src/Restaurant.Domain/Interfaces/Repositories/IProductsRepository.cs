using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Domain.Interfaces.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAll();

        Task<Product> GetById(int id);

        Task<List<Product>> GetByType(ProductType productType);
    }
}

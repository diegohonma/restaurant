using Restaurant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Domain.Interfaces.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAll();

        Task<Product> GetById(int id);
    }
}

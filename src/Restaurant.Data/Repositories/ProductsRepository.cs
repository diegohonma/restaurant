using Newtonsoft.Json;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly string _jsonFile;

        public ProductsRepository(string jsonFile)
        {
            _jsonFile = jsonFile;
        }

        public async Task<List<Product>> GetAll()
        {
            using (var sr = new StreamReader(_jsonFile))
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(await sr.ReadToEndAsync());
                return products ?? new List<Product>();
            }
        }

        public async Task<Product> GetById(int id) => (await GetAll()).FirstOrDefault(p => p.Id == id);
    }
}

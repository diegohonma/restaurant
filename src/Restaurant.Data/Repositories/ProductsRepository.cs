using Newtonsoft.Json;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using System.Collections.Generic;
using System.IO;
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
    }
}

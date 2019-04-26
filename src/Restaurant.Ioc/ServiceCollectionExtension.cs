using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Handlers.Products;
using Restaurant.Data.Repositories;
using Restaurant.Domain.Interfaces;
using System.IO;
using System.Reflection;

namespace Restaurant.Ioc
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterApplicationHandlers(services);
            //RegisterValidators(services);
            RegisterRepositories(services, configuration);
            //RegisterServices(services);
            //RegisterOptions(services, configuration);
        }

        private static void RegisterApplicationHandlers(IServiceCollection services)
        {
            services.AddScoped<IGetProductsHandler, GetProductsHandler>();
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductsRepository>(
                provider => new ProductsRepository(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "products.json")));
        }
    }
}

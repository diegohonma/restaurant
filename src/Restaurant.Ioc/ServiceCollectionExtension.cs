using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Handlers.Orders;
using Restaurant.Application.Handlers.Products;
using Restaurant.Data.Repositories;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces.Repositories;
using Restaurant.Domain.Interfaces.Services;
using Restaurant.Domain.Services;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Restaurant.Ioc
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterApplicationHandlers(services);
            RegisterRepositories(services, configuration);
            RegisterServices(services, configuration);
        }

        private static void RegisterApplicationHandlers(IServiceCollection services)
        {
            services.AddScoped<IGetProductsHandler, GetProductsHandler>();
            services.AddScoped<ICreateOrderHandler, CreateOrderHandler>();
            services.AddScoped<IGetOrderHandler, GetOrderHandler>();
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductsRepository>(
                provider => new ProductsRepository(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "products.json")));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton(new List<Order>());
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderService>(provider => 
                new OrderService(
                    provider.GetRequiredService<IProductsRepository>(), provider.GetRequiredService<IOrderRepository>(),
                    configuration.GetValue<int>("MaxOrdersKitchen")));
        }
    }
}

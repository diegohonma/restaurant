using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Interfaces.Repositories;
using Restaurant.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly int _maxOrdersKitchen;

        public OrderService(
            IProductsRepository productsRepository, IOrderRepository orderRepository,
            int maxOrdersKitchen)
        {
            _productsRepository = productsRepository;
            _orderRepository = orderRepository;
            _maxOrdersKitchen = maxOrdersKitchen;
        }

        public async Task<Entity<Order>> Add(List<Product> products)
        {
            if(_orderRepository.GetByStatus(OrderStatus.Started).Count >= _maxOrdersKitchen)
                return new Entity<Order>(default(Order), "Cozinha ocupada, tente novamente mais tarde.");

            var newOrder = new Order();
            var productsFound = await Task.WhenAll(products.Select(p => _productsRepository.GetById(p.Id)));

            if (productsFound.Count(p => p != null) != products.Count)
                return new Entity<Order>(default(Order), "Produto informado não pode ser encontrado.");

            productsFound.ToList()
                .ForEach(p => newOrder.AddNewProduct(p.Id, p.Description, p.CookTime, p.Type));

            if (newOrder.Products.Count(p => p.Type == ProductType.Burger) >= 2)
            {
                var freeDrink = (await _productsRepository.GetByType(ProductType.DrinkFree)).First();
                newOrder.AddNewProduct(freeDrink.Id, freeDrink.Description, freeDrink.CookTime, freeDrink.Type);
            }

            _orderRepository.Add(newOrder);

            return new Entity<Order>(newOrder, string.Empty);
        }

        public void FinishOrders()
        {
            var orders = _orderRepository.GetByStatus(OrderStatus.Started);

            orders.ForEach(o =>
            {
                if (DateTime.Now > o.OrderDate.AddSeconds(o.Products.Sum(p => Convert.ToInt32(p.CookTime))))
                    o.SetOrderStatus(OrderStatus.Finished);
            });
        }
    }
}

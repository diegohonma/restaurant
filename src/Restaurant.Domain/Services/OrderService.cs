using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Interfaces.Repositories;
using Restaurant.Domain.Interfaces.Services;
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

        public async Task<Order> Add(List<int> productsId)
        {
            if(_orderRepository.GetByStatus(OrderStatus.Started).Count >= _maxOrdersKitchen)
                return default(Order);

            var newOrder = new Order();
            var products = await Task.WhenAll(productsId.Select(p => _productsRepository.GetById(p)));

            if (products.Count(p => p != null) != productsId.Count)
                return default(Order);

            products.ToList()
                .ForEach(p => newOrder.AddNewProduct(p.Id, p.Description, p.CookTime, p.Type));

            if (newOrder.Products.Count(p => p.Type == ProductType.Burger) >= 2)
            {
                var freeDrink = (await _productsRepository.GetByType(ProductType.DrinkFree)).First();
                newOrder.AddNewProduct(freeDrink.Id, freeDrink.Description, freeDrink.CookTime, freeDrink.Type);
            }

            _orderRepository.Add(newOrder);

            return newOrder;
        }
    }
}

using Restaurant.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; }

        public OrderStatus OrderStatus { get; private set; }

        public DateTime OrderDate { get; }

        private readonly List<Product> _products;
        public IReadOnlyCollection<Product> Products => _products;

        public Order() : this(Guid.NewGuid(), OrderStatus.Started)
        {
            _products = new List<Product>();
            OrderDate = DateTime.Now;
        }

        public Order(Guid orderId, OrderStatus orderStatus)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            OrderDate = DateTime.Now;
        }

        public void AddNewProduct(int id, string description, int cookTime, ProductType type)
            =>_products.Add(new Product(id, description, cookTime, type));

        public void SetOrderStatus(OrderStatus orderStatus)
            => OrderStatus = orderStatus;
    }
}

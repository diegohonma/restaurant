﻿using Restaurant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Entity<Order>> Add(List<Product> products);

        void FinishOrders();
    }
}

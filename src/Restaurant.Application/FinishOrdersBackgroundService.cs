using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurant.Domain.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class FinishOrdersBackgroundService : IHostedService
    {
        private readonly IServiceProvider _services;
        private Timer _timer;

        public FinishOrdersBackgroundService(IServiceProvider services)
        {
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(FinishOrders, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void FinishOrders(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                orderService.FinishOrders();
            }
        }
    }
}

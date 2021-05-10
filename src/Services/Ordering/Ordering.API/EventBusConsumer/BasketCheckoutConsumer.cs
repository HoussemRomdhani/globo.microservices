using MassTransit;
using Messaging.Models;
using Microsoft.Extensions.Logging;
using Ordering.API.Models;
using Ordering.API.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly OrderingDbContext _context;
        private readonly ILogger<BasketCheckoutConsumer> _logger;
        public BasketCheckoutConsumer(OrderingDbContext context, ILogger<BasketCheckoutConsumer> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var message = context.Message;
            if (message != null)
            {
                var order = new Order(message);
                _context.Orders.Add(order);

                var products = message.Products.Select(x => new OrderProduct
                {
                    OrderProductId = Guid.NewGuid(),
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Order = order
                });

                _context.Products.AddRange(products);

                _context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}

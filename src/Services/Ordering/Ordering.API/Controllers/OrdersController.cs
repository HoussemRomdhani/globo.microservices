using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Dtos;
using Ordering.API.Repositories;
using System;
using System.Linq;

namespace Ordering.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderingDbContext _context;
        public OrdersController(OrderingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult Get(Guid userId)
        {
            var result = _context.Orders.Where(x => x.UserId == userId)
                                        .Include(x => x.Products)
                                        .Select(order => new OrderDto {
                                          Id = order.OrderId,
                                          UserId = order.UserId,
                                          TotalPrice = order.TotalPrice,
                                          CreatedAt = order.CreatedAt,
                                          Products = order.Products.Select(product => new OrderProductDto
                                          {
                                              Id = product.ProductId,
                                              ProductName = product.ProductName,
                                              Price = product.Price,
                                              Quantity = product.Quantity
                                          }).ToList()
                                        })
                                        .OrderByDescending(x => x.CreatedAt)
                                        .ToList();
                                       
            return Ok(result);
        }
    }
}

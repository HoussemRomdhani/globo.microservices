using System;
using System.Collections.Generic;

namespace Ordering.API.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<OrderProductDto> Products { get; set; }
    }
}

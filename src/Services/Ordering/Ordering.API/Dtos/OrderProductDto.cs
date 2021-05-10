using System;

namespace Ordering.API.Dtos
{
    public class OrderProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

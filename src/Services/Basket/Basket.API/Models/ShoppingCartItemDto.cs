using System;

namespace Basket.API.Models
{
    public class ShoppingCartItemDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

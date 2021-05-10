using System;
using System.Collections.Generic;

namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public Guid UserId { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

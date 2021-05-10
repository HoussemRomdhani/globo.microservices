using System;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Models
{
    public class OrderProduct
    {
        [Key]
        public Guid OrderProductId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}

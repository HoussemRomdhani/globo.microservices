using System;

namespace Catalog.API.Models
{
    public class ProductInputDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
    }
}

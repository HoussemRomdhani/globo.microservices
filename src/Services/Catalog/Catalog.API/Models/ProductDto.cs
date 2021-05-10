using Catalog.API.Entities;
using System;

namespace Catalog.API.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        //   public Category Category { get; set; }

        public ProductDto(Product item)
        {
            Id = item.ProductId;
            Name = item.Name;
            Price = item.Price;
            Description = item.Description;
            Image = item.Image;
            CategoryId = item.CategoryId;
        }
    }
}

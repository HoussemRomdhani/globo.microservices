using Catalog.API.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Entities
{
    public class Product
    {
        [Required]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }


        public Product()
        {
        }

        public Product(Guid id)
        {
            ProductId = id;
        }

        public Product(ProductInputDto item)
        {
            ProductId = Guid.NewGuid();
            Name = item.Name;
            Price = item.Price;
            Description = item.Description;
            Image = item.Image;
            CategoryId = item.CategoryId;
        }
    }
}
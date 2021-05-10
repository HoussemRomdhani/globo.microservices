using System;
using BackOffice.Client.Models;

namespace BackOffice.Client.ViewModels
{
    public class ProductForDisplayDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

        public ProductForDisplayDto(Product model)
        {
            Id = model.Id;
            Name = model.Name;
            Price = model.Price;
        }
    }
}

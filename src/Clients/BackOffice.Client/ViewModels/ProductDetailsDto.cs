using BackOffice.Client.Models;
using System;

namespace BackOffice.Client.ViewModels
{
    public class ProductDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }

        public ProductDetailsDto(Product model)
        {
            Id = model.Id;
            Name = model.Name;
            Price = model.Price;
            Description = model.Description;
            const string imgExtension = "png";
            Image = $"data:{imgExtension};base64,{model.Image}";
        }
    }
}

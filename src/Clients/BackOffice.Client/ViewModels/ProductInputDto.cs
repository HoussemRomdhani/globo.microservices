using BackOffice.Client.Extensions;
using System;

namespace BackOffice.Client.ViewModels
{
    public class ProductInputDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }

        public ProductInputDto(ProductForUpdateInputDto dto)
        {
            Name = dto.Name;
            Price = dto.Price;
            Description = dto.Description;
            Image = dto.Image?.ToBase64();
            CategoryId = Guid.TryParse(dto.CategoryId, out var tempId) ? tempId : default;
        }

        public ProductInputDto(ProductForCreationInputDto dto)
        {
            Name = dto.Name;
            Price = dto.Price;
            Description = dto.Description;
            Image = dto.Image?.ToBase64();
            CategoryId = Guid.TryParse(dto.CategoryId, out var tempId) ? tempId : default;
        } 
    }
}

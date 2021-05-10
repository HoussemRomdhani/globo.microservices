using BackOffice.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Client.ViewModels
{
    public class ProductForUpdateInputDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

       // [Required]
        public IFormFile Image { get; set; }

        public string CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public ProductForUpdateInputDto()
        {
                
        }

        public ProductForUpdateInputDto(Product model)
        {
            Id = model.Id;
            Name = model.Name;
            Price = model.Price;
            Description = model.Description;
            CategoryId = model.CategoryId.ToString();
        }
    }
}

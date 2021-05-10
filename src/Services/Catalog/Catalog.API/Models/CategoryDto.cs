using Catalog.API.Entities;
using System;

namespace Catalog.API.Models
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryDto(Category value)
        {
            Id = value.CategoryId;
            Name = value.Name;
        }
    }
}

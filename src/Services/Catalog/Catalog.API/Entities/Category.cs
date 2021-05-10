using Catalog.API.Models;
using System;
using System.Collections.Generic;

namespace Catalog.API.Entities
{
    public class Category
    {
      
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Category()
        {
        }

        public Category(Guid id)
        {
            CategoryId = id;
        }

        public Category(CategoryInputDto item)
        {
            CategoryId = Guid.NewGuid();
            Name = item.Name;
        }

    }
}

using Catalog.API.Models;
using System;
using System.Collections.Generic;

namespace Catalog.API.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> Get();
        CategoryDto GetById(Guid id);
        CategoryDto Create(CategoryInputDto item);
        void Update(Guid id, CategoryInputDto item);
        void Delete(Guid id);
    }
}

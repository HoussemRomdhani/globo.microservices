using Catalog.API.Entities;
using System;
using System.Collections.Generic;

namespace Catalog.API.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Get();
        Category GetById(Guid id);
        Category Add(Category item);
        void Update(Category itemIn);
        void Remove(Category itemIn);
    }
}

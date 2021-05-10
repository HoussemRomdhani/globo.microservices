using Catalog.API.Entities;
using System;
using System.Collections.Generic;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get(Guid? categoryId);
        Product GetById(Guid id);
        Product Add(Product item);
        void Update(Product item);
        void Remove(Product item);
    }
}

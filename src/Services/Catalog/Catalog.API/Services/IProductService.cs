using Catalog.API.Models;
using System;
using System.Collections.Generic;

namespace Catalog.API.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> Get(Guid? categoryId);
        ProductDto GetById(Guid id);
        ProductDto Create(ProductInputDto item);
        void Update(Guid id, ProductInputDto item);
        void Delete(Guid id);
    }
}

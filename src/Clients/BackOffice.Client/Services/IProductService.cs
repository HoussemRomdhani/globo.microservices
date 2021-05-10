using BackOffice.Client.Models;
using BackOffice.Client.ViewModels;
using System;
using System.Collections.Generic;

namespace BackOffice.Client.Services
{
    public interface IProductService
    {
        IEnumerable<Product> Get();
        Product GetById(Guid id);
        Product Create(ProductInputDto model);
        bool Update(Guid id, ProductInputDto model);
        bool Delete(Guid id);
    }
}

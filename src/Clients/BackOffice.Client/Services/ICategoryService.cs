using BackOffice.Client.Models;
using BackOffice.Client.ViewModels;
using System;
using System.Collections.Generic;

namespace BackOffice.Client.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> Get();
        Category GetById(Guid id);
        Category Create(CategoryInputDto model);
        bool Update(Guid id, CategoryInputDto model);
        bool Delete(Guid id);
    }
}

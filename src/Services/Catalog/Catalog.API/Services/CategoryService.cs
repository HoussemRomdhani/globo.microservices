using Catalog.API.Entities;
using Catalog.API.Models;
using Catalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public CategoryDto Create(CategoryInputDto item)
        {
            if (item == null)
                return null;

            var categoryEntity = _categoryRepository.Add(new Category(item));

            if (categoryEntity == null)
                return null;

            return new CategoryDto(categoryEntity);
        }

        public CategoryDto GetById(Guid id)
        {
            var categoryEntity = _categoryRepository.GetById(id);

            if (categoryEntity == null)
                return null;

            return new CategoryDto(categoryEntity);
        }

        public void Delete(Guid id)
        {
            var categoryEntity = _categoryRepository.GetById(id);

            if (categoryEntity != null)
                _categoryRepository.Remove(categoryEntity);
        }

        public void Update(Guid id, CategoryInputDto item)
        {
            var categoryEntity = _categoryRepository.GetById(id);

            if (categoryEntity != null)
            {
                 categoryEntity.Name = item.Name;
                _categoryRepository.Update(categoryEntity);
            }
        }

        public IEnumerable<CategoryDto> Get()
        {
            var categoryEntities = _categoryRepository.Get();
            return categoryEntities.Select(x => new CategoryDto(x)).ToList();
        }
    }
}

using Catalog.API.Entities;
using Catalog.API.Models;
using Catalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public ProductDto Create(ProductInputDto item)
        {
            if (item == null)
                return null;

            var entity = _productRepository.Add(new Product(item));

            if (entity == null)
                return null;

            return new ProductDto(entity);
        }

        public ProductDto GetById(Guid id)
        {
            var entity = _productRepository.GetById(id);

            if (entity == null)
                return null;

            return new ProductDto(entity);
        }

        public void Delete(Guid id)
        {
            var entity = _productRepository.GetById(id);

            if (entity != null)
                _productRepository.Remove(entity);
        }

        public void Update(Guid id, ProductInputDto item)
        {
            var entity = _productRepository.GetById(id);

            if (entity != null)
            {
                entity.Name = item.Name;
                entity.Price = item.Price;
                entity.Description = item.Description;
                entity.Image = item.Image;
                entity.CategoryId = item.CategoryId;
                _productRepository.Update(entity);
            }
        }

        public IEnumerable<ProductDto> Get(Guid? categoryId)
        {
            var entities = _productRepository.Get(categoryId);
            return entities.Select(x => new ProductDto(x)).ToList();
        }
    }
}

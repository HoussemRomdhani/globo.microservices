using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductCatalogDbContext _context;

        public ProductRepository(ProductCatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Product Add(Product itemIn)
        {
             _context.Products.Add(itemIn);
            _context.SaveChanges();
            return itemIn;
        }

        public IEnumerable<Product> Get(Guid? categoryId)
        {
            if (!categoryId.HasValue)
                return _context.Products.ToList();

            return _context.Products.Where(x => x.CategoryId == categoryId)
                                    .ToList();
        }

        public Product GetById(Guid id)
        {
            return _context.Products.Where(x => x.ProductId == id)
                                    .FirstOrDefault();
        }

        public void Remove(Product item)
        {
           _context.Products.Remove(item);
            _context.SaveChanges();

        }

        public void Update(Product item)
        {
            _context.Products.Update(item);
            _context.SaveChanges();
        }
    }
}

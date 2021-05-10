using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductCatalogDbContext _context;

        public CategoryRepository(ProductCatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Category Add(Category itemIn)
        {
            _context.Categories.Add(itemIn);
            _context.SaveChanges();
            return itemIn;
        }

        public IEnumerable<Category> Get()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(Guid id)
        {
            return _context.Categories.Where(x => x.CategoryId == id)
                                   .FirstOrDefault();
        }

        public void Remove(Category itemIn)
        {
           _context.Categories.Remove(itemIn);
            _context.SaveChanges();

        }

        public void Update(Category itemIn)
        {
            _context.Categories.Update(itemIn);
            _context.SaveChanges();
        }
    }
}

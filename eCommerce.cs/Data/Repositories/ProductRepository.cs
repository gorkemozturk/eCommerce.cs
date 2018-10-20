using eCommerce.cs.Data.Entities;
using eCommerce.cs.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Product FindWithProductTypesAndSpecialTags(int? id)
        {
            return _context.Set<Product>().Include(a => a.ProductType).Include(a => a.SpecialTag).SingleOrDefault(a => a.ProductID == id);
        }

        public IEnumerable<Product> GetAllWithProductTypesAndSpecialTags()
        {
            return _context.Set<Product>().Include(a => a.ProductType).Include(a => a.SpecialTag).ToList();
        }
    }
}

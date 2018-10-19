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

        public IEnumerable<Product> GetAllWithProductTypesAndSpecialTags()
        {
            return _context.Products.Include(a => a.ProductType).Include(a => a.SpecialTag);
        }
    }
}

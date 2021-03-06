﻿using eCommerce.cs.Data.Entities;
using eCommerce.cs.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Repositories
{
    public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<ProductType> GetAllWithProductTypes()
        {
            return _context.ProductTypes;
        }
    }
}

using eCommerce.cs.Data.Entities;
using eCommerce.cs.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Repositories
{
    public class SpecialTagRepository : Repository<SpecialTag>, ISpecialTagRepository
    {
        public SpecialTagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

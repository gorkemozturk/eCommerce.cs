using eCommerce.cs.Data.Entities;
using eCommerce.cs.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Order FindOrder(int? id)
        {
            return _context.Set<Order>().Where(o => o.OrderID == id).FirstOrDefault();
        }
    }
}

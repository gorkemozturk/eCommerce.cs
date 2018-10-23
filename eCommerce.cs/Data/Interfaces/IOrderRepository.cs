﻿using eCommerce.cs.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order FindOrder(int? id);
    }
}

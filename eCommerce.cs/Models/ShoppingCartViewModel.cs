using eCommerce.cs.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Models
{
    public class ShoppingCartViewModel
    {
        public List<Product> Products { get; set; }
        public Order Order { get; set; }
    }
}

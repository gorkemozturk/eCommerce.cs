using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Entities
{
    public class ProductType
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
    }
}

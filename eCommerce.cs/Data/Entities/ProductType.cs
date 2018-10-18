using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Entities
{
    public class ProductType
    {
        [Key]
        [DisplayName("Product ID")]
        public int ProductID { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
    }
}

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
        [DisplayName("Product Type ID")]
        public int ProductTypeID { get; set; }

        [Required]
        [DisplayName("Product Type Name")]
        public string ProductTypeName { get; set; }
    }
}

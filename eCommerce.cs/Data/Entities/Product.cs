using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Entities
{
    public class Product
    {
        [Key]
        [DisplayName("Product ID")]
        public int ProductID { get; set; }

        [DisplayName("Product Type")]
        public int ProductTypeID { get; set; }

        [DisplayName("Special Tag")]
        public int SpecialTagID { get; set; }

        [DisplayName("Product Name")]
        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; }

        [DisplayName("Available")]
        [Required]
        public bool IsAvailable { get; set; }

        [ForeignKey("ProductTypeID")]
        public virtual ProductType ProductType { get; set; }

        [ForeignKey("SpecialTagID")]
        public virtual SpecialTag SpecialTag { get; set; }
    }
}

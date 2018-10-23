using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Entities
{
    public class Order
    {
        [Key]
        [DisplayName("Product ID")]
        public int OrderID { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Phone Number")]
        public string CustomerPhoneNumber { get; set; }

        [DisplayName("Email")]
        public string CustomerEmail { get; set; }

        [DisplayName("Cost")]
        public decimal Cost { get; set; }
        public bool IsConfirmed { get; set; }
    }
}

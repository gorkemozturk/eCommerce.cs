using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.cs.Data.Entities
{
    public class SpecialTag
    {
        [Key]
        [DisplayName("Tag ID")]
        public int TagID { get; set; }

        [Required]
        [DisplayName("Tag Name")]
        public string TagName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class Product
    {
        [Key]
        public required string Product_Type { get; set; }

        public required string Product_Name { get; set; }

        public required int Price { get; set; }
    }
}

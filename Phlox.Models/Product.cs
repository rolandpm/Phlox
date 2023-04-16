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
        public required string Type { get; set; }

        public required string Name { get; set; }

        public required int Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class Item
    {
        [Key]
        public required Guid Item_Id { get; set; }

        [ForeignKey("User_Id")]
        public required Guid User_Id { get; set; }

        [ForeignKey("Product_Type")]
        public required string Product_Type { get; set; }

        public required string Item_Name { get; set; }

        public required int Quantity { get; set; }

        public string? Info { get; set; }
    }
}

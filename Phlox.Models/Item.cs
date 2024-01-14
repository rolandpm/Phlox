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
        public required Guid item_id { get; set; }

        [ForeignKey("User_Id")]
        public required Guid user_id { get; set; }

        [ForeignKey("Product_Type")]
        public required string product_type { get; set; }

        public required string item_name { get; set; }

        public required int quantity { get; set; }

        public string? info { get; set; }
    }
}

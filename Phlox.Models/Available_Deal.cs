using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class Available_Deal
    {
        [Key]
        [ForeignKey("Deal_Id")]
        public required Guid Deal_Id { get; set; }

        [Key]
        [ForeignKey("Item_Id")]
        public required Guid Item_Id { get; set; }
    }
}

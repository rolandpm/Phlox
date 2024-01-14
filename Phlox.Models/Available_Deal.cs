using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Phlox.Models
{
    [PrimaryKey(nameof(deal_id), nameof(item_id))]
    public class Available_Deal
    {
        [ForeignKey("Deal_Id")]
        public required Guid deal_id { get; set; }

        [ForeignKey("Item_Id")]
        public required Guid item_id { get; set; }
    }
}

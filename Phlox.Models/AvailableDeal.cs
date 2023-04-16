using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class AvailableDeal
    {
        [Key]
        [ForeignKey("Id")]
        public required Guid DealId { get; set; }

        [Key]
        [ForeignKey("Id")]
        public required Guid ItemId { get; set; }
    }
}

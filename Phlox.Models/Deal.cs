using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class Deal
    {
        [Key]
        public Guid deal_id { get; set; }

        public required string deal_name { get; set; }

        public int discount { get; set; }

        public string? info { get; set; }
    }
}

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
        public Guid Deal_Id { get; set; }

        public required string Deal_Name { get; set; }

        public int Discount { get; set; }

        public string? Info { get; set; }
    }
}

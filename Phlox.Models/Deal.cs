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
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required int Discount { get; set; }

        public string? Description { get; set; }
    }
}

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
        public required Guid Id { get; set; }

        [ForeignKey("Id")]
        public required Guid UserId { get; set; }

        [ForeignKey("Type")]
        public required string ProductType { get; set; }

        public required string Name { get; set; }

        public required int Quantity { get; set; }

        public string? Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class ItemPhoto
    {
        [Key]
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        [Required]
        public required byte[] Photo { get; set; }

        [JsonIgnore]
        public required Item Item { get; init; }
    }
}

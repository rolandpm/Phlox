using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Phlox.Models
{
    [PrimaryKey(nameof(ItemId), nameof(DealId))]
    public class ItemDeal
    {
        [ForeignKey(nameof(Item))]
        [Required]
        public required int ItemId { get; set; }

        [ForeignKey(nameof(Deal))]
        [Required]
        public required int DealId { get; set; }

        [JsonIgnore]
        public required Item Item { get; init; }

        [JsonIgnore]
        public required Deal Deal { get; init; }
    }
}

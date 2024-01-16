using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace Phlox.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int? Id { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public required int UserId { get; set; }

        [ForeignKey(nameof(Product))]
        [Required]
        public required int ProductId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required int Quantity { get; set; }

        public string? Info { get; set; }

        [JsonIgnore]
        public required Users User { get; init; }

        [JsonIgnore]
        public required Product Product { get; init; }

        [JsonIgnore]
        public List<ItemDeal>? ItemDeals { get; } = [];
    }
}

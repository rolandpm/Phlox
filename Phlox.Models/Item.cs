using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Phlox.Models
{
    [PrimaryKey(nameof(Id))]
    public class Item
    {
        //TODO: ToString
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int UserId { get; set; }

        [Required]
        public required int ProductId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required int Quantity { get; set; }

        public string? Info { get; set; }

        [JsonIgnore]
        public List<ItemDeal>? ItemDeals { get; } = new();

        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public Users? User { get; init; }

        [JsonIgnore]
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; init; }
    }

    public class ItemDTO
    {
        [JsonRequired]
        public int UserId { get; set; }

        [JsonRequired]
        public int ProductId { get; init; }

        [JsonRequired]
        public required string Name { get; set; }

        [JsonRequired]
        public required int Quantity { get; set; }

        public string? Info { get; set; }

        public Item ToModel()
        {
            return new Item
            {
                UserId = UserId,
                ProductId = ProductId,
                Name = Name,
                Quantity = Quantity,
                Info = Info
            };
        }
    }
}

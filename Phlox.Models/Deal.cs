using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
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
    [PrimaryKey(nameof(Id))]
    public class Deal
    {
        //TODO: ToString
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public int Discount { get; set; }

        public string? Info { get; set; }

        [JsonIgnore]
        public List<ItemDeal> ItemDeals { get; } = new();
    }

    public class DealDTO
    {
        [JsonRequired]
        public required string Name { get; set; }

        public int Discount { get; set; }

        public string? Info { get; set; }

        public Deal ToModel()
        {
            return new Deal
            {
                Name = Name,
                Discount = Discount,
                Info = Info
            };
        }
    }
}

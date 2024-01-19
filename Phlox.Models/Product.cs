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
    public class Product
    {
        //TODO: ToString
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] 
        public required string Type { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required int Price { get; set; }

        [JsonIgnore]
        public List<Item>? Items { get; } = new();
    }

    public class ProductDTO
    {
        [JsonRequired]
        public required string Type { get; set; }

        [JsonRequired]
        public required string Name { get; set; }

        [JsonRequired]
        public required int Price { get; set; }

        public Product ToModel()
        {
            return new Product
            {
                Type = Type,
                Name = Name,
                Price = Price
            };
        }
    }
}

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
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int? Id { get; set; }

        [Required] 
        public required string Type { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required int Price { get; set; }

        [JsonIgnore]
        public List<Item>? Items { get; } = [];
    }
}

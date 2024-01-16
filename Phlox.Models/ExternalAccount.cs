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
    public class ExternalAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int? Id { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public required int UserId { get; set; }

        [Required]
        public required string ServiceName { get; set; }

        [Required]
        public required string Nickname { get; set; }

        [JsonIgnore]
        public required Users User { get; init; }

    }
}

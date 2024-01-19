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
    public class ExternalAccount
    {
        //TODO: ToString
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int UserId { get; set; }

        [Required]
        public required string ServiceName { get; set; }

        [Required]
        public required string Nickname { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UserId))]
        public Users? User { get; init; }
    }

    public class ExternalAccountDTO
    {
        [JsonRequired]
        public int UserId { get; set; }

        [JsonRequired]
        public required string ServiceName { get; set; }

        [JsonRequired]
        public required string Nickname { get; set; }

        public ExternalAccount ToModel()
        {
            return new ExternalAccount
            {
                UserId = UserId,
                ServiceName = ServiceName,
                Nickname = Nickname
            };
        }
    }
}

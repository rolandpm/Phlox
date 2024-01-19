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
    public class Users
    {
        //TODO: ToString
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string Nickname { get; set; }

        public string? Email { get; set; }

        [JsonIgnore]
        public List<Item>? Items { get; } = new();

        [JsonIgnore]
        public List<ExternalAccount>? ExternalAccounts { get; } = new();
    }

    public class UsersDTO
    {
        [JsonRequired]
        public required string LastName { get; set; }

        [JsonRequired]
        public required string FirstName { get; set; }

        [JsonRequired]
        public required string Nickname { get; set; }

        public string? Email { get; set; }

        public Users ToModel()
        {
            return new Users
            {
                LastName = LastName,
                FirstName = FirstName,
                Nickname = Nickname,
                Email = Email
            };
        }
    }
}

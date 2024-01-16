﻿using Swashbuckle.AspNetCore.Annotations;
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
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int? Id { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string Nickname { get; set; }

        public string? Email { get; set; }

        [JsonIgnore]
        public List<Item>? Items { get; }

        [JsonIgnore]
        public List<ExternalAccount>? ExternalAccounts { get; } = [];
    }
}

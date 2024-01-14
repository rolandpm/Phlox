using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class Users
    {
        [Key]
        public required Guid user_id { get; set; }
        public required string last_name { get; set; }
        public required string first_name { get; set; }
        public required string nickname { get; set; }
        public string? email { get; set; }
    }
}

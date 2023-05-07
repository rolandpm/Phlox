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
        public required Guid User_Id { get; set; }
        public required string Last_Name { get; set; }
        public required string First_Name { get; set; }
        public required string Nickname { get; set; }
        public string? Email { get; set; }
    }
}

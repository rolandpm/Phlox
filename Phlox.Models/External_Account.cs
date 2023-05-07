using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class External_Account
    {
        [Key]
        public Guid External_Account_Id { get; set; }

        [ForeignKey("User_Id")]
        public Guid User_Id { get; set; }

        public required string Service_Name { get; set; }

        public required string Nickname { get; set; }

    }
}

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
        public Guid external_account_id { get; set; }

        [ForeignKey("User_Id")]
        public Guid user_id { get; set; }

        public required string service_name { get; set; }

        public required string nickname { get; set; }

    }
}

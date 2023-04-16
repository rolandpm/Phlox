using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class ExternalAccount
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public Guid UserId { get; set; }

        public required string ServiceName { get; set; }

        public required string Username { get; set; }

    }
}

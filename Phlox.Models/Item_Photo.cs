using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phlox.Models
{
    public class Item_Photo
    {
        [Key]
        [ForeignKey("Item_Id")]
        public Guid item_Id { get; set; }

        public required byte[] photo { get; set; }
    }
}

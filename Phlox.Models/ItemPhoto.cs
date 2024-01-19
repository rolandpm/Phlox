using Microsoft.EntityFrameworkCore;
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
    [PrimaryKey(nameof(ItemId))]
    public class ItemPhoto
    {
        //TODO: ToString
        public required int ItemId { get; set; }

        [Required]
        public required byte[] Photo { get; set; }

        [JsonIgnore]
        [ForeignKey("ItemId")]
        public Item? Item { get; init; }
    }

    public class ItemPhotoDTO
    {
        [JsonRequired]
        public required int ItemId { get; set; }

        [JsonRequired]
        public required byte[] Photo { get; set; }

        public ItemPhoto ToModel()
        {
            return new ItemPhoto
            {
                ItemId = ItemId,
                Photo = Photo
            };
        }
    }
}

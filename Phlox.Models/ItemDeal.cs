using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Phlox.Models
{
    [PrimaryKey(nameof(ItemId), nameof(DealId))]
    public class ItemDeal
    {
        //TODO: ToString
        public required int ItemId { get; set; }
        public required int DealId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ItemId))]
        public Item? Item { get; init; }

        [JsonIgnore]
        [ForeignKey(nameof(DealId))]
        public Deal? Deal { get; init; }
    }

    public class ItemDealDTO
    {
        [JsonRequired]
        public int ItemId { get; set; }

        [JsonRequired]
        public int DealId { get; set; }

        public ItemDeal ToModel()
        {
            return new ItemDeal
            {
                ItemId = ItemId,
                DealId = DealId
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class Food :BaseEntity
    {
        public string? Title { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }
        public string? PhotoType { get; set; }
        public bool IsAvailable { get; set; }
        public int  Discount { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}

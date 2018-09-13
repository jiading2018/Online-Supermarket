using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public GroceryType Type { get; set; }
        [Range(0, (double)Decimal.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, 1000)]
        public int AmountInStock { get; set; }

        //public ICollection<CartItem> Cart { get; set; }
    }

    public enum GroceryType
    {
        Cereal,
        CannedGoods,
        Bread,
        Candy
    }
}

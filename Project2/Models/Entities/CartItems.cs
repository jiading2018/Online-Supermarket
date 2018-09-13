using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models.Entities
{
    public class CartItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroceryType Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public int CartId { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}

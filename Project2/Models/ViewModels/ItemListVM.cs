using Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Models.ViewModels
{
    public class ItemListVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int AmountInStock { get; set; }
        
    }

}

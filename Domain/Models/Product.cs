using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
         public Product(string uid, string name, string description, int quantity, double price)
        {
            Uid = uid;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public string Uid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}

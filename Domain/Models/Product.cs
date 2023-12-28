using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
         public Product(string productId, string name, string description, int quantity, double price)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}

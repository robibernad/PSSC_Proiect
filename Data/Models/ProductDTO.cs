using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Models
{
    public class ProductDTO
    {
        public string ProductId { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "TEXT")]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}

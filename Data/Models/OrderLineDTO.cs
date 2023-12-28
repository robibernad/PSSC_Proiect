using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderLineDTO
    {
        public string OrderLineId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public string OrderHeaderId { get; set; }
        public string ProductId { get; set; }
    }
}

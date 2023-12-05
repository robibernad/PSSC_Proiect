using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderLineDTO
    {
        public string Uid { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public string OrderHeaderUid { get; set; }
        public string ProductUid { get; set; }
    }
}

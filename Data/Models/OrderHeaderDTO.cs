using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderHeaderDTO
    {
        public string OrderHeaderId { get; set; }
        public string ClientId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        public double Total { get; set; }
    }
}



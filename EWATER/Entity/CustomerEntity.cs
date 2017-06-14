using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWATER.Entity
{
    public class CustomerEntity
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public int TotalSales { get; set; }
    }
}
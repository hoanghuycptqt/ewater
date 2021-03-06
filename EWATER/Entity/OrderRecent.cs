﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWATER.Entity
{
    public class OrderRecent
    {
        public string OrderID { get; set; }
        public string CustomerName { get; set; }
        public int? StaffID { get; set; }
        public string StaffName { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public int TotalPrice { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

    }
}
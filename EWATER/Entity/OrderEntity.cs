using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWATER.Entity
{
    public class OrderEntity
    {
        public string OrderID { get; set; }
        public int CustomerID { get; set; }
        public int? StaffID { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
    public class OrderItem
    {
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
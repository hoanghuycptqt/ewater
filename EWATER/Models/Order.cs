using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EWATER.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public int CustomerID { get; set; }
        public int? StaffID { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
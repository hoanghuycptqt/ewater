using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EWATER.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }

    }
}
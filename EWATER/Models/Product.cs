using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWATER.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public byte[] Image { get; set; }

    }
}
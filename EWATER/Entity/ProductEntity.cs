using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWATER.Entity
{
    public class ProductEntity
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWATER.Entity
{
    public class StaffEntity
    {
        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public string StaffEmail { get; set; }
        public string StaffPhone { get; set; }
        public string JobTitle { get; set; }
        public int TotalSales { get; set; }
    }
}
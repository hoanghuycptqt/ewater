using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWATER.Models
{
    public class ContactClient
    {
        public int ContactClientID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
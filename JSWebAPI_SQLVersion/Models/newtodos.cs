using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSWebAPI_SQLVersion.Models
{
    public class newtodos
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public bool isDone { get; set; }
    }
}
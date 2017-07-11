using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSWebAPI_SQLVersion.Models
{
    public class Employeedetails
    {
        public int id_number { get; set; }
        public string given_name { get; set; }

        public string family_name { get; set; }

        public string preferred_name { get; set; }

        public string gender { get; set; }

    }
}
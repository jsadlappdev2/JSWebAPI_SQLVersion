using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSWebAPI_SQLVersion.Models
{
    public class ying_logs
    {
        public int id { get; set; }
        public string username { get; set; }

        public string appname { get; set; }
        public string modulename { get; set; }   

        public DateTime usestarttime { get; set; }

    }
}
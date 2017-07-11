using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSWebAPI_SQLVersion.Models
{
    public class appusers
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string UserMail { get; set; }

    }
}
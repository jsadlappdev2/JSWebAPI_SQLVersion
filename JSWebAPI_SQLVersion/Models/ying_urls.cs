using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSWebAPI_SQLVersion.Models
{
    public class ying_urls
    {
        public int id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public DateTime entrytime { get; set; }
        public string valid_flag { get; set; }

        public string icon { get; set; }

    }
}
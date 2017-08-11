using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSWebAPI_SQLVersion.Models
{
    public class sysapikey
    {

        public int id { get; set; }
        public string apikey_provider { get; set; }

        public string apikey_name { get; set; }
        public string apikey_value1 { get; set; }
        public string apikey_value2 { get; set; }
        public string endpoint_1 { get; set; }
        public string endpoint_2 { get; set; }
        public string ref_url { get; set; }
        public string is_free { get; set; }

        public string apply_email { get; set; }

        public string apply_password { get; set; }

        public string apply_info { get; set; }


        public DateTime apply_date { get; set; }

        public DateTime expire_date { get; set; }
        public string valid_flag { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSWebAPI_SQLVersion.Models
{
    public class filesinAWSS3
    {
        public int id { get; set; }
        public string bucket_name { get; set; }
        public string sub_folder { get; set; }
        public string file_orginal_name { get; set; }
        public string file_path { get; set; }
        public string file_saved_name { get; set; }

        public string flle_type { get; set; }

        public string file_url { get; set; }

        public string upload_person { get; set; }

        public DateTime upload_date { get; set; }

    
    }
}
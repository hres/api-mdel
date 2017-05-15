using System;

namespace mdelsWebApi.Models
{
    public class Establishment
    {
        public int establishment_id { get; set; }
        public int company_id { get; set; }
        public DateTime? entry_date { get; set; }
        public string application_type { get; set; }
        public string licence_status { get; set; }
        public DateTime? status_date { get; set; }
        public string imp_dist_class_I { get; set; }
        public string imp_dist_class_2_3_4 { get; set; }
        public string dist_flag { get; set; }
        public string class_I_flag { get; set; }
        public string imp_dist_class_II { get; set; }
        public string imp_dist_class_III { get; set; }
        public string imp_dist_class_IV { get; set; }
        public string dist_class_I { get; set; }
        public string dist_class_II { get; set; }
        public string dist_class_III { get; set; }
        public string dist_class_IV { get; set; }
        public string not_importer { get; set; }
        public string not_import_dist { get; set; }
        public string company_name { get; set; }
        public string addr_line_1 { get; set; }
        public string addr_line_2 { get; set; }
        public string addr_line_3 { get; set; }
        public string addr_line_4 { get; set; }
        public string addr_line_5 { get; set; }
        public string postal_code { get; set; }
        public string region_code { get; set; }
        public string city { get; set; }
        public string country_cd { get; set; }
        public string region_cd { get; set; }

    }
}
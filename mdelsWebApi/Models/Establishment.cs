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

    }
}
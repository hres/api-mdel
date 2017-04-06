using System;

namespace mdelsWebApi.Models
{
    public class Company
    {
        public int company_id { get; set; }
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
        public string company_status { get; set; }
        public DateTime? status_dt { get; set; }
    }
}
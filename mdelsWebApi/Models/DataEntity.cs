using System;
using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    public enum category
    {
        company = 1,
        id = 2,
        country = 3,
        province = 4
    }

    public class Search
    {
        public string country_name { get; set; }
        public string country_cd { get; set; }
        public int original_licence_no { get; set; }
        public String licence_status { get; set; }
        public int application_id { get; set; }
        public String licence_name { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string device_name { get; set; }
        public string import { get; set; }
        public string[] dist_Class = new string[4];
        public string region_cd { get; set; }
        public string region_desc { get; set; }
        public int establishment_id { get; set; }


    }

    public class Detail
    {
        public int establishment_id { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
}

    public class CompanyDetail
    {
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }

    }

    public class CountryDetail
    {
        public Company company;

        public IList<Company> companyList { get; set; }

        public IList<Establishment> establishmentList { get; set; }

    }

    public class EstablishmentDetail
    {
        public int establishment_id { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
    }

    public class provinceDetail
    {
        public Company company;
        public IList<Company> companyList { get; set; }
    }

}
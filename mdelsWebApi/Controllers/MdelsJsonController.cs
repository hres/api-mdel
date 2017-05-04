using System.Collections.Generic;
using System.Linq;
using mdelsWebApi.Models;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;
using System;
using System.Text;
using System.ComponentModel;

namespace mdelsWebApi.Controllers
{
    public class MdelsJsonController : Controller
    {
        public ActionResult GetAllListForJsonByCategory(string lang, string status, string term, int categoryType)
        {
            var companyResult = new List<Company>();
            var searchResult = new List<Search>();
            var companyController = new CompanyController();
            var establishmentController = new EstablishmentController();
            var countryController = new CountryController();
            var provinceController = new ProvinceController();

            var numberTerm = 0;

            if (!string.IsNullOrWhiteSpace(term))
            {
                numberTerm = UtilityHelper.GetNumberTerm(term);
            }

            switch (categoryType)
            {
                case (int)category.company:
                    if (numberTerm > 0)
                    {
                        //companyResult.Add(companyController.GetCompanyById(numberTerm, lang,status));
                        var company = new Company();
                        company = companyController.GetCompanyByID(numberTerm);
                        if (company.company_id != 0)
                        {
                            companyResult.Add(companyController.GetCompanyByID(numberTerm));
                        }
                    }
                    else
                    {
                        companyResult = companyController.GetAllCompany(term).ToList();
                    }
                    return Json(new { companyResult }, JsonRequestBehavior.AllowGet);

                case (int)category.establishment:
                    var establishmentResult = new List<Establishment>();

                    if (numberTerm > 0)
                    {
                        establishmentResult.Add(establishmentController.GetEstablishmentById(numberTerm));
                    }
                    else
                    {
                        establishmentResult = establishmentController.GetAllEstablishment(term).ToList();
                    }
                    if (establishmentResult.Count > 0)
                    {
                        foreach (var est in establishmentResult)
                        {
                            var search = new Search();
                            var company = new Company();
                            var address = new StringBuilder();
                            company = companyController.GetCompanyByID(est.company_id);
                            //search.establishment_id = est.establishment_id???

                            search.company_id = est.company_id;
                            search.original_licence_no = est.establishment_id;      //using orig licence no for now

                            if (company != null && company.company_id > 0)
                            {
                                search.company_name = company.company_name;
                                search.company_address = UtilityHelper.BuildAddress(company);
                            }
                            searchResult.Add(search);
                        }
                    }
                    return Json(new { searchResult }, JsonRequestBehavior.AllowGet);

                case (int)category.country:
                    var countryResult = new List<Country>();

                    if (numberTerm > 0)
                    {
                        countryResult.Add(countryController.GetCountryByID(numberTerm, lang));
                    }
                    else
                    {
                        countryResult = countryController.GetAllCountry(lang, term).ToList();
                    }
                    if (countryResult.Count > 0)
                    {
                        foreach (var c in countryResult)
                        {
                            var search = new Search();
                            var company = new Company();
                            var address = new StringBuilder();
                            //company = companyController.GetCompanyByID(); how to make this work? 

                            search.licence_status = c.country_cd;
                            search.licence_name = c.country_desc;
                            search.company_id = 555;

                            if (company != null && company.company_id > 0)
                            {
                                search.company_name = company.company_name;
                                search.company_address = UtilityHelper.BuildAddress(company);
                            }
                            searchResult.Add(search);
                        }
                    }
                    return Json(new { searchResult }, JsonRequestBehavior.AllowGet);

                case (int)category.province:
                    var provinceResult = new List<Province>();

                    if (numberTerm > 0)
                    {
                        provinceResult.Add(provinceController.GetProvinceByID(numberTerm, lang));
                    }
                    else
                    {
                        provinceResult = provinceController.GetAllProvince(lang, term).ToList();
                    }
                    if (provinceResult.Count > 0)
                    {
                        foreach (var p in provinceResult)
                        {
                            var search = new Search();
                            var company = new Company();
                            var address = new StringBuilder();
                            //company = companyController.GetCompanyByID(); how to make this work? 

                            search.licence_status = p.region_cd;
                            search.licence_name = p.region_desc;
                            search.device_name = p.country_cd;

                            if (company != null && company.company_id > 0)
                            {
                                search.company_name = company.company_name;
                                search.company_address = UtilityHelper.BuildAddress(company);
                            }
                            searchResult.Add(search);
                        }
                    }
                    return Json(new { searchResult }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { companyResult }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCompanyByIDForJson(int id)
        {
            var companyController = new CompanyController();
            var data = new CompanyDetail();

            //1. Get Company
            var company = new Company();
            company = companyController.GetCompanyByID(id);

            if (company != null && company.company_id > 0)
            {
                data.company_id = company.company_id; 
                data.company_name = company.company_name;
                data.company_address = UtilityHelper.BuildAddress(company);
            }

            var jsonResult = Json(new { data }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


    }
}

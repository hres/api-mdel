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
            var establishmentList = new List<Establishment>();
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

                    establishmentList = establishmentController.GetEstablishmentList(companyResult).ToList();

                    foreach (Establishment e in establishmentList)
                    {
                        Search s = new Search();
                        s.company_id = e.company_id;
                        s.company_name = e.company_name;
                        s.company_address = UtilityHelper.BuildAddress(e);
                        s.establishment_id = e.establishment_id;
                        searchResult.Add(s);
                    }

                    return Json(new { searchResult }, JsonRequestBehavior.AllowGet);

                case (int)category.id:

                    var sTerm = Int32.Parse(term);

                    var establishmentResult = new Establishment();
                    establishmentResult = establishmentController.GetEstablishmentById(sTerm);
                    Search search = new Search();

                    if (establishmentResult != null)
                    {
                        search.company_id = establishmentResult.company_id;
                        search.company_name = establishmentResult.company_name;
                        search.company_address = UtilityHelper.BuildAddress(establishmentResult);
                        search.establishment_id = establishmentResult.establishment_id;
                        searchResult.Add(search);
                    }
                    
                    return Json(new { searchResult }, JsonRequestBehavior.AllowGet);

                case (int)category.country:
                    companyResult = companyController.GetAllCompanyByLocation(term, "country").ToList();
                    establishmentList = establishmentController.GetEstablishmentList(companyResult).ToList();

                    if (establishmentList.Count > 0)
                    {

                        foreach (Establishment e in establishmentList)
                        {
                            Search s = new Search();
                            s.company_id = e.company_id;
                            s.company_name = e.company_name;
                            s.company_address = UtilityHelper.BuildAddress(e);
                            s.establishment_id = e.establishment_id;
                            searchResult.Add(s);
                        }
                    }
                    return Json(new { searchResult }, JsonRequestBehavior.AllowGet);

                case (int)category.province:

                    companyResult = companyController.GetAllCompanyByLocation(term, "province").ToList();
                    establishmentList = establishmentController.GetEstablishmentList(companyResult).ToList();

                    if (establishmentList.Count > 0)
                    {
                        foreach (Establishment e in establishmentList)
                        {
                            Search s = new Search();
                            s.company_id = e.company_id;
                            s.company_name = e.company_name;
                            s.company_address = UtilityHelper.BuildAddress(e);
                            s.establishment_id = e.establishment_id;
                            searchResult.Add(s);
                        }
                    }
                    return Json(new { searchResult }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { companyResult }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetEstablishmentByIDForJson([DefaultValue("0")] int id)
        {
            var est = new Establishment();
            var establishmentController = new EstablishmentController();
            var data = new EstablishmentDetail();

            est = establishmentController.GetEstablishmentById(id);
            data.company_id = est.company_id;
            data.company_name = est.company_name;
            data.company_address = UtilityHelper.BuildAddress(est);
            data.establishment_id = est.establishment_id;
            data.dist_class[0] = est.dist_class_I;
            data.dist_class[1] = est.dist_class_II;
            data.dist_class[2] = est.dist_class_III;
            data.dist_class[3] = est.dist_class_IV;
            data.import = est.not_importer;

            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult autoCompleteList([DefaultValue("company")] string category)
        {

            var list = new List<string>();

            switch (category)
            {
                case "company":
                    var companyList = new List<Company>();
                    var companyController = new CompanyController();
                    companyList = companyController.GetAllCompany("").ToList();
                    var estList = new List<Establishment>();
                    var estController = new EstablishmentController();
                    estList = estController.GetEstablishmentList(companyList).ToList();

                    foreach (Establishment e in estList)
                    {
                        list.Add(e.company_name);
                    }
                    break;

                case "country":
                    var countryList = new List<Country>();
                    var countryController = new CountryController();
                    countryList = countryController.GetAllCountry("en", "").ToList();
                    foreach (Country c in countryList)
                    {
                        list.Add(c.country_desc);
                    }
                    break;

                case "id":

                    var establishmentList = new List<Establishment>();
                    var establishmentController = new EstablishmentController();
                    establishmentList = establishmentController.GetAllEstablishment("").ToList();

                    foreach (Establishment e in establishmentList)
                    {
                        list.Add(e.establishment_id.ToString());
                        list.Add(e.company_id.ToString());
                    }

                    break;

                case "province":
                    var provinceList = new List<Province>();
                    var provinceController = new ProvinceController();
                    provinceList = provinceController.GetAllProvince("en", "").ToList();
                    foreach (Province p in provinceList)
                    {
                        list.Add(p.region_desc);
                    }
                    break;

            }

            var jsonResult = Json(new { list }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

    }

}
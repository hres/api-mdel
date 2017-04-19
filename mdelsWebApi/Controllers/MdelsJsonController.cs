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
            System.Diagnostics.Debug.WriteLine("Mdels JSON Controller");

            var companyResult = new List<Company>();
            var searchResult = new List<Search>();
            var companyController = new CompanyController();
            var numberTerm = 0;
            if (!string.IsNullOrWhiteSpace(term))
            {
                numberTerm = UtilityHelper.GetNumberTerm(term);
            }

            switch (categoryType)
            {
                case (int)category.company:
                    if ( numberTerm > 0)
                    {
                        //companyResult.Add(companyController.GetCompanyById(numberTerm, lang,status));
                        var company = new Company();
                        company = companyController.GetCompanyByID(numberTerm) ;
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
            }
            return  Json(new { companyResult }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCompanyByIDForJson(int id, [DefaultValue(0)] int licID, [DefaultValue(0)] int devID, string identifier, string lang, string status)
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

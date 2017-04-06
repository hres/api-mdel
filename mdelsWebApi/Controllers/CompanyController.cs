using mdelsWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
namespace mdelsWebApi.Controllers
{
    public class CompanyController : ApiController
    {
        static readonly ICompanyRepository databasePlaceholder = new CompanyRepository();

        public IEnumerable<Company> GetAllCompany()
        {

            return databasePlaceholder.GetAll();
        }


        public Company GetCompanyByID(int id)
        {
            Company company = databasePlaceholder.Get(id);
            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return company;
        }
    }
}

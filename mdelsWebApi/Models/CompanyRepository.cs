using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    public class CompanyRepository : ICompanyRepository
    {
        // We are using the list and _fakeDatabaseID to represent what would
    // most likely be a database of some sort, with an auto-incrementing ID field:
        private List<Company> companies = new List<Company>();
        private Company company = new Company();
        DBConnection dbConnection = new DBConnection("en");


        public IEnumerable<Company> GetAll(string company_name)
        {
            companies = dbConnection.GetAllCompany(company_name);
            return companies;
        }


        public Company Get(int id)
        {
            company = dbConnection.GetCompanyById(id);
            return company;
        }

        public IEnumerable<Company> GetAllCompanyByCountry(string cd)
        {
            companies = dbConnection.GetCompanyByCountry(cd);
            return companies;
        }

        public IEnumerable<Company> GetAllCompanyByProvince(string cd)
        {
            companies = dbConnection.GetCompanyByProvince(cd);
            return companies;
        }

    }
}
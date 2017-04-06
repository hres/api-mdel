using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Company Get(int id);
    }
}

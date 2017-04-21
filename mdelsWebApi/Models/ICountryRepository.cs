using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdelsWebApi.Models
{
    interface ICountryRepository
    {
        IEnumerable<Country> GetAll(string lang, string country);
        Country Get(int id, string lang);
    }
}

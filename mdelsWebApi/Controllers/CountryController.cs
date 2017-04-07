using mdelsWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace mdelsWebApi.Controllers
{
    public class CountryController : ApiController
    {
        static readonly ICountryRepository databasePlaceholder = new CountryRepository();

        public IEnumerable<Country> GetAllCountry(string lang)
        {
            return databasePlaceholder.GetAll(lang);
        }

        public Country GetCountryByID(int id, string lang)
        {
            Country country = databasePlaceholder.Get(id, lang);
            if (country == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return country;
        }

    }
}
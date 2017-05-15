using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    public class CountryRepository : ICountryRepository
    {
        private List<Country> countries = new List<Country>();
        private Country country = new Country();
        DBConnection dbConnection = new DBConnection("en");


        public IEnumerable<Country> GetAll(string lang, string country)
        {
            countries = dbConnection.GetAllCountry(lang, country);

            return countries;
        }

        /*
        public Country Get(string id, string lang)
        {
            country = dbConnection.GetCountryById(id, lang);
            return country;
        }
        */
    }
}
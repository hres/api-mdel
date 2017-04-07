using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    public class CountryRepository : ICountryRepository
    {
        private List<Country> countries = new List<Country>();
        private Country country = new Country();
        DBConnection dbConnection = new DBConnection("en");


        public IEnumerable<Country> GetAll(string lang)
        {
            countries = dbConnection.GetAllCountry(lang);

            return countries;
        }


        public Country Get(int id, string lang)
        {
            country = dbConnection.GetCountryById(id, lang);
            return country;
        }
    }
}
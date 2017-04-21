using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    public class ProvinceRepository : IProvinceRepository
    {
        // We are using the list and _fakeDatabaseID to represent what would
    // most likely be a database of some sort, with an auto-incrementing ID field:
        private List<Province> provinces = new List<Province>();
        private Province province = new Province();
    DBConnection dbConnection = new DBConnection("en");


    public IEnumerable<Province> GetAll(string lang, string province)
    {
            provinces = dbConnection.GetAllProvince(lang, province);

        return provinces;
    }


    public Province Get(int id, string lang)
    {
        province = dbConnection.GetProvinceById(id, lang);
        return province;
    }


    }
}
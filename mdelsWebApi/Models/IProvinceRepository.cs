using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    interface IProvinceRepository
    {
        IEnumerable<Province> GetAll(string lang, string province);
        //Province Get(string id, string lang);
    }
}

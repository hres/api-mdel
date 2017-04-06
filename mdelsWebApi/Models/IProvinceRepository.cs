using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    interface IProvinceRepository
    {
        IEnumerable<Province> GetAll(string lang);
        Province Get(int id, string lang);
    }
}

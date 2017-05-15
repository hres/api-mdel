using mdelsWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace mdelsWebApi.Controllers
{
    public class ProvinceController : ApiController
    {
        static readonly IProvinceRepository databasePlaceholder = new ProvinceRepository();

        public IEnumerable<Province> GetAllProvince(string lang, string province)
        {
            return databasePlaceholder.GetAll(lang, province);
        }

        /*
        public Province GetProvinceByID(string id, string lang)
        {
            Province province = databasePlaceholder.Get(id, lang);
            if (province == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return province;
        }
        */

    }
}
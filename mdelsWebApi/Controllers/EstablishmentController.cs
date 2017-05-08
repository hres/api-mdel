using mdelsWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
namespace mdelsWebApi.Controllers
{
    public class EstablishmentController : ApiController
    {
        static readonly IEstablishmentRepository databasePlaceholder = new EstablishmentRepository();

        public IEnumerable<Establishment> GetAllEstablishment(string establishmentName)
        {
            return databasePlaceholder.GetAll(establishmentName);
        }

        public Establishment GetEstablishmentById(int id)
        {
            Establishment establishment = databasePlaceholder.Get(id);
            if (establishment == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return establishment;
        }

        public IEnumerable<Establishment> GetEstablishmentList(IEnumerable<Company> companyList)
        {
            return databasePlaceholder.GetEstablishmentList(companyList);
        }

    }
}

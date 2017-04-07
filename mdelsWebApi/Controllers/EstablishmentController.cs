using mdelsWebApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
namespace mdelsWebApi.Controllers
{
    public class EstablishmentController : ApiController
    {
        static readonly IEstablishmentRepository databasePlaceholder = new EstablishmentRepository();

        public IEnumerable<Establishment> GetAllEstablishment()
        {
            return databasePlaceholder.GetAll();
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mdelsWebApi.Models
{
    public class EstablishmentRepository : IEstablishmentRepository
    {
        private List<Establishment> establishments = new List<Establishment>();
        private Establishment establishment = new Establishment();
        DBConnection dbConnection = new DBConnection("en");

        public IEnumerable<Establishment> GetAll(string establishment_id)
        {
            establishments = dbConnection.GetAllEstablishment(establishment_id);

            return establishments;
        }


        public Establishment Get(int id)
        {
            establishment = dbConnection.GetEstablishmentById(id);
            return establishment;
        }

        public IEnumerable<Establishment> GetEstablishmentList(IEnumerable<Company> companyList)
        {
            establishments = dbConnection.GetEstablishmentList(companyList);

            return establishments;
        }

    }
}
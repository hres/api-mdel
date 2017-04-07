using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdelsWebApi.Models
{
    interface IEstablishmentRepository
    {
        IEnumerable<Establishment> GetAll();
        Establishment Get(int id);
    }
}

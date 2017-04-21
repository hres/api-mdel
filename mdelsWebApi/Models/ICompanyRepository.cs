﻿using System.Collections.Generic;

namespace mdelsWebApi.Models
{
    interface ICompanyRepository
    {
        IEnumerable<Company> GetAll(string company_name);
        Company Get(int id);
    }
}

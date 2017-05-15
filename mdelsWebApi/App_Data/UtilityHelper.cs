﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using mdelsWebApi.Models;
using mdelsWebApi.Controllers;
using System.Text;

namespace mdelsWebApi
{
    /// <summary>
    /// Summary description for Common
    /// </summary>
    public static class UtilityHelper
    {
        public static void SetDefaultCulture(string lang)
        {
            if (lang == "en")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-CA");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-CA");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr-FR");
            }
        }


        public static List<T> ToNonNullList<T>(this IEnumerable<T> obj)
        {
            return obj == null ? new List<T>() : obj.ToList();
        }

        public static string BuildAddress(Company company)
        {
            var address = new StringBuilder();
            address.Append(company.addr_line_1).Append(" ");
            address.Append(company.addr_line_2).Append(" ");
            address.Append(company.addr_line_3).AppendLine();
            address.Append(company.city).Append(",");
            address.Append(company.region_cd).Append(",");
            address.Append(company.country_cd).Append(",");
            address.Append(company.postal_code);
            return address.ToString();
        }

        public static string BuildAddress(Establishment company)
        {
            var address = new StringBuilder();
            address.Append(company.addr_line_1).Append(" ");
            address.Append(company.addr_line_2).Append(" ");
            address.Append(company.addr_line_3).AppendLine();
            address.Append(company.city).Append(",");
            address.Append(company.region_cd).Append(",");
            address.Append(company.country_cd).Append(",");
            address.Append(company.postal_code);
            return address.ToString();
        }

        public static int GetNumberTerm(string term)
        {
            int result;
            bool res = int.TryParse(term, out result);
            if (res == false)
            {
                return 0;
            }
            else
            {
                return result;
            }
        }
    }
}
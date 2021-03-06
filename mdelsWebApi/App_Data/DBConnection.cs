﻿using mdelsWebApi.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace mdelsWebApi
{

    public class DBConnection
    {
        private string _lang;
        public string Lang
        {
            get { return this._lang; }
            set { this._lang = value; }
        }

        public DBConnection(string lang)
        {
            this._lang = lang;
        }

        private string mdelsDBConnection
        {
            get { return ConfigurationManager.ConnectionStrings["mdels"].ToString(); }
        }
    public List<Establishment> GetAllEstablishment(string establishment_id)
        {
            var items = new List<Establishment>();
            
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_ESTABLISHMENT INNER JOIN MDELS_OWNER.WQRY_EST_COMPANY ON MDELS_OWNER.WQRY_ESTABLISHMENT.COMPANY_ID = MDELS_OWNER.WQRY_EST_COMPANY.COMPANY_ID";

            if ((!string.IsNullOrEmpty(establishment_id)))
            {
                commandText += " WHERE UPPER(ESTABLISHMENT_ID) LIKE '%" + establishment_id + "%'";
            }

            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Establishment();
                                item.establishment_id = dr["ESTABLISHMENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ESTABLISHMENT_ID"]);
                                item.company_id = dr["COMPANY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["COMPANY_ID"]);
                                item.entry_date = dr["ENTRY_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ENTRY_DATE"]);
                                item.application_type = dr["APPLICATION_TYPE"] == DBNull.Value ? string.Empty : dr["APPLICATION_TYPE"].ToString().Trim();
                                item.licence_status = dr["LICENCE_STATUS"] == DBNull.Value ? string.Empty : dr["LICENCE_STATUS"].ToString().Trim();
                                item.status_date = dr["STATUS_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["STATUS_DATE"]);
                                item.imp_dist_class_I = dr["IMP_DIST_CLASS_I"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_I"].ToString().Trim();
                                item.imp_dist_class_I = dr["IMP_DIST_CLASS_2_3_4"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_2_3_4"].ToString().Trim();
                                item.dist_flag = dr["DIST_FLAG"] == DBNull.Value ? string.Empty : dr["DIST_FLAG"].ToString().Trim();
                                item.class_I_flag = dr["CLASS_I_FLAG"] == DBNull.Value ? string.Empty : dr["CLASS_I_FLAG"].ToString().Trim();
                                item.imp_dist_class_II = dr["IMP_DIST_CLASS_II"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_II"].ToString().Trim();
                                item.imp_dist_class_III = dr["IMP_DIST_CLASS_III"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_III"].ToString().Trim();
                                item.imp_dist_class_IV = dr["IMP_DIST_CLASS_IV"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_IV"].ToString().Trim();
                                item.dist_class_II = dr["DIST_CLASS_II"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_II"].ToString().Trim();
                                item.dist_class_III = dr["DIST_CLASS_III"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_III"].ToString().Trim();
                                item.dist_class_IV = dr["DIST_CLASS_IV"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_IV"].ToString().Trim();
                                item.not_importer = dr["NOT_IMPORTER"] == DBNull.Value ? string.Empty : dr["NOT_IMPORTER"].ToString().Trim();
                                item.not_import_dist = dr["NOT_IMPORT_DIST"] == DBNull.Value ? string.Empty : dr["NOT_IMPORT_DIST"].ToString().Trim();

                                item.company_name = dr["COMPANY_NAME"] == DBNull.Value ? string.Empty : dr["COMPANY_NAME"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_1"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_1"].ToString().Trim();
                                item.addr_line_2 = dr["ADDR_LINE_2"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_2"].ToString().Trim();
                                item.addr_line_3 = dr["ADDR_LINE_3"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_3"].ToString().Trim();
                                item.addr_line_4 = dr["ADDR_LINE_4"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_4"].ToString().Trim();
                                item.addr_line_5 = dr["ADDR_LINE_5"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_5"].ToString().Trim();
                                item.postal_code = dr["POSTAL_CODE"] == DBNull.Value ? string.Empty : dr["POSTAL_CODE"].ToString().Trim();
                                item.region_code = dr["REGION_CODE"] == DBNull.Value ? string.Empty : dr["REGION_CODE"].ToString().Trim();
                                item.city = dr["CITY"] == DBNull.Value ? string.Empty : dr["CITY"].ToString().Trim();
                                item.country_cd = dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim();
                                item.region_cd = dr["REGION_CD"] == DBNull.Value ? string.Empty : dr["REGION_CD"].ToString().Trim();

                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllEstablishment()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return items;
        }

        public List<Establishment> GetEstablishmentList(IEnumerable<Company> companyList)
        {

            var items = new List<Establishment>();
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_ESTABLISHMENT INNER JOIN MDELS_OWNER.WQRY_EST_COMPANY ON MDELS_OWNER.WQRY_ESTABLISHMENT.COMPANY_ID = MDELS_OWNER.WQRY_EST_COMPANY.COMPANY_ID";

            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Establishment();

                                item.establishment_id = dr["ESTABLISHMENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ESTABLISHMENT_ID"]);
                                item.company_id = dr["COMPANY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["COMPANY_ID"]);

                                var tempID = item.company_id;
                                foreach (Company c in companyList)
                                {
                                    if (tempID == c.company_id && item.establishment_id != 0)
                                    {
                                        item.entry_date = dr["ENTRY_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ENTRY_DATE"]);
                                        item.application_type = dr["APPLICATION_TYPE"] == DBNull.Value ? string.Empty : dr["APPLICATION_TYPE"].ToString().Trim();
                                        item.licence_status = dr["LICENCE_STATUS"] == DBNull.Value ? string.Empty : dr["LICENCE_STATUS"].ToString().Trim();
                                        item.status_date = dr["STATUS_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["STATUS_DATE"]);
                                        item.imp_dist_class_I = dr["IMP_DIST_CLASS_I"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_I"].ToString().Trim();
                                        item.imp_dist_class_I = dr["IMP_DIST_CLASS_2_3_4"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_2_3_4"].ToString().Trim();
                                        item.dist_flag = dr["DIST_FLAG"] == DBNull.Value ? string.Empty : dr["DIST_FLAG"].ToString().Trim();
                                        item.class_I_flag = dr["CLASS_I_FLAG"] == DBNull.Value ? string.Empty : dr["CLASS_I_FLAG"].ToString().Trim();
                                        item.imp_dist_class_II = dr["IMP_DIST_CLASS_II"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_II"].ToString().Trim();
                                        item.imp_dist_class_III = dr["IMP_DIST_CLASS_III"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_III"].ToString().Trim();
                                        item.imp_dist_class_IV = dr["IMP_DIST_CLASS_IV"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_IV"].ToString().Trim();
                                        item.dist_class_II = dr["DIST_CLASS_II"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_II"].ToString().Trim();
                                        item.dist_class_III = dr["DIST_CLASS_III"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_III"].ToString().Trim();
                                        item.dist_class_IV = dr["DIST_CLASS_IV"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_IV"].ToString().Trim();
                                        item.not_importer = dr["NOT_IMPORTER"] == DBNull.Value ? string.Empty : dr["NOT_IMPORTER"].ToString().Trim();
                                        item.not_import_dist = dr["NOT_IMPORT_DIST"] == DBNull.Value ? string.Empty : dr["NOT_IMPORT_DIST"].ToString().Trim();

                                        item.company_name = dr["COMPANY_NAME"] == DBNull.Value ? string.Empty : dr["COMPANY_NAME"].ToString().Trim();
                                        item.addr_line_1 = dr["ADDR_LINE_1"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_1"].ToString().Trim();
                                        item.addr_line_2 = dr["ADDR_LINE_2"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_2"].ToString().Trim();
                                        item.addr_line_3 = dr["ADDR_LINE_3"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_3"].ToString().Trim();
                                        item.addr_line_4 = dr["ADDR_LINE_4"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_4"].ToString().Trim();
                                        item.addr_line_5 = dr["ADDR_LINE_5"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_5"].ToString().Trim();
                                        item.postal_code = dr["POSTAL_CODE"] == DBNull.Value ? string.Empty : dr["POSTAL_CODE"].ToString().Trim();
                                        item.region_code = dr["REGION_CODE"] == DBNull.Value ? string.Empty : dr["REGION_CODE"].ToString().Trim();
                                        item.city = dr["CITY"] == DBNull.Value ? string.Empty : dr["CITY"].ToString().Trim();
                                        item.country_cd = dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim();
                                        item.region_cd = dr["REGION_CD"] == DBNull.Value ? string.Empty : dr["REGION_CD"].ToString().Trim();

                                        items.Add(item);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllEstablishment()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return items;

        }
        public Establishment GetEstablishmentById(int id)
        {
            Establishment establishment = new Establishment();
            string commandText = "";

            if (id >= 10000)
            {
                commandText += "SELECT * FROM MDELS_OWNER.WQRY_ESTABLISHMENT INNER JOIN MDELS_OWNER.WQRY_EST_COMPANY ON MDELS_OWNER.WQRY_ESTABLISHMENT.COMPANY_ID = MDELS_OWNER.WQRY_EST_COMPANY.COMPANY_ID WHERE MDELS_OWNER.WQRY_ESTABLISHMENT.COMPANY_ID = " + id;
            }
            else
            {
                commandText += "SELECT * FROM MDELS_OWNER.WQRY_ESTABLISHMENT INNER JOIN MDELS_OWNER.WQRY_EST_COMPANY ON MDELS_OWNER.WQRY_ESTABLISHMENT.COMPANY_ID = MDELS_OWNER.WQRY_EST_COMPANY.COMPANY_ID WHERE ESTABLISHMENT_ID = " + id;
            }

            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Establishment();
                                item.establishment_id = dr["ESTABLISHMENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ESTABLISHMENT_ID"]);
                                item.company_id = dr["COMPANY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["COMPANY_ID"]);
                                item.entry_date = dr["ENTRY_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ENTRY_DATE"]);
                                item.application_type = dr["APPLICATION_TYPE"] == DBNull.Value ? string.Empty : dr["APPLICATION_TYPE"].ToString().Trim();
                                item.licence_status = dr["LICENCE_STATUS"] == DBNull.Value ? string.Empty : dr["LICENCE_STATUS"].ToString().Trim();
                                item.status_date = dr["STATUS_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["STATUS_DATE"]);
                                item.imp_dist_class_I = dr["IMP_DIST_CLASS_I"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_I"].ToString().Trim();
                                item.imp_dist_class_I = dr["IMP_DIST_CLASS_2_3_4"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_2_3_4"].ToString().Trim();
                                item.dist_flag = dr["DIST_FLAG"] == DBNull.Value ? string.Empty : dr["DIST_FLAG"].ToString().Trim();
                                item.class_I_flag = dr["CLASS_I_FLAG"] == DBNull.Value ? string.Empty : dr["CLASS_I_FLAG"].ToString().Trim();
                                item.imp_dist_class_II = dr["IMP_DIST_CLASS_II"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_II"].ToString().Trim();
                                item.imp_dist_class_III = dr["IMP_DIST_CLASS_III"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_III"].ToString().Trim();
                                item.imp_dist_class_IV = dr["IMP_DIST_CLASS_IV"] == DBNull.Value ? string.Empty : dr["IMP_DIST_CLASS_IV"].ToString().Trim();
                                item.dist_class_II = dr["DIST_CLASS_II"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_II"].ToString().Trim();
                                item.dist_class_III = dr["DIST_CLASS_III"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_III"].ToString().Trim();
                                item.dist_class_IV = dr["DIST_CLASS_IV"] == DBNull.Value ? string.Empty : dr["DIST_CLASS_IV"].ToString().Trim();
                                item.not_importer = dr["NOT_IMPORTER"] == DBNull.Value ? string.Empty : dr["NOT_IMPORTER"].ToString().Trim();
                                item.not_import_dist = dr["NOT_IMPORT_DIST"] == DBNull.Value ? string.Empty : dr["NOT_IMPORT_DIST"].ToString().Trim();

                                item.company_name = dr["COMPANY_NAME"] == DBNull.Value ? string.Empty : dr["COMPANY_NAME"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_1"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_1"].ToString().Trim();
                                item.addr_line_2 = dr["ADDR_LINE_2"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_2"].ToString().Trim();
                                item.addr_line_3 = dr["ADDR_LINE_3"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_3"].ToString().Trim();
                                item.addr_line_4 = dr["ADDR_LINE_4"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_4"].ToString().Trim();
                                item.addr_line_5 = dr["ADDR_LINE_5"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_5"].ToString().Trim();
                                item.postal_code = dr["POSTAL_CODE"] == DBNull.Value ? string.Empty : dr["POSTAL_CODE"].ToString().Trim();
                                item.region_code = dr["REGION_CODE"] == DBNull.Value ? string.Empty : dr["REGION_CODE"].ToString().Trim();
                                item.city = dr["CITY"] == DBNull.Value ? string.Empty : dr["CITY"].ToString().Trim();
                                item.country_cd = dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim();
                                item.region_cd = dr["REGION_CD"] == DBNull.Value ? string.Empty : dr["REGION_CD"].ToString().Trim();

                                establishment = item;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetEstablishmentById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }

            return establishment;
        }

        public List<Company> GetAllCompany(string companyName)
        {
            var items = new List<Company>();
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_COMPANY";

            if ((!string.IsNullOrEmpty(companyName)))
            {
                commandText += " WHERE UPPER(COMPANY_NAME) LIKE '%" + companyName.ToUpper().Trim() + "%'";
            }
            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Company();
                                item.company_id = dr["COMPANY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["COMPANY_ID"]);
                                item.company_name = dr["COMPANY_NAME"] == DBNull.Value ? string.Empty : dr["COMPANY_NAME"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_1"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_1"].ToString().Trim();
                                item.addr_line_2 = dr["ADDR_LINE_2"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_2"].ToString().Trim();
                                item.addr_line_3 = dr["ADDR_LINE_3"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_3"].ToString().Trim();
                                item.addr_line_4 = dr["ADDR_LINE_4"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_4"].ToString().Trim();
                                item.addr_line_5 = dr["ADDR_LINE_5"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_5"].ToString().Trim();
                                item.postal_code = dr["POSTAL_CODE"] == DBNull.Value ? string.Empty : dr["POSTAL_CODE"].ToString().Trim();
                                item.region_code = dr["REGION_CODE"] == DBNull.Value ? string.Empty : dr["REGION_CODE"].ToString().Trim();
                                item.city = dr["CITY"] == DBNull.Value ? string.Empty : dr["CITY"].ToString().Trim();
                                item.country_cd = dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim();
                                item.region_cd = dr["REGION_CD"] == DBNull.Value ? string.Empty : dr["REGION_CD"].ToString().Trim();
                                item.company_status = dr["COMPANY_STATUS"] == DBNull.Value ? string.Empty : dr["COMPANY_STATUS"].ToString().Trim();
                                item.status_dt = dr["STATUS_DT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["STATUS_DT"]);
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllCompany()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return items;
        }

        public Company GetCompanyById(int id)
        {
            var company = new Company();
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_COMPANY WHERE COMPANY_ID = '" + id + "'";

            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())   //this line errors in getCountryById
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Company();
                                item.company_id = dr["COMPANY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["COMPANY_ID"]);
                                item.company_name = dr["COMPANY_NAME"] == DBNull.Value ? string.Empty : dr["COMPANY_NAME"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_1"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_1"].ToString().Trim();
                                item.addr_line_2 = dr["ADDR_LINE_2"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_2"].ToString().Trim();
                                item.addr_line_3 = dr["ADDR_LINE_3"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_3"].ToString().Trim();
                                item.addr_line_4 = dr["ADDR_LINE_4"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_4"].ToString().Trim();
                                item.addr_line_5 = dr["ADDR_LINE_5"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_5"].ToString().Trim();
                                item.postal_code = dr["POSTAL_CODE"] == DBNull.Value ? string.Empty : dr["POSTAL_CODE"].ToString().Trim();
                                item.region_code = dr["REGION_CODE"] == DBNull.Value ? string.Empty : dr["REGION_CODE"].ToString().Trim();
                                item.city = dr["CITY"] == DBNull.Value ? string.Empty : dr["CITY"].ToString().Trim();
                                item.country_cd = dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim();
                                item.region_cd = dr["REGION_CD"] == DBNull.Value ? string.Empty : dr["REGION_CD"].ToString().Trim();
                                item.company_status = dr["COMPANY_STATUS"] == DBNull.Value ? string.Empty : dr["COMPANY_STATUS"].ToString().Trim();
                                item.status_dt = dr["STATUS_DT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["STATUS_DT"]);

                                company = item;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetCompanyById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return company;
        }

        public List<Company> GetAllCompanyByLocation(string cd, string searchType, string lang)
        {
            var items = new List<Company>();
            string commandText = "";
            List<string> codes = getLocationCodeFromName(cd, searchType, lang);

            if (searchType == "province")
            {
                commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_COMPANY WHERE UPPER(REGION_CD) = '";
                commandText += codes[0] + "'";

                for (int i = 1; i < codes.Count; i++)
                {
                    commandText += " OR UPPER(REGION_CD) = '" + codes[i] + "'";
                }
            }
            else
            {
                commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_COMPANY WHERE UPPER(COUNTRY_CD) = '";
                commandText += codes[0] + "'";

                for (int i = 1; i < codes.Count; i++)
                {
                    commandText += " OR UPPER(COUNTRY_CD) = '" + codes[i] + "'";
                }
            }

            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Company();
                                item.company_id = dr["COMPANY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["COMPANY_ID"]);
                                item.company_name = dr["COMPANY_NAME"] == DBNull.Value ? string.Empty : dr["COMPANY_NAME"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_1"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_1"].ToString().Trim();
                                item.addr_line_2 = dr["ADDR_LINE_2"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_2"].ToString().Trim();
                                item.addr_line_3 = dr["ADDR_LINE_3"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_3"].ToString().Trim();
                                item.addr_line_4 = dr["ADDR_LINE_4"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_4"].ToString().Trim();
                                item.addr_line_5 = dr["ADDR_LINE_5"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_5"].ToString().Trim();
                                item.postal_code = dr["POSTAL_CODE"] == DBNull.Value ? string.Empty : dr["POSTAL_CODE"].ToString().Trim();
                                item.region_code = dr["REGION_CODE"] == DBNull.Value ? string.Empty : dr["REGION_CODE"].ToString().Trim();
                                item.city = dr["CITY"] == DBNull.Value ? string.Empty : dr["CITY"].ToString().Trim();
                                item.country_cd = dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim();
                                item.region_cd = dr["REGION_CD"] == DBNull.Value ? string.Empty : dr["REGION_CD"].ToString().Trim();
                                item.company_status = dr["COMPANY_STATUS"] == DBNull.Value ? string.Empty : dr["COMPANY_STATUS"].ToString().Trim();
                                item.status_dt = dr["STATUS_DT"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["STATUS_DT"]);
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetCompanyByCountry()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return items;
        }

        public List<Country> GetAllCountry(string lang, string country)
        {
            var items = new List<Country>();

            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_COUNTRY";

            if ((!string.IsNullOrEmpty(country)))
            {
                if (lang == "fr")
                {
                    commandText += " WHERE UPPER(COUNTRY_DESC_F) LIKE '%" + country.ToUpper().Trim() + "%'";    //get the french country name if language is french
                }
                else
                {
                    commandText += " WHERE UPPER(COUNTRY_DESC) LIKE '%" + country.ToUpper().Trim() + "%'";      //get the english country name otherwise
                }
            }

            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Country();
                                item.country_cd = dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim();
                                item.country_desc = dr["COUNTRY_DESC"] == DBNull.Value ? string.Empty : dr["COUNTRY_DESC"].ToString().Trim();
                                item.country_desc_f = dr["COUNTRY_DESC_F"] == DBNull.Value ? string.Empty : dr["COUNTRY_DESC_F"].ToString().Trim();
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllCountry()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return items;
        }

       
        public List<Province> GetAllProvince(string lang, string province)
        {
            var items = new List<Province>();
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_PROVINCE";

            if ((!string.IsNullOrEmpty(province)))
            {
                if (lang == "fr")
                {
                    commandText += " WHERE UPPER(REGION_DESC_F) LIKE '%" + province.ToUpper().Trim() + "%'";    //get the french province name if language is french
                }
                else
                {
                    commandText += " WHERE UPPER(REGION_DESC) LIKE '%" + province.ToUpper().Trim() + "%'";      //get the english province name otherwise
                }
            }

            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var item = new Province();
                                item.country_cd = dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim();
                                item.region_cd = dr["REGION_CD"] == DBNull.Value ? string.Empty : dr["REGION_CD"].ToString().Trim();
                                item.region_desc = dr["REGION_DESC"] == DBNull.Value ? string.Empty : dr["REGION_DESC"].ToString().Trim();
                                item.region_desc_f = dr["REGION_DESC_F"] == DBNull.Value ? string.Empty : dr["REGION_DESC_F"].ToString().Trim();
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetAllProvince()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return items;
        }

       
        private List<string> getLocationCodeFromName(string cd, string searchType, string lang)
        {
            List<string> codes = new List<string>();
            string commandText;

            codes.Add(cd);

            if (searchType == "province")
            {
                if(lang == "fr")
                {
                    commandText = "SELECT REGION_CD FROM WQRY_EST_PROVINCE WHERE UPPER(REGION_DESC_F) LIKE '%" + cd.ToUpper().Trim() + "%'";
                }
                else
                {
                    commandText = "SELECT REGION_CD FROM WQRY_EST_PROVINCE WHERE UPPER(REGION_DESC) LIKE '%" + cd.ToUpper().Trim() + "%'";
                }
                
            }
            else
            {
                if (lang == "fr")
                {
                    commandText = "SELECT COUNTRY_CD FROM WQRY_EST_COUNTRY WHERE UPPER(COUNTRY_DESC_F) LIKE '%" + cd.ToUpper().Trim() + "%'";
                }
                else
                {
                    commandText = "SELECT COUNTRY_CD FROM WQRY_EST_COUNTRY WHERE UPPER(COUNTRY_DESC) LIKE '%" + cd.ToUpper().Trim() + "%'";
                }
                
            }

            using (OracleConnection con = new OracleConnection(mdelsDBConnection))
            {
                OracleCommand cmd = new OracleCommand(commandText, con);
                try
                {
                    con.Open();
                    using (OracleDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (searchType == "province")
                                {
                                    codes.Add(dr["REGION_CD"] == DBNull.Value ? string.Empty : dr["REGION_CD"].ToString().Trim());
                                }
                                else
                                {
                                    codes.Add(dr["COUNTRY_CD"] == DBNull.Value ? string.Empty : dr["COUNTRY_CD"].ToString().Trim());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetCompanyByCountry()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return codes;
        }

    }
}

using mdelsWebApi.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

using System.Configuration;
using System.Data;

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


        public List<Company> GetAllCompany()
        {
            var items = new List<Company>();
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_COMPANY";
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
                                item.addr_line_1 = dr["ADDR_LINE_2"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_2"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_3"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_3"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_4"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_4"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_5"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_5"].ToString().Trim();
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
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_COMPANY WHERE COMPANY_ID = " + id;
            using (

                OracleConnection con = new OracleConnection(mdelsDBConnection))
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
                                item.addr_line_1 = dr["ADDR_LINE_2"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_2"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_3"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_3"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_4"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_4"].ToString().Trim();
                                item.addr_line_1 = dr["ADDR_LINE_5"] == DBNull.Value ? string.Empty : dr["ADDR_LINE_5"].ToString().Trim();
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

        public List<Province> GetAllProvince(string lang)
        {
            var items = new List<Province>();
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_PROVINCE";
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

        public Province GetProvinceById(int id, string lang)
        {
            var province = new Province();
            string commandText = "SELECT * FROM MDELS_OWNER.WQRY_EST_PROVINCE WHERE REGION_ID = " + id;
            using (

                OracleConnection con = new OracleConnection(mdelsDBConnection))
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
                          
                                province = item;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMessages = string.Format("DbConnection.cs - GetProvinceById()");
                    ExceptionHelper.LogException(ex, errorMessages);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            return province;
        }
    }
}

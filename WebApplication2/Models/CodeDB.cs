using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace WebApplication2.Models
{
    public class CodeDB
    {
        public SqlConnection con;

        public bool Open(string Connection = "DefaultConnection")
        {
            con = new SqlConnection(@WebConfigurationManager.ConnectionStrings[Connection].ToString());
        //checking for sql connection
           try
            {
                bool b = true;
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                return b;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }


        public bool Close()
        {
            try
            {
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public int ToInt(object s)
        {

            try
            {
                return Int32.Parse(s.ToString());
            }
            catch
            {
                return 0;
            }

         }

        public int DataInsert(string sql)
        { int LastID = 0;
            string query = sql + ";SELECT @@Identity;";
            try
            {
                if (con.State.ToString() == "Open")
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    LastID = this.ToInt(cmd.ExecuteScalar());
                }

                return this.ToInt(LastID);
            }

            catch
            {
                return 0;
            }
        }




    }
}